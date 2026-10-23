Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Resources
Imports System.Text
Imports System.Text.RegularExpressions

''' <summary>
''' BLADE 字体包安装窗体：加载 .dnfp / .json 并直接部署到当前 fonts 目录。
''' 导出功能在 FormOutput；打包/安装的公共分析逻辑通过 Shared 方法对外复用（FormMain 拖放安装）。
''' </summary>
Public Class FormInstall

#Region "── 内部类型 ────────────────────────────────────────────"

    ''' <summary>安装进度载体（供进度条与标题栏使用）。</summary>
    Private Class InstallProgress
        Public IsMarquee As Boolean
        Public Current As Integer
        Public Total As Integer
        Public Message As String
    End Class

    ''' <summary>包内容分析结果：待新增 / 待覆盖 / 待删除 文件清单。</summary>
    Private Class ScanResult
        Public NewJson As String
        Public ToAdd As New List(Of String)
        Public ToReplace As New List(Of String)
        Public ToDelete As New List(Of String)
        Public PackSize As Long
    End Class

#End Region

#Region "── 常量：目录结构 ──────────────────────────────────────"

    Private Shared ReadOnly AppRoot As String = Application.StartupPath
    Private Shared ReadOnly ManifestDir As String = Path.Combine(AppRoot, "manifest")
    Private Shared ReadOnly PendingDeletionsPath As String = Path.Combine(ManifestDir, "pendingdeletions.txt")
    Private Shared ReadOnly PendingCopiesPath As String = Path.Combine(ManifestDir, "pendingcopies.txt")
    Private Shared ReadOnly CacheDir As String = Path.Combine(AppRoot, "cache")

#End Region

#Region "── 公开属性 ────────────────────────────────────────────"

    ''' <summary>当前正在操作的字体目录路径（由 FormMain 传入）。</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property FontDir As String = ""

    ''' <summary>
    ''' 窗体首次显示时自动加载并安装的文件路径（由 FormMain 传入，用于「拖放 .dnfp 到 BLADE」场景）。
    ''' 使用后立即清空。
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property AutoInstallFile As String = ""

#End Region

#Region "── 私有字段 ────────────────────────────────────────────"

    ''' <summary>已加载的 .dnfp 路径。</summary>
    Private _loadedDnfpPath As String = ""

    ''' <summary>已加载的 .json 配置路径。</summary>
    Private _loadedJsonPath As String = ""

    ''' <summary>已加载文件的类型（"dnfp" / "json" / 空）。</summary>
    Private _loadedFileType As String = ""

    ''' <summary>本窗体本次会话内生成的图标，FormClosed 时统一 Dispose。</summary>
    Private ReadOnly _ownedIcons As New List(Of Image)

#End Region

#Region "── 窗体生命周期 ────────────────────────────────────────"

    Private Sub FormBlade_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EnsureDirectoriesExist()
        ApplyAwareIcons()

        ' 未传入有效字体目录时禁用安装相关按钮
        If String.IsNullOrEmpty(FontDir) OrElse Not Directory.Exists(FontDir) Then
            btnInstall.Enabled = False
            btnOpen.Enabled = False
        End If

        lstbDNFP.ItemHeight = FormMain.SharedItemHeight

        ' ListBox 双缓冲
        Dim dblProp = GetType(Control).GetProperty(
            "DoubleBuffered",
            Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
        If dblProp IsNot Nothing Then
            dblProp.SetValue(lstbDNFP, True, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' 首次显示时如带有 AutoInstallFile（.dnfp / .json），则自动加载并执行安装。
    ''' AutoInstallFile 使用后立即清空，避免重复触发。
    ''' </summary>
    Private Sub FormBlade_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If String.IsNullOrEmpty(AutoInstallFile) Then Return

        Dim f = AutoInstallFile
        AutoInstallFile = ""

        If Not File.Exists(f) Then Return

        Dim ext = Path.GetExtension(f).ToLower()
        Try
            If ext = ".dnfp" Then
                LoadPackage(f)
            ElseIf ext = ".json" Then
                ImportJsonConfig(f)
            Else
                Return
            End If

            If btnInstall.Enabled Then btnInstall.PerformClick()
        Catch ex As Exception
            MessageBox.Show($"自动加载安装文件失败：{ex.Message}", "安装", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>释放本次会话生成的图标。</summary>
    Private Sub FormInstall_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        For Each img In _ownedIcons
            Try : img.Dispose() : Catch : End Try
        Next
        _ownedIcons.Clear()
    End Sub

#End Region

#Region "── DPI 图标 ────────────────────────────────────────────"

    ''' <summary>
    ''' 从 Icons.resx 应用本窗体的图标。
    ''' 注：控件名 btnBroswe（Designer 既有拼写），此处沿用，勿改。
    ''' 资源缺失时静默跳过（控件保持 Designer 原图）。
    ''' </summary>
    Private Sub ApplyAwareIcons()
        Dim rm As ResourceManager
        Try
            rm = New ResourceManager("DNFE.Icons", GetType(FormMain).Assembly)
        Catch ex As MissingManifestResourceException
            Return
        End Try

        Dim map As New Dictionary(Of Button, String) From {
            {btnBroswe, "drag"}
        }

        AwareIcon.ApplyButtonIcons(Me, rm, map, _ownedIcons)
    End Sub

#End Region

#Region "── 加载字体包 / 配置 ────────────────────────────────────"

    Private Sub btnZipBroswe_Click(sender As Object, e As EventArgs) Handles btnBroswe.Click
        Using ofd As New OpenFileDialog
            ofd.Title = "选择「.dnfp 或 index.json」"
            ofd.Filter = "通用筛选 (*.dnfp;*.json)|*.dnfp;*.json|DDNet 字体包 (*.dnfp)|*.dnfp|JSON 配置 (*.json)|*.json"
            If ofd.ShowDialog(Me) <> DialogResult.OK Then Return

            Dim ext = Path.GetExtension(ofd.FileName).ToLower
            If ext = ".dnfp" Then
                LoadPackage(ofd.FileName)
            ElseIf ext = ".json" Then
                ImportJsonConfig(ofd.FileName)
            Else
                MessageBox.Show("不支持的文件类型。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Using
    End Sub

    Private Sub btnZipBroswe_DragEnter(sender As Object, e As DragEventArgs) Handles btnBroswe.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If files.Length = 1 Then
                Dim ext = Path.GetExtension(files(0)).ToLower
                If ext = ".dnfp" OrElse ext = ".zip" OrElse ext = ".json" Then
                    e.Effect = DragDropEffects.Copy
                    Return
                End If
            End If
        End If
        e.Effect = DragDropEffects.None
    End Sub

    Private Sub btnZipBroswe_DragDrop(sender As Object, e As DragEventArgs) Handles btnBroswe.DragDrop
        Dim files = CType(e.Data.GetData(DataFormats.FileDrop), String())
        If files Is Nothing OrElse files.Length = 0 Then Return
        Dim file = files(0)
        Dim ext = Path.GetExtension(file).ToLower
        If ext = ".dnfp" Then
            LoadPackage(file)
        ElseIf ext = ".json" Then
            ImportJsonConfig(file)
        Else
            MessageBox.Show("请拖放 .dnfp 或 .json 文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ''' <summary>
    ''' 加载并解析 .dnfp 字体包（仅读取元信息与文件清单，不执行安装）。
    ''' 校验：必须含 verification.txt 与 fonts/index.json。
    ''' </summary>
    Private Sub LoadPackage(filePath As String)
        ResetLoadState()

        If Not File.Exists(filePath) Then Return
        If Not Path.GetExtension(filePath).Equals(".dnfp", StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("仅支持 .dnfp 字体包。", "读取字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Dim fi As New FileInfo(filePath)

            Using archive As ZipArchive = ZipFile.OpenRead(filePath)
                Dim verEntry = archive.GetEntry("verification.txt")
                If verEntry Is Nothing Then
                    MessageBox.Show("字体包验证失败：缺少 verification.txt。", "读取字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    lblDNFP.Text = "等待数据加载"
                    Return
                End If

                Dim jsonEntry = archive.GetEntry("fonts/index.json")
                If jsonEntry Is Nothing Then
                    MessageBox.Show("字体包验证失败：缺少 index.json。", "读取字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    lblDNFP.Text = "等待数据加载"
                    Return
                End If

                ' 解析 verification.txt 键值对
                Dim verData As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                Using sr As New StreamReader(verEntry.Open(), Encoding.UTF8)
                    For Each line As String In sr.ReadToEnd().Split({vbCrLf, vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                        Dim sep As Integer = line.IndexOf("="c)
                        If sep > 0 Then
                            verData(line.Substring(0, sep).Trim()) = line.Substring(sep + 1).Trim()
                        End If
                    Next
                End Using

                ' 备注
                Dim noteVal As String = ""
                lblZipNote.Text = If(verData.TryGetValue("note", noteVal) AndAlso Not String.IsNullOrWhiteSpace(noteVal), noteVal, "-")

                ' 打包模式
                Dim modeVal As String = ""
                verData.TryGetValue("mode", modeVal)
                Dim modeStr As String = "-"
                Select Case modeVal.ToLower()
                    Case "normal" : modeStr = "默认打包"
                    Case "standard" : modeStr = "标准打包"
                    Case "full" : modeStr = "完整打包"
                    Case Else : modeStr = If(String.IsNullOrEmpty(modeVal), "-", modeVal)
                End Select

                ' 来源目录
                Dim sourceVal As String = ""
                verData.TryGetValue("source", sourceVal)
                Dim sourceStr As String = "-"
                Select Case sourceVal.ToLower()
                    Case "local" : sourceStr = "用户目录"
                    Case "compatible" : sourceStr = "兼容目录"
                    Case "install" : sourceStr = "安装目录"
                    Case Else : sourceStr = If(String.IsNullOrEmpty(sourceVal), "-", sourceVal)
                End Select

                lblZipType.Text = If(String.IsNullOrEmpty(modeStr) OrElse modeStr = "-", sourceStr, $"{modeStr}, {sourceStr}")

                ' 列出包内 fonts/ 下的文件
                lstbDNFP.Items.Clear()
                Dim fontCount As Integer = 0
                For Each entry As ZipArchiveEntry In archive.Entries
                    If entry.FullName.StartsWith("fonts/", StringComparison.OrdinalIgnoreCase) AndAlso
                       Not entry.FullName.EndsWith("/") Then
                        Dim fileName = entry.Name
                        lstbDNFP.Items.Add(fileName)
                        If Not fileName.Equals("index.json", StringComparison.OrdinalIgnoreCase) Then fontCount += 1
                    End If
                Next

                lblDNFP.Text = $"字体 {fontCount} 项, 配置 1 项"
            End Using

            lblZipName.Text = fi.Name
            lblZipSize.Text = FormatBytes(fi.Length)
            lblZipDate.Text = fi.LastWriteTime.ToString("yyyy/MM/dd HH:mm")
            _loadedDnfpPath = filePath
            _loadedFileType = "dnfp"

            ' 保留「安装」字样，附加文件名
            Me.Text = $"安装 - {fi.Name}"

            Dim hasFontDir As Boolean = Not String.IsNullOrEmpty(FontDir) AndAlso Directory.Exists(FontDir)
            btnInstall.Enabled = hasFontDir
            btnOpen.Enabled = hasFontDir
        Catch ex As Exception
            MessageBox.Show("读取字体包时发生错误：" & ex.Message, "读取字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>加载 index.json 配置文件（仅记录元信息，不执行安装）。</summary>
    Private Sub ImportJsonConfig(filePath As String)
        ResetLoadState()

        If Not File.Exists(filePath) Then
            MessageBox.Show("文件不存在。", "安装配置", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If Not Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("请选择 .json 配置文件。", "安装配置", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If String.IsNullOrEmpty(FontDir) OrElse Not Directory.Exists(FontDir) Then
            MessageBox.Show("字体目录无效，无法安装配置。", "安装配置", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim fi As New FileInfo(filePath)
            lstbDNFP.Items.Add("index.json")
            lblZipName.Text = fi.Name
            lblZipSize.Text = FormatBytes(fi.Length)
            lblZipDate.Text = fi.LastWriteTime.ToString("yyyy/MM/dd HH:mm")
            lblZipNote.Text = "-"
            lblZipType.Text = "JSON 配置"
            lblDNFP.Text = "配置 1 项"

            _loadedJsonPath = filePath
            _loadedFileType = "json"
            btnInstall.Enabled = True
            btnOpen.Enabled = True

            Me.Text = $"安装 - {fi.Name}"
        Catch ex As Exception
            MessageBox.Show("加载配置失败：" & ex.Message, "安装配置", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>重置加载状态与界面元信息（不改变字体目录）。</summary>
    Private Sub ResetLoadState()
        _loadedJsonPath = ""
        _loadedDnfpPath = ""
        _loadedFileType = ""
        lstbDNFP.Items.Clear()
        lblZipName.Text = "-"
        lblZipSize.Text = "-"
        lblZipDate.Text = "-"
        lblZipNote.Text = "-"
        lblZipType.Text = "-"
        btnInstall.Enabled = False
        btnOpen.Enabled = False

        ' 重置标题栏为纯「安装」
        Me.Text = "安装"
    End Sub

#End Region

#Region "── 核心安装逻辑（Shared，供 FormMain 复用） ────────────"

    ''' <summary>直接安装 .json 配置到目标字体目录（覆盖 index.json）。</summary>
    Public Shared Function InstallJsonConfigDirect(jsonPath As String, fontDir As String, owner As IWin32Window) As Boolean
        If String.IsNullOrEmpty(jsonPath) OrElse Not File.Exists(jsonPath) Then Return False
        If String.IsNullOrEmpty(fontDir) OrElse Not Directory.Exists(fontDir) Then
            MessageBox.Show(owner, "字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Dim confirm = MessageBox.Show(owner, "确认要覆盖当前配置吗？", "确认安装", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If confirm <> DialogResult.OK Then Return False

        Try
            File.Copy(jsonPath, Path.Combine(fontDir, "index.json"), True)
            MessageBox.Show(owner, "配置写入成功。", "安装配置", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            MessageBox.Show(owner, $"写入配置失败：{ex.Message}", "安装配置", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 直接安装 .dnfp 字体包（无进度条）。
    ''' 供 FormMain 拖放 .dnfp 到安装按钮时复用。
    ''' </summary>
    Public Shared Function InstallDnfpDirect(packagePath As String, fontDir As String, owner As IWin32Window) As Boolean
        If String.IsNullOrEmpty(packagePath) OrElse Not File.Exists(packagePath) Then Return False
        If String.IsNullOrEmpty(fontDir) OrElse Not Directory.Exists(fontDir) Then
            MessageBox.Show(owner, "字体目录无效或不存在，拒绝安装。", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Dim tempDir = Path.Combine(Path.GetTempPath(), "dnfe_import_" & Guid.NewGuid.ToString("N").Substring(0, 8))

        Try
            ' 解压
            If Directory.Exists(tempDir) Then Directory.Delete(tempDir, True)
            Directory.CreateDirectory(tempDir)
            ZipFile.ExtractToDirectory(packagePath, tempDir)

            Dim srcDir = Path.Combine(tempDir, "fonts")
            If Not Directory.Exists(srcDir) Then srcDir = tempDir

            ' 分析包内容
            Dim scanResult As ScanResult = AnalyzePackage(srcDir, fontDir, packagePath)
            If scanResult Is Nothing Then
                MessageBox.Show(owner, "字体包内未找到配置文件。", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            ' 确认对话框：列出新增 / 覆盖 / 删除项
            Using dlg As New DialogMessage()
                dlg.Text = "确认安装"
                dlg.TitleText = $"确认要安装「{Path.GetFileName(packagePath)}」吗？"
                dlg.DescriptionText = $"字体包大小: {FormatBytes(scanResult.PackSize)}"
                dlg.ConfirmText = "安装"
                dlg.CancelText = "取消"

                For Each f In scanResult.ToAdd
                    dlg.Items.Add($"[新增] {Path.GetFileName(f)}")
                Next
                For Each f In scanResult.ToReplace
                    dlg.Items.Add($"[覆盖] {Path.GetFileName(f)}")
                Next
                For Each f In scanResult.ToDelete
                    dlg.Items.Add($"[删除] {Path.GetFileName(f)}")
                Next

                If scanResult.ToAdd.Count = 0 AndAlso scanResult.ToReplace.Count = 0 AndAlso scanResult.ToDelete.Count = 0 Then
                    dlg.Items.Add("(没有需要操作的文件)")
                End If

                If dlg.ShowDialog(owner) <> DialogResult.OK Then Return False
            End Using

            ' 执行安装（Nothing 表示无进度条）
            ExecuteInstall(scanResult, fontDir, Nothing)

            ' 完成提示
            Dim hasPending As Boolean = (scanResult.ToAdd.Count > 0) OrElse (scanResult.ToReplace.Count > 0) OrElse (scanResult.ToDelete.Count > 0)
            If hasPending Then
                MessageBox.Show(owner, "字体包安装成功，下次启动时生效。", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(owner, "字体包安装成功。", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show(owner, "安装字体包时发生错误：" & ex.Message, "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Try
                If Directory.Exists(tempDir) Then Directory.Delete(tempDir, True)
            Catch
            End Try
        End Try
    End Function

    ''' <summary>
    ''' 异步安装（带进度条）。
    ''' 流程：后台解压 &amp; 分析 → UI 线程确认 → 后台执行安装 → UI 线程完成提示。
    ''' </summary>
    Private Shared Async Function InstallDnfpAsync(packagePath As String, fontDir As String,
                                                   owner As IWin32Window,
                                                   progress As IProgress(Of InstallProgress)) As Task(Of Boolean)
        Dim tempDir = Path.Combine(Path.GetTempPath(), "dnfe_import_" & Guid.NewGuid.ToString("N").Substring(0, 8))

        Try
            ' 1. 后台解压 + 分析
            Dim scanResult As ScanResult = Await Task.Run(Function()
                                                              If Directory.Exists(tempDir) Then Directory.Delete(tempDir, True)
                                                              Directory.CreateDirectory(tempDir)
                                                              ZipFile.ExtractToDirectory(packagePath, tempDir)

                                                              Dim srcDir = Path.Combine(tempDir, "fonts")
                                                              If Not Directory.Exists(srcDir) Then srcDir = tempDir

                                                              Return AnalyzePackage(srcDir, fontDir, packagePath)
                                                          End Function)

            If scanResult Is Nothing Then
                MessageBox.Show(owner, "字体包内未找到配置文件。", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            ' 2. UI 线程：确认对话框
            Using dlg As New DialogMessage()
                dlg.Text = "确认安装"
                dlg.TitleText = $"确认要安装「{Path.GetFileName(packagePath)}」吗？"
                dlg.DescriptionText = $"字体包大小: {FormatBytes(scanResult.PackSize)}"
                dlg.ConfirmText = "安装"
                dlg.CancelText = "取消"

                For Each f In scanResult.ToAdd
                    dlg.Items.Add($"[新增] {Path.GetFileName(f)}")
                Next
                For Each f In scanResult.ToReplace
                    dlg.Items.Add($"[覆盖] {Path.GetFileName(f)}")
                Next
                For Each f In scanResult.ToDelete
                    dlg.Items.Add($"[删除] {Path.GetFileName(f)}")
                Next

                If scanResult.ToAdd.Count = 0 AndAlso scanResult.ToReplace.Count = 0 AndAlso scanResult.ToDelete.Count = 0 Then
                    dlg.Items.Add("(没有需要操作的文件)")
                End If

                If dlg.ShowDialog(owner) <> DialogResult.OK Then Return False
            End Using

            ' 3. 后台执行文件复制
            Await Task.Run(Sub()
                               ExecuteInstall(scanResult, fontDir, progress)
                           End Sub)

            ' 4. 完成提示
            Dim hasPending As Boolean = (scanResult.ToAdd.Count > 0) OrElse (scanResult.ToReplace.Count > 0) OrElse (scanResult.ToDelete.Count > 0)
            If hasPending Then
                MessageBox.Show(owner, "字体包安装成功，下次启动时生效。", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(owner, "字体包安装成功。", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Return True
        Finally
            Try
                If Directory.Exists(tempDir) Then Directory.Delete(tempDir, True)
            Catch
            End Try
        End Try
    End Function

    ''' <summary>
    ''' 分析包内容与目标目录的差异，生成新增 / 覆盖 / 删除清单。
    ''' 删除规则：目标目录中既不在包内、也不在 index.json 引用列表、且不是 Font Awesome 的字体，视为多余。
    ''' </summary>
    Private Shared Function AnalyzePackage(srcDir As String, fontDir As String, packagePath As String) As ScanResult
        Dim jsonSrc = Path.Combine(srcDir, "index.json")
        If Not File.Exists(jsonSrc) Then jsonSrc = Path.Combine(srcDir, "index.JSON")
        If Not File.Exists(jsonSrc) Then Return Nothing

        Dim newJson = File.ReadAllText(jsonSrc, Encoding.UTF8)
        Dim referencedFiles As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim m = Regex.Match(newJson, """font files""\s*:\s*\[\s*(.*?)\s*\]", RegexOptions.Singleline)
        If m.Success Then
            For Each mm As Match In Regex.Matches(m.Groups(1).Value, """([^""]+)""")
                referencedFiles.Add(mm.Groups(1).Value)
            Next
        End If

        ' 包内字体文件
        Dim zipFonts As New List(Of String)
        zipFonts.AddRange(Directory.GetFiles(srcDir, "*.ttf"))
        zipFonts.AddRange(Directory.GetFiles(srcDir, "*.ttc"))
        zipFonts.AddRange(Directory.GetFiles(srcDir, "*.otf"))
        Dim zipFileNames As New HashSet(Of String)(zipFonts.Select(Function(f) Path.GetFileName(f)), StringComparer.OrdinalIgnoreCase)

        ' 目标目录当前字体
        Dim currentFonts As New List(Of String)
        currentFonts.AddRange(Directory.GetFiles(fontDir, "*.ttf"))
        currentFonts.AddRange(Directory.GetFiles(fontDir, "*.ttc"))
        currentFonts.AddRange(Directory.GetFiles(fontDir, "*.otf"))

        Dim res As New ScanResult With {.NewJson = newJson}

        ' 包内：目标已存在 → 覆盖；不存在 → 新增
        For Each f In zipFonts
            If File.Exists(Path.Combine(fontDir, Path.GetFileName(f))) Then
                res.ToReplace.Add(f)
            Else
                res.ToAdd.Add(f)
            End If
        Next

        ' 目标多余：既不在包内、也不在 index.json 引用里
        For Each f In currentFonts
            Dim fn = Path.GetFileName(f)
            If Not zipFileNames.Contains(fn) AndAlso Not referencedFiles.Contains(fn) Then
                If Not fn.Contains("Font_Awesome", StringComparison.OrdinalIgnoreCase) AndAlso
                   Not fn.Contains("FontAwesome", StringComparison.OrdinalIgnoreCase) Then
                    res.ToDelete.Add(f)
                End If
            End If
        Next

        Try
            res.PackSize = New FileInfo(packagePath).Length
        Catch
        End Try

        Return res
    End Function

    ''' <summary>
    ''' 执行安装：复制文件、写 index.json、记录失败项到 pending 清单（下次启动时重试）。
    ''' progress 为 Nothing 表示无进度条。
    ''' </summary>
    Private Shared Sub ExecuteInstall(scanResult As ScanResult, fontDir As String, progress As IProgress(Of InstallProgress))
        ' 清理上一次的 cache
        Directory.CreateDirectory(CacheDir)
        For Each ext In {"*.ttf", "*.ttc", "*.otf"}
            For Each oldFile In Directory.GetFiles(CacheDir, ext)
                Try : File.Delete(oldFile) : Catch : End Try
            Next
        Next
        Directory.CreateDirectory(ManifestDir)

        Dim totalSteps As Integer = scanResult.ToAdd.Count + scanResult.ToReplace.Count
        Dim step0 As Integer = 0

        ' toAdd：直接复制；失败则写缓存 + 记入 pending
        Dim addCopyLines As New List(Of String)
        For Each f In scanResult.ToAdd
            step0 += 1
            progress?.Report(New InstallProgress With {.IsMarquee = False, .Current = step0, .Total = totalSteps, .Message = $"复制 {Path.GetFileName(f)}"})

            Dim destPath = Path.Combine(fontDir, Path.GetFileName(f))
            Try
                File.Copy(f, destPath, False)
            Catch
                Dim cached = Path.Combine(CacheDir, Path.GetFileName(f))
                Try
                    File.Copy(f, cached, True)
                    addCopyLines.Add($"{cached}|{destPath}")
                Catch
                End Try
            End Try
        Next
        AppendPendingLines(PendingCopiesPath, addCopyLines)

        ' toReplace：缓存新文件，标记旧文件删除
        Dim replaceDeleteLines As New List(Of String)
        Dim replaceCopyLines As New List(Of String)
        For Each f In scanResult.ToReplace
            step0 += 1
            progress?.Report(New InstallProgress With {.IsMarquee = False, .Current = step0, .Total = totalSteps, .Message = $"覆盖 {Path.GetFileName(f)}"})

            Dim destPath = Path.Combine(fontDir, Path.GetFileName(f))
            Dim cached = Path.Combine(CacheDir, Path.GetFileName(f))
            Try
                File.Copy(f, cached, True)
                replaceDeleteLines.Add(destPath)
                replaceCopyLines.Add($"{cached}|{destPath}")
            Catch
            End Try
        Next
        AppendPendingLines(PendingDeletionsPath, replaceDeleteLines)
        AppendPendingLines(PendingCopiesPath, replaceCopyLines)

        ' toDelete：标记删除
        AppendPendingLines(PendingDeletionsPath, scanResult.ToDelete)

        ' 写入 index.json
        File.WriteAllText(Path.Combine(fontDir, "index.json"), scanResult.NewJson, Encoding.UTF8)
    End Sub

#End Region

#Region "── 安装按钮事件 ────────────────────────────────────────"

    Private Async Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        ' 已加载 .json：直接走配置覆盖
        If Not String.IsNullOrEmpty(_loadedJsonPath) AndAlso File.Exists(_loadedJsonPath) Then
            If InstallJsonConfigDirect(_loadedJsonPath, FontDir, Me) Then ResetLoadState()
            Return
        End If

        If String.IsNullOrEmpty(_loadedDnfpPath) OrElse Not File.Exists(_loadedDnfpPath) Then
            MessageBox.Show(Me, "未加载字体包。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim origTitle = Text
        btnInstall.Enabled = False
        btnOpen.Enabled = False
        btnBroswe.Enabled = False

        Dim progress As New Progress(Of InstallProgress)(
            Sub(p)
                If IsDisposed OrElse Disposing Then Return
                ProgressBar1.Visible = True
                If p.IsMarquee Then
                    ProgressBar1.Style = ProgressBarStyle.Marquee
                Else
                    ProgressBar1.Style = ProgressBarStyle.Continuous
                    ProgressBar1.Maximum = Math.Max(1, p.Total)
                    ProgressBar1.Value = Math.Min(p.Current, ProgressBar1.Maximum)
                End If

                If Not String.IsNullOrEmpty(p.Message) Then
                    ' 截断超长路径
                    Dim displayMsg = p.Message
                    If displayMsg.Length > 40 Then displayMsg = "..." & displayMsg.Substring(displayMsg.Length - 37)

                    If p.Total > 0 AndAlso Not p.IsMarquee Then
                        Text = $"{origTitle} - {displayMsg} ({p.Current}/{p.Total})"
                    Else
                        Text = $"{origTitle} - {displayMsg}"
                    End If
                End If
            End Sub)

        ProgressBar1.Visible = True
        ProgressBar1.Style = ProgressBarStyle.Marquee
        ProgressBar1.Value = 0

        Try
            Dim ok = Await InstallDnfpAsync(_loadedDnfpPath, FontDir, Me, progress)
            If ok Then ResetLoadState()
        Catch ex As Exception
            MessageBox.Show($"安装失败：{ex.Message}", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnInstall.Enabled = True
            btnOpen.Enabled = True
            btnBroswe.Enabled = True
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0
            Text = origTitle
        End Try
    End Sub

#End Region

#Region "── 打开按钮事件 ────────────────────────────────────────"

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        If _loadedFileType = "json" Then
            If Not String.IsNullOrEmpty(_loadedJsonPath) AndAlso File.Exists(_loadedJsonPath) Then
                Try
                    Process.Start(New ProcessStartInfo(_loadedJsonPath) With {.UseShellExecute = True})
                Catch ex As Exception
                    MessageBox.Show($"无法打开文件：{ex.Message}", "打开文件", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                MessageBox.Show("JSON 文件不存在或已移动。", "打开文件", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        ElseIf _loadedFileType = "dnfp" Then
            If String.IsNullOrEmpty(_loadedDnfpPath) Then
                MessageBox.Show("数据尚未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Process.Start("explorer.exe", $"""{_loadedDnfpPath}""")
        Else
            MessageBox.Show("未加载任何数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

#End Region

#Region "── 列表自绘 ────────────────────────────────────────────"

    Private Sub lstbBlade_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstbDNFP.DrawItem
        If e.Index < 0 OrElse e.Index >= lstbDNFP.Items.Count Then Return
        e.DrawBackground()

        Dim itemText = lstbDNFP.Items(e.Index).ToString
        Dim textColor = If((e.State And DrawItemState.Selected) <> 0, SystemColors.HighlightText, Color.Black)

        Dim textY = e.Bounds.Y + (e.Bounds.Height - e.Font.Height) \ 2
        Dim textRect As New Rectangle(e.Bounds.X + 4, textY, e.Bounds.Width - 8, e.Font.Height)

        TextRenderer.DrawText(e.Graphics, itemText, e.Font, textRect, textColor, TextFormatFlags.Left Or TextFormatFlags.NoPrefix)

        ' 右侧序号
        Dim tag = $"{e.Index + 1}"
        Using tagFont As New Font("Consolas", 7.5F, FontStyle.Regular, GraphicsUnit.Point)
            Using tagColor As New SolidBrush(If((e.State And DrawItemState.Selected) <> 0, Color.FromArgb(200, 255, 255, 255), Color.Gray))
                Dim tagSize = e.Graphics.MeasureString(tag, tagFont)
                e.Graphics.DrawString(tag, tagFont, tagColor,
                    New PointF(e.Bounds.Right - tagSize.Width - 4, e.Bounds.Y + (e.Bounds.Height - tagSize.Height) / 2 + 1))
            End Using
        End Using
        e.DrawFocusRectangle()
    End Sub

#End Region

#Region "── 通用辅助 ────────────────────────────────────────────"

    ''' <summary>确保应用运行所需的子目录存在（ManifestDir / CacheDir）。</summary>
    Private Sub EnsureDirectoriesExist()
        For Each dir As String In {ManifestDir, CacheDir}
            If Not Directory.Exists(dir) Then
                Try
                    Directory.CreateDirectory(dir)
                Catch
                End Try
            End If
        Next
    End Sub

    ''' <summary>把若干行追加到 pending 文本文件（自动去重、忽略空行）。</summary>
    Private Shared Sub AppendPendingLines(path As String, lines As IEnumerable(Of String))
        If lines Is Nothing Then Return
        Dim arr = lines.Where(Function(l) Not String.IsNullOrWhiteSpace(l)).ToList()
        If arr.Count = 0 Then Return
        Try
            Dim existing As New List(Of String)
            If File.Exists(path) Then
                existing.AddRange(File.ReadAllLines(path).Where(Function(l) Not String.IsNullOrWhiteSpace(l)))
            End If
            existing.AddRange(arr)
            File.WriteAllLines(path, existing.Distinct(StringComparer.OrdinalIgnoreCase))
        Catch
        End Try
    End Sub

    Private Shared Function FormatBytes(bytes As Long) As String
        If bytes >= 1024L * 1024 * 1024 Then Return (bytes / 1024 / 1024 / 1024).ToString("F2") & " GB"
        If bytes >= 1024L * 1024 Then Return (bytes / 1024 / 1024).ToString("F2") & " MB"
        If bytes >= 1024 Then Return (bytes / 1024).ToString("F0") & " KB"
        Return bytes.ToString() & " B"
    End Function

#End Region

#Region "── 快捷键 ──────────────────────────────────────────────"

    ''' <summary>按 Esc 关闭（若 ComboBox 处于下拉状态则先收起下拉）。</summary>
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            Dim cbo = TryCast(ActiveControl, ComboBox)
            If cbo IsNot Nothing AndAlso cbo.DroppedDown Then Return MyBase.ProcessCmdKey(msg, keyData)
            Me.Close()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

#End Region

End Class