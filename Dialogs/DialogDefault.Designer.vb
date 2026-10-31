<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogDefault
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogDefault))
        btnOK = New Button()
        btnCancel = New Button()
        TableLayoutPanel1 = New TableLayoutPanel()
        lblTitle = New Label()
        rdoFontCfg = New RadioButton()
        rdoCfg = New RadioButton()
        split1 = New Label()
        lblNote = New Label()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnOK
        ' 
        btnOK.Location = New Point(260, 243)
        btnOK.Name = "btnOK"
        btnOK.Padding = New Padding(3, 0, 3, 0)
        btnOK.Size = New Size(52, 30)
        btnOK.TabIndex = 13
        btnOK.Text = "恢复"
        btnOK.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(318, 243)
        btnCancel.Name = "btnCancel"
        btnCancel.Padding = New Padding(3, 0, 3, 0)
        btnCancel.Size = New Size(52, 30)
        btnCancel.TabIndex = 12
        btnCancel.Text = "取消"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Controls.Add(lblTitle, 0, 0)
        TableLayoutPanel1.Controls.Add(rdoFontCfg, 0, 1)
        TableLayoutPanel1.Controls.Add(rdoCfg, 0, 2)
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
        TableLayoutPanel1.Size = New Size(358, 207)
        TableLayoutPanel1.TabIndex = 11
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
        lblTitle.Text = "选择恢复模式"
        ' 
        ' rdoFontCfg
        ' 
        rdoFontCfg.Appearance = Appearance.Button
        rdoFontCfg.Dock = DockStyle.Fill
        rdoFontCfg.Image = CType(resources.GetObject("rdoFontCfg.Image"), Image)
        rdoFontCfg.ImageAlign = ContentAlignment.MiddleLeft
        rdoFontCfg.Location = New Point(0, 24)
        rdoFontCfg.Margin = New Padding(0, 3, 0, 3)
        rdoFontCfg.Name = "rdoFontCfg"
        rdoFontCfg.Padding = New Padding(3, 0, 3, 0)
        rdoFontCfg.Size = New Size(358, 46)
        rdoFontCfg.TabIndex = 1
        rdoFontCfg.TabStop = True
        rdoFontCfg.Text = "恢复默认字体, 默认配置" & vbCrLf & "(删除导入的字体, 恢复官方字体和配置)"
        rdoFontCfg.TextImageRelation = TextImageRelation.ImageBeforeText
        rdoFontCfg.UseVisualStyleBackColor = True
        ' 
        ' rdoCfg
        ' 
        rdoCfg.Appearance = Appearance.Button
        rdoCfg.Dock = DockStyle.Fill
        rdoCfg.Image = CType(resources.GetObject("rdoCfg.Image"), Image)
        rdoCfg.ImageAlign = ContentAlignment.MiddleLeft
        rdoCfg.Location = New Point(0, 76)
        rdoCfg.Margin = New Padding(0, 3, 0, 3)
        rdoCfg.Name = "rdoCfg"
        rdoCfg.Padding = New Padding(3, 0, 3, 0)
        rdoCfg.Size = New Size(358, 46)
        rdoCfg.TabIndex = 2
        rdoCfg.TabStop = True
        rdoCfg.Text = "恢复默认配置" & vbCrLf & "(保留导入字体, 恢复官方配置)"
        rdoCfg.TextImageRelation = TextImageRelation.ImageBeforeText
        rdoCfg.UseVisualStyleBackColor = True
        ' 
        ' split1
        ' 
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
        lblNote.Dock = DockStyle.Top
        lblNote.ForeColor = Color.DarkGray
        lblNote.Location = New Point(0, 150)
        lblNote.Margin = New Padding(0, 3, 0, 3)
        lblNote.Name = "lblNote"
        lblNote.Size = New Size(358, 54)
        lblNote.TabIndex = 37
        lblNote.Text = "* 选择「恢复默认字体, 默认配置」: 字体将在下次启动时恢复; 配置文件立即恢复" & vbCrLf & "* 选择「恢复默认配置」: 配置文件立即恢复"
        ' 
        ' DialogRestore
        ' 
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 285)
        Controls.Add(btnOK)
        Controls.Add(btnCancel)
        Controls.Add(TableLayoutPanel1)
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogRestore"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "恢复默认"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblTitle As Label
    Friend WithEvents rdoFontCfg As RadioButton
    Friend WithEvents rdoCfg As RadioButton
    Friend WithEvents split1 As Label
    Friend WithEvents lblNote As Label
End Class
