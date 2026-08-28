<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormOptions))
        splitDirectory = New Label()
        btnRecoveryLA = New Button()
        ComboBox1 = New ComboBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        CheckBox1 = New CheckBox()
        CheckBox2 = New CheckBox()
        Label4 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        Label5 = New Label()
        Label6 = New Label()
        SuspendLayout()
        ' 
        ' splitDirectory
        ' 
        splitDirectory.ForeColor = Color.Silver
        splitDirectory.Location = New Point(12, 12)
        splitDirectory.Margin = New Padding(3)
        splitDirectory.Name = "splitDirectory"
        splitDirectory.Size = New Size(358, 18)
        splitDirectory.TabIndex = 3
        splitDirectory.Text = "渲染  ───────────────────────────────────────────────────────────"
        splitDirectory.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnRecoveryLA
        ' 
        btnRecoveryLA.Image = CType(resources.GetObject("btnRecoveryLA.Image"), Image)
        btnRecoveryLA.Location = New Point(340, 36)
        btnRecoveryLA.Name = "btnRecoveryLA"
        btnRecoveryLA.Size = New Size(30, 30)
        btnRecoveryLA.TabIndex = 9
        btnRecoveryLA.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(118, 39)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(216, 26)
        ComboBox1.TabIndex = 10
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 42)
        Label1.Margin = New Padding(3)
        Label1.Name = "Label1"
        Label1.Size = New Size(64, 18)
        Label1.TabIndex = 11
        Label1.Text = "列表渲染"
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Silver
        Label2.Location = New Point(12, 131)
        Label2.Margin = New Padding(3)
        Label2.Name = "Label2"
        Label2.Size = New Size(358, 18)
        Label2.TabIndex = 12
        Label2.Text = "显示  ───────────────────────────────────────────────────────────"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(12, 156)
        Label3.Margin = New Padding(3)
        Label3.Name = "Label3"
        Label3.Size = New Size(50, 18)
        Label3.TabIndex = 13
        Label3.Text = "标题栏"
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(118, 155)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(86, 22)
        CheckBox1.TabIndex = 14
        CheckBox1.Text = "显示来源"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Location = New Point(118, 183)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(114, 22)
        CheckBox2.TabIndex = 15
        CheckBox2.Text = "显示详细路径"
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.Silver
        Label4.Location = New Point(12, 211)
        Label4.Margin = New Padding(3)
        Label4.Name = "Label4"
        Label4.Size = New Size(358, 18)
        Label4.TabIndex = 16
        Label4.Text = "格式关联  ───────────────────────────────────────────────────────────"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(118, 295)
        Button1.Name = "Button1"
        Button1.Size = New Size(80, 30)
        Button1.TabIndex = 17
        Button1.Text = "关联格式"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(202, 371)
        Button2.Margin = New Padding(3, 6, 3, 3)
        Button2.Name = "Button2"
        Button2.Size = New Size(52, 30)
        Button2.TabIndex = 18
        Button2.Text = "应用"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(318, 371)
        Button3.Name = "Button3"
        Button3.Size = New Size(52, 30)
        Button3.TabIndex = 19
        Button3.Text = "取消"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(260, 371)
        Button4.Name = "Button4"
        Button4.Size = New Size(52, 30)
        Button4.TabIndex = 20
        Button4.Text = "确定"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.ForeColor = Color.DarkGray
        Label5.Location = New Point(118, 235)
        Label5.Margin = New Padding(3)
        Label5.Name = "Label5"
        Label5.Size = New Size(248, 54)
        Label5.TabIndex = 21
        Label5.Text = ".dnfp 格式是基于 .zip 格式的二次" & vbCrLf & "封装. 点击下方按钮使用压缩软件关联" & vbCrLf & "该格式, 以便查看字体包内容."
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.ForeColor = Color.DarkGray
        Label6.Location = New Point(118, 71)
        Label6.Margin = New Padding(3)
        Label6.Name = "Label6"
        Label6.Size = New Size(218, 54)
        Label6.TabIndex = 22
        Label6.Text = "* 系统: 最佳性能, 不显示序号;" & vbCrLf & "* GDI+: 平衡渲染质量和速度;" & vbCrLf & "* GDI: 最佳外观,性能开销增加"
        ' 
        ' FormOptions
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 413)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label4)
        Controls.Add(CheckBox2)
        Controls.Add(CheckBox1)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(ComboBox1)
        Controls.Add(btnRecoveryLA)
        Controls.Add(splitDirectory)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "FormOptions"
        Text = "选项"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents splitDirectory As Label
    Friend WithEvents btnRecoveryLA As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
End Class
