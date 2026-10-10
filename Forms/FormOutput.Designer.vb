<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOutput
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormOutput))
        tblExportMode = New TableLayoutPanel()
        lblCount3 = New Label()
        lblCount2 = New Label()
        lblCount1 = New Label()
        chkNote = New CheckBox()
        rbStandard = New RadioButton()
        rbFull = New RadioButton()
        lblNormal = New Label()
        tbNote = New TextBox()
        lblStandard = New Label()
        lblFull = New Label()
        rbNormal = New RadioButton()
        tblExport = New TableLayoutPanel()
        btnSaveJson = New Button()
        ProgressBar1 = New ProgressBar()
        btnSaveDNFP = New Button()
        Label3 = New Label()
        lblDate = New Label()
        lblTime = New Label()
        lblUsername = New Label()
        tblExportMode.SuspendLayout()
        tblExport.SuspendLayout()
        SuspendLayout()
        ' 
        ' tblExportMode
        ' 
        tblExportMode.ColumnCount = 3
        tblExportMode.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblExportMode.ColumnStyles.Add(New ColumnStyle())
        tblExportMode.ColumnStyles.Add(New ColumnStyle())
        tblExportMode.Controls.Add(lblCount3, 1, 2)
        tblExportMode.Controls.Add(lblCount2, 1, 1)
        tblExportMode.Controls.Add(lblCount1, 1, 0)
        tblExportMode.Controls.Add(chkNote, 0, 4)
        tblExportMode.Controls.Add(rbStandard, 0, 1)
        tblExportMode.Controls.Add(rbFull, 0, 2)
        tblExportMode.Controls.Add(lblNormal, 2, 0)
        tblExportMode.Controls.Add(tbNote, 0, 5)
        tblExportMode.Controls.Add(lblStandard, 2, 1)
        tblExportMode.Controls.Add(lblFull, 2, 2)
        tblExportMode.Controls.Add(rbNormal, 0, 0)
        tblExportMode.Location = New Point(12, 12)
        tblExportMode.Margin = New Padding(3, 3, 3, 6)
        tblExportMode.Name = "tblExportMode"
        tblExportMode.RowCount = 6
        tblExportMode.RowStyles.Add(New RowStyle())
        tblExportMode.RowStyles.Add(New RowStyle())
        tblExportMode.RowStyles.Add(New RowStyle())
        tblExportMode.RowStyles.Add(New RowStyle())
        tblExportMode.RowStyles.Add(New RowStyle())
        tblExportMode.RowStyles.Add(New RowStyle())
        tblExportMode.Size = New Size(358, 266)
        tblExportMode.TabIndex = 39
        ' 
        ' lblCount3
        ' 
        lblCount3.AutoEllipsis = True
        lblCount3.AutoSize = True
        lblCount3.Dock = DockStyle.Right
        lblCount3.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblCount3.Location = New Point(238, 104)
        lblCount3.Margin = New Padding(3, 3, 3, 0)
        lblCount3.Name = "lblCount3"
        lblCount3.Size = New Size(74, 46)
        lblCount3.TabIndex = 18
        lblCount3.Text = "字体 - 项"
        lblCount3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCount2
        ' 
        lblCount2.AutoEllipsis = True
        lblCount2.AutoSize = True
        lblCount2.Dock = DockStyle.Right
        lblCount2.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblCount2.Location = New Point(238, 52)
        lblCount2.Margin = New Padding(3)
        lblCount2.Name = "lblCount2"
        lblCount2.Size = New Size(74, 46)
        lblCount2.TabIndex = 17
        lblCount2.Text = "字体 - 项"
        lblCount2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCount1
        ' 
        lblCount1.AutoEllipsis = True
        lblCount1.AutoSize = True
        lblCount1.Dock = DockStyle.Right
        lblCount1.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblCount1.Location = New Point(238, 0)
        lblCount1.Margin = New Padding(3, 0, 3, 3)
        lblCount1.Name = "lblCount1"
        lblCount1.Size = New Size(74, 46)
        lblCount1.TabIndex = 16
        lblCount1.Text = "字体 - 项"
        lblCount1.TextAlign = ContentAlignment.MiddleRight
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
        rbStandard.Dock = DockStyle.Left
        rbStandard.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rbStandard.Image = CType(resources.GetObject("rbStandard.Image"), Image)
        rbStandard.Location = New Point(0, 52)
        rbStandard.Margin = New Padding(0, 3, 3, 3)
        rbStandard.Name = "rbStandard"
        rbStandard.Padding = New Padding(3, 0, 3, 0)
        rbStandard.Size = New Size(142, 46)
        rbStandard.TabIndex = 5
        rbStandard.TabStop = True
        rbStandard.Text = "标准" & vbCrLf & "(含预装字体)"
        rbStandard.TextImageRelation = TextImageRelation.ImageBeforeText
        rbStandard.UseVisualStyleBackColor = False
        ' 
        ' rbFull
        ' 
        rbFull.Appearance = Appearance.Button
        rbFull.AutoSize = True
        rbFull.BackColor = Color.Transparent
        rbFull.Dock = DockStyle.Left
        rbFull.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rbFull.Image = CType(resources.GetObject("rbFull.Image"), Image)
        rbFull.Location = New Point(0, 104)
        rbFull.Margin = New Padding(0, 3, 3, 0)
        rbFull.Name = "rbFull"
        rbFull.Padding = New Padding(3, 0, 3, 0)
        rbFull.Size = New Size(142, 46)
        rbFull.TabIndex = 14
        rbFull.TabStop = True
        rbFull.Text = "完整" & vbCrLf & "(未使用字体)"
        rbFull.TextImageRelation = TextImageRelation.ImageBeforeText
        rbFull.UseVisualStyleBackColor = False
        ' 
        ' lblNormal
        ' 
        lblNormal.AutoEllipsis = True
        lblNormal.AutoSize = True
        lblNormal.Dock = DockStyle.Right
        lblNormal.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblNormal.Location = New Point(318, 0)
        lblNormal.Margin = New Padding(3, 0, 0, 3)
        lblNormal.Name = "lblNormal"
        lblNormal.Size = New Size(40, 46)
        lblNormal.TabIndex = 12
        lblNormal.Text = "- MB"
        lblNormal.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tbNote
        ' 
        tblExportMode.SetColumnSpan(tbNote, 3)
        tbNote.Dock = DockStyle.Top
        tbNote.ForeColor = Color.Black
        tbNote.Location = New Point(0, 184)
        tbNote.Margin = New Padding(0, 3, 0, 0)
        tbNote.Multiline = True
        tbNote.Name = "tbNote"
        tbNote.PlaceholderText = "最多 40 个字符"
        tbNote.Size = New Size(358, 73)
        tbNote.TabIndex = 10
        ' 
        ' lblStandard
        ' 
        lblStandard.AutoEllipsis = True
        lblStandard.AutoSize = True
        lblStandard.Dock = DockStyle.Right
        lblStandard.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblStandard.Location = New Point(318, 52)
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
        lblFull.Dock = DockStyle.Right
        lblFull.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblFull.Location = New Point(318, 104)
        lblFull.Margin = New Padding(3, 3, 0, 0)
        lblFull.Name = "lblFull"
        lblFull.Size = New Size(40, 46)
        lblFull.TabIndex = 15
        lblFull.Text = "- MB"
        lblFull.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' rbNormal
        ' 
        rbNormal.Appearance = Appearance.Button
        rbNormal.AutoSize = True
        rbNormal.BackColor = Color.Transparent
        rbNormal.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rbNormal.Image = CType(resources.GetObject("rbNormal.Image"), Image)
        rbNormal.Location = New Point(0, 0)
        rbNormal.Margin = New Padding(0, 0, 3, 3)
        rbNormal.Name = "rbNormal"
        rbNormal.Padding = New Padding(3, 0, 3, 0)
        rbNormal.Size = New Size(142, 46)
        rbNormal.TabIndex = 4
        rbNormal.TabStop = True
        rbNormal.Text = "默认" & vbCrLf & "(已使用字体)"
        rbNormal.TextImageRelation = TextImageRelation.ImageBeforeText
        rbNormal.UseVisualStyleBackColor = False
        ' 
        ' tblExport
        ' 
        tblExport.BackColor = Color.Transparent
        tblExport.ColumnCount = 3
        tblExport.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblExport.ColumnStyles.Add(New ColumnStyle())
        tblExport.ColumnStyles.Add(New ColumnStyle())
        tblExport.Controls.Add(btnSaveJson, 1, 0)
        tblExport.Controls.Add(ProgressBar1, 0, 0)
        tblExport.Controls.Add(btnSaveDNFP, 2, 0)
        tblExport.Location = New Point(12, 314)
        tblExport.Name = "tblExport"
        tblExport.RowCount = 1
        tblExport.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblExport.Size = New Size(358, 30)
        tblExport.TabIndex = 40
        ' 
        ' btnSaveJson
        ' 
        btnSaveJson.Location = New Point(206, 0)
        btnSaveJson.Margin = New Padding(3, 0, 3, 0)
        btnSaveJson.Name = "btnSaveJson"
        btnSaveJson.Padding = New Padding(3, 0, 3, 0)
        btnSaveJson.Size = New Size(94, 30)
        btnSaveJson.TabIndex = 41
        btnSaveJson.Text = "配置另存为"
        btnSaveJson.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.BackColor = Color.WhiteSmoke
        ProgressBar1.Dock = DockStyle.Fill
        ProgressBar1.Location = New Point(0, 3)
        ProgressBar1.Margin = New Padding(0, 3, 3, 3)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(200, 24)
        ProgressBar1.TabIndex = 7
        ProgressBar1.Visible = False
        ' 
        ' btnSaveDNFP
        ' 
        btnSaveDNFP.BackColor = Color.Transparent
        btnSaveDNFP.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnSaveDNFP.Location = New Point(306, 0)
        btnSaveDNFP.Margin = New Padding(3, 0, 0, 0)
        btnSaveDNFP.Name = "btnSaveDNFP"
        btnSaveDNFP.Padding = New Padding(3, 0, 3, 0)
        btnSaveDNFP.Size = New Size(52, 30)
        btnSaveDNFP.TabIndex = 2
        btnSaveDNFP.Text = "保存"
        btnSaveDNFP.UseVisualStyleBackColor = False
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label3.Location = New Point(82, 272)
        Label3.Margin = New Padding(3)
        Label3.Name = "Label3"
        Label3.Size = New Size(72, 18)
        Label3.TabIndex = 49
        Label3.Text = "快速插入:"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblDate
        ' 
        lblDate.AutoSize = True
        lblDate.Cursor = Cursors.Hand
        lblDate.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblDate.Location = New Point(306, 272)
        lblDate.Margin = New Padding(0, 3, 3, 3)
        lblDate.Name = "lblDate"
        lblDate.Size = New Size(64, 18)
        lblDate.TabIndex = 48
        lblDate.Text = "当前日期"
        lblDate.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblTime
        ' 
        lblTime.AutoSize = True
        lblTime.Cursor = Cursors.Hand
        lblTime.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblTime.Location = New Point(226, 272)
        lblTime.Margin = New Padding(0, 3, 0, 3)
        lblTime.Name = "lblTime"
        lblTime.Size = New Size(80, 18)
        lblTime.TabIndex = 47
        lblTime.Text = "当前时间 /"
        lblTime.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblUsername
        ' 
        lblUsername.AutoSize = True
        lblUsername.Cursor = Cursors.Hand
        lblUsername.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblUsername.Location = New Point(160, 272)
        lblUsername.Margin = New Padding(3, 3, 0, 3)
        lblUsername.Name = "lblUsername"
        lblUsername.Size = New Size(66, 18)
        lblUsername.TabIndex = 46
        lblUsername.Text = "用户名 /"
        lblUsername.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' FormOutput
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 356)
        Controls.Add(Label3)
        Controls.Add(lblDate)
        Controls.Add(lblTime)
        Controls.Add(lblUsername)
        Controls.Add(tblExport)
        Controls.Add(tblExportMode)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "FormOutput"
        StartPosition = FormStartPosition.CenterParent
        Text = "导出"
        tblExportMode.ResumeLayout(False)
        tblExportMode.PerformLayout()
        tblExport.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tblExportMode As TableLayoutPanel
    Friend WithEvents chkNote As CheckBox
    Friend WithEvents rbStandard As RadioButton
    Friend WithEvents rbFull As RadioButton
    Friend WithEvents lblNormal As Label
    Friend WithEvents tbNote As TextBox
    Friend WithEvents lblStandard As Label
    Friend WithEvents lblFull As Label
    Friend WithEvents rbNormal As RadioButton
    Friend WithEvents tblExport As TableLayoutPanel
    Friend WithEvents btnSaveDNFP As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents btnSaveJson As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents lblDate As Label
    Friend WithEvents lblTime As Label
    Friend WithEvents lblUsername As Label
    Friend WithEvents lblCount3 As Label
    Friend WithEvents lblCount2 As Label
    Friend WithEvents lblCount1 As Label
End Class
