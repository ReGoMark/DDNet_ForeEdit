<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogLicense
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogLicense))
        txt1 = New TextBox()
        SuspendLayout()
        ' 
        ' txt1
        ' 
        txt1.BackColor = Color.White
        txt1.BorderStyle = BorderStyle.None
        txt1.ForeColor = Color.Black
        txt1.Location = New Point(12, 12)
        txt1.Multiline = True
        txt1.Name = "txt1"
        txt1.ReadOnly = True
        txt1.ScrollBars = ScrollBars.Vertical
        txt1.Size = New Size(358, 495)
        txt1.TabIndex = 0
        txt1.Text = resources.GetString("txt1.Text")
        ' 
        ' DialogLicense
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 519)
        Controls.Add(txt1)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogLicense"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "许可协议"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txt1 As TextBox
End Class
