Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks

''' <summary>
''' 备份对话框：选择要备份的 DDNet 用户数据范围，打包为 zip。
''' 备份源：%AppData%\Roaming\DDNet\
''' 备份内容：settings_ddnet.cfg、其他 *.cfg、ddnet-saves.txt、demos\、videos\、assets\、ghosts\
''' 压缩包内路径结构与实际目录一致。
''' 打包在后台线程执行，支持关闭窗体取消。
''' </summary>
Public Class FormBackup

    ''' <summary>DDNet 用户数据根目录。</summary>
    Private ReadOnly DdnetDir As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DDNet")

    ''' <summary>FAT32 / 部分网盘的 4 GB 单文件限制（用于超限颜色提示）。</summary>
    Private Const SizeWarnLimit As Long = 4L * 1024 * 1024 * 1024

    ''' <summary>各项大小的缓存（键：category；值：字节数）。Load 时扫描一次，之后只做加法。</summary>
    Private ReadOnly _sizeCache As New Dictionary(Of String, Long)

    ''' <summary>当前备份操作的取消令牌源。为 Nothing 表示无操作进行中。</summary>
    Private _cts As CancellationTokenSource = Nothing

    ' ─── 后台备份辅助类型 ────────────────────────────────────────────

    ''' <summary>备份范围选项（主线程快照，避免后台访问 CheckBox）。</summary>
    Private Class BackupOptions
        Public Settings As Boolean
        Public Saves As Boolean
        Public Cfg As Boolean
        Public Demos As Boolean
        Public Videos As Boolean
        Public Assets As Boolean
        Public Ghosts As Boolean
    End Class

    ''' <summary>后台备份的进度载体。</summary>
    Private Class BackupProgress
        Public Current As Integer
        Public Total As Integer
        Public Message As String
    End Class

    ' ─── 窗体生命周期 ──────────────────────────────────────────────────

    Private Sub DialogBackup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    ''' <summary>窗体关闭时若正在备份，询问用户是否中断。</summary>
    Private Sub FormBackup_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If _cts IsNot Nothing AndAlso Not _cts.IsCancellationRequested Then
            If e.CloseReason = CloseReason.UserClosing Then
                Dim result = MessageBox.Show("备份正在进行，确认要中断并关闭吗？", "备份",
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

    ' ─── 大小与数量显示 ──────────────────────────────────────────────

    ''' <summary>刷新 7 组大小 + 数量标签，并缓存各项字节数。</summary>
    Private Sub RefreshSizes()
        ' settings_ddnet.cfg
        Dim a = GetSingleFileInfo("settings_ddnet.cfg")
        _sizeCache("settings") = a.size
        lblSizeSettings.Text = FormatSizeOrDash(a.size, a.count)
        lblCountSettings.Text = FormatCountOrDash(a.count)

        ' 其他 *.cfg
        Dim b = GetOtherCfgInfo()
        _sizeCache("cfg") = b.size
        lblSizeCfg.Text = FormatSizeOrDash(b.size, b.count)
        lblCountCfg.Text = FormatCountOrDash(b.count)

        ' ddnet-saves.txt
        Dim c = GetSingleFileInfo("ddnet-saves.txt")
        _sizeCache("saves") = c.size
        lblSizeSaves.Text = FormatSizeOrDash(c.size, c.count)
        lblCountSaves.Text = FormatCountOrDash(c.count)

        ' 文件夹
        Dim d = GetFolderInfo("demos")
        _sizeCache("demos") = d.size
        lblSizeDemos.Text = FormatSizeOrDash(d.size, d.count)
        lblCountDemos.Text = FormatCountOrDash(d.count)

        Dim e = GetFolderInfo("videos")
        _sizeCache("videos") = e.size
        lblSizeVideos.Text = FormatSizeOrDash(e.size, e.count)
        lblCountVideos.Text = FormatCountOrDash(e.count)

        Dim f = GetFolderInfo("assets")
        _sizeCache("assets") = f.size
        lblSizeAssets.Text = FormatSizeOrDash(f.size, f.count)
        lblCountAssets.Text = FormatCountOrDash(f.count)

        Dim g = GetFolderInfo("ghosts")
        _sizeCache("ghosts") = g.size
        lblSizeGhosts.Text = FormatSizeOrDash(g.size, g.count)
        lblCountGhosts.Text = FormatCountOrDash(g.count)

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

        chkSettings.Checked = newValue
        chkCfg.Checked = newValue
        chkSaves.Checked = newValue
        chkDemos.Checked = newValue
        chkVideos.Checked = newValue
        chkAssets.Checked = newValue
        chkGhosts.Checked = newValue
    End Sub

    ' ─── 备份（异步）──────────────────────────────────────────────

    Private Async Sub btnBackupTo_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        If _cts IsNot Nothing Then Return   ' 防重入

        If Not (chkSettings.Checked OrElse chkCfg.Checked OrElse chkSaves.Checked OrElse
                chkDemos.Checked OrElse chkVideos.Checked OrElse
                chkAssets.Checked OrElse chkGhosts.Checked) Then
            Return
        End If

        If Not Directory.Exists(DdnetDir) Then
            MessageBox.Show($"找不到 DDNet 数据目录：{DdnetDir}", "备份",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' ── 选保存路径 ──
        Dim outputPath As String
        Using sfd As New SaveFileDialog()
            sfd.Title = "保存备份"
            sfd.Filter = "Zip 文件 (*.zip)|*.zip|所有文件|*.*"
            sfd.FileName = "无标题.zip"
            sfd.DefaultExt = "zip"
            If sfd.ShowDialog(Me) <> DialogResult.OK Then Return
            outputPath = sfd.FileName
        End Using

        ' ── 快照复选框状态 ──
        Dim opt As New BackupOptions With {
            .Settings = chkSettings.Checked,
            .Saves = chkSaves.Checked,
            .Cfg = chkCfg.Checked,
            .Demos = chkDemos.Checked,
            .Videos = chkVideos.Checked,
            .Assets = chkAssets.Checked,
            .Ghosts = chkGhosts.Checked
        }
        Dim rootDir As String = DdnetDir

        ' ── 建立取消源 ──
        _cts = New CancellationTokenSource()
        Dim token As CancellationToken = _cts.Token

        ' ── UI 状态 ──
        Dim origTitle As String = Me.Text
        ProgressBar2.Visible = True
        ProgressBar2.Minimum = 0
        ProgressBar2.Maximum = 1
        ProgressBar2.Value = 0
        btnBackup.Enabled = False
        btnCancel.Enabled = False
        btnSelectAll.Enabled = False
        btnSnapshot.Enabled = False

        ' ── 进度回调 ──
        Dim progress = New Progress(Of BackupProgress)(
            Sub(p)
                If IsDisposed OrElse Disposing Then Return
                Try
                    If p.Total > 0 AndAlso ProgressBar2.Maximum <> p.Total Then
                        ProgressBar2.Maximum = p.Total
                    End If
                    If p.Current >= ProgressBar2.Minimum AndAlso p.Current <= ProgressBar2.Maximum Then
                        ProgressBar2.Value = p.Current
                    End If
                    If Not String.IsNullOrEmpty(p.Message) Then
                        Me.Text = $"{origTitle} - {p.Message}"
                    End If
                Catch
                End Try
            End Sub)

        ' ── 结果状态（后台线程写入，Await 后读取）──
        Dim succeeded As Boolean = False
        Dim cancelled As Boolean = False
        Dim errorMsg As String = ""

        ' ── 后台执行：整个 Lambda 自己 Try/Catch，对外不抛异常 ──
        Try
            Await Task.Run(Sub()
                               Try
                                   If DoBackupCore(outputPath, rootDir, opt, progress, token) Then
                                       succeeded = True
                                   Else
                                       cancelled = True
                                   End If
                               Catch ex As Exception
                                   errorMsg = ex.Message
                               End Try
                           End Sub)
        Catch ex As Exception
            ' 极端情况：Task 本身出错（几乎不会发生）
            errorMsg = ex.Message
        End Try

        ' ── 处理结果 ──
        If IsDisposed OrElse Disposing Then
            ' 窗体已 Dispose，静默清理
            Try
                _cts?.Dispose()
            Catch
            End Try
            _cts = Nothing
            Try
                If File.Exists(outputPath) AndAlso (cancelled OrElse errorMsg <> "") Then
                    File.Delete(outputPath)
                End If
            Catch
            End Try
            Return
        End If

        If cancelled Then
            Try
                If File.Exists(outputPath) Then File.Delete(outputPath)
            Catch
            End Try
            ' 静默：用户取消是主动行为
        ElseIf errorMsg <> "" Then
            Try
                If File.Exists(outputPath) Then File.Delete(outputPath)
            Catch
            End Try
            MessageBox.Show($"备份失败：{errorMsg}", "备份",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf succeeded Then
            MessageBox.Show("用户数据已备份。", "备份",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        ' ── 恢复 UI ──
        Try
            _cts?.Dispose()
        Catch
        End Try
        _cts = Nothing

        ProgressBar2.Visible = False
        ProgressBar2.Value = 0
        Me.Text = origTitle
        btnBackup.Enabled = True
        btnCancel.Enabled = True
        btnSelectAll.Enabled = True
        btnSnapshot.Enabled = True
    End Sub

    ''' <summary>
    ''' 后台线程：收集文件 + 写入 zip + verification.txt。
    ''' 返回 True 表示成功，False 表示用户取消。真正的错误以异常抛出，由外层 Lambda 捕获。
    ''' </summary>
    Private Function DoBackupCore(zipPath As String,
                                  rootDir As String,
                                  opt As BackupOptions,
                                  progress As IProgress(Of BackupProgress),
                                  token As CancellationToken) As Boolean

        ' ══════ 1. 收集文件 ══════
        Dim entries As New List(Of Tuple(Of String, String))

        If opt.Settings Then
            Dim f = Path.Combine(rootDir, "settings_ddnet.cfg")
            If File.Exists(f) Then entries.Add(Tuple.Create("settings_ddnet.cfg", f))
        End If

        If opt.Saves Then
            Dim f = Path.Combine(rootDir, "ddnet-saves.txt")
            If File.Exists(f) Then entries.Add(Tuple.Create("ddnet-saves.txt", f))
        End If

        If opt.Cfg Then
            Try
                For Each f In Directory.GetFiles(rootDir, "*.cfg")
                    Dim fn = Path.GetFileName(f)
                    If fn.Equals("settings_ddnet.cfg", StringComparison.OrdinalIgnoreCase) Then Continue For
                    entries.Add(Tuple.Create(fn, f))
                Next
            Catch
            End Try
        End If

        Dim folderMap As New List(Of Tuple(Of Boolean, String)) From {
            Tuple.Create(opt.Demos, "demos"),
            Tuple.Create(opt.Videos, "videos"),
            Tuple.Create(opt.Assets, "assets"),
            Tuple.Create(opt.Ghosts, "ghosts")
        }
        For Each t In folderMap
            If token.IsCancellationRequested Then Return False
            If Not t.Item1 Then Continue For
            Dim absFolder = Path.Combine(rootDir, t.Item2)
            If Not Directory.Exists(absFolder) Then Continue For
            Try
                For Each f In Directory.EnumerateFiles(absFolder, "*", SearchOption.AllDirectories)
                    If token.IsCancellationRequested Then Return False
                    Dim relSub = f.Substring(absFolder.Length).TrimStart("\"c, "/"c)
                    Dim rel = t.Item2 & "\" & relSub
                    entries.Add(Tuple.Create(rel, f))
                Next
            Catch
            End Try
        Next

        If entries.Count = 0 Then
            Throw New InvalidOperationException("没有可备份的文件。")
        End If

        ' ══════ 2. 写入 zip ══════
        Dim totalSteps As Integer = entries.Count + 1

        If File.Exists(zipPath) Then File.Delete(zipPath)

        Using zip As ZipArchive = ZipFile.Open(zipPath, ZipArchiveMode.Create)
            Dim n As Integer = 0
            For Each entry In entries
                If token.IsCancellationRequested Then Return False

                Dim relPath As String = entry.Item1.Replace("\"c, "/"c)
                Dim absPath As String = entry.Item2
                Try
                    zip.CreateEntryFromFile(absPath, relPath, CompressionLevel.Optimal)
                Catch
                End Try

                n += 1
                progress.Report(New BackupProgress With {
                    .Current = n,
                    .Total = totalSteps,
                    .Message = $"拷贝: {Path.GetFileName(absPath)} ({n}/{totalSteps})"
                })
            Next

            If token.IsCancellationRequested Then Return False

            ' verification.txt
            Dim verEntry As ZipArchiveEntry = zip.CreateEntry("verification.txt")
            Using writer As New StreamWriter(verEntry.Open(), Encoding.UTF8)
                writer.WriteLine("app=DDNetForeEdit")
                writer.WriteLine($"date={DateTime.Now:yyyy/MM/dd HH:mm}")
                writer.WriteLine("mode=Backup")
                writer.WriteLine("source=Local")
                writer.WriteLine($"files={entries.Count}")
            End Using

            n += 1
            progress.Report(New BackupProgress With {
                .Current = n,
                .Total = totalSteps,
                .Message = $"写入: verification.txt ({n}/{totalSteps})"
            })
        End Using

        Return True
    End Function

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
        SetRowEnabled(chkSettings, lblSizeSettings, lblCountSettings)
        SetRowEnabled(chkCfg, lblSizeCfg, lblCountCfg)
        SetRowEnabled(chkSaves, lblSizeSaves, lblCountSaves)
        SetRowEnabled(chkDemos, lblSizeDemos, lblCountDemos)
        SetRowEnabled(chkVideos, lblSizeVideos, lblCountVideos)
        SetRowEnabled(chkAssets, lblSizeAssets, lblCountAssets)
        SetRowEnabled(chkGhosts, lblSizeGhosts, lblCountGhosts)
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