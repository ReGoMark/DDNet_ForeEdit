<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormInstall
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormInstall))
        btnOpen = New Button()
        btnInstall = New Button()
        Panel1 = New Panel()
        tblPackInfo = New TableLayoutPanel()
        lblZipNote = New Label()
        lblZipDate = New Label()
        tagDNFPNote = New Label()
        lblZipName = New Label()
        tagDNFPDate = New Label()
        tagDNFPName = New Label()
        tagDNFPType = New Label()
        tagDNFPSize = New Label()
        lblZipType = New Label()
        lblZipSize = New Label()
        btnBroswe = New Button()
        lstbDNFP = New ListBox()
        lblDNFP = New Label()
        Panel1.SuspendLayout()
        tblPackInfo.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnOpen
        ' 
        btnOpen.BackColor = Color.Transparent
        btnOpen.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnOpen.Location = New Point(190, 353)
        btnOpen.Name = "btnOpen"
        btnOpen.Size = New Size(52, 30)
        btnOpen.TabIndex = 28
        btnOpen.Text = "查看"
        btnOpen.UseVisualStyleBackColor = False
        ' 
        ' btnInstall
        ' 
        btnInstall.BackColor = Color.Transparent
        btnInstall.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnInstall.Location = New Point(248, 353)
        btnInstall.Name = "btnInstall"
        btnInstall.Padding = New Padding(3, 0, 0, 0)
        btnInstall.Size = New Size(122, 30)
        btnInstall.TabIndex = 25
        btnInstall.Text = "安装到当前目录"
        btnInstall.UseVisualStyleBackColor = False
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = SystemColors.Window
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(tblPackInfo)
        Panel1.ForeColor = SystemColors.InfoText
        Panel1.Location = New Point(12, 208)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(358, 122)
        Panel1.TabIndex = 36
        ' 
        ' tblPackInfo
        ' 
        tblPackInfo.AutoSize = True
        tblPackInfo.ColumnCount = 2
        tblPackInfo.ColumnStyles.Add(New ColumnStyle())
        tblPackInfo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tblPackInfo.Controls.Add(lblZipNote, 1, 4)
        tblPackInfo.Controls.Add(lblZipDate, 1, 3)
        tblPackInfo.Controls.Add(tagDNFPNote, 0, 4)
        tblPackInfo.Controls.Add(lblZipName, 1, 0)
        tblPackInfo.Controls.Add(tagDNFPDate, 0, 3)
        tblPackInfo.Controls.Add(tagDNFPName, 0, 0)
        tblPackInfo.Controls.Add(tagDNFPType, 0, 1)
        tblPackInfo.Controls.Add(tagDNFPSize, 0, 2)
        tblPackInfo.Controls.Add(lblZipType, 1, 1)
        tblPackInfo.Controls.Add(lblZipSize, 1, 2)
        tblPackInfo.Dock = DockStyle.Fill
        tblPackInfo.Location = New Point(0, 0)
        tblPackInfo.Margin = New Padding(0)
        tblPackInfo.Name = "tblPackInfo"
        tblPackInfo.RowCount = 5
        tblPackInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPackInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPackInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPackInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPackInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 20F))
        tblPackInfo.Size = New Size(356, 120)
        tblPackInfo.TabIndex = 2
        ' 
        ' lblZipNote
        ' 
        lblZipNote.AutoEllipsis = True
        lblZipNote.AutoSize = True
        lblZipNote.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZipNote.Location = New Point(45, 99)
        lblZipNote.Margin = New Padding(3)
        lblZipNote.Name = "lblZipNote"
        lblZipNote.Size = New Size(16, 18)
        lblZipNote.TabIndex = 21
        lblZipNote.Text = "-"
        lblZipNote.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblZipDate
        ' 
        lblZipDate.AutoEllipsis = True
        lblZipDate.AutoSize = True
        lblZipDate.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZipDate.Location = New Point(45, 75)
        lblZipDate.Margin = New Padding(3)
        lblZipDate.Name = "lblZipDate"
        lblZipDate.Size = New Size(16, 18)
        lblZipDate.TabIndex = 17
        lblZipDate.Text = "-"
        lblZipDate.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagDNFPNote
        ' 
        tagDNFPNote.AutoSize = True
        tagDNFPNote.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagDNFPNote.Location = New Point(3, 99)
        tagDNFPNote.Margin = New Padding(3)
        tagDNFPNote.Name = "tagDNFPNote"
        tagDNFPNote.Size = New Size(36, 18)
        tagDNFPNote.TabIndex = 14
        tagDNFPNote.Text = "备注"
        tagDNFPNote.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblZipName
        ' 
        lblZipName.AutoEllipsis = True
        lblZipName.AutoSize = True
        lblZipName.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZipName.Location = New Point(45, 3)
        lblZipName.Margin = New Padding(3)
        lblZipName.Name = "lblZipName"
        lblZipName.Size = New Size(16, 18)
        lblZipName.TabIndex = 15
        lblZipName.Text = "-"
        lblZipName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagDNFPDate
        ' 
        tagDNFPDate.AutoSize = True
        tagDNFPDate.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagDNFPDate.Location = New Point(3, 75)
        tagDNFPDate.Margin = New Padding(3)
        tagDNFPDate.Name = "tagDNFPDate"
        tagDNFPDate.Size = New Size(36, 18)
        tagDNFPDate.TabIndex = 13
        tagDNFPDate.Text = "日期"
        tagDNFPDate.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagDNFPName
        ' 
        tagDNFPName.AutoSize = True
        tagDNFPName.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagDNFPName.Location = New Point(3, 3)
        tagDNFPName.Margin = New Padding(3)
        tagDNFPName.Name = "tagDNFPName"
        tagDNFPName.Size = New Size(36, 18)
        tagDNFPName.TabIndex = 11
        tagDNFPName.Text = "名称"
        tagDNFPName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagDNFPType
        ' 
        tagDNFPType.AutoSize = True
        tagDNFPType.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagDNFPType.Location = New Point(3, 27)
        tagDNFPType.Margin = New Padding(3)
        tagDNFPType.Name = "tagDNFPType"
        tagDNFPType.Size = New Size(36, 18)
        tagDNFPType.TabIndex = 19
        tagDNFPType.Text = "类型"
        tagDNFPType.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagDNFPSize
        ' 
        tagDNFPSize.AutoSize = True
        tagDNFPSize.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagDNFPSize.Location = New Point(3, 51)
        tagDNFPSize.Margin = New Padding(3)
        tagDNFPSize.Name = "tagDNFPSize"
        tagDNFPSize.Size = New Size(36, 18)
        tagDNFPSize.TabIndex = 12
        tagDNFPSize.Text = "大小"
        tagDNFPSize.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblZipType
        ' 
        lblZipType.AutoEllipsis = True
        lblZipType.AutoSize = True
        lblZipType.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZipType.Location = New Point(45, 27)
        lblZipType.Margin = New Padding(3)
        lblZipType.Name = "lblZipType"
        lblZipType.Size = New Size(16, 18)
        lblZipType.TabIndex = 20
        lblZipType.Text = "-"
        lblZipType.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblZipSize
        ' 
        lblZipSize.AutoEllipsis = True
        lblZipSize.AutoSize = True
        lblZipSize.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZipSize.Location = New Point(45, 51)
        lblZipSize.Margin = New Padding(3)
        lblZipSize.Name = "lblZipSize"
        lblZipSize.Size = New Size(16, 18)
        lblZipSize.TabIndex = 16
        lblZipSize.Text = "-"
        lblZipSize.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnBroswe
        ' 
        btnBroswe.AllowDrop = True
        btnBroswe.BackColor = Color.Transparent
        btnBroswe.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnBroswe.Image = CType(resources.GetObject("btnBroswe.Image"), Image)
        btnBroswe.Location = New Point(12, 12)
        btnBroswe.Name = "btnBroswe"
        btnBroswe.Size = New Size(68, 30)
        btnBroswe.TabIndex = 1
        btnBroswe.Text = "浏览"
        btnBroswe.TextAlign = ContentAlignment.MiddleRight
        btnBroswe.TextImageRelation = TextImageRelation.ImageBeforeText
        btnBroswe.UseVisualStyleBackColor = False
        ' 
        ' lstbDNFP
        ' 
        lstbDNFP.DrawMode = DrawMode.OwnerDrawFixed
        lstbDNFP.ForeColor = Color.Black
        lstbDNFP.FormattingEnabled = True
        lstbDNFP.IntegralHeight = False
        lstbDNFP.ItemHeight = 25
        lstbDNFP.Location = New Point(12, 48)
        lstbDNFP.Name = "lstbDNFP"
        lstbDNFP.Size = New Size(358, 154)
        lstbDNFP.TabIndex = 47
        ' 
        ' lblDNFP
        ' 
        lblDNFP.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblDNFP.Location = New Point(86, 18)
        lblDNFP.Name = "lblDNFP"
        lblDNFP.Size = New Size(284, 18)
        lblDNFP.TabIndex = 48
        lblDNFP.Text = "等待数据加载"
        lblDNFP.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' FormInstall
        ' 
        AllowDrop = True
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 395)
        Controls.Add(lblDNFP)
        Controls.Add(btnInstall)
        Controls.Add(btnOpen)
        Controls.Add(btnBroswe)
        Controls.Add(lstbDNFP)
        Controls.Add(Panel1)
        DoubleBuffered = True
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "FormInstall"
        StartPosition = FormStartPosition.CenterParent
        Text = "安装"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        tblPackInfo.ResumeLayout(False)
        tblPackInfo.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents btnInstall As Button
    Friend WithEvents btnOpen As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents tblPackInfo As TableLayoutPanel
    Friend WithEvents lblZipNote As Label
    Friend WithEvents lblZipDate As Label
    Friend WithEvents tagDNFPNote As Label
    Friend WithEvents lblZipName As Label
    Friend WithEvents tagDNFPDate As Label
    Friend WithEvents tagDNFPName As Label
    Friend WithEvents tagDNFPType As Label
    Friend WithEvents tagDNFPSize As Label
    Friend WithEvents lblZipType As Label
    Friend WithEvents lblZipSize As Label
    Friend WithEvents btnBroswe As Button
    Friend WithEvents lstbDNFP As ListBox
    Friend WithEvents lblDNFP As Label
End Class
