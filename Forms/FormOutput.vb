Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Text

''' <summary>
''' 导出窗体：负责将当前字体目录的字体和配置导出为 .dnfp 字体包或 index.json 配置文件。
''' </summary>
Public Class FormOutput

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

    ' ─── 窗体生命周期 ──────────────────────────────────────────────────

    ''' <summary>窗体加载事件：初始化控件状态。</summary>
    Private Sub FormOutput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 防御：确保列表不为 Nothing
        If NormFontFiles Is Nothing Then NormFontFiles = New List(Of String)
        If StdFontFiles Is Nothing Then StdFontFiles = New List(Of String)
        If FullFontFiles Is Nothing Then FullFontFiles = New List(Of String)
        If String.IsNullOrEmpty(JsonContent) Then JsonContent = ""

        tbNote.MaxLength = 40
        rbNormal.Checked = True
        chkNote.Checked = False
        tbNote.Enabled = False
        tbNote.Text = ""

        Dim hasAnyFonts As Boolean = NormFontFiles.Count > 0 OrElse StdFontFiles.Count > 0 OrElse FullFontFiles.Count > 0
        Dim hasJson As Boolean = Not String.IsNullOrEmpty(JsonContent)

        If Not hasAnyFonts OrElse Not hasJson Then
            btnSaveDNFP.Enabled = False
            btnSaveJson.Enabled = False
            tblExportMode.Enabled = False
            Label3.Enabled = False
            lblUsername.Enabled = False
            lblTime.Enabled = False
            lblDate.Enabled = False
        End If

        RefreshPackSizeLabels()
        UpdateRadioButtonLabels()
    End Sub

    ' ─── 估算包体积 ──────────────────────────────────────────────────
    ''' <summary>刷新三种打包模式的预估体积标签和字体数量标签。</summary>
    Private Sub RefreshPackSizeLabels()
        lblNormal.Text = FormatBytes(EstimatePackedSize(NormFontFiles))
        lblStandard.Text = FormatBytes(EstimatePackedSize(StdFontFiles))
        lblFull.Text = FormatBytes(EstimatePackedSize(FullFontFiles))

        lblCount1.Text = FormatCount(NormFontFiles)
        lblCount2.Text = FormatCount(StdFontFiles)
        lblCount3.Text = FormatCount(FullFontFiles)
    End Sub

    ''' <summary>把字体列表格式化为 "共 N 项"。</summary>
    Private Function FormatCount(files As List(Of String)) As String
        Dim count As Integer = If(files IsNot Nothing, files.Count, 0)
        Return $"字体 {count} 项"
    End Function

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
        Label3.Enabled = chkNote.Checked
        lblDate.Enabled = chkNote.Checked
        lblTime.Enabled = chkNote.Checked
        lblUsername.Enabled = chkNote.Checked
    End Sub

    ' ─── 打包模式切换 ─────────────────────────────────────────────────

    ''' <summary>默认打包模式选中时更新标签颜色。</summary>
    Private Sub rbNormal_CheckedChanged(sender As Object, e As EventArgs) Handles rbNormal.CheckedChanged
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

    ''' <summary>更新打包模式 RadioButton 的标签颜色（体积标签 + 数量标签同步）。</summary>
    Private Sub UpdateRadioButtonLabels()
        Dim activeColor As Color = Color.FromArgb(64, 64, 64)
        Dim inactiveColor As Color = Color.DarkGray

        If rbNormal.Checked Then
            lblNormal.ForeColor = activeColor
            lblCount1.ForeColor = activeColor
            lblStandard.ForeColor = inactiveColor
            lblCount2.ForeColor = inactiveColor
            lblFull.ForeColor = inactiveColor
            lblCount3.ForeColor = inactiveColor
        ElseIf rbStandard.Checked Then
            lblNormal.ForeColor = inactiveColor
            lblCount1.ForeColor = inactiveColor
            lblStandard.ForeColor = activeColor
            lblCount2.ForeColor = activeColor
            lblFull.ForeColor = inactiveColor
            lblCount3.ForeColor = inactiveColor
        Else
            lblNormal.ForeColor = inactiveColor
            lblCount1.ForeColor = inactiveColor
            lblStandard.ForeColor = inactiveColor
            lblCount2.ForeColor = inactiveColor
            lblFull.ForeColor = activeColor
            lblCount3.ForeColor = activeColor
        End If
    End Sub

    ' ─── 快速插入：用户名 / 日期 / 时间 ──────────────────────────────

    ''' <summary>点击「用户名」标签：在备注框光标处插入 "@" + 当前 Windows 用户名。</summary>
    Private Sub lblUsername_Click(sender As Object, e As EventArgs) Handles lblUsername.Click
        InsertTextIntoNote("@" & Environment.UserName)
    End Sub

    ''' <summary>点击「日期」标签：在备注框光标处插入当前日期（yyyy/MM/dd）。</summary>
    Private Sub lblDate_Click(sender As Object, e As EventArgs) Handles lblDate.Click
        InsertTextIntoNote(DateTime.Now.ToString("yyyy/MM/dd"))
    End Sub

    ''' <summary>点击「时间」标签：在备注框光标处插入当前时间（HH:mm:ss）。</summary>
    Private Sub lblTime_Click(sender As Object, e As EventArgs) Handles lblTime.Click
        InsertTextIntoNote(DateTime.Now.ToString("HH:mm:ss"))
    End Sub

    ''' <summary>在 tbNote 的光标位置插入文本，超出 MaxLength 时截断。</summary>
    Private Sub InsertTextIntoNote(text As String)
        If String.IsNullOrEmpty(text) Then Return

        If Not chkNote.Checked Then chkNote.Checked = True

        ' 计算剩余可用长度：MaxLength - (现有长度 - 被选中的长度)
        Dim currentLen As Integer = tbNote.TextLength
        Dim selLen As Integer = tbNote.SelectionLength
        Dim maxLen As Integer = tbNote.MaxLength
        Dim remain As Integer = maxLen - (currentLen - selLen)

        If remain <= 0 Then Return

        ' 超出剩余空间则截断
        If text.Length > remain Then
            text = text.Substring(0, remain)
        End If

        tbNote.SelectedText = text
        tbNote.Focus()
    End Sub

    ' ─── 保存字体包（.dnfp）─────────────────────────────────────────────

    ''' <summary>保存字体包按钮点击事件：将选中模式的字体打包为 .dnfp 文件。</summary>
    Private Sub btnSaveDNFP_Click(sender As Object, e As EventArgs) Handles btnSaveDNFP.Click
        Dim filesToPack As List(Of String)
        Dim defaultFileName As String = "无标题"

        If rbNormal.Checked Then
            filesToPack = NormFontFiles
        ElseIf rbStandard.Checked Then
            filesToPack = StdFontFiles
        Else
            filesToPack = FullFontFiles
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
            btnSaveDNFP.Enabled = False
            btnSaveJson.Enabled = False

            Try
                File.WriteAllText(tempJsonPath, JsonContent, Encoding.UTF8)
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
                    If rbNormal.Checked Then
                        modeStr = "Normal"
                    ElseIf rbStandard.Checked Then
                        modeStr = "Standard"
                    Else
                        modeStr = "Full"
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
                    Me.Text = $"{origTitle} - 写入: verification.txt"
                End Using
            Catch ex As Exception
                MessageBox.Show("导出字体包时发生错误：" & ex.Message, "导出字体包", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ProgressBar1.Visible = False
                ProgressBar1.Value = 0
                Me.Text = origTitle
                btnSaveDNFP.Enabled = True
                btnSaveJson.Enabled = True
                Try
                    If File.Exists(tempJsonPath) Then File.Delete(tempJsonPath)
                Catch
                End Try
            End Try
        End Using
    End Sub

    ' ─── 保存配置（.json）─────────────────────────────────────────────

    ''' <summary>保存配置按钮点击事件：将当前 index.json 内容保存为独立文件。</summary>
    Private Sub btnSaveJson_Click(sender As Object, e As EventArgs) Handles btnSaveJson.Click
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
                'MessageBox.Show("配置文件已导出。", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"配置文件导出失败：{ex.Message}", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

End Class