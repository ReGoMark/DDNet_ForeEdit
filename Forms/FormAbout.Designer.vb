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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAbout))
        lblVersion = New Label()
        lblCopyright = New Label()
        splitDirectory = New Label()
        lblTeeworlds = New Label()
        Label10 = New Label()
        lblMail = New Label()
        lblSponsor = New Label()
        lblLicense = New Label()
        lblSarasaSans = New Label()
        lblDDNet = New Label()
        lblThanks = New Label()
        btnOK = New Button()
        lblGithub = New Label()
        lbldotNET10 = New Label()
        ToolTip1 = New ToolTip(components)
        lblGitee = New Label()
        lblBilibili = New Label()
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' lblVersion
        ' 
        lblVersion.AutoSize = True
        lblVersion.Location = New Point(12, 60)
        lblVersion.Margin = New Padding(3)
        lblVersion.Name = "lblVersion"
        lblVersion.Size = New Size(212, 18)
        lblVersion.TabIndex = 1
        lblVersion.Text = "版本 26H2 (Build xxxxxx.x)"
        ' 
        ' lblCopyright
        ' 
        lblCopyright.AutoSize = True
        lblCopyright.Location = New Point(12, 84)
        lblCopyright.Margin = New Padding(3)
        lblCopyright.Name = "lblCopyright"
        lblCopyright.Size = New Size(298, 18)
        lblCopyright.TabIndex = 2
        lblCopyright.Text = "(C) 2018-2026 ReGoMark, 保留所有权利。"
        ' 
        ' splitDirectory
        ' 
        splitDirectory.ForeColor = Color.DarkGray
        splitDirectory.Location = New Point(12, 108)
        splitDirectory.Margin = New Padding(3)
        splitDirectory.Name = "splitDirectory"
        splitDirectory.Size = New Size(358, 18)
        splitDirectory.TabIndex = 4
        splitDirectory.Text = "第三方资源引用  ───────────────────────────────────────────────────────────"
        splitDirectory.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblTeeworlds
        ' 
        lblTeeworlds.AutoEllipsis = True
        lblTeeworlds.AutoSize = True
        lblTeeworlds.Cursor = Cursors.Hand
        lblTeeworlds.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblTeeworlds.Location = New Point(194, 156)
        lblTeeworlds.Margin = New Padding(3)
        lblTeeworlds.Name = "lblTeeworlds"
        lblTeeworlds.Size = New Size(80, 18)
        lblTeeworlds.TabIndex = 11
        lblTeeworlds.Text = "Teeworlds"
        ToolTip1.SetToolTip(lblTeeworlds, "访问「Teeworlds」官方仓库")
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkGray
        Label10.Location = New Point(12, 180)
        Label10.Margin = New Padding(3)
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
        lblMail.Location = New Point(12, 228)
        lblMail.Margin = New Padding(3)
        lblMail.Name = "lblMail"
        lblMail.Size = New Size(64, 18)
        lblMail.TabIndex = 14
        lblMail.Text = "联系作者"
        ToolTip1.SetToolTip(lblMail, "通过邮件联系作者")
        ' 
        ' lblSponsor
        ' 
        lblSponsor.AutoSize = True
        lblSponsor.Cursor = Cursors.Hand
        lblSponsor.ForeColor = SystemColors.Highlight
        lblSponsor.Location = New Point(152, 228)
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
        lblLicense.Location = New Point(180, 204)
        lblLicense.Margin = New Padding(3)
        lblLicense.Name = "lblLicense"
        lblLicense.Size = New Size(64, 18)
        lblLicense.TabIndex = 17
        lblLicense.Text = "许可协议"
        ToolTip1.SetToolTip(lblLicense, "查看用户许可协议")
        ' 
        ' lblSarasaSans
        ' 
        lblSarasaSans.AutoEllipsis = True
        lblSarasaSans.AutoSize = True
        lblSarasaSans.Cursor = Cursors.Hand
        lblSarasaSans.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblSarasaSans.Location = New Point(12, 132)
        lblSarasaSans.Margin = New Padding(3)
        lblSarasaSans.Name = "lblSarasaSans"
        lblSarasaSans.Size = New Size(192, 18)
        lblSarasaSans.TabIndex = 36
        lblSarasaSans.Text = "Sarasa Gothic (更纱黑体)"
        ToolTip1.SetToolTip(lblSarasaSans, "访问「更纱黑体」官方仓库")
        ' 
        ' lblDDNet
        ' 
        lblDDNet.AutoEllipsis = True
        lblDDNet.AutoSize = True
        lblDDNet.Cursor = Cursors.Hand
        lblDDNet.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblDDNet.Location = New Point(12, 156)
        lblDDNet.Margin = New Padding(3)
        lblDDNet.Name = "lblDDNet"
        lblDDNet.Size = New Size(176, 18)
        lblDDNet.TabIndex = 37
        lblDDNet.Text = "DDNet (DDraceNetwork)"
        ToolTip1.SetToolTip(lblDDNet, "访问「DDNet」官方仓库")
        ' 
        ' lblThanks
        ' 
        lblThanks.AutoSize = True
        lblThanks.Cursor = Cursors.Hand
        lblThanks.ForeColor = SystemColors.Highlight
        lblThanks.Location = New Point(194, 228)
        lblThanks.Margin = New Padding(3)
        lblThanks.Name = "lblThanks"
        lblThanks.Size = New Size(36, 18)
        lblThanks.TabIndex = 38
        lblThanks.Text = "鸣谢"
        ToolTip1.SetToolTip(lblThanks, "查看致谢名单")
        ' 
        ' btnOK
        ' 
        btnOK.Location = New Point(318, 252)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(52, 30)
        btnOK.TabIndex = 39
        btnOK.Text = "确定"
        btnOK.UseVisualStyleBackColor = True
        ' 
        ' lblGithub
        ' 
        lblGithub.AutoSize = True
        lblGithub.Cursor = Cursors.Hand
        lblGithub.ForeColor = SystemColors.Highlight
        lblGithub.Location = New Point(12, 204)
        lblGithub.Margin = New Padding(3)
        lblGithub.Name = "lblGithub"
        lblGithub.Size = New Size(56, 18)
        lblGithub.TabIndex = 40
        lblGithub.Text = "Github"
        ToolTip1.SetToolTip(lblGithub, "访问本项目仓库主页")
        ' 
        ' lbldotNET10
        ' 
        lbldotNET10.AutoSize = True
        lbldotNET10.Cursor = Cursors.Hand
        lbldotNET10.ForeColor = SystemColors.Highlight
        lbldotNET10.ImageAlign = ContentAlignment.MiddleRight
        lbldotNET10.Location = New Point(250, 204)
        lbldotNET10.Name = "lbldotNET10"
        lbldotNET10.Size = New Size(64, 18)
        lbldotNET10.TabIndex = 41
        lbldotNET10.Text = ".NET 10"
        lbldotNET10.TextAlign = ContentAlignment.MiddleRight
        ToolTip1.SetToolTip(lbldotNET10, "了解有关 .NET 10 的最新动态")
        ' 
        ' lblGitee
        ' 
        lblGitee.AutoSize = True
        lblGitee.Cursor = Cursors.Hand
        lblGitee.ForeColor = SystemColors.Highlight
        lblGitee.Location = New Point(74, 204)
        lblGitee.Margin = New Padding(3)
        lblGitee.Name = "lblGitee"
        lblGitee.Size = New Size(100, 18)
        lblGitee.TabIndex = 43
        lblGitee.Text = "Gitee (国内)"
        ToolTip1.SetToolTip(lblGitee, "访问本项目仓库主页(国内)")
        ' 
        ' lblBilibili
        ' 
        lblBilibili.AutoSize = True
        lblBilibili.Cursor = Cursors.Hand
        lblBilibili.ForeColor = SystemColors.Highlight
        lblBilibili.Location = New Point(82, 228)
        lblBilibili.Margin = New Padding(3)
        lblBilibili.Name = "lblBilibili"
        lblBilibili.Size = New Size(64, 18)
        lblBilibili.TabIndex = 44
        lblBilibili.Text = "哔哩哔哩"
        ToolTip1.SetToolTip(lblBilibili, "查看哔哩哔哩主页")
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Consolas", 9F, FontStyle.Bold)
        Label1.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label1.Location = New Point(12, 12)
        Label1.Margin = New Padding(3)
        Label1.Name = "Label1"
        Label1.Size = New Size(120, 18)
        Label1.TabIndex = 42
        Label1.Text = "DDNet ForeEdit"
        ' 
        ' FormAbout
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 294)
        Controls.Add(lblBilibili)
        Controls.Add(lblGitee)
        Controls.Add(Label1)
        Controls.Add(lbldotNET10)
        Controls.Add(lblGithub)
        Controls.Add(btnOK)
        Controls.Add(lblThanks)
        Controls.Add(lblDDNet)
        Controls.Add(lblSarasaSans)
        Controls.Add(lblLicense)
        Controls.Add(lblSponsor)
        Controls.Add(lblMail)
        Controls.Add(Label10)
        Controls.Add(lblTeeworlds)
        Controls.Add(splitDirectory)
        Controls.Add(lblCopyright)
        Controls.Add(lblVersion)
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
    Friend WithEvents lblVersion As Label
    Friend WithEvents lblCopyright As Label
    Friend WithEvents splitDirectory As Label
    Friend WithEvents lblTeeworlds As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents lblMail As Label
    Friend WithEvents lblSponsor As Label
    Friend WithEvents lblLicense As Label
    Friend WithEvents lblSarasaSans As Label
    Friend WithEvents lblDDNet As Label
    Friend WithEvents lblThanks As Label
    Friend WithEvents btnOK As Button
    Friend WithEvents lblGithub As Label
    Friend WithEvents lbldotNET10 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label1 As Label
    Friend WithEvents lblGitee As Label
    Friend WithEvents lblBilibili As Label
End Class
