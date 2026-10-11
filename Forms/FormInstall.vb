Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions

''' <summary>
''' BLADE 字体包安装窗体：加载 .dnfp / .json 并直接部署到当前 fonts 目录。 导出功能在 FormOutput。
''' </summary>
Public Class FormInstall

    ' ─── 由 FormMain 传入的属性 ──────────────────────────────────────

    ''' <summary>
    ''' 当前正在操作的字体目录路径。
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property FontDir As String = ""

    ' ─── 目录结构常量 ────────────────────────────────────────────────

    Private ReadOnly AppRoot As String = Application.StartupPath
    Private ReadOnly ManifestDir As String = Path.Combine(AppRoot, "manifest")
    Private ReadOnly PendingDeletionsPath As String = Path.Combine(ManifestDir, "pendingdeletions.txt")
    Private ReadOnly PendingCopiesPath As String = Path.Combine(ManifestDir, "pendingcopies.txt")
    Private ReadOnly CacheDir As String = Path.Combine(AppRoot, "cache")

    ' ─── 状态字段 ────────────────────────────────────────────────────

    ''' <summary>
    ''' 当前已加载的 .dnfp 文件路径。
    ''' </summary>
    Private _loadedDnfpPath As String = ""

    ''' <summary>
    ''' 当前已加载的 .json 配置文件路径。
    ''' </summary>
    Private _loadedJsonPath As String = ""

    ''' <summary>
    ''' 当前加载的文件类型（"dnfp" 或 "json"）。
    ''' </summary>
    Private _loadedFileType As String = ""

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

    Private Sub AppendPendingLines(path As String, lines As IEnumerable(Of String))
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

    ''' <summary>
    ''' 加载 .dnfp 字体包：验证结构、填充包信息面板、列出内容到 lstbBlade。
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

                ' 解析 verification.txt
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

                ' 模式 + 来源
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

                ' 列出包内容
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

            ' 文件信息
            lblZipName.Text = fi.Name
            Dim sz As Long = fi.Length
            lblZipSize.Text = FormatBytes(sz)
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

    ''' <summary>
    ''' 加载 .json 配置（仅用于覆盖 fonts 目录的 index.json）。
    ''' </summary>
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

    ''' <summary>
    ''' 安装按钮：
    ''' - .json → 直接覆盖 fonts\index.json。
    ''' - .dnfp → 解压 → 分类（新增/覆盖/删除）→ 立即复制或挂起 → 写 index.json。
    ''' </summary>
    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        ' ── JSON 分支 ──
        If Not String.IsNullOrEmpty(_loadedJsonPath) AndAlso File.Exists(_loadedJsonPath) Then
            If String.IsNullOrEmpty(FontDir) OrElse Not Directory.Exists(FontDir) Then
                MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim confirm = MessageBox.Show("确认要覆盖当前配置吗？", "确认安装", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If confirm <> DialogResult.OK Then Return

            Try
                File.Copy(_loadedJsonPath, Path.Combine(FontDir, "index.json"), True)
                MessageBox.Show("配置写入成功。", "安装配置", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ResetLoadState()
            Catch ex As Exception
                MessageBox.Show($"写入配置失败：{ex.Message}", "安装配置", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return
        End If

        ' ── DNF P 分支 ──
        If String.IsNullOrEmpty(_loadedDnfpPath) OrElse Not File.Exists(_loadedDnfpPath) Then
            MessageBox.Show("未加载字体包。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If String.IsNullOrEmpty(FontDir) OrElse Not Directory.Exists(FontDir) Then
            MessageBox.Show("字体目录无效或不存在，拒绝安装。", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim tempDir = Path.Combine(Path.GetTempPath(), "dnfe_import_" & Guid.NewGuid.ToString("N").Substring(0, 8))

        Try
            ' 1. 解压
            If Directory.Exists(tempDir) Then Directory.Delete(tempDir, True)
            Directory.CreateDirectory(tempDir)
            ZipFile.ExtractToDirectory(_loadedDnfpPath, tempDir)

            Dim srcDir = Path.Combine(tempDir, "fonts")
            If Not Directory.Exists(srcDir) Then srcDir = tempDir

            ' 2. 读 index.json
            Dim jsonSrc = Path.Combine(srcDir, "index.json")
            If Not File.Exists(jsonSrc) Then jsonSrc = Path.Combine(srcDir, "index.JSON")
            If Not File.Exists(jsonSrc) Then
                MessageBox.Show("字体包内未找到配置文件。", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim newJson = File.ReadAllText(jsonSrc, Encoding.UTF8)

            ' 3. 解析 index.json 引用的文件名
            Dim referencedFiles As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            Dim m = Regex.Match(newJson, """font files""\s*:\s*\[\s*(.*?)\s*\]", RegexOptions.Singleline)
            If m.Success Then
                For Each mm As Match In Regex.Matches(m.Groups(1).Value, """([^""]+)""")
                    referencedFiles.Add(mm.Groups(1).Value)
                Next
            End If

            ' 4. 收集包内字体
            Dim zipFonts As New List(Of String)
            zipFonts.AddRange(Directory.GetFiles(srcDir, "*.ttf"))
            zipFonts.AddRange(Directory.GetFiles(srcDir, "*.ttc"))
            zipFonts.AddRange(Directory.GetFiles(srcDir, "*.otf"))
            Dim zipFileNames As New HashSet(Of String)(zipFonts.Select(Function(f) Path.GetFileName(f)), StringComparer.OrdinalIgnoreCase)

            ' 5. 收集当前目录字体
            Dim currentFonts As New List(Of String)
            currentFonts.AddRange(Directory.GetFiles(FontDir, "*.ttf"))
            currentFonts.AddRange(Directory.GetFiles(FontDir, "*.ttc"))
            currentFonts.AddRange(Directory.GetFiles(FontDir, "*.otf"))

            ' 6. 分类
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

            ' 7. 确认对话框
            Dim packSize As Long = 0
            Try
                packSize = New FileInfo(_loadedDnfpPath).Length
            Catch
            End Try

            Using dlg As New DialogMessage()
                dlg.Text = "确认安装"
                dlg.TitleText = $"确认要安装「{Path.GetFileName(_loadedDnfpPath)}」吗？"
                dlg.DescriptionText = $"字体包大小: {FormatBytes(packSize)}"
                dlg.ConfirmText = "安装"
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
                    dlg.Items.Add("(没有需要操作的文件)")
                End If

                If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
            End Using

            ' 8. 执行安装
            Directory.CreateDirectory(CacheDir)

            ' 清理旧缓存
            For Each ext In {"*.ttf", "*.ttc", "*.otf"}
                For Each old In Directory.GetFiles(CacheDir, ext)
                    Try
                        File.Delete(old)
                    Catch
                    End Try
                Next
            Next

            Directory.CreateDirectory(ManifestDir)

            ' ── toAdd：直接复制；失败则缓存后挂起 ──
            Dim addCopyLines As New List(Of String)
            For Each f In toAdd
                Dim destPath = Path.Combine(FontDir, Path.GetFileName(f))
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

            ' ── toReplace：缓存新文件，标记旧文件删除，标记新文件待复制 ──
            Dim replaceDeleteLines As New List(Of String)
            Dim replaceCopyLines As New List(Of String)
            For Each f In toReplace
                Dim destPath = Path.Combine(FontDir, Path.GetFileName(f))
                Dim cached = Path.Combine(CacheDir, Path.GetFileName(f))
                Try
                    File.Copy(f, cached, True)
                    replaceDeleteLines.Add(destPath)
                    replaceCopyLines.Add($"{cached}|{destPath}")
                Catch ex As Exception
                    MessageBox.Show("缓存字体时出错，已跳过该文件。" & vbCrLf &
                                    $"· {Path.GetFileName(f)}" & vbCrLf &
                                    $"· {ex.Message}",
                                    "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            Next
            AppendPendingLines(PendingDeletionsPath, replaceDeleteLines)
            AppendPendingLines(PendingCopiesPath, replaceCopyLines)

            ' ── toDelete：标记删除 ──
            AppendPendingLines(PendingDeletionsPath, toDelete)

            ' 写入 index.json
            File.WriteAllText(Path.Combine(FontDir, "index.json"), newJson, Encoding.UTF8)

            ' 提示
            Dim hasPending As Boolean = (addCopyLines.Count > 0) OrElse
                                        (replaceDeleteLines.Count > 0) OrElse
                                        (toDelete.Count > 0)
            If hasPending Then
                MessageBox.Show("字体包安装成功，下次启动时生效。", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("字体包安装成功。", "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("安装字体包时发生错误：" & ex.Message, "安装字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Try
                If Directory.Exists(tempDir) Then Directory.Delete(tempDir, True)
            Catch
            End Try
        End Try
    End Sub

    ' ─── 打开 ────────────────────────────────────────────────────────

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

    ' ─── 通用辅助 ────────────────────────────────────────────────────

    Private Function FormatBytes(bytes As Long) As String
        If bytes >= 1024L * 1024 * 1024 Then Return (bytes / 1024 / 1024 / 1024).ToString("F2") & " GB"
        If bytes >= 1024L * 1024 Then Return (bytes / 1024 / 1024).ToString("F2") & " MB"
        If bytes >= 1024 Then Return (bytes / 1024).ToString("F0") & " KB"
        Return bytes.ToString() & " B"
    End Function

End Class