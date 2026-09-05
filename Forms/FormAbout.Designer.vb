<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAbout
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAbout))
        lblName = New Label()
        lblVersion = New Label()
        lblCopyright = New Label()
        splitDirectory = New Label()
        Label6 = New Label()
        Label9 = New Label()
        Label10 = New Label()
        lblMail = New Label()
        lblSponsor = New Label()
        lblLicense = New Label()
        Label14 = New Label()
        Label16 = New Label()
        lblThanks = New Label()
        btnApply = New Button()
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' lblName
        ' 
        lblName.AutoSize = True
        lblName.Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        lblName.Location = New Point(12, 12)
        lblName.Margin = New Padding(3)
        lblName.Name = "lblName"
        lblName.Size = New Size(120, 18)
        lblName.TabIndex = 0
        lblName.Text = "DDNet ForeEdit"
        ' 
        ' lblVersion
        ' 
        lblVersion.AutoSize = True
        lblVersion.Location = New Point(12, 66)
        lblVersion.Margin = New Padding(3)
        lblVersion.Name = "lblVersion"
        lblVersion.Size = New Size(212, 18)
        lblVersion.TabIndex = 1
        lblVersion.Text = "版本 26H2 (Build xxxxxx.x)"
        ' 
        ' lblCopyright
        ' 
        lblCopyright.AutoSize = True
        lblCopyright.Location = New Point(12, 90)
        lblCopyright.Margin = New Padding(3)
        lblCopyright.Name = "lblCopyright"
        lblCopyright.Size = New Size(298, 18)
        lblCopyright.TabIndex = 2
        lblCopyright.Text = "(C) 2018-2026 ReGoMark, 保留所有权利。"
        ' 
        ' splitDirectory
        ' 
        splitDirectory.ForeColor = Color.Silver
        splitDirectory.Location = New Point(12, 117)
        splitDirectory.Margin = New Padding(3, 6, 3, 6)
        splitDirectory.Name = "splitDirectory"
        splitDirectory.Size = New Size(358, 18)
        splitDirectory.TabIndex = 4
        splitDirectory.Text = "第三方资源引用  ───────────────────────────────────────────────────────────"
        splitDirectory.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.Silver
        Label6.Location = New Point(12, 39)
        Label6.Margin = New Padding(3, 6, 3, 6)
        Label6.Name = "Label6"
        Label6.Size = New Size(358, 18)
        Label6.TabIndex = 8
        Label6.Text = "不可见分割线  ───────────────────────────────────────────────────────────"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        Label6.Visible = False
        ' 
        ' Label9
        ' 
        Label9.AutoEllipsis = True
        Label9.AutoSize = True
        Label9.Cursor = Cursors.Hand
        Label9.ForeColor = SystemColors.Highlight
        Label9.Location = New Point(166, 168)
        Label9.Margin = New Padding(3)
        Label9.Name = "Label9"
        Label9.Size = New Size(116, 18)
        Label9.TabIndex = 11
        Label9.Text = "Teeworlds-图标"
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.Silver
        Label10.Location = New Point(12, 195)
        Label10.Margin = New Padding(3, 6, 3, 6)
        Label10.Name = "Label10"
        Label10.Size = New Size(358, 18)
        Label10.TabIndex = 12
        Label10.Text = "联络  ───────────────────────────────────────────────────────────"
        Label10.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblMail
        ' 
        lblMail.AutoSize = True
        lblMail.Cursor = Cursors.Hand
        lblMail.ForeColor = SystemColors.Highlight
        lblMail.Location = New Point(12, 222)
        lblMail.Margin = New Padding(3)
        lblMail.Name = "lblMail"
        lblMail.Size = New Size(64, 18)
        lblMail.TabIndex = 14
        lblMail.Text = "联系作者"
        ' 
        ' lblSponsor
        ' 
        lblSponsor.AutoSize = True
        lblSponsor.Cursor = Cursors.Hand
        lblSponsor.ForeColor = SystemColors.Highlight
        lblSponsor.Location = New Point(186, 222)
        lblSponsor.Margin = New Padding(3)
        lblSponsor.Name = "lblSponsor"
        lblSponsor.Size = New Size(36, 18)
        lblSponsor.TabIndex = 15
        lblSponsor.Text = "赞助"
        ' 
        ' lblLicense
        ' 
        lblLicense.AutoSize = True
        lblLicense.Cursor = Cursors.Hand
        lblLicense.ForeColor = SystemColors.Highlight
        lblLicense.Location = New Point(228, 222)
        lblLicense.Margin = New Padding(3)
        lblLicense.Name = "lblLicense"
        lblLicense.Size = New Size(64, 18)
        lblLicense.TabIndex = 17
        lblLicense.Text = "许可协议"
        ' 
        ' Label14
        ' 
        Label14.AutoEllipsis = True
        Label14.AutoSize = True
        Label14.Cursor = Cursors.Hand
        Label14.ForeColor = SystemColors.Highlight
        Label14.Location = New Point(12, 144)
        Label14.Margin = New Padding(3)
        Label14.Name = "Label14"
        Label14.Size = New Size(176, 18)
        Label14.TabIndex = 36
        Label14.Text = "Sarasa Gothic-更纱黑体"
        ' 
        ' Label16
        ' 
        Label16.AutoEllipsis = True
        Label16.AutoSize = True
        Label16.Cursor = Cursors.Hand
        Label16.ForeColor = SystemColors.Highlight
        Label16.Location = New Point(12, 168)
        Label16.Margin = New Padding(3)
        Label16.Name = "Label16"
        Label16.Size = New Size(148, 18)
        Label16.TabIndex = 37
        Label16.Text = "DDRaceNetwork-图标"
        ' 
        ' lblThanks
        ' 
        lblThanks.AutoSize = True
        lblThanks.Cursor = Cursors.Hand
        lblThanks.ForeColor = SystemColors.Highlight
        lblThanks.Location = New Point(144, 222)
        lblThanks.Margin = New Padding(3)
        lblThanks.Name = "lblThanks"
        lblThanks.Size = New Size(36, 18)
        lblThanks.TabIndex = 38
        lblThanks.Text = "鸣谢"
        ' 
        ' btnApply
        ' 
        btnApply.Location = New Point(318, 246)
        btnApply.Name = "btnApply"
        btnApply.Size = New Size(52, 30)
        btnApply.TabIndex = 39
        btnApply.Text = "确定"
        btnApply.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Cursor = Cursors.Hand
        Label1.ForeColor = SystemColors.Highlight
        Label1.Location = New Point(82, 222)
        Label1.Margin = New Padding(3)
        Label1.Name = "Label1"
        Label1.Size = New Size(56, 18)
        Label1.TabIndex = 40
        Label1.Text = "Github"
        ' 
        ' FormAbout
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 288)
        Controls.Add(Label1)
        Controls.Add(btnApply)
        Controls.Add(lblThanks)
        Controls.Add(Label16)
        Controls.Add(Label14)
        Controls.Add(lblLicense)
        Controls.Add(lblSponsor)
        Controls.Add(lblMail)
        Controls.Add(Label10)
        Controls.Add(Label9)
        Controls.Add(Label6)
        Controls.Add(splitDirectory)
        Controls.Add(lblCopyright)
        Controls.Add(lblVersion)
        Controls.Add(lblName)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "FormAbout"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "关于"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblName As Label
    Friend WithEvents lblVersion As Label
    Friend WithEvents lblCopyright As Label
    Friend WithEvents splitDirectory As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents lblMail As Label
    Friend WithEvents lblSponsor As Label
    Friend WithEvents lblLicense As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents lblThanks As Label
    Friend WithEvents btnApply As Button
    Friend WithEvents Label1 As Label
End Class
