<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogPending
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogPending))
        Label1 = New Label()
        tblExport = New TableLayoutPanel()
        lblSizeTotal = New Label()
        Label2 = New Label()
        ProgressBar1 = New ProgressBar()
        splitDirectory = New Label()
        tblExport.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.ForeColor = SystemColors.Highlight
        Label1.Location = New Point(0, 0)
        Label1.Margin = New Padding(0, 0, 3, 3)
        Label1.Name = "Label1"
        Label1.Size = New Size(80, 18)
        Label1.TabIndex = 0
        Label1.Text = "正在执行: "
        ' 
        ' tblExport
        ' 
        tblExport.BackColor = Color.Transparent
        tblExport.ColumnCount = 3
        tblExport.ColumnStyles.Add(New ColumnStyle())
        tblExport.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblExport.ColumnStyles.Add(New ColumnStyle())
        tblExport.Controls.Add(lblSizeTotal, 0, 3)
        tblExport.Controls.Add(Label2, 2, 0)
        tblExport.Controls.Add(ProgressBar1, 0, 1)
        tblExport.Controls.Add(Label1, 0, 0)
        tblExport.Controls.Add(splitDirectory, 0, 2)
        tblExport.Location = New Point(12, 12)
        tblExport.Name = "tblExport"
        tblExport.RowCount = 4
        tblExport.RowStyles.Add(New RowStyle())
        tblExport.RowStyles.Add(New RowStyle())
        tblExport.RowStyles.Add(New RowStyle())
        tblExport.RowStyles.Add(New RowStyle())
        tblExport.Size = New Size(358, 100)
        tblExport.TabIndex = 41
        ' 
        ' lblSizeTotal
        ' 
        lblSizeTotal.AutoSize = True
        tblExport.SetColumnSpan(lblSizeTotal, 3)
        lblSizeTotal.ForeColor = Color.DarkGray
        lblSizeTotal.Location = New Point(3, 78)
        lblSizeTotal.Margin = New Padding(3)
        lblSizeTotal.Name = "lblSizeTotal"
        lblSizeTotal.Size = New Size(268, 18)
        lblSizeTotal.TabIndex = 73
        lblSizeTotal.Text = "任务执行完毕后将启动 DDNet ForeEdit"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Dock = DockStyle.Fill
        Label2.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label2.Location = New Point(326, 0)
        Label2.Margin = New Padding(3, 0, 0, 3)
        Label2.Name = "Label2"
        Label2.Size = New Size(32, 18)
        Label2.TabIndex = 43
        Label2.Text = "-/-"
        Label2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.BackColor = Color.WhiteSmoke
        tblExport.SetColumnSpan(ProgressBar1, 3)
        ProgressBar1.Dock = DockStyle.Fill
        ProgressBar1.Location = New Point(0, 24)
        ProgressBar1.Margin = New Padding(0, 3, 0, 3)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(358, 24)
        ProgressBar1.TabIndex = 7
        ProgressBar1.Visible = False
        ' 
        ' splitDirectory
        ' 
        tblExport.SetColumnSpan(splitDirectory, 3)
        splitDirectory.ForeColor = Color.DarkGray
        splitDirectory.Location = New Point(3, 54)
        splitDirectory.Margin = New Padding(3)
        splitDirectory.Name = "splitDirectory"
        splitDirectory.Size = New Size(352, 18)
        splitDirectory.TabIndex = 74
        splitDirectory.Text = "───────────────────────────────────────────────────────────"
        splitDirectory.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' DialogPend
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 124)
        Controls.Add(tblExport)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "DialogPend"
        StartPosition = FormStartPosition.CenterScreen
        Text = "任务"
        tblExport.ResumeLayout(False)
        tblExport.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents tblExport As TableLayoutPanel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Label2 As Label
    Friend WithEvents lblSizeTotal As Label
    Friend WithEvents splitDirectory As Label
End Class
