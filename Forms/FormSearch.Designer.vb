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
        txtKeywords = New TextBox()
        btnNext = New Button()
        btnTail = New Button()
        btnHead = New Button()
        btnPrior = New Button()
        lblStatus = New Label()
        btnTurn = New Button()
        btnSelectAll = New Button()
        cmbCases = New CheckBox()
        cmbAllwords = New CheckBox()
        cmbFormal = New CheckBox()
        ToolTip1 = New ToolTip(components)
        Label10 = New Label()
        SuspendLayout()
        ' 
        ' txtKeywords
        ' 
        txtKeywords.ForeColor = Color.Black
        txtKeywords.Location = New Point(12, 12)
        txtKeywords.Multiline = True
        txtKeywords.Name = "txtKeywords"
        txtKeywords.PlaceholderText = "输入要查找的内容"
        txtKeywords.Size = New Size(358, 30)
        txtKeywords.TabIndex = 0
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
        ' btnTail
        ' 
        btnTail.AllowDrop = True
        btnTail.AutoSize = True
        btnTail.Image = CType(resources.GetObject("btnTail.Image"), Image)
        btnTail.Location = New Point(340, 109)
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
        btnHead.Location = New Point(304, 109)
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
        lblStatus.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblStatus.Location = New Point(12, 169)
        lblStatus.Margin = New Padding(3, 3, 3, 6)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(228, 18)
        lblStatus.TabIndex = 20
        lblStatus.Text = "信息"
        ' 
        ' btnTurn
        ' 
        btnTurn.AllowDrop = True
        btnTurn.AutoSize = True
        btnTurn.Location = New Point(304, 163)
        btnTurn.Name = "btnTurn"
        btnTurn.Padding = New Padding(3, 0, 3, 0)
        btnTurn.Size = New Size(66, 30)
        btnTurn.TabIndex = 21
        btnTurn.Text = "转到行"
        ToolTip1.SetToolTip(btnTurn, "按序号跳转(Ctrl+G)")
        btnTurn.UseVisualStyleBackColor = True
        ' 
        ' btnSelectAll
        ' 
        btnSelectAll.AllowDrop = True
        btnSelectAll.AutoSize = True
        btnSelectAll.Location = New Point(246, 163)
        btnSelectAll.Name = "btnSelectAll"
        btnSelectAll.Padding = New Padding(3, 0, 3, 0)
        btnSelectAll.Size = New Size(52, 30)
        btnSelectAll.TabIndex = 22
        btnSelectAll.Text = "全选"
        btnSelectAll.UseVisualStyleBackColor = True
        ' 
        ' cmbCases
        ' 
        cmbCases.Appearance = Appearance.Button
        cmbCases.Image = CType(resources.GetObject("cmbCases.Image"), Image)
        cmbCases.Location = New Point(100, 109)
        cmbCases.Name = "cmbCases"
        cmbCases.Size = New Size(82, 30)
        cmbCases.TabIndex = 23
        cmbCases.Text = "大小写"
        cmbCases.TextAlign = ContentAlignment.MiddleCenter
        cmbCases.TextImageRelation = TextImageRelation.ImageBeforeText
        cmbCases.UseVisualStyleBackColor = True
        ' 
        ' cmbAllwords
        ' 
        cmbAllwords.Appearance = Appearance.Button
        cmbAllwords.Image = CType(resources.GetObject("cmbAllwords.Image"), Image)
        cmbAllwords.Location = New Point(12, 109)
        cmbAllwords.Name = "cmbAllwords"
        cmbAllwords.Size = New Size(82, 30)
        cmbAllwords.TabIndex = 24
        cmbAllwords.Text = "全字段"
        cmbAllwords.TextAlign = ContentAlignment.MiddleCenter
        cmbAllwords.TextImageRelation = TextImageRelation.ImageBeforeText
        cmbAllwords.UseVisualStyleBackColor = True
        ' 
        ' cmbFormal
        ' 
        cmbFormal.Appearance = Appearance.Button
        cmbFormal.Image = CType(resources.GetObject("cmbFormal.Image"), Image)
        cmbFormal.Location = New Point(188, 109)
        cmbFormal.Name = "cmbFormal"
        cmbFormal.Size = New Size(82, 30)
        cmbFormal.TabIndex = 25
        cmbFormal.Text = "正规则"
        cmbFormal.TextAlign = ContentAlignment.MiddleCenter
        cmbFormal.TextImageRelation = TextImageRelation.ImageBeforeText
        cmbFormal.UseVisualStyleBackColor = True
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkGray
        Label10.Location = New Point(12, 82)
        Label10.Margin = New Padding(3, 6, 3, 6)
        Label10.Name = "Label10"
        Label10.Size = New Size(358, 18)
        Label10.TabIndex = 54
        Label10.Text = "自定义  ───────────────────────────────────────────────────────────"
        Label10.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' FormSearch
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 205)
        Controls.Add(Label10)
        Controls.Add(cmbFormal)
        Controls.Add(cmbAllwords)
        Controls.Add(cmbCases)
        Controls.Add(btnSelectAll)
        Controls.Add(btnTurn)
        Controls.Add(lblStatus)
        Controls.Add(btnPrior)
        Controls.Add(btnHead)
        Controls.Add(btnTail)
        Controls.Add(btnNext)
        Controls.Add(txtKeywords)
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

    Friend WithEvents txtKeywords As TextBox
    Friend WithEvents btnNext As Button
    Friend WithEvents btnTail As Button
    Friend WithEvents btnHead As Button
    Friend WithEvents btnPrior As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnTurn As Button
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents cmbCases As CheckBox
    Friend WithEvents cmbAllwords As CheckBox
    Friend WithEvents cmbFormal As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label10 As Label
End Class
