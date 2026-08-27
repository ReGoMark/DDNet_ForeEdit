Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Text

''' <summary>
''' BLADE 字体包管理窗体，负责导出 .dnfp 字体包和导入字体包到当前目录。
''' </summary>
Public Class FormBlade

    ' ─── 由 FormMain 传入的属性 ──────────────────────────────────────

    ''' <summary>当前正在操作的字体目录路径。</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property FontDir As String = ""

    ''' <summary>当前已使用的字体文件列表（用于默认打包）。</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property NormFontFiles As List(Of String) = Nothing

    ''' <summary>包含预装字体的标准字体列表（用于标准打包）。</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property StdFontFiles As List(Of String) = Nothing

    ''' <summary>目录中所有字体文件列表（用于完整打包）。</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property FullFontFiles As List(Of String) = Nothing

    ''' <summary>当前生成的 index.json 内容。</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property JsonContent As String = ""

    ''' <summary>应用程序版本号。</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property AppVersion As String = ""

    ''' <summary>目录类型（Local/Compatible/Install）。</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property DirectoryType As String = ""

    ' ─── 目录结构常量（与 FormMain 保持一致）──────────────────────

    ''' <summary>程序根目录（exe 所在目录）。</summary>
    Private ReadOnly AppRoot As String = Application.StartupPath

    ''' <summary>清单目录（manifest）。</summary>
    Private ReadOnly ManifestDir As String = Path.Combine(AppRoot, "manifest")

    ''' <summary>待删除清单文件路径。</summary>
    Private ReadOnly PendingDeletionsPath As String = Path.Combine(ManifestDir, "pendingdeletions.txt")

    ''' <summary>待复制清单文件路径。</summary>
    Private ReadOnly PendingCopiesPath As String = Path.Combine(ManifestDir, "pendingcopies.txt")

    ''' <summary>临时缓存目录（用于导入字体包时缓存）。</summary>
    Private ReadOnly CacheDir As String = Path.Combine(AppRoot, "cache")

    ''' <summary>当前已加载的 .dnfp 文件路径。</summary>
    Private _loadedDnfpPath As String = ""

    ' ─── 窗体生命周期 ──────────────────────────────────────────────────

    ''' <summary>窗体加载事件：初始化控件状态、确保目录存在。</summary>
    Private Sub FormBlade_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 确保目录存在
        EnsureDirectoriesExist()

        ' 防御：确保列表不为 Nothing
        If NormFontFiles Is Nothing Then NormFontFiles = New List(Of String)
        If StdFontFiles Is Nothing Then StdFontFiles = New List(Of String)
        If FullFontFiles Is Nothing Then FullFontFiles = New List(Of String)
        If String.IsNullOrEmpty(JsonContent) Then JsonContent = ""

        tbNote.MaxLength = 60
        rbNorm.Checked = True
        chkNote.Checked = False
        tbNote.Enabled = False
        tbNote.Text = ""

        Dim hasFontDir As Boolean = Not String.IsNullOrEmpty(FontDir) AndAlso Directory.Exists(FontDir)
        Dim hasAnyFonts As Boolean = NormFontFiles.Count > 0 OrElse StdFontFiles.Count > 0 OrElse FullFontFiles.Count > 0
        Dim hasJson As Boolean = Not String.IsNullOrEmpty(JsonContent)

        If Not hasFontDir Then
            btnInstall.Enabled = False
            btnExport.Enabled = False
            tblExportMode.Enabled = False
            btnOpen.Enabled = False
        ElseIf Not hasAnyFonts OrElse Not hasJson Then
            btnExport.Enabled = False
            tblExportMode.Enabled = False
        End If

        ' 设置 ListBox 行高（从主窗体共享属性获取）
        lstbBlade.ItemHeight = FormMain.SharedItemHeight

        RefreshPackSizeLabels()
        UpdateRadioButtonLabels()
    End Sub

    ''' <summary>确保所有必需目录存在。</summary>
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

    ' ─── 估算包体积 ──────────────────────────────────────────────────

    ''' <summary>刷新三种打包模式的预估体积标签。</summary>
    Private Sub RefreshPackSizeLabels()
        lblNorm.Text = FormatBytes(EstimatePackedSize(NormFontFiles))
        lblStandard.Text = FormatBytes(EstimatePackedSize(StdFontFiles))
        lblFull.Text = FormatBytes(EstimatePackedSize(FullFontFiles))
    End Sub

    ''' <summary>估算字体列表压缩后的体积（字节）。</summary>
    Private Function EstimatePackedSize(files As List(Of String)) As Long
        If files Is Nothing OrElse files.Count = 0 Then Return 0

        Dim total As Long = 0
        For Each f As String In files
            If Not File.Exists(f) Then Continue For

            Dim fileInfo As New FileInfo(f)
            Dim rawSize As Long = fileInfo.Length
            Dim ext As String = fileInfo.Extension.ToLower()

            Dim ratio As Double
            Select Case ext
                Case ".ttc" : ratio = 0.88
                Case ".ttf", ".otf" : ratio = 0.75
                Case ".json" : ratio = 0.25
                Case Else : ratio = 0.85
            End Select
            total += CLng(rawSize * ratio)
        Next

        If Not String.IsNullOrEmpty(JsonContent) Then
            total += CLng(Encoding.UTF8.GetByteCount(JsonContent) * 0.2)
        End If
        total += (files.Count * 64)
        Return total
    End Function

    ''' <summary>将字节数格式化为 B / KB / MB / GB 字符串。</summary>
    Private Function FormatBytes(bytes As Long) As String
        If bytes >= 1024 * 1024 * 1024 Then Return $"{bytes / 1024 / 1024 / 1024:F2} GB"
        If bytes >= 1024 * 1024 Then Return $"{bytes / 1024 / 1024:F2} MB"
        If bytes >= 1024 Then Return $"{bytes / 1024:F0} KB"
        Return $"{bytes} B"
    End Function

    ' ─── 备注复选框 ──────────────────────────────────────────────────

    ''' <summary>备注复选框状态改变时启用/禁用备注文本框。</summary>
    Private Sub chkNote_CheckedChanged(sender As Object, e As EventArgs) Handles chkNote.CheckedChanged
        tbNote.Enabled = chkNote.Checked
    End Sub

    ' ─── 导出字体包 ──────────────────────────────────────────────────

    ''' <summary>导出字体包按钮点击事件。</summary>
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim filesToPack As List(Of String)
        Dim defaultFileName As String = "无标题"

        If rbNorm.Checked Then
            filesToPack = NormFontFiles
            rbStandard.ForeColor = Color.DarkGray
            rbFull.ForeColor = Color.DarkGray
            rbNorm.ForeColor = Color.FromArgb(64, 64, 64)
        ElseIf rbStandard.Checked Then
            filesToPack = StdFontFiles
            rbNorm.ForeColor = Color.DarkGray
            rbFull.ForeColor = Color.DarkGray
            rbStandard.ForeColor = Color.FromArgb(64, 64, 64)
        Else
            filesToPack = FullFontFiles
            rbNorm.ForeColor = Color.DarkGray
            rbStandard.ForeColor = Color.DarkGray
            rbFull.ForeColor = Color.FromArgb(64, 64, 64)
        End If

        If filesToPack Is Nothing OrElse filesToPack.Count = 0 Then
            MessageBox.Show("当前目录内无字体，拒绝导出。", "导出字体包", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrEmpty(JsonContent) Then
            MessageBox.Show("配置内容为空，拒绝导出。", "导出字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Using sfd As New SaveFileDialog()
            sfd.Title = "保存字体包"
            sfd.FileName = defaultFileName
            sfd.Filter = "DDNet 字体包 (*.dnfp)|*.dnfp|所有文件|*.*"
            sfd.DefaultExt = "dnfp"
            If sfd.ShowDialog(Me) <> DialogResult.OK Then Return

            Dim tempJsonPath As String = Path.Combine(Path.GetTempPath(), "dnfe_export_index.json")
            Dim origTitle As String = Me.Text
            Dim totalSteps As Integer = filesToPack.Count + 2

            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = totalSteps
            ProgressBar1.Value = 0
            btnExport.Enabled = False

            Try
                File.WriteAllText(tempJsonPath, JsonContent, Encoding.Default)
                If File.Exists(sfd.FileName) Then File.Delete(sfd.FileName)

                Using zipArchive As ZipArchive = ZipFile.Open(sfd.FileName, ZipArchiveMode.Create)
                    ' 步骤1：写入 index.json
                    zipArchive.CreateEntryFromFile(tempJsonPath, "fonts/index.json", CompressionLevel.Optimal)
                    ProgressBar1.Value = 1
                    Me.Text = $"{origTitle} - 写入: index.json"

                    ' 步骤2~N：写入字体文件
                    Dim nstep As Integer = 1
                    For Each fontFile As String In filesToPack
                        If File.Exists(fontFile) Then
                            Dim fileName As String = Path.GetFileName(fontFile)
                            zipArchive.CreateEntryFromFile(fontFile, $"fonts/{fileName}", CompressionLevel.Optimal)
                            nstep += 1
                            ProgressBar1.Value = nstep
                            Me.Text = $"{origTitle} - 拷贝: {fileName} ({nstep}/{totalSteps})"
                        End If
                    Next

                    ' 最后一步：写入验证文件
                    Dim verEntry As ZipArchiveEntry = zipArchive.CreateEntry("verification.txt")
                    Dim modeStr As String
                    If rbNorm.Checked Then
                        modeStr = "normal"
                    ElseIf rbStandard.Checked Then
                        modeStr = "standard"
                    Else
                        modeStr = "full"
                    End If

                    Using writer As New StreamWriter(verEntry.Open(), Encoding.UTF8)
                        writer.WriteLine($"app=DDNetForeEdit")
                        writer.WriteLine($"date={DateTime.Now:yyyy/MM/dd HH:mm}")
                        writer.WriteLine($"mode={modeStr}")
                        writer.WriteLine($"source={DirectoryType}")
                        writer.WriteLine($"fonts={filesToPack.Count}")
                        If chkNote.Checked AndAlso Not String.IsNullOrWhiteSpace(tbNote.Text) Then
                            writer.WriteLine($"note={tbNote.Text.Trim()}")
                        End If
                    End Using
                    nstep += 1
                    ProgressBar1.Value = nstep
                    Me.Text = $"{origTitle} - 写入: Verification.txt"
                End Using

                MessageBox.Show("字体包导出成功。", "导出字体包", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("导出字体包时发生错误：" & ex.Message, "导出字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ProgressBar1.Visible = False
                ProgressBar1.Value = 0
                Me.Text = origTitle
                btnExport.Enabled = True
                Try
                    If File.Exists(tempJsonPath) Then File.Delete(tempJsonPath)
                Catch
                End Try
            End Try
        End Using
    End Sub

    ' ─── 导入字体包 ──────────────────────────────────────────────────

    ''' <summary>安装字体包按钮点击事件。</summary>
    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        If String.IsNullOrEmpty(_loadedDnfpPath) OrElse Not File.Exists(_loadedDnfpPath) Then
            MessageBox.Show("数据尚未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If String.IsNullOrEmpty(FontDir) OrElse Not Directory.Exists(FontDir) Then
            MessageBox.Show("字体目录无效或不存在，拒绝导入。", "导入字体包", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim tempDir = Path.Combine(Path.GetTempPath(), "dnfe_import_" & Guid.NewGuid.ToString("N").Substring(0, 8))

        Try
            ' 1. 解压到临时目录
            If Directory.Exists(tempDir) Then Directory.Delete(tempDir, True)
            Directory.CreateDirectory(tempDir)
            ZipFile.ExtractToDirectory(_loadedDnfpPath, tempDir)

            Dim srcDir = Path.Combine(tempDir, "fonts")
            If Not Directory.Exists(srcDir) Then srcDir = tempDir

            ' 2. 读取 index.json
            Dim jsonSrc = Path.Combine(srcDir, "index.json")
            If Not File.Exists(jsonSrc) Then jsonSrc = Path.Combine(srcDir, "index.JSON")
            If Not File.Exists(jsonSrc) Then
                MessageBox.Show("字体包内未找到配置。", "导入字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim newJson = File.ReadAllText(jsonSrc, Encoding.UTF8)

            ' 3. 解析 index.json 引用的文件名
            Dim referencedFiles As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            Dim m = RegularExpressions.Regex.Match(newJson,
                """font files""\s*:\s*\[\s*(.*?)\s*\]",
                RegularExpressions.RegexOptions.Singleline)
            If m.Success Then
                For Each mm As RegularExpressions.Match In RegularExpressions.Regex.Matches(m.Groups(1).Value, """([^""]+)""")
                    referencedFiles.Add(mm.Groups(1).Value)
                Next
            End If

            ' 4. 收集包内字体文件
            Dim zipFonts As New List(Of String)
            zipFonts.AddRange(Directory.GetFiles(srcDir, "*.ttf"))
            zipFonts.AddRange(Directory.GetFiles(srcDir, "*.ttc"))
            zipFonts.AddRange(Directory.GetFiles(srcDir, "*.otf"))
            Dim zipFileNames As New HashSet(Of String)(zipFonts.Select(Function(f) Path.GetFileName(f)), StringComparer.OrdinalIgnoreCase)

            ' 5. 收集当前目录字体文件
            Dim currentFonts As New List(Of String)
            currentFonts.AddRange(Directory.GetFiles(FontDir, "*.ttf"))
            currentFonts.AddRange(Directory.GetFiles(FontDir, "*.ttc"))
            currentFonts.AddRange(Directory.GetFiles(FontDir, "*.otf"))

            ' 6. 分类：新增、替换、删除
            Dim toAdd As New List(Of String)
            Dim toReplace As New List(Of String)
            Dim toDelete As New List(Of String)

            For Each f In zipFonts
                If File.Exists(Path.Combine(FontDir, Path.GetFileName(f))) Then
                    toReplace.Add(f)
                Else
                    toAdd.Add(f)
                End If
            Next

            For Each f In currentFonts
                Dim fn = Path.GetFileName(f)
                If Not zipFileNames.Contains(fn) AndAlso Not referencedFiles.Contains(fn) Then
                    If Not fn.Contains("Font_Awesome", StringComparison.OrdinalIgnoreCase) AndAlso
                       Not fn.Contains("FontAwesome", StringComparison.OrdinalIgnoreCase) Then
                        toDelete.Add(f)
                    End If
                End If
            Next

            ' ─── 使用 DialogPopup 确认 ──────────────────────────────────
            Using dlg As New DialogPopUp()
                dlg.Text = "确认导入"
                dlg.TitleText = $"确认要安装「{Path.GetFileName(_loadedDnfpPath)}」字体包吗？"
                dlg.DescriptionText = "以下列出了即将执行的操作："
                dlg.ConfirmText = "确定"
                dlg.CancelText = "取消"

                For Each f In toAdd
                    dlg.Items.Add($"[新增] {Path.GetFileName(f)}")
                Next

                For Each f In toReplace
                    dlg.Items.Add($"[覆盖] {Path.GetFileName(f)}")
                Next

                For Each f In toDelete
                    dlg.Items.Add($"[删除] {Path.GetFileName(f)}")
                Next

                If toAdd.Count = 0 AndAlso toReplace.Count = 0 AndAlso toDelete.Count = 0 Then
                    dlg.Items.Add("（没有需要操作的文件）")
                End If

                If dlg.ShowDialog(Me) <> DialogResult.OK Then
                    Return
                End If
            End Using

            ' ─── 执行导入操作 ──────────────────────────────────────────
            Directory.CreateDirectory(CacheDir)

            ' 清理旧缓存
            For Each ext In {"*.ttf", "*.ttc", "*.otf"}
                For Each old In Directory.GetFiles(CacheDir, ext)
                    Try : File.Delete(old) : Catch : End Try
                Next
            Next

            Directory.CreateDirectory(ManifestDir)

            ' toAdd：直接复制；失败则缓存后标记
            For Each f In toAdd
                Dim destPath = Path.Combine(FontDir, Path.GetFileName(f))
                Try
                    File.Copy(f, destPath, False)
                Catch
                    Dim cached = Path.Combine(CacheDir, Path.GetFileName(f))
                    Try
                        File.Copy(f, cached, True)
                        File.AppendAllText(PendingCopiesPath, $"{cached}|{destPath}" & Environment.NewLine)
                    Catch
                    End Try
                End Try
            Next

            ' toReplace：缓存新文件，标记旧文件删除，标记新文件待复制
            For Each f In toReplace
                Dim destPath = Path.Combine(FontDir, Path.GetFileName(f))
                Dim cached = Path.Combine(CacheDir, Path.GetFileName(f))
                Try
                    File.Copy(f, cached, True)
                    File.AppendAllText(PendingDeletionsPath, destPath & Environment.NewLine)
                    File.AppendAllText(PendingCopiesPath, $"{cached}|{destPath}" & Environment.NewLine)
                Catch ex As Exception
                    MessageBox.Show("缓存字体时出错，已跳过该文件。" & vbCrLf &
                                    $"· {Path.GetFileName(f)}" & vbCrLf &
                                    $"· {ex.Message}",
                                    "导入字体包", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            Next

            ' toDelete：标记多余字体删除
            For Each f In toDelete
                File.AppendAllText(PendingDeletionsPath, f & Environment.NewLine)
            Next

            ' 写入 index.json
            Dim jsonDest = Path.Combine(FontDir, "index.json")
            File.WriteAllText(jsonDest, newJson, Encoding.UTF8)

            MessageBox.Show(
                "字体包导入成功：" & vbCrLf &
                $"· 配置已覆盖" & vbCrLf &
                $"· 新增字体已导入" & vbCrLf &
                If(toReplace.Count > 0, $"· 替换字体已挂起，将在下次启动时执行" & vbCrLf, "") &
                If(toDelete.Count > 0, $"· 删除字体已挂起，将在下次启动时执行" & vbCrLf, ""),
                "导入字体包", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("导入字体包时发生错误。" & vbCrLf & $"· {ex.Message}",
                            "导入字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Try
                If Directory.Exists(tempDir) Then Directory.Delete(tempDir, True)
            Catch
            End Try
        End Try
    End Sub

    ' ─── 加载字体包 ──────────────────────────────────────────────────

    ''' <summary>加载并验证 .dnfp 字体包，填充包信息面板。</summary>
    Private Sub LoadPackage(filePath As String)
        ' 重置状态
        _loadedDnfpPath = ""
        lstbBlade.Items.Clear()
        lblZipName.Text = "-"
        lblZipSize.Text = "-"
        lblZipDate.Text = "-"
        lblZipNote.Text = "-"
        btnInstall.Enabled = False
        btnOpen.Enabled = False

        If Not File.Exists(filePath) Then Return

        If Not Path.GetExtension(filePath).Equals(".dnfp", StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("仅支持 .dnfp 字体包。", "读取字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Dim fi As New FileInfo(filePath)

            Using archive As ZipArchive = ZipFile.OpenRead(filePath)
                ' 1. 验证 verification.txt
                Dim verEntry = archive.GetEntry("verification.txt")
                If verEntry Is Nothing Then
                    MessageBox.Show("所选字体包验证失败：" & vbCrLf & "· 缺少 verification.txt。",
                                    "读取字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    lblZip.Text = "等待数据加载"
                    Return
                End If

                ' 2. 验证 fonts/index.json
                Dim jsonEntry = archive.GetEntry("fonts/index.json")
                Dim hasIndexJson As Boolean = (jsonEntry IsNot Nothing)
                If Not hasIndexJson Then
                    MessageBox.Show("所选字体包验证失败：" & vbCrLf & "· 缺少 fonts/index.json 配置文件。",
                                    "读取字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    lblZip.Text = "等待数据加载"
                    Return
                End If

                ' 3. 解析 verification.txt
                Dim verData As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                Using sr As New StreamReader(verEntry.Open(), Encoding.UTF8)
                    For Each line As String In sr.ReadToEnd().Split({vbCrLf, vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                        Dim sep As Integer = line.IndexOf("="c)
                        If sep > 0 Then
                            verData(line.Substring(0, sep).Trim()) = line.Substring(sep + 1).Trim()
                        End If
                    Next
                End Using

                ' 4. 填充备注
                Dim noteVal As String = ""
                lblZipNote.Text = If(verData.TryGetValue("note", noteVal) AndAlso Not String.IsNullOrWhiteSpace(noteVal), noteVal, "-")

                ' 5. 填充打包模式与来源
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

                ' 6. 列出所有内容
                lstbBlade.Items.Clear()

                Dim fontCount As Integer = 0
                For Each entry As ZipArchiveEntry In archive.Entries
                    If entry.FullName.StartsWith("fonts/", StringComparison.OrdinalIgnoreCase) AndAlso
                       Not entry.FullName.EndsWith("/") Then
                        Dim fileName = entry.Name
                        lstbBlade.Items.Add(fileName)
                        If Not fileName.Equals("index.json", StringComparison.OrdinalIgnoreCase) Then
                            fontCount += 1
                        End If
                    End If
                Next

                Dim configCount As Integer = If(hasIndexJson, 1, 0)
                lblZip.Text = $"字体 {fontCount} 项, 配置 {configCount} 项"
            End Using

            ' 7. 填充文件信息
            lblZipName.Text = fi.Name
            Dim sz As Long = fi.Length
            lblZipSize.Text = If(sz >= 1024L * 1024 * 1024, (sz / 1024 / 1024 / 1024).ToString("F2") & " GB",
                            If(sz >= 1024L * 1024, (sz / 1024 / 1024).ToString("F2") & " MB",
                            If(sz >= 1024, (sz / 1024).ToString("F0") & " KB", sz.ToString() & " B")))
            lblZipDate.Text = fi.LastWriteTime.ToString("yyyy/MM/dd HH:mm")
            _loadedDnfpPath = filePath

            Dim hasFontDir As Boolean = Not String.IsNullOrEmpty(FontDir) AndAlso Directory.Exists(FontDir)
            btnInstall.Enabled = hasFontDir
            btnOpen.Enabled = hasFontDir
        Catch ex As Exception
            MessageBox.Show("读取字体包时发生错误：" & ex.Message, "读取字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─── 包管理按钮事件 ─────────────────────────────────────────────

    ''' <summary>浏览字体包按钮点击事件。</summary>
    Private Sub btnZipBroswe_Click(sender As Object, e As EventArgs) Handles btnZipBroswe.Click
        Using ofd As New OpenFileDialog()
            ofd.Title = "选择「DDNet 字体包」"
            ofd.Filter = "DDNet 字体包 (*.dnfp)|*.dnfp|所有文件|*.*"
            If ofd.ShowDialog(Me) <> DialogResult.OK Then Return
            LoadPackage(ofd.FileName)
        End Using
    End Sub

    ''' <summary>拖放进入事件：允许拖放 .dnfp 文件。</summary>
    Private Sub btnZipBroswe_DragEnter(sender As Object, e As DragEventArgs) Handles btnZipBroswe.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If files.Length = 1 Then
                Dim ext = Path.GetExtension(files(0)).ToLower()
                If ext = ".dnfp" OrElse ext = ".zip" Then
                    e.Effect = DragDropEffects.Copy
                    Return
                End If
            End If
        End If
        e.Effect = DragDropEffects.None
    End Sub

    ''' <summary>拖放释放事件：加载拖入的字体包。</summary>
    Private Sub btnZipBroswe_DragDrop(sender As Object, e As DragEventArgs) Handles btnZipBroswe.DragDrop
        Dim files = CType(e.Data.GetData(DataFormats.FileDrop), String())
        If files IsNot Nothing AndAlso files.Length > 0 Then LoadPackage(files(0))
    End Sub

    ''' <summary>打开字体包所在文件夹。</summary>
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        If String.IsNullOrEmpty(_loadedDnfpPath) Then
            MessageBox.Show("数据尚未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Process.Start("explorer.exe", $"""{_loadedDnfpPath}""")
    End Sub

    ' ─── 列表绘制 ─────────────────────────────────────────────────────

    ''' <summary>自定义绘制字体包内容列表项，显示序号。</summary>
    Private Sub lstbBlade_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstbBlade.DrawItem
        If e.Index < 0 OrElse e.Index >= lstbBlade.Items.Count Then Return
        e.DrawBackground()

        Dim itemText As String = lstbBlade.Items(e.Index).ToString()
        Dim textColor As Color = If((e.State And DrawItemState.Selected) <> 0, SystemColors.HighlightText, SystemColors.WindowText)

        Dim textY As Integer = e.Bounds.Y + (e.Bounds.Height - e.Font.Height) \ 2
        Dim textRect As New Rectangle(e.Bounds.X + 2, textY, e.Bounds.Width - 8, e.Font.Height)

        TextRenderer.DrawText(e.Graphics, itemText, e.Font, textRect, textColor, TextFormatFlags.Left Or TextFormatFlags.NoPrefix)

        Dim tag As String = $"{e.Index + 1}"
        Using tagFont As New Font("Consolas", 7.5F, FontStyle.Regular, GraphicsUnit.Point)
            Using tagColor As New SolidBrush(If((e.State And DrawItemState.Selected) <> 0, Color.FromArgb(200, 255, 255, 255), Color.Gray))
                Dim tagSize As SizeF = e.Graphics.MeasureString(tag, tagFont)
                e.Graphics.DrawString(tag, tagFont, tagColor,
                    New PointF(e.Bounds.Right - tagSize.Width - 4, e.Bounds.Y + (e.Bounds.Height - tagSize.Height) / 2 + 1))
            End Using
        End Using
        e.DrawFocusRectangle()
    End Sub

    ' ─── 打包模式切换 ─────────────────────────────────────────────────

    ''' <summary>默认打包模式选中时更新标签颜色。</summary>
    Private Sub rbNorm_CheckedChanged(sender As Object, e As EventArgs) Handles rbNorm.CheckedChanged
        UpdateRadioButtonLabels()
    End Sub

    ''' <summary>标准打包模式选中时更新标签颜色。</summary>
    Private Sub rbStandard_CheckedChanged(sender As Object, e As EventArgs) Handles rbStandard.CheckedChanged
        UpdateRadioButtonLabels()
    End Sub

    ''' <summary>完整打包模式选中时更新标签颜色。</summary>
    Private Sub rbFull_CheckedChanged(sender As Object, e As EventArgs) Handles rbFull.CheckedChanged
        UpdateRadioButtonLabels()
    End Sub

    ''' <summary>更新打包模式 RadioButton 的标签颜色。</summary>
    Private Sub UpdateRadioButtonLabels()
        If rbNorm.Checked Then
            lblFull.ForeColor = Color.Gray
            lblStandard.ForeColor = Color.Gray
            lblNorm.ForeColor = SystemColors.WindowText
        ElseIf rbStandard.Checked Then
            lblNorm.ForeColor = Color.Gray
            lblFull.ForeColor = Color.Gray
            lblStandard.ForeColor = SystemColors.WindowText
        Else
            lblNorm.ForeColor = Color.Gray
            lblStandard.ForeColor = Color.Gray
            lblFull.ForeColor = SystemColors.WindowText
        End If
    End Sub

End Class