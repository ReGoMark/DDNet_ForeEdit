Imports System.IO
Imports System.IO.Compression
Imports System.Text

''' <summary>
''' 备份对话框：选择要备份的 DDNet 用户数据范围，打包为 zip。
''' 备份源：%AppData%\Roaming\DDNet\
''' 备份内容：settings_ddnet.cfg、其他 *.cfg、ddnet-saves.txt、demos\、videos\、assets\、ghosts\
''' 压缩包内路径结构与实际目录一致。
''' </summary>
Public Class DialogBackup

    ''' <summary>DDNet 用户数据根目录。</summary>
    Private ReadOnly DdnetDir As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DDNet")

    ''' <summary>FAT32 / 部分网盘的 4 GB 单文件限制（用于超限颜色提示）。</summary>
    Private Const SizeWarnLimit As Long = 4L * 1024 * 1024 * 1024

    ''' <summary>各项大小的缓存（键：category；值：字节数）。Load 时扫描一次，之后只做加法。</summary>
    Private ReadOnly _sizeCache As New Dictionary(Of String, Long)

    ' ─── 窗体生命周期 ──────────────────────────────────────────────────

    Private Sub DialogBackup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 默认勾选全部
        'chkSettings.Checked = True
        'chkCfg.Checked = True
        'chkSaves.Checked = True
        'chkDemos.Checked = True
        'chkVideos.Checked = True
        'chkAssets.Checked = True
        'chkGhosts.Checked = True

        'ToolTip1.SetToolTip(btnSelectAll, "切换全选/全不选")
        'ToolTip1.SetToolTip(btnBackupTo, "选择保存位置并开始备份")
        'ToolTip1.SetToolTip(btnCancel, "关闭窗口")

        RefreshSizes()
        UpdateRowEnabledStates()

        ' 双缓冲
        Dim dblProp = GetType(Control).GetProperty("DoubleBuffered",
                                                   Reflection.BindingFlags.Instance Or
                                                   Reflection.BindingFlags.NonPublic)
        If dblProp IsNot Nothing Then
            dblProp.SetValue(Me, True, Nothing)
        End If
    End Sub

    ' ─── 大小与数量显示 ──────────────────────────────────────────────

    ''' <summary>刷新 7 组大小 + 数量标签，并缓存各项字节数。</summary>
    Private Sub RefreshSizes()
        ' settings_ddnet.cfg
        Dim a = GetSingleFileInfo("settings_ddnet.cfg")
        _sizeCache("settings") = a.size
        lblSize1.Text = FormatSizeOrDash(a.size, a.count)
        lblCount1.Text = FormatCountOrDash(a.count)

        ' 其他 *.cfg
        Dim b = GetOtherCfgInfo()
        _sizeCache("cfg") = b.size
        lblSize2.Text = FormatSizeOrDash(b.size, b.count)
        lblCount2.Text = FormatCountOrDash(b.count)

        ' ddnet-saves.txt
        Dim c = GetSingleFileInfo("ddnet-saves.txt")
        _sizeCache("saves") = c.size
        lblSize3.Text = FormatSizeOrDash(c.size, c.count)
        lblCount3.Text = FormatCountOrDash(c.count)

        ' 文件夹
        Dim d = GetFolderInfo("demos")
        _sizeCache("demos") = d.size
        lblSize4.Text = FormatSizeOrDash(d.size, d.count)
        lblCount4.Text = FormatCountOrDash(d.count)

        Dim e = GetFolderInfo("videos")
        _sizeCache("videos") = e.size
        lblSize5.Text = FormatSizeOrDash(e.size, e.count)
        lblCount5.Text = FormatCountOrDash(e.count)

        Dim f = GetFolderInfo("assets")
        _sizeCache("assets") = f.size
        lblSize6.Text = FormatSizeOrDash(f.size, f.count)
        lblCount6.Text = FormatCountOrDash(f.count)

        Dim g = GetFolderInfo("ghosts")
        _sizeCache("ghosts") = g.size
        lblSize7.Text = FormatSizeOrDash(g.size, g.count)
        lblCount7.Text = FormatCountOrDash(g.count)

        RefreshTotalSize()
    End Sub

    ''' <summary>刷新 lblSizeTotal：勾选项的字节数之和。</summary>
    Private Sub RefreshTotalSize()
        Dim total As Long = 0
        If chkSettings.Checked AndAlso _sizeCache.ContainsKey("settings") Then total += _sizeCache("settings")
        If chkCfg.Checked AndAlso _sizeCache.ContainsKey("cfg") Then total += _sizeCache("cfg")
        If chkSaves.Checked AndAlso _sizeCache.ContainsKey("saves") Then total += _sizeCache("saves")
        If chkDemos.Checked AndAlso _sizeCache.ContainsKey("demos") Then total += _sizeCache("demos")
        If chkVideos.Checked AndAlso _sizeCache.ContainsKey("videos") Then total += _sizeCache("videos")
        If chkAssets.Checked AndAlso _sizeCache.ContainsKey("assets") Then total += _sizeCache("assets")
        If chkGhosts.Checked AndAlso _sizeCache.ContainsKey("ghosts") Then total += _sizeCache("ghosts")
        lblSizeTotal.Text = $"备份大小: {FormatBytes(total)}"
    End Sub

    ''' <summary>单个文件信息：count 为 0 表示不存在。</summary>
    Private Function GetSingleFileInfo(fileName As String) As (count As Integer, size As Long)
        Try
            Dim f = Path.Combine(DdnetDir, fileName)
            If Not File.Exists(f) Then Return (0, 0L)
            Return (1, New FileInfo(f).Length)
        Catch
            Return (0, 0L)
        End Try
    End Function

    ''' <summary>其他 *.cfg（除 settings_ddnet.cfg）的合计数量与大小。</summary>
    Private Function GetOtherCfgInfo() As (count As Integer, size As Long)
        Try
            If Not Directory.Exists(DdnetDir) Then Return (0, 0L)
            Dim total As Long = 0
            Dim count As Integer = 0
            For Each f In Directory.GetFiles(DdnetDir, "*.cfg")
                If Path.GetFileName(f).Equals("settings_ddnet.cfg", StringComparison.OrdinalIgnoreCase) Then Continue For
                Try
                    total += New FileInfo(f).Length
                    count += 1
                Catch
                End Try
            Next
            Return (count, total)
        Catch
            Return (0, 0L)
        End Try
    End Function

    ''' <summary>文件夹递归统计：文件数量与字节总数。</summary>
    Private Function GetFolderInfo(subFolder As String) As (count As Integer, size As Long)
        Try
            Dim dir = Path.Combine(DdnetDir, subFolder)
            If Not Directory.Exists(dir) Then Return (0, 0L)

            Dim total As Long = 0
            Dim count As Integer = 0
            For Each f In Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories)
                Try
                    total += New FileInfo(f).Length
                    count += 1
                Catch
                End Try
            Next

            Return (count, total)
        Catch
            Return (0, 0L)
        End Try
    End Function

    ''' <summary>count 为 0 时返回 "-"，否则返回格式化的字节大小。</summary>
    Private Function FormatSizeOrDash(size As Long, count As Integer) As String
        If count = 0 Then Return "-"
        Return FormatBytes(size)
    End Function

    ''' <summary>count 为 0 时返回 "-"，否则返回 "N 项"。</summary>
    Private Function FormatCountOrDash(count As Integer) As String
        If count = 0 Then Return "-"
        Return $"{count} 项"
    End Function

    Private Function FormatBytes(bytes As Long) As String
        If bytes >= 1024L * 1024 * 1024 Then Return (bytes / 1024 / 1024 / 1024).ToString("F2") & " GB"
        If bytes >= 1024L * 1024 Then Return (bytes / 1024 / 1024).ToString("F2") & " MB"
        If bytes >= 1024 Then Return (bytes / 1024).ToString("F1") & " KB"
        Return bytes.ToString() & " B"
    End Function

    ' ─── 勾选状态变化时刷新总大小 ────────────────────────────────────

    Private Sub AnyCheckBox_CheckedChanged(sender As Object, e As EventArgs) _
        Handles chkSettings.CheckedChanged,
                chkCfg.CheckedChanged,
                chkSaves.CheckedChanged,
                chkDemos.CheckedChanged,
                chkVideos.CheckedChanged,
                chkAssets.CheckedChanged,
                chkGhosts.CheckedChanged
        RefreshTotalSize()
        UpdateRowEnabledStates()
    End Sub

    ' ─── 全选 / 全不选（按钮文本固定为"全选"）────────────────────

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        Dim allChecked As Boolean =
            chkSettings.Checked AndAlso chkCfg.Checked AndAlso chkSaves.Checked AndAlso
            chkDemos.Checked AndAlso chkVideos.Checked AndAlso
            chkAssets.Checked AndAlso chkGhosts.Checked

        Dim newValue As Boolean = Not allChecked

        ' 关闭总大小刷新（避免每个勾选触发一次计算）
        ' 这里简单处理：直接改，最后手动刷一次
        chkSettings.Checked = newValue
        chkCfg.Checked = newValue
        chkSaves.Checked = newValue
        chkDemos.Checked = newValue
        chkVideos.Checked = newValue
        chkAssets.Checked = newValue
        chkGhosts.Checked = newValue

        ' 各 CheckedChanged 已触发 RefreshTotalSize，不用重复刷
    End Sub

    ' ─── 备份 ──────────────────────────────────────────────────────

    Private Sub btnBackupTo_Click(sender As Object, e As EventArgs) Handles btnBackupTo.Click
        If Not (chkSettings.Checked OrElse chkCfg.Checked OrElse chkSaves.Checked OrElse
                chkDemos.Checked OrElse chkVideos.Checked OrElse
                chkAssets.Checked OrElse chkGhosts.Checked) Then
            'MessageBox.Show("请至少选择一项要备份的内容。", "备份",
            '                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not Directory.Exists(DdnetDir) Then
            MessageBox.Show($"找不到 DDNet 数据目录：{DdnetDir}", "备份",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Using sfd As New SaveFileDialog()
            sfd.Title = "保存备份"
            sfd.Filter = "Zip 文件 (*.zip)|*.zip|所有文件|*.*"
            sfd.FileName = "无标题" & ".zip"
            sfd.DefaultExt = "zip"
            If sfd.ShowDialog(Me) <> DialogResult.OK Then Return

            DoBackup(sfd.FileName)
        End Using
    End Sub

    ''' <summary>执行备份：收集勾选项的文件，写入 zip，末尾追加 verification.txt 元数据。</summary>
    Private Sub DoBackup(zipPath As String)
        Dim entries As New List(Of Tuple(Of String, String))

        If chkSettings.Checked Then
            Dim f = Path.Combine(DdnetDir, "settings_ddnet.cfg")
            If File.Exists(f) Then entries.Add(Tuple.Create("settings_ddnet.cfg", f))
        End If

        If chkSaves.Checked Then
            Dim f = Path.Combine(DdnetDir, "ddnet-saves.txt")
            If File.Exists(f) Then entries.Add(Tuple.Create("ddnet-saves.txt", f))
        End If

        If chkCfg.Checked Then
            Try
                For Each f In Directory.GetFiles(DdnetDir, "*.cfg")
                    Dim fn = Path.GetFileName(f)
                    If fn.Equals("settings_ddnet.cfg", StringComparison.OrdinalIgnoreCase) Then Continue For
                    entries.Add(Tuple.Create(fn, f))
                Next
            Catch
            End Try
        End If

        Dim folderMap As New List(Of Tuple(Of CheckBox, String)) From {
            Tuple.Create(chkDemos, "demos"),
            Tuple.Create(chkVideos, "videos"),
            Tuple.Create(chkAssets, "assets"),
            Tuple.Create(chkGhosts, "ghosts")
        }
        For Each t In folderMap
            If Not t.Item1.Checked Then Continue For
            Dim absFolder = Path.Combine(DdnetDir, t.Item2)
            If Not Directory.Exists(absFolder) Then Continue For
            Try
                For Each f In Directory.EnumerateFiles(absFolder, "*", SearchOption.AllDirectories)
                    Dim relSub = f.Substring(absFolder.Length).TrimStart("\"c, "/"c)
                    Dim rel = t.Item2 & "\" & relSub
                    entries.Add(Tuple.Create(rel, f))
                Next
            Catch
            End Try
        Next

        If entries.Count = 0 Then
            MessageBox.Show("没有可备份的文件。", "备份",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim origTitle = Me.Text
        Dim totalSteps As Integer = entries.Count + 1   ' +1 是 verification.txt

        ProgressBar2.Visible = True
        ProgressBar2.Minimum = 0
        ProgressBar2.Maximum = totalSteps
        ProgressBar2.Value = 0
        btnBackupTo.Enabled = False
        btnCancel.Enabled = False
        btnSelectAll.Enabled = False

        Try
            If File.Exists(zipPath) Then File.Delete(zipPath)

            Using zip As ZipArchive = ZipFile.Open(zipPath, ZipArchiveMode.Create)
                ' 逐个写入实际文件
                Dim n As Integer = 0
                For Each entry In entries
                    Dim relPath As String = entry.Item1.Replace("\"c, "/"c)
                    Dim absPath As String = entry.Item2
                    Try
                        zip.CreateEntryFromFile(absPath, relPath, CompressionLevel.Optimal)
                    Catch
                    End Try
                    n += 1
                    ProgressBar2.Value = n
                    Me.Text = $"{origTitle} - 拷贝: {Path.GetFileName(absPath)} ({n}/{totalSteps})"
                Next

                ' 最后写入 verification.txt（与 Blade 的 .dnfp 格式一致）
                Dim verEntry As ZipArchiveEntry = zip.CreateEntry("verification.txt")
                Using writer As New StreamWriter(verEntry.Open(), Encoding.UTF8)
                    writer.WriteLine("app=DDNetForeEdit")
                    writer.WriteLine($"date={DateTime.Now:yyyy/MM/dd HH:mm}")
                    writer.WriteLine("mode=Backup")
                    writer.WriteLine("source=Local")
                    writer.WriteLine($"files={entries.Count}")
                End Using
                n += 1
                ProgressBar2.Value = n
                Me.Text = $"{origTitle} - 写入: verification.txt ({n}/{totalSteps})"
            End Using

            Me.Text = origTitle
            MessageBox.Show("用户数据已备份。", "备份",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"备份失败：{ex.Message}", "备份",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ProgressBar2.Visible = False
            ProgressBar2.Value = 0
            Me.Text = origTitle
            btnBackupTo.Enabled = True
            btnCancel.Enabled = True
            btnSelectAll.Enabled = True
        End Try
    End Sub

    ' ─── 快照（保存配置到程序目录的 backups） ──────────────────────

    ''' <summary>
    ''' 快照：把 settings_ddnet.cfg 和 ddnet-saves.txt 复制到程序目录的 backups\ 下。
    ''' 每次点击都覆盖，不做时间戳。
    ''' </summary>
    Private Sub btnSnapshot_Click(sender As Object, e As EventArgs) Handles btnSnapshot.Click
        If Not Directory.Exists(DdnetDir) Then
            MessageBox.Show($"找不到 DDNet 数据目录：{DdnetDir}", "快照",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim backupsDir = Path.Combine(Application.StartupPath, "backup")
        Try
            Directory.CreateDirectory(backupsDir)
        Catch ex As Exception
            MessageBox.Show($"无法创建备份目录：{ex.Message}", "快照",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Dim srcSettings = Path.Combine(DdnetDir, "settings_ddnet.cfg")
        Dim srcSaves = Path.Combine(DdnetDir, "ddnet-saves.txt")
        Dim copied As New List(Of String)
        Dim errors As New List(Of String)

        Try
            If File.Exists(srcSettings) Then
                File.Copy(srcSettings, Path.Combine(backupsDir, "settings_ddnet.cfg"), True)
                copied.Add("settings_ddnet.cfg")
            End If
        Catch ex As Exception
            errors.Add($"settings_ddnet.cfg：{ex.Message}")
        End Try

        Try
            If File.Exists(srcSaves) Then
                File.Copy(srcSaves, Path.Combine(backupsDir, "ddnet-saves.txt"), True)
                copied.Add("ddnet-saves.txt")
            End If
        Catch ex As Exception
            errors.Add($"ddnet-saves.txt：{ex.Message}")
        End Try

        ' 结果提示
        If errors.Count > 0 Then
            MessageBox.Show("快照部分失败：" & vbCrLf & String.Join(vbCrLf, errors), "快照",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf copied.Count = 0 Then
            MessageBox.Show("没有可保存的文件。", "快照",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            MessageBox.Show($"快照已保存到本程序安装目录下 backup 文件夹", "快照", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' ─── 取消 ──────────────────────────────────────────────────────

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' 根据复选框状态更新每一行标签（大小 + 数量）的启用状态。
    ''' 未勾选时，标签变灰（模拟禁用视觉），避免用户误以为该项会被备份。
    ''' </summary>
    Private Sub UpdateRowEnabledStates()
        SetRowEnabled(chkSettings, lblSize1, lblCount1)
        SetRowEnabled(chkCfg, lblSize2, lblCount2)
        SetRowEnabled(chkSaves, lblSize3, lblCount3)
        SetRowEnabled(chkDemos, lblSize4, lblCount4)
        SetRowEnabled(chkVideos, lblSize5, lblCount5)
        SetRowEnabled(chkAssets, lblSize6, lblCount6)
        SetRowEnabled(chkGhosts, lblSize7, lblCount7)
    End Sub

    ''' <summary>
    ''' 单行标签的启用/禁用视觉切换。
    ''' 勾选 → 正常色；未勾选 → 灰色。
    ''' </summary>
    Private Sub SetRowEnabled(chk As CheckBox, sizeLbl As Label, countLbl As Label)
        Dim c As Color = If(chk.Checked, Color.FromArgb(64, 64, 64), Color.DarkGray)
        sizeLbl.ForeColor = c
        countLbl.ForeColor = c
    End Sub
End Class