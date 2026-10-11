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
        lblCountFull = New Label()
        lblCountStandard = New Label()
        lblCountDefault = New Label()
        chkNote = New CheckBox()
        rdoStandard = New RadioButton()
        rdoFull = New RadioButton()
        lblSizeDefault = New Label()
        txtNote = New TextBox()
        lblSizeStandard = New Label()
        lblSizeFull = New Label()
        rdoDefault = New RadioButton()
        tblExport = New TableLayoutPanel()
        btnCfg = New Button()
        ProgressBar1 = New ProgressBar()
        btnDNFP = New Button()
        tagInsert = New Label()
        lblDate = New Label()
        lblTime = New Label()
        lblUserName = New Label()
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
        tblExportMode.Controls.Add(lblCountFull, 1, 2)
        tblExportMode.Controls.Add(lblCountStandard, 1, 1)
        tblExportMode.Controls.Add(lblCountDefault, 1, 0)
        tblExportMode.Controls.Add(chkNote, 0, 4)
        tblExportMode.Controls.Add(rdoStandard, 0, 1)
        tblExportMode.Controls.Add(rdoFull, 0, 2)
        tblExportMode.Controls.Add(lblSizeDefault, 2, 0)
        tblExportMode.Controls.Add(txtNote, 0, 5)
        tblExportMode.Controls.Add(lblSizeStandard, 2, 1)
        tblExportMode.Controls.Add(lblSizeFull, 2, 2)
        tblExportMode.Controls.Add(rdoDefault, 0, 0)
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
        ' lblCountFull
        ' 
        lblCountFull.AutoEllipsis = True
        lblCountFull.AutoSize = True
        lblCountFull.Dock = DockStyle.Right
        lblCountFull.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblCountFull.Location = New Point(238, 104)
        lblCountFull.Margin = New Padding(3, 3, 3, 0)
        lblCountFull.Name = "lblCountFull"
        lblCountFull.Size = New Size(74, 46)
        lblCountFull.TabIndex = 18
        lblCountFull.Text = "字体 - 项"
        lblCountFull.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCountStandard
        ' 
        lblCountStandard.AutoEllipsis = True
        lblCountStandard.AutoSize = True
        lblCountStandard.Dock = DockStyle.Right
        lblCountStandard.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblCountStandard.Location = New Point(238, 52)
        lblCountStandard.Margin = New Padding(3)
        lblCountStandard.Name = "lblCountStandard"
        lblCountStandard.Size = New Size(74, 46)
        lblCountStandard.TabIndex = 17
        lblCountStandard.Text = "字体 - 项"
        lblCountStandard.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblCountDefault
        ' 
        lblCountDefault.AutoEllipsis = True
        lblCountDefault.AutoSize = True
        lblCountDefault.Dock = DockStyle.Right
        lblCountDefault.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblCountDefault.Location = New Point(238, 0)
        lblCountDefault.Margin = New Padding(3, 0, 3, 3)
        lblCountDefault.Name = "lblCountDefault"
        lblCountDefault.Size = New Size(74, 46)
        lblCountDefault.TabIndex = 16
        lblCountDefault.Text = "字体 - 项"
        lblCountDefault.TextAlign = ContentAlignment.MiddleRight
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
        ' rdoStandard
        ' 
        rdoStandard.Appearance = Appearance.Button
        rdoStandard.AutoSize = True
        rdoStandard.BackColor = Color.Transparent
        rdoStandard.Dock = DockStyle.Left
        rdoStandard.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rdoStandard.Image = CType(resources.GetObject("rdoStandard.Image"), Image)
        rdoStandard.Location = New Point(0, 52)
        rdoStandard.Margin = New Padding(0, 3, 3, 3)
        rdoStandard.Name = "rdoStandard"
        rdoStandard.Padding = New Padding(3, 0, 3, 0)
        rdoStandard.Size = New Size(142, 46)
        rdoStandard.TabIndex = 5
        rdoStandard.TabStop = True
        rdoStandard.Text = "标准" & vbCrLf & "(含预装字体)"
        rdoStandard.TextImageRelation = TextImageRelation.ImageBeforeText
        rdoStandard.UseVisualStyleBackColor = False
        ' 
        ' rdoFull
        ' 
        rdoFull.Appearance = Appearance.Button
        rdoFull.AutoSize = True
        rdoFull.BackColor = Color.Transparent
        rdoFull.Dock = DockStyle.Left
        rdoFull.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rdoFull.Image = CType(resources.GetObject("rdoFull.Image"), Image)
        rdoFull.Location = New Point(0, 104)
        rdoFull.Margin = New Padding(0, 3, 3, 0)
        rdoFull.Name = "rdoFull"
        rdoFull.Padding = New Padding(3, 0, 3, 0)
        rdoFull.Size = New Size(142, 46)
        rdoFull.TabIndex = 14
        rdoFull.TabStop = True
        rdoFull.Text = "完整" & vbCrLf & "(未使用字体)"
        rdoFull.TextImageRelation = TextImageRelation.ImageBeforeText
        rdoFull.UseVisualStyleBackColor = False
        ' 
        ' lblSizeDefault
        ' 
        lblSizeDefault.AutoEllipsis = True
        lblSizeDefault.AutoSize = True
        lblSizeDefault.Dock = DockStyle.Right
        lblSizeDefault.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblSizeDefault.Location = New Point(318, 0)
        lblSizeDefault.Margin = New Padding(3, 0, 0, 3)
        lblSizeDefault.Name = "lblSizeDefault"
        lblSizeDefault.Size = New Size(40, 46)
        lblSizeDefault.TabIndex = 12
        lblSizeDefault.Text = "- MB"
        lblSizeDefault.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtNote
        ' 
        tblExportMode.SetColumnSpan(txtNote, 3)
        txtNote.Dock = DockStyle.Top
        txtNote.ForeColor = Color.Black
        txtNote.Location = New Point(0, 184)
        txtNote.Margin = New Padding(0, 3, 0, 0)
        txtNote.Multiline = True
        txtNote.Name = "txtNote"
        txtNote.PlaceholderText = "最多 40 个字符"
        txtNote.Size = New Size(358, 73)
        txtNote.TabIndex = 10
        ' 
        ' lblSizeStandard
        ' 
        lblSizeStandard.AutoEllipsis = True
        lblSizeStandard.AutoSize = True
        lblSizeStandard.Dock = DockStyle.Right
        lblSizeStandard.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblSizeStandard.Location = New Point(318, 52)
        lblSizeStandard.Margin = New Padding(3, 3, 0, 3)
        lblSizeStandard.Name = "lblSizeStandard"
        lblSizeStandard.Size = New Size(40, 46)
        lblSizeStandard.TabIndex = 13
        lblSizeStandard.Text = "- MB"
        lblSizeStandard.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblSizeFull
        ' 
        lblSizeFull.AutoEllipsis = True
        lblSizeFull.AutoSize = True
        lblSizeFull.Dock = DockStyle.Right
        lblSizeFull.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblSizeFull.Location = New Point(318, 104)
        lblSizeFull.Margin = New Padding(3, 3, 0, 0)
        lblSizeFull.Name = "lblSizeFull"
        lblSizeFull.Size = New Size(40, 46)
        lblSizeFull.TabIndex = 15
        lblSizeFull.Text = "- MB"
        lblSizeFull.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' rdoDefault
        ' 
        rdoDefault.Appearance = Appearance.Button
        rdoDefault.AutoSize = True
        rdoDefault.BackColor = Color.Transparent
        rdoDefault.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rdoDefault.Image = CType(resources.GetObject("rdoDefault.Image"), Image)
        rdoDefault.Location = New Point(0, 0)
        rdoDefault.Margin = New Padding(0, 0, 3, 3)
        rdoDefault.Name = "rdoDefault"
        rdoDefault.Padding = New Padding(3, 0, 3, 0)
        rdoDefault.Size = New Size(142, 46)
        rdoDefault.TabIndex = 4
        rdoDefault.TabStop = True
        rdoDefault.Text = "默认" & vbCrLf & "(已使用字体)"
        rdoDefault.TextImageRelation = TextImageRelation.ImageBeforeText
        rdoDefault.UseVisualStyleBackColor = False
        ' 
        ' tblExport
        ' 
        tblExport.BackColor = Color.Transparent
        tblExport.ColumnCount = 3
        tblExport.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblExport.ColumnStyles.Add(New ColumnStyle())
        tblExport.ColumnStyles.Add(New ColumnStyle())
        tblExport.Controls.Add(btnCfg, 1, 0)
        tblExport.Controls.Add(ProgressBar1, 0, 0)
        tblExport.Controls.Add(btnDNFP, 2, 0)
        tblExport.Location = New Point(12, 314)
        tblExport.Name = "tblExport"
        tblExport.RowCount = 1
        tblExport.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tblExport.Size = New Size(358, 30)
        tblExport.TabIndex = 40
        ' 
        ' btnCfg
        ' 
        btnCfg.Location = New Point(164, 0)
        btnCfg.Margin = New Padding(3, 0, 3, 0)
        btnCfg.Name = "btnCfg"
        btnCfg.Padding = New Padding(3, 0, 3, 0)
        btnCfg.Size = New Size(94, 30)
        btnCfg.TabIndex = 41
        btnCfg.Text = "配置另存为"
        btnCfg.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.BackColor = Color.WhiteSmoke
        ProgressBar1.Dock = DockStyle.Fill
        ProgressBar1.Location = New Point(0, 3)
        ProgressBar1.Margin = New Padding(0, 3, 3, 3)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(158, 24)
        ProgressBar1.TabIndex = 7
        ProgressBar1.Visible = False
        ' 
        ' btnDNFP
        ' 
        btnDNFP.BackColor = Color.Transparent
        btnDNFP.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnDNFP.Location = New Point(264, 0)
        btnDNFP.Margin = New Padding(3, 0, 0, 0)
        btnDNFP.Name = "btnDNFP"
        btnDNFP.Padding = New Padding(3, 0, 3, 0)
        btnDNFP.Size = New Size(94, 30)
        btnDNFP.TabIndex = 2
        btnDNFP.Text = "导出字体包"
        btnDNFP.UseVisualStyleBackColor = False
        ' 
        ' tagInsert
        ' 
        tagInsert.AutoSize = True
        tagInsert.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagInsert.Location = New Point(82, 272)
        tagInsert.Margin = New Padding(3)
        tagInsert.Name = "tagInsert"
        tagInsert.Size = New Size(72, 18)
        tagInsert.TabIndex = 49
        tagInsert.Text = "快速插入:"
        tagInsert.TextAlign = ContentAlignment.MiddleLeft
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
        ' lblUserName
        ' 
        lblUserName.AutoSize = True
        lblUserName.Cursor = Cursors.Hand
        lblUserName.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblUserName.Location = New Point(160, 272)
        lblUserName.Margin = New Padding(3, 3, 0, 3)
        lblUserName.Name = "lblUserName"
        lblUserName.Size = New Size(66, 18)
        lblUserName.TabIndex = 46
        lblUserName.Text = "用户名 /"
        lblUserName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' FormOutput
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 356)
        Controls.Add(tagInsert)
        Controls.Add(lblDate)
        Controls.Add(lblTime)
        Controls.Add(lblUserName)
        Controls.Add(tblExport)
        Controls.Add(tblExportMode)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
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
    Friend WithEvents rdoStandard As RadioButton
    Friend WithEvents rdoFull As RadioButton
    Friend WithEvents lblSizeDefault As Label
    Friend WithEvents txtNote As TextBox
    Friend WithEvents lblSizeStandard As Label
    Friend WithEvents lblSizeFull As Label
    Friend WithEvents rdoDefault As RadioButton
    Friend WithEvents tblExport As TableLayoutPanel
    Friend WithEvents btnDNFP As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents btnCfg As Button
    Friend WithEvents tagInsert As Label
    Friend WithEvents lblDate As Label
    Friend WithEvents lblTime As Label
    Friend WithEvents lblUserName As Label
    Friend WithEvents lblCountFull As Label
    Friend WithEvents lblCountStandard As Label
    Friend WithEvents lblCountDefault As Label
End Class
