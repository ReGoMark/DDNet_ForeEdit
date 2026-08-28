<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogThanks
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogThanks))
        lblNote = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label8 = New Label()
        Label1 = New Label()
        Label7 = New Label()
        Label6 = New Label()
        SuspendLayout()
        ' 
        ' lblNote
        ' 
        lblNote.AutoEllipsis = True
        lblNote.AutoSize = True
        lblNote.Location = New Point(12, 12)
        lblNote.Margin = New Padding(3)
        lblNote.Name = "lblNote"
        lblNote.Size = New Size(170, 18)
        lblNote.TabIndex = 11
        lblNote.Text = "感谢以下参与测试的朋友:"
        ' 
        ' Label2
        ' 
        Label2.AutoEllipsis = True
        Label2.AutoSize = True
        Label2.Location = New Point(12, 204)
        Label2.Margin = New Padding(3)
        Label2.Name = "Label2"
        Label2.Size = New Size(324, 18)
        Label2.TabIndex = 13
        Label2.Text = "DDNet ForeEdit 的诞生也感谢开源社区的支持。"
        ' 
        ' Label3
        ' 
        Label3.AutoEllipsis = True
        Label3.AutoSize = True
        Label3.Location = New Point(12, 36)
        Label3.Margin = New Padding(3)
        Label3.Name = "Label3"
        Label3.Size = New Size(88, 18)
        Label3.TabIndex = 14
        Label3.Text = "@ Katyusha"
        ' 
        ' Label4
        ' 
        Label4.AutoEllipsis = True
        Label4.AutoSize = True
        Label4.Location = New Point(12, 60)
        Label4.Margin = New Padding(3)
        Label4.Name = "Label4"
        Label4.Size = New Size(52, 18)
        Label4.TabIndex = 15
        Label4.Text = "@ 洛初"
        ' 
        ' Label5
        ' 
        Label5.AutoEllipsis = True
        Label5.AutoSize = True
        Label5.Location = New Point(12, 84)
        Label5.Margin = New Padding(3)
        Label5.Name = "Label5"
        Label5.Size = New Size(80, 18)
        Label5.TabIndex = 16
        Label5.Text = "@ 旅行的风"
        ' 
        ' Label8
        ' 
        Label8.AutoEllipsis = True
        Label8.AutoSize = True
        Label8.Location = New Point(12, 132)
        Label8.Margin = New Padding(3)
        Label8.Name = "Label8"
        Label8.Size = New Size(88, 18)
        Label8.TabIndex = 18
        Label8.Text = "@ Stocandy"
        ' 
        ' Label1
        ' 
        Label1.AutoEllipsis = True
        Label1.AutoSize = True
        Label1.Location = New Point(12, 156)
        Label1.Margin = New Padding(3)
        Label1.Name = "Label1"
        Label1.Size = New Size(104, 18)
        Label1.TabIndex = 20
        Label1.Text = "@ yellow_qwq"
        ' 
        ' Label7
        ' 
        Label7.AutoEllipsis = True
        Label7.AutoSize = True
        Label7.Location = New Point(12, 180)
        Label7.Margin = New Padding(3)
        Label7.Name = "Label7"
        Label7.Size = New Size(108, 18)
        Label7.TabIndex = 21
        Label7.Text = "(排名不分先后)"
        ' 
        ' Label6
        ' 
        Label6.AutoEllipsis = True
        Label6.AutoSize = True
        Label6.Location = New Point(12, 108)
        Label6.Margin = New Padding(3)
        Label6.Name = "Label6"
        Label6.Size = New Size(94, 18)
        Label6.TabIndex = 22
        Label6.Text = "@ 秦龙不是龙"
        ' 
        ' DialogThanks
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 234)
        Controls.Add(Label6)
        Controls.Add(Label7)
        Controls.Add(Label1)
        Controls.Add(Label8)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(lblNote)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogThanks"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "鸣谢"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents lblNote As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
End Class
