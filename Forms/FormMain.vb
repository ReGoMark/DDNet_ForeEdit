Imports System.Drawing.Text
Imports System.IO
Imports System.Runtime
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions

''' <summary>
''' DDNet ForeEdit 主窗体。 职责：字体目录加载、配置编辑、回退字体管理、字体包导出/导入、快捷键分发。
''' </summary>
Public Class FormMain

#Region "── 窗口截图 ────────────────────────────────"

    <DllImport("user32.dll")>
    Private Shared Function PrintWindow(hwnd As IntPtr, hdcBlt As IntPtr, nFlags As UInteger) As Boolean
    End Function

    Private Const PW_RENDERFULLCONTENT As UInteger = &H2

    <StructLayout(LayoutKind.Sequential)>
    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

#End Region

#Region "── 状态字段：字体映射与缓存 ────────────────────────────────"

    ''' <summary>
    ''' 与 lstbDirFonts.Items 平行：索引 i 对应的内部键（唯一，用于所有字典）。 格式： "{文件名}#{faceIndex}"（小写）。显示文本另行存放于 lstbDirFonts.Items(i)。
    ''' </summary>
    Private _listKeys As New List(Of String)

    ''' <summary>
    ''' 回退字体列表，存储 DDNet 名称（"family style"）。
    ''' </summary>
    Private _fallbacks As New List(Of String)

    ''' <summary>
    ''' 映射：DDNet 名称（"family style"，小写）→ 列表项键（同名时取列表中最先出现的一项）。
    ''' </summary>
    Private _familyToGdi As New Dictionary(Of String, String)

    ''' <summary>
    ''' 映射：列表项键（小写）→ TTC 序号标签（如 "1/3"）。
    ''' </summary>
    Private _ttcTag As New Dictionary(Of String, String)

    ''' <summary>
    ''' 映射：列表项键（小写）→ TTC 物理索引（0-based）。
    ''' </summary>
    Private _ttcPhysicalIndex As New Dictionary(Of String, Integer)

    ''' <summary>
    ''' 映射：列表项键（小写）→ index.json 使用的 DDNet 名称（"family_name style_name"）。
    ''' </summary>
    Private _gdiToFamily As New Dictionary(Of String, String)

    ''' <summary>
    ''' 映射：列表项键（小写）→ 该字体面在 PrivateFontCollection 中对应的 GDI+ 家族名。
    ''' </summary>
    Private _keyToGdiFamily As New Dictionary(Of String, String)

    ''' <summary>
    ''' index.json 中 "font files" 的文件名顺序。
    ''' </summary>
    Private _jsonFontFileOrder As New List(Of String)

    ''' <summary>
    ''' 映射：列表项键（小写）→ FontInfoTable。
    ''' </summary>
    Private _gdiToFontInfo As New Dictionary(Of String, FontInfoTable)

    Dim ni As FontInfoTable = Nothing

    ''' <summary>
    ''' 映射：列表项键（小写）→ 字体文件的完整物理路径。
    ''' </summary>
    Private _gdiToFilePath As New Dictionary(Of String, String)

    ''' <summary>
    ''' 缓存：列表项键（小写）→ PrivateFontCollection 实例。
    ''' </summary>
    Private _fontCache As New Dictionary(Of String, PrivateFontCollection)

    ''' <summary>
    ''' 所有已加载的 PrivateFontCollection 实例，用于统一释放。
    ''' </summary>
    Private _fontCollections As New List(Of PrivateFontCollection)

#End Region

#Region "── 状态字段：配置与目录 ──────────────────────────────────"

    Private _currentFontDirectory As String = ""
    Private _installDirectory As String = ""
    Private _lastFileExtension As String = ""
    Private _isFillingComboBoxes As Boolean = False
    Private _originalDefaultFont As String = ""
    Private _originalJapaneseFont As String = ""
    Private _originalKoreanFont As String = ""
    Private _originalSimplifiedChineseFont As String = ""
    Private _originalTraditionalChineseFont As String = ""

#End Region

#Region "── 状态字段：子窗体与共享资源 ────────────────────────────"

    Public Shared Property SharedItemHeight As Integer = 20

    Private _menu As New Win32ContextMenu()
    Private _listContextMenu As New Win32ContextMenu()
    Private _previewWindow As FormPreview = Nothing
    Private _searchWindow As FormSearch = Nothing

#End Region

#Region "── 绘制缓存（字体列表 / 回退列表自绘） ─────────────────"

    Private _drawFontCache As New Dictionary(Of String, Font)
    Private _tagSizeCache As New Dictionary(Of String, SizeF)
    Private ReadOnly _tagBrushNormal As New SolidBrush(Color.Gray)
    Private ReadOnly _tagBrushSelected As New SolidBrush(Color.FromArgb(200, 255, 255, 255))
    Private ReadOnly _tagFont As New Font("Consolas", 7.5F, FontStyle.Regular, GraphicsUnit.Point)

#End Region

#Region "── 常量：版本与标题 ──────────────────────────────────────"

    Public ReadOnly BuildVersion As String = "Build 2601003.12"
    Private ReadOnly TitleText As String = "DDNet ForeEdit"

#End Region

#Region "── 常量：目录结构 ────────────────────────────────────────"

    Private ReadOnly AppRoot As String = Application.StartupPath
    Private ReadOnly StandardFontsDir As String = Path.Combine(AppRoot, "data", "standard")
    Private ReadOnly ManifestDir As String = Path.Combine(AppRoot, "manifest")
    Private ReadOnly PendingDeletionsPath As String = Path.Combine(ManifestDir, "pendingdeletions.txt")
    Private ReadOnly PendingCopiesPath As String = Path.Combine(ManifestDir, "pendingcopies.txt")
    Private ReadOnly CacheDir As String = Path.Combine(AppRoot, "cache")

#End Region

#Region "── 常量：字体文件清单 ────────────────────────────────────"

    Private ReadOnly StandardFontFiles() As String = {
        "DejaVuSans.ttf",
        "Font_Awesome_6_Free-Solid-900.otf",
        "GlowSansJ-Compressed-Book.otf",
        "SourceHanSans.ttc"
    }

    Private ReadOnly ProtectedFontFiles() As String = {
        "Font_Awesome_6_Free-Solid-900.otf"
    }

#End Region

#Region "── 列表项键辅助 ──────────────────────────────────────────"

    ''' <summary>
    ''' 生成唯一的列表项键： "{文件名}#{faceIndex}"（小写）。
    ''' </summary>
    Private Function MakeFaceKey(filePath As String, faceIndex As Integer) As String
        Return (Path.GetFileName(filePath) & "#" & faceIndex.ToString()).ToLower()
    End Function

    ''' <summary>
    ''' 按列表索引取内部键（越界返回空字符串）。
    ''' </summary>
    Private Function KeyAt(idx As Integer) As String
        If idx < 0 OrElse idx >= _listKeys.Count Then Return ""
        Return _listKeys(idx)
    End Function

    ''' <summary>
    ''' 取当前选中项的键。
    ''' </summary>
    Private Function SelectedKey() As String
        Return KeyAt(lstbDirFonts.SelectedIndex)
    End Function

    ''' <summary>
    ''' ComboBox 索引 → 列表项键（LA 无偏移，其他 ComboBox 索引 0 为 "-"）。
    ''' </summary>
    Private Function ComboKey(cb As ComboBox) As String
        If cb Is Nothing OrElse cb.SelectedIndex < 0 Then Return ""
        Dim offset As Integer = If(cb Is cbLA, 0, 1)
        Return KeyAt(cb.SelectedIndex - offset)
    End Function

    ''' <summary>
    ''' 列表项键 → 列表索引（找不到返回 -1）。
    ''' </summary>
    Private Function ListIndexByKey(key As String) As Integer
        If String.IsNullOrEmpty(key) Then Return -1
        Return _listKeys.IndexOf(key.ToLower())
    End Function

#End Region

#Region "── 窗体生命周期 ──────────────────────────────────────────"

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EnsureDirectoriesExist()
        UpdateLanguageVariantControls()
        UpdateFallbackControls()
        ExecutePendingDeletions()
        ExecutePendingCopies()

        Me.Text = TitleText
        Me.KeyPreview = True
        lblDirFonts.Text = "等待数据加载"

        lstbDirFonts.ItemHeight = AwareListHeight.GetScaledItemHeight(Me)
        lstbFallbackFonts.ItemHeight = AwareListHeight.GetScaledItemHeight(Me)
        SharedItemHeight = AwareListHeight.GetScaledItemHeight(Me)

        Dim dblProp = GetType(Control).GetProperty(
            "DoubleBuffered",
            Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
        If dblProp IsNot Nothing Then
            dblProp.SetValue(lstbDirFonts, True, Nothing)
            dblProp.SetValue(lstbFallbackFonts, True, Nothing)
        End If

        _menu.AddRange({
    New Win32ContextMenu.MenuItem() With {.Id = 1, .Text = "预览(&P)", .ShortcutText = "Ctrl+P", .Icon = ImageList1.Images(0), .OnClick = Sub() btnPreview.PerformClick()},
    New Win32ContextMenu.MenuItem() With {.Id = 2, .Text = "查找(&F)", .ShortcutText = "Ctrl+F", .Icon = ImageList1.Images(4), .OnClick = Sub() btnSearch.PerformClick()},
    New Win32ContextMenu.MenuItem() With {.Id = 3, .Text = "Blade(&B)", .ShortcutText = "Ctrl+B", .Icon = ImageList1.Images(1), .OnClick = Sub() btnBlade.PerformClick()},
    Win32ContextMenu.MenuItem.Separator,
    New Win32ContextMenu.MenuItem() With {.Id = 4, .Text = "管理(&M)",
    .SubItems = New List(Of Win32ContextMenu.MenuItem) From {
            New Win32ContextMenu.MenuItem() With {.Id = 41, .Text = "未使用字体(&U)", .ShortcutText = "Ctrl+Shift+U", .OnClick = Sub() CheckFontJsonMatch()},
            New Win32ContextMenu.MenuItem() With {.Id = 42, .Text = "预装字体验证(&V)", .ShortcutText = "Ctrl+Shift+V", .OnClick = Sub() VerifyStandardFonts()},
            Win32ContextMenu.MenuItem.Separator,
            New Win32ContextMenu.MenuItem() With {.Id = 43, .Text = "配置文件(&J)", .ShortcutText = "Ctrl+Shift+J", .OnClick = Sub() OpenJsonConfig()},
            New Win32ContextMenu.MenuItem() With {.Id = 44, .Text = "字体目录(&D)", .ShortcutText = "Ctrl+L", .OnClick = Sub() OpenFontDirectory()},
            Win32ContextMenu.MenuItem.Separator,
            New Win32ContextMenu.MenuItem() With {.Id = 47, .Text = "恢复默认(&R)", .ShortcutText = "F8", .OnClick = Sub() btnDefault.PerformClick()}
        }},
    New Win32ContextMenu.MenuItem() With {.Id = 5, .Text = "刷新(&R)", .ShortcutText = "F5", .OnClick = Sub() btnRefresh.PerformClick()},
    Win32ContextMenu.MenuItem.Separator,
    New Win32ContextMenu.MenuItem() With {.Id = 7, .Text = "帮助(&H)", .Icon = ImageList1.Images(3),
    .SubItems = New List(Of Win32ContextMenu.MenuItem) From {
            New Win32ContextMenu.MenuItem() With {.Id = 74, .Text = "下载字体资源(&R)", .OnClick = Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://www.maoken.com/", .UseShellExecute = True})},
            New Win32ContextMenu.MenuItem() With {.Id = 75, .Text = "下载游戏材质(&H)", .OnClick = Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://teedata.net/", .UseShellExecute = True})},
            New Win32ContextMenu.MenuItem() With {.Id = 76, .Text = "访问中文社区(&T)", .OnClick = Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://teeworlds.cn/ddnet", .UseShellExecute = True})},
            Win32ContextMenu.MenuItem.Separator,
            New Win32ContextMenu.MenuItem() With {.Id = 71, .Text = "视频教程(&V)", .OnClick = Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://www.bilibili.com/video/BV1h7PezCE2i/?spm_id_from=333.1387.0.0&vd_source=c4099c355c2d06f10ac210fe7bae65a6", .UseShellExecute = True})},
            New Win32ContextMenu.MenuItem() With {.Id = 72, .Text = "说明文档(&D)", .OnClick = Sub() OpenLocalFile(Application.StartupPath & "\data\documents.pdf")},
            New Win32ContextMenu.MenuItem() With {.Id = 73, .Text = "快捷键参考(&K)", .OnClick = Sub() OpenLocalFile(Application.StartupPath & "\data\hotkeys.pdf")},
            Win32ContextMenu.MenuItem.Separator,
            New Win32ContextMenu.MenuItem() With {.Id = 77, .Text = "更新日志(&L)", .OnClick = Sub() OpenLocalFile(Application.StartupPath & "\data\updates.txt")},
            New Win32ContextMenu.MenuItem() With {.Id = 78, .Text = "获取更新(&U)", .OnClick = Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/ReGoMark/DDNet_ForeEdit/Release", .UseShellExecute = True})}
        }},
    New Win32ContextMenu.MenuItem() With {.Id = 8, .Text = "关于(&A)", .OnClick = Sub() FormAbout.ShowDialog(Me)}})

        _listContextMenu.AddRange({
        New Win32ContextMenu.MenuItem() With {.Id = 101, .Text = "查看(&V)", .ShortcutText = "Return", .OnClick = Sub() OpenSelectedFontFile()},
        New Win32ContextMenu.MenuItem() With {.Id = 102, .Text = "保存(&S)", .OnClick = Sub() SaveSelectedFontAs()},
        New Win32ContextMenu.MenuItem() With {.Id = 107, .Text = "卸载(&U)", .ShortcutText = "Delete", .OnClick = Sub() UninstallSelectedFont()},
        Win32ContextMenu.MenuItem.Separator,
        New Win32ContextMenu.MenuItem() With {.Id = 110, .Text = "设置为(&D)",
        .SubItems = New List(Of Win32ContextMenu.MenuItem) From {
                New Win32ContextMenu.MenuItem() With {.Id = 111, .Text = "默认西文(&L)", .OnClick = Sub() SetSelectedFontAsLanguage("LA")},
                Win32ContextMenu.MenuItem.Separator,
                New Win32ContextMenu.MenuItem() With {.Id = 112, .Text = "日文(&J)", .OnClick = Sub() SetSelectedFontAsLanguage("JP")},
                New Win32ContextMenu.MenuItem() With {.Id = 113, .Text = "韩文(&K)", .OnClick = Sub() SetSelectedFontAsLanguage("KR")},
                New Win32ContextMenu.MenuItem() With {.Id = 114, .Text = "简体中文(&S)", .OnClick = Sub() SetSelectedFontAsLanguage("SC")},
                New Win32ContextMenu.MenuItem() With {.Id = 115, .Text = "繁体中文(&T)", .OnClick = Sub() SetSelectedFontAsLanguage("TC")},
                Win32ContextMenu.MenuItem.Separator,
                New Win32ContextMenu.MenuItem() With {.Id = 108, .Text = "回退字体(&B)", .OnClick = Sub() InsertSelectedFontToFallbacks()}
            }
        },
        Win32ContextMenu.MenuItem.Separator,
        New Win32ContextMenu.MenuItem() With {.Id = 105, .Text = "复制字体名称(&N)", .ShortcutText = "Ctrl+Shift+N", .OnClick = Sub() CopySelectedFontName()},
        New Win32ContextMenu.MenuItem() With {.Id = 106, .Text = "复制家族名称(&F)", .ShortcutText = "Ctrl+Shift+F", .OnClick = Sub() CopySelectedFamilyName()},
        New Win32ContextMenu.MenuItem() With {.Id = 104, .Text = "打开字体目录(&L)", .OnClick = Sub() OpenSelectedFontFolder()},
        Win32ContextMenu.MenuItem.Separator,
        New Win32ContextMenu.MenuItem() With {.Id = 103, .Text = "属性(&I)", .ShortcutText = "Alt+Return", .OnClick = Sub() ShowFontPropertiesForSelected()}
    })
    End Sub

    ''' <summary>
    ''' 打开本地文档/文件，失败时提示。
    ''' </summary>
    Private Sub OpenLocalFile(filePath As String)
        Try
            If IO.File.Exists(filePath) Then
                Process.Start(New ProcessStartInfo With {.FileName = filePath, .UseShellExecute = True})
            Else
                MessageBox.Show($"文件不存在：{filePath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show($"打开文件失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub EnsureDirectoriesExist()
        For Each dir As String In {StandardFontsDir, ManifestDir, CacheDir}
            If Not Directory.Exists(dir) Then
                Try
                    Directory.CreateDirectory(dir)
                Catch ex As Exception
                End Try
            End If
        Next
    End Sub

    Private Sub CopyScreenshotToClipboard()
        If Me.WindowState = FormWindowState.Minimized Then
            MessageBox.Show("窗口已最小化，无法截图。", "截图", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            Using bmp As New Bitmap(Me.Width, Me.Height, Imaging.PixelFormat.Format32bppArgb)
                Using g As Graphics = Graphics.FromImage(bmp)
                    Dim hdc As IntPtr = g.GetHdc()
                    Try
                        PrintWindow(Me.Handle, hdc, PW_RENDERFULLCONTENT)
                    Finally
                        g.ReleaseHdc(hdc)
                    End Try
                End Using
                Clipboard.SetImage(bmp)
            End Using
        Catch ex As Exception
            MessageBox.Show($"截图失败：{ex.Message}", "截图", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 将当前选中字体设置为指定语言的字体。
    ''' </summary>
    Private Sub SetSelectedFontAsLanguage(langKey As String)
        Dim listIdx As Integer = lstbDirFonts.SelectedIndex
        If listIdx < 0 Then Return

        Dim targetCb As ComboBox = Nothing
        Select Case langKey.ToUpper()
            Case "LA" : targetCb = cbLA
            Case "JP" : targetCb = cbJP
            Case "KR" : targetCb = cbKR
            Case "SC" : targetCb = cbSC
            Case "TC" : targetCb = cbTC
            Case Else : Return
        End Select

        If langKey.ToUpper() <> "LA" AndAlso Not chkLanguageVariants.Checked Then
            chkLanguageVariants.Checked = True
        End If

        Dim offset As Integer = If(targetCb Is cbLA, 0, 1)
        Dim cbIdx As Integer = listIdx + offset
        If cbIdx >= 0 AndAlso cbIdx < targetCb.Items.Count Then
            targetCb.SelectedIndex = cbIdx
        Else
            MessageBox.Show("字体未在语言列表中。", "设置失败", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ''' <summary>
    ''' 将当前选中字体插入到回退字体列表。
    ''' </summary>
    Private Sub InsertSelectedFontToFallbacks()
        Dim key As String = SelectedKey()
        If String.IsNullOrEmpty(key) Then Return

        Dim familyName As String = ""
        If Not _gdiToFamily.TryGetValue(key, familyName) Then
            familyName = lstbDirFonts.SelectedItem.ToString()
        End If

        If _fallbacks.Contains(familyName) Then Return

        If Not chkFallbackFonts.Checked Then chkFallbackFonts.Checked = True

        _fallbacks.Add(familyName)
        RefreshFallbackListBox()
    End Sub

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

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        _menu.Dispose()
        _listContextMenu.Dispose()
        MyBase.OnFormClosed(e)
    End Sub

#End Region

#Region "── 核心加载逻辑 ──────────────────────────────────────────"

    Private Sub LoadFontDirectory(fdirectory As String,
                                  Optional sourceLabel As String = "",
                                  Optional skipLocalCheck As Boolean = False)

        Dim appData As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Dim ddnetFonts As String = Path.Combine(appData, "DDNet", "fonts")
        Dim teeworldsFonts As String = Path.Combine(appData, "Teeworlds", "fonts")
        Dim hasDDNet As Boolean = Directory.Exists(ddnetFonts)
        Dim hasTeeworlds As Boolean = Directory.Exists(teeworldsFonts)

        If fdirectory.ToLower() <> ddnetFonts.ToLower() AndAlso
           fdirectory.ToLower() <> teeworldsFonts.ToLower() Then
            _installDirectory = fdirectory
        End If

        UpdateLocalDirectoryIndicator(hasDDNet, hasTeeworlds)

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

        ClearAllCaches()

        Dim dirExists As Boolean = Directory.Exists(fdirectory)
        CheckStandardFontIntegrity()
        CheckJsonIntegrity()

        lstbDirFonts.Items.Clear()
        _listKeys.Clear()

        If dirExists Then
            Dim fontFiles As New List(Of String)
            fontFiles.AddRange(Directory.GetFiles(fdirectory, "*.ttf"))
            fontFiles.AddRange(Directory.GetFiles(fdirectory, "*.ttc"))
            fontFiles.AddRange(Directory.GetFiles(fdirectory, "*.otf"))
            fontFiles.Sort()

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
                Dim nameInfos As List(Of FontInfoTable) = FontInfoReader.ReadAllFontNames(filePath)

                Try
                    Dim pfc As New PrivateFontCollection()
                    pfc.AddFontFile(filePath)
                    If pfc.Families.Length > 0 Then
                        _fontCollections.Add(pfc)
                        RegisterFontFile(filePath, pfc, nameInfos)
                    Else
                        lstbDirFonts.Items.Add(fileName)
                        _listKeys.Add(fileName.ToLower())
                        pfc.Dispose()
                    End If
                Catch ex As Exception
                    lstbDirFonts.Items.Add(fileName)
                    _listKeys.Add(fileName.ToLower())
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

        UpdateFontStatisticsLabel()
        PopulateComboBoxes()
        LoadFallbackList()
        UpdateLanguageVariantControls()
        UpdateFallbackControls()

        If Not dirExists Then
            MessageBox.Show($"未找到字体目录: {fdirectory}", "加载字体目录", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ClearAllCaches()
        For Each pfc As PrivateFontCollection In _fontCollections
            pfc.Dispose()
        Next
        _fontCollections.Clear()
        _fontCache.Clear()
        _ttcTag.Clear()
        _gdiToFilePath.Clear()
        _gdiToFamily.Clear()
        _keyToGdiFamily.Clear()
        _familyToGdi.Clear()
        _ttcPhysicalIndex.Clear()
        _gdiToFontInfo.Clear()
        _listKeys.Clear()

        lblFontName.Text = "-" : lblFontFamily.Text = "-" : lblFontType.Text = "-"
        lblFontCoverage.Text = "-" : lblFontModify.Text = "-" : lblFontSize.Text = "-"

        For Each f In _drawFontCache.Values
            f.Dispose()
        Next
        _drawFontCache.Clear()
        _tagSizeCache.Clear()
    End Sub

    ''' <summary>
    ''' 把一个字体文件里的每个字体面登记为列表中的独立一项（TTF/OTF 为 1 个，TTC 为 N 个）。 显示文本 = GDI+ 本地化家族名（可能与其他项重复）；内部键 = "文件名#faceIndex"，唯一。
    ''' </summary>
    Private Sub RegisterFontFile(filePath As String,
                                 pfc As PrivateFontCollection,
                                 nameInfos As List(Of FontInfoTable))
        Dim fileName As String = Path.GetFileName(filePath)
        Dim isTtc As Boolean = Path.GetExtension(filePath).Equals(".ttc", StringComparison.OrdinalIgnoreCase)

        If nameInfos.Count = 0 Then
            ' 名称表读取失败：按 GDI+ 家族逐项登记（无 DDNet 名映射）
            Dim i As Integer = 0
            For Each fam As FontFamily In pfc.Families
                Dim key As String = MakeFaceKey(filePath, i)
                lstbDirFonts.Items.Add(fam.Name)
                _listKeys.Add(key)
                _fontCache(key) = pfc
                _gdiToFilePath(key) = filePath
                _keyToGdiFamily(key) = fam.Name
                i += 1
            Next
            Return
        End If

        For i As Integer = 0 To nameInfos.Count - 1
            Dim info As FontInfoTable = nameInfos(i)
            Dim gdiFamily As String = FindGdiFamilyName(pfc, info, i)
            Dim ddnetName As String = info.DdnetName
            Dim displayName As String = gdiFamily
            If String.IsNullOrEmpty(displayName) Then displayName = ddnetName
            If String.IsNullOrEmpty(displayName) Then displayName = fileName

            Dim key As String = MakeFaceKey(filePath, i)

            lstbDirFonts.Items.Add(displayName)   ' 显示文本不加后缀，允许重复
            _listKeys.Add(key)
            _fontCache(key) = pfc
            _gdiToFilePath(key) = filePath
            _keyToGdiFamily(key) = gdiFamily
            _ttcPhysicalIndex(key) = i
            _gdiToFontInfo(key) = info
            If isTtc AndAlso nameInfos.Count > 1 Then _ttcTag(key) = $"{i + 1}/{nameInfos.Count}"

            If Not String.IsNullOrEmpty(ddnetName) Then
                _gdiToFamily(key) = ddnetName
                If Not _familyToGdi.ContainsKey(ddnetName.ToLower()) Then
                    _familyToGdi(ddnetName.ToLower()) = key
                End If
            End If
        Next
    End Sub

    Private Function FindGdiFamilyName(pfc As PrivateFontCollection,
                                       info As FontInfoTable,
                                       faceIndex As Integer) As String
        Dim fams As FontFamily() = pfc.Families
        If fams.Length = 0 Then Return ""
        For Each raw As String In info.AllRawNames
            For Each fam As FontFamily In fams
                If String.Equals(fam.Name, raw, StringComparison.OrdinalIgnoreCase) Then Return fam.Name
            Next
        Next
        For Each fam As FontFamily In fams
            If String.Equals(fam.Name, info.FamilyName, StringComparison.OrdinalIgnoreCase) Then Return fam.Name
        Next
        Return fams(Math.Min(faceIndex, fams.Length - 1)).Name
    End Function

    Private Function JsonFileRank(key As String) As Integer
        Dim fp As String = ""
        If Not _gdiToFilePath.TryGetValue(key, fp) Then Return Integer.MaxValue
        Dim fn As String = Path.GetFileName(fp)
        Dim idx As Integer = _jsonFontFileOrder.FindIndex(
            Function(f) String.Equals(f, fn, StringComparison.OrdinalIgnoreCase))
        Return If(idx < 0, Integer.MaxValue, idx)
    End Function

    ''' <summary>
    ''' 按 DDNet 的 GetFaceByName 规则把名称解析为列表项键： 第一优先级：完整名称 "family style" 精确匹配；第二优先级：仅匹配 family。
    ''' 同级有多个候选时，取所在文件在 index.json "font files" 中最靠前的。找不到返回空字符串。
    ''' </summary>
    Private Function ResolveFaceKey(ddnetName As String) As String
        If String.IsNullOrEmpty(ddnetName) Then Return ""

        Dim exactKey As String = "" : Dim exactRank As Integer = Integer.MaxValue
        Dim familyKey As String = "" : Dim familyRank As Integer = Integer.MaxValue

        For i As Integer = 0 To _listKeys.Count - 1
            Dim key As String = _listKeys(i)
            Dim fullName As String = ""
            If Not _gdiToFamily.TryGetValue(key, fullName) Then Continue For
            Dim rank As Integer = JsonFileRank(key)

            If String.Equals(fullName, ddnetName, StringComparison.OrdinalIgnoreCase) Then
                If exactKey = "" OrElse rank < exactRank Then
                    exactKey = key : exactRank = rank
                End If
            Else
                Dim fi As FontInfoTable = Nothing
                If _gdiToFontInfo.TryGetValue(key, fi) AndAlso
                   String.Equals(fi.FamilyName, ddnetName, StringComparison.OrdinalIgnoreCase) Then
                    If familyKey = "" OrElse rank < familyRank Then
                        familyKey = key : familyRank = rank
                    End If
                End If
            End If
        Next

        Return If(exactKey <> "", exactKey, familyKey)
    End Function

    Private Function NormalizeDdnetName(name As String) As String
        Dim key As String = ResolveFaceKey(name)
        If String.IsNullOrEmpty(key) Then Return name
        Dim fullName As String = ""
        Return If(_gdiToFamily.TryGetValue(key, fullName), fullName, name)
    End Function

    ''' <summary>
    ''' 按当前列表内容重建「DDNet 名称 → 列表项键」映射（删除字体后调用）。
    ''' </summary>
    Private Sub RebuildFamilyToGdi()
        _familyToGdi.Clear()
        For i As Integer = 0 To _listKeys.Count - 1
            Dim key As String = _listKeys(i)
            Dim fullName As String = ""
            If _gdiToFamily.TryGetValue(key, fullName) AndAlso
               Not _familyToGdi.ContainsKey(fullName.ToLower()) Then
                _familyToGdi(fullName.ToLower()) = key
            End If
        Next
    End Sub

    Private Function ParseJsonFontFileOrder(jsonText As String) As List(Of String)
        Dim result As New List(Of String)
        Dim m As Match = Regex.Match(jsonText, """font files""\s*:\s*\[\s*(.*?)\s*\]", RegexOptions.Singleline)
        If Not m.Success Then Return result
        For Each mm As Match In Regex.Matches(m.Groups(1).Value, """([^""]+)""")
            result.Add(mm.Groups(1).Value)
        Next
        Return result
    End Function

#End Region

#Region "── 用户目录与指示器 ──────────────────────────────────────"

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

    Private Function ResolveUserDirectory(hasDDNet As Boolean, hasTeeworlds As Boolean,
                                          ddnetPath As String, teeworldsPath As String) As String
        Dim target As String = ""
        If hasDDNet AndAlso hasTeeworlds Then
            Using dlg As New DialogDirectory()
                dlg.DDNetPath = ddnetPath
                dlg.TeeworldsPath = teeworldsPath
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    target = dlg.SelectedPath
                Else
                    chkDirSelect.CheckState = CheckState.Unchecked
                    Return ""
                End If
            End Using
        ElseIf hasDDNet Then
            target = ddnetPath
        ElseIf hasTeeworlds Then
            target = teeworldsPath
        Else
            target = ddnetPath
        End If

        If Not Directory.Exists(target) Then
            Dim result = MessageBox.Show("用户目录不存在，是否创建？", "创建用户目录", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result <> DialogResult.Yes Then
                chkDirSelect.CheckState = CheckState.Unchecked
                Return ""
            End If
            Try
                Directory.CreateDirectory(target)
            Catch ex As Exception
                MessageBox.Show($"用户目录创建失败：{ex.Message}", "创建用户目录", MessageBoxButtons.OK, MessageBoxIcon.Error)
                chkDirSelect.CheckState = CheckState.Unchecked
                Return ""
            End Try
        End If
        Return target
    End Function

#End Region

#Region "── 字体统计与组合框 ──────────────────────────────────────"

    Private Sub UpdateFontStatisticsLabel()
        Dim totalItems As Integer = lstbDirFonts.Items.Count
        Dim ttcCount As Integer = _ttcTag.Count
        Dim actualFiles As Integer = _gdiToFilePath.Values.Distinct(StringComparer.OrdinalIgnoreCase).Count()
        Dim unusedList As List(Of String) = GetUnusedFontFiles()
        Dim unusedCount As Integer = If(unusedList IsNot Nothing, unusedList.Count, 0)

        Dim baseText As String = If(ttcCount > 0,
                                    $"字体 {totalItems} 项, 实际 {actualFiles} 项",
                                    $"字体 {totalItems} 项")
        lblDirFonts.Text = If(unusedCount > 0, $"{baseText}, 未使用 {unusedCount} 项", baseText)
    End Sub

    Private Sub PopulateComboBoxes()
        SyncComboBoxItems()
        LoadConfigurationFromJson()
    End Sub

    ''' <summary>
    ''' 同步所有语言组合框。ComboBox 项按索引与 _listKeys 对齐；项文本直接取 ListBox 显示文本（允许重复）。
    ''' </summary>
    Private Sub SyncComboBoxItems()
        _isFillingComboBoxes = True
        For Each cb As ComboBox In {cbLA, cbJP, cbKR, cbSC, cbTC}
            Dim previousKey As String = ComboKey(cb)
            cb.Items.Clear()
            If cb IsNot cbLA Then cb.Items.Add("-")
            For i As Integer = 0 To lstbDirFonts.Items.Count - 1
                cb.Items.Add(lstbDirFonts.Items(i))
            Next

            Dim restored As Boolean = False
            If Not String.IsNullOrEmpty(previousKey) Then
                Dim li As Integer = ListIndexByKey(previousKey)
                If li >= 0 Then
                    Dim offset As Integer = If(cb Is cbLA, 0, 1)
                    Dim cbIdx As Integer = li + offset
                    If cbIdx >= 0 AndAlso cbIdx < cb.Items.Count Then
                        cb.SelectedIndex = cbIdx
                        restored = True
                    End If
                End If
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

    Private Sub LoadConfigurationFromJson()
        _jsonFontFileOrder.Clear()
        Dim jsonPath As String = Path.Combine(_currentFontDirectory, "index.json")
        If Not File.Exists(jsonPath) Then
            chkLanguageVariants.Checked = False
            chkFallbackFonts.Checked = False
            Return
        End If

        Try
            _isFillingComboBoxes = True
            Dim jsonText As String = File.ReadAllText(jsonPath, Encoding.UTF8)
            _jsonFontFileOrder = ParseJsonFontFileOrder(jsonText)

            _originalDefaultFont = ParseJsonString(jsonText, "default")
            SelectComboBoxItemByFamilyName(cbLA, _originalDefaultFont)

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

            Dim fallbackPattern As String = """fallbacks""\s*:\s*\[([\s\S]*?)\]"
            Dim fbMatch As Match = Regex.Match(jsonText, fallbackPattern, RegexOptions.IgnoreCase)
            Dim hasFallbacks As Boolean = fbMatch.Success AndAlso Regex.IsMatch(fbMatch.Groups(1).Value, """[^""]+""")
            chkFallbackFonts.Checked = hasFallbacks
        Catch ex As Exception
        Finally
            _isFillingComboBoxes = False
            RefreshAllPreviewLabels()
            If _previewWindow IsNot Nothing AndAlso Not _previewWindow.IsDisposed AndAlso _previewWindow.Visible Then
                SynchronizePreviewWindow()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 根据 index.json 中的名称在组合框中选中对应字体面。
    ''' </summary>
    Private Sub SelectComboBoxItemByFamilyName(cb As ComboBox, familyName As String)
        If String.IsNullOrEmpty(familyName) Then Return
        Dim key As String = ResolveFaceKey(familyName)
        If String.IsNullOrEmpty(key) Then Return
        Dim li As Integer = ListIndexByKey(key)
        If li < 0 Then Return
        Dim offset As Integer = If(cb Is cbLA, 0, 1)
        Dim cbIdx As Integer = li + offset
        If cbIdx >= 0 AndAlso cbIdx < cb.Items.Count Then
            cb.SelectedIndex = cbIdx
        End If
    End Sub

#End Region

#Region "── 预览标签与字体获取 ────────────────────────────────────"

    Private Sub RefreshAllPreviewLabels()
        Dim comboBoxes As ComboBox() = {cbLA, cbJP, cbKR, cbSC, cbTC}
        Dim labels As Label() = {lblLA, lblJP, lblKorean, lblSC, lblTC}
        For i As Integer = 0 To comboBoxes.Length - 1
            Dim key As String = ComboKey(comboBoxes(i))
            If Not String.IsNullOrEmpty(key) Then ApplyFontToLabel(labels(i), key)
        Next
    End Sub

    Private Sub ApplyFontToLabel(lbl As Label, key As String)
        Dim oldFont = lbl.Font
        lbl.Font = GetFontForKey(key, 9.0F)
        oldFont?.Dispose()
    End Sub

    ''' <summary>
    ''' 根据列表项键获取 Font 实例（优先从缓存加载）。
    ''' </summary>
    Private Function GetFontForKey(key As String, size As Single) As Font
        Dim pfc As PrivateFontCollection = Nothing
        If _fontCache.TryGetValue(key, pfc) AndAlso pfc.Families.Length > 0 Then
            Dim gdiFamily As String = ""
            If Not _keyToGdiFamily.TryGetValue(key, gdiFamily) Then gdiFamily = ""
            Dim fam As FontFamily = Nothing
            If Not String.IsNullOrEmpty(gdiFamily) Then
                For Each f As FontFamily In pfc.Families
                    If f.Name.ToLower() = gdiFamily.ToLower() Then fam = f : Exit For
                Next
            End If
            If fam Is Nothing Then fam = pfc.Families(0)

            Dim style As FontStyle = FontStyle.Regular
            Dim ni As FontInfoTable = Nothing
            If _gdiToFontInfo.TryGetValue(key, ni) Then style = ni.GdiFontStyle
            If Not fam.IsStyleAvailable(style) Then style = FontStyle.Regular
            Try
                Return New Font(fam, size, style, GraphicsUnit.Point)
            Catch ex As Exception
            End Try
        End If
        Return New Font(Me.Font.FontFamily, size)
    End Function

#End Region

#Region "── 字体列表自绘与选中 ────────────────────────────────────"

    Private Sub lstbDirectoryFonts_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstbDirFonts.DrawItem
        If e.Index < 0 OrElse e.Index >= lstbDirFonts.Items.Count Then Return
        e.DrawBackground()

        Dim itemText As String = lstbDirFonts.Items(e.Index).ToString()
        Dim key As String = KeyAt(e.Index)

        If Not _drawFontCache.ContainsKey(key) Then
            _drawFontCache(key) = GetFontForKey(key, 9.0F)
        End If
        Dim drawFont As Font = _drawFontCache(key)

        Dim textColor As Color = If((e.State And DrawItemState.Selected) <> 0,
                                SystemColors.HighlightText, Color.Black)
        Dim textRect As New Rectangle(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Width - 8, e.Bounds.Height)
        TextRenderer.DrawText(e.Graphics, itemText, drawFont, textRect, textColor,
                          TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix)

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

    Private Sub lstbDirectoryFonts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstbDirFonts.SelectedIndexChanged
        If lstbDirFonts.SelectedItems.Count > 1 Then
            lblDirFonts.Text = $"字体 {lstbDirFonts.Items.Count} 项, 选中 {lstbDirFonts.SelectedItems.Count} 项"
            Return
        End If
        If lstbDirFonts.SelectedItems.Count = 1 Then UpdateFontStatisticsLabel()
        If lstbDirFonts.SelectedItems.Count = 0 Then ClearPropertyPanel() : Return

        Dim key As String = SelectedKey()
        Dim filePath As String = ""
        If Not _gdiToFilePath.TryGetValue(key, filePath) Then
            ClearPropertyPanel() : Return
        End If

        Dim fileInfo As New FileInfo(filePath)
        lblFontName.Text = fileInfo.Name
        Dim fileSize As Long = fileInfo.Length
        lblFontSize.Text = If(fileSize >= 1024 * 1024, $"{fileSize / 1024 / 1024:F2} MB",
                              If(fileSize >= 1024, $"{fileSize / 1024:F1} KB", $"{fileSize} B"))

        Dim ttcIndex As Integer = 0
        _ttcPhysicalIndex.TryGetValue(key, ttcIndex)
        Dim nameInfos As List(Of FontInfoTable) = FontInfoReader.ReadAllFontNames(filePath)
        Dim info As FontInfoTable = If(nameInfos.Count > ttcIndex, nameInfos(ttcIndex),
                                       If(nameInfos.Count > 0, nameInfos(0), Nothing))

        Dim tag As String = ""
        Dim baseType As String = If(_ttcTag.TryGetValue(key, tag),
                            "TTC", fileInfo.Extension.TrimStart(".").ToUpper())
        If info IsNot Nothing Then
            Dim weightPart As String = If(info.WeightClass > 0, info.WeightName, "")
            Dim widthPart As String = If(info.WidthClass > 0 AndAlso info.WidthClass <> 5, info.WidthName, "")
            Dim italicPart As String = If(info.IsItalic, "斜", "")     ' ← 新增：斜体标记
            Dim extras As String = String.Join(", ", {weightPart, widthPart, italicPart}.Where(Function(s) s <> ""))
            lblFontType.Text = If(String.IsNullOrEmpty(extras), baseType, $"{baseType}, {extras}")
        Else
            lblFontType.Text = baseType
        End If

        lblFontModify.Text = fileInfo.LastWriteTime.ToString("yyyy/MM/dd, HH:mm")
        Dim familyName As String = ""
        lblFontFamily.Text = If(_gdiToFamily.TryGetValue(key, familyName), familyName, "-")

        Dim ext As String = Path.GetExtension(filePath).ToLower()
        Dim coverage As String = ""
        If ext = ".ttc" Then
            Dim idx As Integer = 0
            _ttcPhysicalIndex.TryGetValue(key, idx)
            coverage = FontInfoReader.ReadLanguageCoverage(filePath, idx)
        Else
            coverage = FontInfoReader.ReadLanguageCoverage(filePath)
        End If
        lblFontCoverage.Text = coverage
    End Sub

    Private Sub ClearPropertyPanel()
        lblFontName.Text = "-" : lblFontSize.Text = "-"
        lblFontType.Text = "-" : lblFontModify.Text = "-"
        lblFontFamily.Text = "-" : lblFontCoverage.Text = "-"
    End Sub

#End Region

#Region "── 字体列表右键菜单与相关操作 ────────────────────────────"

    Private Sub lstbDirFonts_MouseDown(sender As Object, e As MouseEventArgs) Handles lstbDirFonts.MouseDown
        If e.Button <> MouseButtons.Right Then Return

        Dim idx As Integer = lstbDirFonts.IndexFromPoint(e.Location)
        If idx < 0 OrElse idx >= lstbDirFonts.Items.Count Then Return

        If Not lstbDirFonts.SelectedIndices.Contains(idx) Then
            lstbDirFonts.ClearSelected()
            lstbDirFonts.SelectedIndex = idx
        End If

        If lstbDirFonts.SelectedIndices.Count <> 1 Then Return

        _listContextMenu.Show(lstbDirFonts, e.Location)
    End Sub

    Private Sub OpenSelectedFontFile()
        Dim key As String = SelectedKey()
        Dim filePath As String = ""
        If Not _gdiToFilePath.TryGetValue(key, filePath) Then Return
        If Not File.Exists(filePath) Then Return
        Try
            Process.Start(New ProcessStartInfo(filePath) With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show($"无法打开字体：{ex.Message}", "打开字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveSelectedFontAs()
        Dim key As String = SelectedKey()
        Dim filePath As String = ""
        If Not _gdiToFilePath.TryGetValue(key, filePath) Then Return
        If Not File.Exists(filePath) Then Return

        Dim ext As String = Path.GetExtension(filePath)
        Using sfd As New SaveFileDialog
            sfd.Title = "另存为"
            sfd.FileName = Path.GetFileName(filePath)
            sfd.Filter = $"{ext.TrimStart("."c).ToUpper()} 字体文件|*{ext}|所有文件|*.*"
            sfd.DefaultExt = ext.TrimStart("."c)
            If sfd.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                File.Copy(filePath, sfd.FileName, True)
                MessageBox.Show("字体文件已保存。")
            Catch ex As Exception
                MessageBox.Show($"另存为失败：{ex.Message}", "另存为", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub OpenSelectedFontFolder()
        Dim key As String = SelectedKey()
        Dim filePath As String = ""
        If Not _gdiToFilePath.TryGetValue(key, filePath) Then Return
        If Not File.Exists(filePath) Then Return
        Process.Start("explorer.exe", $"/select,""{filePath}""")
    End Sub

    Private Sub CopySelectedFontPath()
        Dim key As String = SelectedKey()
        Dim filePath As String = ""
        If Not _gdiToFilePath.TryGetValue(key, filePath) Then Return
        Clipboard.SetText(filePath)
    End Sub

    Private Sub SelectAllFonts()
        If lstbDirFonts.Items.Count = 0 Then Return
        If lstbDirFonts.SelectionMode = SelectionMode.One Then Return
        lstbDirFonts.ClearSelected()
        For i As Integer = 0 To lstbDirFonts.Items.Count - 1
            lstbDirFonts.SetSelected(i, True)
        Next
    End Sub

#End Region

#Region "── ComboBox 事件 ─────────────────────────────────────────"

    Private Sub ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLA.SelectedIndexChanged,
        cbJP.SelectedIndexChanged, cbKR.SelectedIndexChanged, cbSC.SelectedIndexChanged, cbTC.SelectedIndexChanged

        If _isFillingComboBoxes Then Return

        Dim cb = CType(sender, ComboBox)
        Dim labelMap As New Dictionary(Of ComboBox, Label) From {
            {cbLA, lblLA}, {cbJP, lblJP}, {cbKR, lblKorean}, {cbSC, lblSC}, {cbTC, lblTC}
        }
        Dim key As String = ComboKey(cb)
        If Not String.IsNullOrEmpty(key) AndAlso labelMap.ContainsKey(cb) Then
            ApplyFontToLabel(labelMap(cb), key)
        End If

        If _previewWindow IsNot Nothing AndAlso _previewWindow.Visible Then
            Dim langKey = cb.Name.Replace("cb", "").ToUpper()
            Dim curNi As FontInfoTable = Nothing
            If Not String.IsNullOrEmpty(key) Then _gdiToFontInfo.TryGetValue(key, curNi)
            _previewWindow.UpdatePreview(langKey, curNi)
        End If
    End Sub

#End Region

#Region "── 回退字体管理 ──────────────────────────────────────────"

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
                _fallbacks.Add(NormalizeDdnetName(mm.Groups(1).Value))
            Next
        Catch ex As Exception
            MessageBox.Show($"读取回退字体配置时出错：{ex.Message}", "读取回退字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        PopulateFallbackComboBox()
        RefreshFallbackListBox()
    End Sub

    Private Sub PopulateFallbackComboBox()
        cbFallbackFonts.Items.Clear()
        For i As Integer = 0 To lstbDirFonts.Items.Count - 1
            cbFallbackFonts.Items.Add(lstbDirFonts.Items(i))
        Next
        If cbFallbackFonts.Items.Count > 0 Then cbFallbackFonts.SelectedIndex = 0
    End Sub

    Private Sub RefreshFallbackListBox()
        lstbFallbackFonts.Items.Clear()
        For Each familyName As String In _fallbacks
            Dim key As String = ResolveFaceKey(familyName)
            Dim displayName As String = familyName
            If Not String.IsNullOrEmpty(key) Then
                Dim li As Integer = ListIndexByKey(key)
                If li >= 0 Then displayName = lstbDirFonts.Items(li).ToString()
            End If
            lstbFallbackFonts.Items.Add(displayName)
        Next
        lstbFallbackFonts.Refresh()
    End Sub

    Private Sub lstbFallbackFonts_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstbFallbackFonts.DrawItem
        If e.Index < 0 OrElse e.Index >= lstbFallbackFonts.Items.Count Then Return
        e.DrawBackground()

        Dim itemText As String = lstbFallbackFonts.Items(e.Index).ToString()
        Dim textColor As Color = If((e.State And DrawItemState.Selected) <> 0,
                                    SystemColors.HighlightText, Color.Black)
        Dim textRect As New Rectangle(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Width - 8, e.Bounds.Height)
        TextRenderer.DrawText(e.Graphics, itemText, e.Font, textRect, textColor,
                              TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix)

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

    Private Sub InsertFallbackItem(sender As Object, e As EventArgs) Handles btnInsert.Click
        Dim cbIdx As Integer = cbFallbackFonts.SelectedIndex
        If cbIdx < 0 Then Return
        Dim key As String = KeyAt(cbIdx)
        If String.IsNullOrEmpty(key) Then Return

        Dim familyName As String = ""
        If Not _gdiToFamily.TryGetValue(key, familyName) Then
            familyName = lstbDirFonts.Items(cbIdx).ToString()
        End If

        Dim insertIndex As Integer = If(lstbFallbackFonts.SelectedIndex >= 0,
                                        lstbFallbackFonts.SelectedIndex + 1,
                                        _fallbacks.Count)
        If _fallbacks.Contains(familyName) Then Return

        _fallbacks.Insert(insertIndex, familyName)
        RefreshFallbackListBox()
        lstbFallbackFonts.SelectedIndex = insertIndex
    End Sub

    Private Sub RemoveFallbackItem(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim idx As Integer = lstbFallbackFonts.SelectedIndex
        If idx < 0 Then Return
        _fallbacks.RemoveAt(idx)
        RefreshFallbackListBox()
        If _fallbacks.Count > 0 Then lstbFallbackFonts.SelectedIndex = Math.Min(idx, _fallbacks.Count - 1)
    End Sub

    Private Sub RestoreFallbackList(sender As Object, e As EventArgs) Handles btnRecoveryFallbackFonts.Click
        LoadFallbackList()
    End Sub

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

#End Region

#Region "── 开关：语言变体 / 回退字体 ──────────────────────────────"

    Private Sub chkLanguageVariants_CheckedChanged(sender As Object, e As EventArgs) Handles chkLanguageVariants.CheckedChanged
        UpdateLanguageVariantControls()
    End Sub

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
        tagJP.Enabled = enabled : tagTC.Enabled = enabled
        tagKR.Enabled = enabled : tagSC.Enabled = enabled
    End Sub

    Private Sub chkFallbackFonts_CheckedChanged(sender As Object, e As EventArgs) Handles chkFallbackFonts.CheckedChanged
        UpdateFallbackControls()
    End Sub

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

#End Region

#Region "── 跟随 / 恢复语言字体 ────────────────────────────────────"

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
            Dim cbIdx As Integer = cbLA.SelectedIndex + 1
            If cbIdx < targetComboBox.Items.Count Then targetComboBox.SelectedIndex = cbIdx
        End If
    End Sub

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
    ''' 获取组合框当前选中字体面的 DDNet 名称。
    ''' </summary>
    Private Function GetSelectedFamilyName(cb As ComboBox) As String
        If cb.SelectedIndex < 0 Then Return ""
        If cb IsNot cbLA AndAlso cb.SelectedIndex = 0 Then Return ""
        Dim key As String = ComboKey(cb)
        If String.IsNullOrEmpty(key) Then Return ""
        Dim familyName As String = ""
        Return If(_gdiToFamily.TryGetValue(key, familyName), familyName, "")
    End Function

#End Region

#Region "── 配置生成与写入 ────────────────────────────────────────"

    Private Function CollectUsedFaceKeys() As List(Of String)
        Dim keys As New List(Of String)
        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim addKey As Action(Of String) = Sub(k As String)
                                              If Not String.IsNullOrEmpty(k) AndAlso seen.Add(k) Then keys.Add(k)
                                          End Sub

        For Each cb As ComboBox In {cbLA, cbJP, cbKR, cbSC, cbTC}
            If Not String.IsNullOrEmpty(GetSelectedFamilyName(cb)) Then
                addKey(ComboKey(cb))
            End If
        Next
        For Each famName As String In _fallbacks
            addKey(ResolveFaceKey(famName))
        Next
        Return keys
    End Function

    Private Function BuildFontFileNameList(usedKeys As List(Of String)) As List(Of String)
        Dim fileSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim fontFileNames As New List(Of String)

        For Each key As String In usedKeys
            Dim filePath As String = ""
            If _gdiToFilePath.TryGetValue(key, filePath) Then
                Dim fileName As String = Path.GetFileName(filePath)
                If fileSet.Add(fileName) Then fontFileNames.Add(fileName)
            End If
        Next

        If Not fileSet.Contains("Font_Awesome_6_Free-Solid-900.otf") Then
            If Directory.Exists(_currentFontDirectory) Then
                Dim faFiles = Directory.GetFiles(_currentFontDirectory, "*Font*Awesome*.otf", SearchOption.TopDirectoryOnly)
                If faFiles.Length = 0 Then
                    faFiles = Directory.GetFiles(_currentFontDirectory, "*Font*Awesome*.ttf", SearchOption.TopDirectoryOnly)
                End If
                If faFiles.Length > 0 Then
                    Dim faFile = Path.GetFileName(faFiles(0))
                    If fileSet.Add(faFile) Then fontFileNames.Add(faFile)
                End If
            End If
        End If

        Return fontFileNames
    End Function

    Private Function GameResolveKey(ddnetName As String, orderedFileNames As List(Of String)) As String
        For Each fn As String In orderedFileNames
            For i As Integer = 0 To _listKeys.Count - 1
                Dim key As String = _listKeys(i)
                Dim fp As String = ""
                Dim fullName As String = ""
                If _gdiToFilePath.TryGetValue(key, fp) AndAlso
                   String.Equals(Path.GetFileName(fp), fn, StringComparison.OrdinalIgnoreCase) AndAlso
                   _gdiToFamily.TryGetValue(key, fullName) AndAlso
                   String.Equals(fullName, ddnetName, StringComparison.OrdinalIgnoreCase) Then
                    Return key
                End If
            Next
        Next
        Return ""
    End Function

    Private Function FindAmbiguousSelections() As List(Of String)
        Dim conflicts As New List(Of String)
        Dim usedKeys As List(Of String) = CollectUsedFaceKeys()
        Dim fileNames As List(Of String) = BuildFontFileNameList(usedKeys)

        For Each key As String In usedKeys
            Dim ddnetName As String = ""
            If Not _gdiToFamily.TryGetValue(key, ddnetName) Then Continue For
            Dim winner As String = GameResolveKey(ddnetName, fileNames)
            If winner <> "" AndAlso Not String.Equals(winner, key, StringComparison.OrdinalIgnoreCase) Then
                Dim li As Integer = ListIndexByKey(key)
                Dim winIdx As Integer = ListIndexByKey(winner)
                Dim keyText As String = If(li >= 0, lstbDirFonts.Items(li).ToString(), key)
                Dim winText As String = If(winIdx >= 0, lstbDirFonts.Items(winIdx).ToString(), winner)
                conflicts.Add($"{ddnetName}：选中的是「{keyText}」，游戏会加载「{winText}」")
            End If
        Next
        Return conflicts
    End Function

    Private Function BuildIndexJsonContent() As String
        Dim fontFileNames As List(Of String) = BuildFontFileNameList(CollectUsedFaceKeys())

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
            sb.Append("        """).Append(NormalizeDdnetName(_fallbacks(i))).Append("""")
            If i < _fallbacks.Count - 1 Then sb.AppendLine(",") Else sb.AppendLine()
        Next
        sb.AppendLine("    ],")
        sb.AppendLine("    ""icon"": ""Font Awesome 6 Free""")
        sb.AppendLine("}")
        Return sb.ToString()
    End Function

    Private Sub ApplyConfiguration(sender As Object, e As EventArgs) Handles btnApply.Click
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim jsonPath = Path.Combine(_currentFontDirectory, "index.json")

        Dim ambiguous As List(Of String) = FindAmbiguousSelections()
        If ambiguous.Count > 0 Then
            Dim warnText As String = "以下字体名称在已引用的字体文件中重复，游戏只会使用最先加载的那个：" &
                                     vbCrLf & vbCrLf &
                                     String.Join(vbCrLf, ambiguous.Select(Function(a) "· " & a)) &
                                     vbCrLf & vbCrLf & "仍要写入配置吗？"
            If MessageBox.Show(warnText, "写入配置",
                               MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) <> DialogResult.OK Then Return
        End If

        If MessageBox.Show("确认要写入配置吗？", "写入配置",
                           MessageBoxButtons.OKCancel, MessageBoxIcon.Question) <> DialogResult.OK Then Return

        Try
            Dim content = BuildIndexJsonContent()
            File.WriteAllText(jsonPath, content, Encoding.UTF8)
            _jsonFontFileOrder = ParseJsonFontFileOrder(content)

            _originalDefaultFont = GetSelectedFamilyName(cbLA)
            _originalJapaneseFont = GetSelectedFamilyName(cbJP)
            _originalKoreanFont = GetSelectedFamilyName(cbKR)
            _originalSimplifiedChineseFont = GetSelectedFamilyName(cbSC)
            _originalTraditionalChineseFont = GetSelectedFamilyName(cbTC)

            If (Control.ModifierKeys And Keys.Shift) = Keys.Shift Then
                Dim parentDir = Directory.GetParent(_currentFontDirectory)?.Parent
                Dim exePath = Path.Combine(parentDir.FullName, "ddnet.exe")
                If File.Exists(exePath) Then Process.Start(exePath)
            End If
        Catch ex As Exception
            MessageBox.Show($"配置写入失败：{ex.Message}", "写入配置", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExportConfiguration(sender As Object, e As EventArgs) Handles btnCopy.Click
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                MessageBox.Show("配置文件已导出。", "导出配置", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"配置文件导出失败：{ex.Message}", "导出配置", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

#End Region

#Region "── 恢复默认 ──────────────────────────────────────────────"

    Private Sub RestoreDefaultFonts(sender As Object, e As EventArgs) Handles btnDefault.Click
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If chkDirSelect.Checked Then
            Dim result = MessageBox.Show($"要恢复用户目录的默认状态，请按以下步骤操作：
* 单击「确定」在资源管理器中打开该文件夹
* 关闭本程序，注意不是关闭资源管理器
* 清空配置和所有字体，或删除整个 fonts 文件夹",
                                     "还原默认字体",
                                     MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            If result = DialogResult.OK Then
                Process.Start("explorer.exe", $"""{_currentFontDirectory}""")
            End If
            Return
        End If

        Dim sourceJson = Path.Combine(StandardFontsDir, "index.json")
        Dim destJson = Path.Combine(_currentFontDirectory, "index.json")

        If Not File.Exists(sourceJson) OrElse Not Directory.Exists(StandardFontsDir) Then
            MessageBox.Show("预装字体或配置文件缺失，无法还原。", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim stdFiles As New List(Of String)
        stdFiles.AddRange(Directory.GetFiles(StandardFontsDir, "*.ttf"))
        stdFiles.AddRange(Directory.GetFiles(StandardFontsDir, "*.ttc"))
        stdFiles.AddRange(Directory.GetFiles(StandardFontsDir, "*.otf"))
        If stdFiles.Count = 0 Then
            MessageBox.Show("预装字体缺失，无法还原。", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Using dlg As New DialogRestore()
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return

            If dlg.SelectedMode = DialogRestore.RestoreMode.FontAndConfig Then
                Try
                    File.Copy(sourceJson, destJson, True)

                    Dim failedCopies As New List(Of String)
                    For Each src In stdFiles
                        Dim fileName = Path.GetFileName(src)
                        Dim destPath = Path.Combine(_currentFontDirectory, fileName)
                        Try
                            File.Copy(src, destPath, True)
                        Catch
                            failedCopies.Add($"{src}|{destPath}")
                        End Try
                    Next

                    Dim stdNames As New HashSet(Of String)(
                    stdFiles.Select(Function(f) Path.GetFileName(f)),
                    StringComparer.OrdinalIgnoreCase)

                    Dim toDelete As New List(Of String)
                    For Each ext In {"*.ttf", "*.ttc", "*.otf"}
                        For Each f In Directory.GetFiles(_currentFontDirectory, ext)
                            If Not stdNames.Contains(Path.GetFileName(f)) Then toDelete.Add(f)
                        Next
                    Next

                    For Each line In failedCopies
                        Dim dest = line.Split("|"c)(1)
                        If Not toDelete.Contains(dest, StringComparer.OrdinalIgnoreCase) Then toDelete.Add(dest)
                    Next

                    AppendPendingLines(PendingCopiesPath, failedCopies)
                    AppendPendingLines(PendingDeletionsPath, toDelete)

                    Dim hasPending As Boolean = (failedCopies.Count > 0) OrElse (toDelete.Count > 0)
                    If hasPending Then
                        MessageBox.Show("默认字体和配置恢复成功，下次启动时生效。", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("默认字体和配置恢复成功。", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    LoadFontDirectory(If(String.IsNullOrEmpty(_installDirectory), _currentFontDirectory, _installDirectory))
                Catch ex As Exception
                    MessageBox.Show($"恢复默认字体时发生错误：{ex.Message}", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                Try
                    File.Copy(sourceJson, destJson, True)
                    LoadConfigurationFromJson()
                    LoadFallbackList()
                    MessageBox.Show("默认配置已恢复。", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show($"恢复默认配置时发生错误：{ex.Message}", "还原默认字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

#End Region

#Region "── 字体目录 / 打开 / 刷新 ──────────────────────────────────"

    Private Sub btnLocate_Click(sender As Object, e As EventArgs) Handles btnLocate.Click
        If (Control.ModifierKeys And Keys.Shift) = Keys.Shift Then
            Clipboard.SetText(tbPath.Text)
        Else
            OpenFontDirectory()
        End If
    End Sub

    Private Sub OpenFontDirectory()
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Process.Start("explorer.exe", $"""{_currentFontDirectory}""")
    End Sub

    Private Sub RefreshCurrentDirectory(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If Not String.IsNullOrEmpty(_currentFontDirectory) Then
            If chkDirSelect.Checked Then
                LoadFontDirectory(_currentFontDirectory, "", skipLocalCheck:=True)
            Else
                LoadFontDirectory(_currentFontDirectory, GetSourceLabel(_lastFileExtension), skipLocalCheck:=True)
            End If
        Else
            MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

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

#End Region

#Region "── 浏览 / 拖放加载字体目录 ────────────────────────────────"

    Private Sub btnBrowse_DragEnter(sender As Object, e As DragEventArgs) Handles btnBrowse.DragEnter
        e.Effect = If(e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

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
            MessageBox.Show($"加载字体目录时发生错误：{ex.Message}", "加载错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Using dlg As New OpenFileDialog()
            dlg.Title = "选择「DDNet 快捷方式」「DDNet.exe」或「index.json」"
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
                MessageBox.Show($"加载字体目录时发生错误：{ex.Message}", "加载错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

#End Region

#Region "── 路径解析辅助 ──────────────────────────────────────────"

    Private Function ResolveFileToFontDirectory(selectedFile As String) As String
        Dim ext = Path.GetExtension(selectedFile).ToLower()
        Select Case ext
            Case ".url"
                Dim installPath = SteamLinkResolver.GetInstallDirectory(selectedFile)
                If String.IsNullOrEmpty(installPath) Then
                    MessageBox.Show("无法解析 .URL 快捷方式，请确认快捷方式有效。", "解析错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return ""
                End If
                Return Path.Combine(installPath, "DDNet", "data", "fonts")
            Case ".lnk"
                Dim target = ResolveLnkTarget(selectedFile)
                If String.IsNullOrEmpty(target) Then
                    MessageBox.Show("无法解析 .lnk 快捷方式，请确认快捷方式有效。", "解析错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return ""
                End If
                Return BuildFontDirectoryFromExe(target)
            Case ".exe"
                Return BuildFontDirectoryFromExe(selectedFile)
            Case ".json"
                Dim dir = Path.GetDirectoryName(selectedFile)
                If Not String.IsNullOrEmpty(dir) AndAlso Directory.Exists(dir) Then Return dir
                MessageBox.Show("无法定位 index.json 目录，请确认目录结构有效。", "路径错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return ""
            Case Else
                MessageBox.Show("请选择有效的文件类型：.url, .lnk, .exe, .json。", "文件类型错误", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return ""
        End Select
    End Function

    Private Function BuildFontDirectoryFromExe(exePath As String) As String
        Dim dir = If(File.Exists(exePath), Path.GetDirectoryName(exePath), exePath)
        Return Path.Combine(dir, "data", "fonts")
    End Function

    Private Function ResolveLnkTarget(lnkPath As String) As String
        Try
            Dim shell As Object = CreateObject("WScript.Shell")
            Dim shortcut As Object = shell.CreateShortcut(lnkPath)
            Return CStr(shortcut.TargetPath)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function GetSourceLabel(ext As String) As String
        Select Case ext.TrimStart(".").ToUpper()
            Case "LNK" : Return "快捷方式"
            Case "URL" : Return "超链接"
            Case "JSON" : Return "配置"
            Case "EXE" : Return "应用程序"
            Case Else : Return ext.TrimStart(".").ToUpper()
        End Select
    End Function

#End Region

#Region "── 字体导入 ──────────────────────────────────────────────"

    Private Sub btnAdd_DragEnter(sender As Object, e As DragEventArgs) Handles btnFontInstall.DragEnter
        e.Effect = If(e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub btnAdd_DragDrop(sender As Object, e As DragEventArgs) Handles btnFontInstall.DragDrop
        Dim files = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
        If files IsNot Nothing AndAlso files.Length > 0 Then ImportFontFiles(files)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnFontInstall.Click
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    Private Sub ImportFontFiles(sourcePaths As String())
        If Not Directory.Exists(_currentFontDirectory) Then
            MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        If toAdd.Count = 0 AndAlso duplicates.Count = 0 Then
            MessageBox.Show("没有可导入的字体，仅支持 .ttf、.otf、.ttc 字体。", "安装字体", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If toAdd.Count = 0 Then
            Dim dupSize As Long = 0
            For Each dup As String In duplicates
                Try
                    Dim p = Path.Combine(_currentFontDirectory, dup)
                    If File.Exists(p) Then dupSize += New FileInfo(p).Length
                Catch
                End Try
            Next
            Using dlg As New DialogPopUp()
                dlg.Text = "安装字体"
                dlg.TitleText = $"所选 {duplicates.Count} 个字体已存在, 跳过安装"
                dlg.DescriptionText = $"新增字体大小: {FormatBytes(dupSize)}"
                dlg.btnOK.Visible = False
                dlg.CancelText = "确定"
                For Each dup In duplicates
                    dlg.Items.Add($"[跳过] {dup}")
                Next
                dlg.ShowDialog(Me)
            End Using
            Return
        End If

        Try
            For Each src As String In toAdd
                File.Copy(src, Path.Combine(_currentFontDirectory, Path.GetFileName(src)), False)
            Next

            Dim addSize As Long = 0
            For Each f As String In toAdd
                Try
                    If File.Exists(f) Then addSize += New FileInfo(f).Length
                Catch
                End Try
            Next

            Using dlg As New DialogPopUp()
                dlg.Text = "安装字体"
                dlg.TitleText = $"成功安装 {toAdd.Count} 个字体，跳过 {duplicates.Count} 个已存在的字体"
                dlg.DescriptionText = $"新增字体大小: {FormatBytes(addSize)}"
                dlg.btnOK.Visible = False
                dlg.CancelText = "确定"
                For Each f As String In toAdd
                    dlg.Items.Add($"{Path.GetFileName(f)}")
                Next
                For Each dup In duplicates
                    dlg.Items.Add($"[跳过] {dup}")
                Next
                dlg.ShowDialog(Me)
            End Using

            IncrementallyScanNewFonts(toAdd)
        Catch ex As Exception
            MessageBox.Show($"安装字体时出错：{ex.Message}", "安装字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub IncrementallyScanNewFonts(newFilePaths As IEnumerable(Of String))
        For Each filePath As String In newFilePaths
            Dim fileName = Path.GetFileName(filePath)
            Dim nameInfos = FontInfoReader.ReadAllFontNames(filePath)

            Try
                Dim pfc As New PrivateFontCollection()
                pfc.AddFontFile(filePath)
                If pfc.Families.Length = 0 Then
                    pfc.Dispose()
                    lstbDirFonts.Items.Add(fileName)
                    _listKeys.Add(fileName.ToLower())
                    Continue For
                End If
                _fontCollections.Add(pfc)
                RegisterFontFile(filePath, pfc, nameInfos)
            Catch ex As Exception
                lstbDirFonts.Items.Add(fileName)
                _listKeys.Add(fileName.ToLower())
            End Try
        Next

        SyncComboBoxItems()
        PopulateFallbackComboBox()
        UpdateFontStatisticsLabel()
    End Sub

#End Region

#Region "── 字体删除（挂起） ──────────────────────────────────────"

    Private Sub DeleteSelectedFonts(sender As Object, e As EventArgs) Handles btnFontUninstall.Click
        UninstallSelectedFont()
    End Sub

    Private Sub UninstallSelectedFont()
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim filePathSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim blockedNames As New List(Of String)

        For Each idx As Integer In lstbDirFonts.SelectedIndices
            Dim key As String = KeyAt(idx)
            If String.IsNullOrEmpty(key) Then Continue For
            Dim fp As String = ""
            If Not _gdiToFilePath.TryGetValue(key, fp) Then Continue For
            Dim fn = Path.GetFileName(fp)
            If ProtectedFontFiles.Any(Function(f) f.Equals(fn, StringComparison.OrdinalIgnoreCase)) Then
                blockedNames.Add(fn)
                Continue For
            End If
            filePathSet.Add(fp)
        Next

        If blockedNames.Count > 0 Then
            MessageBox.Show($"字体「{blockedNames(0)}」写保护。", "字体写保护", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        If filePathSet.Count = 0 Then Return

        Using dlg As New DialogPopUp()
            dlg.Text = "确认卸载"
            dlg.TitleText = $"确认要卸载以下 {filePathSet.Count} 个字体?"
            dlg.ConfirmText = "卸载"
            dlg.CancelText = "取消"

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
            For Each fp As String In presetFiles
                dlg.Items.Add($"[预装] {Path.GetFileName(fp)}")
            Next
            For Each fp As String In normalFiles
                dlg.Items.Add(Path.GetFileName(fp))
            Next
            dlg.DescriptionText = "字体文件将在下次启动时卸载"
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
        End Using

        Try
            Directory.CreateDirectory(Path.GetDirectoryName(PendingDeletionsPath))
            File.AppendAllLines(PendingDeletionsPath, filePathSet)

            Dim toRemoveKeys As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            For Each fp As String In filePathSet
                For Each kvp In _gdiToFilePath
                    If kvp.Value.ToLower() = fp.ToLower() Then toRemoveKeys.Add(kvp.Key)
                Next
            Next

            ' 同步移除 ListBox 与平行键列表
            For i = lstbDirFonts.Items.Count - 1 To 0 Step -1
                Dim key As String = KeyAt(i)
                If toRemoveKeys.Contains(key) Then
                    lstbDirFonts.Items.RemoveAt(i)
                    _listKeys.RemoveAt(i)
                End If
            Next

            Dim deletedFamilyNames As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            For Each key As String In toRemoveKeys
                Dim cfg = ""
                If _gdiToFamily.TryGetValue(key, cfg) Then deletedFamilyNames.Add(cfg)
                deletedFamilyNames.Add(key)
            Next

            For Each key As String In toRemoveKeys
                _gdiToFilePath.Remove(key)
                _gdiToFamily.Remove(key)
                _keyToGdiFamily.Remove(key)
                _gdiToFontInfo.Remove(key)
                _fontCache.Remove(key)
                _ttcTag.Remove(key)
                _ttcPhysicalIndex.Remove(key)
            Next

            RebuildFamilyToGdi()

            _fallbacks.RemoveAll(Function(fn) deletedFamilyNames.Contains(fn) AndAlso String.IsNullOrEmpty(ResolveFaceKey(fn)))
            RefreshFallbackListBox()
            SyncComboBoxItems()
            PopulateFallbackComboBox()
            UpdateFontStatisticsLabel()
        Catch ex As Exception
            MessageBox.Show($"卸载字体失败：{ex.Message}", "卸载字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "── 字体属性对话框（ShellExecuteEx） ───────────────────────"

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

    Private Sub ShowFontProperties(sender As Object, e As EventArgs) Handles btnFontInfo.Click
        ShowFontPropertiesForSelected()
    End Sub

    Private Sub ShowFontPropertiesForSelected()
        If lstbDirFonts.SelectedIndex < 0 Then
            MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim key As String = SelectedKey()
        Dim filePath As String = ""
        If Not _gdiToFilePath.TryGetValue(key, filePath) Then Return
        If Not File.Exists(filePath) Then Return

        Dim sei As New ShellExecuteInfo()
        sei.cbSize = Marshal.SizeOf(sei)
        sei.lpVerb = "properties"
        sei.lpFile = filePath
        sei.nShow = SW_SHOW
        sei.fMask = SEE_MASK_INVOKEIDLIST
        ShellExecuteEx(sei)
    End Sub

#End Region

#Region "── 系统/用户字体文件夹链接 ───────────────────────────────"

    Private Sub OpenSystemFonts(sender As Object, e As EventArgs) Handles lblSystemFonts.Click
        Dim winDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows)
        Process.Start("explorer.exe", Path.Combine(winDir, "Fonts"))
    End Sub

    Private Sub OpenUserFonts(sender As Object, e As EventArgs) Handles lblLocalFonts.Click
        Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\AppData\Local\Microsoft\Windows\Fonts")
    End Sub

#End Region

#Region "── 双击字体列表 / 复制字体名与族名 ──────────────────────"

    Private Sub lstbDirectoryFonts_DoubleClick(sender As Object, e As EventArgs) Handles lstbDirFonts.DoubleClick
        OpenSelectedFontFile()
    End Sub

    Private Sub CopyFamilyName(sender As Object, e As EventArgs) Handles lblFontFamily.DoubleClick
        CopySelectedFamilyName()
    End Sub

    Private Sub CopySelectedFamilyName()
        If lblFontFamily.Text = "-" OrElse String.IsNullOrEmpty(lblFontFamily.Text) Then Return
        Clipboard.SetText(lblFontFamily.Text)
    End Sub

    Private Sub CopyFontName(sender As Object, e As EventArgs) Handles lblFontName.DoubleClick
        CopySelectedFontName()
    End Sub

    ''' <summary>
    ''' 复制当前选中字体的显示名（ListBox 文本）。
    ''' </summary>
    Private Sub CopySelectedFontName()
        If lstbDirFonts.SelectedIndex < 0 Then Return
        Dim itemText = lstbDirFonts.SelectedItem.ToString()
        If Not String.IsNullOrEmpty(itemText) Then Clipboard.SetText(itemText)
    End Sub

#End Region

#Region "── 预览窗口 ──────────────────────────────────────────────"

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

    Private Sub SynchronizePreviewWindow()
        If _previewWindow Is Nothing OrElse _previewWindow.IsDisposed Then Return
        Dim configs As New Dictionary(Of String, ComboBox) From {
            {"LA", cbLA}, {"JP", cbJP}, {"KR", cbKR}, {"SC", cbSC}, {"TC", cbTC}
        }
        For Each kvp In configs
            Dim curNi As FontInfoTable = Nothing
            Dim key As String = ComboKey(kvp.Value)
            If Not String.IsNullOrEmpty(key) Then _gdiToFontInfo.TryGetValue(key, curNi)
            _previewWindow.UpdatePreview(kvp.Key, curNi)
        Next
    End Sub

#End Region

#Region "── 预装字体验证 ──────────────────────────────────────────"

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
        Dim problemFileNames As New List(Of String)

        For Each kvp In required
            Dim fileName = kvp.Key
            Dim expectedFamily = kvp.Value
            Dim filePath = Path.Combine(_currentFontDirectory, fileName)

            If Not File.Exists(filePath) Then
                missing.Add($"✖ {fileName}")
                problemFileNames.Add(fileName)
                Continue For
            End If

            Dim actualFamily = ""
            Try
                Dim names = FontInfoReader.ReadAllFontNames(filePath)
                If names.Count > 0 Then actualFamily = names(0).FamilyName
            Catch ex As Exception
                mismatch.Add($"✖ {fileName}")
                problemFileNames.Add(fileName)
                Continue For
            End Try

            If String.Equals(actualFamily, expectedFamily, StringComparison.OrdinalIgnoreCase) Then
                present.Add($"✔ {fileName}")
            Else
                mismatch.Add($"✖ {fileName}")
                problemFileNames.Add(fileName)
            End If
        Next

        Dim hasProblem As Boolean = (missing.Count > 0 OrElse mismatch.Count > 0)

        Dim dialogResult As DialogResult
        Using dlg As New DialogPopUp()
            dlg.Text = "预装字体验证"
            If hasProblem Then
                dlg.TitleText = $"存在 {missing.Count + mismatch.Count} 个问题"
                dlg.DescriptionText = "部分预装字体缺失或引用不匹配"
            Else
                dlg.TitleText = "所有预装字体完整"
                dlg.DescriptionText = "所有字体存在且家族名称匹配"
            End If
            dlg.ConfirmText = "修复"
            dlg.CancelText = "取消"
            dlg.btnOK.Visible = True

            For Each item In present
                dlg.Items.Add(item)
            Next
            For Each item In mismatch
                dlg.Items.Add(item)
            Next
            For Each item In missing
                dlg.Items.Add(item)
            Next
            dialogResult = dlg.ShowDialog(Me)
        End Using

        If hasProblem AndAlso dialogResult = DialogResult.OK Then
            RepairStandardFonts(problemFileNames)
        End If
    End Sub

    Private Function RepairStandardFonts(problemFileNames As List(Of String)) As Boolean
        If problemFileNames Is Nothing OrElse problemFileNames.Count = 0 Then Return False

        If Not Directory.Exists(StandardFontsDir) Then
            MessageBox.Show("预装字体源目录不存在，无法修复。" & vbCrLf & $"路径：{StandardFontsDir}",
                            "修复预装字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim deleteLines As New List(Of String)
        Dim copyLines As New List(Of String)
        Dim missingInSource As New List(Of String)
        Dim immediateCount As Integer = 0

        For Each fileName As String In problemFileNames
            Dim srcPath = Path.Combine(StandardFontsDir, fileName)
            Dim destPath = Path.Combine(_currentFontDirectory, fileName)

            If Not File.Exists(srcPath) Then
                missingInSource.Add(fileName)
                Continue For
            End If

            Try
                File.Copy(srcPath, destPath, True)
                immediateCount += 1
            Catch
                If File.Exists(destPath) Then deleteLines.Add(destPath)
                copyLines.Add($"{srcPath}|{destPath}")
            End Try
        Next

        If missingInSource.Count > 0 Then
            Dim msg = "以下字体在预装源目录中也不存在，无法修复：" & vbCrLf &
                  String.Join(vbCrLf, missingInSource.Select(Function(f) "  · " & f))
            MessageBox.Show(msg, "修复预装字体", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        If immediateCount = 0 AndAlso copyLines.Count = 0 AndAlso deleteLines.Count = 0 Then Return False

        Try
            Directory.CreateDirectory(ManifestDir)
            AppendPendingLines(PendingDeletionsPath, deleteLines)
            AppendPendingLines(PendingCopiesPath, copyLines)

            Dim hasPending As Boolean = (copyLines.Count > 0) OrElse (deleteLines.Count > 0)
            If hasPending Then
                MessageBox.Show("预装字体修复成功，下次启动时生效。", "修复预装字体", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("预装字体修复成功。", "修复预装字体", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show($"写入修复清单失败：{ex.Message}", "修复预装字体", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

#End Region

#Region "── 配置指示灯 / 未使用字体检查 ────────────────────────────"

    Private Sub lblConfig_Click(sender As Object, e As EventArgs) Handles lblConfig.Click
        OpenJsonConfig()
    End Sub

    Private Sub OpenJsonConfig()
        If String.IsNullOrEmpty(_currentFontDirectory) Then
            MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim filePath = Path.Combine(_currentFontDirectory, "index.json")
        If Not File.Exists(filePath) Then
            MessageBox.Show("未找到配置文件：" & filePath, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            Process.Start(New ProcessStartInfo(filePath) With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show("无法打开配置文件：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CheckUnusedFonts(sender As Object, e As EventArgs) Handles lblDirFonts.Click
        CheckFontJsonMatch()
    End Sub

    Private Sub CheckFontJsonMatch()
        If String.IsNullOrEmpty(_currentFontDirectory) OrElse Not Directory.Exists(_currentFontDirectory) Then Return

        Dim jsonPath = Path.Combine(_currentFontDirectory, "index.json")
        If Not File.Exists(jsonPath) Then
            MessageBox.Show("无法检查字体使用情况，配置或字体不存在。", "未使用字体", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim unused = GetUnusedFontFiles()
        If unused Is Nothing OrElse unused.Count = 0 Then
            MessageBox.Show("所有字体均被引用。", "未使用字体", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim totalSize As Long = 0
        For Each fileName As String In unused
            Dim filePath = Path.Combine(_currentFontDirectory, fileName)
            If File.Exists(filePath) Then
                Try
                    totalSize += New FileInfo(filePath).Length
                Catch
                End Try
            End If
        Next

        Using dlg As New DialogPopUp()
            dlg.Text = "未使用字体"
            dlg.TitleText = $"以下 {unused.Count} 个字体未使用:"
            dlg.DescriptionText = $"未使用字体大小: {FormatBytes(totalSize)}"
            dlg.ConfirmText = "全选"
            dlg.CancelText = "取消"
            dlg.btnOK.Visible = True
            For Each fn As String In unused
                dlg.Items.Add(fn)
            Next
            If dlg.ShowDialog(Me) = DialogResult.OK Then SelectUnusedFontsInList(unused)
        End Using
    End Sub

    Private Function GetUnusedFontFiles() As List(Of String)
        If String.IsNullOrEmpty(_currentFontDirectory) OrElse Not Directory.Exists(_currentFontDirectory) Then Return Nothing

        Dim jsonPath = Path.Combine(_currentFontDirectory, "index.json")
        If Not File.Exists(jsonPath) Then Return Nothing

        Dim allFiles As New List(Of String)
        allFiles.AddRange(Directory.GetFiles(_currentFontDirectory, "*.ttf"))
        allFiles.AddRange(Directory.GetFiles(_currentFontDirectory, "*.ttc"))
        allFiles.AddRange(Directory.GetFiles(_currentFontDirectory, "*.otf"))
        If allFiles.Count = 0 Then Return New List(Of String)

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
        End Try

        Dim unused As New List(Of String)
        For Each f As String In allFiles
            Dim fileName = Path.GetFileName(f)
            If Not usedFiles.Contains(fileName) Then unused.Add(fileName)
        Next
        Return unused
    End Function

    Private Sub SelectUnusedFontsInList(unusedFileNames As List(Of String))
        If unusedFileNames Is Nothing OrElse unusedFileNames.Count = 0 Then Return
        If lstbDirFonts.Items.Count = 0 Then Return

        Dim unusedSet As New HashSet(Of String)(unusedFileNames, StringComparer.OrdinalIgnoreCase)
        lstbDirFonts.ClearSelected()
        For i As Integer = 0 To lstbDirFonts.Items.Count - 1
            Dim key As String = KeyAt(i)
            Dim filePath As String = ""
            If _gdiToFilePath.TryGetValue(key, filePath) Then
                If unusedSet.Contains(Path.GetFileName(filePath)) Then lstbDirFonts.SetSelected(i, True)
            End If
        Next
        lstbDirFonts.Focus()
        lblDirFonts.Text = $"字体 {lstbDirFonts.Items.Count} 项, 选中 {lstbDirFonts.SelectedIndices.Count} 项"
    End Sub

#End Region

#Region "── 配置 / 预装完整性检查 ─────────────────────────────────"

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
                If Not File.Exists(Path.Combine(_currentFontDirectory, fileName)) Then missing.Add(fileName)
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

#End Region

#Region "── BLADE（字体包管理集成） ────────────────────────────────"

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
            If File.Exists(jsonPath) Then jsonContent = BuildIndexJsonContent()
        End If
        ' ── 排除已标记待删除的字体文件（下次启动才真正删除）──
        Dim pendingSet As HashSet(Of String) = GetPendingDeletionFileNames()
        If pendingSet.Count > 0 Then
            normalFiles = normalFiles.Where(Function(f) Not pendingSet.Contains(Path.GetFileName(f))).ToList()
            standardFiles = standardFiles.Where(Function(f) Not pendingSet.Contains(Path.GetFileName(f))).ToList()
            fullFiles = fullFiles.Where(Function(f) Not pendingSet.Contains(Path.GetFileName(f))).ToList()
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

    Private Function GetUsedFontFiles() As List(Of String)
        Dim fontFiles As New List(Of String)
        Dim fileSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim pendingSet As HashSet(Of String) = GetPendingDeletionFileNames()

        Dim addByKey As Action(Of String) = Sub(key As String)
                                                Dim fp As String = ""
                                                If _gdiToFilePath.TryGetValue(key, fp) AndAlso
                                                   File.Exists(fp) AndAlso
                                                   Not pendingSet.Contains(Path.GetFileName(fp)) AndAlso
                                                   Not fileSet.Contains(Path.GetFileName(fp)) Then
                                                    fontFiles.Add(fp)
                                                    fileSet.Add(Path.GetFileName(fp))
                                                End If
                                            End Sub

        For Each cb As ComboBox In {cbLA, cbJP, cbKR, cbSC, cbTC}
            Dim key As String = ComboKey(cb)
            If Not String.IsNullOrEmpty(key) Then addByKey(key)
        Next

        For Each familyName As String In _fallbacks
            Dim key As String = ResolveFaceKey(familyName)
            If Not String.IsNullOrEmpty(key) Then addByKey(key)
        Next

        If Not fileSet.Contains("Font_Awesome_6_Free-Solid-900.otf") AndAlso Directory.Exists(_currentFontDirectory) Then
            Dim faFiles = Directory.GetFiles(_currentFontDirectory, "*Font*Awesome*.otf", SearchOption.TopDirectoryOnly)
            If faFiles.Length = 0 Then faFiles = Directory.GetFiles(_currentFontDirectory, "*Font*Awesome*.ttf", SearchOption.TopDirectoryOnly)
            If faFiles.Length > 0 AndAlso Not pendingSet.Contains(Path.GetFileName(faFiles(0))) Then
                Dim faName = Path.GetFileName(faFiles(0))
                If fileSet.Add(faName) Then fontFiles.Add(faFiles(0))
            End If
        End If

        Return fontFiles
    End Function

    ''' <summary>
    ''' 读取 pendingdeletions.txt，返回待删除文件的文件名集合（忽略大小写）。 用于让 Blade 打包时排除已标记待删除的字体。
    ''' </summary>
    Private Function GetPendingDeletionFileNames() As HashSet(Of String)
        Dim set0 As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Try
            If File.Exists(PendingDeletionsPath) Then
                For Each line In File.ReadAllLines(PendingDeletionsPath)
                    If Not String.IsNullOrWhiteSpace(line) Then
                        set0.Add(Path.GetFileName(line.Trim()))
                    End If
                Next
            End If
        Catch
        End Try
        Return set0
    End Function

#End Region

#Region "── 上下文菜单遗留事件（未绑定，保留） ────────────────────"

    Private Sub HelpMenu_Click(sender As Object, e As EventArgs)
        FormAbout.Show()
    End Sub

    Private Sub AboutMenu_Click(sender As Object, e As EventArgs)
        FormAbout.ShowDialog(Me)
    End Sub

    Private Sub PreviewMenu_Click(sender As Object, e As EventArgs)
        FormPreview.Show()
    End Sub

#End Region

#Region "── 启动时清理挂起操作 ────────────────────────────────────"

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
        End Try
    End Sub

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

                If Not File.Exists(src) Then Continue For

                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(dest))
                    File.Copy(src, dest, True)

                    If IsInCacheDir(src) Then
                        Try
                            File.Delete(src)
                        Catch
                        End Try
                    End If
                Catch ex As Exception
                    remaining.Add(line)
                End Try
            Next

            If remaining.Count > 0 Then
                File.WriteAllLines(PendingCopiesPath, remaining)
            Else
                File.Delete(PendingCopiesPath)
            End If

            If Directory.Exists(CacheDir) AndAlso Not Directory.GetFiles(CacheDir).Any() Then
                Try
                    Directory.Delete(CacheDir)
                Catch
                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function IsInCacheDir(filePath As String) As Boolean
        Try
            Dim cacheFull = Path.GetFullPath(CacheDir).TrimEnd("\"c) & "\"
            Dim fileFull = Path.GetFullPath(filePath)
            Return fileFull.StartsWith(cacheFull, StringComparison.OrdinalIgnoreCase)
        Catch
            Return False
        End Try
    End Function

#End Region

#Region "── 通用辅助 ──────────────────────────────────────────────"

    Private Function FormatBytes(bytes As Long) As String
        If bytes >= 1024L * 1024 * 1024 Then Return (bytes / 1024 / 1024 / 1024).ToString("F2") & " GB"
        If bytes >= 1024L * 1024 Then Return (bytes / 1024 / 1024).ToString("F2") & " MB"
        If bytes >= 1024 Then Return (bytes / 1024).ToString("F0") & " KB"
        Return bytes.ToString() & " B"
    End Function

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
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "── JSON 解析辅助 ─────────────────────────────────────────"

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

    Private Function ParseJsonString(jsonText As String, key As String) As String
        Dim pattern = $"""{Regex.Escape(key)}""\s*:\s*""([^""]*)"""
        Dim m = Regex.Match(jsonText, pattern)
        Return If(m.Success, m.Groups(1).Value, "")
    End Function

#End Region

#Region "── 主菜单按钮 / 悬浮标签 ──────────────────────────────────"

    Private Sub btnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        Dim clicked = _menu.Show(btnMenu, New Point(0, 0), ToolStripDropDownDirection.AboveRight)
    End Sub

    Private Sub lblLA_Click(sender As Object, e As EventArgs) Handles lblLA.Click
        Select Case lblLA.Text
            Case "Font demo text."
                lblLA.Text = "This はい 세례 示範"
            Case "This はい 세례 示範"
                lblLA.Text = "Font demo text."
        End Select
    End Sub

#End Region

#Region "── 查找窗口 ──────────────────────────────────────────────"

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        OpenSearchDialog()
    End Sub

    Private Sub OpenSearchDialog()
        If lstbDirFonts.Items.Count = 0 Then
            MessageBox.Show("字体目录未加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If _searchWindow Is Nothing OrElse _searchWindow.IsDisposed Then
            _searchWindow = New FormSearch()
            _searchWindow.TargetList = lstbDirFonts
            _searchWindow.Show(Me)
        Else
            _searchWindow.TargetList = lstbDirFonts
            If Not _searchWindow.Visible Then _searchWindow.Show(Me)
            _searchWindow.BringToFront()
            _searchWindow.Activate()
        End If
    End Sub

#End Region

#Region "── 全局快捷键分发 ────────────────────────────────────────"

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean

        Dim ctrl As Boolean = (keyData And Keys.Control) = Keys.Control
        Dim shift As Boolean = (keyData And Keys.Shift) = Keys.Shift
        Dim alt As Boolean = (keyData And Keys.Alt) = Keys.Alt
        Dim key As Keys = keyData And Keys.KeyCode

        If ctrl AndAlso Not shift Then
            Select Case key
                Case Keys.O : btnBrowse.PerformClick() : Return True
                Case Keys.L : btnLocate.PerformClick() : Return True
                Case Keys.R : btnRefresh.PerformClick() : Return True
                Case Keys.I : btnFontInstall.PerformClick() : Return True
                Case Keys.P : btnPreview.PerformClick() : Return True
                Case Keys.S : btnApply.PerformClick() : Return True
                Case Keys.J : btnCopy.PerformClick() : Return True
                Case Keys.F : btnSearch.PerformClick() : Return True
                Case Keys.B : btnBlade.PerformClick() : Return True
                Case Keys.U : chkDirSelect.Checked = Not chkDirSelect.Checked : Return True
                Case Keys.A
                    If lstbDirFonts.Focused Then
                        SelectAllFonts()
                        Return True
                    End If
            End Select
        End If

        If ctrl AndAlso shift Then
            Select Case key
                Case Keys.J : OpenJsonConfig() : Return True
                Case Keys.V : VerifyStandardFonts() : Return True
                Case Keys.U : CheckFontJsonMatch() : Return True
                Case Keys.N : CopySelectedFontName() : Return True
                Case Keys.F : CopySelectedFamilyName() : Return True
            End Select
        End If

        If Not ctrl AndAlso Not alt Then
            Select Case key
                Case Keys.F1
                    Try
                        Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/ReGoMark/DDNet_ForeEdit", .UseShellExecute = True})
                    Catch ex As Exception
                        MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Case Keys.F2
                    CopyScreenshotToClipboard()
                Case Keys.F5 : btnRefresh.PerformClick() : Return True
                Case Keys.F8
                    btnDefault.PerformClick() : Return True
                Case Keys.F12
                    Dim exePath As String = IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "tool", "FontInfoReader.exe")
                    Try
                        If IO.File.Exists(exePath) Then
                            Process.Start(New ProcessStartInfo(exePath) With {.UseShellExecute = True, .WorkingDirectory = IO.Path.GetDirectoryName(exePath)})
                        Else
                            MessageBox.Show($"程序不存在：{exePath}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    Catch ex As Exception
                        MessageBox.Show($"启动失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Case Keys.Apps
                    btnMenu.PerformClick() : Return True
                Case Keys.Enter
                    If lstbDirFonts.Focused Then
                        OpenSelectedFontFile()
                        Return True
                    End If
                Case Keys.Delete
                    If lstbDirFonts.Focused Then
                        UninstallSelectedFont()
                        Return True
                    End If
            End Select
        End If

        If alt Then
            Select Case key
                Case Keys.Enter : btnFontInfo.PerformClick() : Return True
            End Select
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

#End Region

End Class