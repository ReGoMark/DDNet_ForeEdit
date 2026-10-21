Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Threading

''' <summary>
''' 导出窗体：负责将当前字体目录的字体和配置导出为 .dnfp 字体包或 index.json 配置文件。 打包为异步操作，支持用户取消。
''' </summary>
Public Class FormOutput

    ' ─── 由 FormMain 传入的属性 ──────────────────────────────────────

    ''' <summary>
    ''' 当前正在操作的字体目录路径。
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property FontDir As String = ""

    ''' <summary>
    ''' 当前已使用的字体文件列表（用于默认打包）。
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property NormFontFiles As List(Of String) = Nothing

    ''' <summary>
    ''' 包含预装字体的标准字体列表（用于标准打包）。
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property StdFontFiles As List(Of String) = Nothing

    ''' <summary>
    ''' 目录中所有字体文件列表（用于完整打包）。
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property FullFontFiles As List(Of String) = Nothing

    ''' <summary>
    ''' 当前生成的 index.json 内容。
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property JsonContent As String = ""

    ''' <summary>
    ''' 应用程序版本号。
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property AppVersion As String = ""

    ''' <summary>
    ''' 目录类型（Local/Compatible/Install）。
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property DirectoryType As String = ""

    ' ─── 状态字段 ──────────────────────────────────────────────────────

    ''' <summary>
    ''' 当前打包操作的取消令牌源。为 Nothing 表示无操作进行中。
    ''' </summary>
    Private _cts As CancellationTokenSource = Nothing

    ''' <summary>
    ''' 后台打包的进度载体。
    ''' </summary>
    Private Class PackProgress
        Public Current As Integer
        Public Message As String
    End Class

    ' 三种模式的预估体积（字节），在 Load 时算一次，切换模式只读缓存
    Private _sizeDefault As Long = 0

    Private _sizeStandard As Long = 0
    Private _sizeFull As Long = 0

    ' ─── 窗体生命周期 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 窗体加载事件：初始化控件状态。
    ''' </summary>
    Private Sub FormOutput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 防御：确保列表不为 Nothing
        If NormFontFiles Is Nothing Then NormFontFiles = New List(Of String)
        If StdFontFiles Is Nothing Then StdFontFiles = New List(Of String)
        If FullFontFiles Is Nothing Then FullFontFiles = New List(Of String)
        If String.IsNullOrEmpty(JsonContent) Then JsonContent = ""

        txtNote.MaxLength = 40
        rdoDefault.Checked = True
        chkNote.Checked = False
        txtNote.Enabled = False
        txtNote.Text = ""

        Dim hasAnyFonts As Boolean = NormFontFiles.Count > 0 OrElse StdFontFiles.Count > 0 OrElse FullFontFiles.Count > 0
        Dim hasJson As Boolean = Not String.IsNullOrEmpty(JsonContent)

        If Not hasAnyFonts OrElse Not hasJson Then
            btnDNFP.Enabled = False
            btnCfg.Enabled = False
            tblExportMode.Enabled = False
            lblUserName.Enabled = False
            Label1.Enabled = False
            Label2.Enabled = False
            lblTime.Enabled = False
            lblDate.Enabled = False
        End If

        RefreshPackSizeLabels()
        UpdateRadioButtonLabels()
    End Sub

    ' ─── 估算包体积 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 计算三种打包模式的预估体积并缓存，然后刷新 lblSizeTotal。
    ''' </summary>
    Private Sub RefreshPackSizeLabels()
        _sizeDefault = EstimatePackedSize(NormFontFiles)
        _sizeStandard = EstimatePackedSize(StdFontFiles)
        _sizeFull = EstimatePackedSize(FullFontFiles)

        'lblCountDefault.Text = FormatCount(NormFontFiles)
        'lblCountStandard.Text = FormatCount(StdFontFiles)
        'lblCountFull.Text = FormatCount(FullFontFiles)

        UpdateRadioButtonLabels()
    End Sub

    ''' <summary>
    ''' 把字体列表格式化为 "N 项"。
    ''' </summary>
    Private Function FormatCount(files As List(Of String)) As String
        Dim count As Integer = If(files IsNot Nothing, files.Count, 0)
        Return $"{count} 项"
    End Function

    ''' <summary>
    ''' 估算字体列表压缩后的体积（字节）。
    ''' </summary>
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

    ''' <summary>
    ''' 将字节数格式化为 B / KB / MB / GB 字符串。
    ''' </summary>
    Private Function FormatBytes(bytes As Long) As String
        If bytes >= 1024 * 1024 * 1024 Then Return $"{bytes / 1024 / 1024 / 1024:F2} GB"
        If bytes >= 1024 * 1024 Then Return $"{bytes / 1024 / 1024:F2} MB"
        If bytes >= 1024 Then Return $"{bytes / 1024:F0} KB"
        Return $"{bytes} B"
    End Function

    ' ─── 备注复选框 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 备注复选框状态改变时启用/禁用备注文本框。
    ''' </summary>
    Private Sub chkNote_CheckedChanged(sender As Object, e As EventArgs) Handles chkNote.CheckedChanged
        txtNote.Enabled = chkNote.Checked
        lblDate.Enabled = chkNote.Checked
        lblTime.Enabled = chkNote.Checked
        lblUserName.Enabled = chkNote.Checked
        Label1.Enabled = chkNote.Checked
        Label2.Enabled = chkNote.Checked
    End Sub

    ' ─── 打包模式切换 ─────────────────────────────────────────────────

    ''' <summary>
    ''' 默认打包模式选中时更新标签颜色。
    ''' </summary>
    Private Sub rbNormal_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDefault.CheckedChanged
        UpdateRadioButtonLabels()
    End Sub

    ''' <summary>
    ''' 标准打包模式选中时更新标签颜色。
    ''' </summary>
    Private Sub rbStandard_CheckedChanged(sender As Object, e As EventArgs) Handles rdoStandard.CheckedChanged
        UpdateRadioButtonLabels()
    End Sub

    ''' <summary>
    ''' 完整打包模式选中时更新标签颜色。
    ''' </summary>
    Private Sub rbFull_CheckedChanged(sender As Object, e As EventArgs) Handles rdoFull.CheckedChanged
        UpdateRadioButtonLabels()
    End Sub

    ''' <summary>
    ''' 按当前选中的打包模式，刷新 lblSizeTotal：预计大小 (项目数)。
    ''' </summary>
    Private Sub UpdateRadioButtonLabels()
        Dim bytes As Long
        Dim files As List(Of String)
        If rdoDefault.Checked Then
            bytes = _sizeDefault : files = NormFontFiles
        ElseIf rdoStandard.Checked Then
            bytes = _sizeStandard : files = StdFontFiles
        Else
            bytes = _sizeFull : files = FullFontFiles
        End If
        lblSizeTotal.Text = $"字体包大小: {FormatBytes(bytes)} (字体 {FormatCount(files)})"
    End Sub

    ' ─── 快速插入：用户名 / 日期 / 时间 ──────────────────────────────

    ''' <summary>
    ''' 点击「用户名」标签：在备注框光标处插入 "@" + 当前 Windows 用户名。
    ''' </summary>
    Private Sub lblUsername_Click(sender As Object, e As EventArgs) Handles lblUserName.Click
        InsertTextIntoNote("@" & Environment.UserName)
    End Sub

    ''' <summary>
    ''' 点击「日期」标签：在备注框光标处插入当前日期（yyyy/MM/dd）。
    ''' </summary>
    Private Sub lblDate_Click(sender As Object, e As EventArgs) Handles lblDate.Click
        InsertTextIntoNote(DateTime.Now.ToString("yyyy/MM/dd"))
    End Sub

    ''' <summary>
    ''' 点击「时间」标签：在备注框光标处插入当前时间（HH:mm:ss）。
    ''' </summary>
    Private Sub lblTime_Click(sender As Object, e As EventArgs) Handles lblTime.Click
        InsertTextIntoNote(DateTime.Now.ToString("HH:mm:ss"))
    End Sub

    ''' <summary>
    ''' 在 tbNote 的光标位置插入文本，超出 MaxLength 时截断。
    ''' </summary>
    Private Sub InsertTextIntoNote(text As String)
        If String.IsNullOrEmpty(text) Then Return

        If Not chkNote.Checked Then chkNote.Checked = True

        Dim currentLen As Integer = txtNote.TextLength
        Dim selLen As Integer = txtNote.SelectionLength
        Dim maxLen As Integer = txtNote.MaxLength
        Dim remain As Integer = maxLen - (currentLen - selLen)

        If remain <= 0 Then Return

        If text.Length > remain Then
            text = text.Substring(0, remain)
        End If

        txtNote.SelectedText = text
        txtNote.Focus()
    End Sub

    ' ─── 窗体关闭：打包中弹确认 ──────────────────────────────────────

    ''' <summary>
    ''' 窗体关闭时若正在打包，询问用户是否中断。
    ''' </summary>
    Private Sub FormOutput_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If _cts IsNot Nothing AndAlso Not _cts.IsCancellationRequested Then
            If e.CloseReason = CloseReason.UserClosing Then
                Dim result = MessageBox.Show("字体包正在导出，确认要中断并关闭吗？", "导出字体包",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If result <> DialogResult.Yes Then
                    e.Cancel = True
                    Return
                End If
            End If
            Try
                _cts.Cancel()
            Catch
            End Try
        End If
    End Sub

    ' ─── 保存字体包（.dnfp）─────────────────────────────────────────────

    ''' <summary>
    ''' 保存字体包按钮点击事件：异步将选中模式的字体打包为 .dnfp 文件，支持取消。
    ''' </summary>
    Private Async Sub btnSaveDNFP_Click(sender As Object, e As EventArgs) Handles btnDNFP.Click
        ' ── 选择要打包的文件 ──
        Dim filesToPack As List(Of String)
        Dim modeStr As String
        If rdoDefault.Checked Then
            filesToPack = NormFontFiles : modeStr = "Normal"
        ElseIf rdoStandard.Checked Then
            filesToPack = StdFontFiles : modeStr = "Standard"
        Else
            filesToPack = FullFontFiles : modeStr = "Full"
        End If

        If filesToPack Is Nothing OrElse filesToPack.Count = 0 Then
            MessageBox.Show("当前目录内无字体，拒绝导出。", "导出字体包", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrEmpty(JsonContent) Then
            MessageBox.Show("配置内容为空，拒绝导出。", "导出字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' ── 选保存路径 ──
        Dim outputPath As String
        Using sfd As New SaveFileDialog()
            sfd.Title = "保存字体包"
            sfd.FileName = "无标题"
            sfd.Filter = "字体包文件 (*.dnfp)|*.dnfp|压缩文件 (*.zip)|*.zip|所有文件|*.*"
            sfd.DefaultExt = "dnfp"
            If sfd.ShowDialog(Me) <> DialogResult.OK Then Return
            outputPath = sfd.FileName
        End Using

        ' ── 建立取消源 ──
        _cts = New CancellationTokenSource()
        Dim token As CancellationToken = _cts.Token

        ' ── UI 状态 ──
        Dim origTitle As String = Me.Text
        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = filesToPack.Count + 2
        ProgressBar1.Value = 0
        btnDNFP.Enabled = False
        btnCfg.Enabled = False

        ' ── 进度回调（自动编组回 UI 线程）──
        Dim progress = New Progress(Of PackProgress)(
            Sub(p)
                If IsDisposed OrElse Disposing Then Return
                Try
                    If p.Current >= ProgressBar1.Minimum AndAlso p.Current <= ProgressBar1.Maximum Then
                        ProgressBar1.Value = p.Current
                    End If
                    If Not String.IsNullOrEmpty(p.Message) Then
                        Me.Text = $"{origTitle} - {p.Message} ({p.Current}/{ProgressBar1.Maximum})"
                    End If
                Catch
                End Try
            End Sub)

        ' ── 快照：避免后台线程访问 UI 控件 ──
        Dim filesSnapshot As List(Of String) = filesToPack.ToList()
        Dim jsonSnapshot As String = JsonContent
        Dim noteSnapshot As String = If(chkNote.Checked, txtNote.Text.Trim(), "")
        Dim dirTypeSnapshot As String = DirectoryType

        Try
            Await Task.Run(Sub() DoPackZip(outputPath, filesSnapshot, jsonSnapshot,
                                           noteSnapshot, modeStr, dirTypeSnapshot, progress, token), token)

            If Not IsDisposed AndAlso Not Disposing Then
                MessageBox.Show("字体包已导出。", "导出字体包", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As OperationCanceledException
            ' 用户取消或关闭：删除未完成的输出文件
            Try
                If File.Exists(outputPath) Then File.Delete(outputPath)
            Catch
            End Try
            'If Not IsDisposed AndAlso Not Disposing Then
            '    MessageBox.Show("导出已取消，未完成的文件已删除。", "导出字体包",
            '                    MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If
        Catch ex As Exception
            ' 出错时也清理半成品
            Try
                If File.Exists(outputPath) Then File.Delete(outputPath)
            Catch
            End Try
            If Not IsDisposed AndAlso Not Disposing Then
                MessageBox.Show("导出字体包时发生错误：" & ex.Message, "导出字体包",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            Try
                _cts?.Dispose()
            Catch
            End Try
            _cts = Nothing

            If Not IsDisposed AndAlso Not Disposing Then
                ProgressBar1.Visible = False
                ProgressBar1.Value = 0
                Me.Text = origTitle
                btnDNFP.Enabled = True
                btnCfg.Enabled = True
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 后台线程：打包所有字体到 .dnfp 文件。支持取消。
    ''' </summary>
    Private Sub DoPackZip(outputPath As String,
                          filesToPack As List(Of String),
                          jsonContent As String,
                          noteText As String,
                          modeStr As String,
                          dirType As String,
                          progress As IProgress(Of PackProgress),
                          token As CancellationToken)
        Dim tempJsonPath As String = Path.Combine(Path.GetTempPath(),
                                                  "dnfe_export_" & Guid.NewGuid().ToString("N") & ".json")
        Try
            token.ThrowIfCancellationRequested()

            File.WriteAllText(tempJsonPath, jsonContent, Encoding.UTF8)
            If File.Exists(outputPath) Then File.Delete(outputPath)

            Using zipArchive As ZipArchive = ZipFile.Open(outputPath, ZipArchiveMode.Create)
                ' ── 1. index.json（小文本，保留 Optimal）──
                token.ThrowIfCancellationRequested()
                zipArchive.CreateEntryFromFile(tempJsonPath, "fonts/index.json", CompressionLevel.Optimal)
                progress.Report(New PackProgress With {.Current = 1, .Message = "写入: index.json"})

                ' ── 2. 字体文件（Fastest，比 Optimal 快 3~5 倍）──
                Dim n As Integer = 1
                For Each fontFile As String In filesToPack
                    token.ThrowIfCancellationRequested()
                    If File.Exists(fontFile) Then
                        Dim fn As String = Path.GetFileName(fontFile)
                        zipArchive.CreateEntryFromFile(fontFile, "fonts/" & fn, CompressionLevel.Fastest)
                        n += 1
                        progress.Report(New PackProgress With {.Current = n, .Message = "复制: " & fn})
                    End If
                Next

                ' ── 3. verification.txt ──
                token.ThrowIfCancellationRequested()
                Dim verEntry As ZipArchiveEntry = zipArchive.CreateEntry("verification.txt", CompressionLevel.Optimal)
                Using writer As New StreamWriter(verEntry.Open(), Encoding.UTF8)
                    writer.WriteLine("app=DDNetForeEdit")
                    writer.WriteLine($"date={DateTime.Now:yyyy/MM/dd HH:mm}")
                    writer.WriteLine($"mode={modeStr}")
                    writer.WriteLine($"source={dirType}")
                    writer.WriteLine($"fonts={filesToPack.Count}")
                    If Not String.IsNullOrWhiteSpace(noteText) Then
                        writer.WriteLine($"note={noteText}")
                    End If
                End Using
                n += 1
                progress.Report(New PackProgress With {.Current = n, .Message = "写入: verification.txt"})
            End Using
        Finally
            Try
                If File.Exists(tempJsonPath) Then File.Delete(tempJsonPath)
            Catch
            End Try
        End Try
    End Sub

    ' ─── 保存配置（.json）─────────────────────────────────────────────

    ''' <summary>
    ''' 保存配置按钮点击事件：将当前 index.json 内容保存为独立文件。
    ''' </summary>
    Private Sub btnSaveJson_Click(sender As Object, e As EventArgs) Handles btnCfg.Click
        If String.IsNullOrEmpty(JsonContent) Then
            MessageBox.Show("配置内容为空，拒绝导出。", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using sfd As New SaveFileDialog
            sfd.Title = "另存为「index.json」"
            sfd.FileName = "index.json"
            sfd.Filter = "JSON|index.json|所有文件|*.*"
            If sfd.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                If File.Exists(sfd.FileName) Then File.Delete(sfd.FileName)
                File.WriteAllText(sfd.FileName, JsonContent, Encoding.UTF8)
                MessageBox.Show("配置文件已导出。", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"配置文件导出失败：{ex.Message}", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' ─── 按 Esc 关闭 ────────────────────────────────────────────────
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            Dim cbo = TryCast(ActiveControl, ComboBox)
            If cbo IsNot Nothing AndAlso cbo.DroppedDown Then
                Return MyBase.ProcessCmdKey(msg, keyData)
            End If

            Me.Close()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class