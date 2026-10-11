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
        btnLocal = New Button()
        lbl1 = New Label()
        link1 = New LinkLabel()
        btnIgnore = New Button()
        lbl2 = New Label()
        lbl3 = New Label()
        btnExit = New Button()
        SuspendLayout()
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 9F)
        lblTitle.ForeColor = SystemColors.Highlight
        lblTitle.Location = New Point(12, 12)
        lblTitle.Margin = New Padding(3)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(285, 20)
        lblTitle.TabIndex = 0
        lblTitle.Text = "缺失字体: 等距更纱黑体 Slab SC Regular"
        ' 
        ' split1
        ' 
        split1.Font = New Font("Segoe UI", 9F)
        split1.ForeColor = Color.DarkGray
        split1.Location = New Point(12, 41)
        split1.Margin = New Padding(3, 6, 3, 6)
        split1.Name = "split1"
        split1.Size = New Size(358, 18)
        split1.TabIndex = 3
        split1.Text = "安装方法  ───────────────────────────────────────────────────────"
        split1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnLocal
        ' 
        btnLocal.Font = New Font("Segoe UI", 9F)
        btnLocal.Location = New Point(12, 215)
        btnLocal.Name = "btnLocal"
        btnLocal.Padding = New Padding(3, 0, 3, 0)
        btnLocal.Size = New Size(89, 30)
        btnLocal.TabIndex = 4
        btnLocal.Text = "本地目录"
        btnLocal.UseVisualStyleBackColor = True
        ' 
        ' lbl1
        ' 
        lbl1.Font = New Font("Segoe UI", 9F)
        lbl1.Location = New Point(12, 68)
        lbl1.Margin = New Padding(3)
        lbl1.Name = "lbl1"
        lbl1.Size = New Size(358, 40)
        lbl1.TabIndex = 5
        lbl1.Text = "1. 点击链接访问「更纱黑体官方仓库」下载该字体, 或点击「本地目录」从本地安装"
        ' 
        ' link1
        ' 
        link1.ActiveLinkColor = SystemColors.HotTrack
        link1.AutoEllipsis = True
        link1.Font = New Font("Segoe UI", 9F)
        link1.LinkColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        link1.Location = New Point(12, 114)
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
        btnIgnore.Location = New Point(170, 215)
        btnIgnore.Name = "btnIgnore"
        btnIgnore.Padding = New Padding(3, 0, 3, 0)
        btnIgnore.Size = New Size(105, 30)
        btnIgnore.TabIndex = 8
        btnIgnore.Text = "忽略并继续"
        btnIgnore.UseVisualStyleBackColor = True
        ' 
        ' lbl2
        ' 
        lbl2.AutoSize = True
        lbl2.Font = New Font("Segoe UI", 9F)
        lbl2.Location = New Point(12, 140)
        lbl2.Margin = New Padding(3)
        lbl2.Name = "lbl2"
        lbl2.Size = New Size(287, 20)
        lbl2.TabIndex = 10
        lbl2.Text = "2. 选中字体文件, 右键单击选择「安装」"
        ' 
        ' lbl3
        ' 
        lbl3.AutoSize = True
        lbl3.Font = New Font("Segoe UI", 9F)
        lbl3.Location = New Point(12, 166)
        lbl3.Margin = New Padding(3, 3, 3, 6)
        lbl3.Name = "lbl3"
        lbl3.Size = New Size(120, 20)
        lbl3.TabIndex = 11
        lbl3.Text = "3. 重新启动程序"
        ' 
        ' btnExit
        ' 
        btnExit.Font = New Font("Segoe UI", 9F)
        btnExit.Location = New Point(281, 215)
        btnExit.Name = "btnExit"
        btnExit.Padding = New Padding(3, 0, 3, 0)
        btnExit.Size = New Size(89, 30)
        btnExit.TabIndex = 12
        btnExit.Text = "退出程序"
        btnExit.UseVisualStyleBackColor = True
        ' 
        ' DialogCheck
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 257)
        Controls.Add(btnExit)
        Controls.Add(lbl3)
        Controls.Add(lbl2)
        Controls.Add(btnIgnore)
        Controls.Add(link1)
        Controls.Add(lbl1)
        Controls.Add(btnLocal)
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
        Text = "自检程序"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents split1 As Label
    Friend WithEvents btnLocal As Button
    Friend WithEvents lbl1 As Label
    Friend WithEvents link1 As LinkLabel
    Friend WithEvents btnIgnore As Button
    Friend WithEvents lbl2 As Label
    Friend WithEvents lbl3 As Label
    Friend WithEvents btnExit As Button
End Class
