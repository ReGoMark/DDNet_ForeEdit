<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pending
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pending))
        lblTask = New Label()
        ProgressBar1 = New ProgressBar()
        btnContinue = New Button()
        btnPause = New Button()
        btnCancel = New Button()
        lblCount = New Label()
        lblNote = New Label()
        TableLayoutPanel1 = New TableLayoutPanel()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblTask
        ' 
        lblTask.AutoSize = True
        lblTask.ForeColor = SystemColors.Highlight
        lblTask.Location = New Point(0, 0)
        lblTask.Margin = New Padding(0, 0, 3, 0)
        lblTask.Name = "lblTask"
        lblTask.Size = New Size(64, 18)
        lblTask.TabIndex = 0
        lblTask.Text = "正在执行"
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(12, 36)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(358, 24)
        ProgressBar1.TabIndex = 1
        ' 
        ' btnContinue
        ' 
        btnContinue.Enabled = False
        btnContinue.Image = CType(resources.GetObject("btnContinue.Image"), Image)
        btnContinue.Location = New Point(286, 52)
        btnContinue.Name = "btnContinue"
        btnContinue.Size = New Size(30, 30)
        btnContinue.TabIndex = 2
        btnContinue.UseVisualStyleBackColor = True
        btnContinue.Visible = False
        ' 
        ' btnPause
        ' 
        btnPause.Enabled = False
        btnPause.Image = CType(resources.GetObject("btnPause.Image"), Image)
        btnPause.Location = New Point(322, 52)
        btnPause.Name = "btnPause"
        btnPause.Size = New Size(30, 30)
        btnPause.TabIndex = 3
        btnPause.UseVisualStyleBackColor = True
        btnPause.Visible = False
        ' 
        ' btnCancel
        ' 
        btnCancel.Enabled = False
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.Location = New Point(358, 52)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(30, 30)
        btnCancel.TabIndex = 4
        btnCancel.UseVisualStyleBackColor = True
        btnCancel.Visible = False
        ' 
        ' lblCount
        ' 
        lblCount.AutoSize = True
        lblCount.Location = New Point(326, 0)
        lblCount.Margin = New Padding(3, 0, 0, 0)
        lblCount.Name = "lblCount"
        lblCount.Size = New Size(32, 18)
        lblCount.TabIndex = 5
        lblCount.Text = "-/-"
        ' 
        ' lblNote
        ' 
        lblNote.AutoSize = True
        lblNote.Location = New Point(12, 66)
        lblNote.Margin = New Padding(3)
        lblNote.Name = "lblNote"
        lblNote.Size = New Size(254, 18)
        lblNote.TabIndex = 6
        lblNote.Text = "请等待 DDNet ForeEdit 启动......"
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.AutoSize = True
        TableLayoutPanel1.ColumnCount = 3
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.Controls.Add(lblTask, 0, 0)
        TableLayoutPanel1.Controls.Add(lblCount, 2, 0)
        TableLayoutPanel1.Location = New Point(12, 12)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New Size(358, 18)
        TableLayoutPanel1.TabIndex = 7
        ' 
        ' DialogPending
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 96)
        Controls.Add(btnCancel)
        Controls.Add(btnContinue)
        Controls.Add(btnPause)
        Controls.Add(lblNote)
        Controls.Add(TableLayoutPanel1)
        Controls.Add(ProgressBar1)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "DialogPending"
        Text = "挂起"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTask As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents btnContinue As Button
    Friend WithEvents btnPause As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblCount As Label
    Friend WithEvents lblNote As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
