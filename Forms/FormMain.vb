Imports System.Drawing.Text
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions

''' <summary>
''' DDNet ForeEdit 主窗体，负责字体目录加载、配置编辑、回退字体管理及打包导出。
''' </summary>
Public Class FormMain

    ' ─── 私有字段 ────────────────────────────────────────────────────────

    ''' <summary>
    ''' 回退字体列表，存储 FontFamily 名称。
    ''' </summary>
    Private _fallbacks As New List(Of String)

    ''' <summary>
    ''' 映射：FamilyName（小写）→ GDI+ 显示名称。
    ''' </summary>
    Private _familyToGdi As New Dictionary(Of String, String)

    ''' <summary>
    ''' 映射：GDI名（小写）→ TTC 序号标签，如 "1/3"。
    ''' </summary>
    Private _ttcTag As New Dictionary(Of String, String)

    ''' <summary>
    ''' 映射：GDI名（小写）→ TTC 物理索引（0-based）。
    ''' </summary>
    Private _ttcPhysicalIndex As New Dictionary(Of String, Integer)

    ''' <summary>
    ''' 映射：GDI名（小写）→ 配置文件使用的 FamilyName。
    ''' </summary>
    Private _gdiToFamily As New Dictionary(Of String, String)

    ''' <summary>
    ''' 映射：GDI名（小写）→ 字体文件的完整物理路径。
    ''' </summary>
    Private _gdiToFilePath As New Dictionary(Of String, String)

    ''' <summary>
    ''' 缓存：GDI名（小写）→ PrivateFontCollection 实例。
    ''' </summary>
    Private _fontCache As New Dictionary(Of String, PrivateFontCollection)

    ''' <summary>
    ''' 所有已加载的 PrivateFontCollection 实例，用于统一释放。
    ''' </summary>
    Private _fontCollections As New List(Of PrivateFontCollection)

    ''' <summary>
    ''' DPI 缩放后的标准行高（供子窗体使用）。
    ''' </summary>
    Public Shared Property SharedItemHeight As Integer = 20

    ' 窗口级别，构造一次复用
    Private _menu As New Win32ContextMenu()

    ' 原始配置值（用于“恢复”按钮）
    Private _originalDefaultFont As String = ""

    Private _originalJapaneseFont As String = ""
    Private _originalKoreanFont As String = ""
    Private _originalSimplifiedChineseFont As String = ""
    Private _originalTraditionalChineseFont As String = ""

    ''' <summary>
    ''' 当前正在操作的字体目录路径。
    ''' </summary>
    Private _currentFontDirectory As String = ""

    ''' <summary>
    ''' 安装目录路径（非用户目录）。
    ''' </summary>
    Private _installDirectory As String = ""

    ''' <summary>
    ''' 上次加载的文件扩展名（用于标题显示）。
    ''' </summary>
    Private _lastFileExtension As String = ""

    ''' <summary>
    ''' 防止 ComboBox 事件循环的锁定标志。
    ''' </summary>
    Private _isFillingComboBoxes As Boolean = False

    ''' <summary>
    ''' 预览窗体实例。
    ''' </summary>
    Private _previewWindow As FormPreview = Nothing

    ' ─── DrawItem 绘制缓存 ───────────────────────────────────────────────

    ''' <summary>
    ''' 缓存：GDI名（小写）→ 绘制用的 Font 实例。
    ''' </summary>
    Private _drawFontCache As New Dictionary(Of String, Font)

    ''' <summary>
    ''' 缓存：TTC 序号标签字符串 → 测量尺寸。
    ''' </summary>
    Private _tagSizeCache As New Dictionary(Of String, SizeF)

    ''' <summary>
    ''' 绘制 TTC 标签的普通画刷。
    ''' </summary>
    Private ReadOnly _tagBrushNormal As New SolidBrush(Color.Gray)

    ''' <summary>
    ''' 绘制 TTC 标签的选中画刷。
    ''' </summary>
    Private ReadOnly _tagBrushSelected As New SolidBrush(Color.FromArgb(200, 255, 255, 255))

    ''' <summary>
    ''' 绘制 TTC 标签的字体。
    ''' </summary>
    Private ReadOnly _tagFont As New Font("Consolas", 7.5F, FontStyle.Regular, GraphicsUnit.Point)

    ' ─── 版本信息 ──────────────────────────────────────────────────────

    ''' <summary>
    ''' 构建版本号。
    ''' </summary>
    Public ReadOnly BuildVersion As String = "Build 260827.x"

    ''' <summary>
    ''' 窗体标题前缀。
    ''' </summary>
    Private ReadOnly TitleText As String = "DDNet ForeEdit"

    ' ─── 目录结构常量 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 程序根目录（exe 所在目录）。
    ''' </summary>
    Private ReadOnly AppRoot As String = Application.StartupPath

    ''' <summary>
    ''' 预装标准字体目录（data\standard）。
    ''' </summary>
    Private ReadOnly StandardFontsDir As String = Path.Combine(AppRoot, "data", "standard")

    ''' <summary>
    ''' 内容文件目录（data\content）。
    ''' </summary>
    Private ReadOnly ContentDir As String = Path.Combine(AppRoot, "data", "content")

    ''' <summary>
    ''' 清单目录（manifest）。
    ''' </summary>
    Private ReadOnly ManifestDir As String = Path.Combine(AppRoot, "manifest")

    ''' <summary>
    ''' 待删除清单文件路径。
    ''' </summary>
    Private ReadOnly PendingDeletionsPath As String = Path.Combine(ManifestDir, "pendingdeletions.txt")

    ''' <summary>
    ''' 待复制清单文件路径。
    ''' </summary>
    Private ReadOnly PendingCopiesPath As String = Path.Combine(ManifestDir, "pendingcopies.txt")

    ''' <summary>
    ''' 临时缓存目录（cache，仅用于 FormBlade 导入）。
    ''' </summary>
    Private ReadOnly CacheDir As String = Path.Combine(AppRoot, "cache")

    ' ─── 常量定义 ────────────────────────────────────────────────────────

    ''' <summary>
    ''' 预装标准字体文件列表。
    ''' </summary>
    Private ReadOnly StandardFontFiles() As String = {
        "DejaVuSans.ttf",
        "Font_Awesome_6_Free-Solid-900.otf",
        "GlowSansJ-Compressed-Book.otf",
        "SourceHanSans.ttc"
    }

    ''' <summary>
    ''' 受保护的字体文件（禁止删除）。
    ''' </summary>
    Private ReadOnly ProtectedFontFiles() As String = {
        "Font_Awesome_6_Free-Solid-900.otf"
    }

    ' ─── 窗体生命周期 ────────────────────────────────────────────────────

    ''' <summary>
    ''' 窗体加载事件：初始化目录、UI 状态、清理挂起操作。
    ''' </summary>
    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EnsureDirectoriesExist()
        UpdateLanguageVariantControls()
        UpdateFallbackControls()
        ExecutePendingDeletions()
        ExecutePendingCopies()
        Me.Text = TitleText
        lblDirFonts.Text = "等待数据加载"
        lstbDirFonts.ItemHeight = AwareListHeight.GetScaledItemHeight(Me)
        lstbFallbackFonts.ItemHeight = AwareListHeight.GetScaledItemHeight(Me)
        SharedItemHeight = AwareListHeight.GetScaledItemHeight(Me)

        _menu.AddRange({
    New Win32ContextMenu.MenuItem() With {
        .Id = 1,
        .Text = "预览(&P)",
        .Icon = ImageList1.Images(0),
        .OnClick = Sub() FormPreview.Show(Me)
    },
    New Win32ContextMenu.MenuItem() With {
        .Id = 2,
        .Text = "Blade(&B)",
        .Icon = ImageList1.Images(1),
        .OnClick = Sub() FormBlade.ShowDialog(Me)
    },
            Win32ContextMenu.MenuItem.Separator,
              New Win32ContextMenu.MenuItem() With {
        .Id = 3,
        .Text = "管理(&M)",
        .SubItems = New List(Of Win32ContextMenu.MenuItem) From {
            New Win32ContextMenu.MenuItem() With {
                .Id = 41,
                .Text = "未使用字体(&U)",
                .OnClick = Sub() CheckFontJsonMatch()
            },
            New Win32ContextMenu.MenuItem() With {
                .Id = 42,
                .Text = "预装字体验证(&V)",
                .OnClick = Sub() VerifyStandardFonts()
            },
            New Win32ContextMenu.MenuItem() With {
                .Id = 43,
                .Text = "配置文件(&J)",
                .OnClick = Sub() OpenJsonConfig()
            },
            New Win32ContextMenu.MenuItem() With {
                .Id = 44,
                .Text = "字体目录(&D)",
                .OnClick = Sub() OpenFontDirectory()
            }
        }
    },
        New Win32ContextMenu.MenuItem() With {
        .Id = 5,
        .Text = "刷新(&R)",
        .OnClick = Sub() btnRefresh.PerformClick()
    },
    Win32ContextMenu.MenuItem.Separator,
    New Win32ContextMenu.MenuItem() With {
        .Id = 6,
        .Text = "帮助(&H)",
        .Icon = ImageList1.Images(3),
        .SubItems = New List(Of Win32ContextMenu.MenuItem) From {
            New Win32ContextMenu.MenuItem() With {
                .Id = 61,
                .Text = "视频教程(&V)",
                .OnClick = Sub()
                               Process.Start(New ProcessStartInfo With {
                                   .FileName = "https://www.bilibili.com/video/BV1h7PezCE2i/?spm_id_from=333.1387.0.0&vd_source=c4099c355c2d06f10ac210fe7bae65a6",
                                   .UseShellExecute = True
                               })
                           End Sub
            },
            New Win32ContextMenu.MenuItem() With {
                .Id = 62,
                .Text = "更新日志(&L)",
                .OnClick = Sub()
                               Dim filePath = Application.StartupPath & "\data\updates.txt"

                               Try
                                   If IO.File.Exists(filePath) Then
                                       ' 方案 1：使用 ProcessStartInfo（推荐）
                                       Dim psi As New ProcessStartInfo With {
                                       .FileName = filePath,
                                       .UseShellExecute = True
                                   }
                                       Process.Start(psi)
                                   Else
                                       MessageBox.Show("文件不存在: " & filePath, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                   End If
                               Catch ex As Exception
                                   MessageBox.Show("打开文件失败: " & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                               End Try
                           End Sub
            },
            New Win32ContextMenu.MenuItem() With {
                .Id = 63,
                .Text = "获取更新(&U)",
                .OnClick = Sub()
                               Try
                                   Dim psi As New ProcessStartInfo()
                                   psi.FileName = "https://github.com/ReGoMark/DDNet_ForeEdit"
                                   psi.UseShellExecute = True
                                   Process.Start(psi)
                               Catch ex As Exception
                                   MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
                               End Try
                           End Sub
            }
        }
    },
    New Win32ContextMenu.MenuItem() With {
        .Id = 7,
        .Text = "关于(&A)",
        .OnClick = Sub() FormAbout.ShowDialog(Me)
    }
})
    End Sub

    ''' <summary>
    ''' 确保所有必需目录存在。
    ''' </summary>
    Private Sub EnsureDirectoriesExist()
        For Each dir As String In {StandardFontsDir, ContentDir, ManifestDir, CacheDir}
            If Not Directory.Exists(dir) Then
                Try
                    Directory.CreateDirectory(dir)
                Catch ex As Exception
                    ' 忽略创建失败（非关键目录）
                End Try
            End If
        Next
    End Sub

    ''' <summary>
    ''' 窗体关闭事件：释放所有 GDI 资源和子窗体。
    ''' </summary>
    Private Sub FormMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        For Each pfc As PrivateFontCollection In _fontCollections
            pfc.Dispose()
        Next
        _fontCollections.Clear()
        _fontCache.Clear()

        If _previewWindow IsNot Nothing AndAlso Not _previewWindow.IsDisposed Then
            _previewWindow.Close()
            _previewWindow.Dispose()
        End If

        For Each f In _drawFontCache.Values
            f.Dispose()
        Next
        _tagFont.Dispose()
        _tagBrushNormal.Dispose()
        _tagBrushSelected.Dispose()
    End Sub

    ' ─── 核心加载逻辑 ────────────────────────────────────────────────────

    ''' <summary>
    ''' 全量加载字体目录：扫描字体文件、构建缓存、读取 index.json 并更新 UI。
    ''' </summary>
    ''' <param name="fdirectory">目标字体目录路径。</param>
    ''' <param name="sourceLabel">来源标签（如“快捷方式”），用于标题显示。</param>
    ''' <param name="skipLocalCheck">是否跳过用户目录切换检查（刷新时使用）。</param>
    Private Sub LoadFontDirectory(fdirectory As String,
                                 Optional sourceLabel As String = "",
                                 Optional skipLocalCheck As Boolean = False)

        Dim appData As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Dim ddnetFonts As String = Path.Combine(appData, "DDNet", "fonts")
        Dim teeworldsFonts As String = Path.Combine(appData, "Teeworlds", "fonts")
        Dim hasDDNet As Boolean = Directory.Exists(ddnetFonts)
        Dim hasTeeworlds As Boolean = Directory.Exists(teeworldsFonts)

        ' 记录安装目录（非用户目录）
        If fdirectory.ToLower() <> ddnetFonts.ToLower() AndAlso
           fdirectory.ToLower() <> teeworldsFonts.ToLower() Then
            _installDirectory = fdirectory
        End If

        UpdateLocalDirectoryIndicator(hasDDNet, hasTeeworlds)

        ' 根据用户目录复选框决定实际目录
        If chkDirSelect.Checked AndAlso Not skipLocalCheck Then
            fdirectory = ResolveUserDirectory(hasDDNet, hasTeeworlds, ddnetFonts, teeworldsFonts)
            If String.IsNullOrEmpty(fdirectory) Then Return
        Else
            If Not String.IsNullOrEmpty(_installDirectory) AndAlso Not skipLocalCheck Then
                fdirectory = _installDirectory
            End If
        End If

        _currentFontDirectory = fdirectory
        tbPath.Text = fdirectory

        ' 清空旧缓存
        For Each pfc As PrivateFontCollection In _fontCollections
            pfc.Dispose()
        Next
        _fontCollections.Clear()
        _fontCache.Clear()
        _ttcTag.Clear()
        _gdiToFilePath.Clear()
        _gdiToFamily.Clear()
        _familyToGdi.Clear()
        _ttcPhysicalIndex.Clear()
        lblFontName.Text = "-" : lblFontFamily.Text = "-" : lblFontType.Text = "-"
        lblFontCoverage.Text = "-" : lblFontModify.Text = "-" : lblFontSize.Text = "-"

        For Each f In _drawFontCache.Values
            f.Dispose()
        Next
        _drawFontCache.Clear()
        _tagSizeCache.Clear()

        Dim dirExists As Boolean = Directory.Exists(fdirectory)
        Dim jsonPath As String = Path.Combine(fdirectory, "index.json")

        ' 完整性检查
        CheckStandardFontIntegrity()
        CheckJsonIntegrity()

        lstbDirFonts.Items.Clear()
        If dirExists Then
            Dim fontFiles As New List(Of String)
            fontFiles.AddRange(Directory.GetFiles(fdirectory, "*.ttf"))
            fontFiles.AddRange(Directory.GetFiles(fdirectory, "*.ttc"))
            fontFiles.AddRange(Directory.GetFiles(fdirectory, "*.otf"))
            fontFiles.Sort()

            ' 排除已标记待删除的文件（按文件名匹配）
            If File.Exists(PendingDeletionsPath) Then
                Dim markedFullPaths = File.ReadAllLines(PendingDeletionsPath).Where(Function(line) Not String.IsNullOrWhiteSpace(line))
                Dim markedFileNames As New HashSet(Of String)(markedFullPaths.Select(Function(p) Path.GetFileName(p)), StringComparer.OrdinalIgnoreCase)
                fontFiles = fontFiles.Where(Function(f) Not markedFileNames.Contains(Path.GetFileName(f))).ToList()
            End If

            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = If(fontFiles.Count > 0, fontFiles.Count, 1)
            ProgressBar1.Value = 0
            Dim loadedCount As Integer = 0

            For Each filePath As String In fontFiles
                Dim fileName As String = Path.GetFileName(filePath)
                Dim isTtc As Boolean = Path.GetExtension(filePath).ToLower() = ".ttc"
                Dim nameInfos As List(Of FontNameInfo) = FontInfoReader.ReadAllFontNames(filePath)

                Try
                    Dim pfc As New PrivateFontCollection()
                    pfc.AddFontFile(filePath)
                    If pfc.Families.Length > 0 Then
                        _fontCollections.Add(pfc)
                        Dim total As Integer = pfc.Families.Length

                        If isTtc Then
                            ' TTC 处理：建立 GDI 名到 FamilyName 的映射
                            Dim rawToFamily As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                            For Each ni As FontNameInfo In nameInfos
                                For Each raw As String In ni.AllRawNames
                                    If Not rawToFamily.ContainsKey(raw) Then
                                        rawToFamily(raw) = ni.FamilyName
                                    End If
                                Next
                            Next

                            For idx As Integer = 0 To total - 1
                                Dim gdiName As String = pfc.Families(idx).Name
                                lstbDirFonts.Items.Add(gdiName)
                                _fontCache(gdiName.ToLower()) = pfc
                                _gdiToFilePath(gdiName.ToLower()) = filePath

                                Dim famName As String = ""
                                rawToFamily.TryGetValue(gdiName, famName)
                                If Not String.IsNullOrEmpty(famName) Then
                                    _gdiToFamily(gdiName.ToLower()) = famName
                                    If Not _familyToGdi.ContainsKey(famName.ToLower()) Then
                                        _familyToGdi(famName.ToLower()) = gdiName
                                    End If
                                End If

                                ' 查找物理索引
                                Dim physIdx As Integer = -1
                                For i As Integer = 0 To nameInfos.Count - 1
                                    If nameInfos(i).AllRawNames.Any(Function(r) String.Equals(r, gdiName, StringComparison.OrdinalIgnoreCase)) Then
                                        physIdx = i
                                        Exit For
                                    End If
                                Next
                                _ttcPhysicalIndex(gdiName.ToLower()) = physIdx
                                If total > 1 Then _ttcTag(gdiName.ToLower()) = $"{idx + 1}/{total}"
                            Next
                        Else
                            ' TTF / OTF
                            Dim sharedFamilyName As String = If(nameInfos.Count > 0, nameInfos(0).FamilyName, "")
                            Dim gdiName As String = pfc.Families(0).Name
                            If String.IsNullOrEmpty(gdiName) Then gdiName = sharedFamilyName
                            If String.IsNullOrEmpty(gdiName) Then gdiName = fileName

                            lstbDirFonts.Items.Add(gdiName)
                            _fontCache(gdiName.ToLower()) = pfc
                            _gdiToFilePath(gdiName.ToLower()) = filePath
                            If Not String.IsNullOrEmpty(sharedFamilyName) Then
                                _gdiToFamily(gdiName.ToLower()) = sharedFamilyName
                                If Not _familyToGdi.ContainsKey(sharedFamilyName.ToLower()) Then
                                    _familyToGdi(sharedFamilyName.ToLower()) = gdiName
                                End If
                            End If
                        End If
                    Else
                        lstbDirFonts.Items.Add(fileName)
                        pfc.Dispose()
                    End If
                Catch ex As Exception
                    lstbDirFonts.Items.Add(fileName)
                End Try

                loadedCount += 1
                ProgressBar1.Value = loadedCount
                Me.Text = $"{TitleText} - 读取: {fileName} ({loadedCount}/{fontFiles.Count})"
            Next
            ProgressBar1.Visible = False

            If Not String.IsNullOrEmpty(sourceLabel) Then
                Me.Text = $"{TitleText} - {sourceLabel}"
            Else
                Me.Text = TitleText
            End If
        End If

        ' 更新 UI
        UpdateFontStatisticsLabel()
        PopulateComboBoxes()
        LoadFallbackList()
        UpdateLanguageVariantControls()
        UpdateFallbackControls()

        If Not dirExists Then
            MessageBox.Show($"未找到字体目录: {fdirectory}",
                            "加载字体目录", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' ─── 辅助方法（用户目录、指示器） ───────────────────────────────

    ''' <summary>
    ''' 更新用户目录复选框的显示状态和提示。
    ''' </summary>
    Private Sub UpdateLocalDirectoryIndicator(hasDDNet As Boolean, hasTeeworlds As Boolean)
        If hasDDNet AndAlso hasTeeworlds Then
            chkDirSelect.ForeColor = Color.FromArgb(0, 120, 215)
            chkDirSelect.Text = "优先目录 ✔"
            ToolTip1.SetToolTip(chkDirSelect, "存在多用户目录, DDNet 目录优先")
        ElseIf hasDDNet Then
            chkDirSelect.ForeColor = Color.FromArgb(0, 176, 80)
            chkDirSelect.Text = "用户目录 ✔"
            ToolTip1.SetToolTip(chkDirSelect, "存在 DDNet 目录")
        ElseIf hasTeeworlds Then
            chkDirSelect.ForeColor = Color.FromArgb(255, 140, 0)
            chkDirSelect.Text = "兼容目录 ✔"
            ToolTip1.SetToolTip(chkDirSelect, "存在 Teeworlds 目录")
        Else
            chkDirSelect.ForeColor = Color.FromArgb(232, 17, 35)
            chkDirSelect.Text = "用户目录 ✖"
            ToolTip1.SetToolTip(chkDirSelect, "不存在用户目录, 使用安装目录")
        End If
    End Sub

    ''' <summary>
    ''' 解析用户目录，若存在多目录则弹窗选择。
    ''' </summary>
    ''' <param name="hasDDNet">DDNet 目录是否存在。</param>
    ''' <param name="hasTeeworlds">Teeworlds 目录是否存在。</param>
    ''' <param name="ddnetPath">DDNet 目录完整路径。</param>
    ''' <param name="teeworldsPath">Teeworlds 目录完整路径。</param>
    ''' <returns>用户选择的目录路径；若取消则返回空字符串。</returns>
    Private Function ResolveUserDirectory(hasDDNet As Boolean, hasTeeworlds As Boolean,
                                      ddnetPath As String, teeworldsPath As String) As String

        ' 1. 根据目录存在情况决定目标路径
        Dim target As String = ""

        If hasDDNet AndAlso hasTeeworlds Then
            ' 两个目录都存在 → 弹窗让用户选择
            Using dlg As New DialogDirectory()
                dlg.DDNetPath = ddnetPath
                dlg.TeeworldsPath = teeworldsPath
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    target = dlg.SelectedPath
                Else
                    ' 用户取消 → 取消用户目录复选框，返回空字符串
                    chkDirSelect.CheckState = CheckState.Unchecked
                    Return ""
                End If
            End Using
        ElseIf hasDDNet Then
            ' 仅 DDNet 存在
            target = ddnetPath
        ElseIf hasTeeworlds Then
            ' 仅 Teeworlds 存在
            target = teeworldsPath
        Else
            ' 两个都不存在 → 默认使用 DDNet 路径（后续会提示创建）
            target = ddnetPath
        End If

        ' 2. 如果目标目录不存在，询问用户是否创建
        If Not Directory.Exists(target) Then
            Dim result = MessageBox.Show($"用户目录不存在,是否立即创建?{vbCrLf}{target}",
                                     "创建用户目录", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result <> DialogResult.Yes Then
                chkDirSelect.CheckState = CheckState.Unchecked
                Return ""
            End If
            Try
                Directory.CreateDirectory(target)
                MessageBox.Show("用户目录已创建", "创建用户目录", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"用户目录创建失败: {ex.Message}", "创建用户目录", MessageBoxButtons.OK, MessageBoxIcon.Error)
                chkDirSelect.CheckState = CheckState.Unchecked
                Return ""
            End Try
        End If

        Return target
    End Function

    ' ─── 更新统计标签 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 更新状态栏显示字体总数、实际文件数、未使用数量。
    ''' </summary>
    Private Sub UpdateFontStatisticsLabel()
        Dim totalItems As Integer = lstbDirFonts.Items.Count
        Dim ttcCount As Integer = _ttcTag.Count
        Dim actualFiles As Integer = _gdiToFilePath.Values.Distinct(StringComparer.OrdinalIgnoreCase).Count()
        Dim unusedList As List(Of String) = GetUnusedFontFiles()
        Dim unusedCount As Integer = If(unusedList IsNot Nothing, unusedList.Count, 0)

        Dim baseText As String
        If ttcCount > 0 Then
            baseText = $"字体 {totalItems} 项, 实际 {actualFiles} 项"
        Else
            baseText = $"字体 {totalItems} 项"
        End If
        lblDirFonts.Text = If(unusedCount > 0, $"{baseText}, 未使用 {unusedCount} 项", baseText)
    End Sub

    ' ─── 组合框填充与同步 ─────────────────────────────────────────────

    ''' <summary>
    ''' 填充所有语言组合框并从配置加载选中项。
    ''' </summary>
    Private Sub PopulateComboBoxes()
        SyncComboBoxItems()
        LoadConfigurationFromJson()
    End Sub

    ''' <summary>
    ''' 同步所有语言组合框的项列表，恢复先前选中项。
    ''' </summary>
    Private Sub SyncComboBoxItems()
        _isFillingComboBoxes = True
        For Each cb As ComboBox In {cbLA, cbJP, cbKR, cbSC, cbTC}
            Dim previousSelection As String = If(cb.SelectedIndex >= 0, cb.SelectedItem.ToString(), "")
            cb.Items.Clear()
            If cb IsNot cbLA Then cb.Items.Add("-")
            For Each item As Object In lstbDirFonts.Items
                cb.Items.Add(item)
            Next

            Dim restored As Boolean = False
            If Not String.IsNullOrEmpty(previousSelection) Then
                For i As Integer = 0 To cb.Items.Count - 1
                    If cb.Items(i).ToString() = previousSelection Then
                        cb.SelectedIndex = i
                        restored = True
                        Exit For
                    End If
                Next
            End If
            If Not restored Then
                cb.SelectedIndex = If(cb.Items.Count > 0, 0, -1)
            End If
        Next
        _isFillingComboBoxes = False
        RefreshAllPreviewLabels()
        If _previewWindow IsNot Nothing AndAlso Not _previewWindow.IsDisposed AndAlso _previewWindow.Visible Then
            SynchronizePreviewWindow()
        End If
    End Sub

    ''' <summary>
    ''' 从 index.json 加载配置并更新 UI 控件状态。
    ''' </summary>
    Private Sub LoadConfigurationFromJson()
        Dim jsonPath As String = Path.Combine(_currentFontDirectory, "index.json")
        If Not File.Exists(jsonPath) Then
            chkLanguageVariants.Checked = False
            chkFallbackFonts.Checked = False
            Return
        End If

        Try
            _isFillingComboBoxes = True
            Dim jsonText As String = File.ReadAllText(jsonPath, Encoding.UTF8)

            ' 默认字体
            _originalDefaultFont = ParseJsonString(jsonText, "default")
            SelectComboBoxItemByFamilyName(cbLA, _originalDefaultFont)

            ' 语言变体
            Dim variantsBlock As String = ParseJsonBlock(jsonText, "language variants")
            Dim hasVariants As Boolean = Not String.IsNullOrWhiteSpace(variantsBlock) AndAlso variantsBlock.Contains(":")
            chkLanguageVariants.Checked = hasVariants

            If hasVariants Then
                _originalJapaneseFont = ParseJsonString(variantsBlock, "japanese")
                _originalKoreanFont = ParseJsonString(variantsBlock, "korean")
                _originalSimplifiedChineseFont = ParseJsonString(variantsBlock, "simplified_chinese")
                _originalTraditionalChineseFont = ParseJsonString(variantsBlock, "traditional_chinese")
                SelectComboBoxItemByFamilyName(cbJP, _originalJapaneseFont)
                SelectComboBoxItemByFamilyName(cbKR, _originalKoreanFont)
                SelectComboBoxItemByFamilyName(cbSC, _originalSimplifiedChineseFont)
                SelectComboBoxItemByFamilyName(cbTC, _originalTraditionalChineseFont)
            End If

            ' 回退列表
            Dim fallbackPattern As String = """fallbacks""\s*:\s*\[([\s\S]*?)\]"
            Dim fbMatch As Match = Regex.Match(jsonText, fallbackPattern, RegexOptions.IgnoreCase)
            Dim hasFallbacks As Boolean = fbMatch.Success AndAlso Regex.IsMatch(fbMatch.Groups(1).Value, """[^""]+""")
            chkFallbackFonts.Checked = hasFallbacks
        Catch ex As Exception
            ' 解析失败时保持默认状态
        Finally
            _isFillingComboBoxes = False
            RefreshAllPreviewLabels()
            If _previewWindow IsNot Nothing AndAlso Not _previewWindow.IsDisposed AndAlso _previewWindow.Visible Then
                SynchronizePreviewWindow()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 根据 FamilyName 在组合框中选中对应项。
    ''' </summary>
    Private Sub SelectComboBoxItemByFamilyName(cb As ComboBox, familyName As String)
        If String.IsNullOrEmpty(familyName) Then Return
        Dim gdiName As String = ""
        If Not _familyToGdi.TryGetValue(familyName.ToLower(), gdiName) Then
            gdiName = familyName
        End If
        For i As Integer = 0 To cb.Items.Count - 1
            If cb.Items(i).ToString().ToLower() = gdiName.ToLower() Then
                cb.SelectedIndex = i
                Return
            End If
        Next
    End Sub

    ' ─── 预览标签刷新 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 刷新所有语言预览标签的字体。
    ''' </summary>
    Private Sub RefreshAllPreviewLabels()
        Dim comboBoxes As ComboBox() = {cbLA, cbJP, cbKR, cbSC, cbTC}
        Dim labels As Label() = {lblLA, lblJP, lblKorean, lblSC, lblTC}
        For i As Integer = 0 To comboBoxes.Length - 1
            If comboBoxes(i).SelectedIndex >= 0 Then
                ApplyFontToLabel(labels(i), comboBoxes(i).SelectedItem.ToString())
            End If
        Next
    End Sub

    ''' <summary>
    ''' 为标签应用指定 GDI 名称的字体。
    ''' </summary>
    Private Sub ApplyFontToLabel(lbl As Label, gdiName As String)
        lbl.Font = GetFontForGdiName(gdiName, 9.0F)
    End Sub

    ''' <summary>
    ''' 根据 GDI 名称获取字体实例（优先从缓存加载）。
    ''' </summary>
    Private Function GetFontForGdiName(gdiName As String, size As Single) As Font
        Dim pfc As PrivateFontCollection = Nothing
        If _fontCache.TryGetValue(gdiName.ToLower(), pfc) AndAlso pfc.Families.Length > 0 Then
            Dim fam As FontFamily = Nothing
            For Each f As FontFamily In pfc.Families
                If f.Name.ToLower() = gdiName.ToLower() Then fam = f : Exit For
            Next
            If fam Is Nothing Then fam = pfc.Families(0)
            For Each style As FontStyle In {FontStyle.Regular, FontStyle.Bold, FontStyle.Italic}
                Try
                    Return New Font(fam, size, style, GraphicsUnit.Point)
                Catch ex As Exception
                End Try
            Next
        End If
        Return New Font(Me.Font.FontFamily, size)
    End Function

    ' ─── 字体列表自绘（显示 TTC 序号） ────────────────────────────────

    ''' <summary>
    ''' 自定义绘制字体列表项，显示 TTC 序号标签。
    ''' </summary>
    Private Sub lstbDirectoryFonts_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstbDirFonts.DrawItem
        If e.Index < 0 OrElse e.Index >= lstbDirFonts.Items.Count Then Return
        e.DrawBackground()

        Dim itemText As String = lstbDirFonts.Items(e.Index).ToString()
        Dim key As String = itemText.ToLower()

        ' 获取或创建绘制字体缓存
        If Not _drawFontCache.ContainsKey(key) Then
            _drawFontCache(key) = GetFontForGdiName(itemText, 9.0F)
        End If
        Dim drawFont As Font = _drawFontCache(key)

        Dim textColor As Color = If((e.State And DrawItemState.Selected) <> 0,
                                SystemColors.HighlightText, SystemColors.WindowText)
        Dim textRect As New Rectangle(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Width - 8, e.Bounds.Height)
        TextRenderer.DrawText(e.Graphics, itemText, drawFont, textRect, textColor,
                              TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix)

        ' 绘制 TTC 序号标签
        Dim tag As String = ""
        If _ttcTag.TryGetValue(key, tag) Then
            If Not _tagSizeCache.ContainsKey(tag) Then
                _tagSizeCache(tag) = e.Graphics.MeasureString(tag, _tagFont)
            End If
            Dim tagSize = _tagSizeCache(tag)
            Dim tagBrush = If((e.State And DrawItemState.Selected) <> 0, _tagBrushSelected, _tagBrushNormal)
            e.Graphics.DrawString(tag, _tagFont, tagBrush,
            New PointF(e.Bounds.Right - tagSize.Width - 4,
                       e.Bounds.Y + (e.Bounds.Height - tagSize.Height) / 2 + 1))
        End If

        e.DrawFocusRectangle()
    End Sub

    ' ─── 选中字体显示详细信息 ──────────────────────────────────────────

    ''' <summary>
    ''' 字体列表选中项改变时，在右侧面板显示详细信息。
    ''' </summary>
    Private Sub lstbDirectoryFonts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstbDirFonts.SelectedIndexChanged
        If lstbDirFonts.SelectedItems.Count > 1 Then
            lblDirFonts.Text = $"字体 {lstbDirFonts.Items.Count} 项, 选中 {lstbDirFonts.SelectedItems.Count} 项"
            Return
        End If
        If lstbDirFonts.SelectedItems.Count = 1 Then
            UpdateFontStatisticsLabel()
        End If
        If lstbDirFonts.SelectedItems.Count = 0 Then
            ClearPropertyPanel()
            Return
        End If

        Dim gdiName As String = lstbDirFonts.SelectedItem.ToString()
        Dim filePath As String = ""
        If Not _gdiToFilePath.TryGetValue(gdiName.ToLower(), filePath) Then
            ClearPropertyPanel()
            Return
        End If

        Dim fileInfo As New FileInfo(filePath)
        lblFontName.Text = fileInfo.Name
        Dim fileSize As Long = fileInfo.Length
        lblFontSize.Text = If(fileSize >= 1024 * 1024, $"{fileSize / 1024 / 1024:F2} MB",
                         If(fileSize >= 1024, $"{fileSize / 1024:F1} KB", $"{fileSize} B"))

        Dim ttcIndex As Integer = 0
        _ttcPhysicalIndex.TryGetValue(gdiName.ToLower(), ttcIndex)
        Dim nameInfos As List(Of FontNameInfo) = FontInfoReader.ReadAllFontNames(filePath)
        Dim info As FontNameInfo = If(nameInfos.Count > ttcIndex, nameInfos(ttcIndex),
                                      If(nameInfos.Count > 0, nameInfos(0), Nothing))

        Dim tag As String = ""
        Dim baseType As String = If(_ttcTag.TryGetValue(gdiName.ToLower(), tag),
                                    "TTC",
                                    fileInfo.Extension.TrimStart(".").ToUpper())
        If info IsNot Nothing Then
            Dim weightPart As String = If(info.WeightClass > 0, info.WeightName, "")
            Dim widthPart As String = If(info.WidthClass > 0 AndAlso info.WidthClass <> 5, info.WidthName, "")
            Dim extras As String = String.Join(", ", {weightPart, widthPart}.Where(Function(s) s <> ""))
            lblFontType.Text = If(String.IsNullOrEmpty(extras), baseType, $"{baseType}, {extras}")
        Else
            lblFontType.Text = baseType
        End If

        lblFontModify.Text = fileInfo.LastWriteTime.ToString("yyyy/MM/dd, HH:mm")
        Dim familyName As String = ""
        lblFontFamily.Text = If(_gdiToFamily.TryGetValue(gdiName.ToLower(), familyName), familyName, "-")

        ' 语言覆盖信息
        Dim ext As String = Path.GetExtension(filePath).ToLower()
        Dim coverage As String = ""
        If ext = ".ttc" Then
            Dim idx As Integer = 0
            _ttcPhysicalIndex.TryGetValue(gdiName.ToLower(), idx)
            coverage = FontInfoReader.ReadLanguageCoverage(filePath, idx)
        Else
            coverage = FontInfoReader.ReadLanguageCoverage(filePath)
        End If
        lblFontCoverage.Text = coverage
    End Sub

    ''' <summary>
    ''' 清空右侧属性面板显示。
    ''' </summary>
    Private Sub ClearPropertyPanel()
        lblFontName.Text = "-" : lblFontSize.Text = "-"
        lblFontType.Text = "-" : lblFontModify.Text = "-"
        lblFontFamily.Text = "-" : lblFontCoverage.Text = "-"
    End Sub

    ' ─── ComboBox 选择变更事件 ─────────────────────────────────────────

    ''' <summary>
    ''' 语言组合框选择变更时更新预览标签，并同步预览窗体。
    ''' </summary>
    Private Sub ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLA.SelectedIndexChanged,
        cbJP.SelectedIndexChanged, cbKR.SelectedIndexChanged, cbSC.SelectedIndexChanged, cbTC.SelectedIndexChanged

        If _isFillingComboBoxes Then Return

        Dim cb = CType(sender, ComboBox)
        Dim labelMap As New Dictionary(Of ComboBox, Label) From {
            {cbLA, lblLA}, {cbJP, lblJP}, {cbKR, lblKorean}, {cbSC, lblSC}, {cbTC, lblTC}
        }
        If cb.SelectedIndex >= 0 AndAlso labelMap.ContainsKey(cb) Then
            ApplyFontToLabel(labelMap(cb), cb.SelectedItem.ToString())
        End If

        If _previewWindow IsNot Nothing AndAlso _previewWindow.Visible Then
            Dim langKey = cb.Name.Replace("cb", "").ToUpper()
            Dim fontName = If(cb.SelectedIndex >= 0, cb.SelectedItem.ToString(), "")
            _previewWindow.UpdatePreview(langKey, fontName, AddressOf ApplyFontToLabel)
        End If
    End Sub

    ' ─── 回退字体相关 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 从 index.json 加载回退字体列表。
    ''' </summary>
    Private Sub LoadFallbackList()
        lstbFallbackFonts.Items.Clear()
        _fallbacks.Clear()

        Dim jsonPath As String = Path.Combine(_currentFontDirectory, "index.json")
        If Not File.Exists(jsonPath) Then Return

        Try
            Dim jsonText As String = File.ReadAllText(jsonPath, Encoding.UTF8)
            Dim pattern As String = """fallbacks""\s*:\s*\[\s*(.*?)\s*\]"
            Dim m As Match = Regex.Match(jsonText, pattern, RegexOptions.Singleline)
            If Not m.Success Then Return

            Dim arrBlock As String = m.Groups(1).Value
            Dim matches As MatchCollection = Regex.Matches(arrBlock, """([^""]+)""")
            For Each mm As Match In matches
                _fallbacks.Add(mm.Groups(1).Value)
            Next
        Catch ex As Exception
            MessageBox.Show($"读取回退字体配置时出错: {ex.Message}",
                            "读取回退字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        PopulateFallbackComboBox()
        RefreshFallbackListBox()
    End Sub

    ''' <summary>
    ''' 填充回退字体下拉框。
    ''' </summary>
    Private Sub PopulateFallbackComboBox()
        cbFallbackFonts.Items.Clear()
        For Each item As Object In lstbDirFonts.Items
            cbFallbackFonts.Items.Add(item)
        Next
        If cbFallbackFonts.Items.Count > 0 Then cbFallbackFonts.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' 刷新回退字体列表显示。
    ''' </summary>
    Private Sub RefreshFallbackListBox()
        lstbFallbackFonts.Items.Clear()
        For Each familyName As String In _fallbacks
            Dim displayName As String = familyName
            Dim gdiName As String = ""
            If _familyToGdi.TryGetValue(familyName.ToLower(), gdiName) Then
                displayName = gdiName
            End If
            lstbFallbackFonts.Items.Add(displayName)
        Next
        lstbFallbackFonts.Refresh()
    End Sub

    ''' <summary>
    ''' 自定义绘制回退列表项，显示序号。
    ''' </summary>
    Private Sub lstbFallbackFonts_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstbFallbackFonts.DrawItem
        If e.Index < 0 OrElse e.Index >= lstbFallbackFonts.Items.Count Then Return
        e.DrawBackground()

        Dim itemText As String = lstbFallbackFonts.Items(e.Index).ToString()
        Dim textColor As Color = If((e.State And DrawItemState.Selected) <> 0,
                                    SystemColors.HighlightText, SystemColors.WindowText)

        Dim textRect As New Rectangle(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Width - 8, e.Bounds.Height)
        TextRenderer.DrawText(e.Graphics, itemText, e.Font, textRect, textColor,
                              TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix)

        ' 显示序号
        Dim tag As String = $"{e.Index + 1}"
        Using tagFont As New Font("Consolas", 7.5F, FontStyle.Regular, GraphicsUnit.Point)
            Using tagBrush As New SolidBrush(
                    If((e.State And DrawItemState.Selected) <> 0,
                       Color.FromArgb(200, 255, 255, 255), Color.Gray))
                Dim tagSize As SizeF = e.Graphics.MeasureString(tag, tagFont)
                e.Graphics.DrawString(tag, tagFont, tagBrush,
                    New PointF(e.Bounds.Right - tagSize.Width - 4,
                               e.Bounds.Y + (e.Bounds.Height - tagSize.Height) / 2 + 1))
            End Using
        End Using
        e.DrawFocusRectangle()
    End Sub

    ' ─── 回退操作（插入、移除、移动） ─────────────────────────────────

    ''' <summary>
    ''' 在回退列表中插入当前选中的字体。
    ''' </summary>
    Private Sub InsertFallbackItem(sender As Object, e As EventArgs) Handles btnInsert.Click
        If cbFallbackFonts.SelectedIndex < 0 Then
            'MessageBox.Show("请选择要插入的字体.", "插入回退字体", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim gdiName As String = cbFallbackFonts.SelectedItem.ToString()
        Dim familyName As String = ""
        If Not _gdiToFamily.TryGetValue(gdiName.ToLower(), familyName) Then
            familyName = gdiName
        End If

        Dim insertIndex As Integer
        If lstbFallbackFonts.SelectedIndex >= 0 Then
            insertIndex = lstbFallbackFonts.SelectedIndex + 1
        Else
            insertIndex = _fallbacks.Count
        End If

        If _fallbacks.Contains(familyName) Then
            MessageBox.Show($"字体已存在，跳过插入", "插入回退字体", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        _fallbacks.Insert(insertIndex, familyName)
        RefreshFallbackListBox()
        lstbFallbackFonts.SelectedIndex = insertIndex
    End Sub

    ''' <summary>
    ''' 从回退列表中移除当前选中的字体。
    ''' </summary>
    Private Sub RemoveFallbackItem(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim idx As Integer = lstbFallbackFonts.SelectedIndex
        If idx < 0 Then
            'MessageBox.Show("请先选择要移除的字体。", "移除回退字体", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        _fallbacks.RemoveAt(idx)
        RefreshFallbackListBox()
        If _fallbacks.Count > 0 Then
            lstbFallbackFonts.SelectedIndex = Math.Min(idx, _fallbacks.Count - 1)
        End If
    End Sub

    ''' <summary>
    ''' 重置回退列表为配置文件中的原始列表。
    ''' </summary>
    Private Sub RestoreFallbackList(sender As Object, e As EventArgs) Handles btnRecoveryFallbackFonts.Click
        LoadFallbackList()
    End Sub

    ''' <summary>
    ''' 将回退列表中的选中项向上移动一位。
    ''' </summary>
    Private Sub MoveFallbackUp(sender As Object, e As EventArgs) Handles btnUp.Click
        Dim idx As Integer = lstbFallbackFonts.SelectedIndex
        If idx > 0 Then
            Dim temp As String = _fallbacks(idx)
            _fallbacks(idx) = _fallbacks(idx - 1)
            _fallbacks(idx - 1) = temp
            RefreshFallbackListBox()
            lstbFallbackFonts.SelectedIndex = idx - 1
        End If
    End Sub

    ''' <summary>
    ''' 将回退列表中的选中项向下移动一位。
    ''' </summary>
    Private Sub MoveFallbackDown(sender As Object, e As EventArgs) Handles btnDown.Click
        Dim idx As Integer = lstbFallbackFonts.SelectedIndex
        If idx >= 0 AndAlso idx < _fallbacks.Count - 1 Then
            Dim temp As String = _fallbacks(idx)
            _fallbacks(idx) = _fallbacks(idx + 1)
            _fallbacks(idx + 1) = temp
            RefreshFallbackListBox()
            lstbFallbackFonts.SelectedIndex = idx + 1
        End If
    End Sub

    ' ─── 语言变体开关 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 语言变体编辑开关改变时更新相关控件状态。
    ''' </summary>
    Private Sub chkLanguageVariants_CheckedChanged(sender As Object, e As EventArgs) Handles chkLanguageVariants.CheckedChanged
        UpdateLanguageVariantControls()
    End Sub

    ''' <summary>
    ''' 启用或禁用语言变体相关的控件。
    ''' </summary>
    Private Sub UpdateLanguageVariantControls()
        Dim enabled As Boolean = chkLanguageVariants.Checked
        cbJP.Enabled = enabled : cbKR.Enabled = enabled
        cbSC.Enabled = enabled : cbTC.Enabled = enabled
        btnFollowJP.Enabled = enabled : btnFollowKR.Enabled = enabled
        btnFollowSC.Enabled = enabled : btnFollowTC.Enabled = enabled
        btnRecoveryJP.Enabled = enabled : btnRecoveryKR.Enabled = enabled
        btnRecoverySC.Enabled = enabled : btnRecoveryTC.Enabled = enabled
        lblJP.Enabled = enabled : lblKorean.Enabled = enabled
        lblSC.Enabled = enabled : lblTC.Enabled = enabled
    End Sub

    ' ─── 回退开关 ──────────────────────────────────────────────────────

    ''' <summary>
    ''' 回退字体编辑开关改变时更新相关控件状态。
    ''' </summary>
    Private Sub chkFallbackFonts_CheckedChanged(sender As Object, e As EventArgs) Handles chkFallbackFonts.CheckedChanged
        UpdateFallbackControls()
    End Sub

    ''' <summary>
    ''' 启用或禁用回退字体相关的控件。
    ''' </summary>
    Private Sub UpdateFallbackControls()
        Dim enabled As Boolean = chkFallbackFonts.Checked
        lstbFallbackFonts.Enabled = enabled
        cbFallbackFonts.Enabled = enabled
        btnInsert.Enabled = enabled
        btnRemove.Enabled = enabled
        btnRecoveryFallbackFonts.Enabled = enabled
        btnUp.Enabled = enabled
        btnDown.Enabled = enabled
    End Sub

    ' ─── 跟随英文 / 恢复按钮 ──────────────────────────────────────────

    ''' <summary>
    ''' 将指定语言变体设置为与默认西文相同的字体。
    ''' </summary>
    Private Sub FollowDefaultLanguage(sender As Object, e As EventArgs) Handles btnFollowJP.Click,
        btnFollowKR.Click, btnFollowSC.Click, btnFollowTC.Click

        Dim btn = CType(sender, Button)
        Dim targetComboBox As ComboBox = Nothing
        Select Case btn.Name
            Case "btnFollowJP" : targetComboBox = cbJP
            Case "btnFollowKR" : targetComboBox = cbKR
            Case "btnFollowSC" : targetComboBox = cbSC
            Case "btnFollowTC" : targetComboBox = cbTC
        End Select
        If targetComboBox IsNot Nothing AndAlso cbLA.SelectedIndex >= 0 Then
            SelectComboBoxItemByFamilyName(targetComboBox, GetSelectedFamilyName(cbLA))
        End If
    End Sub

    ''' <summary>
    ''' 将指定语言恢复为配置中的原始值。
    ''' </summary>
    Private Sub RestoreOriginalLanguage(sender As Object, e As EventArgs) Handles btnRecoveryLA.Click,
        btnRecoveryJP.Click, btnRecoveryKR.Click, btnRecoverySC.Click, btnRecoveryTC.Click

        Dim btn = CType(sender, Button)
        Select Case btn.Name
            Case "btnRecoveryLA" : If Not String.IsNullOrEmpty(_originalDefaultFont) Then SelectComboBoxItemByFamilyName(cbLA, _originalDefaultFont)
            Case "btnRecoveryJP" : If Not String.IsNullOrEmpty(_originalJapaneseFont) Then SelectComboBoxItemByFamilyName(cbJP, _originalJapaneseFont)
            Case "btnRecoveryKR" : If Not String.IsNullOrEmpty(_originalKoreanFont) Then SelectComboBoxItemByFamilyName(cbKR, _originalKoreanFont)
            Case "btnRecoverySC" : If Not String.IsNullOrEmpty(_originalSimplifiedChineseFont) Then SelectComboBoxItemByFamilyName(cbSC, _originalSimplifiedChineseFont)
            Case "btnRecoveryTC" : If Not String.IsNullOrEmpty(_originalTraditionalChineseFont) Then SelectComboBoxItemByFamilyName(cbTC, _originalTraditionalChineseFont)
        End Select
    End Sub

    ''' <summary>
    ''' 获取组合框当前选中的 FamilyName。
    ''' </summary>
    Private Function GetSelectedFamilyName(cb As ComboBox) As String
        If cb.SelectedIndex < 0 OrElse cb.SelectedItem Is Nothing Then Return ""
        If cb IsNot cbLA AndAlso cb.SelectedIndex = 0 Then Return ""
        Dim gdiName As String = cb.SelectedItem.ToString()
        Dim familyName As String = ""
        Return If(_gdiToFamily.TryGetValue(gdiName.ToLower(), familyName), familyName, gdiName)
    End Function

    ' ─── 构建 index.json ───────────────────────────────────────────────

    ''' <summary>
    ''' 根据当前 UI 状态生成完整的 index.json 内容。
    ''' </summary>
    Private Function BuildIndexJsonContent() As String
        Dim usedFamilyNames As New HashSet(Of String)
        Dim familyToGdiMap As New Dictionary(Of String, String)

        ' 从组合框收集
        For Each cb As ComboBox In {cbLA, cbJP, cbKR, cbSC, cbTC}
            Dim famName As String = GetSelectedFamilyName(cb)
            If Not String.IsNullOrEmpty(famName) Then
                usedFamilyNames.Add(famName)
                If Not familyToGdiMap.ContainsKey(famName) Then
                    familyToGdiMap(famName) = cb.SelectedItem.ToString()
                End If
            End If
        Next

        ' 从回退列表收集
        For Each famName As String In _fallbacks
            usedFamilyNames.Add(famName)
            If Not familyToGdiMap.ContainsKey(famName) Then
                For Each kvp In _gdiToFamily
                    If kvp.Value = famName Then
                        familyToGdiMap(famName) = kvp.Key
                        Exit For
                    End If
                Next
            End If
        Next

        ' 收集对应的文件名
        Dim fileSet As New HashSet(Of String)
        Dim fontFileNames As New List(Of String)
        For Each famName As String In usedFamilyNames
            If familyToGdiMap.ContainsKey(famName) Then
                Dim gdiName As String = familyToGdiMap(famName)
                Dim filePath As String = ""
                If _gdiToFilePath.TryGetValue(gdiName.ToLower(), filePath) Then
                    Dim fileName As String = Path.GetFileName(filePath)
                    If Not fileSet.Contains(fileName) Then
                        fontFileNames.Add(fileName)
                        fileSet.Add(fileName)
                    End If
                End If
            End If
        Next

        ' 强制包含 Font Awesome
        If Not fileSet.Contains("Font_Awesome_6_Free-Solid-900.otf") Then
            If Directory.Exists(_currentFontDirectory) Then
                Dim faFiles = Directory.GetFiles(_currentFontDirectory, "*Font*Awesome*.otf", SearchOption.TopDirectoryOnly)
                If faFiles.Length = 0 Then
                    faFiles = Directory.GetFiles(_currentFontDirectory, "*Font*Awesome*.ttf", SearchOption.TopDirectoryOnly)
                End If
                If faFiles.Length > 0 Then
                    Dim faFile = Path.GetFileName(faFiles(0))
                    If Not fileSet.Contains(faFile) Then
                        fontFileNames.Add(faFile)
                        fileSet.Add(faFile)
                    End If
                End If
            End If
        End If

        ' 构造 JSON
        Dim sb As New StringBuilder()
        sb.AppendLine("{")
        sb.AppendLine("    ""font files"": [")
        For i = 0 To fontFileNames.Count - 1
            sb.Append("        """).Append(fontFileNames(i)).Append("""")
            If i < fontFileNames.Count - 1 Then sb.AppendLine(",") Else sb.AppendLine()
        Next
        sb.AppendLine("    ],")
        sb.AppendLine($"    ""default"": ""{GetSelectedFamilyName(cbLA)}"",")
        sb.AppendLine("    ""language variants"": {")
        If chkLanguageVariants.Checked Then
            Dim lines As New List(Of String)
            Dim langMap As New Dictionary(Of String, ComboBox) From {
                {"japanese", cbJP}, {"korean", cbKR},
                {"simplified_chinese", cbSC}, {"traditional_chinese", cbTC}
            }
            For Each kvp In langMap
                Dim famName As String = GetSelectedFamilyName(kvp.Value)
                If Not String.IsNullOrEmpty(famName) Then
                    lines.Add($"        ""{kvp.Key}"": ""{famName}""")
                End If
            Next
            For i = 0 To lines.Count - 1
                sb.Append(lines(i))
                If i < lines.Count - 1 Then sb.AppendLine(",") Else sb.AppendLine()
            Next
        End If
        sb.AppendLine("    },")
        sb.AppendLine("    ""fallbacks"": [")
        For i = 0 To _fallbacks.Count - 1
            sb.Append("        """).Append(_fallbacks(i)).Append("""")
            If i < _fallbacks.Count - 1 Then sb.AppendLine(",") Else sb.AppendLine()
        Next
        sb.AppendLine("    ],")
        sb.AppendLine("    ""icon"": ""Font Awesome 6 Free""")
        sb.AppendLine("}")
        Return sb.ToString()
    End Function

    ' ─── 写入配置 ──────────────────────────────────────────────────────

    ''' <summary>
    ''' 将当前配置写入 index.json。
    ''' </summary>
    Private Sub ApplyConfiguration(sender As Object, e As EventArgs) Handles btnApply.Click
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("数据尚未加载", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim jsonPath = Path.Combine(_currentFontDirectory, "index.json")
        If MessageBox.Show("确认要写入配置吗？", "写入配置",
                           MessageBoxButtons.OKCancel, MessageBoxIcon.Question) <> DialogResult.OK Then Return

        Try
            Dim content = BuildIndexJsonContent()
            File.WriteAllText(jsonPath, content, Encoding.UTF8)
            ' 更新恢复基准值
            _originalDefaultFont = GetSelectedFamilyName(cbLA)
            _originalJapaneseFont = GetSelectedFamilyName(cbJP)
            _originalKoreanFont = GetSelectedFamilyName(cbKR)
            _originalSimplifiedChineseFont = GetSelectedFamilyName(cbSC)
            _originalTraditionalChineseFont = GetSelectedFamilyName(cbTC)
            MessageBox.Show("配置写入成功", "写入配置", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"配置写入失败：{vbCrLf}{ex.Message}", "写入配置", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─── 导出配置 ──────────────────────────────────────────────────────

    ''' <summary>
    ''' 将当前配置另存为独立的 index.json 文件。
    ''' </summary>
    Private Sub ExportConfiguration(sender As Object, e As EventArgs) Handles btnCopy.Click
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("数据尚未加载", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using sfd As New SaveFileDialog
            sfd.Title = "另存为「index.json」"
            sfd.FileName = "index.json"
            sfd.Filter = "JSON|index.json|所有文件|*.*"
            If sfd.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                Dim content = BuildIndexJsonContent()
                If File.Exists(sfd.FileName) Then File.Delete(sfd.FileName)
                File.WriteAllText(sfd.FileName, content, Encoding.UTF8)
                MessageBox.Show("配置导出成功", "导出配置", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"配置导出失败：{vbCrLf}{ex.Message}", "导出配置", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' ─── 恢复默认 ──────────────────────────────────────────────────────

    ''' <summary>
    ''' 恢复默认配置和字体文件。
    ''' </summary>
    Private Sub RestoreDefaultFonts(sender As Object, e As EventArgs) Handles btnDefault.Click
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("数据尚未加载", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If chkDirSelect.Checked Then
            Dim result = MessageBox.Show("默认情况下用户目录内不存在字体文件夹。若要恢复默认状态，请按以下步骤操作：" & vbCrLf &
                                     "· 单击「确定」在资源管理器中打开该文件夹" & vbCrLf &
                                     "· 关闭本程序，注意不是关闭资源管理器" & vbCrLf &
                                     "· 清空配置和所有字体，或者删除整个 fonts 文件夹" & vbCrLf &
                                     "——————" & vbCrLf &
                                     "* 该操作会删除字体文件夹内所有数据，请注意备份数据" & vbCrLf &
                                     "* 第三方客户端如 TClient 可能会使用 Teeworlds 目录",
                                     "还原默认字体",
                                     MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            If result = DialogResult.OK Then
                Process.Start("explorer.exe", $"""{_currentFontDirectory}""")
            End If
            Return
        End If

        Dim sourceJson = Path.Combine(StandardFontsDir, "index.json")
        Dim destJson = Path.Combine(_currentFontDirectory, "index.json")

        ' 检查标准字体目录是否存在
        If Not File.Exists(sourceJson) OrElse Not Directory.Exists(StandardFontsDir) Then
            MessageBox.Show("预装配置或字体缺失，无法还原", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim stdFiles As New List(Of String)
        stdFiles.AddRange(Directory.GetFiles(StandardFontsDir, "*.ttf"))
        stdFiles.AddRange(Directory.GetFiles(StandardFontsDir, "*.ttc"))
        stdFiles.AddRange(Directory.GetFiles(StandardFontsDir, "*.otf"))
        If stdFiles.Count = 0 Then
            MessageBox.Show("预装字体不存在", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' ─── 使用 DialogDefault 替代 MessageBox ──────────────────────────
        Using dlg As New DialogRestore()
            If dlg.ShowDialog(Me) <> DialogResult.OK Then
                Return
            End If

            If dlg.SelectedMode = DialogRestore.RestoreMode.FontAndConfig Then
                ' 恢复预装配置和字体
                Try
                    File.Copy(sourceJson, destJson, True)

                    ' 标记当前所有字体为待删除
                    Dim currentFonts As New List(Of String)
                    currentFonts.AddRange(Directory.GetFiles(_currentFontDirectory, "*.ttf"))
                    currentFonts.AddRange(Directory.GetFiles(_currentFontDirectory, "*.ttc"))
                    currentFonts.AddRange(Directory.GetFiles(_currentFontDirectory, "*.otf"))
                    If currentFonts.Count > 0 Then
                        Dim existing As New List(Of String)
                        If File.Exists(PendingDeletionsPath) Then
                            existing.AddRange(File.ReadAllLines(PendingDeletionsPath))
                        End If
                        existing.AddRange(currentFonts)
                        File.WriteAllLines(PendingDeletionsPath, existing.Distinct)
                    End If

                    ' 直接写入待复制清单，源路径为 Standard 目录，不再缓存
                    Dim copyLines As New List(Of String)
                    For Each src In stdFiles
                        Dim fileName = Path.GetFileName(src)
                        Dim destPath = Path.Combine(_currentFontDirectory, fileName)
                        copyLines.Add($"{src}|{destPath}")
                    Next
                    File.WriteAllLines(PendingCopiesPath, copyLines)

                    MessageBox.Show("默认字体还原成功：" & vbCrLf &
                                "* 预装配置已还原" & vbCrLf &
                                "* 预装字体还原已挂起，将在下次启动时执行",
                                "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LoadFontDirectory(If(String.IsNullOrEmpty(_installDirectory), _currentFontDirectory, _installDirectory))
                Catch ex As Exception
                    MessageBox.Show($"还原时发生错误：{vbCrLf}{ex.Message}", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else ' ConfigOnly
                ' 仅恢复预装配置
                Try
                    File.Copy(sourceJson, destJson, True)
                    LoadConfigurationFromJson()
                    LoadFallbackList()
                    MessageBox.Show("预装配置已还原", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show($"还原配置时发生错误：{vbCrLf}{ex.Message}", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    ' ─── 打开文件夹 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 在资源管理器中打开当前字体目录。
    ''' </summary>
    Private Sub btnLocate_Click(sender As Object, e As EventArgs) Handles btnLocate.Click
        OpenFontDirectory()
    End Sub
    Private Sub OpenFontDirectory()
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("数据尚未加载", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Process.Start("explorer.exe", $"""{_currentFontDirectory}""")
    End Sub

    ''' <summary>
    ''' 刷新当前字体目录列表。
    ''' </summary>
    Private Sub RefreshCurrentDirectory(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If Not String.IsNullOrEmpty(_currentFontDirectory) Then
            If chkDirSelect.Checked Then
                LoadFontDirectory(_currentFontDirectory, "", skipLocalCheck:=True)
            Else
                LoadFontDirectory(_currentFontDirectory, GetSourceLabel(_lastFileExtension), skipLocalCheck:=True)
            End If
        Else
            MessageBox.Show("数据尚未加载", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' ─── 用户目录切换 ─────────────────────────────────────────────────

    ''' <summary>
    ''' 用户目录复选框状态改变时重新加载目录。
    ''' </summary>
    Private Sub chkLocal_CheckedChanged(sender As Object, e As EventArgs) Handles chkDirSelect.CheckedChanged
        If Not String.IsNullOrEmpty(_installDirectory) Then
            LoadFontDirectory(_installDirectory)
            If chkDirSelect.Checked Then
                LoadFontDirectory(_currentFontDirectory, "", skipLocalCheck:=True)
            Else
                LoadFontDirectory(_currentFontDirectory, GetSourceLabel(_lastFileExtension), skipLocalCheck:=True)
            End If
        End If
    End Sub

    ' ─── 浏览/拖放加载 ────────────────────────────────────────────────

    ''' <summary>
    ''' 拖放进入事件：允许拖放文件。
    ''' </summary>
    Private Sub btnBrowse_DragEnter(sender As Object, e As DragEventArgs) Handles btnBrowse.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ''' <summary>
    ''' 拖放释放事件：解析拖放的文件并加载字体目录。
    ''' </summary>
    Private Sub btnBrowse_DragDrop(sender As Object, e As DragEventArgs) Handles btnBrowse.DragDrop
        Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
        If files Is Nothing OrElse files.Length = 0 Then Return
        Try
            Dim resolvedDir As String = ResolveFileToFontDirectory(files(0))
            If Not String.IsNullOrEmpty(resolvedDir) Then
                Dim ext As String = Path.GetExtension(files(0)).TrimStart(".").ToUpper()
                LoadFontDirectory(resolvedDir, GetSourceLabel(ext))
                _lastFileExtension = ext
            End If
        Catch ex As Exception
            MessageBox.Show($"加载时发生错误：{vbCrLf}{ex.Message}", "加载错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 浏览按钮点击：打开文件选择对话框加载字体目录。
    ''' </summary>
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Using dlg As New OpenFileDialog()
            dlg.Title = "选择「DDNet 快捷方式、DDNet.exe 或 index.json」"
            dlg.Filter = "通用筛选|*.lnk;*.url;*.exe;*.json|快捷方式 (*.lnk;*.url)|*.lnk;*.url|可执行程序 (*.exe)|*.exe|JSON 文档 (*.json)|*.json"
            dlg.CheckFileExists = True
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return

            Try
                Dim resolvedDir As String = ResolveFileToFontDirectory(dlg.FileName)
                If Not String.IsNullOrEmpty(resolvedDir) Then
                    Dim ext As String = Path.GetExtension(dlg.FileName).TrimStart("."c).ToUpper()
                    LoadFontDirectory(resolvedDir, GetSourceLabel(ext))
                    _lastFileExtension = ext
                End If
            Catch ex As Exception
                MessageBox.Show($"加载时发生错误：{vbCrLf}{ex.Message}", "加载错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' ─── 路径解析辅助 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 根据选定文件解析出字体目录路径。
    ''' </summary>
    Private Function ResolveFileToFontDirectory(selectedFile As String) As String
        Dim ext = Path.GetExtension(selectedFile).ToLower()
        Select Case ext
            Case ".url"
                Dim installPath = SteamLinkResolver.GetInstallDirectory(selectedFile)
                If String.IsNullOrEmpty(installPath) Then
                    MessageBox.Show("无法解析 .URL 快捷方式，请确认快捷方式有效", "解析错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return ""
                End If
                Return Path.Combine(installPath, "DDNet", "data", "fonts")

            Case ".lnk"
                Dim target = ResolveLnkTarget(selectedFile)
                If String.IsNullOrEmpty(target) Then
                    MessageBox.Show("无法解析 .lnk 快捷方式，请确认快捷方式有效", "解析错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return ""
                End If
                Return BuildFontDirectoryFromExe(target)

            Case ".exe"
                Return BuildFontDirectoryFromExe(selectedFile)

            Case ".json"
                Dim dir = Path.GetDirectoryName(selectedFile)
                If Not String.IsNullOrEmpty(dir) AndAlso Directory.Exists(dir) Then
                    Return dir
                Else
                    MessageBox.Show("无法定位 index.json 目录，请确认目录存在", "路径错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return ""
                End If

            Case Else
                MessageBox.Show("请选择有效的文件类型：.url, .lnk, .exe, .json", "文件类型错误", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return ""
        End Select
    End Function

    ''' <summary>
    ''' 根据可执行文件路径构建字体目录。
    ''' </summary>
    Private Function BuildFontDirectoryFromExe(exePath As String) As String
        Dim dir = If(File.Exists(exePath), Path.GetDirectoryName(exePath), exePath)
        Return Path.Combine(dir, "data", "fonts")
    End Function

    ''' <summary>
    ''' 解析 .lnk 快捷方式的目标路径。
    ''' </summary>
    Private Function ResolveLnkTarget(lnkPath As String) As String
        Try
            Dim shell As Object = CreateObject("WScript.Shell")
            Dim shortcut As Object = shell.CreateShortcut(lnkPath)
            Return CStr(shortcut.TargetPath)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' 根据扩展名获取来源标签。
    ''' </summary>
    Private Function GetSourceLabel(ext As String) As String
        Select Case ext.TrimStart(".").ToUpper()
            Case "LNK" : Return "快捷方式"
            Case "URL" : Return "超链接"
            Case "JSON" : Return "配置"
            Case "EXE" : Return "应用程序"
            Case Else : Return ext.TrimStart(".").ToUpper()
        End Select
    End Function

    ' ─── 添加字体 ──────────────────────────────────────────────────────

    ''' <summary>
    ''' 拖放添加字体的进入事件。
    ''' </summary>
    Private Sub btnAdd_DragEnter(sender As Object, e As DragEventArgs) Handles btnFontInstall.DragEnter
        e.Effect = If(e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    ''' <summary>
    ''' 拖放添加字体的释放事件。
    ''' </summary>
    Private Sub btnAdd_DragDrop(sender As Object, e As DragEventArgs) Handles btnFontInstall.DragDrop
        Dim files = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
        If files IsNot Nothing AndAlso files.Length > 0 Then ImportFontFiles(files)
    End Sub

    ''' <summary>
    ''' 点击添加按钮，弹出文件选择对话框导入字体。
    ''' </summary>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnFontInstall.Click
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("数据尚未加载", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using dlg As New OpenFileDialog()
            dlg.Title = "选择「.ttf、.otf 或 .ttc 字体」"
            dlg.Filter = "字体文件 (*.ttf;*.otf;*.ttc)|*.ttf;*.otf;*.ttc|所有文件|*.*"
            dlg.Multiselect = True
            dlg.CheckFileExists = True
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
            ImportFontFiles(dlg.FileNames)
        End Using
    End Sub

    ''' <summary>
    ''' 实际执行字体文件导入，跳过重复项。
    ''' </summary>
    Private Sub ImportFontFiles(sourcePaths As String())
        If Not Directory.Exists(_currentFontDirectory) Then
            MessageBox.Show("字体目录不存在，无法导入字体", "导入字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim allowedExts = {".ttf", ".ttc", ".otf"}
        Dim toAdd As New List(Of String)
        Dim duplicates As New List(Of String)

        For Each src As String In sourcePaths
            If Not allowedExts.Contains(Path.GetExtension(src).ToLower()) Then Continue For
            Dim dest = Path.Combine(_currentFontDirectory, Path.GetFileName(src))
            If File.Exists(dest) Then
                duplicates.Add(Path.GetFileName(src))
            Else
                toAdd.Add(src)
            End If
        Next

        ' 没有可导入的有效字体
        If toAdd.Count = 0 AndAlso duplicates.Count = 0 Then
            MessageBox.Show("没有可导入的字体，仅支持 .ttf、.otf、.ttc 字体。", "导入字体", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 全部重复 → 用 DialogPopup 显示所有跳过文件
        If toAdd.Count = 0 Then
            Using dlg As New DialogPopUp()
                dlg.Text = "导入字体"
                dlg.TitleText = $"所选 {duplicates.Count} 个字体已存在, 跳过导入"
                dlg.DescriptionText = ""
                dlg.btnOK.Visible = False
                dlg.CancelText = "确定"   ' 隐藏取消按钮

                For Each dup In duplicates
                    dlg.Items.Add($"[跳过] {dup}")
                Next
                dlg.ShowDialog(Me)
            End Using
            Return
        End If

        ' 执行复制
        Try
            For Each src As String In toAdd
                File.Copy(src, Path.Combine(_currentFontDirectory, Path.GetFileName(src)), False)
            Next

            ' ─── 展示混合结果（成功 + 跳过）用 DialogPopup ──────────────
            Using dlg As New DialogPopUp()
                dlg.Text = "导入字体"
                dlg.TitleText = $"成功导入 {toAdd.Count} 个字体，跳过 {duplicates.Count} 个已存在的字体"
                dlg.DescriptionText = ""
                dlg.btnOK.Visible = False
                dlg.CancelText = "确定"   ' 隐藏取消按钮

                ' 新增的文件不加前缀（只显示文件名）
                For Each f As String In toAdd
                    dlg.Items.Add($"✔ {Path.GetFileName(f)}")
                Next
                ' 跳过的文件添加 [跳过] 前缀
                For Each dup In duplicates
                    dlg.Items.Add($"✖ [跳过] {dup}")
                Next

                dlg.ShowDialog(Me)
            End Using

            ' 增量扫描新字体
            IncrementallyScanNewFonts(toAdd)
        Catch ex As Exception
            MessageBox.Show($"导入字体时出错：{ex.Message}", "导入字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 增量扫描新导入的字体，更新缓存和 UI。
    ''' </summary>
    Private Sub IncrementallyScanNewFonts(newFilePaths As IEnumerable(Of String))
        For Each filePath As String In newFilePaths
            Dim fileName = Path.GetFileName(filePath)
            Dim isTtc = Path.GetExtension(filePath).ToLower() = ".ttc"
            Dim nameInfos = FontInfoReader.ReadAllFontNames(filePath)

            Try
                Dim pfc As New PrivateFontCollection()
                pfc.AddFontFile(filePath)
                If pfc.Families.Length = 0 Then
                    pfc.Dispose()
                    lstbDirFonts.Items.Add(fileName)
                    Continue For
                End If
                _fontCollections.Add(pfc)
                Dim total = pfc.Families.Length

                If isTtc Then
                    Dim rawToFamily As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                    For Each ni As FontNameInfo In nameInfos
                        For Each raw As String In ni.AllRawNames
                            If Not rawToFamily.ContainsKey(raw) Then rawToFamily(raw) = ni.FamilyName
                        Next
                    Next

                    For idx = 0 To total - 1
                        Dim gdi = pfc.Families(idx).Name
                        lstbDirFonts.Items.Add(gdi)
                        _fontCache(gdi.ToLower()) = pfc
                        _gdiToFilePath(gdi.ToLower()) = filePath

                        Dim fam = ""
                        If rawToFamily.TryGetValue(gdi, fam) Then
                            _gdiToFamily(gdi.ToLower()) = fam
                            If Not _familyToGdi.ContainsKey(fam.ToLower()) Then
                                _familyToGdi(fam.ToLower()) = gdi
                            End If
                        End If
                        If total > 1 Then _ttcTag(gdi.ToLower()) = $"{idx + 1}/{total}"
                    Next
                Else
                    Dim ni0 = If(nameInfos.Count > 0, nameInfos(0), Nothing)
                    Dim gdi = pfc.Families(0).Name
                    If String.IsNullOrEmpty(gdi) Then gdi = If(ni0 IsNot Nothing, ni0.FamilyName, "")
                    If String.IsNullOrEmpty(gdi) Then gdi = fileName

                    lstbDirFonts.Items.Add(gdi)
                    _fontCache(gdi.ToLower()) = pfc
                    _gdiToFilePath(gdi.ToLower()) = filePath
                    If ni0 IsNot Nothing AndAlso Not String.IsNullOrEmpty(ni0.FamilyName) Then
                        _gdiToFamily(gdi.ToLower()) = ni0.FamilyName
                        If Not _familyToGdi.ContainsKey(ni0.FamilyName.ToLower()) Then
                            _familyToGdi(ni0.FamilyName.ToLower()) = gdi
                        End If
                    End If
                End If
            Catch ex As Exception
                lstbDirFonts.Items.Add(fileName)
            End Try
        Next

        SyncComboBoxItems()
        PopulateFallbackComboBox()
        UpdateFontStatisticsLabel()
    End Sub

    ' ─── 删除字体（挂起） ─────────────────────────────────────────────

    ''' <summary>
    ''' 删除选中的字体（标记为待删除，下次启动执行）。
    ''' </summary>
    Private Sub DeleteSelectedFonts(sender As Object, e As EventArgs) Handles btnFontUninstall.Click
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("数据尚未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim filePathSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim blockedNames As New List(Of String)

        For Each idx As Integer In lstbDirFonts.SelectedIndices
            Dim itemText = lstbDirFonts.Items(idx).ToString()
            Dim fp As String = ""
            If Not _gdiToFilePath.TryGetValue(itemText.ToLower(), fp) Then Continue For
            Dim fn = Path.GetFileName(fp)
            If ProtectedFontFiles.Any(Function(f) f.Equals(fn, StringComparison.OrdinalIgnoreCase)) Then
                blockedNames.Add(fn)
                Continue For
            End If
            filePathSet.Add(fp)
        Next

        If blockedNames.Count > 0 Then
            MessageBox.Show($"该字体负责基础显示，拒绝删除。", "字体写保护", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        If filePathSet.Count = 0 Then Return

        ' 收集 TTC 影响的子字体名称
        Dim affectedNames As New List(Of String)
        For Each fp As String In filePathSet
            If Not Path.GetExtension(fp).Equals(".ttc", StringComparison.OrdinalIgnoreCase) Then Continue For
            For Each kvp In _gdiToFilePath
                If kvp.Value.ToLower() = fp.ToLower() Then
                    Dim affected = ""
                    affectedNames.Add(If(_gdiToFamily.TryGetValue(kvp.Key, affected), affected, kvp.Key))
                End If
            Next
        Next
        affectedNames = affectedNames.Distinct().ToList()

        Using dlg As New DialogPopUp()
            dlg.Text = "确认删除"
            dlg.TitleText = $"确认要删除以下 {filePathSet.Count} 个字体?"
            dlg.ConfirmText = "删除"
            dlg.CancelText = "取消"

            ' ─── 分离预装字体和非预装字体 ──────────────────────────────
            Dim presetFiles As New List(Of String)
            Dim normalFiles As New List(Of String)

            For Each fp As String In filePathSet
                Dim fn = Path.GetFileName(fp)
                If StandardFontFiles.Any(Function(f) f.Equals(fn, StringComparison.OrdinalIgnoreCase)) Then
                    presetFiles.Add(fp)
                Else
                    normalFiles.Add(fp)
                End If
            Next

            ' 先添加预装字体（标记为 [预装]）
            For Each fp As String In presetFiles
                dlg.Items.Add($"[预装] {Path.GetFileName(fp)}")
            Next

            ' 再添加非预装字体（不添加前缀）
            For Each fp As String In normalFiles
                dlg.Items.Add(Path.GetFileName(fp))
            Next

            ' 如果有 TTC 影响的子字体，在说明中显示
            If affectedNames.Count > 0 Then
                Dim ttcNames = String.Join("、", affectedNames)
                'dlg.DescriptionText = $"来自 TTC 的子字体也会一并删除：{ttcNames}"
                dlg.DescriptionText = $"来自 TTC 的子字体也会一并删除{Environment.NewLine}字体文件将在下次启动时删除"
            Else
                dlg.DescriptionText = "字体文件将在下次启动时删除"
            End If

            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
        End Using

        Try
            Directory.CreateDirectory(Path.GetDirectoryName(PendingDeletionsPath))
            File.AppendAllLines(PendingDeletionsPath, filePathSet)

            ' 从内存中移除
            Dim toRemoveGdi As New List(Of String)
            For Each fp As String In filePathSet
                For Each kvp As KeyValuePair(Of String, String) In _gdiToFilePath
                    If kvp.Value.ToLower() = fp.ToLower() Then
                        toRemoveGdi.Add(kvp.Key)
                    End If
                Next
            Next

            For i = lstbDirFonts.Items.Count - 1 To 0 Step -1
                Dim gdi = lstbDirFonts.Items(i).ToString().ToLower()
                If toRemoveGdi.Any(Function(g) g = gdi) Then
                    lstbDirFonts.Items.RemoveAt(i)
                End If
            Next

            Dim deletedFamilyNames As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            For Each gdi As String In toRemoveGdi
                Dim fam = ""
                If _gdiToFamily.TryGetValue(gdi, fam) Then deletedFamilyNames.Add(fam)
                deletedFamilyNames.Add(gdi)
            Next

            For Each gdi As String In toRemoveGdi
                _gdiToFilePath.Remove(gdi)
                _gdiToFamily.Remove(gdi)
                _fontCache.Remove(gdi)
                _ttcTag.Remove(gdi)
                _ttcPhysicalIndex.Remove(gdi)
            Next

            Dim keysToRemove = _familyToGdi.Where(
                Function(kvp) toRemoveGdi.Contains(kvp.Value.ToLower())).Select(
                Function(kvp) kvp.Key).ToList()
            For Each k In keysToRemove
                _familyToGdi.Remove(k)
            Next

            _fallbacks.RemoveAll(Function(fn) deletedFamilyNames.Contains(fn))
            RefreshFallbackListBox()
            SyncComboBoxItems()
            PopulateFallbackComboBox()
            UpdateFontStatisticsLabel()

            'MessageBox.Show("字体删除已挂起，将在下次启动时执行。", "删除字体", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"删除字体失败：{ex.Message}", "删除字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─── 字体属性对话框 ──────────────────────────────────────────────

    ''' <summary>
    ''' 用于 ShellExecuteEx 的结构体。
    ''' </summary>
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Public Structure ShellExecuteInfo
        Public cbSize As Integer
        Public fMask As UInteger
        Public hwnd As IntPtr
        <MarshalAs(UnmanagedType.LPTStr)> Public lpVerb As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpFile As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpParameters As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpDirectory As String
        Public nShow As Integer
        Public hInstApp As IntPtr
        Public lpIDList As IntPtr
        <MarshalAs(UnmanagedType.LPTStr)> Public lpClass As String
        Public hkeyClass As IntPtr
        Public dwHotKey As UInteger
        Public hIconOrMonitor As IntPtr
        Public hProcess As IntPtr
    End Structure

    <DllImport("shell32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function ShellExecuteEx(ByRef lpExecInfo As ShellExecuteInfo) As Boolean
    End Function

    Private Const SW_SHOW As Integer = 5
    Private Const SEE_MASK_INVOKEIDLIST As UInteger = &HC

    ''' <summary>
    ''' 显示选中字体的属性对话框。
    ''' </summary>
    Private Sub ShowFontProperties(sender As Object, e As EventArgs) Handles btnFontInfo.Click
        If String.IsNullOrEmpty(_currentFontDirectory) OrElse lstbDirFonts.SelectedItems.Count <> 1 Then Return
        Dim gdiName = lstbDirFonts.SelectedItem.ToString()
        Dim filePath = ""
        If _gdiToFilePath.TryGetValue(gdiName.ToLower(), filePath) AndAlso File.Exists(filePath) Then
            Dim sei As New ShellExecuteInfo()
            sei.cbSize = Marshal.SizeOf(sei)
            sei.lpVerb = "properties"
            sei.lpFile = filePath
            sei.nShow = SW_SHOW
            sei.fMask = SEE_MASK_INVOKEIDLIST
            ShellExecuteEx(sei)
        End If
    End Sub

    ' ─── 系统/用户字体文件夹链接 ─────────────────────────────────────

    ''' <summary>
    ''' 打开系统字体文件夹。
    ''' </summary>
    Private Sub OpenSystemFonts(sender As Object, e As EventArgs) Handles lblSystemFonts.Click
        Process.Start("explorer.exe", "C:\WINDOWS\FONTS")
    End Sub

    ''' <summary>
    ''' 打开用户字体文件夹。
    ''' </summary>
    Private Sub OpenUserFonts(sender As Object, e As EventArgs) Handles lblLocalFonts.Click
        Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Microsoft\Windows\Fonts")
    End Sub

    ' ─── 双击字体列表打开文件 ─────────────────────────────────────────

    ''' <summary>
    ''' 双击字体列表项，在资源管理器中打开该字体文件。
    ''' </summary>
    Private Sub lstbDirectoryFonts_DoubleClick(sender As Object, e As EventArgs) Handles lstbDirFonts.DoubleClick
        If lstbDirFonts.SelectedIndex < 0 Then Return
        Dim gdiName = lstbDirFonts.SelectedItem.ToString()
        Dim filePath = ""
        If _gdiToFilePath.TryGetValue(gdiName.ToLower(), filePath) Then
            Try
                Process.Start(New ProcessStartInfo(filePath) With {.UseShellExecute = True})
            Catch ex As Exception
                MessageBox.Show($"无法打开字体：{ex.Message}", "打开字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' ─── 复制家族名和文件名 ──────────────────────────────────────────

    ''' <summary>
    ''' 双击族名标签，复制族名到剪贴板。
    ''' </summary>
    Private Sub CopyFamilyName(sender As Object, e As EventArgs) Handles lblFontFamily.DoubleClick
        If lblFontFamily.Text <> "-" AndAlso Not String.IsNullOrEmpty(lblFontFamily.Text) Then
            Clipboard.SetText(lblFontFamily.Text)
        End If
    End Sub

    ''' <summary>
    ''' 双击文件名标签，复制字体名称到剪贴板。
    ''' </summary>
    Private Sub CopyFontName(sender As Object, e As EventArgs) Handles lblFontName.DoubleClick
        If lstbDirFonts.SelectedIndex >= 0 Then
            Dim itemText = lstbDirFonts.SelectedItem.ToString()
            If Not String.IsNullOrEmpty(itemText) Then Clipboard.SetText(itemText)
        End If
    End Sub

    ' ─── 预览窗口 ──────────────────────────────────────────────────────

    ''' <summary>
    ''' 打开或激活预览窗体。
    ''' </summary>
    Private Sub ShowPreviewWindow(sender As Object, e As EventArgs) Handles btnPreview.Click
        If _previewWindow Is Nothing OrElse _previewWindow.IsDisposed Then
            _previewWindow = New FormPreview
            _previewWindow.StartPosition = FormStartPosition.CenterParent
        End If
        SynchronizePreviewWindow()
        If _previewWindow.Visible = False Then
            _previewWindow.Show(Me)
            _previewWindow.BringToFront()
        Else
            _previewWindow.BringToFront()
        End If
    End Sub

    ''' <summary>
    ''' 同步预览窗体的字体配置。
    ''' </summary>
    Private Sub SynchronizePreviewWindow()
        If _previewWindow Is Nothing OrElse _previewWindow.IsDisposed Then Return
        Dim configs As New Dictionary(Of String, ComboBox) From {
            {"LA", cbLA}, {"JP", cbJP}, {"KR", cbKR}, {"SC", cbSC}, {"TC", cbTC}
        }
        For Each kvp In configs
            Dim fontName = If(kvp.Value.SelectedIndex >= 0, kvp.Value.SelectedItem.ToString(), "")
            _previewWindow.UpdatePreview(kvp.Key, fontName, AddressOf ApplyFontToLabel)
        Next
    End Sub

    ' ─── 预装验证 ──────────────────────────────────────────────────────

    ''' <summary>
    ''' 点击预装指示灯，验证预装标准字体是否完整。
    ''' </summary>
    Private Sub lblPreset_Click(sender As Object, e As EventArgs) Handles lblPreset.Click
        VerifyStandardFonts()
    End Sub

    Private Sub VerifyStandardFonts()
        If String.IsNullOrEmpty(_currentFontDirectory) Then Return

        Dim required As New Dictionary(Of String, String) From {
        {"DejaVuSans.ttf", "DejaVu Sans"},
        {"Font_Awesome_6_Free-Solid-900.otf", "Font Awesome 6 Free"},
        {"GlowSansJ-Compressed-Book.otf", "Glow Sans J"},
        {"SourceHanSans.ttc", "Source Han Sans"}
    }

        Dim present As New List(Of String)
        Dim missing As New List(Of String)
        Dim mismatch As New List(Of String)

        For Each kvp In required
            Dim fileName = kvp.Key
            Dim expectedFamily = kvp.Value
            Dim filePath = Path.Combine(_currentFontDirectory, fileName)

            If Not File.Exists(filePath) Then
                missing.Add($"✖ {fileName}")
                Continue For
            End If

            Dim actualFamily = ""
            Try
                Dim names = FontInfoReader.ReadAllFontNames(filePath)
                If names.Count > 0 Then actualFamily = names(0).FamilyName
            Catch ex As Exception
                mismatch.Add($"✖ {fileName}")
                Continue For
            End Try

            If String.Equals(actualFamily, expectedFamily, StringComparison.OrdinalIgnoreCase) Then
                present.Add($"✔ {fileName}")
            Else
                mismatch.Add($"✖ {fileName}")
            End If
        Next

        Using dlg As New DialogPopUp()
            dlg.Text = "预装字体验证"
            dlg.btnOK.Visible = False

            If missing.Count = 0 AndAlso mismatch.Count = 0 Then
                dlg.TitleText = "所有预装字体完整"
                dlg.DescriptionText = "所有字体存在且家族名称匹配"
                dlg.CancelText = "确定"
            Else
                dlg.TitleText = $"存在 {missing.Count + mismatch.Count} 个问题"
                dlg.DescriptionText = "部分预装字体缺失或家族名称不匹配"
                dlg.CancelText = "确定"
            End If

            For Each item In present
                dlg.Items.Add(item)
            Next
            For Each item In mismatch
                dlg.Items.Add(item)
            Next
            For Each item In missing
                dlg.Items.Add(item)
            Next

            dlg.ShowDialog(Me)
        End Using
    End Sub

    ' ─── 配置指示灯点击打开 index.json ──────────────────────────────

    ''' <summary>
    ''' 点击配置指示灯，在关联程序中打开 index.json。
    ''' </summary>
    Private Sub lblConfig_Click(sender As Object, e As EventArgs) Handles lblConfig.Click
        OpenJsonConfig()
    End Sub

    Private Sub OpenJsonConfig()
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("数据尚未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ' 只有配置有效时才打开（非错误状态）
        If lblConfig.ForeColor = Color.FromArgb(232, 17, 35) AndAlso lblConfig.Text = "配置 ✖" Then
            Return
        End If
        Process.Start("explorer.exe", $"""{_currentFontDirectory}""\index.json")
    End Sub

    ' ─── 检查未使用字体 ──────────────────────────────────────────────

    ''' <summary>
    ''' 点击状态标签，检查并列出未使用的字体文件。
    ''' </summary>
    Private Sub CheckUnusedFonts(sender As Object, e As EventArgs) Handles lblDirFonts.Click
        CheckFontJsonMatch()
    End Sub

    ''' <summary>
    ''' 检查字体使用情况，并显示冗余列表。
    ''' </summary>
    Private Sub CheckFontJsonMatch()
        If String.IsNullOrEmpty(_currentFontDirectory) OrElse Not Directory.Exists(_currentFontDirectory) Then Return

        Dim jsonPath = Path.Combine(_currentFontDirectory, "index.json")
        If Not File.Exists(jsonPath) Then
            MessageBox.Show("无法检查字体使用情况，配置或字体不存在。", "检查未使用字体", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim unused = GetUnusedFontFiles()
        If unused Is Nothing OrElse unused.Count = 0 Then
            MessageBox.Show("所有字体均被引用。", "检查未使用字体", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' ─── 计算未使用字体文件的总大小 ──────────────────────────────────
        Dim totalSize As Long = 0
        For Each fileName As String In unused
            Dim filePath = Path.Combine(_currentFontDirectory, fileName)
            If File.Exists(filePath) Then
                Try
                    Dim fi As New FileInfo(filePath)
                    totalSize += fi.Length
                Catch
                End Try
            End If
        Next

        Dim sizeText As String
        If totalSize >= 1024L * 1024 * 1024 Then
            sizeText = (totalSize / 1024 / 1024 / 1024).ToString("F2") & " GB"
        ElseIf totalSize >= 1024L * 1024 Then
            sizeText = (totalSize / 1024 / 1024).ToString("F2") & " MB"
        ElseIf totalSize >= 1024 Then
            sizeText = (totalSize / 1024).ToString("F0") & " KB"
        Else
            sizeText = totalSize.ToString() & " B"
        End If

        Using dlg As New DialogPopUp()
            dlg.Text = "未使用字体"
            dlg.TitleText = $"以下 {unused.Count} 个字体未使用:"
            dlg.DescriptionText = $"未使用字体大小: {sizeText}"
            dlg.CancelText = "确定"
            dlg.btnOK.Visible = False
            For Each fn As String In unused
                dlg.Items.Add(fn)
            Next

            dlg.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' 获取未在配置中引用的字体文件列表。
    ''' </summary>
    Private Function GetUnusedFontFiles() As List(Of String)
        If String.IsNullOrEmpty(_currentFontDirectory) OrElse Not Directory.Exists(_currentFontDirectory) Then Return Nothing

        Dim jsonPath = Path.Combine(_currentFontDirectory, "index.json")
        If Not File.Exists(jsonPath) Then Return Nothing

        Dim allFiles As New List(Of String)
        allFiles.AddRange(Directory.GetFiles(_currentFontDirectory, "*.ttf"))
        allFiles.AddRange(Directory.GetFiles(_currentFontDirectory, "*.ttc"))
        allFiles.AddRange(Directory.GetFiles(_currentFontDirectory, "*.otf"))
        If allFiles.Count = 0 Then Return New List(Of String)

        ' 排除已标记待删除的文件
        If File.Exists(PendingDeletionsPath) Then
            Dim markedSet As New HashSet(Of String)(
                File.ReadAllLines(PendingDeletionsPath).Where(Function(l) Not String.IsNullOrWhiteSpace(l)),
                StringComparer.OrdinalIgnoreCase)
            allFiles = allFiles.Where(Function(f) Not markedSet.Contains(f)).ToList()
        End If

        Dim usedFiles As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Try
            Dim jsonText = File.ReadAllText(jsonPath, Encoding.UTF8)
            Dim m = Regex.Match(jsonText, """font files""\s*:\s*\[\s*(.*?)\s*\]", RegexOptions.Singleline)
            If m.Success Then
                For Each mm As Match In Regex.Matches(m.Groups(1).Value, """([^""]+)""")
                    usedFiles.Add(mm.Groups(1).Value)
                Next
            End If
        Catch ex As Exception
            ' 忽略解析错误
        End Try

        Dim unused As New List(Of String)
        For Each f As String In allFiles
            Dim fileName = Path.GetFileName(f)
            If Not usedFiles.Contains(fileName) Then unused.Add(fileName)
        Next
        Return unused
    End Function

    ' ─── 配置和预装完整性检查 ─────────────────────────────────────────

    ''' <summary>
    ''' 检查 index.json 的完整性和引用有效性。
    ''' </summary>
    Private Sub CheckJsonIntegrity()
        Dim jsonPath = Path.Combine(_currentFontDirectory, "index.json")
        If Not File.Exists(jsonPath) Then
            lblConfig.ForeColor = Color.FromArgb(232, 17, 35)
            lblConfig.Text = "配置 ✖"
            ToolTip1.SetToolTip(lblConfig, "状态：配置缺失")
            Return
        End If

        Try
            Dim jsonText = File.ReadAllText(jsonPath, Encoding.UTF8)
            Dim pattern = """font files""\s*:\s*\[\s*(.*?)\s*\]"
            Dim m = Regex.Match(jsonText, pattern, RegexOptions.Singleline)
            If Not m.Success Then
                lblConfig.ForeColor = Color.FromArgb(255, 140, 0)
                lblConfig.Text = "配置 ✖"
                ToolTip1.SetToolTip(lblConfig, "状态：配置格式错误")
                Return
            End If

            Dim missing As New List(Of String)
            For Each mm As Match In Regex.Matches(m.Groups(1).Value, """([^""]+)""")
                Dim fileName = mm.Groups(1).Value
                If Not File.Exists(Path.Combine(_currentFontDirectory, fileName)) Then
                    missing.Add(fileName)
                End If
            Next

            If missing.Count = 0 Then
                lblConfig.ForeColor = Color.FromArgb(0, 176, 80)
                lblConfig.Text = "配置 ✔"
                ToolTip1.SetToolTip(lblConfig, "状态：配置就绪")
            Else
                lblConfig.ForeColor = Color.FromArgb(255, 140, 0)
                lblConfig.Text = "配置 ✖"
                ToolTip1.SetToolTip(lblConfig, "状态：配置字体引用缺失")
            End If
        Catch ex As Exception
            lblConfig.ForeColor = Color.FromArgb(232, 17, 35)
            lblConfig.Text = "配置 ✖"
        End Try
    End Sub

    ''' <summary>
    ''' 检查预装标准字体是否完整。
    ''' </summary>
    Private Sub CheckStandardFontIntegrity()
        Dim missingCount = StandardFontFiles.Count(
            Function(f) Not File.Exists(Path.Combine(_currentFontDirectory, f)))
        If missingCount = 0 Then
            lblPreset.ForeColor = Color.FromArgb(0, 176, 80)
            lblPreset.Text = "预装 ✔"
            ToolTip1.SetToolTip(lblPreset, "状态：预装字体就绪")
        Else
            lblPreset.ForeColor = Color.FromArgb(232, 17, 35)
            lblPreset.Text = "预装 ✖"
            ToolTip1.SetToolTip(lblPreset, "状态：预装字体缺失")
        End If
    End Sub

    ' ─── BLADE（字体包管理） ──────────────────────────────────────────

    ''' <summary>
    ''' 打开 BLADE 字体包管理器。
    ''' </summary>
    Private Sub OpenBladeTool(sender As Object, e As EventArgs) Handles btnBlade.Click
        Dim appVersion = If(String.IsNullOrEmpty(Application.ProductVersion), BuildVersion, Application.ProductVersion)
        Dim normalFiles As New List(Of String)
        Dim standardFiles As New List(Of String)
        Dim fullFiles As New List(Of String)
        Dim jsonContent = ""
        Dim dirType = "未知"

        Dim hasValidDir = Not String.IsNullOrEmpty(_currentFontDirectory) AndAlso Directory.Exists(_currentFontDirectory)
        If hasValidDir Then
            Dim appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            If _currentFontDirectory.ToLower.Contains(Path.Combine(appData, "ddnet", "fonts").ToLower) Then
                dirType = "Local"
            ElseIf _currentFontDirectory.ToLower.Contains(Path.Combine(appData, "teeworlds", "fonts").ToLower) Then
                dirType = "Compatible"
            Else
                dirType = "Install"
            End If

            Dim used = GetUsedFontFiles()
            If used IsNot Nothing Then normalFiles.AddRange(used)

            standardFiles.AddRange(normalFiles)
            Dim stdFontsDir = Path.Combine(Application.StartupPath, "Data", "Standard")
            If Directory.Exists(stdFontsDir) Then
                Dim stdExts = {"*.ttf", "*.ttc", "*.otf"}
                Dim stdFileNames = New HashSet(Of String)(standardFiles.Select(Function(f) Path.GetFileName(f)), StringComparer.OrdinalIgnoreCase)
                For Each ext In stdExts
                    For Each f In Directory.GetFiles(stdFontsDir, ext)
                        If Not stdFileNames.Contains(Path.GetFileName(f)) Then
                            standardFiles.Add(f)
                            stdFileNames.Add(Path.GetFileName(f))
                        End If
                    Next
                Next
            End If

            Dim allExts = {"*.ttf", "*.ttc", "*.otf"}
            For Each ext In allExts
                fullFiles.AddRange(Directory.GetFiles(_currentFontDirectory, ext))
            Next

            Dim jsonPath = Path.Combine(_currentFontDirectory, "index.json")
            If File.Exists(jsonPath) Then
                jsonContent = BuildIndexJsonContent()
            End If
        End If

        Try
            Dim shareForm As New FormBlade
            shareForm.FontDir = _currentFontDirectory
            shareForm.NormFontFiles = normalFiles
            shareForm.StdFontFiles = standardFiles
            shareForm.FullFontFiles = fullFiles
            shareForm.JsonContent = jsonContent
            shareForm.AppVersion = appVersion
            shareForm.DirectoryType = dirType
            shareForm.ShowDialog(Me)
        Catch ex As Exception
            MessageBox.Show($"启动 BLADE 时出错：{ex.Message}", "启动 BLADE", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 获取当前配置中使用的字体文件列表。
    ''' </summary>
    Private Function GetUsedFontFiles() As List(Of String)
        Dim fontFiles As New List(Of String)
        Dim fileSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        Dim addByGdi As Action(Of String) = Sub(gdi As String)
                                                Dim fp As String = ""
                                                If _gdiToFilePath.TryGetValue(gdi.ToLower(), fp) AndAlso File.Exists(fp) AndAlso Not fileSet.Contains(Path.GetFileName(fp)) Then
                                                    fontFiles.Add(fp)
                                                    fileSet.Add(Path.GetFileName(fp))
                                                End If
                                            End Sub

        For Each cb As ComboBox In {cbLA, cbJP, cbKR, cbSC, cbTC}
            If cb.SelectedIndex >= 0 Then addByGdi(cb.SelectedItem.ToString())
        Next

        For Each familyName As String In _fallbacks
            Dim gdi As String = ""
            If _familyToGdi.TryGetValue(familyName.ToLower(), gdi) Then
                addByGdi(gdi)
            Else
                addByGdi(familyName)
            End If
        Next

        If Not fileSet.Contains("Font_Awesome_6_Free-Solid-900.otf") AndAlso Directory.Exists(_currentFontDirectory) Then
            Dim faFiles = Directory.GetFiles(_currentFontDirectory, "*Font*Awesome*.otf", SearchOption.TopDirectoryOnly)
            If faFiles.Length = 0 Then faFiles = Directory.GetFiles(_currentFontDirectory, "*Font*Awesome*.ttf", SearchOption.TopDirectoryOnly)
            If faFiles.Length > 0 Then addByGdi(Path.GetFileNameWithoutExtension(faFiles(0)))
            If faFiles.Length > 0 AndAlso Not fileSet.Contains(Path.GetFileName(faFiles(0))) Then
                fontFiles.Add(faFiles(0))
                fileSet.Add(Path.GetFileName(faFiles(0)))
            End If
        End If

        Return fontFiles
    End Function

    ' ─── 上下文菜单事件 ───────────────────────────────────────────────

    ''' <summary>
    ''' 帮助菜单项。
    ''' </summary>
    Private Sub HelpMenu_Click(sender As Object, e As EventArgs)
        FormAbout.Show()
    End Sub

    ''' <summary>
    ''' 关于菜单项。
    ''' </summary>
    Private Sub AboutMenu_Click(sender As Object, e As EventArgs)
        FormAbout.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' 预览菜单项，触发预览按钮。
    ''' </summary>
    Private Sub PreviewMenu_Click(sender As Object, e As EventArgs)
        FormPreview.Show()
    End Sub

    ' ─── 启动时清理挂起操作 ──────────────────────────────────────────

    ''' <summary>
    ''' 执行待删除清单中的文件删除操作（移至回收站）。
    ''' </summary>
    Private Sub ExecutePendingDeletions()
        Try
            If Not File.Exists(PendingDeletionsPath) Then Return
            Dim lines = File.ReadAllLines(PendingDeletionsPath)
            Dim remaining As New List(Of String)

            For Each filePath In lines
                If String.IsNullOrWhiteSpace(filePath) Then Continue For
                If Not File.Exists(filePath) Then Continue For
                Try
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(
                        filePath,
                        Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                        Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin)
                Catch ex As Exception
                    remaining.Add(filePath)
                End Try
            Next

            If remaining.Count > 0 Then
                File.WriteAllLines(PendingDeletionsPath, remaining)
            Else
                File.Delete(PendingDeletionsPath)
            End If
        Catch ex As Exception
            ' 忽略异常
        End Try
    End Sub

    ''' <summary>
    ''' 执行待复制清单中的文件复制操作。
    ''' </summary>
    ''' <summary>
    ''' 执行待复制清单中的文件复制操作，并在成功后删除缓存文件。
    ''' </summary>
    Private Sub ExecutePendingCopies()
        Try
            If Not File.Exists(PendingCopiesPath) Then Return
            Dim lines = File.ReadAllLines(PendingCopiesPath)
            Dim remaining As New List(Of String)

            For Each line In lines
                If String.IsNullOrWhiteSpace(line) Then Continue For
                Dim parts = line.Split("|"c)
                If parts.Length <> 2 Then Continue For

                Dim src = parts(0).Trim()
                Dim dest = parts(1).Trim()

                ' 如果源文件不存在，跳过（可能已被删除）
                If Not File.Exists(src) Then
                    Continue For
                End If

                Try
                    ' 确保目标目录存在
                    Directory.CreateDirectory(Path.GetDirectoryName(dest))
                    ' 复制文件（覆盖目标）
                    File.Copy(src, dest, True)
                    ' 复制成功后删除缓存文件
                    File.Delete(src)
                Catch ex As Exception
                    ' 复制失败，保留该项以便下次重试
                    remaining.Add(line)
                End Try
            Next

            ' 更新清单：只保留失败项
            If remaining.Count > 0 Then
                File.WriteAllLines(PendingCopiesPath, remaining)
            Else
                ' 所有项都成功，删除清单文件
                File.Delete(PendingCopiesPath)
            End If

            ' 如果缓存目录为空，则删除该目录
            If Directory.Exists(CacheDir) AndAlso Not Directory.GetFiles(CacheDir).Any() Then
                Try
                    Directory.Delete(CacheDir)
                Catch
                End Try
            End If
        Catch ex As Exception
            ' 忽略异常，避免影响程序启动
        End Try
    End Sub

    ' ─── JSON 解析辅助 ───────────────────────────────────────────────

    ''' <summary>
    ''' 解析 JSON 中指定键的对象块。
    ''' </summary>
    Private Function ParseJsonBlock(jsonText As String, key As String) As String
        Dim pattern = $"""{Regex.Escape(key)}""\s*:\s*\{{"
        Dim m = Regex.Match(jsonText, pattern)
        If Not m.Success Then Return ""

        Dim startPos = m.Index + m.Length - 1
        Dim depth = 0
        For i = startPos To jsonText.Length - 1
            If jsonText(i) = "{"c Then depth += 1
            If jsonText(i) = "}"c Then
                depth -= 1
                If depth = 0 Then Return jsonText.Substring(startPos, i - startPos + 1)
            End If
        Next
        Return ""
    End Function

    ''' <summary>
    ''' 解析 JSON 中指定键的字符串值。
    ''' </summary>
    Private Function ParseJsonString(jsonText As String, key As String) As String
        Dim pattern = $"""{Regex.Escape(key)}""\s*:\s*""([^""]*)"""
        Dim m = Regex.Match(jsonText, pattern)
        Return If(m.Success, m.Groups(1).Value, "")
    End Function

    ' ─── 选项和菜单按钮 ──────────────────────────────────────────────

    Private Sub btnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        Dim clicked = _menu.Show(btnMenu, New Point(0, 0), ToolStripDropDownDirection.AboveRight)
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        _menu.Dispose()
        MyBase.OnFormClosed(e)
    End Sub

End Class