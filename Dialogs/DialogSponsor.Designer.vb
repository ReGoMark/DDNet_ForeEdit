<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogSponsor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogSponsor))
        lbl2 = New Label()
        pic1 = New PictureBox()
        lbl1 = New Label()
        CType(pic1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lbl2
        ' 
        lbl2.Location = New Point(12, 51)
        lbl2.Margin = New Padding(3)
        lbl2.Name = "lbl2"
        lbl2.Size = New Size(358, 40)
        lbl2.TabIndex = 0
        lbl2.Text = "如果你觉得这个项目还不错，可以扫描下方的二维码赞助一下。赞助完全自愿，不会解锁任何额外功能。"
        ' 
        ' pic1
        ' 
        pic1.Image = CType(resources.GetObject("pic1.Image"), Image)
        pic1.Location = New Point(12, 97)
        pic1.Name = "pic1"
        pic1.Size = New Size(358, 113)
        pic1.SizeMode = PictureBoxSizeMode.Zoom
        pic1.TabIndex = 1
        pic1.TabStop = False
        ' 
        ' lbl1
        ' 
        lbl1.Font = New Font("等距更纱黑体 Slab SC", 9F)
        lbl1.ForeColor = SystemColors.Highlight
        lbl1.Location = New Point(12, 9)
        lbl1.Margin = New Padding(3, 0, 3, 3)
        lbl1.Name = "lbl1"
        lbl1.Size = New Size(358, 36)
        lbl1.TabIndex = 2
        lbl1.Text = "本项目完全开源且免费，任何社会组织和个人不得用于商业目的。"
        ' 
        ' DialogSponsor
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 228)
        Controls.Add(lbl1)
        Controls.Add(pic1)
        Controls.Add(lbl2)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogSponsor"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "赞助"
        CType(pic1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents lbl2 As Label
    Friend WithEvents pic1 As PictureBox
    Friend WithEvents lbl1 As Label
End Class
