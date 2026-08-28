<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBlade
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormBlade))
        tblInstall = New TableLayoutPanel()
        btnInstall = New Button()
        btnOpen = New Button()
        Panel1 = New Panel()
        tblPackInfo = New TableLayoutPanel()
        lblZipNote = New Label()
        lblZipDate = New Label()
        tagZipNote = New Label()
        lblZipName = New Label()
        tagZipDate = New Label()
        tagZipName = New Label()
        tagZipType = New Label()
        tagZipSize = New Label()
        lblZipType = New Label()
        lblZipSize = New Label()
        tblPackList = New TableLayoutPanel()
        lstbBlade = New ListBox()
        btnZipBroswe = New Button()
        lblZip = New Label()
        splitInstall = New Label()
        tblExportMode = New TableLayoutPanel()
        tbNote = New TextBox()
        chkNote = New CheckBox()
        rbStandard = New RadioButton()
        rbFull = New RadioButton()
        lblNorm = New Label()
        lblStandard = New Label()
        lblFull = New Label()
        rbNorm = New RadioButton()
        tblExport = New TableLayoutPanel()
        btnExport = New Button()
        ProgressBar1 = New ProgressBar()
        splitExport = New Label()
        tblInstall.SuspendLayout()
        Panel1.SuspendLayout()
        tblPackInfo.SuspendLayout()
        tblPackList.SuspendLayout()
        tblExportMode.SuspendLayout()
        tblExport.SuspendLayout()
        SuspendLayout()
        ' 
        ' tblInstall
        ' 
        tblInstall.AutoSize = True
        tblInstall.BackColor = Color.Transparent
        tblInstall.ColumnCount = 2
        tblInstall.ColumnStyles.Add(New ColumnStyle())
        tblInstall.ColumnStyles.Add(New ColumnStyle())
        tblInstall.Controls.Add(btnInstall, 5, 0)
        tblInstall.Controls.Add(btnOpen, 0, 0)
        tblInstall.Location = New Point(404, 361)
        tblInstall.Margin = New Padding(3, 6, 3, 3)
        tblInstall.Name = "tblInstall"
        tblInstall.RowCount = 1
        tblInstall.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblInstall.Size = New Size(180, 30)
        tblInstall.TabIndex = 41
        ' 
        ' btnInstall
        ' 
        btnInstall.BackColor = Color.Transparent
        btnInstall.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnInstall.Location = New Point(58, 0)
        btnInstall.Margin = New Padding(3, 0, 0, 0)
        btnInstall.Name = "btnInstall"
        btnInstall.Padding = New Padding(3, 0, 0, 0)
        btnInstall.Size = New Size(122, 30)
        btnInstall.TabIndex = 25
        btnInstall.Text = "安装到当前目录"
        btnInstall.UseVisualStyleBackColor = False
        ' 
        ' btnOpen
        ' 
        btnOpen.BackColor = Color.Transparent
        btnOpen.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnOpen.Location = New Point(0, 0)
        btnOpen.Margin = New Padding(0, 0, 3, 0)
        btnOpen.Name = "btnOpen"
        btnOpen.Size = New Size(52, 30)
        btnOpen.TabIndex = 28
        btnOpen.Text = "打开"
        btnOpen.UseVisualStyleBackColor = False
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = SystemColors.Window
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(tblPackInfo)
        Panel1.ForeColor = SystemColors.InfoText
        Panel1.Location = New Point(304, 227)
        Panel1.Margin = New Padding(3, 3, 3, 6)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(280, 122)
        Panel1.TabIndex = 36
        ' 
        ' tblPackInfo
        ' 
        tblPackInfo.AutoSize = True
        tblPackInfo.ColumnCount = 2
        tblPackInfo.ColumnStyles.Add(New ColumnStyle())
        tblPackInfo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblPackInfo.Controls.Add(lblZipNote, 1, 4)
        tblPackInfo.Controls.Add(lblZipDate, 1, 3)
        tblPackInfo.Controls.Add(tagZipNote, 0, 4)
        tblPackInfo.Controls.Add(lblZipName, 1, 0)
        tblPackInfo.Controls.Add(tagZipDate, 0, 3)
        tblPackInfo.Controls.Add(tagZipName, 0, 0)
        tblPackInfo.Controls.Add(tagZipType, 0, 1)
        tblPackInfo.Controls.Add(tagZipSize, 0, 2)
        tblPackInfo.Controls.Add(lblZipType, 1, 1)
        tblPackInfo.Controls.Add(lblZipSize, 1, 2)
        tblPackInfo.Dock = DockStyle.Fill
        tblPackInfo.Location = New Point(0, 0)
        tblPackInfo.Margin = New Padding(0)
        tblPackInfo.Name = "tblPackInfo"
        tblPackInfo.RowCount = 5
        tblPackInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPackInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPackInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPackInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPackInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPackInfo.Size = New Size(278, 120)
        tblPackInfo.TabIndex = 2
        ' 
        ' lblZipNote
        ' 
        lblZipNote.AutoEllipsis = True
        lblZipNote.AutoSize = True
        lblZipNote.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZipNote.Location = New Point(45, 99)
        lblZipNote.Margin = New Padding(3)
        lblZipNote.Name = "lblZipNote"
        lblZipNote.Size = New Size(16, 18)
        lblZipNote.TabIndex = 21
        lblZipNote.Text = "-"
        lblZipNote.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblZipDate
        ' 
        lblZipDate.AutoEllipsis = True
        lblZipDate.AutoSize = True
        lblZipDate.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZipDate.Location = New Point(45, 75)
        lblZipDate.Margin = New Padding(3)
        lblZipDate.Name = "lblZipDate"
        lblZipDate.Size = New Size(16, 18)
        lblZipDate.TabIndex = 17
        lblZipDate.Text = "-"
        lblZipDate.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagZipNote
        ' 
        tagZipNote.AutoSize = True
        tagZipNote.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagZipNote.Location = New Point(3, 99)
        tagZipNote.Margin = New Padding(3)
        tagZipNote.Name = "tagZipNote"
        tagZipNote.Size = New Size(36, 18)
        tagZipNote.TabIndex = 14
        tagZipNote.Text = "备注"
        tagZipNote.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblZipName
        ' 
        lblZipName.AutoEllipsis = True
        lblZipName.AutoSize = True
        lblZipName.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZipName.Location = New Point(45, 3)
        lblZipName.Margin = New Padding(3)
        lblZipName.Name = "lblZipName"
        lblZipName.Size = New Size(16, 18)
        lblZipName.TabIndex = 15
        lblZipName.Text = "-"
        lblZipName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagZipDate
        ' 
        tagZipDate.AutoSize = True
        tagZipDate.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagZipDate.Location = New Point(3, 75)
        tagZipDate.Margin = New Padding(3)
        tagZipDate.Name = "tagZipDate"
        tagZipDate.Size = New Size(36, 18)
        tagZipDate.TabIndex = 13
        tagZipDate.Text = "日期"
        tagZipDate.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagZipName
        ' 
        tagZipName.AutoSize = True
        tagZipName.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagZipName.Location = New Point(3, 3)
        tagZipName.Margin = New Padding(3)
        tagZipName.Name = "tagZipName"
        tagZipName.Size = New Size(36, 18)
        tagZipName.TabIndex = 11
        tagZipName.Text = "名称"
        tagZipName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagZipType
        ' 
        tagZipType.AutoSize = True
        tagZipType.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagZipType.Location = New Point(3, 27)
        tagZipType.Margin = New Padding(3)
        tagZipType.Name = "tagZipType"
        tagZipType.Size = New Size(36, 18)
        tagZipType.TabIndex = 19
        tagZipType.Text = "类型"
        tagZipType.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagZipSize
        ' 
        tagZipSize.AutoSize = True
        tagZipSize.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagZipSize.Location = New Point(3, 51)
        tagZipSize.Margin = New Padding(3)
        tagZipSize.Name = "tagZipSize"
        tagZipSize.Size = New Size(36, 18)
        tagZipSize.TabIndex = 12
        tagZipSize.Text = "大小"
        tagZipSize.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblZipType
        ' 
        lblZipType.AutoEllipsis = True
        lblZipType.AutoSize = True
        lblZipType.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZipType.Location = New Point(45, 27)
        lblZipType.Margin = New Padding(3)
        lblZipType.Name = "lblZipType"
        lblZipType.Size = New Size(16, 18)
        lblZipType.TabIndex = 20
        lblZipType.Text = "-"
        lblZipType.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblZipSize
        ' 
        lblZipSize.AutoEllipsis = True
        lblZipSize.AutoSize = True
        lblZipSize.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZipSize.Location = New Point(45, 51)
        lblZipSize.Margin = New Padding(3)
        lblZipSize.Name = "lblZipSize"
        lblZipSize.Size = New Size(16, 18)
        lblZipSize.TabIndex = 16
        lblZipSize.Text = "-"
        lblZipSize.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tblPackList
        ' 
        tblPackList.ColumnCount = 2
        tblPackList.ColumnStyles.Add(New ColumnStyle())
        tblPackList.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblPackList.Controls.Add(lstbBlade, 0, 1)
        tblPackList.Controls.Add(btnZipBroswe, 0, 0)
        tblPackList.Controls.Add(lblZip, 1, 0)
        tblPackList.Location = New Point(304, 40)
        tblPackList.Name = "tblPackList"
        tblPackList.RowCount = 2
        tblPackList.RowStyles.Add(New RowStyle())
        tblPackList.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblPackList.Size = New Size(280, 181)
        tblPackList.TabIndex = 40
        ' 
        ' lstbBlade
        ' 
        tblPackList.SetColumnSpan(lstbBlade, 2)
        lstbBlade.Dock = DockStyle.Fill
        lstbBlade.DrawMode = DrawMode.OwnerDrawFixed
        lstbBlade.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lstbBlade.FormattingEnabled = True
        lstbBlade.IntegralHeight = False
        lstbBlade.ItemHeight = 25
        lstbBlade.Location = New Point(0, 36)
        lstbBlade.Margin = New Padding(0, 6, 0, 0)
        lstbBlade.Name = "lstbBlade"
        lstbBlade.Size = New Size(280, 145)
        lstbBlade.TabIndex = 10
        ' 
        ' btnZipBroswe
        ' 
        btnZipBroswe.AllowDrop = True
        btnZipBroswe.AutoSize = True
        btnZipBroswe.BackColor = Color.Transparent
        btnZipBroswe.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnZipBroswe.Location = New Point(0, 0)
        btnZipBroswe.Margin = New Padding(0, 0, 3, 0)
        btnZipBroswe.Name = "btnZipBroswe"
        btnZipBroswe.Size = New Size(85, 30)
        btnZipBroswe.TabIndex = 1
        btnZipBroswe.Text = "安装/拖放"
        btnZipBroswe.UseVisualStyleBackColor = False
        ' 
        ' lblZip
        ' 
        lblZip.AutoEllipsis = True
        lblZip.AutoSize = True
        lblZip.Dock = DockStyle.Right
        lblZip.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZip.Location = New Point(185, 3)
        lblZip.Margin = New Padding(3)
        lblZip.Name = "lblZip"
        lblZip.Size = New Size(92, 24)
        lblZip.TabIndex = 16
        lblZip.Text = "等待数据加载"
        lblZip.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' splitInstall
        ' 
        splitInstall.ForeColor = Color.DarkGray
        splitInstall.Location = New Point(304, 15)
        splitInstall.Margin = New Padding(6, 6, 3, 6)
        splitInstall.Name = "splitInstall"
        splitInstall.Size = New Size(280, 16)
        splitInstall.TabIndex = 39
        splitInstall.Text = "安装  ─────────────────────────────────"
        ' 
        ' tblExportMode
        ' 
        tblExportMode.ColumnCount = 2
        tblExportMode.ColumnStyles.Add(New ColumnStyle())
        tblExportMode.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblExportMode.Controls.Add(tbNote, 0, 4)
        tblExportMode.Controls.Add(chkNote, 0, 3)
        tblExportMode.Controls.Add(rbStandard, 0, 1)
        tblExportMode.Controls.Add(rbFull, 0, 2)
        tblExportMode.Controls.Add(lblNorm, 1, 0)
        tblExportMode.Controls.Add(lblStandard, 1, 1)
        tblExportMode.Controls.Add(lblFull, 1, 2)
        tblExportMode.Controls.Add(rbNorm, 0, 0)
        tblExportMode.Location = New Point(12, 40)
        tblExportMode.Margin = New Padding(3, 3, 3, 6)
        tblExportMode.Name = "tblExportMode"
        tblExportMode.RowCount = 5
        tblExportMode.RowStyles.Add(New RowStyle())
        tblExportMode.RowStyles.Add(New RowStyle())
        tblExportMode.RowStyles.Add(New RowStyle())
        tblExportMode.RowStyles.Add(New RowStyle())
        tblExportMode.RowStyles.Add(New RowStyle())
        tblExportMode.Size = New Size(280, 281)
        tblExportMode.TabIndex = 38
        ' 
        ' tbNote
        ' 
        tblExportMode.SetColumnSpan(tbNote, 2)
        tbNote.Dock = DockStyle.Top
        tbNote.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tbNote.Location = New Point(0, 184)
        tbNote.Margin = New Padding(0, 3, 0, 0)
        tbNote.Multiline = True
        tbNote.Name = "tbNote"
        tbNote.PlaceholderText = "最多 20 个中文字符"
        tbNote.Size = New Size(280, 78)
        tbNote.TabIndex = 10
        ' 
        ' chkNote
        ' 
        chkNote.AutoSize = True
        chkNote.Checked = True
        chkNote.CheckState = CheckState.Checked
        chkNote.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        chkNote.Location = New Point(0, 156)
        chkNote.Margin = New Padding(0, 6, 3, 3)
        chkNote.Name = "chkNote"
        chkNote.Size = New Size(86, 22)
        chkNote.TabIndex = 11
        chkNote.Text = "添加备注"
        chkNote.UseVisualStyleBackColor = True
        ' 
        ' rbStandard
        ' 
        rbStandard.Appearance = Appearance.Button
        rbStandard.AutoSize = True
        rbStandard.BackColor = Color.Transparent
        rbStandard.Dock = DockStyle.Fill
        rbStandard.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rbStandard.Location = New Point(0, 52)
        rbStandard.Margin = New Padding(0, 3, 3, 3)
        rbStandard.Name = "rbStandard"
        rbStandard.Padding = New Padding(3, 0, 3, 0)
        rbStandard.Size = New Size(138, 46)
        rbStandard.TabIndex = 5
        rbStandard.TabStop = True
        rbStandard.Text = "标准" & vbCrLf & "(也包含预装字体)"
        rbStandard.UseVisualStyleBackColor = False
        ' 
        ' rbFull
        ' 
        rbFull.Appearance = Appearance.Button
        rbFull.AutoSize = True
        rbFull.BackColor = Color.Transparent
        rbFull.Dock = DockStyle.Fill
        rbFull.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rbFull.Location = New Point(0, 104)
        rbFull.Margin = New Padding(0, 3, 3, 0)
        rbFull.Name = "rbFull"
        rbFull.Padding = New Padding(3, 0, 3, 0)
        rbFull.Size = New Size(138, 46)
        rbFull.TabIndex = 14
        rbFull.TabStop = True
        rbFull.Text = "完整" & vbCrLf & "(附加未使用字体)"
        rbFull.UseVisualStyleBackColor = False
        ' 
        ' lblNorm
        ' 
        lblNorm.AutoEllipsis = True
        lblNorm.AutoSize = True
        lblNorm.Dock = DockStyle.Left
        lblNorm.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblNorm.Location = New Point(144, 0)
        lblNorm.Margin = New Padding(3, 0, 0, 3)
        lblNorm.Name = "lblNorm"
        lblNorm.Size = New Size(40, 46)
        lblNorm.TabIndex = 12
        lblNorm.Text = "- MB"
        lblNorm.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblStandard
        ' 
        lblStandard.AutoEllipsis = True
        lblStandard.AutoSize = True
        lblStandard.Dock = DockStyle.Left
        lblStandard.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblStandard.Location = New Point(144, 52)
        lblStandard.Margin = New Padding(3, 3, 0, 3)
        lblStandard.Name = "lblStandard"
        lblStandard.Size = New Size(40, 46)
        lblStandard.TabIndex = 13
        lblStandard.Text = "- MB"
        lblStandard.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblFull
        ' 
        lblFull.AutoEllipsis = True
        lblFull.AutoSize = True
        lblFull.Dock = DockStyle.Left
        lblFull.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblFull.Location = New Point(144, 104)
        lblFull.Margin = New Padding(3, 3, 0, 0)
        lblFull.Name = "lblFull"
        lblFull.Size = New Size(40, 46)
        lblFull.TabIndex = 15
        lblFull.Text = "- MB"
        lblFull.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' rbNorm
        ' 
        rbNorm.Appearance = Appearance.Button
        rbNorm.AutoSize = True
        rbNorm.BackColor = Color.Transparent
        rbNorm.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rbNorm.Location = New Point(0, 0)
        rbNorm.Margin = New Padding(0, 0, 3, 3)
        rbNorm.Name = "rbNorm"
        rbNorm.Padding = New Padding(3, 0, 3, 0)
        rbNorm.Size = New Size(138, 46)
        rbNorm.TabIndex = 4
        rbNorm.TabStop = True
        rbNorm.Text = "默认" & vbCrLf & "(当前已使用字体)"
        rbNorm.UseVisualStyleBackColor = False
        ' 
        ' tblExport
        ' 
        tblExport.BackColor = Color.Transparent
        tblExport.ColumnCount = 3
        tblExport.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblExport.ColumnStyles.Add(New ColumnStyle())
        tblExport.ColumnStyles.Add(New ColumnStyle())
        tblExport.Controls.Add(btnExport, 2, 0)
        tblExport.Controls.Add(ProgressBar1, 0, 0)
        tblExport.Location = New Point(12, 361)
        tblExport.Name = "tblExport"
        tblExport.RowCount = 1
        tblExport.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblExport.Size = New Size(280, 30)
        tblExport.TabIndex = 37
        ' 
        ' btnExport
        ' 
        btnExport.AutoSize = True
        btnExport.BackColor = Color.Transparent
        btnExport.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnExport.Location = New Point(196, 0)
        btnExport.Margin = New Padding(3, 0, 0, 0)
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(84, 30)
        btnExport.TabIndex = 2
        btnExport.Text = "导出到..."
        btnExport.UseVisualStyleBackColor = False
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.BackColor = Color.WhiteSmoke
        ProgressBar1.Dock = DockStyle.Fill
        ProgressBar1.Location = New Point(3, 3)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(187, 24)
        ProgressBar1.TabIndex = 7
        ProgressBar1.Visible = False
        ' 
        ' splitExport
        ' 
        splitExport.ForeColor = Color.DarkGray
        splitExport.Location = New Point(12, 15)
        splitExport.Margin = New Padding(3, 6, 6, 6)
        splitExport.Name = "splitExport"
        splitExport.Size = New Size(280, 16)
        splitExport.TabIndex = 35
        splitExport.Text = "导出  ─────────────────────────────────"
        ' 
        ' FormBlade
        ' 
        AllowDrop = True
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(596, 403)
        Controls.Add(tblInstall)
        Controls.Add(Panel1)
        Controls.Add(tblPackList)
        Controls.Add(splitInstall)
        Controls.Add(tblExportMode)
        Controls.Add(tblExport)
        Controls.Add(splitExport)
        DoubleBuffered = True
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "FormBlade"
        StartPosition = FormStartPosition.CenterParent
        Text = "Blade"
        tblInstall.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        tblPackInfo.ResumeLayout(False)
        tblPackInfo.PerformLayout()
        tblPackList.ResumeLayout(False)
        tblPackList.PerformLayout()
        tblExportMode.ResumeLayout(False)
        tblExportMode.PerformLayout()
        tblExport.ResumeLayout(False)
        tblExport.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tblInstall As TableLayoutPanel
    Friend WithEvents btnInstall As Button
    Friend WithEvents btnOpen As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents tblPackInfo As TableLayoutPanel
    Friend WithEvents lblZipNote As Label
    Friend WithEvents lblZipDate As Label
    Friend WithEvents tagZipNote As Label
    Friend WithEvents lblZipName As Label
    Friend WithEvents tagZipDate As Label
    Friend WithEvents tagZipName As Label
    Friend WithEvents tagZipType As Label
    Friend WithEvents tagZipSize As Label
    Friend WithEvents lblZipType As Label
    Friend WithEvents lblZipSize As Label
    Friend WithEvents tblPackList As TableLayoutPanel
    Friend WithEvents lblZip As Label
    Friend WithEvents lstbBlade As ListBox
    Friend WithEvents btnZipBroswe As Button
    Friend WithEvents splitInstall As Label
    Friend WithEvents tblExportMode As TableLayoutPanel
    Friend WithEvents tbNote As TextBox
    Friend WithEvents rbNorm As RadioButton
    Friend WithEvents chkNote As CheckBox
    Friend WithEvents rbStandard As RadioButton
    Friend WithEvents rbFull As RadioButton
    Friend WithEvents lblNorm As Label
    Friend WithEvents lblStandard As Label
    Friend WithEvents lblFull As Label
    Friend WithEvents tblExport As TableLayoutPanel
    Friend WithEvents btnExport As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents splitExport As Label
End Class
