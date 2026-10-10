<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogRestore
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogRestore))
        btnOK = New Button()
        btnCancel = New Button()
        TableLayoutPanel1 = New TableLayoutPanel()
        lblTitle = New Label()
        rbFontConfig = New RadioButton()
        rbConfig = New RadioButton()
        splitExport = New Label()
        Label2 = New Label()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnOK
        ' 
        btnOK.Location = New Point(260, 243)
        btnOK.Name = "btnOK"
        btnOK.Padding = New Padding(3, 0, 3, 0)
        btnOK.Size = New Size(52, 30)
        btnOK.TabIndex = 13
        btnOK.Text = "恢复"
        btnOK.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(318, 243)
        btnCancel.Name = "btnCancel"
        btnCancel.Padding = New Padding(3, 0, 3, 0)
        btnCancel.Size = New Size(52, 30)
        btnCancel.TabIndex = 12
        btnCancel.Text = "取消"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Controls.Add(lblTitle, 0, 0)
        TableLayoutPanel1.Controls.Add(rbFontConfig, 0, 1)
        TableLayoutPanel1.Controls.Add(rbConfig, 0, 2)
        TableLayoutPanel1.Controls.Add(splitExport, 0, 3)
        TableLayoutPanel1.Controls.Add(Label2, 0, 4)
        TableLayoutPanel1.Location = New Point(12, 12)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 5
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New Size(358, 207)
        TableLayoutPanel1.TabIndex = 11
        ' 
        ' lblTitle
        ' 
        lblTitle.Dock = DockStyle.Fill
        lblTitle.ForeColor = SystemColors.Highlight
        lblTitle.Location = New Point(0, 0)
        lblTitle.Margin = New Padding(0, 0, 0, 3)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(358, 18)
        lblTitle.TabIndex = 0
        lblTitle.Text = "选择恢复默认的模式: "
        ' 
        ' rbFontConfig
        ' 
        rbFontConfig.Appearance = Appearance.Button
        rbFontConfig.Dock = DockStyle.Fill
        rbFontConfig.Image = CType(resources.GetObject("rbFontConfig.Image"), Image)
        rbFontConfig.ImageAlign = ContentAlignment.MiddleLeft
        rbFontConfig.Location = New Point(0, 24)
        rbFontConfig.Margin = New Padding(0, 3, 0, 3)
        rbFontConfig.Name = "rbFontConfig"
        rbFontConfig.Padding = New Padding(3, 0, 3, 0)
        rbFontConfig.Size = New Size(358, 46)
        rbFontConfig.TabIndex = 1
        rbFontConfig.TabStop = True
        rbFontConfig.Text = "恢复默认字体, 默认配置" & vbCrLf & "(删除导入的字体, 恢复官方字体和配置)"
        rbFontConfig.TextImageRelation = TextImageRelation.ImageBeforeText
        rbFontConfig.UseVisualStyleBackColor = True
        ' 
        ' rbConfig
        ' 
        rbConfig.Appearance = Appearance.Button
        rbConfig.Dock = DockStyle.Fill
        rbConfig.Image = CType(resources.GetObject("rbConfig.Image"), Image)
        rbConfig.ImageAlign = ContentAlignment.MiddleLeft
        rbConfig.Location = New Point(0, 76)
        rbConfig.Margin = New Padding(0, 3, 0, 3)
        rbConfig.Name = "rbConfig"
        rbConfig.Padding = New Padding(3, 0, 3, 0)
        rbConfig.Size = New Size(358, 46)
        rbConfig.TabIndex = 2
        rbConfig.TabStop = True
        rbConfig.Text = "恢复默认配置" & vbCrLf & "(保留导入字体, 恢复官方配置)"
        rbConfig.TextImageRelation = TextImageRelation.ImageBeforeText
        rbConfig.UseVisualStyleBackColor = True
        ' 
        ' splitExport
        ' 
        splitExport.Dock = DockStyle.Fill
        splitExport.ForeColor = Color.DarkGray
        splitExport.Location = New Point(3, 128)
        splitExport.Margin = New Padding(3)
        splitExport.Name = "splitExport"
        splitExport.Size = New Size(352, 16)
        splitExport.TabIndex = 36
        splitExport.Text = "─────────────────────────────────────────────────────────"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Dock = DockStyle.Top
        Label2.ForeColor = Color.DarkGray
        Label2.Location = New Point(0, 150)
        Label2.Margin = New Padding(0, 3, 0, 3)
        Label2.Name = "Label2"
        Label2.Size = New Size(358, 54)
        Label2.TabIndex = 37
        Label2.Text = "* 选择「恢复默认字体, 默认配置」: 字体将在下次启动时恢复; 配置文件立即恢复" & vbCrLf & "* 选择「恢复默认配置」: 配置文件立即恢复"
        ' 
        ' DialogRestore
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 285)
        Controls.Add(btnOK)
        Controls.Add(btnCancel)
        Controls.Add(TableLayoutPanel1)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogRestore"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "恢复默认"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblTitle As Label
    Friend WithEvents rbFontConfig As RadioButton
    Friend WithEvents rbConfig As RadioButton
    Friend WithEvents splitExport As Label
    Friend WithEvents Label2 As Label
End Class
