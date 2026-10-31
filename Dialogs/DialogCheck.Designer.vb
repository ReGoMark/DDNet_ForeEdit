<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogCheck
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogCheck))
        lblTitle = New Label()
        split1 = New Label()
        lbl1 = New Label()
        link1 = New LinkLabel()
        btnIgnore = New Button()
        btnExit = New Button()
        Label2 = New Label()
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoEllipsis = True
        lblTitle.Font = New Font("Segoe UI", 9F)
        lblTitle.ForeColor = SystemColors.Highlight
        lblTitle.Location = New Point(12, 12)
        lblTitle.Margin = New Padding(3)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(358, 40)
        lblTitle.TabIndex = 0
        lblTitle.Text = "缺少字体「等距更纱黑体 Slab SC」，可能会导致 DDNet ForeEdit 界面显示异常。"
        ' 
        ' split1
        ' 
        split1.Font = New Font("Segoe UI", 9F)
        split1.ForeColor = Color.DarkGray
        split1.Location = New Point(12, 58)
        split1.Margin = New Padding(3)
        split1.Name = "split1"
        split1.Size = New Size(358, 18)
        split1.TabIndex = 3
        split1.Text = "───────────────────────────────────────────────────────"
        split1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lbl1
        ' 
        lbl1.AutoSize = True
        lbl1.Font = New Font("Segoe UI", 9F)
        lbl1.Location = New Point(12, 82)
        lbl1.Margin = New Padding(3)
        lbl1.Name = "lbl1"
        lbl1.Size = New Size(124, 20)
        lbl1.TabIndex = 5
        lbl1.Text = "点击从官方下载:"
        ' 
        ' link1
        ' 
        link1.ActiveLinkColor = SystemColors.HotTrack
        link1.AutoEllipsis = True
        link1.Cursor = Cursors.Hand
        link1.Font = New Font("Segoe UI", 9F)
        link1.LinkColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        link1.Location = New Point(12, 108)
        link1.Margin = New Padding(3)
        link1.Name = "link1"
        link1.Size = New Size(358, 20)
        link1.TabIndex = 6
        link1.TabStop = True
        link1.Text = "https://github.com/be5invis/Sarasa-Gothic/releases"
        ' 
        ' btnIgnore
        ' 
        btnIgnore.Font = New Font("Segoe UI", 9F)
        btnIgnore.Location = New Point(170, 200)
        btnIgnore.Name = "btnIgnore"
        btnIgnore.Padding = New Padding(3, 0, 3, 0)
        btnIgnore.Size = New Size(105, 30)
        btnIgnore.TabIndex = 8
        btnIgnore.Text = "忽略并继续"
        btnIgnore.UseVisualStyleBackColor = True
        ' 
        ' btnExit
        ' 
        btnExit.Font = New Font("Segoe UI", 9F)
        btnExit.Location = New Point(281, 200)
        btnExit.Name = "btnExit"
        btnExit.Padding = New Padding(3, 0, 3, 0)
        btnExit.Size = New Size(89, 30)
        btnExit.TabIndex = 12
        btnExit.Text = "退出程序"
        btnExit.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 9F)
        Label2.Location = New Point(12, 134)
        Label2.Margin = New Padding(3)
        Label2.Name = "Label2"
        Label2.Size = New Size(124, 20)
        Label2.TabIndex = 14
        Label2.Text = "点击从本地安装:"
        ' 
        ' Label1
        ' 
        Label1.AutoEllipsis = True
        Label1.Cursor = Cursors.Hand
        Label1.Font = New Font("微软雅黑", 9F, FontStyle.Underline, GraphicsUnit.Point, CByte(134))
        Label1.Location = New Point(12, 157)
        Label1.Name = "Label1"
        Label1.Size = New Size(358, 20)
        Label1.TabIndex = 15
        Label1.Text = "字体目录"
        ' 
        ' DialogCheck
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 242)
        Controls.Add(Label1)
        Controls.Add(Label2)
        Controls.Add(btnExit)
        Controls.Add(btnIgnore)
        Controls.Add(link1)
        Controls.Add(lbl1)
        Controls.Add(split1)
        Controls.Add(lblTitle)
        Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogCheck"
        StartPosition = FormStartPosition.CenterScreen
        Text = "DDNet ForeEdit 自检程序"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents split1 As Label
    Friend WithEvents lbl1 As Label
    Friend WithEvents link1 As LinkLabel
    Friend WithEvents btnIgnore As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
