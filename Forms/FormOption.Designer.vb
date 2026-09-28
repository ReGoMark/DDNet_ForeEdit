<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOption
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormOption))
        splitDirectory = New Label()
        ComboBox1 = New ComboBox()
        btnRecoveryLA = New Button()
        Label1 = New Label()
        ComboBox2 = New ComboBox()
        Button1 = New Button()
        Label3 = New Label()
        CheckBox1 = New CheckBox()
        btnApply = New Button()
        Button2 = New Button()
        Button3 = New Button()
        lblVersion = New Label()
        Label2 = New Label()
        Label4 = New Label()
        SuspendLayout()
        ' 
        ' splitDirectory
        ' 
        splitDirectory.ForeColor = Color.Silver
        splitDirectory.Location = New Point(12, 15)
        splitDirectory.Margin = New Padding(3, 6, 3, 6)
        splitDirectory.Name = "splitDirectory"
        splitDirectory.Size = New Size(358, 18)
        splitDirectory.TabIndex = 5
        splitDirectory.Text = "渲染  ───────────────────────────────────────────────────────────"
        splitDirectory.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"性能", "最佳外观", "平衡"})
        ComboBox1.Location = New Point(110, 45)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(224, 26)
        ComboBox1.TabIndex = 7
        ' 
        ' btnRecoveryLA
        ' 
        btnRecoveryLA.Image = CType(resources.GetObject("btnRecoveryLA.Image"), Image)
        btnRecoveryLA.Location = New Point(340, 42)
        btnRecoveryLA.Name = "btnRecoveryLA"
        btnRecoveryLA.Size = New Size(30, 30)
        btnRecoveryLA.TabIndex = 9
        btnRecoveryLA.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Silver
        Label1.Location = New Point(12, 138)
        Label1.Margin = New Padding(3, 6, 3, 6)
        Label1.Name = "Label1"
        Label1.Size = New Size(358, 18)
        Label1.TabIndex = 10
        Label1.Text = "语言  ───────────────────────────────────────────────────────────"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ComboBox2
        ' 
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"English", "日本語", "한국인", "简体中文", "繁体中文"})
        ComboBox2.Location = New Point(110, 168)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(224, 26)
        ComboBox2.TabIndex = 11
        ' 
        ' Button1
        ' 
        Button1.Image = CType(resources.GetObject("Button1.Image"), Image)
        Button1.Location = New Point(340, 165)
        Button1.Name = "Button1"
        Button1.Size = New Size(30, 30)
        Button1.TabIndex = 13
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.Silver
        Label3.Location = New Point(12, 80)
        Label3.Margin = New Padding(3, 6, 3, 6)
        Label3.Name = "Label3"
        Label3.Size = New Size(358, 18)
        Label3.TabIndex = 14
        Label3.Text = "显示  ───────────────────────────────────────────────────────────"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(110, 107)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(186, 22)
        CheckBox1.TabIndex = 15
        CheckBox1.Text = "启用工具提示(默认启用)"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' btnApply
        ' 
        btnApply.Location = New Point(202, 231)
        btnApply.Margin = New Padding(3, 6, 3, 3)
        btnApply.Name = "btnApply"
        btnApply.Size = New Size(52, 30)
        btnApply.TabIndex = 40
        btnApply.Text = "应用"
        btnApply.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(260, 231)
        Button2.Margin = New Padding(3, 6, 3, 3)
        Button2.Name = "Button2"
        Button2.Size = New Size(52, 30)
        Button2.TabIndex = 41
        Button2.Text = "确定"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(318, 231)
        Button3.Margin = New Padding(3, 6, 3, 3)
        Button3.Name = "Button3"
        Button3.Size = New Size(52, 30)
        Button3.TabIndex = 42
        Button3.Text = "取消"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' lblVersion
        ' 
        lblVersion.AutoSize = True
        lblVersion.Location = New Point(12, 48)
        lblVersion.Margin = New Padding(3)
        lblVersion.Name = "lblVersion"
        lblVersion.Size = New Size(64, 18)
        lblVersion.TabIndex = 6
        lblVersion.Text = "列表渲染"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 108)
        Label2.Margin = New Padding(3)
        Label2.Name = "Label2"
        Label2.Size = New Size(50, 18)
        Label2.TabIndex = 43
        Label2.Text = "主程序"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(12, 171)
        Label4.Margin = New Padding(3)
        Label4.Name = "Label4"
        Label4.Size = New Size(64, 18)
        Label4.TabIndex = 44
        Label4.Text = "界面语言"
        ' 
        ' FormOption
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 273)
        Controls.Add(Label4)
        Controls.Add(Label2)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(btnApply)
        Controls.Add(CheckBox1)
        Controls.Add(Label3)
        Controls.Add(Button1)
        Controls.Add(ComboBox2)
        Controls.Add(Label1)
        Controls.Add(btnRecoveryLA)
        Controls.Add(ComboBox1)
        Controls.Add(lblVersion)
        Controls.Add(splitDirectory)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "FormOption"
        Text = "选项"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents splitDirectory As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents btnRecoveryLA As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents btnApply As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents lblVersion As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
End Class
