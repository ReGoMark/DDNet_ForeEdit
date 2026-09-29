<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSearch))
        tbKeyword = New TextBox()
        btnNext = New Button()
        Label10 = New Label()
        Button1 = New Button()
        btnTail = New Button()
        btnHead = New Button()
        btnPrior = New Button()
        lblStatus = New Label()
        btnGo = New Button()
        btnSelectAll = New Button()
        cbCases = New CheckBox()
        cbAllfields = New CheckBox()
        cbFormal = New CheckBox()
        ToolTip1 = New ToolTip(components)
        SuspendLayout()
        ' 
        ' tbKeyword
        ' 
        tbKeyword.Location = New Point(12, 12)
        tbKeyword.Multiline = True
        tbKeyword.Name = "tbKeyword"
        tbKeyword.Size = New Size(358, 30)
        tbKeyword.TabIndex = 0
        ' 
        ' btnNext
        ' 
        btnNext.AllowDrop = True
        btnNext.AutoSize = True
        btnNext.Location = New Point(282, 48)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(88, 30)
        btnNext.TabIndex = 2
        btnNext.Text = "查找下一个"
        btnNext.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnNext, "下一个匹配 (F3)")
        btnNext.UseVisualStyleBackColor = True
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.Silver
        Label10.Location = New Point(12, 87)
        Label10.Margin = New Padding(3, 6, 3, 6)
        Label10.Name = "Label10"
        Label10.Size = New Size(358, 18)
        Label10.TabIndex = 13
        Label10.Text = " ───────────────────────────────────────────────────────────"
        Label10.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Button1
        ' 
        Button1.AllowDrop = True
        Button1.AutoSize = True
        Button1.Location = New Point(318, 141)
        Button1.Name = "Button1"
        Button1.Padding = New Padding(3, 0, 3, 0)
        Button1.Size = New Size(52, 30)
        Button1.TabIndex = 15
        Button1.Text = "取消"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' btnTail
        ' 
        btnTail.AllowDrop = True
        btnTail.AutoSize = True
        btnTail.Image = CType(resources.GetObject("btnTail.Image"), Image)
        btnTail.Location = New Point(120, 48)
        btnTail.Name = "btnTail"
        btnTail.Size = New Size(30, 30)
        btnTail.TabIndex = 17
        btnTail.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnTail, "末尾匹配(F4)")
        btnTail.UseVisualStyleBackColor = True
        ' 
        ' btnHead
        ' 
        btnHead.AllowDrop = True
        btnHead.AutoSize = True
        btnHead.Image = CType(resources.GetObject("btnHead.Image"), Image)
        btnHead.Location = New Point(84, 48)
        btnHead.Name = "btnHead"
        btnHead.Size = New Size(30, 30)
        btnHead.TabIndex = 18
        btnHead.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnHead, "首个匹配(F2)")
        btnHead.UseVisualStyleBackColor = True
        ' 
        ' btnPrior
        ' 
        btnPrior.AllowDrop = True
        btnPrior.AutoSize = True
        btnPrior.Image = CType(resources.GetObject("btnPrior.Image"), Image)
        btnPrior.Location = New Point(246, 48)
        btnPrior.Name = "btnPrior"
        btnPrior.Size = New Size(30, 30)
        btnPrior.TabIndex = 19
        btnPrior.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(btnPrior, "上一个匹配(Shift+F3)")
        btnPrior.UseVisualStyleBackColor = True
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoEllipsis = True
        lblStatus.ForeColor = Color.Silver
        lblStatus.Location = New Point(12, 114)
        lblStatus.Margin = New Padding(3, 3, 3, 6)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(358, 18)
        lblStatus.TabIndex = 20
        lblStatus.Text = "信息"
        ' 
        ' btnGo
        ' 
        btnGo.AllowDrop = True
        btnGo.AutoSize = True
        btnGo.Location = New Point(12, 48)
        btnGo.Name = "btnGo"
        btnGo.Padding = New Padding(3, 0, 3, 0)
        btnGo.Size = New Size(66, 30)
        btnGo.TabIndex = 21
        btnGo.Text = "转到行"
        ToolTip1.SetToolTip(btnGo, "按序号跳转(Ctrl+G)")
        btnGo.UseVisualStyleBackColor = True
        ' 
        ' btnSelectAll
        ' 
        btnSelectAll.AllowDrop = True
        btnSelectAll.AutoSize = True
        btnSelectAll.Location = New Point(260, 141)
        btnSelectAll.Name = "btnSelectAll"
        btnSelectAll.Padding = New Padding(3, 0, 3, 0)
        btnSelectAll.Size = New Size(52, 30)
        btnSelectAll.TabIndex = 22
        btnSelectAll.Text = "全选"
        btnSelectAll.UseVisualStyleBackColor = True
        ' 
        ' cbCases
        ' 
        cbCases.Appearance = Appearance.Button
        cbCases.Image = CType(resources.GetObject("cbCases.Image"), Image)
        cbCases.Location = New Point(48, 141)
        cbCases.Name = "cbCases"
        cbCases.Size = New Size(30, 30)
        cbCases.TabIndex = 23
        cbCases.TextAlign = ContentAlignment.MiddleCenter
        cbCases.UseVisualStyleBackColor = True
        ' 
        ' cbAllfields
        ' 
        cbAllfields.Appearance = Appearance.Button
        cbAllfields.Image = CType(resources.GetObject("cbAllfields.Image"), Image)
        cbAllfields.Location = New Point(12, 141)
        cbAllfields.Name = "cbAllfields"
        cbAllfields.Size = New Size(30, 30)
        cbAllfields.TabIndex = 24
        cbAllfields.TextAlign = ContentAlignment.MiddleCenter
        cbAllfields.UseVisualStyleBackColor = True
        ' 
        ' cbFormal
        ' 
        cbFormal.Appearance = Appearance.Button
        cbFormal.Image = CType(resources.GetObject("cbFormal.Image"), Image)
        cbFormal.Location = New Point(84, 141)
        cbFormal.Name = "cbFormal"
        cbFormal.Size = New Size(30, 30)
        cbFormal.TabIndex = 25
        cbFormal.TextAlign = ContentAlignment.MiddleCenter
        cbFormal.UseVisualStyleBackColor = True
        ' 
        ' FormSearch
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 183)
        Controls.Add(cbFormal)
        Controls.Add(cbAllfields)
        Controls.Add(cbCases)
        Controls.Add(btnSelectAll)
        Controls.Add(btnGo)
        Controls.Add(lblStatus)
        Controls.Add(btnPrior)
        Controls.Add(btnHead)
        Controls.Add(btnTail)
        Controls.Add(Button1)
        Controls.Add(Label10)
        Controls.Add(btnNext)
        Controls.Add(tbKeyword)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "FormSearch"
        StartPosition = FormStartPosition.CenterParent
        Text = "查找"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tbKeyword As TextBox
    Friend WithEvents btnNext As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents btnTail As Button
    Friend WithEvents btnHead As Button
    Friend WithEvents btnPrior As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnGo As Button
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents cbCases As CheckBox
    Friend WithEvents cbAllfields As CheckBox
    Friend WithEvents cbFormal As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
End Class
