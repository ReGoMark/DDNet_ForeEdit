<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogPopUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogPopUp))
        lblTitle = New Label()
        lstbPopup = New ListBox()
        btnOK = New Button()
        btnCancel = New Button()
        splitNote = New Label()
        TableLayoutPanel1 = New TableLayoutPanel()
        lblNote = New Label()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblTitle
        ' 
        TableLayoutPanel1.SetColumnSpan(lblTitle, 3)
        lblTitle.Location = New Point(0, 0)
        lblTitle.Margin = New Padding(0, 0, 0, 3)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(260, 18)
        lblTitle.TabIndex = 0
        lblTitle.Text = "大标题"
        ' 
        ' lstbPopup
        ' 
        lstbPopup.BorderStyle = BorderStyle.None
        TableLayoutPanel1.SetColumnSpan(lstbPopup, 3)
        lstbPopup.Dock = DockStyle.Top
        lstbPopup.DrawMode = DrawMode.OwnerDrawFixed
        lstbPopup.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lstbPopup.FormattingEnabled = True
        lstbPopup.IntegralHeight = False
        lstbPopup.ItemHeight = 25
        lstbPopup.Location = New Point(0, 24)
        lstbPopup.Margin = New Padding(0, 3, 0, 3)
        lstbPopup.Name = "lstbPopup"
        lstbPopup.Size = New Size(358, 160)
        lstbPopup.TabIndex = 1
        ' 
        ' btnOK
        ' 
        btnOK.Location = New Point(248, 256)
        btnOK.Margin = New Padding(3, 3, 3, 0)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(52, 30)
        btnOK.TabIndex = 4
        btnOK.Text = "确定"
        btnOK.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(306, 256)
        btnCancel.Margin = New Padding(3, 3, 0, 0)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(52, 30)
        btnCancel.TabIndex = 5
        btnCancel.Text = "取消"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' splitNote
        ' 
        TableLayoutPanel1.SetColumnSpan(splitNote, 3)
        splitNote.Dock = DockStyle.Fill
        splitNote.ForeColor = Color.Silver
        splitNote.Location = New Point(3, 190)
        splitNote.Margin = New Padding(3)
        splitNote.Name = "splitNote"
        splitNote.Size = New Size(352, 18)
        splitNote.TabIndex = 6
        splitNote.Text = "───────────────────────────────────────────────────────────"
        splitNote.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.AutoSize = True
        TableLayoutPanel1.ColumnCount = 3
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.Controls.Add(btnOK, 1, 5)
        TableLayoutPanel1.Controls.Add(lblTitle, 0, 0)
        TableLayoutPanel1.Controls.Add(lstbPopup, 0, 1)
        TableLayoutPanel1.Controls.Add(splitNote, 0, 2)
        TableLayoutPanel1.Controls.Add(lblNote, 0, 3)
        TableLayoutPanel1.Controls.Add(btnCancel, 2, 5)
        TableLayoutPanel1.Location = New Point(12, 12)
        TableLayoutPanel1.Margin = New Padding(3, 3, 3, 6)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 6
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New Size(358, 286)
        TableLayoutPanel1.TabIndex = 7
        ' 
        ' lblNote
        ' 
        lblNote.AutoEllipsis = True
        TableLayoutPanel1.SetColumnSpan(lblNote, 3)
        lblNote.Dock = DockStyle.Fill
        lblNote.Location = New Point(0, 214)
        lblNote.Margin = New Padding(0, 3, 0, 3)
        lblNote.Name = "lblNote"
        TableLayoutPanel1.SetRowSpan(lblNote, 2)
        lblNote.Size = New Size(358, 36)
        lblNote.TabIndex = 7
        lblNote.Text = "Label1" & vbCrLf & "123"
        ' 
        ' DialogPopUp
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 311)
        Controls.Add(TableLayoutPanel1)
        DoubleBuffered = True
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogPopUp"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "提示"
        TableLayoutPanel1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents lstbPopup As ListBox
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents splitNote As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblNote As Label
End Class
