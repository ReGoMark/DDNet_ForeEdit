Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions

''' <summary>
''' BLADE 字体包安装窗体：加载 .dnfp / .json 并直接部署到当前 fonts 目录。 导出功能在 FormOutput。
''' </summary>
Public Class FormInstall

    ' ─── 内部辅助类 ──────────────────────────────────────────────────
    Private Class InstallProgress
        Public IsMarquee As Boolean
        Public Current As Integer
        Public Total As Integer
        Public Message As String
    End Class

    Private Class ScanResult
        Public NewJson As String
        Public ToAdd As New List(Of String)
        Public ToReplace As New List(Of String)
        Public ToDelete As New List(Of String)
        Public PackSize As Long
    End Class

    ' ─── 目录结构常量（提至类级别，避免重复声明） ────────────────────

    Private Shared ReadOnly AppRoot As String = Application.StartupPath
    Private Shared ReadOnly ManifestDir As String = Path.Combine(AppRoot, "manifest")
    Private Shared ReadOnly PendingDeletionsPath As String = Path.Combine(ManifestDir, "pendingdeletions.txt")
    Private Shared ReadOnly PendingCopiesPath As String = Path.Combine(ManifestDir, "pendingcopies.txt")
    Private Shared ReadOnly CacheDir As String = Path.Combine(AppRoot, "cache")

    ' ─── 由 FormMain 传入的属性 ──────────────────────────────────────

    ''' <summary>
    ''' 当前正在操作的字体目录路径。
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property FontDir As String = ""

    ' ─── 状态字段 ────────────────────────────────────────────────────

    Private _loadedDnfpPath As String = ""
    Private _loadedJsonPath As String = ""
    Private _loadedFileType As String = ""

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property AutoInstallFile As String = ""

    ' ─── 窗体生命周期 ──────────────────────────────────────────────────

    Private Sub FormBlade_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EnsureDirectoriesExist()

        If String.IsNullOrEmpty(FontDir) OrElse Not Directory.Exists(FontDir) Then
            btnInstall.Enabled = False
            btnOpen.Enabled = False
        End If

        lstbDNFP.ItemHeight = FormMain.SharedItemHeight

        Dim dblProp = GetType(Control).GetProperty("DoubleBuffered", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
        If dblProp IsNot Nothing Then
            dblProp.SetValue(lstbDNFP, True, Nothing)
        End If
    End Sub

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

    ' ─── 通用辅助 ────────────────────────────────────────────────────

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

    ' ─── 加载字体包 / JSON ──────────────────────────────────────────

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

                Dim verData As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                Using sr As New StreamReader(verEntry.Open(), Encoding.UTF8)
                    For Each line As String In sr.ReadToEnd().Split({vbCrLf, vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                        Dim sep As Integer = line.IndexOf("="c)
                        If sep > 0 Then
                            verData(line.Substring(0, sep).Trim()) = line.Substring(sep + 1).Trim()
                        End If
                    Next
                End Using

                Dim noteVal As String = ""
                lblZipNote.Text = If(verData.TryGetValue("note", noteVal) AndAlso Not String.IsNullOrWhiteSpace(noteVal), noteVal, "-")

                Dim modeVal As String = ""
                verData.TryGetValue("mode", modeVal)
                Dim modeStr As String = "-"
                Select Case modeVal.ToLower()
                    Case "normal" : modeStr = "默认打包"
                    Case "standard" : modeStr = "标准打包"
                    Case "full" : modeStr = "完整打包"
                    Case Else : modeStr = If(String.IsNullOrEmpty(modeVal), "-", modeVal)
                End Select

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

            Dim hasFontDir As Boolean = Not String.IsNullOrEmpty(FontDir) AndAlso Directory.Exists(FontDir)
            btnInstall.Enabled = hasFontDir
            btnOpen.Enabled = hasFontDir
        Catch ex As Exception
            MessageBox.Show("读取字体包时发生错误：" & ex.Message, "读取字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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
        Catch ex As Exception
            MessageBox.Show("加载配置失败：" & ex.Message, "安装配置", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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
    End Sub

    ' ─── 安装 ────────────────────────────────────────────────────────

#Region "── 核心安装逻辑 ────────────"

    ''' <summary>
    ''' 直接安装 .json 配置到目标字体目录
    ''' </summary>
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
    ''' 直接安装 .dnfp 字体包（供 FormMain 拖放复用，无进度条）
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

            ' 确认对话框
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

            ' 执行安装（传 Nothing 表示无进度条）
            ExecuteInstall(scanResult, fontDir, Nothing)

            ' 提示
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
    ''' 异步安装（供 FormInstall 窗体使用，带进度条）
    ''' </summary>
    Private Shared Async Function InstallDnfpAsync(packagePath As String, fontDir As String, owner As IWin32Window, progress As IProgress(Of InstallProgress)) As Task(Of Boolean)
        Dim tempDir = Path.Combine(Path.GetTempPath(), "dnfe_import_" & Guid.NewGuid.ToString("N").Substring(0, 8))

        Try
            ' 1. 后台解压与分析（去掉了解压和分析的进度文本提示）
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
    ''' 提取出来的公共分析与差异比对逻辑
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

        Dim zipFonts As New List(Of String)
        zipFonts.AddRange(Directory.GetFiles(srcDir, "*.ttf"))
        zipFonts.AddRange(Directory.GetFiles(srcDir, "*.ttc"))
        zipFonts.AddRange(Directory.GetFiles(srcDir, "*.otf"))
        Dim zipFileNames As New HashSet(Of String)(zipFonts.Select(Function(f) Path.GetFileName(f)), StringComparer.OrdinalIgnoreCase)

        Dim currentFonts As New List(Of String)
        currentFonts.AddRange(Directory.GetFiles(fontDir, "*.ttf"))
        currentFonts.AddRange(Directory.GetFiles(fontDir, "*.ttc"))
        currentFonts.AddRange(Directory.GetFiles(fontDir, "*.otf"))

        Dim res As New ScanResult With {.NewJson = newJson}

        For Each f In zipFonts
            If File.Exists(Path.Combine(fontDir, Path.GetFileName(f))) Then
                res.ToReplace.Add(f)
            Else
                res.ToAdd.Add(f)
            End If
        Next

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
    ''' 提取出来的公共执行逻辑（复制、缓存、写挂起记录）
    ''' </summary>
    Private Shared Sub ExecuteInstall(scanResult As ScanResult, fontDir As String, progress As IProgress(Of InstallProgress))
        Directory.CreateDirectory(CacheDir)
        For Each ext In {"*.ttf", "*.ttc", "*.otf"}
            For Each oldFile In Directory.GetFiles(CacheDir, ext)
                Try : File.Delete(oldFile) : Catch : End Try
            Next
        Next
        Directory.CreateDirectory(ManifestDir)

        Dim totalSteps As Integer = scanResult.ToAdd.Count + scanResult.ToReplace.Count
        Dim step0 As Integer = 0

        ' toAdd：直接复制
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

    ' ─── 按钮事件 ────────────────────────────────────────────────────

    Private Async Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        If Not String.IsNullOrEmpty(_loadedJsonPath) AndAlso File.Exists(_loadedJsonPath) Then
            If InstallJsonConfigDirect(_loadedJsonPath, FontDir, Me) Then ResetLoadState()
            Return
        End If

        If String.IsNullOrEmpty(_loadedDnfpPath) OrElse Not File.Exists(_loadedDnfpPath) Then
            MessageBox.Show(Me, "未加载字体包。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim origTitle As String = Me.Text
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
                        Me.Text = $"{origTitle} - {displayMsg} ({p.Current}/{p.Total})"
                    Else
                        Me.Text = $"{origTitle} - {displayMsg}"
                    End If
                End If
            End Sub)

        ProgressBar1.Visible = True
        ProgressBar1.Style = ProgressBarStyle.Marquee
        ProgressBar1.Value = 0

        Try
            Dim ok As Boolean = Await InstallDnfpAsync(_loadedDnfpPath, FontDir, Me, progress)
            If ok Then ResetLoadState()
        Catch ex As Exception
            MessageBox.Show($"安装失败：{ex.Message}", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnInstall.Enabled = True
            btnOpen.Enabled = True
            btnBroswe.Enabled = True
            ProgressBar1.Visible = False
            ProgressBar1.Value = 0
            Me.Text = origTitle
        End Try
    End Sub

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

    ' ─── 列表自绘 ────────────────────────────────────────────────────

    Private Sub lstbBlade_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstbDNFP.DrawItem
        If e.Index < 0 OrElse e.Index >= lstbDNFP.Items.Count Then Return
        e.DrawBackground()

        Dim itemText = lstbDNFP.Items(e.Index).ToString
        Dim textColor = If((e.State And DrawItemState.Selected) <> 0, SystemColors.HighlightText, Color.Black)

        Dim textY = e.Bounds.Y + (e.Bounds.Height - e.Font.Height) \ 2
        Dim textRect As New Rectangle(e.Bounds.X + 4, textY, e.Bounds.Width - 8, e.Font.Height)

        TextRenderer.DrawText(e.Graphics, itemText, e.Font, textRect, textColor, TextFormatFlags.Left Or TextFormatFlags.NoPrefix)

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

    ' ─── 辅助 ────────────────────────────────────────────────────────

    Private Shared Function FormatBytes(bytes As Long) As String
        If bytes >= 1024L * 1024 * 1024 Then Return (bytes / 1024 / 1024 / 1024).ToString("F2") & " GB"
        If bytes >= 1024L * 1024 Then Return (bytes / 1024 / 1024).ToString("F2") & " MB"
        If bytes >= 1024 Then Return (bytes / 1024).ToString("F0") & " KB"
        Return bytes.ToString() & " B"
    End Function

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            Dim cbo = TryCast(ActiveControl, ComboBox)
            If cbo IsNot Nothing AndAlso cbo.DroppedDown Then Return MyBase.ProcessCmdKey(msg, keyData)
            Me.Close()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class