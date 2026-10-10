<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogBackup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogBackup))
        btnBackupTo = New Button()
        btnCancel = New Button()
        lblTitle = New Label()
        TableLayoutPanel1 = New TableLayoutPanel()
        lblSizeTotal = New Label()
        lblCount1 = New Label()
        chkVideos = New CheckBox()
        chkSaves = New CheckBox()
        chkGhosts = New CheckBox()
        chkDemos = New CheckBox()
        chkAssets = New CheckBox()
        lblSize1 = New Label()
        lblSize2 = New Label()
        lblSize3 = New Label()
        lblSize4 = New Label()
        lblSize5 = New Label()
        lblSize6 = New Label()
        lblSize7 = New Label()
        chkSettings = New CheckBox()
        chkCfg = New CheckBox()
        lblCount2 = New Label()
        lblCount3 = New Label()
        lblCount4 = New Label()
        lblCount5 = New Label()
        lblCount6 = New Label()
        lblCount7 = New Label()
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
        ' btnBackupTo
        ' 
        btnBackupTo.AutoSize = True
        btnBackupTo.Location = New Point(245, 0)
        btnBackupTo.Margin = New Padding(3, 0, 3, 0)
        btnBackupTo.Name = "btnBackupTo"
        btnBackupTo.Padding = New Padding(3, 0, 3, 0)
        btnBackupTo.Size = New Size(55, 30)
        btnBackupTo.TabIndex = 8
        btnBackupTo.Text = "备份"
        btnBackupTo.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(306, 0)
        btnCancel.Margin = New Padding(3, 0, 0, 0)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(52, 30)
        btnCancel.TabIndex = 9
        btnCancel.Text = "取消"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        TableLayoutPanel1.SetColumnSpan(lblTitle, 2)
        lblTitle.Dock = DockStyle.Left
        lblTitle.ForeColor = SystemColors.Highlight
        lblTitle.Location = New Point(0, 0)
        lblTitle.Margin = New Padding(0, 0, 0, 6)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(184, 18)
        lblTitle.TabIndex = 0
        lblTitle.Text = "选择用户目录下备份的内容:"
        lblTitle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 3
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.Controls.Add(lblSizeTotal, 0, 15)
        TableLayoutPanel1.Controls.Add(lblCount1, 1, 1)
        TableLayoutPanel1.Controls.Add(chkVideos, 0, 7)
        TableLayoutPanel1.Controls.Add(lblTitle, 0, 0)
        TableLayoutPanel1.Controls.Add(chkSaves, 0, 3)
        TableLayoutPanel1.Controls.Add(chkGhosts, 0, 13)
        TableLayoutPanel1.Controls.Add(chkDemos, 0, 5)
        TableLayoutPanel1.Controls.Add(chkAssets, 0, 10)
        TableLayoutPanel1.Controls.Add(lblSize1, 2, 1)
        TableLayoutPanel1.Controls.Add(lblSize2, 2, 2)
        TableLayoutPanel1.Controls.Add(lblSize3, 2, 3)
        TableLayoutPanel1.Controls.Add(lblSize4, 2, 5)
        TableLayoutPanel1.Controls.Add(lblSize5, 2, 7)
        TableLayoutPanel1.Controls.Add(lblSize6, 2, 10)
        TableLayoutPanel1.Controls.Add(lblSize7, 2, 13)
        TableLayoutPanel1.Controls.Add(chkSettings, 0, 1)
        TableLayoutPanel1.Controls.Add(chkCfg, 0, 2)
        TableLayoutPanel1.Controls.Add(lblCount2, 1, 2)
        TableLayoutPanel1.Controls.Add(lblCount3, 1, 3)
        TableLayoutPanel1.Controls.Add(lblCount4, 1, 5)
        TableLayoutPanel1.Controls.Add(lblCount5, 1, 7)
        TableLayoutPanel1.Controls.Add(lblCount6, 1, 10)
        TableLayoutPanel1.Controls.Add(lblCount7, 1, 13)
        TableLayoutPanel1.Controls.Add(splitExport, 0, 14)
        TableLayoutPanel1.Location = New Point(12, 12)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 17
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
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Size = New Size(358, 265)
        TableLayoutPanel1.TabIndex = 10
        ' 
        ' lblSizeTotal
        ' 
        lblSizeTotal.AutoSize = True
        TableLayoutPanel1.SetColumnSpan(lblSizeTotal, 3)
        lblSizeTotal.ForeColor = Color.DarkGray
        lblSizeTotal.Location = New Point(0, 245)
        lblSizeTotal.Margin = New Padding(0, 3, 0, 3)
        lblSizeTotal.Name = "lblSizeTotal"
        lblSizeTotal.Size = New Size(72, 18)
        lblSizeTotal.TabIndex = 70
        lblSizeTotal.Text = "备份大小:"
        ' 
        ' lblCount1
        ' 
        lblCount1.AutoSize = True
        lblCount1.Dock = DockStyle.Right
        lblCount1.Location = New Point(280, 27)
        lblCount1.Margin = New Padding(3)
        lblCount1.Name = "lblCount1"
        lblCount1.Size = New Size(16, 22)
        lblCount1.TabIndex = 62
        lblCount1.Text = "-"
        lblCount1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' chkVideos
        ' 
        chkVideos.AutoSize = True
        chkVideos.Dock = DockStyle.Left
        chkVideos.Location = New Point(0, 139)
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
        chkSaves.Location = New Point(0, 83)
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
        chkGhosts.Location = New Point(0, 195)
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
        chkDemos.Location = New Point(0, 111)
        chkDemos.Margin = New Padding(0, 3, 3, 3)
        chkDemos.Name = "chkDemos"
        chkDemos.Size = New Size(170, 22)
        chkDemos.TabIndex = 12
        chkDemos.Text = "回放 (...\videos\)"
        ToolTip1.SetToolTip(chkDemos, "demos 文件夹")
        chkDemos.UseVisualStyleBackColor = True
        ' 
        ' chkAssets
        ' 
        chkAssets.AutoSize = True
        chkAssets.Dock = DockStyle.Left
        chkAssets.Location = New Point(0, 167)
        chkAssets.Margin = New Padding(0, 3, 3, 3)
        chkAssets.Name = "chkAssets"
        chkAssets.Size = New Size(170, 22)
        chkAssets.TabIndex = 11
        chkAssets.Text = "材质 (...\assets\)"
        ToolTip1.SetToolTip(chkAssets, "assets 文件夹")
        chkAssets.UseVisualStyleBackColor = True
        ' 
        ' lblSize1
        ' 
        lblSize1.AutoSize = True
        lblSize1.Dock = DockStyle.Right
        lblSize1.Location = New Point(302, 27)
        lblSize1.Margin = New Padding(3, 3, 0, 3)
        lblSize1.Name = "lblSize1"
        lblSize1.Size = New Size(56, 22)
        lblSize1.TabIndex = 52
        lblSize1.Text = "Label1"
        lblSize1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSize2
        ' 
        lblSize2.AutoSize = True
        lblSize2.Dock = DockStyle.Right
        lblSize2.Location = New Point(302, 55)
        lblSize2.Margin = New Padding(3, 3, 0, 3)
        lblSize2.Name = "lblSize2"
        lblSize2.Size = New Size(56, 22)
        lblSize2.TabIndex = 53
        lblSize2.Text = "Label2"
        lblSize2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSize3
        ' 
        lblSize3.AutoSize = True
        lblSize3.Dock = DockStyle.Right
        lblSize3.Location = New Point(302, 83)
        lblSize3.Margin = New Padding(3, 3, 0, 3)
        lblSize3.Name = "lblSize3"
        lblSize3.Size = New Size(56, 22)
        lblSize3.TabIndex = 54
        lblSize3.Text = "Label3"
        lblSize3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSize4
        ' 
        lblSize4.AutoSize = True
        lblSize4.Dock = DockStyle.Right
        lblSize4.Location = New Point(302, 111)
        lblSize4.Margin = New Padding(3, 3, 0, 3)
        lblSize4.Name = "lblSize4"
        lblSize4.Size = New Size(56, 22)
        lblSize4.TabIndex = 55
        lblSize4.Text = "Label4"
        lblSize4.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSize5
        ' 
        lblSize5.AutoSize = True
        lblSize5.Dock = DockStyle.Right
        lblSize5.Location = New Point(302, 139)
        lblSize5.Margin = New Padding(3, 3, 0, 3)
        lblSize5.Name = "lblSize5"
        lblSize5.Size = New Size(56, 22)
        lblSize5.TabIndex = 56
        lblSize5.Text = "Label5"
        lblSize5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSize6
        ' 
        lblSize6.AutoSize = True
        lblSize6.Dock = DockStyle.Right
        lblSize6.Location = New Point(302, 167)
        lblSize6.Margin = New Padding(3, 3, 0, 3)
        lblSize6.Name = "lblSize6"
        lblSize6.Size = New Size(56, 22)
        lblSize6.TabIndex = 57
        lblSize6.Text = "Label6"
        lblSize6.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblSize7
        ' 
        lblSize7.AutoSize = True
        lblSize7.Dock = DockStyle.Right
        lblSize7.Location = New Point(302, 195)
        lblSize7.Margin = New Padding(3, 3, 0, 3)
        lblSize7.Name = "lblSize7"
        lblSize7.Size = New Size(56, 22)
        lblSize7.TabIndex = 58
        lblSize7.Text = "Label7"
        lblSize7.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' chkSettings
        ' 
        chkSettings.AutoSize = True
        chkSettings.Dock = DockStyle.Left
        chkSettings.Location = New Point(0, 27)
        chkSettings.Margin = New Padding(0, 3, 3, 3)
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
        chkCfg.Location = New Point(0, 55)
        chkCfg.Margin = New Padding(0, 3, 3, 3)
        chkCfg.Name = "chkCfg"
        chkCfg.Size = New Size(150, 22)
        chkCfg.TabIndex = 8
        chkCfg.Text = "配置文件 (*.cfg)"
        ToolTip1.SetToolTip(chkCfg, "遍历所有 .cfg 文件")
        chkCfg.UseVisualStyleBackColor = True
        ' 
        ' lblCount2
        ' 
        lblCount2.AutoSize = True
        lblCount2.Dock = DockStyle.Right
        lblCount2.Location = New Point(280, 55)
        lblCount2.Margin = New Padding(3)
        lblCount2.Name = "lblCount2"
        lblCount2.Size = New Size(16, 22)
        lblCount2.TabIndex = 63
        lblCount2.Text = "-"
        lblCount2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCount3
        ' 
        lblCount3.AutoSize = True
        lblCount3.Dock = DockStyle.Right
        lblCount3.Location = New Point(280, 83)
        lblCount3.Margin = New Padding(3)
        lblCount3.Name = "lblCount3"
        lblCount3.Size = New Size(16, 22)
        lblCount3.TabIndex = 64
        lblCount3.Text = "-"
        lblCount3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCount4
        ' 
        lblCount4.AutoSize = True
        lblCount4.Dock = DockStyle.Right
        lblCount4.Location = New Point(280, 111)
        lblCount4.Margin = New Padding(3)
        lblCount4.Name = "lblCount4"
        lblCount4.Size = New Size(16, 22)
        lblCount4.TabIndex = 65
        lblCount4.Text = "-"
        lblCount4.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCount5
        ' 
        lblCount5.AutoSize = True
        lblCount5.Dock = DockStyle.Right
        lblCount5.Location = New Point(280, 139)
        lblCount5.Margin = New Padding(3)
        lblCount5.Name = "lblCount5"
        lblCount5.Size = New Size(16, 22)
        lblCount5.TabIndex = 66
        lblCount5.Text = "-"
        lblCount5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCount6
        ' 
        lblCount6.AutoSize = True
        lblCount6.Dock = DockStyle.Right
        lblCount6.Location = New Point(280, 167)
        lblCount6.Margin = New Padding(3)
        lblCount6.Name = "lblCount6"
        lblCount6.Size = New Size(16, 22)
        lblCount6.TabIndex = 67
        lblCount6.Text = "-"
        lblCount6.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCount7
        ' 
        lblCount7.AutoSize = True
        lblCount7.Dock = DockStyle.Right
        lblCount7.Location = New Point(280, 195)
        lblCount7.Margin = New Padding(3)
        lblCount7.Name = "lblCount7"
        lblCount7.Size = New Size(16, 22)
        lblCount7.TabIndex = 68
        lblCount7.Text = "-"
        lblCount7.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' splitExport
        ' 
        TableLayoutPanel1.SetColumnSpan(splitExport, 3)
        splitExport.ForeColor = Color.DarkGray
        splitExport.Location = New Point(3, 223)
        splitExport.Margin = New Padding(3)
        splitExport.Name = "splitExport"
        splitExport.Size = New Size(352, 16)
        splitExport.TabIndex = 69
        splitExport.Text = "─────────────────────────────────────────────────────────"
        ' 
        ' btnSelectAll
        ' 
        btnSelectAll.AllowDrop = True
        btnSelectAll.AutoSize = True
        btnSelectAll.Location = New Point(12, 273)
        btnSelectAll.Margin = New Padding(0, 0, 3, 0)
        btnSelectAll.Name = "btnSelectAll"
        btnSelectAll.Padding = New Padding(3, 0, 3, 0)
        btnSelectAll.Size = New Size(55, 28)
        btnSelectAll.TabIndex = 23
        btnSelectAll.Text = "全选"
        btnSelectAll.UseVisualStyleBackColor = True
        btnSelectAll.Visible = False
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
        tblExport.Controls.Add(ProgressBar2, 0, 0)
        tblExport.Controls.Add(btnCancel, 3, 0)
        tblExport.Controls.Add(btnBackupTo, 2, 0)
        tblExport.Location = New Point(12, 301)
        tblExport.Name = "tblExport"
        tblExport.RowCount = 1
        tblExport.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblExport.Size = New Size(358, 30)
        tblExport.TabIndex = 50
        ' 
        ' btnSnapshot
        ' 
        btnSnapshot.AutoSize = True
        btnSnapshot.Location = New Point(184, 0)
        btnSnapshot.Margin = New Padding(3, 0, 3, 0)
        btnSnapshot.Name = "btnSnapshot"
        btnSnapshot.Padding = New Padding(3, 0, 3, 0)
        btnSnapshot.Size = New Size(55, 30)
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
        ProgressBar2.Size = New Size(175, 24)
        ProgressBar2.TabIndex = 7
        ProgressBar2.Visible = False
        ' 
        ' DialogBackup
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 343)
        Controls.Add(tblExport)
        Controls.Add(btnSelectAll)
        Controls.Add(TableLayoutPanel1)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogBackup"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "备份"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        tblExport.ResumeLayout(False)
        tblExport.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnBackupTo As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblTitle As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents chkCfg As CheckBox
    Friend WithEvents chkSaves As CheckBox
    Friend WithEvents chkSettings As CheckBox
    Friend WithEvents chkDemos As CheckBox
    Friend WithEvents tblExport As TableLayoutPanel
    Friend WithEvents ProgressBar2 As ProgressBar
    Friend WithEvents lblSize1 As Label
    Friend WithEvents lblSize2 As Label
    Friend WithEvents lblSize3 As Label
    Friend WithEvents lblSize4 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents lblCount1 As Label
    Friend WithEvents lblCount2 As Label
    Friend WithEvents lblCount3 As Label
    Friend WithEvents lblCount4 As Label
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents chkVideos As CheckBox
    Friend WithEvents chkGhosts As CheckBox
    Friend WithEvents chkAssets As CheckBox
    Friend WithEvents lblSize5 As Label
    Friend WithEvents lblSize6 As Label
    Friend WithEvents lblSize7 As Label
    Friend WithEvents lblCount5 As Label
    Friend WithEvents lblCount6 As Label
    Friend WithEvents lblCount7 As Label
    Friend WithEvents splitExport As Label
    Friend WithEvents lblSizeTotal As Label
    Friend WithEvents btnSnapshot As Button
End Class
