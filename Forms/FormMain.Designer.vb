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
        lblConfig = New Label()
        btnLocate = New Button()
        chkDirSelect = New CheckBox()
        lblPreset = New Label()
        btnBrowse = New Button()
        btnRefresh = New Button()
        tbPath = New TextBox()
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
        lstbDirFonts = New ListBox()
        splitDirectory = New Label()
        lblDirFonts = New Label()
        lblLocalFonts = New Label()
        splitFont = New Label()
        lblSystemFonts = New Label()
        tblDirFonts = New TableLayoutPanel()
        splitConfig = New Label()
        tblConfigFonts = New TableLayoutPanel()
        btnRecoveryLA = New Button()
        tagKR = New Label()
        pnlFallbackFonts = New Panel()
        lstbFallbackFonts = New ListBox()
        pnlFallback = New Panel()
        cbFallbackFonts = New ComboBox()
        btnUp = New Button()
        btnInsert = New Button()
        btnRecoveryFallbackFonts = New Button()
        btnRemove = New Button()
        btnDown = New Button()
        tagSC = New Label()
        tagTC = New Label()
        lblLA = New Label()
        lblTC = New Label()
        lblSC = New Label()
        lblKorean = New Label()
        lblJP = New Label()
        chkLanguageVariants = New CheckBox()
        chkFallbackFonts = New CheckBox()
        cbLA = New ComboBox()
        cbJP = New ComboBox()
        cbKR = New ComboBox()
        cbSC = New ComboBox()
        cbTC = New ComboBox()
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
        btnPreview = New Button()
        btnMenu = New Button()
        btnApply = New Button()
        btnCopy = New Button()
        btnDefault = New Button()
        btnBlade = New Button()
        ProgressBar1 = New ProgressBar()
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
        tblTopBar.ColumnCount = 7
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.ColumnStyles.Add(New ColumnStyle())
        tblTopBar.Controls.Add(lblConfig, 3, 0)
        tblTopBar.Controls.Add(btnLocate, 6, 0)
        tblTopBar.Controls.Add(chkDirSelect, 1, 0)
        tblTopBar.Controls.Add(lblPreset, 2, 0)
        tblTopBar.Controls.Add(btnBrowse, 0, 0)
        tblTopBar.Controls.Add(btnRefresh, 5, 0)
        tblTopBar.Controls.Add(tbPath, 4, 0)
        tblTopBar.Location = New Point(12, 12)
        tblTopBar.Name = "tblTopBar"
        tblTopBar.RowCount = 1
        tblTopBar.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblTopBar.Size = New Size(488, 30)
        tblTopBar.TabIndex = 0
        ' 
        ' lblConfig
        ' 
        lblConfig.AutoSize = True
        lblConfig.Cursor = Cursors.Hand
        lblConfig.Dock = DockStyle.Left
        lblConfig.Location = New Point(266, 3)
        lblConfig.Margin = New Padding(3)
        lblConfig.Name = "lblConfig"
        lblConfig.Size = New Size(58, 24)
        lblConfig.TabIndex = 6
        lblConfig.Text = "配置 ✔"
        lblConfig.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnLocate
        ' 
        btnLocate.Location = New Point(408, 0)
        btnLocate.Margin = New Padding(3, 0, 0, 0)
        btnLocate.Name = "btnLocate"
        btnLocate.Size = New Size(80, 30)
        btnLocate.TabIndex = 3
        btnLocate.Text = "字体目录"
        btnLocate.UseVisualStyleBackColor = True
        ' 
        ' chkDirSelect
        ' 
        chkDirSelect.Appearance = Appearance.Button
        chkDirSelect.Location = New Point(94, 0)
        chkDirSelect.Margin = New Padding(3, 0, 3, 0)
        chkDirSelect.Name = "chkDirSelect"
        chkDirSelect.Size = New Size(102, 30)
        chkDirSelect.TabIndex = 5
        chkDirSelect.Text = "用户目录 ✔"
        chkDirSelect.TextAlign = ContentAlignment.MiddleCenter
        chkDirSelect.UseVisualStyleBackColor = True
        ' 
        ' lblPreset
        ' 
        lblPreset.AutoSize = True
        lblPreset.Cursor = Cursors.Hand
        lblPreset.Dock = DockStyle.Left
        lblPreset.Location = New Point(202, 3)
        lblPreset.Margin = New Padding(3)
        lblPreset.Name = "lblPreset"
        lblPreset.Size = New Size(58, 24)
        lblPreset.TabIndex = 5
        lblPreset.Text = "预装 ✔"
        lblPreset.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnBrowse
        ' 
        btnBrowse.AllowDrop = True
        btnBrowse.Location = New Point(0, 0)
        btnBrowse.Margin = New Padding(0, 0, 3, 0)
        btnBrowse.Name = "btnBrowse"
        btnBrowse.Size = New Size(88, 30)
        btnBrowse.TabIndex = 1
        btnBrowse.Text = "浏览/拖放"
        btnBrowse.UseVisualStyleBackColor = True
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), Image)
        btnRefresh.Location = New Point(372, 0)
        btnRefresh.Margin = New Padding(3, 0, 3, 0)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(30, 30)
        btnRefresh.TabIndex = 2
        ToolTip1.SetToolTip(btnRefresh, "刷新")
        btnRefresh.UseVisualStyleBackColor = True
        ' 
        ' tbPath
        ' 
        tbPath.Location = New Point(330, 3)
        tbPath.Name = "tbPath"
        tbPath.Size = New Size(36, 26)
        tbPath.TabIndex = 7
        tbPath.Visible = False
        ' 
        ' pnlFontInfo
        ' 
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
        btnFontInstall.Location = New Point(0, 0)
        btnFontInstall.Margin = New Padding(0, 0, 3, 0)
        btnFontInstall.Name = "btnFontInstall"
        btnFontInstall.Size = New Size(88, 30)
        btnFontInstall.TabIndex = 2
        btnFontInstall.Text = "安装/拖放"
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
        btnFontInfo.UseVisualStyleBackColor = True
        ' 
        ' btnFontUninstall
        ' 
        btnFontUninstall.Location = New Point(94, 0)
        btnFontUninstall.Margin = New Padding(3, 0, 3, 0)
        btnFontUninstall.Name = "btnFontUninstall"
        btnFontUninstall.Size = New Size(52, 30)
        btnFontUninstall.TabIndex = 4
        btnFontUninstall.Text = "卸载"
        btnFontUninstall.UseVisualStyleBackColor = True
        ' 
        ' lstbDirFonts
        ' 
        lstbDirFonts.DrawMode = DrawMode.OwnerDrawFixed
        lstbDirFonts.FormattingEnabled = True
        lstbDirFonts.IntegralHeight = False
        lstbDirFonts.ItemHeight = 25
        lstbDirFonts.Location = New Point(12, 27)
        lstbDirFonts.Name = "lstbDirFonts"
        lstbDirFonts.SelectionMode = SelectionMode.MultiExtended
        lstbDirFonts.Size = New Size(260, 185)
        lstbDirFonts.TabIndex = 4
        ' 
        ' splitDirectory
        ' 
        splitDirectory.ForeColor = Color.Silver
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
        lblDirFonts.Size = New Size(140, 18)
        lblDirFonts.TabIndex = 0
        lblDirFonts.Text = "等待数据加载......"
        lblDirFonts.TextAlign = ContentAlignment.MiddleLeft
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
        ' 
        ' splitFont
        ' 
        splitFont.AutoSize = True
        splitFont.Location = New Point(436, 3)
        splitFont.Margin = New Padding(0, 3, 0, 3)
        splitFont.Name = "splitFont"
        splitFont.Size = New Size(16, 18)
        splitFont.TabIndex = 2
        splitFont.Text = "|"
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
        ' splitConfig
        ' 
        splitConfig.ForeColor = Color.Silver
        splitConfig.Location = New Point(12, 293)
        splitConfig.Margin = New Padding(3)
        splitConfig.Name = "splitConfig"
        splitConfig.Size = New Size(488, 18)
        splitConfig.TabIndex = 5
        splitConfig.Text = "当前配置  ────────────────────────────────────────────────────────────"
        splitConfig.TextAlign = ContentAlignment.MiddleLeft
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
        tblConfigFonts.Controls.Add(chkLanguageVariants, 0, 1)
        tblConfigFonts.Controls.Add(chkFallbackFonts, 0, 6)
        tblConfigFonts.Controls.Add(cbLA, 1, 0)
        tblConfigFonts.Controls.Add(cbJP, 1, 2)
        tblConfigFonts.Controls.Add(cbKR, 1, 3)
        tblConfigFonts.Controls.Add(cbSC, 1, 4)
        tblConfigFonts.Controls.Add(cbTC, 1, 5)
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
        tblConfigFonts.Location = New Point(12, 317)
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
        tblConfigFonts.Size = New Size(488, 335)
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
        pnlFallbackFonts.Controls.Add(lstbFallbackFonts)
        pnlFallbackFonts.Controls.Add(pnlFallback)
        pnlFallbackFonts.Dock = DockStyle.Top
        pnlFallbackFonts.Location = New Point(0, 216)
        pnlFallbackFonts.Margin = New Padding(0)
        pnlFallbackFonts.Name = "pnlFallbackFonts"
        pnlFallbackFonts.Size = New Size(488, 117)
        pnlFallbackFonts.TabIndex = 30
        ' 
        ' lstbFallbackFonts
        ' 
        lstbFallbackFonts.DrawMode = DrawMode.OwnerDrawFixed
        lstbFallbackFonts.FormattingEnabled = True
        lstbFallbackFonts.IntegralHeight = False
        lstbFallbackFonts.ItemHeight = 25
        lstbFallbackFonts.Location = New Point(0, 3)
        lstbFallbackFonts.Name = "lstbFallbackFonts"
        lstbFallbackFonts.Size = New Size(260, 98)
        lstbFallbackFonts.TabIndex = 7
        ' 
        ' pnlFallback
        ' 
        pnlFallback.Controls.Add(cbFallbackFonts)
        pnlFallback.Controls.Add(btnUp)
        pnlFallback.Controls.Add(btnInsert)
        pnlFallback.Controls.Add(btnRecoveryFallbackFonts)
        pnlFallback.Controls.Add(btnRemove)
        pnlFallback.Controls.Add(btnDown)
        pnlFallback.Location = New Point(266, 3)
        pnlFallback.Name = "pnlFallback"
        pnlFallback.Size = New Size(222, 98)
        pnlFallback.TabIndex = 29
        ' 
        ' cbFallbackFonts
        ' 
        cbFallbackFonts.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cbFallbackFonts.Dock = DockStyle.Top
        cbFallbackFonts.FormattingEnabled = True
        cbFallbackFonts.Location = New Point(0, 0)
        cbFallbackFonts.Name = "cbFallbackFonts"
        cbFallbackFonts.Size = New Size(222, 26)
        cbFallbackFonts.TabIndex = 24
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
        ' btnRecoveryFallbackFonts
        ' 
        btnRecoveryFallbackFonts.Image = CType(resources.GetObject("btnRecoveryFallbackFonts.Image"), Image)
        btnRecoveryFallbackFonts.Location = New Point(192, 32)
        btnRecoveryFallbackFonts.Margin = New Padding(3, 3, 0, 3)
        btnRecoveryFallbackFonts.Name = "btnRecoveryFallbackFonts"
        btnRecoveryFallbackFonts.Size = New Size(30, 30)
        btnRecoveryFallbackFonts.TabIndex = 8
        ToolTip1.SetToolTip(btnRecoveryFallbackFonts, "还原")
        btnRecoveryFallbackFonts.UseVisualStyleBackColor = True
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
        ' chkLanguageVariants
        ' 
        chkLanguageVariants.AutoSize = True
        tblConfigFonts.SetColumnSpan(chkLanguageVariants, 2)
        chkLanguageVariants.Location = New Point(0, 35)
        chkLanguageVariants.Margin = New Padding(0, 3, 3, 3)
        chkLanguageVariants.Name = "chkLanguageVariants"
        chkLanguageVariants.Size = New Size(128, 22)
        chkLanguageVariants.TabIndex = 10
        chkLanguageVariants.Text = "修改多语言字体"
        chkLanguageVariants.UseVisualStyleBackColor = True
        ' 
        ' chkFallbackFonts
        ' 
        chkFallbackFonts.AutoSize = True
        tblConfigFonts.SetColumnSpan(chkFallbackFonts, 2)
        chkFallbackFonts.Location = New Point(0, 191)
        chkFallbackFonts.Margin = New Padding(0, 3, 3, 3)
        chkFallbackFonts.Name = "chkFallbackFonts"
        chkFallbackFonts.Size = New Size(114, 22)
        chkFallbackFonts.TabIndex = 11
        chkFallbackFonts.Text = "修改回退字体"
        chkFallbackFonts.UseVisualStyleBackColor = True
        ' 
        ' cbLA
        ' 
        cbLA.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cbLA.Dock = DockStyle.Left
        cbLA.FormattingEnabled = True
        cbLA.Location = New Point(70, 4)
        cbLA.Margin = New Padding(3, 4, 3, 2)
        cbLA.Name = "cbLA"
        cbLA.Size = New Size(190, 26)
        cbLA.TabIndex = 12
        ' 
        ' cbJP
        ' 
        cbJP.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cbJP.Dock = DockStyle.Left
        cbJP.FormattingEnabled = True
        cbJP.Location = New Point(70, 64)
        cbJP.Margin = New Padding(3, 4, 3, 2)
        cbJP.Name = "cbJP"
        cbJP.Size = New Size(190, 26)
        cbJP.TabIndex = 13
        ' 
        ' cbKR
        ' 
        cbKR.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cbKR.Dock = DockStyle.Left
        cbKR.FormattingEnabled = True
        cbKR.Location = New Point(70, 96)
        cbKR.Margin = New Padding(3, 4, 3, 2)
        cbKR.Name = "cbKR"
        cbKR.Size = New Size(190, 26)
        cbKR.TabIndex = 14
        ' 
        ' cbSC
        ' 
        cbSC.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cbSC.Dock = DockStyle.Left
        cbSC.FormattingEnabled = True
        cbSC.Location = New Point(70, 128)
        cbSC.Margin = New Padding(3, 4, 3, 2)
        cbSC.Name = "cbSC"
        cbSC.Size = New Size(190, 26)
        cbSC.TabIndex = 15
        ' 
        ' cbTC
        ' 
        cbTC.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cbTC.Dock = DockStyle.Left
        cbTC.FormattingEnabled = True
        cbTC.Location = New Point(70, 160)
        cbTC.Margin = New Padding(3, 4, 3, 2)
        cbTC.Name = "cbTC"
        cbTC.Size = New Size(190, 26)
        cbTC.TabIndex = 16
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
        tblBottonBar.ColumnCount = 7
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle())
        tblBottonBar.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        tblBottonBar.Controls.Add(btnPreview, 1, 0)
        tblBottonBar.Controls.Add(btnMenu, 0, 0)
        tblBottonBar.Controls.Add(btnApply, 6, 0)
        tblBottonBar.Controls.Add(btnCopy, 5, 0)
        tblBottonBar.Controls.Add(btnDefault, 2, 0)
        tblBottonBar.Controls.Add(btnBlade, 3, 0)
        tblBottonBar.Controls.Add(ProgressBar1, 4, 0)
        tblBottonBar.Location = New Point(12, 661)
        tblBottonBar.Name = "tblBottonBar"
        tblBottonBar.RowCount = 1
        tblBottonBar.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblBottonBar.Size = New Size(488, 30)
        tblBottonBar.TabIndex = 7
        ' 
        ' btnPreview
        ' 
        btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), Image)
        btnPreview.Location = New Point(36, 0)
        btnPreview.Margin = New Padding(3, 0, 3, 0)
        btnPreview.Name = "btnPreview"
        btnPreview.Size = New Size(30, 30)
        btnPreview.TabIndex = 2
        ToolTip1.SetToolTip(btnPreview, "预览")
        btnPreview.UseVisualStyleBackColor = True
        ' 
        ' btnMenu
        ' 
        btnMenu.Image = CType(resources.GetObject("btnMenu.Image"), Image)
        btnMenu.Location = New Point(0, 0)
        btnMenu.Margin = New Padding(0, 0, 3, 0)
        btnMenu.Name = "btnMenu"
        btnMenu.Size = New Size(30, 30)
        btnMenu.TabIndex = 1
        ToolTip1.SetToolTip(btnMenu, "功能")
        btnMenu.UseVisualStyleBackColor = True
        ' 
        ' btnApply
        ' 
        btnApply.Location = New Point(436, 0)
        btnApply.Margin = New Padding(3, 0, 0, 0)
        btnApply.Name = "btnApply"
        btnApply.Size = New Size(52, 30)
        btnApply.TabIndex = 5
        btnApply.Text = "应用"
        btnApply.UseVisualStyleBackColor = True
        ' 
        ' btnCopy
        ' 
        btnCopy.Location = New Point(356, 0)
        btnCopy.Margin = New Padding(3, 0, 3, 0)
        btnCopy.Name = "btnCopy"
        btnCopy.Size = New Size(74, 30)
        btnCopy.TabIndex = 6
        btnCopy.Text = "导出配置"
        btnCopy.UseVisualStyleBackColor = True
        ' 
        ' btnDefault
        ' 
        btnDefault.Image = CType(resources.GetObject("btnDefault.Image"), Image)
        btnDefault.Location = New Point(72, 0)
        btnDefault.Margin = New Padding(3, 0, 3, 0)
        btnDefault.Name = "btnDefault"
        btnDefault.Size = New Size(30, 30)
        btnDefault.TabIndex = 3
        ToolTip1.SetToolTip(btnDefault, "恢复默认")
        btnDefault.UseVisualStyleBackColor = True
        ' 
        ' btnBlade
        ' 
        btnBlade.AllowDrop = True
        btnBlade.Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Italic, GraphicsUnit.Point, CByte(134))
        btnBlade.Location = New Point(108, 0)
        btnBlade.Margin = New Padding(3, 0, 3, 0)
        btnBlade.Name = "btnBlade"
        btnBlade.Padding = New Padding(3, 0, 3, 0)
        btnBlade.Size = New Size(64, 30)
        btnBlade.TabIndex = 8
        btnBlade.Text = "Blade"
        ToolTip1.SetToolTip(btnBlade, "导入字体包或配置")
        btnBlade.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(178, 3)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(172, 24)
        ProgressBar1.TabIndex = 9
        ProgressBar1.Visible = False
        ' 
        ' pnlDirFonts
        ' 
        pnlDirFonts.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlDirFonts.Controls.Add(lstbDirFonts)
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
        ImageList1.Images.SetKeyName(0, "preview_16.ico")
        ImageList1.Images.SetKeyName(1, "blade_c.ico")
        ImageList1.Images.SetKeyName(2, "options_16.ico")
        ImageList1.Images.SetKeyName(3, "help_c.ico")
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
        Controls.Add(splitConfig)
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
        Text = "DDNet ForeEdit"
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
        pnlDirFonts.ResumeLayout(False)
        pnlDirFonts.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tblTopBar As TableLayoutPanel
    Friend WithEvents btnBrowse As Button
    Friend WithEvents lblConfig As Label
    Friend WithEvents btnLocate As Button
    Friend WithEvents chkDirSelect As CheckBox
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
    Friend WithEvents lstbDirFonts As ListBox
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents btnFontInstall As Button
    Friend WithEvents btnFontInfo As Button
    Friend WithEvents btnFontUninstall As Button
    Friend WithEvents tblDirFonts As TableLayoutPanel
    Friend WithEvents splitConfig As Label
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
    Friend WithEvents chkLanguageVariants As CheckBox
    Friend WithEvents chkFallbackFonts As CheckBox
    Friend WithEvents cbLA As ComboBox
    Friend WithEvents cbJP As ComboBox
    Friend WithEvents cbKR As ComboBox
    Friend WithEvents cbSC As ComboBox
    Friend WithEvents cbTC As ComboBox
    Friend WithEvents lstbFallbackFonts As ListBox
    Friend WithEvents btnRecoveryLA As Button
    Friend WithEvents btnRecoveryJP As Button
    Friend WithEvents btnFollowJP As Button
    Friend WithEvents btnFollowKR As Button
    Friend WithEvents btnRecoveryKR As Button
    Friend WithEvents btnFollowSC As Button
    Friend WithEvents btnRecoverySC As Button
    Friend WithEvents btnFollowTC As Button
    Friend WithEvents btnRecoveryTC As Button
    Friend WithEvents btnRecoveryFallbackFonts As Button
    Friend WithEvents cbFallbackFonts As ComboBox
    Friend WithEvents btnRemove As Button
    Friend WithEvents btnDown As Button
    Friend WithEvents btnUp As Button
    Friend WithEvents btnInsert As Button
    Friend WithEvents tblBottonBar As TableLayoutPanel
    Friend WithEvents btnDefault As Button
    Friend WithEvents btnPreview As Button
    Friend WithEvents btnMenu As Button
    Friend WithEvents btnApply As Button
    Friend WithEvents btnCopy As Button
    Friend WithEvents btnBlade As Button
    Friend WithEvents tbPath As TextBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents pnlFallback As Panel
    Friend WithEvents pnlFallbackFonts As Panel
    Friend WithEvents pnlDirFonts As Panel
    Friend WithEvents ImageList1 As ImageList

End Class
