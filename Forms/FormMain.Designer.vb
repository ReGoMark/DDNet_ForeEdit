<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        tblTopBar = New TableLayoutPanel()
        btnSearch = New Button()
        lblCfg = New Label()
        btnLocate = New Button()
        chkLocalDir = New CheckBox()
        lblPreset = New Label()
        tbPath = New TextBox()
        btnBrowse = New Button()
        btnRefresh = New Button()
        pnlFontInfo = New Panel()
        tblFontInfo = New TableLayoutPanel()
        tagFontFamily = New Label()
        tagFontType = New Label()
        tagFontCoverage = New Label()
        tagFontSize = New Label()
        tagFontModify = New Label()
        lblFontName = New Label()
        lblFontFamily = New Label()
        lblFontType = New Label()
        lblFontCoverage = New Label()
        lblFontSize = New Label()
        lblFontModify = New Label()
        tagFontName = New Label()
        TableLayoutPanel4 = New TableLayoutPanel()
        btnFontInstall = New Button()
        btnFontInfo = New Button()
        btnFontUninstall = New Button()
        lstDirFonts = New ListBox()
        splitDirectory = New Label()
        lblDirFonts = New Label()
        lblLocalFonts = New Label()
        splitFont = New Label()
        lblSystemFonts = New Label()
        tblDirFonts = New TableLayoutPanel()
        splitCfg = New Label()
        tblConfigFonts = New TableLayoutPanel()
        btnRecoveryLA = New Button()
        tagKR = New Label()
        pnlFallbackFonts = New Panel()
        lstFallback = New ListBox()
        pnlFallback = New Panel()
        cmbFallback = New ComboBox()
        btnUp = New Button()
        btnInsert = New Button()
        btnRecoveryFallback = New Button()
        btnRemove = New Button()
        btnDown = New Button()
        tagSC = New Label()
        tagTC = New Label()
        lblLA = New Label()
        lblTC = New Label()
        lblSC = New Label()
        lblKorean = New Label()
        lblJP = New Label()
        chkLanguage = New CheckBox()
        chkFallback = New CheckBox()
        cmbLA = New ComboBox()
        cmbJP = New ComboBox()
        cmbKR = New ComboBox()
        cmbSC = New ComboBox()
        cmbTC = New ComboBox()
        btnRecoveryJP = New Button()
        btnFollowJP = New Button()
        btnFollowKR = New Button()
        btnRecoveryKR = New Button()
        btnFollowSC = New Button()
        btnRecoverySC = New Button()
        btnFollowTC = New Button()
        btnRecoveryTC = New Button()
        tagLA = New Label()
        tagJP = New Label()
        tblBottonBar = New TableLayoutPanel()
        btnMenu = New Button()
        btnApply = New Button()
        btnPreview = New Button()
        ProgressBar1 = New ProgressBar()
        btnBlade = New Button()
        btnOutput = New Button()
        btnDefault = New Button()
        ToolTip1 = New ToolTip(components)
        pnlDirFonts = New Panel()
        ImageList1 = New ImageList(components)
        tblTopBar.SuspendLayout()
        pnlFontInfo.SuspendLayout()
        tblFontInfo.SuspendLayout()
        TableLayoutPanel4.SuspendLayout()
        tblDirFonts.SuspendLayout()
        tblConfigFonts.SuspendLayout()
        pnlFallbackFonts.SuspendLayout()
        pnlFallback.SuspendLayout()
        tblBottonBar.SuspendLayout()
        pnlDirFonts.SuspendLayout()
        SuspendLayout()
        ' 
        ' tblTopBar
        ' 
        tblTopBar.ColumnCount = 8
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.Controls.Add(btnSearch, 6, 0)
        tblTopBar.Controls.Add(lblCfg, 3, 0)
        tblTopBar.Controls.Add(btnLocate, 7, 0)
        tblTopBar.Controls.Add(chkLocalDir, 1, 0)
        tblTopBar.Controls.Add(lblPreset, 2, 0)
        tblTopBar.Controls.Add(tbPath, 4, 0)
        tblTopBar.Controls.Add(btnBrowse, 0, 0)
        tblTopBar.Controls.Add(btnRefresh, 5, 0)
        tblTopBar.Location = New Point(12, 12)
        tblTopBar.Name = "tblTopBar"
        tblTopBar.RowCount = 1
        tblTopBar.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblTopBar.Size = New Size(488, 30)
        tblTopBar.TabIndex = 0
        ' 
        ' btnSearch
        ' 
        btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), Image)
        btnSearch.Location = New Point(372, 0)
        btnSearch.Margin = New Padding(3, 0, 3, 0)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(30, 30)
        btnSearch.TabIndex = 29
        ToolTip1.SetToolTip(btnSearch, "查找(Ctrl+F)")
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' lblCfg
        ' 
        lblCfg.AutoSize = True
        lblCfg.Cursor = Cursors.Hand
        lblCfg.Dock = DockStyle.Left
        lblCfg.Location = New Point(240, 3)
        lblCfg.Margin = New Padding(0, 3, 0, 3)
        lblCfg.Name = "lblCfg"
        lblCfg.Size = New Size(58, 24)
        lblCfg.TabIndex = 6
        lblCfg.Text = "配置 ✔"
        lblCfg.TextAlign = ContentAlignment.MiddleLeft
        ToolTip1.SetToolTip(lblCfg, "配置文件(Ctrl+Shift+J)")
        ' 
        ' btnLocate
        ' 
        btnLocate.Location = New Point(408, 0)
        btnLocate.Margin = New Padding(3, 0, 0, 0)
        btnLocate.Name = "btnLocate"
        btnLocate.Padding = New Padding(3, 0, 3, 0)
        btnLocate.Size = New Size(80, 30)
        btnLocate.TabIndex = 3
        btnLocate.Text = "打开位置"
        ToolTip1.SetToolTip(btnLocate, "打开字体目录(Ctrl+L)" & vbCrLf & "复制路径(Shift+点击)")
        btnLocate.UseVisualStyleBackColor = True
        ' 
        ' chkLocalDir
        ' 
        chkLocalDir.Appearance = Appearance.Button
        chkLocalDir.Location = New Point(74, 0)
        chkLocalDir.Margin = New Padding(3, 0, 3, 0)
        chkLocalDir.Name = "chkLocalDir"
        chkLocalDir.Size = New Size(102, 30)
        chkLocalDir.TabIndex = 5
        chkLocalDir.Text = "用户目录 ✔"
        chkLocalDir.TextAlign = ContentAlignment.MiddleCenter
        ToolTip1.SetToolTip(chkLocalDir, "切换用户目录(Ctrl+U)")
        chkLocalDir.UseVisualStyleBackColor = True
        ' 
        ' lblPreset
        ' 
        lblPreset.AutoSize = True
        lblPreset.Cursor = Cursors.Hand
        lblPreset.Dock = DockStyle.Left
        lblPreset.Location = New Point(182, 3)
        lblPreset.Margin = New Padding(3, 3, 0, 3)
        lblPreset.Name = "lblPreset"
        lblPreset.Size = New Size(58, 24)
        lblPreset.TabIndex = 5
        lblPreset.Text = "预装 ✔"
        lblPreset.TextAlign = ContentAlignment.MiddleLeft
        ToolTip1.SetToolTip(lblPreset, "预装字体验证(Ctrl+Shift+S)")
        ' 
        ' tbPath
        ' 
        tbPath.Location = New Point(301, 0)
        tbPath.Margin = New Padding(3, 0, 3, 0)
        tbPath.Multiline = True
        tbPath.Name = "tbPath"
        tbPath.Size = New Size(29, 30)
        tbPath.TabIndex = 7
        tbPath.Visible = False
        ' 
        ' btnBrowse
        ' 
        btnBrowse.AllowDrop = True
        btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), Image)
        btnBrowse.Location = New Point(0, 0)
        btnBrowse.Margin = New Padding(0, 0, 3, 0)
        btnBrowse.Name = "btnBrowse"
        btnBrowse.Size = New Size(68, 30)
        btnBrowse.TabIndex = 1
        btnBrowse.Text = "浏览"
        btnBrowse.TextAlign = ContentAlignment.MiddleRight
        btnBrowse.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnBrowse, "浏览/拖放快捷方式(Ctrl+O)")
        btnBrowse.UseVisualStyleBackColor = True
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), Image)
        btnRefresh.Location = New Point(336, 0)
        btnRefresh.Margin = New Padding(3, 0, 3, 0)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(30, 30)
        btnRefresh.TabIndex = 2
        ToolTip1.SetToolTip(btnRefresh, "刷新(F5)")
        btnRefresh.UseVisualStyleBackColor = True
        ' 
        ' pnlFontInfo
        ' 
        pnlFontInfo.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        pnlFontInfo.BackColor = Color.White
        pnlFontInfo.BorderStyle = BorderStyle.FixedSingle
        pnlFontInfo.Controls.Add(tblFontInfo)
        pnlFontInfo.Location = New Point(278, 27)
        pnlFontInfo.Name = "pnlFontInfo"
        pnlFontInfo.Padding = New Padding(0, 1, 0, 1)
        pnlFontInfo.Size = New Size(222, 149)
        pnlFontInfo.TabIndex = 5
        ' 
        ' tblFontInfo
        ' 
        tblFontInfo.AutoSize = True
        tblFontInfo.ColumnCount = 2
        tblFontInfo.ColumnStyles.Add(New ColumnStyle())
        tblFontInfo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblFontInfo.Controls.Add(tagFontFamily, 0, 1)
        tblFontInfo.Controls.Add(tagFontType, 0, 2)
        tblFontInfo.Controls.Add(tagFontCoverage, 0, 3)
        tblFontInfo.Controls.Add(tagFontSize, 0, 4)
        tblFontInfo.Controls.Add(tagFontModify, 0, 5)
        tblFontInfo.Controls.Add(lblFontName, 1, 0)
        tblFontInfo.Controls.Add(lblFontFamily, 1, 1)
        tblFontInfo.Controls.Add(lblFontType, 1, 2)
        tblFontInfo.Controls.Add(lblFontCoverage, 1, 3)
        tblFontInfo.Controls.Add(lblFontSize, 1, 4)
        tblFontInfo.Controls.Add(lblFontModify, 1, 5)
        tblFontInfo.Controls.Add(tagFontName, 0, 0)
        tblFontInfo.Dock = DockStyle.Fill
        tblFontInfo.Location = New Point(0, 1)
        tblFontInfo.Name = "tblFontInfo"
        tblFontInfo.RowCount = 6
        tblFontInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tblFontInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tblFontInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tblFontInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tblFontInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tblFontInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 16.666666F))
        tblFontInfo.Size = New Size(220, 145)
        tblFontInfo.TabIndex = 0
        ' 
        ' tagFontFamily
        ' 
        tagFontFamily.AutoSize = True
        tagFontFamily.Dock = DockStyle.Left
        tagFontFamily.Location = New Point(3, 27)
        tagFontFamily.Margin = New Padding(3)
        tagFontFamily.Name = "tagFontFamily"
        tagFontFamily.Size = New Size(36, 18)
        tagFontFamily.TabIndex = 1
        tagFontFamily.Text = "族名"
        tagFontFamily.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagFontType
        ' 
        tagFontType.AutoSize = True
        tagFontType.Dock = DockStyle.Left
        tagFontType.Location = New Point(3, 51)
        tagFontType.Margin = New Padding(3)
        tagFontType.Name = "tagFontType"
        tagFontType.Size = New Size(36, 18)
        tagFontType.TabIndex = 2
        tagFontType.Text = "类型"
        tagFontType.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagFontCoverage
        ' 
        tagFontCoverage.AutoSize = True
        tagFontCoverage.Dock = DockStyle.Left
        tagFontCoverage.Location = New Point(3, 75)
        tagFontCoverage.Margin = New Padding(3)
        tagFontCoverage.Name = "tagFontCoverage"
        tagFontCoverage.Size = New Size(36, 18)
        tagFontCoverage.TabIndex = 3
        tagFontCoverage.Text = "语言"
        tagFontCoverage.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagFontSize
        ' 
        tagFontSize.AutoSize = True
        tagFontSize.Dock = DockStyle.Left
        tagFontSize.Location = New Point(3, 99)
        tagFontSize.Margin = New Padding(3)
        tagFontSize.Name = "tagFontSize"
        tagFontSize.Size = New Size(36, 18)
        tagFontSize.TabIndex = 4
        tagFontSize.Text = "大小"
        tagFontSize.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagFontModify
        ' 
        tagFontModify.AutoSize = True
        tagFontModify.Dock = DockStyle.Left
        tagFontModify.Location = New Point(3, 123)
        tagFontModify.Margin = New Padding(3)
        tagFontModify.Name = "tagFontModify"
        tagFontModify.Size = New Size(36, 19)
        tagFontModify.TabIndex = 5
        tagFontModify.Text = "日期"
        tagFontModify.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblFontName
        ' 
        lblFontName.AutoEllipsis = True
        lblFontName.AutoSize = True
        lblFontName.Cursor = Cursors.Hand
        lblFontName.Location = New Point(45, 3)
        lblFontName.Margin = New Padding(3)
        lblFontName.Name = "lblFontName"
        lblFontName.Size = New Size(16, 18)
        lblFontName.TabIndex = 6
        lblFontName.Text = "-"
        lblFontName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblFontFamily
        ' 
        lblFontFamily.AutoEllipsis = True
        lblFontFamily.AutoSize = True
        lblFontFamily.Cursor = Cursors.Hand
        lblFontFamily.Location = New Point(45, 27)
        lblFontFamily.Margin = New Padding(3)
        lblFontFamily.Name = "lblFontFamily"
        lblFontFamily.Size = New Size(16, 18)
        lblFontFamily.TabIndex = 7
        lblFontFamily.Text = "-"
        lblFontFamily.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblFontType
        ' 
        lblFontType.AutoEllipsis = True
        lblFontType.AutoSize = True
        lblFontType.Location = New Point(45, 51)
        lblFontType.Margin = New Padding(3)
        lblFontType.Name = "lblFontType"
        lblFontType.Size = New Size(16, 18)
        lblFontType.TabIndex = 8
        lblFontType.Text = "-"
        lblFontType.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblFontCoverage
        ' 
        lblFontCoverage.AutoEllipsis = True
        lblFontCoverage.AutoSize = True
        lblFontCoverage.Location = New Point(45, 75)
        lblFontCoverage.Margin = New Padding(3)
        lblFontCoverage.Name = "lblFontCoverage"
        lblFontCoverage.Size = New Size(16, 18)
        lblFontCoverage.TabIndex = 9
        lblFontCoverage.Text = "-"
        lblFontCoverage.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblFontSize
        ' 
        lblFontSize.AutoEllipsis = True
        lblFontSize.AutoSize = True
        lblFontSize.Location = New Point(45, 99)
        lblFontSize.Margin = New Padding(3)
        lblFontSize.Name = "lblFontSize"
        lblFontSize.Size = New Size(16, 18)
        lblFontSize.TabIndex = 10
        lblFontSize.Text = "-"
        lblFontSize.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblFontModify
        ' 
        lblFontModify.AutoEllipsis = True
        lblFontModify.AutoSize = True
        lblFontModify.Location = New Point(45, 123)
        lblFontModify.Margin = New Padding(3)
        lblFontModify.Name = "lblFontModify"
        lblFontModify.Size = New Size(16, 18)
        lblFontModify.TabIndex = 11
        lblFontModify.Text = "-"
        lblFontModify.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagFontName
        ' 
        tagFontName.AutoSize = True
        tagFontName.Dock = DockStyle.Left
        tagFontName.Location = New Point(3, 3)
        tagFontName.Margin = New Padding(3)
        tagFontName.Name = "tagFontName"
        tagFontName.Size = New Size(36, 18)
        tagFontName.TabIndex = 0
        tagFontName.Text = "标题"
        tagFontName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TableLayoutPanel4
        ' 
        TableLayoutPanel4.AutoSize = True
        TableLayoutPanel4.ColumnCount = 4
        TableLayoutPanel4.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel4.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel4.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel4.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel4.Controls.Add(btnFontInstall, 0, 0)
        TableLayoutPanel4.Controls.Add(btnFontInfo, 3, 0)
        TableLayoutPanel4.Controls.Add(btnFontUninstall, 1, 0)
        TableLayoutPanel4.Location = New Point(278, 182)
        TableLayoutPanel4.Name = "TableLayoutPanel4"
        TableLayoutPanel4.RowCount = 1
        TableLayoutPanel4.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel4.Size = New Size(222, 30)
        TableLayoutPanel4.TabIndex = 3
        ' 
        ' btnFontInstall
        ' 
        btnFontInstall.AllowDrop = True
        btnFontInstall.Image = CType(resources.GetObject("btnFontInstall.Image"), Image)
        btnFontInstall.Location = New Point(0, 0)
        btnFontInstall.Margin = New Padding(0, 0, 3, 0)
        btnFontInstall.Name = "btnFontInstall"
        btnFontInstall.Size = New Size(68, 30)
        btnFontInstall.TabIndex = 2
        btnFontInstall.Text = "浏览"
        btnFontInstall.TextAlign = ContentAlignment.MiddleRight
        btnFontInstall.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnFontInstall, "安装/拖放字体(Ctrl+I)")
        btnFontInstall.UseVisualStyleBackColor = True
        ' 
        ' btnFontInfo
        ' 
        btnFontInfo.Location = New Point(170, 0)
        btnFontInfo.Margin = New Padding(3, 0, 0, 0)
        btnFontInfo.Name = "btnFontInfo"
        btnFontInfo.Size = New Size(52, 30)
        btnFontInfo.TabIndex = 5
        btnFontInfo.Text = "属性"
        ToolTip1.SetToolTip(btnFontInfo, "字体属性(Alt+Return)")
        btnFontInfo.UseVisualStyleBackColor = True
        ' 
        ' btnFontUninstall
        ' 
        btnFontUninstall.Location = New Point(74, 0)
        btnFontUninstall.Margin = New Padding(3, 0, 3, 0)
        btnFontUninstall.Name = "btnFontUninstall"
        btnFontUninstall.Size = New Size(52, 30)
        btnFontUninstall.TabIndex = 4
        btnFontUninstall.Text = "卸载"
        ToolTip1.SetToolTip(btnFontUninstall, "卸载字体(Delete)")
        btnFontUninstall.UseVisualStyleBackColor = True
        ' 
        ' lstDirFonts
        ' 
        lstDirFonts.DrawMode = DrawMode.OwnerDrawFixed
        lstDirFonts.ForeColor = Color.Black
        lstDirFonts.FormattingEnabled = True
        lstDirFonts.IntegralHeight = False
        lstDirFonts.ItemHeight = 25
        lstDirFonts.Location = New Point(12, 27)
        lstDirFonts.Name = "lstDirFonts"
        lstDirFonts.SelectionMode = SelectionMode.MultiExtended
        lstDirFonts.Size = New Size(260, 185)
        lstDirFonts.TabIndex = 4
        ' 
        ' splitDirectory
        ' 
        splitDirectory.ForeColor = Color.DarkGray
        splitDirectory.Location = New Point(12, 48)
        splitDirectory.Margin = New Padding(3)
        splitDirectory.Name = "splitDirectory"
        splitDirectory.Size = New Size(488, 18)
        splitDirectory.TabIndex = 2
        splitDirectory.Text = "当前目录  ───────────────────────────────────────────────────────────"
        splitDirectory.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblDirFonts
        ' 
        lblDirFonts.AutoEllipsis = True
        lblDirFonts.AutoSize = True
        lblDirFonts.Cursor = Cursors.Hand
        lblDirFonts.Location = New Point(0, 3)
        lblDirFonts.Margin = New Padding(0, 3, 3, 3)
        lblDirFonts.Name = "lblDirFonts"
        lblDirFonts.Size = New Size(92, 18)
        lblDirFonts.TabIndex = 0
        lblDirFonts.Text = "等待数据加载"
        lblDirFonts.TextAlign = ContentAlignment.MiddleLeft
        ToolTip1.SetToolTip(lblDirFonts, "查看未使用字体(Ctrl+Shift+U)")
        ' 
        ' lblLocalFonts
        ' 
        lblLocalFonts.AutoSize = True
        lblLocalFonts.Cursor = Cursors.Hand
        lblLocalFonts.Location = New Point(452, 3)
        lblLocalFonts.Margin = New Padding(0, 3, 0, 3)
        lblLocalFonts.Name = "lblLocalFonts"
        lblLocalFonts.Size = New Size(36, 18)
        lblLocalFonts.TabIndex = 1
        lblLocalFonts.Text = "用户"
        lblLocalFonts.TextAlign = ContentAlignment.MiddleLeft
        ToolTip1.SetToolTip(lblLocalFonts, "用户字体目录")
        ' 
        ' splitFont
        ' 
        splitFont.AutoSize = True
        splitFont.Location = New Point(436, 3)
        splitFont.Margin = New Padding(0, 3, 0, 3)
        splitFont.Name = "splitFont"
        splitFont.Size = New Size(16, 18)
        splitFont.TabIndex = 2
        splitFont.Text = "/"
        splitFont.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblSystemFonts
        ' 
        lblSystemFonts.AutoSize = True
        lblSystemFonts.Cursor = Cursors.Hand
        lblSystemFonts.Location = New Point(400, 3)
        lblSystemFonts.Margin = New Padding(3, 3, 0, 3)
        lblSystemFonts.Name = "lblSystemFonts"
        lblSystemFonts.Size = New Size(36, 18)
        lblSystemFonts.TabIndex = 3
        lblSystemFonts.Text = "系统"
        lblSystemFonts.TextAlign = ContentAlignment.MiddleLeft
        ToolTip1.SetToolTip(lblSystemFonts, "系统字体目录")
        ' 
        ' tblDirFonts
        ' 
        tblDirFonts.AutoSize = True
        tblDirFonts.ColumnCount = 4
        tblDirFonts.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblDirFonts.ColumnStyles.Add(New ColumnStyle())
        tblDirFonts.ColumnStyles.Add(New ColumnStyle())
        tblDirFonts.ColumnStyles.Add(New ColumnStyle())
        tblDirFonts.Controls.Add(lblDirFonts, 0, 0)
        tblDirFonts.Controls.Add(lblLocalFonts, 3, 0)
        tblDirFonts.Controls.Add(splitFont, 2, 0)
        tblDirFonts.Controls.Add(lblSystemFonts, 1, 0)
        tblDirFonts.Location = New Point(12, 72)
        tblDirFonts.Name = "tblDirFonts"
        tblDirFonts.RowCount = 1
        tblDirFonts.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblDirFonts.Size = New Size(488, 24)
        tblDirFonts.TabIndex = 4
        ' 
        ' splitCfg
        ' 
        splitCfg.ForeColor = Color.DarkGray
        splitCfg.Location = New Point(12, 296)
        splitCfg.Margin = New Padding(3, 6, 3, 3)
        splitCfg.Name = "splitCfg"
        splitCfg.Size = New Size(488, 18)
        splitCfg.TabIndex = 5
        splitCfg.Text = "当前配置  ────────────────────────────────────────────────────────────"
        splitCfg.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tblConfigFonts
        ' 
        tblConfigFonts.ColumnCount = 5
        tblConfigFonts.ColumnStyles.Add(New ColumnStyle())
        tblConfigFonts.ColumnStyles.Add(New ColumnStyle())
        tblConfigFonts.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblConfigFonts.ColumnStyles.Add(New ColumnStyle())
        tblConfigFonts.ColumnStyles.Add(New ColumnStyle())
        tblConfigFonts.Controls.Add(btnRecoveryLA, 4, 0)
        tblConfigFonts.Controls.Add(tagKR, 0, 3)
        tblConfigFonts.Controls.Add(pnlFallbackFonts, 0, 7)
        tblConfigFonts.Controls.Add(tagSC, 0, 4)
        tblConfigFonts.Controls.Add(tagTC, 0, 5)
        tblConfigFonts.Controls.Add(lblLA, 2, 0)
        tblConfigFonts.Controls.Add(lblTC, 2, 5)
        tblConfigFonts.Controls.Add(lblSC, 2, 4)
        tblConfigFonts.Controls.Add(lblKorean, 2, 3)
        tblConfigFonts.Controls.Add(lblJP, 2, 2)
        tblConfigFonts.Controls.Add(chkLanguage, 0, 1)
        tblConfigFonts.Controls.Add(chkFallback, 0, 6)
        tblConfigFonts.Controls.Add(cmbLA, 1, 0)
        tblConfigFonts.Controls.Add(cmbJP, 1, 2)
        tblConfigFonts.Controls.Add(cmbKR, 1, 3)
        tblConfigFonts.Controls.Add(cmbSC, 1, 4)
        tblConfigFonts.Controls.Add(cmbTC, 1, 5)
        tblConfigFonts.Controls.Add(btnRecoveryJP, 4, 2)
        tblConfigFonts.Controls.Add(btnFollowJP, 3, 2)
        tblConfigFonts.Controls.Add(btnFollowKR, 3, 3)
        tblConfigFonts.Controls.Add(btnRecoveryKR, 4, 3)
        tblConfigFonts.Controls.Add(btnFollowSC, 3, 4)
        tblConfigFonts.Controls.Add(btnRecoverySC, 4, 4)
        tblConfigFonts.Controls.Add(btnFollowTC, 3, 5)
        tblConfigFonts.Controls.Add(btnRecoveryTC, 4, 5)
        tblConfigFonts.Controls.Add(tagLA, 0, 0)
        tblConfigFonts.Controls.Add(tagJP, 0, 2)
        tblConfigFonts.Location = New Point(12, 320)
        tblConfigFonts.Margin = New Padding(3, 3, 3, 6)
        tblConfigFonts.Name = "tblConfigFonts"
        tblConfigFonts.RowCount = 8
        tblConfigFonts.RowStyles.Add(New RowStyle())
        tblConfigFonts.RowStyles.Add(New RowStyle())
        tblConfigFonts.RowStyles.Add(New RowStyle())
        tblConfigFonts.RowStyles.Add(New RowStyle())
        tblConfigFonts.RowStyles.Add(New RowStyle())
        tblConfigFonts.RowStyles.Add(New RowStyle())
        tblConfigFonts.RowStyles.Add(New RowStyle())
        tblConfigFonts.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tblConfigFonts.Size = New Size(488, 329)
        tblConfigFonts.TabIndex = 6
        ' 
        ' btnRecoveryLA
        ' 
        btnRecoveryLA.Image = CType(resources.GetObject("btnRecoveryLA.Image"), Image)
        btnRecoveryLA.Location = New Point(458, 1)
        btnRecoveryLA.Margin = New Padding(3, 1, 0, 0)
        btnRecoveryLA.Name = "btnRecoveryLA"
        btnRecoveryLA.Size = New Size(30, 30)
        btnRecoveryLA.TabIndex = 8
        ToolTip1.SetToolTip(btnRecoveryLA, "还原")
        btnRecoveryLA.UseVisualStyleBackColor = True
        ' 
        ' tagKR
        ' 
        tagKR.AutoSize = True
        tagKR.Dock = DockStyle.Left
        tagKR.Location = New Point(0, 92)
        tagKR.Margin = New Padding(0, 0, 3, 0)
        tagKR.Name = "tagKR"
        tagKR.Size = New Size(36, 32)
        tagKR.TabIndex = 2
        tagKR.Text = "韩文"
        tagKR.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' pnlFallbackFonts
        ' 
        tblConfigFonts.SetColumnSpan(pnlFallbackFonts, 5)
        pnlFallbackFonts.Controls.Add(lstFallback)
        pnlFallbackFonts.Controls.Add(pnlFallback)
        pnlFallbackFonts.Dock = DockStyle.Top
        pnlFallbackFonts.Location = New Point(0, 216)
        pnlFallbackFonts.Margin = New Padding(0)
        pnlFallbackFonts.Name = "pnlFallbackFonts"
        pnlFallbackFonts.Size = New Size(488, 101)
        pnlFallbackFonts.TabIndex = 30
        ' 
        ' lstFallback
        ' 
        lstFallback.DrawMode = DrawMode.OwnerDrawFixed
        lstFallback.ForeColor = Color.Black
        lstFallback.FormattingEnabled = True
        lstFallback.IntegralHeight = False
        lstFallback.ItemHeight = 25
        lstFallback.Location = New Point(0, 3)
        lstFallback.Name = "lstFallback"
        lstFallback.Size = New Size(260, 98)
        lstFallback.TabIndex = 7
        ' 
        ' pnlFallback
        ' 
        pnlFallback.Controls.Add(cmbFallback)
        pnlFallback.Controls.Add(btnUp)
        pnlFallback.Controls.Add(btnInsert)
        pnlFallback.Controls.Add(btnRecoveryFallback)
        pnlFallback.Controls.Add(btnRemove)
        pnlFallback.Controls.Add(btnDown)
        pnlFallback.Location = New Point(266, 3)
        pnlFallback.Name = "pnlFallback"
        pnlFallback.Size = New Size(222, 98)
        pnlFallback.TabIndex = 29
        ' 
        ' cmbFallback
        ' 
        cmbFallback.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbFallback.Dock = DockStyle.Top
        cmbFallback.ForeColor = Color.Black
        cmbFallback.FormattingEnabled = True
        cmbFallback.Location = New Point(0, 0)
        cmbFallback.Name = "cmbFallback"
        cmbFallback.Size = New Size(222, 26)
        cmbFallback.TabIndex = 24
        ' 
        ' btnUp
        ' 
        btnUp.Image = CType(resources.GetObject("btnUp.Image"), Image)
        btnUp.Location = New Point(0, 32)
        btnUp.Name = "btnUp"
        btnUp.Size = New Size(30, 30)
        btnUp.TabIndex = 27
        ToolTip1.SetToolTip(btnUp, "上移")
        btnUp.UseVisualStyleBackColor = True
        ' 
        ' btnInsert
        ' 
        btnInsert.Image = CType(resources.GetObject("btnInsert.Image"), Image)
        btnInsert.Location = New Point(120, 32)
        btnInsert.Name = "btnInsert"
        btnInsert.Size = New Size(30, 30)
        btnInsert.TabIndex = 26
        ToolTip1.SetToolTip(btnInsert, "插入")
        btnInsert.UseVisualStyleBackColor = True
        ' 
        ' btnRecoveryFallback
        ' 
        btnRecoveryFallback.Image = CType(resources.GetObject("btnRecoveryFallback.Image"), Image)
        btnRecoveryFallback.Location = New Point(192, 32)
        btnRecoveryFallback.Margin = New Padding(3, 3, 0, 3)
        btnRecoveryFallback.Name = "btnRecoveryFallback"
        btnRecoveryFallback.Size = New Size(30, 30)
        btnRecoveryFallback.TabIndex = 8
        ToolTip1.SetToolTip(btnRecoveryFallback, "还原")
        btnRecoveryFallback.UseVisualStyleBackColor = True
        ' 
        ' btnRemove
        ' 
        btnRemove.Image = CType(resources.GetObject("btnRemove.Image"), Image)
        btnRemove.Location = New Point(156, 32)
        btnRemove.Name = "btnRemove"
        btnRemove.Size = New Size(30, 30)
        btnRemove.TabIndex = 25
        ToolTip1.SetToolTip(btnRemove, "移除")
        btnRemove.UseVisualStyleBackColor = True
        ' 
        ' btnDown
        ' 
        btnDown.Image = CType(resources.GetObject("btnDown.Image"), Image)
        btnDown.Location = New Point(0, 68)
        btnDown.Margin = New Padding(3, 0, 3, 3)
        btnDown.Name = "btnDown"
        btnDown.Size = New Size(30, 30)
        btnDown.TabIndex = 28
        ToolTip1.SetToolTip(btnDown, "下移")
        btnDown.UseVisualStyleBackColor = True
        ' 
        ' tagSC
        ' 
        tagSC.AutoSize = True
        tagSC.Dock = DockStyle.Left
        tagSC.Location = New Point(0, 124)
        tagSC.Margin = New Padding(0, 0, 3, 0)
        tagSC.Name = "tagSC"
        tagSC.Size = New Size(64, 32)
        tagSC.TabIndex = 3
        tagSC.Text = "简体中文"
        tagSC.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagTC
        ' 
        tagTC.AutoSize = True
        tagTC.Dock = DockStyle.Left
        tagTC.Location = New Point(0, 156)
        tagTC.Margin = New Padding(0, 0, 3, 0)
        tagTC.Name = "tagTC"
        tagTC.Size = New Size(64, 32)
        tagTC.TabIndex = 4
        tagTC.Text = "繁体中文"
        tagTC.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblLA
        ' 
        lblLA.AutoEllipsis = True
        lblLA.AutoSize = True
        tblConfigFonts.SetColumnSpan(lblLA, 2)
        lblLA.Cursor = Cursors.Hand
        lblLA.Dock = DockStyle.Left
        lblLA.Location = New Point(266, 0)
        lblLA.Name = "lblLA"
        lblLA.Size = New Size(128, 32)
        lblLA.TabIndex = 5
        lblLA.Text = "Font demo text."
        lblLA.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblTC
        ' 
        lblTC.AutoEllipsis = True
        lblTC.AutoSize = True
        lblTC.Dock = DockStyle.Left
        lblTC.Location = New Point(266, 156)
        lblTC.Name = "lblTC"
        lblTC.Size = New Size(106, 32)
        lblTC.TabIndex = 9
        lblTC.Text = "字體範例文字。"
        lblTC.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblSC
        ' 
        lblSC.AutoEllipsis = True
        lblSC.AutoSize = True
        lblSC.Dock = DockStyle.Left
        lblSC.Location = New Point(266, 124)
        lblSC.Name = "lblSC"
        lblSC.Size = New Size(106, 32)
        lblSC.TabIndex = 8
        lblSC.Text = "字体示例文本。"
        lblSC.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblKorean
        ' 
        lblKorean.AutoEllipsis = True
        lblKorean.AutoSize = True
        lblKorean.Dock = DockStyle.Left
        lblKorean.Location = New Point(266, 92)
        lblKorean.Name = "lblKorean"
        lblKorean.Size = New Size(114, 32)
        lblKorean.TabIndex = 7
        lblKorean.Text = "글꼴예시텍스트."
        lblKorean.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblJP
        ' 
        lblJP.AutoEllipsis = True
        lblJP.AutoSize = True
        lblJP.Dock = DockStyle.Left
        lblJP.Location = New Point(266, 60)
        lblJP.Name = "lblJP"
        lblJP.Size = New Size(106, 32)
        lblJP.TabIndex = 6
        lblJP.Text = "フォントの例。"
        lblJP.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' chkLanguage
        ' 
        chkLanguage.AutoSize = True
        tblConfigFonts.SetColumnSpan(chkLanguage, 2)
        chkLanguage.Location = New Point(0, 35)
        chkLanguage.Margin = New Padding(0, 3, 3, 3)
        chkLanguage.Name = "chkLanguage"
        chkLanguage.Size = New Size(128, 22)
        chkLanguage.TabIndex = 10
        chkLanguage.Text = "修改多语言字体"
        chkLanguage.UseVisualStyleBackColor = True
        ' 
        ' chkFallback
        ' 
        chkFallback.AutoSize = True
        tblConfigFonts.SetColumnSpan(chkFallback, 2)
        chkFallback.Location = New Point(0, 191)
        chkFallback.Margin = New Padding(0, 3, 3, 3)
        chkFallback.Name = "chkFallback"
        chkFallback.Size = New Size(114, 22)
        chkFallback.TabIndex = 11
        chkFallback.Text = "修改回退字体"
        chkFallback.UseVisualStyleBackColor = True
        ' 
        ' cmbLA
        ' 
        cmbLA.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbLA.Dock = DockStyle.Left
        cmbLA.ForeColor = Color.Black
        cmbLA.FormattingEnabled = True
        cmbLA.Location = New Point(70, 4)
        cmbLA.Margin = New Padding(3, 4, 3, 2)
        cmbLA.Name = "cmbLA"
        cmbLA.Size = New Size(190, 26)
        cmbLA.TabIndex = 12
        ' 
        ' cmbJP
        ' 
        cmbJP.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbJP.Dock = DockStyle.Left
        cmbJP.ForeColor = Color.Black
        cmbJP.FormattingEnabled = True
        cmbJP.Location = New Point(70, 64)
        cmbJP.Margin = New Padding(3, 4, 3, 2)
        cmbJP.Name = "cmbJP"
        cmbJP.Size = New Size(190, 26)
        cmbJP.TabIndex = 13
        ' 
        ' cmbKR
        ' 
        cmbKR.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbKR.Dock = DockStyle.Left
        cmbKR.ForeColor = Color.Black
        cmbKR.FormattingEnabled = True
        cmbKR.Location = New Point(70, 96)
        cmbKR.Margin = New Padding(3, 4, 3, 2)
        cmbKR.Name = "cmbKR"
        cmbKR.Size = New Size(190, 26)
        cmbKR.TabIndex = 14
        ' 
        ' cmbSC
        ' 
        cmbSC.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbSC.Dock = DockStyle.Left
        cmbSC.ForeColor = Color.Black
        cmbSC.FormattingEnabled = True
        cmbSC.Location = New Point(70, 128)
        cmbSC.Margin = New Padding(3, 4, 3, 2)
        cmbSC.Name = "cmbSC"
        cmbSC.Size = New Size(190, 26)
        cmbSC.TabIndex = 15
        ' 
        ' cmbTC
        ' 
        cmbTC.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbTC.Dock = DockStyle.Left
        cmbTC.ForeColor = Color.Black
        cmbTC.FormattingEnabled = True
        cmbTC.Location = New Point(70, 160)
        cmbTC.Margin = New Padding(3, 4, 3, 2)
        cmbTC.Name = "cmbTC"
        cmbTC.Size = New Size(190, 26)
        cmbTC.TabIndex = 16
        ' 
        ' btnRecoveryJP
        ' 
        btnRecoveryJP.Image = CType(resources.GetObject("btnRecoveryJP.Image"), Image)
        btnRecoveryJP.Location = New Point(458, 61)
        btnRecoveryJP.Margin = New Padding(3, 1, 0, 0)
        btnRecoveryJP.Name = "btnRecoveryJP"
        btnRecoveryJP.Size = New Size(30, 30)
        btnRecoveryJP.TabIndex = 7
        ToolTip1.SetToolTip(btnRecoveryJP, "还原")
        btnRecoveryJP.UseVisualStyleBackColor = True
        ' 
        ' btnFollowJP
        ' 
        btnFollowJP.Image = CType(resources.GetObject("btnFollowJP.Image"), Image)
        btnFollowJP.Location = New Point(422, 61)
        btnFollowJP.Margin = New Padding(3, 1, 3, 0)
        btnFollowJP.Name = "btnFollowJP"
        btnFollowJP.Size = New Size(30, 30)
        btnFollowJP.TabIndex = 17
        ToolTip1.SetToolTip(btnFollowJP, "跟随")
        btnFollowJP.UseVisualStyleBackColor = True
        ' 
        ' btnFollowKR
        ' 
        btnFollowKR.Image = CType(resources.GetObject("btnFollowKR.Image"), Image)
        btnFollowKR.Location = New Point(422, 93)
        btnFollowKR.Margin = New Padding(3, 1, 3, 0)
        btnFollowKR.Name = "btnFollowKR"
        btnFollowKR.Size = New Size(30, 30)
        btnFollowKR.TabIndex = 18
        ToolTip1.SetToolTip(btnFollowKR, "跟随")
        btnFollowKR.UseVisualStyleBackColor = True
        ' 
        ' btnRecoveryKR
        ' 
        btnRecoveryKR.Image = CType(resources.GetObject("btnRecoveryKR.Image"), Image)
        btnRecoveryKR.Location = New Point(458, 93)
        btnRecoveryKR.Margin = New Padding(3, 1, 0, 0)
        btnRecoveryKR.Name = "btnRecoveryKR"
        btnRecoveryKR.Size = New Size(30, 30)
        btnRecoveryKR.TabIndex = 19
        ToolTip1.SetToolTip(btnRecoveryKR, "还原")
        btnRecoveryKR.UseVisualStyleBackColor = True
        ' 
        ' btnFollowSC
        ' 
        btnFollowSC.Image = CType(resources.GetObject("btnFollowSC.Image"), Image)
        btnFollowSC.Location = New Point(422, 125)
        btnFollowSC.Margin = New Padding(3, 1, 3, 0)
        btnFollowSC.Name = "btnFollowSC"
        btnFollowSC.Size = New Size(30, 30)
        btnFollowSC.TabIndex = 20
        ToolTip1.SetToolTip(btnFollowSC, "跟随")
        btnFollowSC.UseVisualStyleBackColor = True
        ' 
        ' btnRecoverySC
        ' 
        btnRecoverySC.Image = CType(resources.GetObject("btnRecoverySC.Image"), Image)
        btnRecoverySC.Location = New Point(458, 125)
        btnRecoverySC.Margin = New Padding(3, 1, 0, 0)
        btnRecoverySC.Name = "btnRecoverySC"
        btnRecoverySC.Size = New Size(30, 30)
        btnRecoverySC.TabIndex = 21
        ToolTip1.SetToolTip(btnRecoverySC, "还原")
        btnRecoverySC.UseVisualStyleBackColor = True
        ' 
        ' btnFollowTC
        ' 
        btnFollowTC.Image = CType(resources.GetObject("btnFollowTC.Image"), Image)
        btnFollowTC.Location = New Point(422, 157)
        btnFollowTC.Margin = New Padding(3, 1, 3, 0)
        btnFollowTC.Name = "btnFollowTC"
        btnFollowTC.Size = New Size(30, 30)
        btnFollowTC.TabIndex = 22
        ToolTip1.SetToolTip(btnFollowTC, "跟随")
        btnFollowTC.UseVisualStyleBackColor = True
        ' 
        ' btnRecoveryTC
        ' 
        btnRecoveryTC.Image = CType(resources.GetObject("btnRecoveryTC.Image"), Image)
        btnRecoveryTC.Location = New Point(458, 157)
        btnRecoveryTC.Margin = New Padding(3, 1, 0, 0)
        btnRecoveryTC.Name = "btnRecoveryTC"
        btnRecoveryTC.Size = New Size(30, 30)
        btnRecoveryTC.TabIndex = 23
        ToolTip1.SetToolTip(btnRecoveryTC, "还原")
        btnRecoveryTC.UseVisualStyleBackColor = True
        ' 
        ' tagLA
        ' 
        tagLA.AutoSize = True
        tagLA.Dock = DockStyle.Left
        tagLA.Location = New Point(0, 0)
        tagLA.Margin = New Padding(0, 0, 3, 0)
        tagLA.Name = "tagLA"
        tagLA.Size = New Size(64, 32)
        tagLA.TabIndex = 0
        tagLA.Text = "默认西文"
        tagLA.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagJP
        ' 
        tagJP.AutoSize = True
        tagJP.Dock = DockStyle.Left
        tagJP.Location = New Point(0, 60)
        tagJP.Margin = New Padding(0, 0, 3, 0)
        tagJP.Name = "tagJP"
        tagJP.Size = New Size(36, 32)
        tagJP.TabIndex = 1
        tagJP.Text = "日文"
        tagJP.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tblBottonBar
        ' 
        tblBottonBar.ColumnCount = 9
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.Controls.Add(btnMenu, 0, 0)
        tblBottonBar.Controls.Add(btnApply, 8, 0)
        tblBottonBar.Controls.Add(btnPreview, 1, 0)
        tblBottonBar.Controls.Add(ProgressBar1, 5, 0)
        tblBottonBar.Controls.Add(btnBlade, 6, 0)
        tblBottonBar.Controls.Add(btnOutput, 7, 0)
        tblBottonBar.Controls.Add(btnDefault, 4, 0)
        tblBottonBar.Location = New Point(12, 661)
        tblBottonBar.Name = "tblBottonBar"
        tblBottonBar.RowCount = 1
        tblBottonBar.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblBottonBar.Size = New Size(488, 30)
        tblBottonBar.TabIndex = 7
        ' 
        ' btnMenu
        ' 
        btnMenu.Image = CType(resources.GetObject("btnMenu.Image"), Image)
        btnMenu.Location = New Point(0, 0)
        btnMenu.Margin = New Padding(0, 0, 3, 0)
        btnMenu.Name = "btnMenu"
        btnMenu.Size = New Size(30, 30)
        btnMenu.TabIndex = 1
        ToolTip1.SetToolTip(btnMenu, "菜单(Application)")
        btnMenu.UseVisualStyleBackColor = True
        ' 
        ' btnApply
        ' 
        btnApply.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnApply.Location = New Point(436, 0)
        btnApply.Margin = New Padding(3, 0, 0, 0)
        btnApply.Name = "btnApply"
        btnApply.Size = New Size(52, 30)
        btnApply.TabIndex = 5
        btnApply.Text = "应用"
        ToolTip1.SetToolTip(btnApply, "应用修改(Ctrl+S)" & vbCrLf & "应用修改并启动(Shift+点击)")
        btnApply.UseVisualStyleBackColor = True
        ' 
        ' btnPreview
        ' 
        btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), Image)
        btnPreview.Location = New Point(36, 0)
        btnPreview.Margin = New Padding(3, 0, 3, 0)
        btnPreview.Name = "btnPreview"
        btnPreview.Size = New Size(30, 30)
        btnPreview.TabIndex = 2
        ToolTip1.SetToolTip(btnPreview, "预览(Ctrl+P)")
        btnPreview.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Dock = DockStyle.Fill
        ProgressBar1.Location = New Point(108, 3)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(203, 24)
        ProgressBar1.TabIndex = 9
        ProgressBar1.Visible = False
        ' 
        ' btnBlade
        ' 
        btnBlade.AllowDrop = True
        btnBlade.Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        btnBlade.Location = New Point(317, 0)
        btnBlade.Margin = New Padding(3, 0, 3, 0)
        btnBlade.Name = "btnBlade"
        btnBlade.Padding = New Padding(3, 0, 3, 0)
        btnBlade.Size = New Size(52, 30)
        btnBlade.TabIndex = 8
        btnBlade.Text = "安装"
        ToolTip1.SetToolTip(btnBlade, "安装字体包或配置(Ctrl+B)")
        btnBlade.UseVisualStyleBackColor = True
        ' 
        ' btnOutput
        ' 
        btnOutput.AutoSize = True
        btnOutput.Location = New Point(375, 0)
        btnOutput.Margin = New Padding(3, 0, 3, 0)
        btnOutput.Name = "btnOutput"
        btnOutput.Padding = New Padding(3, 0, 3, 0)
        btnOutput.Size = New Size(55, 30)
        btnOutput.TabIndex = 6
        btnOutput.Text = "导出"
        ToolTip1.SetToolTip(btnOutput, "保存字体包或配置(Ctrl+J)")
        btnOutput.UseVisualStyleBackColor = True
        ' 
        ' btnDefault
        ' 
        btnDefault.Image = CType(resources.GetObject("btnDefault.Image"), Image)
        btnDefault.Location = New Point(72, 0)
        btnDefault.Margin = New Padding(3, 0, 3, 0)
        btnDefault.Name = "btnDefault"
        btnDefault.Size = New Size(30, 30)
        btnDefault.TabIndex = 3
        btnDefault.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnDefault, "恢复默认(F8)")
        btnDefault.UseVisualStyleBackColor = True
        ' 
        ' pnlDirFonts
        ' 
        pnlDirFonts.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlDirFonts.Controls.Add(lstDirFonts)
        pnlDirFonts.Controls.Add(pnlFontInfo)
        pnlDirFonts.Controls.Add(TableLayoutPanel4)
        pnlDirFonts.Location = New Point(0, 75)
        pnlDirFonts.Name = "pnlDirFonts"
        pnlDirFonts.Size = New Size(518, 225)
        pnlDirFonts.TabIndex = 8
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "preview.ico")
        ImageList1.Images.SetKeyName(1, "search_context.ico")
        ImageList1.Images.SetKeyName(2, "blade.ico")
        ImageList1.Images.SetKeyName(3, "output.ico")
        ImageList1.Images.SetKeyName(4, "backup.ico")
        ImageList1.Images.SetKeyName(5, "help.ico")
        ' 
        ' FormMain
        ' 
        AllowDrop = True
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(512, 703)
        Controls.Add(tblBottonBar)
        Controls.Add(tblConfigFonts)
        Controls.Add(splitCfg)
        Controls.Add(tblDirFonts)
        Controls.Add(splitDirectory)
        Controls.Add(tblTopBar)
        Controls.Add(pnlDirFonts)
        DoubleBuffered = True
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "FormMain"
        StartPosition = FormStartPosition.CenterScreen
        Text = " "
        tblTopBar.ResumeLayout(False)
        tblTopBar.PerformLayout()
        pnlFontInfo.ResumeLayout(False)
        pnlFontInfo.PerformLayout()
        tblFontInfo.ResumeLayout(False)
        tblFontInfo.PerformLayout()
        TableLayoutPanel4.ResumeLayout(False)
        tblDirFonts.ResumeLayout(False)
        tblDirFonts.PerformLayout()
        tblConfigFonts.ResumeLayout(False)
        tblConfigFonts.PerformLayout()
        pnlFallbackFonts.ResumeLayout(False)
        pnlFallback.ResumeLayout(False)
        tblBottonBar.ResumeLayout(False)
        tblBottonBar.PerformLayout()
        pnlDirFonts.ResumeLayout(False)
        pnlDirFonts.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tblTopBar As TableLayoutPanel
    Friend WithEvents btnBrowse As Button
    Friend WithEvents lblCfg As Label
    Friend WithEvents btnLocate As Button
    Friend WithEvents chkLocalDir As CheckBox
    Friend WithEvents btnRefresh As Button
    Friend WithEvents lblPreset As Label
    Friend WithEvents splitDirectory As Label
    Friend WithEvents pnlFontInfo As Panel
    Friend WithEvents tblFontInfo As TableLayoutPanel
    Friend WithEvents tagFontFamily As Label
    Friend WithEvents tagFontType As Label
    Friend WithEvents tagFontCoverage As Label
    Friend WithEvents tagFontSize As Label
    Friend WithEvents tagFontModify As Label
    Friend WithEvents lblFontName As Label
    Friend WithEvents lblFontFamily As Label
    Friend WithEvents lblFontType As Label
    Friend WithEvents lblFontCoverage As Label
    Friend WithEvents lblFontSize As Label
    Friend WithEvents lblFontModify As Label
    Friend WithEvents tagFontName As Label
    Friend WithEvents lblLocalFonts As Label
    Friend WithEvents splitFont As Label
    Friend WithEvents lblSystemFonts As Label
    Friend WithEvents lblDirFonts As Label
    Friend WithEvents lstDirFonts As ListBox
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents btnFontInstall As Button
    Friend WithEvents btnFontInfo As Button
    Friend WithEvents btnFontUninstall As Button
    Friend WithEvents tblDirFonts As TableLayoutPanel
    Friend WithEvents splitCfg As Label
    Friend WithEvents tblConfigFonts As TableLayoutPanel
    Friend WithEvents tagLA As Label
    Friend WithEvents tagJP As Label
    Friend WithEvents tagKR As Label
    Friend WithEvents tagSC As Label
    Friend WithEvents tagTC As Label
    Friend WithEvents lblLA As Label
    Friend WithEvents lblTC As Label
    Friend WithEvents lblSC As Label
    Friend WithEvents lblKorean As Label
    Friend WithEvents lblJP As Label
    Friend WithEvents chkLanguage As CheckBox
    Friend WithEvents chkFallback As CheckBox
    Friend WithEvents cmbLA As ComboBox
    Friend WithEvents cmbJP As ComboBox
    Friend WithEvents cmbKR As ComboBox
    Friend WithEvents cmbSC As ComboBox
    Friend WithEvents cmbTC As ComboBox
    Friend WithEvents lstFallback As ListBox
    Friend WithEvents btnRecoveryLA As Button
    Friend WithEvents btnRecoveryJP As Button
    Friend WithEvents btnFollowJP As Button
    Friend WithEvents btnFollowKR As Button
    Friend WithEvents btnRecoveryKR As Button
    Friend WithEvents btnFollowSC As Button
    Friend WithEvents btnRecoverySC As Button
    Friend WithEvents btnFollowTC As Button
    Friend WithEvents btnRecoveryTC As Button
    Friend WithEvents btnRecoveryFallback As Button
    Friend WithEvents cmbFallback As ComboBox
    Friend WithEvents btnRemove As Button
    Friend WithEvents btnDown As Button
    Friend WithEvents btnUp As Button
    Friend WithEvents btnInsert As Button
    Friend WithEvents tblBottonBar As TableLayoutPanel
    Friend WithEvents btnDefault As Button
    Friend WithEvents btnPreview As Button
    Friend WithEvents btnMenu As Button
    Friend WithEvents btnApply As Button
    Friend WithEvents btnOutput As Button
    Friend WithEvents btnBlade As Button
    Friend WithEvents tbPath As TextBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents pnlFallback As Panel
    Friend WithEvents pnlFallbackFonts As Panel
    Friend WithEvents pnlDirFonts As Panel
    Friend WithEvents btnSearch As Button
    Friend WithEvents ImageList1 As ImageList

End Class
