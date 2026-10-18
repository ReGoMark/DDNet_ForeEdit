Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks

''' <summary>
''' 备份对话框：选择要备份的数据范围，打包为 zip。 主来源：%AppData%\DDNet\（Local）。 附加来源：由 FormMain 传入的当前加载 data
''' 目录（Install，可选）。 勾选「也包含当前目录」时，两组数据分别放入 local/ 和 install/ 子文件夹。 打包在后台线程执行，支持关闭窗体取消。
''' </summary>
Public Class FormBackup

    ''' <summary>
    ''' DDNet 用户数据根目录（主来源，可能不存在）。
    ''' </summary>
    Private ReadOnly DdnetDir As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DDNet")

    ''' <summary>
    ''' 由 FormMain 传入的附加数据目录（当前加载的 fonts 目录的父级）。 为空表示主窗体未加载字体目录，chkExtra 会被禁用。
    ''' </summary>
    <System.ComponentModel.Browsable(False),
     System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property DataDirectory As String = ""

    ''' <summary>
    ''' 各项大小的缓存（键：category；值：字节数）。RefreshSizes 时更新。
    ''' </summary>
    Private ReadOnly _sizeCache As New Dictionary(Of String, Long)

    ''' <summary>
    ''' 当前备份操作的取消令牌源。为 Nothing 表示无操作进行中。
    ''' </summary>
    Private _cts As CancellationTokenSource = Nothing

    ' ─── 后台备份辅助类型 ────────────────────────────────────────────

    Private Class BackupOptions
        Public Settings As Boolean
        Public Saves As Boolean
        Public Cfg As Boolean
        Public Demos As Boolean
        Public Videos As Boolean
        Public Assets As Boolean
        Public Ghosts As Boolean
        Public IncludeExtra As Boolean
        Public ExtraRoot As String = ""
    End Class

    Private Class BackupProgress
        Public Current As Integer
        Public Total As Integer
        Public Message As String
    End Class

    ' ─── 窗体生命周期 ──────────────────────────────────────────────────

    Private Sub DialogBackup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateExtraCheckBoxState()
        RefreshSizes()
        UpdateRowEnabledStates()

        Dim dblProp = GetType(Control).GetProperty("DoubleBuffered",
                                                   Reflection.BindingFlags.Instance Or
                                                   Reflection.BindingFlags.NonPublic)
        If dblProp IsNot Nothing Then
            dblProp.SetValue(Me, True, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' DataDirectory 是否可用：非空、存在、且与 DdnetDir 不同。
    ''' </summary>
    Private Function IsExtraAvailable() As Boolean
        If String.IsNullOrEmpty(DataDirectory) OrElse Not Directory.Exists(DataDirectory) Then Return False
        Try
            Dim mainFull = Path.GetFullPath(DdnetDir).TrimEnd("\"c)
            Dim extraFull = Path.GetFullPath(DataDirectory).TrimEnd("\"c)
            Return Not String.Equals(mainFull, extraFull, StringComparison.OrdinalIgnoreCase)
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 当前是否要包含附加目录。
    ''' </summary>
    Private ReadOnly Property IncludeExtraNow As Boolean
        Get
            Return chkExtra.Checked AndAlso chkExtra.Enabled
        End Get
    End Property

    ''' <summary>
    ''' 主目录是否可用。
    ''' </summary>
    Private ReadOnly Property PrimaryAvailable As Boolean
        Get
            Return Directory.Exists(DdnetDir)
        End Get
    End Property

    ''' <summary>
    ''' 刷新 chkExtra 的可用性状态。
    ''' </summary>
    Private Sub UpdateExtraCheckBoxState()
        If IsExtraAvailable() Then
            chkExtra.Enabled = True
            ToolTip1.SetToolTip(chkExtra, $"从当前安装目录下扫描")
        Else
            chkExtra.Checked = False
            chkExtra.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 窗体关闭时若正在备份，询问用户是否中断。
    ''' </summary>
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

    ''' <summary>
    ''' 刷新 7 组大小 + 数量。勾选 chkExtra 时按"分区"统计： 每组显示 local + install 两项之和（若来源不同文件不计重复）。
    ''' </summary>
    Private Sub RefreshSizes()
        Dim incExtra As Boolean = IncludeExtraNow

        Dim a = GetSingleFileInfo("settings_ddnet.cfg", incExtra)
        _sizeCache("settings") = a.size
        lblSizeSettings.Text = FormatSizeOrDash(a.size, a.count)
        lblCountSettings.Text = FormatCountOrDash(a.count)

        Dim b = GetOtherCfgInfo(incExtra)
        _sizeCache("cfg") = b.size
        lblSizeCfg.Text = FormatSizeOrDash(b.size, b.count)
        lblCountCfg.Text = FormatCountOrDash(b.count)

        Dim c = GetSingleFileInfo("ddnet-saves.txt", incExtra)
        _sizeCache("saves") = c.size
        lblSizeSaves.Text = FormatSizeOrDash(c.size, c.count)
        lblCountSaves.Text = FormatCountOrDash(c.count)

        Dim d = GetFolderInfo("demos", incExtra)
        _sizeCache("demos") = d.size
        lblSizeDemos.Text = FormatSizeOrDash(d.size, d.count)
        lblCountDemos.Text = FormatCountOrDash(d.count)

        Dim e = GetFolderInfo("videos", incExtra)
        _sizeCache("videos") = e.size
        lblSizeVideos.Text = FormatSizeOrDash(e.size, e.count)
        lblCountVideos.Text = FormatCountOrDash(e.count)

        Dim f = GetFolderInfo("assets", incExtra)
        _sizeCache("assets") = f.size
        lblSizeAssets.Text = FormatSizeOrDash(f.size, f.count)
        lblCountAssets.Text = FormatCountOrDash(f.count)

        Dim g = GetFolderInfo("ghosts", incExtra)
        _sizeCache("ghosts") = g.size
        lblSizeGhosts.Text = FormatSizeOrDash(g.size, g.count)
        lblCountGhosts.Text = FormatCountOrDash(g.count)

        RefreshTotalSize()
    End Sub

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

    ''' <summary>
    ''' 单文件信息：勾选时 local + install **都计入**（不去重）。
    ''' </summary>
    Private Function GetSingleFileInfo(fileName As String, includeExtra As Boolean) As (count As Integer, size As Long)
        Dim totalSize As Long = 0
        Dim totalCount As Integer = 0

        Dim f1 = Path.Combine(DdnetDir, fileName)
        If File.Exists(f1) Then
            Try
                totalSize += New FileInfo(f1).Length
                totalCount += 1
            Catch
            End Try
        End If

        If includeExtra Then
            Dim f2 = Path.Combine(DataDirectory, fileName)
            If File.Exists(f2) Then
                Try
                    totalSize += New FileInfo(f2).Length
                    totalCount += 1
                Catch
                End Try
            End If
        End If

        Return (totalCount, totalSize)
    End Function

    ''' <summary>
    ''' 其他 *.cfg：勾选时 local + install 都计入（不去重，分别进 local/ 和 install/）。 附加目录的 autoexec_server.cfg 排除。
    ''' </summary>
    Private Function GetOtherCfgInfo(includeExtra As Boolean) As (count As Integer, size As Long)
        Dim total As Long = 0
        Dim count As Integer = 0

        ' 主目录
        Try
            If Directory.Exists(DdnetDir) Then
                For Each f In Directory.GetFiles(DdnetDir, "*.cfg")
                    Dim fn = Path.GetFileName(f)
                    If fn.Equals("settings_ddnet.cfg", StringComparison.OrdinalIgnoreCase) Then Continue For
                    Try
                        total += New FileInfo(f).Length
                        count += 1
                    Catch
                    End Try
                Next
            End If
        Catch
        End Try

        ' 附加目录
        If includeExtra Then
            Try
                If Directory.Exists(DataDirectory) Then
                    For Each f In Directory.GetFiles(DataDirectory, "*.cfg")
                        Dim fn = Path.GetFileName(f)
                        If fn.Equals("settings_ddnet.cfg", StringComparison.OrdinalIgnoreCase) Then Continue For
                        If fn.Equals("autoexec_server.cfg", StringComparison.OrdinalIgnoreCase) Then Continue For
                        Try
                            total += New FileInfo(f).Length
                            count += 1
                        Catch
                        End Try
                    Next
                End If
            Catch
            End Try
        End If

        Return (count, total)
    End Function

    ''' <summary>
    ''' 文件夹统计：勾选时 local + install 都计入（按各自分区去重，不跨分区去重）。
    ''' </summary>
    Private Function GetFolderInfo(subFolder As String, includeExtra As Boolean) As (count As Integer, size As Long)
        Dim total As Long = 0
        Dim count As Integer = 0

        ' 主目录内部按相对路径去重
        Dim seenLocal As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        CollectFolderInfo(DdnetDir, subFolder, seenLocal, total, count)

        ' 附加目录内部按相对路径去重（独立集合，不与主目录交叉）
        If includeExtra Then
            Dim seenExtra As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            CollectFolderInfo(DataDirectory, subFolder, seenExtra, total, count)
        End If

        Return (count, total)
    End Function

    Private Sub CollectFolderInfo(root As String, subFolder As String,
                                  seen As HashSet(Of String),
                                  ByRef total As Long, ByRef count As Integer)
        Try
            Dim dir = Path.Combine(root, subFolder)
            If Not Directory.Exists(dir) Then Return

            For Each f In Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories)
                Dim relSub = f.Substring(dir.Length).TrimStart("\"c, "/"c).Replace("\"c, "/"c)
                Dim key = subFolder.Replace("\"c, "/"c) & "/" & relSub
                If seen.Add(key) Then
                    Try
                        total += New FileInfo(f).Length
                        count += 1
                    Catch
                    End Try
                End If
            Next
        Catch
        End Try
    End Sub

    Private Function FormatSizeOrDash(size As Long, count As Integer) As String
        If count = 0 Then Return "-"
        Return FormatBytes(size)
    End Function

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

    ' ─── 勾选状态变化 ────────────────────────────────────────────────

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

    Private Sub chkExtra_CheckedChanged(sender As Object, e As EventArgs) Handles chkExtra.CheckedChanged
        RefreshSizes()
    End Sub

    ' ─── 全选 / 全不选 ────────────────────────────────────────────────

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

    ' ─── 备份（异步）─────────────────────────────────────────────────

    Private Async Sub btnBackupTo_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        If _cts IsNot Nothing Then Return

        If Not (chkSettings.Checked OrElse chkCfg.Checked OrElse chkSaves.Checked OrElse
                chkDemos.Checked OrElse chkVideos.Checked OrElse
                chkAssets.Checked OrElse chkGhosts.Checked) Then
            Return
        End If

        Dim hasPrimary As Boolean = PrimaryAvailable
        Dim hasExtra As Boolean = IncludeExtraNow AndAlso IsExtraAvailable()
        If Not hasPrimary AndAlso Not hasExtra Then
            MessageBox.Show($"找不到可备份的目录：{DdnetDir}" & vbCrLf &
                            "请在 FormMain 中加载字体目录后重试，或勾选「也包含当前目录」。",
                            "备份", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim outputPath As String
        Using sfd As New SaveFileDialog()
            sfd.Title = "保存备份"
            sfd.Filter = "Zip 文件 (*.zip)|*.zip|所有文件|*.*"
            sfd.FileName = "无标题.zip"
            sfd.DefaultExt = "zip"
            If sfd.ShowDialog(Me) <> DialogResult.OK Then Return
            outputPath = sfd.FileName
        End Using

        Dim opt As New BackupOptions With {
            .Settings = chkSettings.Checked,
            .Saves = chkSaves.Checked,
            .Cfg = chkCfg.Checked,
            .Demos = chkDemos.Checked,
            .Videos = chkVideos.Checked,
            .Assets = chkAssets.Checked,
            .Ghosts = chkGhosts.Checked,
            .IncludeExtra = IncludeExtraNow,
            .ExtraRoot = DataDirectory
        }
        Dim rootDir As String = DdnetDir

        _cts = New CancellationTokenSource()
        Dim token As CancellationToken = _cts.Token

        Dim origTitle As String = Me.Text
        ProgressBar2.Visible = True
        ProgressBar2.Minimum = 0
        ProgressBar2.Maximum = 1
        ProgressBar2.Value = 0
        btnBackup.Enabled = False
        btnCancel.Enabled = False
        btnSelectAll.Enabled = False
        btnSnapshot.Enabled = False

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

        Dim succeeded As Boolean = False
        Dim cancelled As Boolean = False
        Dim errorMsg As String = ""

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
            errorMsg = ex.Message
        End Try

        If IsDisposed OrElse Disposing Then
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
    ''' 后台：收集 + 写 zip + verification.txt。 IncludeExtra 时：主目录 → local/，附加目录 → install/；否则主目录放根。
    ''' </summary>
    Private Function DoBackupCore(zipPath As String,
                                  rootDir As String,
                                  opt As BackupOptions,
                                  progress As IProgress(Of BackupProgress),
                                  token As CancellationToken) As Boolean

        Dim entries As New List(Of Tuple(Of String, String))
        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        Dim localPrefix As String = If(opt.IncludeExtra, "local/", "")
        Dim installPrefix As String = "install/"

        ' ── 主来源（Local）──
        Dim localBefore = entries.Count
        If Not CollectEntries(rootDir, seen, entries, opt, token, isExtra:=False, folderPrefix:=localPrefix) Then Return False
        Dim hasLocal As Boolean = entries.Count > localBefore

        ' ── 附加来源（Install）──
        Dim hasInstall As Boolean = False
        If opt.IncludeExtra AndAlso Not String.IsNullOrEmpty(opt.ExtraRoot) Then
            Try
                Dim mainFull = Path.GetFullPath(rootDir).TrimEnd("\"c)
                Dim extraFull = Path.GetFullPath(opt.ExtraRoot).TrimEnd("\"c)
                If Not String.Equals(mainFull, extraFull, StringComparison.OrdinalIgnoreCase) Then
                    Dim installBefore = entries.Count
                    If Not CollectEntries(opt.ExtraRoot, seen, entries, opt, token, isExtra:=True, folderPrefix:=installPrefix) Then Return False
                    hasInstall = entries.Count > installBefore
                End If
            Catch
            End Try
        End If

        If entries.Count = 0 Then
            Throw New InvalidOperationException("没有可备份的文件。")
        End If

        ' ── 拼接 source 标签 ──
        Dim sourceParts As New List(Of String)
        If hasLocal Then sourceParts.Add("Local")
        If hasInstall Then sourceParts.Add("Install")
        Dim sourceStr As String = If(sourceParts.Count > 0, String.Join(", ", sourceParts), "Local")

        ' ── 写入 zip ──
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

            Dim verEntry As ZipArchiveEntry = zip.CreateEntry("verification.txt")
            Using writer As New StreamWriter(verEntry.Open(), Encoding.UTF8)
                writer.WriteLine("app=DDNetForeEdit")
                writer.WriteLine($"date={DateTime.Now:yyyy/MM/dd HH:mm}")
                writer.WriteLine($"source={sourceStr}")
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

    ''' <summary>
    ''' 从 rootDir 收集备份文件。isExtra = True 时排除 autoexec_server.cfg。 folderPrefix 为压缩包内相对路径前缀（如 "local/"
    ''' 或 "install/"）。
    ''' </summary>
    Private Function CollectEntries(rootDir As String,
                                    seen As HashSet(Of String),
                                    entries As List(Of Tuple(Of String, String)),
                                    opt As BackupOptions,
                                    token As CancellationToken,
                                    isExtra As Boolean,
                                    folderPrefix As String) As Boolean

        If String.IsNullOrEmpty(rootDir) OrElse Not Directory.Exists(rootDir) Then Return True

        If opt.Settings Then
            Dim f = Path.Combine(rootDir, "settings_ddnet.cfg")
            If File.Exists(f) Then AddEntry(seen, entries, folderPrefix & "settings_ddnet.cfg", f)
        End If

        If opt.Saves Then
            Dim f = Path.Combine(rootDir, "ddnet-saves.txt")
            If File.Exists(f) Then AddEntry(seen, entries, folderPrefix & "ddnet-saves.txt", f)
        End If

        If opt.Cfg Then
            Try
                For Each f In Directory.GetFiles(rootDir, "*.cfg")
                    Dim fn = Path.GetFileName(f)
                    If fn.Equals("settings_ddnet.cfg", StringComparison.OrdinalIgnoreCase) Then Continue For
                    If isExtra AndAlso fn.Equals("autoexec_server.cfg", StringComparison.OrdinalIgnoreCase) Then Continue For
                    AddEntry(seen, entries, folderPrefix & fn, f)
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
                    Dim rel = folderPrefix & t.Item2 & "\" & relSub
                    AddEntry(seen, entries, rel, f)
                Next
            Catch
            End Try
        Next

        Return True
    End Function

    Private Sub AddEntry(seen As HashSet(Of String),
                         entries As List(Of Tuple(Of String, String)),
                         relPath As String,
                         absPath As String)
        Dim normalized = relPath.Replace("\"c, "/"c)
        If seen.Add(normalized) Then
            entries.Add(Tuple.Create(relPath, absPath))
        End If
    End Sub

    ' ─── 快照 ────────────────────────────────────────────────────────

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

    ' ─── 行启用状态 ─────────────────────────────────────────────────

    Private Sub UpdateRowEnabledStates()
        SetRowEnabled(chkSettings, lblSizeSettings, lblCountSettings)
        SetRowEnabled(chkCfg, lblSizeCfg, lblCountCfg)
        SetRowEnabled(chkSaves, lblSizeSaves, lblCountSaves)
        SetRowEnabled(chkDemos, lblSizeDemos, lblCountDemos)
        SetRowEnabled(chkVideos, lblSizeVideos, lblCountVideos)
        SetRowEnabled(chkAssets, lblSizeAssets, lblCountAssets)
        SetRowEnabled(chkGhosts, lblSizeGhosts, lblCountGhosts)
    End Sub

    Private Sub SetRowEnabled(chk As CheckBox, sizeLbl As Label, countLbl As Label)
        Dim c As Color = If(chk.Checked, Color.FromArgb(64, 64, 64), Color.DarkGray)
        sizeLbl.ForeColor = c
        countLbl.ForeColor = c
    End Sub

End Class