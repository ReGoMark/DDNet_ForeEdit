<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBlade
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormBlade))
        btnOpen = New Button()
        btnInstall = New Button()
        Panel1 = New Panel()
        tblPackInfo = New TableLayoutPanel()
        lblZipNote = New Label()
        lblZipDate = New Label()
        tagZipNote = New Label()
        lblZipName = New Label()
        tagZipDate = New Label()
        tagZipName = New Label()
        tagZipType = New Label()
        tagZipSize = New Label()
        lblZipType = New Label()
        lblZipSize = New Label()
        btnZipBroswe = New Button()
        lstbBlade = New ListBox()
        lblZip = New Label()
        Panel1.SuspendLayout()
        tblPackInfo.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnOpen
        ' 
        btnOpen.BackColor = Color.Transparent
        btnOpen.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnOpen.Location = New Point(190, 345)
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
        btnInstall.Location = New Point(248, 345)
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
        Panel1.Location = New Point(12, 199)
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
        tblPackInfo.Controls.Add(tagZipNote, 0, 4)
        tblPackInfo.Controls.Add(lblZipName, 1, 0)
        tblPackInfo.Controls.Add(tagZipDate, 0, 3)
        tblPackInfo.Controls.Add(tagZipName, 0, 0)
        tblPackInfo.Controls.Add(tagZipType, 0, 1)
        tblPackInfo.Controls.Add(tagZipSize, 0, 2)
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
        ' tagZipNote
        ' 
        tagZipNote.AutoSize = True
        tagZipNote.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagZipNote.Location = New Point(3, 99)
        tagZipNote.Margin = New Padding(3)
        tagZipNote.Name = "tagZipNote"
        tagZipNote.Size = New Size(36, 18)
        tagZipNote.TabIndex = 14
        tagZipNote.Text = "备注"
        tagZipNote.TextAlign = ContentAlignment.MiddleLeft
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
        ' tagZipDate
        ' 
        tagZipDate.AutoSize = True
        tagZipDate.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagZipDate.Location = New Point(3, 75)
        tagZipDate.Margin = New Padding(3)
        tagZipDate.Name = "tagZipDate"
        tagZipDate.Size = New Size(36, 18)
        tagZipDate.TabIndex = 13
        tagZipDate.Text = "日期"
        tagZipDate.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagZipName
        ' 
        tagZipName.AutoSize = True
        tagZipName.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagZipName.Location = New Point(3, 3)
        tagZipName.Margin = New Padding(3)
        tagZipName.Name = "tagZipName"
        tagZipName.Size = New Size(36, 18)
        tagZipName.TabIndex = 11
        tagZipName.Text = "名称"
        tagZipName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagZipType
        ' 
        tagZipType.AutoSize = True
        tagZipType.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagZipType.Location = New Point(3, 27)
        tagZipType.Margin = New Padding(3)
        tagZipType.Name = "tagZipType"
        tagZipType.Size = New Size(36, 18)
        tagZipType.TabIndex = 19
        tagZipType.Text = "类型"
        tagZipType.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tagZipSize
        ' 
        tagZipSize.AutoSize = True
        tagZipSize.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        tagZipSize.Location = New Point(3, 51)
        tagZipSize.Margin = New Padding(3)
        tagZipSize.Name = "tagZipSize"
        tagZipSize.Size = New Size(36, 18)
        tagZipSize.TabIndex = 12
        tagZipSize.Text = "大小"
        tagZipSize.TextAlign = ContentAlignment.MiddleLeft
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
        ' btnZipBroswe
        ' 
        btnZipBroswe.AllowDrop = True
        btnZipBroswe.BackColor = Color.Transparent
        btnZipBroswe.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnZipBroswe.Image = CType(resources.GetObject("btnZipBroswe.Image"), Image)
        btnZipBroswe.Location = New Point(12, 12)
        btnZipBroswe.Name = "btnZipBroswe"
        btnZipBroswe.Size = New Size(68, 30)
        btnZipBroswe.TabIndex = 1
        btnZipBroswe.Text = "浏览"
        btnZipBroswe.TextAlign = ContentAlignment.MiddleRight
        btnZipBroswe.TextImageRelation = TextImageRelation.ImageBeforeText
        btnZipBroswe.UseVisualStyleBackColor = False
        ' 
        ' lstbBlade
        ' 
        lstbBlade.DrawMode = DrawMode.OwnerDrawFixed
        lstbBlade.ForeColor = Color.Black
        lstbBlade.FormattingEnabled = True
        lstbBlade.IntegralHeight = False
        lstbBlade.ItemHeight = 25
        lstbBlade.Location = New Point(12, 48)
        lstbBlade.Name = "lstbBlade"
        lstbBlade.Size = New Size(358, 145)
        lstbBlade.TabIndex = 47
        ' 
        ' lblZip
        ' 
        lblZip.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblZip.Location = New Point(86, 18)
        lblZip.Name = "lblZip"
        lblZip.Size = New Size(284, 18)
        lblZip.TabIndex = 48
        lblZip.Text = "等待数据加载"
        lblZip.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' FormBlade
        ' 
        AllowDrop = True
        AutoScaleDimensions = New SizeF(7F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 387)
        Controls.Add(lblZip)
        Controls.Add(btnInstall)
        Controls.Add(btnOpen)
        Controls.Add(btnZipBroswe)
        Controls.Add(lstbBlade)
        Controls.Add(Panel1)
        DoubleBuffered = True
        Font = New Font("等距更纱黑体 Slab SC", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "FormBlade"
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
    Friend WithEvents tagZipNote As Label
    Friend WithEvents lblZipName As Label
    Friend WithEvents tagZipDate As Label
    Friend WithEvents tagZipName As Label
    Friend WithEvents tagZipType As Label
    Friend WithEvents tagZipSize As Label
    Friend WithEvents lblZipType As Label
    Friend WithEvents lblZipSize As Label
    Friend WithEvents btnZipBroswe As Button
    Friend WithEvents lstbBlade As ListBox
    Friend WithEvents lblZip As Label
End Class
