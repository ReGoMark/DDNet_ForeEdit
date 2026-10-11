<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBackup
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormBackup))
        btnBackup = New Button()
        btnCancel = New Button()
        TableLayoutPanel1 = New TableLayoutPanel()
        lblSizeTotal = New Label()
        lblCountSettings = New Label()
        chkVideos = New CheckBox()
        chkSaves = New CheckBox()
        chkGhosts = New CheckBox()
        chkDemos = New CheckBox()
        chkAssets = New CheckBox()
        lblSizeSettings = New Label()
        lblSizeCfg = New Label()
        lblSizeSaves = New Label()
        lblSizeDemos = New Label()
        lblSizeVideos = New Label()
        lblSizeAssets = New Label()
        lblSizeGhosts = New Label()
        chkSettings = New CheckBox()
        chkCfg = New CheckBox()
        lblCountCfg = New Label()
        lblCountSaves = New Label()
        lblCountDemos = New Label()
        lblCountVideos = New Label()
        lblCountAssets = New Label()
        lblCountGhosts = New Label()
        splitExport = New Label()
        btnSelectAll = New Button()
        tblExport = New TableLayoutPanel()
        btnSnapshot = New Button()
        ProgressBar2 = New ProgressBar()
        ToolTip1 = New ToolTip(components)
        TableLayoutPanel1.SuspendLayout()
        tblExport.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnBackup
        ' 
        btnBackup.Location = New Point(278, 0)
        btnBackup.Margin = New Padding(3, 0, 0, 0)
        btnBackup.Name = "btnBackup"
        btnBackup.Padding = New Padding(3, 0, 3, 0)
        btnBackup.Size = New Size(80, 30)
        btnBackup.TabIndex = 8
        btnBackup.Text = "保存备份"
        btnBackup.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(351, 260)
        btnCancel.Margin = New Padding(3, 0, 0, 0)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(52, 30)
        btnCancel.TabIndex = 9
        btnCancel.Text = "取消"
        btnCancel.UseVisualStyleBackColor = True
        btnCancel.Visible = False
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 3
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.Controls.Add(lblSizeTotal, 0, 14)
        TableLayoutPanel1.Controls.Add(lblCountSettings, 1, 0)
        TableLayoutPanel1.Controls.Add(chkVideos, 0, 6)
        TableLayoutPanel1.Controls.Add(chkSaves, 0, 2)
        TableLayoutPanel1.Controls.Add(chkGhosts, 0, 12)
        TableLayoutPanel1.Controls.Add(chkDemos, 0, 4)
        TableLayoutPanel1.Controls.Add(chkAssets, 0, 9)
        TableLayoutPanel1.Controls.Add(lblSizeSettings, 2, 0)
        TableLayoutPanel1.Controls.Add(lblSizeCfg, 2, 1)
        TableLayoutPanel1.Controls.Add(lblSizeSaves, 2, 2)
        TableLayoutPanel1.Controls.Add(lblSizeDemos, 2, 4)
        TableLayoutPanel1.Controls.Add(lblSizeVideos, 2, 6)
        TableLayoutPanel1.Controls.Add(lblSizeAssets, 2, 9)
        TableLayoutPanel1.Controls.Add(lblSizeGhosts, 2, 12)
        TableLayoutPanel1.Controls.Add(chkSettings, 0, 0)
        TableLayoutPanel1.Controls.Add(chkCfg, 0, 1)
        TableLayoutPanel1.Controls.Add(lblCountCfg, 1, 1)
        TableLayoutPanel1.Controls.Add(lblCountSaves, 1, 2)
        TableLayoutPanel1.Controls.Add(lblCountDemos, 1, 4)
        TableLayoutPanel1.Controls.Add(lblCountVideos, 1, 6)
        TableLayoutPanel1.Controls.Add(lblCountAssets, 1, 9)
        TableLayoutPanel1.Controls.Add(lblCountGhosts, 1, 12)
        TableLayoutPanel1.Controls.Add(splitExport, 0, 13)
        TableLayoutPanel1.Location = New Point(12, 12)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 16
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Size = New Size(358, 260)
        TableLayoutPanel1.TabIndex = 10
        ' 
        ' lblSizeTotal
        ' 
        lblSizeTotal.AutoSize = True
        TableLayoutPanel1.SetColumnSpan(lblSizeTotal, 3)
        lblSizeTotal.ForeColor = Color.DarkGray
        lblSizeTotal.Location = New Point(0, 218)
        lblSizeTotal.Margin = New Padding(0, 3, 0, 3)
        lblSizeTotal.Name = "lblSizeTotal"
        lblSizeTotal.Size = New Size(72, 18)
        lblSizeTotal.TabIndex = 70
        lblSizeTotal.Text = "备份大小:"
        ' 
        ' lblCountSettings
        ' 
        lblCountSettings.AutoSize = True
        lblCountSettings.Dock = DockStyle.Right
        lblCountSettings.Location = New Point(280, 0)
        lblCountSettings.Margin = New Padding(3, 0, 3, 3)
        lblCountSettings.Name = "lblCountSettings"
        lblCountSettings.Size = New Size(16, 22)
        lblCountSettings.TabIndex = 62
        lblCountSettings.Text = "-"
        lblCountSettings.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' chkVideos
        ' 
        chkVideos.AutoSize = True
        chkVideos.Dock = DockStyle.Left
        chkVideos.Location = New Point(0, 112)
        chkVideos.Margin = New Padding(0, 3, 3, 3)
        chkVideos.Name = "chkVideos"
        chkVideos.Size = New Size(170, 22)
        chkVideos.TabIndex = 51
        chkVideos.Text = "渲染 (...\videos\)"
        ToolTip1.SetToolTip(chkVideos, "videos 文件夹")
        chkVideos.UseVisualStyleBackColor = True
        ' 
        ' chkSaves
        ' 
        chkSaves.AutoSize = True
        chkSaves.Dock = DockStyle.Left
        chkSaves.Location = New Point(0, 56)
        chkSaves.Margin = New Padding(0, 3, 3, 3)
        chkSaves.Name = "chkSaves"
        chkSaves.Size = New Size(182, 22)
        chkSaves.TabIndex = 9
        chkSaves.Text = "存档密码 (saves.txt)"
        ToolTip1.SetToolTip(chkSaves, "ddnet-saves.txt")
        chkSaves.UseVisualStyleBackColor = True
        ' 
        ' chkGhosts
        ' 
        chkGhosts.AutoSize = True
        chkGhosts.Dock = DockStyle.Left
        chkGhosts.Location = New Point(0, 168)
        chkGhosts.Margin = New Padding(0, 3, 3, 3)
        chkGhosts.Name = "chkGhosts"
        chkGhosts.Size = New Size(170, 22)
        chkGhosts.TabIndex = 13
        chkGhosts.Text = "影子 (...\ghosts\)"
        ToolTip1.SetToolTip(chkGhosts, "ghosts 文件夹")
        chkGhosts.UseVisualStyleBackColor = True
        ' 
        ' chkDemos
        ' 
        chkDemos.AutoSize = True
        chkDemos.Dock = DockStyle.Left
        chkDemos.Location = New Point(0, 84)
        chkDemos.Margin = New Padding(0, 3, 3, 3)
        chkDemos.Name = "chkDemos"
        chkDemos.Size = New Size(162, 22)
        chkDemos.TabIndex = 12
        chkDemos.Text = "回放 (...\demos\)"
        ToolTip1.SetToolTip(chkDemos, "demos 文件夹")
        chkDemos.UseVisualStyleBackColor = True
        ' 
        ' chkAssets
        ' 
        chkAssets.AutoSize = True
        chkAssets.Dock = DockStyle.Left
        chkAssets.Location = New Point(0, 140)
        chkAssets.Margin = New Padding(0, 3, 3, 3)
        chkAssets.Name = "chkAssets"
        chkAssets.Size = New Size(170, 22)
        chkAssets.TabIndex = 11
        chkAssets.Text = "材质 (...\assets\)"
        ToolTip1.SetToolTip(chkAssets, "assets 文件夹")
        chkAssets.UseVisualStyleBackColor = True
        ' 
        ' lblSizeSettings
        ' 
        lblSizeSettings.AutoSize = True
        lblSizeSettings.Dock = DockStyle.Right
        lblSizeSettings.Location = New Point(302, 0)
        lblSizeSettings.Margin = New Padding(3, 0, 0, 3)
        lblSizeSettings.Name = "lblSizeSettings"
        lblSizeSettings.Size = New Size(56, 22)
        lblSizeSettings.TabIndex = 52
        lblSizeSettings.Text = "Label1"
        lblSizeSettings.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSizeCfg
        ' 
        lblSizeCfg.AutoSize = True
        lblSizeCfg.Dock = DockStyle.Right
        lblSizeCfg.Location = New Point(302, 28)
        lblSizeCfg.Margin = New Padding(3, 3, 0, 3)
        lblSizeCfg.Name = "lblSizeCfg"
        lblSizeCfg.Size = New Size(56, 22)
        lblSizeCfg.TabIndex = 53
        lblSizeCfg.Text = "Label2"
        lblSizeCfg.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSizeSaves
        ' 
        lblSizeSaves.AutoSize = True
        lblSizeSaves.Dock = DockStyle.Right
        lblSizeSaves.Location = New Point(302, 56)
        lblSizeSaves.Margin = New Padding(3, 3, 0, 3)
        lblSizeSaves.Name = "lblSizeSaves"
        lblSizeSaves.Size = New Size(56, 22)
        lblSizeSaves.TabIndex = 54
        lblSizeSaves.Text = "Label3"
        lblSizeSaves.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSizeDemos
        ' 
        lblSizeDemos.AutoSize = True
        lblSizeDemos.Dock = DockStyle.Right
        lblSizeDemos.Location = New Point(302, 84)
        lblSizeDemos.Margin = New Padding(3, 3, 0, 3)
        lblSizeDemos.Name = "lblSizeDemos"
        lblSizeDemos.Size = New Size(56, 22)
        lblSizeDemos.TabIndex = 55
        lblSizeDemos.Text = "Label4"
        lblSizeDemos.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSizeVideos
        ' 
        lblSizeVideos.AutoSize = True
        lblSizeVideos.Dock = DockStyle.Right
        lblSizeVideos.Location = New Point(302, 112)
        lblSizeVideos.Margin = New Padding(3, 3, 0, 3)
        lblSizeVideos.Name = "lblSizeVideos"
        lblSizeVideos.Size = New Size(56, 22)
        lblSizeVideos.TabIndex = 56
        lblSizeVideos.Text = "Label5"
        lblSizeVideos.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSizeAssets
        ' 
        lblSizeAssets.AutoSize = True
        lblSizeAssets.Dock = DockStyle.Right
        lblSizeAssets.Location = New Point(302, 140)
        lblSizeAssets.Margin = New Padding(3, 3, 0, 3)
        lblSizeAssets.Name = "lblSizeAssets"
        lblSizeAssets.Size = New Size(56, 22)
        lblSizeAssets.TabIndex = 57
        lblSizeAssets.Text = "Label6"
        lblSizeAssets.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSizeGhosts
        ' 
        lblSizeGhosts.AutoSize = True
        lblSizeGhosts.Dock = DockStyle.Right
        lblSizeGhosts.Location = New Point(302, 168)
        lblSizeGhosts.Margin = New Padding(3, 3, 0, 3)
        lblSizeGhosts.Name = "lblSizeGhosts"
        lblSizeGhosts.Size = New Size(56, 22)
        lblSizeGhosts.TabIndex = 58
        lblSizeGhosts.Text = "Label7"
        lblSizeGhosts.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' chkSettings
        ' 
        chkSettings.AutoSize = True
        chkSettings.Dock = DockStyle.Left
        chkSettings.Location = New Point(0, 0)
        chkSettings.Margin = New Padding(0, 0, 3, 3)
        chkSettings.Name = "chkSettings"
        chkSettings.Size = New Size(206, 22)
        chkSettings.TabIndex = 10
        chkSettings.Text = "设置文件 (settings.cfg)"
        ToolTip1.SetToolTip(chkSettings, "settings_ddnet.cfg")
        chkSettings.UseVisualStyleBackColor = True
        ' 
        ' chkCfg
        ' 
        chkCfg.AutoSize = True
        chkCfg.Dock = DockStyle.Left
        chkCfg.Location = New Point(0, 28)
        chkCfg.Margin = New Padding(0, 3, 3, 3)
        chkCfg.Name = "chkCfg"
        chkCfg.Size = New Size(150, 22)
        chkCfg.TabIndex = 8
        chkCfg.Text = "配置文件 (*.cfg)"
        ToolTip1.SetToolTip(chkCfg, "遍历所有 .cfg 文件")
        chkCfg.UseVisualStyleBackColor = True
        ' 
        ' lblCountCfg
        ' 
        lblCountCfg.AutoSize = True
        lblCountCfg.Dock = DockStyle.Right
        lblCountCfg.Location = New Point(280, 28)
        lblCountCfg.Margin = New Padding(3)
        lblCountCfg.Name = "lblCountCfg"
        lblCountCfg.Size = New Size(16, 22)
        lblCountCfg.TabIndex = 63
        lblCountCfg.Text = "-"
        lblCountCfg.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCountSaves
        ' 
        lblCountSaves.AutoSize = True
        lblCountSaves.Dock = DockStyle.Right
        lblCountSaves.Location = New Point(280, 56)
        lblCountSaves.Margin = New Padding(3)
        lblCountSaves.Name = "lblCountSaves"
        lblCountSaves.Size = New Size(16, 22)
        lblCountSaves.TabIndex = 64
        lblCountSaves.Text = "-"
        lblCountSaves.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCountDemos
        ' 
        lblCountDemos.AutoSize = True
        lblCountDemos.Dock = DockStyle.Right
        lblCountDemos.Location = New Point(280, 84)
        lblCountDemos.Margin = New Padding(3)
        lblCountDemos.Name = "lblCountDemos"
        lblCountDemos.Size = New Size(16, 22)
        lblCountDemos.TabIndex = 65
        lblCountDemos.Text = "-"
        lblCountDemos.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCountVideos
        ' 
        lblCountVideos.AutoSize = True
        lblCountVideos.Dock = DockStyle.Right
        lblCountVideos.Location = New Point(280, 112)
        lblCountVideos.Margin = New Padding(3)
        lblCountVideos.Name = "lblCountVideos"
        lblCountVideos.Size = New Size(16, 22)
        lblCountVideos.TabIndex = 66
        lblCountVideos.Text = "-"
        lblCountVideos.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCountAssets
        ' 
        lblCountAssets.AutoSize = True
        lblCountAssets.Dock = DockStyle.Right
        lblCountAssets.Location = New Point(280, 140)
        lblCountAssets.Margin = New Padding(3)
        lblCountAssets.Name = "lblCountAssets"
        lblCountAssets.Size = New Size(16, 22)
        lblCountAssets.TabIndex = 67
        lblCountAssets.Text = "-"
        lblCountAssets.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCountGhosts
        ' 
        lblCountGhosts.AutoSize = True
        lblCountGhosts.Dock = DockStyle.Right
        lblCountGhosts.Location = New Point(280, 168)
        lblCountGhosts.Margin = New Padding(3)
        lblCountGhosts.Name = "lblCountGhosts"
        lblCountGhosts.Size = New Size(16, 22)
        lblCountGhosts.TabIndex = 68
        lblCountGhosts.Text = "-"
        lblCountGhosts.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' splitExport
        ' 
        TableLayoutPanel1.SetColumnSpan(splitExport, 3)
        splitExport.ForeColor = Color.DarkGray
        splitExport.Location = New Point(3, 196)
        splitExport.Margin = New Padding(3)
        splitExport.Name = "splitExport"
        splitExport.Size = New Size(352, 16)
        splitExport.TabIndex = 69
        splitExport.Text = "─────────────────────────────────────────────────────────"
        ' 
        ' btnSelectAll
        ' 
        btnSelectAll.AllowDrop = True
        btnSelectAll.Location = New Point(220, 0)
        btnSelectAll.Margin = New Padding(3, 0, 3, 0)
        btnSelectAll.Name = "btnSelectAll"
        btnSelectAll.Padding = New Padding(3, 0, 3, 0)
        btnSelectAll.Size = New Size(52, 30)
        btnSelectAll.TabIndex = 23
        btnSelectAll.Text = "全选"
        btnSelectAll.UseVisualStyleBackColor = True
        ' 
        ' tblExport
        ' 
        tblExport.BackColor = Color.Transparent
        tblExport.ColumnCount = 4
        tblExport.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblExport.ColumnStyles.Add(New ColumnStyle())
        tblExport.ColumnStyles.Add(New ColumnStyle())
        tblExport.ColumnStyles.Add(New ColumnStyle())
        tblExport.Controls.Add(btnSnapshot, 1, 0)
        tblExport.Controls.Add(btnSelectAll, 2, 0)
        tblExport.Controls.Add(ProgressBar2, 0, 0)
        tblExport.Controls.Add(btnBackup, 3, 0)
        tblExport.Location = New Point(12, 275)
        tblExport.Name = "tblExport"
        tblExport.RowCount = 1
        tblExport.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblExport.Size = New Size(358, 30)
        tblExport.TabIndex = 50
        ' 
        ' btnSnapshot
        ' 
        btnSnapshot.Location = New Point(162, 0)
        btnSnapshot.Margin = New Padding(3, 0, 3, 0)
        btnSnapshot.Name = "btnSnapshot"
        btnSnapshot.Padding = New Padding(3, 0, 3, 0)
        btnSnapshot.Size = New Size(52, 30)
        btnSnapshot.TabIndex = 10
        btnSnapshot.Text = "快照"
        ToolTip1.SetToolTip(btnSnapshot, "快速保存设置文件和存档密码")
        btnSnapshot.UseVisualStyleBackColor = True
        btnSnapshot.Visible = False
        ' 
        ' ProgressBar2
        ' 
        ProgressBar2.BackColor = Color.WhiteSmoke
        ProgressBar2.Dock = DockStyle.Fill
        ProgressBar2.Location = New Point(3, 3)
        ProgressBar2.Name = "ProgressBar2"
        ProgressBar2.Size = New Size(153, 24)
        ProgressBar2.TabIndex = 7
        ProgressBar2.Visible = False
        ' 
        ' FormBackup
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 317)
        Controls.Add(tblExport)
        Controls.Add(btnCancel)
        Controls.Add(TableLayoutPanel1)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "FormBackup"
        StartPosition = FormStartPosition.CenterParent
        Text = "备份 (实验性)"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        tblExport.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents btnBackup As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents chkCfg As CheckBox
    Friend WithEvents chkSaves As CheckBox
    Friend WithEvents chkSettings As CheckBox
    Friend WithEvents chkDemos As CheckBox
    Friend WithEvents tblExport As TableLayoutPanel
    Friend WithEvents ProgressBar2 As ProgressBar
    Friend WithEvents lblSizeSettings As Label
    Friend WithEvents lblSizeCfg As Label
    Friend WithEvents lblSizeSaves As Label
    Friend WithEvents lblSizeDemos As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents lblCountSettings As Label
    Friend WithEvents lblCountCfg As Label
    Friend WithEvents lblCountSaves As Label
    Friend WithEvents lblCountDemos As Label
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents chkVideos As CheckBox
    Friend WithEvents chkGhosts As CheckBox
    Friend WithEvents chkAssets As CheckBox
    Friend WithEvents lblSizeVideos As Label
    Friend WithEvents lblSizeAssets As Label
    Friend WithEvents lblSizeGhosts As Label
    Friend WithEvents lblCountVideos As Label
    Friend WithEvents lblCountAssets As Label
    Friend WithEvents lblCountGhosts As Label
    Friend WithEvents splitExport As Label
    Friend WithEvents lblSizeTotal As Label
    Friend WithEvents btnSnapshot As Button
End Class
