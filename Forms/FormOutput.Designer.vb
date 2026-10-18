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
        chkNote = New CheckBox()
        rdoStandard = New RadioButton()
        rdoFull = New RadioButton()
        lblUserName = New Label()
        txtNote = New TextBox()
        rdoDefault = New RadioButton()
        lblDate = New Label()
        lblTime = New Label()
        Label1 = New Label()
        Label2 = New Label()
        tblExport = New TableLayoutPanel()
        btnCfg = New Button()
        ProgressBar1 = New ProgressBar()
        btnDNFP = New Button()
        lblSizeTotal = New Label()
        tblExportMode.SuspendLayout()
        tblExport.SuspendLayout()
        SuspendLayout()
        ' 
        ' tblExportMode
        ' 
        tblExportMode.ColumnCount = 7
        tblExportMode.ColumnStyles.Add(New ColumnStyle())
        tblExportMode.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblExportMode.ColumnStyles.Add(New ColumnStyle())
        tblExportMode.ColumnStyles.Add(New ColumnStyle())
        tblExportMode.ColumnStyles.Add(New ColumnStyle())
        tblExportMode.ColumnStyles.Add(New ColumnStyle())
        tblExportMode.ColumnStyles.Add(New ColumnStyle())
        tblExportMode.Controls.Add(chkNote, 0, 4)
        tblExportMode.Controls.Add(rdoStandard, 0, 1)
        tblExportMode.Controls.Add(rdoFull, 0, 2)
        tblExportMode.Controls.Add(lblUserName, 2, 4)
        tblExportMode.Controls.Add(txtNote, 0, 5)
        tblExportMode.Controls.Add(rdoDefault, 0, 0)
        tblExportMode.Controls.Add(lblDate, 6, 4)
        tblExportMode.Controls.Add(lblTime, 4, 4)
        tblExportMode.Controls.Add(Label1, 3, 4)
        tblExportMode.Controls.Add(Label2, 5, 4)
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
        tblExportMode.Size = New Size(358, 250)
        tblExportMode.TabIndex = 39
        ' 
        ' chkNote
        ' 
        chkNote.AutoSize = True
        chkNote.Checked = True
        chkNote.CheckState = CheckState.Checked
        chkNote.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        chkNote.Location = New Point(0, 156)
        chkNote.Margin = New Padding(0, 3, 3, 3)
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
        rdoStandard.Dock = DockStyle.Fill
        rdoStandard.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rdoStandard.Image = CType(resources.GetObject("rdoStandard.Image"), Image)
        rdoStandard.ImageAlign = ContentAlignment.MiddleLeft
        rdoStandard.Location = New Point(0, 52)
        rdoStandard.Margin = New Padding(0, 3, 0, 3)
        rdoStandard.Name = "rdoStandard"
        rdoStandard.Padding = New Padding(3, 0, 3, 0)
        rdoStandard.Size = New Size(358, 46)
        rdoStandard.TabIndex = 5
        rdoStandard.TabStop = True
        rdoStandard.Text = "标准" & vbCrLf & "(已使用字体和预装字体, 体积适中)"
        rdoStandard.TextImageRelation = TextImageRelation.ImageBeforeText
        rdoStandard.UseVisualStyleBackColor = False
        ' 
        ' rdoFull
        ' 
        rdoFull.Appearance = Appearance.Button
        rdoFull.AutoSize = True
        rdoFull.BackColor = Color.Transparent
        rdoFull.Dock = DockStyle.Fill
        rdoFull.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rdoFull.Image = CType(resources.GetObject("rdoFull.Image"), Image)
        rdoFull.ImageAlign = ContentAlignment.MiddleLeft
        rdoFull.Location = New Point(0, 104)
        rdoFull.Margin = New Padding(0, 3, 0, 3)
        rdoFull.Name = "rdoFull"
        rdoFull.Padding = New Padding(3, 0, 3, 0)
        rdoFull.Size = New Size(358, 46)
        rdoFull.TabIndex = 14
        rdoFull.TabStop = True
        rdoFull.Text = "完整" & vbCrLf & "(已安装的全部字体, 体积最大)"
        rdoFull.TextImageRelation = TextImageRelation.ImageBeforeText
        rdoFull.UseVisualStyleBackColor = False
        ' 
        ' lblUserName
        ' 
        lblUserName.AutoSize = True
        lblUserName.Cursor = Cursors.Hand
        lblUserName.Dock = DockStyle.Fill
        lblUserName.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblUserName.Location = New Point(148, 156)
        lblUserName.Margin = New Padding(0, 3, 0, 3)
        lblUserName.Name = "lblUserName"
        lblUserName.Size = New Size(50, 22)
        lblUserName.TabIndex = 46
        lblUserName.Text = "用户名"
        lblUserName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtNote
        ' 
        txtNote.Dock = DockStyle.Top
        txtNote.ForeColor = Color.Black
        txtNote.Location = New Point(0, 184)
        txtNote.Margin = New Padding(0, 3, 0, 0)
        txtNote.Multiline = True
        txtNote.Name = "txtNote"
        txtNote.PlaceholderText = "最多 40 个字符"
        txtNote.Size = New Size(358, 66)
        txtNote.TabIndex = 10
        ' 
        ' rdoDefault
        ' 
        rdoDefault.Appearance = Appearance.Button
        rdoDefault.AutoSize = True
        rdoDefault.BackColor = Color.Transparent
        rdoDefault.Dock = DockStyle.Fill
        rdoDefault.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        rdoDefault.Image = CType(resources.GetObject("rdoDefault.Image"), Image)
        rdoDefault.ImageAlign = ContentAlignment.MiddleLeft
        rdoDefault.Location = New Point(0, 0)
        rdoDefault.Margin = New Padding(0, 0, 0, 3)
        rdoDefault.Name = "rdoDefault"
        rdoDefault.Padding = New Padding(3, 0, 3, 0)
        rdoDefault.Size = New Size(358, 46)
        rdoDefault.TabIndex = 4
        rdoDefault.TabStop = True
        rdoDefault.Text = "默认" & vbCrLf & "(已使用字体, 体积最小)"
        rdoDefault.TextImageRelation = TextImageRelation.ImageBeforeText
        rdoDefault.UseVisualStyleBackColor = False
        ' 
        ' lblDate
        ' 
        lblDate.AutoSize = True
        lblDate.Cursor = Cursors.Hand
        lblDate.Dock = DockStyle.Fill
        lblDate.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblDate.Location = New Point(294, 156)
        lblDate.Margin = New Padding(0, 3, 0, 3)
        lblDate.Name = "lblDate"
        lblDate.Size = New Size(64, 22)
        lblDate.TabIndex = 48
        lblDate.Text = "当前日期"
        lblDate.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblTime
        ' 
        lblTime.AutoSize = True
        lblTime.Cursor = Cursors.Hand
        lblTime.Dock = DockStyle.Fill
        lblTime.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblTime.Location = New Point(214, 156)
        lblTime.Margin = New Padding(0, 3, 0, 3)
        lblTime.Name = "lblTime"
        lblTime.Size = New Size(64, 22)
        lblTime.TabIndex = 47
        lblTime.Text = "当前时间"
        lblTime.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Cursor = Cursors.Hand
        Label1.Dock = DockStyle.Fill
        Label1.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label1.Location = New Point(198, 156)
        Label1.Margin = New Padding(0, 3, 0, 3)
        Label1.Name = "Label1"
        Label1.Size = New Size(16, 22)
        Label1.TabIndex = 49
        Label1.Text = "/"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Cursor = Cursors.Hand
        Label2.Dock = DockStyle.Fill
        Label2.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label2.Location = New Point(278, 156)
        Label2.Margin = New Padding(0, 3, 0, 3)
        Label2.Name = "Label2"
        Label2.Size = New Size(16, 22)
        Label2.TabIndex = 50
        Label2.Text = "/"
        Label2.TextAlign = ContentAlignment.MiddleLeft
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
        tblExport.Location = New Point(12, 313)
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
        ' lblSizeTotal
        ' 
        lblSizeTotal.AutoSize = True
        lblSizeTotal.ForeColor = Color.DarkGray
        lblSizeTotal.Location = New Point(12, 271)
        lblSizeTotal.Margin = New Padding(3)
        lblSizeTotal.Name = "lblSizeTotal"
        lblSizeTotal.Size = New Size(72, 18)
        lblSizeTotal.TabIndex = 72
        lblSizeTotal.Text = "备份大小:"
        ' 
        ' FormOutput
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 355)
        Controls.Add(lblSizeTotal)
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
    Friend WithEvents txtNote As TextBox
    Friend WithEvents rdoDefault As RadioButton
    Friend WithEvents tblExport As TableLayoutPanel
    Friend WithEvents btnDNFP As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents btnCfg As Button
    Friend WithEvents lblDate As Label
    Friend WithEvents lblTime As Label
    Friend WithEvents lblUserName As Label
    Friend WithEvents lblSizeTotal As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
