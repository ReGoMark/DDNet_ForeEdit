<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogMessage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogMessage))
        lblTitle = New Label()
        lstContent = New ListBox()
        btnOK = New Button()
        btnCancel = New Button()
        split1 = New Label()
        TableLayoutPanel1 = New TableLayoutPanel()
        lblNote = New Label()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblTitle
        ' 
        lblTitle.Dock = DockStyle.Fill
        lblTitle.ForeColor = SystemColors.Highlight
        lblTitle.Location = New Point(0, 0)
        lblTitle.Margin = New Padding(0, 0, 0, 3)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(358, 18)
        lblTitle.TabIndex = 0
        lblTitle.Text = "大标题"
        ' 
        ' lstContent
        ' 
        lstContent.BorderStyle = BorderStyle.None
        lstContent.Dock = DockStyle.Top
        lstContent.DrawMode = DrawMode.OwnerDrawFixed
        lstContent.ForeColor = Color.Black
        lstContent.FormattingEnabled = True
        lstContent.IntegralHeight = False
        lstContent.ItemHeight = 25
        lstContent.Location = New Point(0, 24)
        lstContent.Margin = New Padding(0, 3, 0, 3)
        lstContent.Name = "lstContent"
        lstContent.Size = New Size(358, 151)
        lstContent.TabIndex = 1
        ' 
        ' btnOK
        ' 
        btnOK.Location = New Point(260, 268)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(52, 30)
        btnOK.TabIndex = 4
        btnOK.Text = "确定"
        btnOK.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(318, 268)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(52, 30)
        btnCancel.TabIndex = 5
        btnCancel.Text = "取消"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' split1
        ' 
        split1.Dock = DockStyle.Fill
        split1.ForeColor = Color.DarkGray
        split1.Location = New Point(3, 181)
        split1.Margin = New Padding(3)
        split1.Name = "split1"
        split1.Size = New Size(352, 18)
        split1.TabIndex = 6
        split1.Text = "───────────────────────────────────────────────────────────"
        split1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.AutoSize = True
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Controls.Add(lblTitle, 0, 0)
        TableLayoutPanel1.Controls.Add(lstContent, 0, 1)
        TableLayoutPanel1.Controls.Add(split1, 0, 2)
        TableLayoutPanel1.Controls.Add(lblNote, 0, 3)
        TableLayoutPanel1.Location = New Point(12, 12)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 4
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New Size(358, 235)
        TableLayoutPanel1.TabIndex = 7
        ' 
        ' lblNote
        ' 
        lblNote.AutoEllipsis = True
        lblNote.AutoSize = True
        lblNote.Dock = DockStyle.Fill
        lblNote.ForeColor = Color.DarkGray
        lblNote.Location = New Point(0, 205)
        lblNote.Margin = New Padding(0, 3, 0, 3)
        lblNote.Name = "lblNote"
        lblNote.Size = New Size(358, 27)
        lblNote.TabIndex = 7
        lblNote.Text = "Line 1"
        ' 
        ' DialogMessage
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 310)
        Controls.Add(btnOK)
        Controls.Add(TableLayoutPanel1)
        Controls.Add(btnCancel)
        DoubleBuffered = True
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogMessage"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "提示"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents lstContent As ListBox
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents split1 As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblNote As Label
End Class
