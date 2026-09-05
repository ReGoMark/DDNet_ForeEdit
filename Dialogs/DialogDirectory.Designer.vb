<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogDirectory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogDirectory))
        TableLayoutPanel1 = New TableLayoutPanel()
        lblTitle = New Label()
        rbDDNet = New RadioButton()
        rbTeeworlds = New RadioButton()
        splitExport = New Label()
        Label2 = New Label()
        btnCancel = New Button()
        btnOK = New Button()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.AutoSize = True
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.Controls.Add(lblTitle, 0, 0)
        TableLayoutPanel1.Controls.Add(rbDDNet, 0, 1)
        TableLayoutPanel1.Controls.Add(rbTeeworlds, 0, 2)
        TableLayoutPanel1.Controls.Add(splitExport, 0, 3)
        TableLayoutPanel1.Controls.Add(Label2, 0, 4)
        TableLayoutPanel1.Location = New Point(12, 12)
        TableLayoutPanel1.Margin = New Padding(3, 3, 3, 6)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 5
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New Size(358, 225)
        TableLayoutPanel1.TabIndex = 8
        ' 
        ' lblTitle
        ' 
        TableLayoutPanel1.SetColumnSpan(lblTitle, 2)
        lblTitle.Dock = DockStyle.Fill
        lblTitle.Location = New Point(0, 0)
        lblTitle.Margin = New Padding(0, 0, 0, 3)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(358, 18)
        lblTitle.TabIndex = 0
        lblTitle.Text = "存在多个用户目录, 请选择: "
        ' 
        ' rbDDNet
        ' 
        rbDDNet.Appearance = Appearance.Button
        rbDDNet.AutoSize = True
        TableLayoutPanel1.SetColumnSpan(rbDDNet, 2)
        rbDDNet.Dock = DockStyle.Top
        rbDDNet.Image = CType(resources.GetObject("rbDDNet.Image"), Image)
        rbDDNet.ImageAlign = ContentAlignment.MiddleLeft
        rbDDNet.Location = New Point(0, 24)
        rbDDNet.Margin = New Padding(0, 3, 0, 3)
        rbDDNet.Name = "rbDDNet"
        rbDDNet.Padding = New Padding(3, 0, 3, 0)
        rbDDNet.Size = New Size(358, 46)
        rbDDNet.TabIndex = 1
        rbDDNet.TabStop = True
        rbDDNet.Text = "DDNet 目录" & vbCrLf & "...\Appdata\Roaming\DDNet\fonts\"
        rbDDNet.TextImageRelation = TextImageRelation.ImageBeforeText
        rbDDNet.UseVisualStyleBackColor = True
        ' 
        ' rbTeeworlds
        ' 
        rbTeeworlds.Appearance = Appearance.Button
        rbTeeworlds.AutoSize = True
        TableLayoutPanel1.SetColumnSpan(rbTeeworlds, 2)
        rbTeeworlds.Dock = DockStyle.Top
        rbTeeworlds.Image = CType(resources.GetObject("rbTeeworlds.Image"), Image)
        rbTeeworlds.ImageAlign = ContentAlignment.MiddleLeft
        rbTeeworlds.Location = New Point(0, 76)
        rbTeeworlds.Margin = New Padding(0, 3, 0, 3)
        rbTeeworlds.Name = "rbTeeworlds"
        rbTeeworlds.Padding = New Padding(3, 0, 3, 0)
        rbTeeworlds.Size = New Size(358, 46)
        rbTeeworlds.TabIndex = 2
        rbTeeworlds.TabStop = True
        rbTeeworlds.Text = "Teeworlds 目录" & vbCrLf & "...\Appdata\Roaming\Teeworlds\fonts\"
        rbTeeworlds.TextImageRelation = TextImageRelation.ImageBeforeText
        rbTeeworlds.UseVisualStyleBackColor = True
        ' 
        ' splitExport
        ' 
        TableLayoutPanel1.SetColumnSpan(splitExport, 2)
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
        TableLayoutPanel1.SetColumnSpan(Label2, 2)
        Label2.ForeColor = Color.DarkGray
        Label2.Location = New Point(0, 150)
        Label2.Margin = New Padding(0, 3, 0, 3)
        Label2.Name = "Label2"
        Label2.Size = New Size(328, 54)
        Label2.TabIndex = 37
        Label2.Text = "* DDNet 目录的字体显示优先于 Teeworlds 目录" & vbCrLf & "* 用户目录的字体显示优先于安装目录" & vbCrLf & "* 旧版客户端可能会使用 Teeworlds 目录"
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(318, 246)
        btnCancel.Name = "btnCancel"
        btnCancel.Padding = New Padding(3, 0, 3, 0)
        btnCancel.Size = New Size(52, 30)
        btnCancel.TabIndex = 9
        btnCancel.Text = "取消"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnOK
        ' 
        btnOK.Location = New Point(260, 246)
        btnOK.Name = "btnOK"
        btnOK.Padding = New Padding(3, 0, 3, 0)
        btnOK.Size = New Size(52, 30)
        btnOK.TabIndex = 10
        btnOK.Text = "确定"
        btnOK.UseVisualStyleBackColor = True
        ' 
        ' DialogDirectory
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 288)
        Controls.Add(btnOK)
        Controls.Add(btnCancel)
        Controls.Add(TableLayoutPanel1)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogDirectory"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "用户目录"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblTitle As Label
    Friend WithEvents rbDDNet As RadioButton
    Friend WithEvents rbTeeworlds As RadioButton
    Friend WithEvents Label2 As Label
    Friend WithEvents splitExport As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOK As Button
End Class
