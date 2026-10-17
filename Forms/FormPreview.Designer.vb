<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPreview
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPreview))
        lblTC = New Label()
        rdoFont10 = New RadioButton()
        rdoFont16 = New RadioButton()
        rdoFont12 = New RadioButton()
        btnConfirm = New Button()
        btnRecovery = New Button()
        tbCustom = New TextBox()
        lblSC = New Label()
        lblKR = New Label()
        lblJP = New Label()
        lblLA = New Label()
        tagTC = New Label()
        tagSC = New Label()
        tagKR = New Label()
        tagJP = New Label()
        tagLA = New Label()
        TableLayoutPanel4 = New TableLayoutPanel()
        TableLayoutPanel1 = New TableLayoutPanel()
        rdoFont9 = New RadioButton()
        TableLayoutPanel2 = New TableLayoutPanel()
        rdoFont22 = New RadioButton()
        TableLayoutPanel4.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblTC
        ' 
        lblTC.AutoSize = True
        lblTC.BackColor = Color.Transparent
        lblTC.Dock = DockStyle.Fill
        lblTC.Font = New Font("等距更纱黑体 Slab SC", 9F)
        lblTC.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblTC.Location = New Point(0, 606)
        lblTC.Margin = New Padding(0, 3, 0, 3)
        lblTC.Name = "lblTC"
        lblTC.Size = New Size(403, 90)
        lblTC.TabIndex = 4
        lblTC.Text = "幾次相忘於世，總在山窮水盡相見。" & vbCrLf & "0123456789 ~！@#￥%……&*（）—+「」" & vbCrLf & "：『』《》？·-=【】；‘，。、" & vbCrLf & "😀😁😂" & ChrW(55358) & ChrW(56611) & "😊😎😍🤔😴" & ChrW(55358) & ChrW(56623) & ChrW(55358) & ChrW(56691) & "😭" & vbCrLf & "╭ ─ │╰ ─ ᴄᴜʀʀᴇɴᴛ ᴍᴀᴘ ★★★✰✰"
        lblTC.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' rdoFont10
        ' 
        rdoFont10.Appearance = Appearance.Button
        rdoFont10.FlatAppearance.BorderSize = 0
        rdoFont10.FlatAppearance.CheckedBackColor = Color.Silver
        rdoFont10.Location = New Point(65, 0)
        rdoFont10.Margin = New Padding(0, 0, 3, 3)
        rdoFont10.Name = "rdoFont10"
        rdoFont10.Padding = New Padding(3, 0, 3, 0)
        rdoFont10.Size = New Size(62, 30)
        rdoFont10.TabIndex = 0
        rdoFont10.TabStop = True
        rdoFont10.Text = "10 点"
        rdoFont10.TextAlign = ContentAlignment.MiddleCenter
        rdoFont10.UseVisualStyleBackColor = True
        ' 
        ' rdoFont16
        ' 
        rdoFont16.Appearance = Appearance.Button
        rdoFont16.FlatAppearance.BorderSize = 0
        rdoFont16.FlatAppearance.CheckedBackColor = Color.Silver
        rdoFont16.Location = New Point(201, 0)
        rdoFont16.Margin = New Padding(3, 0, 3, 3)
        rdoFont16.Name = "rdoFont16"
        rdoFont16.Padding = New Padding(3, 0, 3, 0)
        rdoFont16.Size = New Size(62, 30)
        rdoFont16.TabIndex = 2
        rdoFont16.TabStop = True
        rdoFont16.Text = "16 点"
        rdoFont16.TextAlign = ContentAlignment.MiddleCenter
        rdoFont16.UseVisualStyleBackColor = True
        ' 
        ' rdoFont12
        ' 
        rdoFont12.Appearance = Appearance.Button
        rdoFont12.FlatAppearance.BorderSize = 0
        rdoFont12.FlatAppearance.CheckedBackColor = Color.Silver
        rdoFont12.Location = New Point(133, 0)
        rdoFont12.Margin = New Padding(3, 0, 3, 3)
        rdoFont12.Name = "rdoFont12"
        rdoFont12.Padding = New Padding(3, 0, 3, 0)
        rdoFont12.Size = New Size(62, 30)
        rdoFont12.TabIndex = 1
        rdoFont12.TabStop = True
        rdoFont12.Text = "12 点"
        rdoFont12.TextAlign = ContentAlignment.MiddleCenter
        rdoFont12.UseVisualStyleBackColor = True
        ' 
        ' btnConfirm
        ' 
        btnConfirm.Location = New Point(6, 3)
        btnConfirm.Margin = New Padding(6, 3, 0, 3)
        btnConfirm.Name = "btnConfirm"
        btnConfirm.Padding = New Padding(3, 0, 3, 0)
        btnConfirm.Size = New Size(52, 30)
        btnConfirm.TabIndex = 4
        btnConfirm.Text = "确认"
        btnConfirm.UseVisualStyleBackColor = True
        ' 
        ' btnRecovery
        ' 
        btnRecovery.Location = New Point(6, 39)
        btnRecovery.Margin = New Padding(6, 3, 0, 3)
        btnRecovery.Name = "btnRecovery"
        btnRecovery.Padding = New Padding(3, 0, 3, 0)
        btnRecovery.Size = New Size(52, 30)
        btnRecovery.TabIndex = 5
        btnRecovery.Text = "还原"
        btnRecovery.UseVisualStyleBackColor = True
        ' 
        ' tbCustom
        ' 
        TableLayoutPanel1.SetColumnSpan(tbCustom, 5)
        tbCustom.Dock = DockStyle.Fill
        tbCustom.ForeColor = Color.Black
        tbCustom.Location = New Point(0, 36)
        tbCustom.Margin = New Padding(0, 3, 0, 3)
        tbCustom.Multiline = True
        tbCustom.Name = "tbCustom"
        tbCustom.PlaceholderText = "输入要预览的文本"
        tbCustom.ScrollBars = ScrollBars.Vertical
        tbCustom.Size = New Size(331, 66)
        tbCustom.TabIndex = 3
        ' 
        ' lblSC
        ' 
        lblSC.AutoSize = True
        lblSC.Dock = DockStyle.Fill
        lblSC.Font = New Font("等距更纱黑体 Slab SC", 9F)
        lblSC.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblSC.Location = New Point(0, 480)
        lblSC.Margin = New Padding(0, 3, 0, 3)
        lblSC.Name = "lblSC"
        lblSC.Size = New Size(403, 90)
        lblSC.TabIndex = 3
        lblSC.Text = "弱水三千、巫山十二，指点虚无归路。" & vbCrLf & "0123456789 ~！@#￥%……&*（）—+「」" & vbCrLf & "：" & ChrW(8220) & ChrW(8221) & "《》？·-=【】；‘，。、" & vbCrLf & "😀😁😂" & ChrW(55358) & ChrW(56611) & "😊😎😍🤔😴" & ChrW(55358) & ChrW(56623) & ChrW(55358) & ChrW(56691) & "😭" & vbCrLf & "╭ ─ │╰ ─ ᴄᴜʀʀᴇɴᴛ ᴍᴀᴘ ★★★✰✰"
        lblSC.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblKR
        ' 
        lblKR.AutoSize = True
        lblKR.BackColor = Color.Transparent
        lblKR.Dock = DockStyle.Fill
        lblKR.Font = New Font("等距更纱黑体 Slab SC", 9F)
        lblKR.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblKR.Location = New Point(0, 372)
        lblKR.Margin = New Padding(0, 3, 0, 3)
        lblKR.Name = "lblKR"
        lblKR.Size = New Size(403, 72)
        lblKR.TabIndex = 2
        lblKR.Text = "나의 죽음을 헛되이 말라. " & vbCrLf & "0123456789 ~!@#$%^&*()_+{}:""<>?`-=[];',./" & vbCrLf & "😀😁😂" & ChrW(55358) & ChrW(56611) & "😊😎😍🤔😴" & ChrW(55358) & ChrW(56623) & ChrW(55358) & ChrW(56691) & "😭" & vbCrLf & "╭ ─ │╰ ─ ᴄᴜʀʀᴇɴᴛ ᴍᴀᴘ ★★★✰✰"
        lblKR.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblJP
        ' 
        lblJP.AutoSize = True
        lblJP.Dock = DockStyle.Fill
        lblJP.Font = New Font("等距更纱黑体 Slab SC", 9F)
        lblJP.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblJP.Location = New Point(0, 246)
        lblJP.Margin = New Padding(0, 3, 0, 3)
        lblJP.Name = "lblJP"
        lblJP.Size = New Size(403, 90)
        lblJP.TabIndex = 1
        lblJP.Text = "恥の多い生涯を送って来ました。" & vbCrLf & "0123456789 ~！@#￥%……&*（）—+「」" & vbCrLf & "：『』《》？·-=【】；‘，。、" & vbCrLf & "😀😁😂" & ChrW(55358) & ChrW(56611) & "😊😎😍🤔😴" & ChrW(55358) & ChrW(56623) & ChrW(55358) & ChrW(56691) & "😭" & vbCrLf & "╭ ─ │╰ ─ ᴄᴜʀʀᴇɴᴛ ᴍᴀᴘ ★★★✰✰"
        lblJP.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblLA
        ' 
        lblLA.AutoSize = True
        lblLA.BackColor = Color.Transparent
        lblLA.Dock = DockStyle.Fill
        lblLA.Font = New Font("等距更纱黑体 Slab SC", 9F)
        lblLA.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblLA.Location = New Point(0, 138)
        lblLA.Margin = New Padding(0, 3, 0, 3)
        lblLA.Name = "lblLA"
        lblLA.Size = New Size(403, 72)
        lblLA.TabIndex = 0
        lblLA.Text = "Once we dreamt that we were strangers." & vbCrLf & "0123456789 ~!@#$%^&*()_+{}:""<>?`-=[];',./" & vbCrLf & "😀😁😂" & ChrW(55358) & ChrW(56611) & "😊😎😍🤔😴" & ChrW(55358) & ChrW(56623) & ChrW(55358) & ChrW(56691) & "😭" & vbCrLf & "╭ ─ │╰ ─ ᴄᴜʀʀᴇɴᴛ ᴍᴀᴘ ★★★✰✰"
        lblLA.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagTC
        ' 
        tagTC.AutoSize = True
        tagTC.BackColor = Color.Transparent
        tagTC.Cursor = Cursors.Hand
        tagTC.Dock = DockStyle.Left
        tagTC.ForeColor = Color.DarkGray
        tagTC.Location = New Point(0, 579)
        tagTC.Margin = New Padding(0, 6, 0, 6)
        tagTC.Name = "tagTC"
        tagTC.Size = New Size(381, 18)
        tagTC.TabIndex = 9
        tagTC.Text = "繁体中文  ───────────────────────────────────────────"
        tagTC.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagSC
        ' 
        tagSC.AutoSize = True
        tagSC.Cursor = Cursors.Hand
        tagSC.Dock = DockStyle.Left
        tagSC.ForeColor = Color.DarkGray
        tagSC.Location = New Point(0, 453)
        tagSC.Margin = New Padding(0, 6, 0, 6)
        tagSC.Name = "tagSC"
        tagSC.Size = New Size(381, 18)
        tagSC.TabIndex = 8
        tagSC.Text = "简体中文  ───────────────────────────────────────────"
        tagSC.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagKR
        ' 
        tagKR.AutoSize = True
        tagKR.BackColor = Color.Transparent
        tagKR.Cursor = Cursors.Hand
        tagKR.Dock = DockStyle.Left
        tagKR.ForeColor = Color.DarkGray
        tagKR.Location = New Point(0, 345)
        tagKR.Margin = New Padding(0, 6, 0, 6)
        tagKR.Name = "tagKR"
        tagKR.Size = New Size(381, 18)
        tagKR.TabIndex = 7
        tagKR.Text = "韩文  ───────────────────────────────────────────────"
        tagKR.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagJP
        ' 
        tagJP.AutoSize = True
        tagJP.Cursor = Cursors.Hand
        tagJP.Dock = DockStyle.Left
        tagJP.ForeColor = Color.DarkGray
        tagJP.Location = New Point(0, 219)
        tagJP.Margin = New Padding(0, 6, 0, 6)
        tagJP.Name = "tagJP"
        tagJP.Size = New Size(381, 18)
        tagJP.TabIndex = 6
        tagJP.Text = "日文  ───────────────────────────────────────────────"
        tagJP.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagLA
        ' 
        tagLA.AutoSize = True
        tagLA.BackColor = Color.Transparent
        tagLA.Cursor = Cursors.Hand
        tagLA.Dock = DockStyle.Left
        tagLA.ForeColor = Color.DarkGray
        tagLA.Location = New Point(0, 111)
        tagLA.Margin = New Padding(0, 6, 0, 6)
        tagLA.Name = "tagLA"
        tagLA.Size = New Size(381, 18)
        tagLA.TabIndex = 5
        tagLA.Text = "默认西文  ───────────────────────────────────────────"
        tagLA.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TableLayoutPanel4
        ' 
        TableLayoutPanel4.AutoSize = True
        TableLayoutPanel4.ColumnCount = 1
        TableLayoutPanel4.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel4.Controls.Add(TableLayoutPanel1, 0, 0)
        TableLayoutPanel4.Controls.Add(lblTC, 0, 10)
        TableLayoutPanel4.Controls.Add(lblSC, 0, 8)
        TableLayoutPanel4.Controls.Add(lblKR, 0, 6)
        TableLayoutPanel4.Controls.Add(lblJP, 0, 4)
        TableLayoutPanel4.Controls.Add(lblLA, 0, 2)
        TableLayoutPanel4.Controls.Add(tagLA, 0, 1)
        TableLayoutPanel4.Controls.Add(tagJP, 0, 3)
        TableLayoutPanel4.Controls.Add(tagKR, 0, 5)
        TableLayoutPanel4.Controls.Add(tagSC, 0, 7)
        TableLayoutPanel4.Controls.Add(tagTC, 0, 9)
        TableLayoutPanel4.Dock = DockStyle.Fill
        TableLayoutPanel4.Location = New Point(12, 12)
        TableLayoutPanel4.Name = "TableLayoutPanel4"
        TableLayoutPanel4.RowCount = 12
        TableLayoutPanel4.RowStyles.Add(New RowStyle())
        TableLayoutPanel4.RowStyles.Add(New RowStyle())
        TableLayoutPanel4.RowStyles.Add(New RowStyle())
        TableLayoutPanel4.RowStyles.Add(New RowStyle())
        TableLayoutPanel4.RowStyles.Add(New RowStyle())
        TableLayoutPanel4.RowStyles.Add(New RowStyle())
        TableLayoutPanel4.RowStyles.Add(New RowStyle())
        TableLayoutPanel4.RowStyles.Add(New RowStyle())
        TableLayoutPanel4.RowStyles.Add(New RowStyle())
        TableLayoutPanel4.RowStyles.Add(New RowStyle())
        TableLayoutPanel4.RowStyles.Add(New RowStyle())
        TableLayoutPanel4.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel4.Size = New Size(403, 669)
        TableLayoutPanel4.TabIndex = 3
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.AutoSize = True
        TableLayoutPanel1.ColumnCount = 6
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(rdoFont9, 0, 0)
        TableLayoutPanel1.Controls.Add(tbCustom, 0, 1)
        TableLayoutPanel1.Controls.Add(TableLayoutPanel2, 5, 1)
        TableLayoutPanel1.Controls.Add(rdoFont16, 3, 0)
        TableLayoutPanel1.Controls.Add(rdoFont12, 2, 0)
        TableLayoutPanel1.Controls.Add(rdoFont10, 1, 0)
        TableLayoutPanel1.Controls.Add(rdoFont22, 4, 0)
        TableLayoutPanel1.Dock = DockStyle.Top
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Margin = New Padding(0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 2
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New Size(403, 105)
        TableLayoutPanel1.TabIndex = 6
        ' 
        ' rdoFont9
        ' 
        rdoFont9.Appearance = Appearance.Button
        rdoFont9.FlatAppearance.BorderSize = 0
        rdoFont9.FlatAppearance.CheckedBackColor = Color.Silver
        rdoFont9.Location = New Point(0, 0)
        rdoFont9.Margin = New Padding(0, 0, 3, 3)
        rdoFont9.Name = "rdoFont9"
        rdoFont9.Padding = New Padding(3, 0, 3, 0)
        rdoFont9.Size = New Size(62, 30)
        rdoFont9.TabIndex = 9
        rdoFont9.TabStop = True
        rdoFont9.Text = "9 点"
        rdoFont9.TextAlign = ContentAlignment.MiddleCenter
        rdoFont9.UseVisualStyleBackColor = True
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.AutoSize = True
        TableLayoutPanel2.ColumnCount = 1
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        TableLayoutPanel2.Controls.Add(btnRecovery, 0, 1)
        TableLayoutPanel2.Controls.Add(btnConfirm, 0, 0)
        TableLayoutPanel2.Dock = DockStyle.Fill
        TableLayoutPanel2.Location = New Point(331, 33)
        TableLayoutPanel2.Margin = New Padding(0)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 2
        TableLayoutPanel2.RowStyles.Add(New RowStyle())
        TableLayoutPanel2.RowStyles.Add(New RowStyle())
        TableLayoutPanel2.Size = New Size(72, 72)
        TableLayoutPanel2.TabIndex = 6
        ' 
        ' rdoFont22
        ' 
        rdoFont22.Appearance = Appearance.Button
        rdoFont22.FlatAppearance.BorderSize = 0
        rdoFont22.FlatAppearance.CheckedBackColor = Color.Silver
        rdoFont22.Location = New Point(269, 0)
        rdoFont22.Margin = New Padding(3, 0, 0, 3)
        rdoFont22.Name = "rdoFont22"
        rdoFont22.Padding = New Padding(3, 0, 3, 0)
        rdoFont22.Size = New Size(62, 30)
        rdoFont22.TabIndex = 8
        rdoFont22.TabStop = True
        rdoFont22.Text = "22 点"
        rdoFont22.TextAlign = ContentAlignment.MiddleCenter
        rdoFont22.UseVisualStyleBackColor = True
        ' 
        ' FormPreview
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        AutoScroll = True
        AutoSize = True
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        BackColor = Color.White
        ClientSize = New Size(427, 693)
        Controls.Add(TableLayoutPanel4)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "FormPreview"
        Padding = New Padding(12)
        StartPosition = FormStartPosition.CenterParent
        Text = "预览"
        TableLayoutPanel4.ResumeLayout(False)
        TableLayoutPanel4.PerformLayout()
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        TableLayoutPanel2.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents lblTC As Label
    Friend WithEvents rdoFont10 As RadioButton
    Friend WithEvents rdoFont16 As RadioButton
    Friend WithEvents rdoFont12 As RadioButton
    Friend WithEvents btnConfirm As Button
    Friend WithEvents btnRecovery As Button
    Friend WithEvents tbCustom As TextBox
    Friend WithEvents lblSC As Label
    Friend WithEvents lblKR As Label
    Friend WithEvents lblJP As Label
    Friend WithEvents lblLA As Label
    Friend WithEvents tagTC As Label
    Friend WithEvents tagSC As Label
    Friend WithEvents tagKR As Label
    Friend WithEvents tagJP As Label
    Friend WithEvents tagLA As Label
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents rdoFont22 As RadioButton
    Friend WithEvents rdoFont9 As RadioButton
End Class
