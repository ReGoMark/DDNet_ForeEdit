<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogDirectory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogDirectory))
        TableLayoutPanel1 = New TableLayoutPanel()
        lblTitle = New Label()
        rdoDDNet = New RadioButton()
        rdoTeeworlds = New RadioButton()
        split1 = New Label()
        lblNote = New Label()
        btnCancel = New Button()
        btnOK = New Button()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.Controls.Add(lblTitle, 0, 0)
        TableLayoutPanel1.Controls.Add(rdoDDNet, 0, 1)
        TableLayoutPanel1.Controls.Add(rdoTeeworlds, 0, 2)
        TableLayoutPanel1.Controls.Add(split1, 0, 3)
        TableLayoutPanel1.Controls.Add(lblNote, 0, 4)
        TableLayoutPanel1.Location = New Point(12, 12)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 5
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New Size(358, 215)
        TableLayoutPanel1.TabIndex = 8
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        TableLayoutPanel1.SetColumnSpan(lblTitle, 2)
        lblTitle.Dock = DockStyle.Fill
        lblTitle.Font = New Font("等距更纱黑体 Slab SC", 9F)
        lblTitle.ForeColor = SystemColors.Highlight
        lblTitle.Location = New Point(0, 0)
        lblTitle.Margin = New Padding(0, 0, 0, 3)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(358, 18)
        lblTitle.TabIndex = 0
        lblTitle.Text = "切换用户目录"
        ' 
        ' rdoDDNet
        ' 
        rdoDDNet.Appearance = Appearance.Button
        rdoDDNet.AutoSize = True
        TableLayoutPanel1.SetColumnSpan(rdoDDNet, 2)
        rdoDDNet.Dock = DockStyle.Top
        rdoDDNet.Image = CType(resources.GetObject("rdoDDNet.Image"), Image)
        rdoDDNet.ImageAlign = ContentAlignment.MiddleLeft
        rdoDDNet.Location = New Point(0, 24)
        rdoDDNet.Margin = New Padding(0, 3, 0, 3)
        rdoDDNet.Name = "rdoDDNet"
        rdoDDNet.Padding = New Padding(3, 0, 3, 0)
        rdoDDNet.Size = New Size(358, 46)
        rdoDDNet.TabIndex = 1
        rdoDDNet.TabStop = True
        rdoDDNet.Text = "DDNet 目录" & vbCrLf & "(...\Appdata\Roaming\DDNet\fonts\)"
        rdoDDNet.TextImageRelation = TextImageRelation.ImageBeforeText
        rdoDDNet.UseVisualStyleBackColor = True
        ' 
        ' rdoTeeworlds
        ' 
        rdoTeeworlds.Appearance = Appearance.Button
        rdoTeeworlds.AutoSize = True
        TableLayoutPanel1.SetColumnSpan(rdoTeeworlds, 2)
        rdoTeeworlds.Dock = DockStyle.Top
        rdoTeeworlds.Image = CType(resources.GetObject("rdoTeeworlds.Image"), Image)
        rdoTeeworlds.ImageAlign = ContentAlignment.MiddleLeft
        rdoTeeworlds.Location = New Point(0, 76)
        rdoTeeworlds.Margin = New Padding(0, 3, 0, 3)
        rdoTeeworlds.Name = "rdoTeeworlds"
        rdoTeeworlds.Padding = New Padding(3, 0, 3, 0)
        rdoTeeworlds.Size = New Size(358, 46)
        rdoTeeworlds.TabIndex = 2
        rdoTeeworlds.TabStop = True
        rdoTeeworlds.Text = "Teeworlds 目录" & vbCrLf & "(...\Appdata\Roaming\Teeworlds\fonts\)"
        rdoTeeworlds.TextImageRelation = TextImageRelation.ImageBeforeText
        rdoTeeworlds.UseVisualStyleBackColor = True
        ' 
        ' split1
        ' 
        TableLayoutPanel1.SetColumnSpan(split1, 2)
        split1.Dock = DockStyle.Fill
        split1.ForeColor = Color.DarkGray
        split1.Location = New Point(3, 128)
        split1.Margin = New Padding(3)
        split1.Name = "split1"
        split1.Size = New Size(352, 16)
        split1.TabIndex = 36
        split1.Text = "─────────────────────────────────────────────────────────"
        ' 
        ' lblNote
        ' 
        lblNote.AutoSize = True
        TableLayoutPanel1.SetColumnSpan(lblNote, 2)
        lblNote.Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        lblNote.ForeColor = Color.DarkGray
        lblNote.Location = New Point(0, 150)
        lblNote.Margin = New Padding(0, 3, 0, 3)
        lblNote.Name = "lblNote"
        lblNote.Size = New Size(328, 54)
        lblNote.TabIndex = 37
        lblNote.Text = "* DDNet 目录的字体显示优先于 Teeworlds 目录" & vbCrLf & "* 用户目录的字体显示优先于安装目录" & vbCrLf & "* 旧版客户端可能会使用 Teeworlds 目录"
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(318, 246)
        btnCancel.Name = "btnCancel"
        btnCancel.Padding = New Padding(3, 0, 3, 0)
        btnCancel.Size = New Size(52, 30)
        btnCancel.TabIndex = 9
        btnCancel.Text = "取消"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' btnOK
        ' 
        btnOK.Location = New Point(260, 246)
        btnOK.Name = "btnOK"
        btnOK.Padding = New Padding(3, 0, 3, 0)
        btnOK.Size = New Size(52, 30)
        btnOK.TabIndex = 10
        btnOK.Text = "切换"
        btnOK.UseVisualStyleBackColor = True
        ' 
        ' DialogDirectory
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 288)
        Controls.Add(btnOK)
        Controls.Add(btnCancel)
        Controls.Add(TableLayoutPanel1)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogDirectory"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "用户目录"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblTitle As Label
    Friend WithEvents rdoDDNet As RadioButton
    Friend WithEvents rdoTeeworlds As RadioButton
    Friend WithEvents lblNote As Label
    Friend WithEvents split1 As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOK As Button
End Class
