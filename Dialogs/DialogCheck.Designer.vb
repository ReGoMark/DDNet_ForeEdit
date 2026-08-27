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
        Label1 = New Label()
        Label2 = New Label()
        splitDirectory = New Label()
        btnFolder = New Button()
        Label3 = New Label()
        LinkLabel1 = New LinkLabel()
        btnIgnore = New Button()
        chkNever = New CheckBox()
        Label4 = New Label()
        Label5 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 9F)
        Label1.Location = New Point(12, 12)
        Label1.Margin = New Padding(3)
        Label1.Name = "Label1"
        Label1.Size = New Size(311, 20)
        Label1.TabIndex = 0
        Label1.Text = "检测到未安装以下字体, 可能影响显示效果: "
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 9F)
        Label2.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label2.Location = New Point(12, 38)
        Label2.Margin = New Padding(3)
        Label2.Name = "Label2"
        Label2.Size = New Size(214, 20)
        Label2.TabIndex = 1
        Label2.Text = "等距更纱黑体 Slab SC Regular"
        ' 
        ' splitDirectory
        ' 
        splitDirectory.Font = New Font("Segoe UI", 9F)
        splitDirectory.ForeColor = Color.Silver
        splitDirectory.Location = New Point(12, 67)
        splitDirectory.Margin = New Padding(3, 6, 3, 6)
        splitDirectory.Name = "splitDirectory"
        splitDirectory.Size = New Size(358, 18)
        splitDirectory.TabIndex = 3
        splitDirectory.Text = "安装方法  ───────────────────────────────────────────────────────"
        splitDirectory.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' btnFolder
        ' 
        btnFolder.Font = New Font("Segoe UI", 9F)
        btnFolder.Location = New Point(204, 221)
        btnFolder.Name = "btnFolder"
        btnFolder.Padding = New Padding(3, 0, 3, 0)
        btnFolder.Size = New Size(55, 30)
        btnFolder.TabIndex = 4
        btnFolder.Text = "打开"
        btnFolder.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 9F)
        Label3.Location = New Point(12, 94)
        Label3.Margin = New Padding(3)
        Label3.Name = "Label3"
        Label3.Size = New Size(344, 40)
        Label3.TabIndex = 5
        Label3.Text = "1. 点击链接访问「更纱黑体」官方仓库下载该字" & vbCrLf & "体, 或点击下方「打开」按钮从本地安装"
        ' 
        ' LinkLabel1
        ' 
        LinkLabel1.ActiveLinkColor = SystemColors.HotTrack
        LinkLabel1.AutoEllipsis = True
        LinkLabel1.Font = New Font("Segoe UI", 9F)
        LinkLabel1.LinkColor = SystemColors.Highlight
        LinkLabel1.Location = New Point(12, 140)
        LinkLabel1.Margin = New Padding(3)
        LinkLabel1.Name = "LinkLabel1"
        LinkLabel1.Size = New Size(358, 20)
        LinkLabel1.TabIndex = 6
        LinkLabel1.TabStop = True
        LinkLabel1.Text = "https://github.com/be5invis/Sarasa-Gothic/releases"
        ' 
        ' btnIgnore
        ' 
        btnIgnore.Font = New Font("Segoe UI", 9F)
        btnIgnore.Location = New Point(265, 221)
        btnIgnore.Name = "btnIgnore"
        btnIgnore.Padding = New Padding(3, 0, 3, 0)
        btnIgnore.Size = New Size(105, 30)
        btnIgnore.TabIndex = 8
        btnIgnore.Text = "忽略并继续"
        btnIgnore.UseVisualStyleBackColor = True
        ' 
        ' chkNever
        ' 
        chkNever.AutoSize = True
        chkNever.Font = New Font("Segoe UI", 9F)
        chkNever.Location = New Point(12, 225)
        chkNever.Name = "chkNever"
        chkNever.Size = New Size(95, 24)
        chkNever.TabIndex = 9
        chkNever.Text = "不再提示"
        chkNever.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 9F)
        Label4.Location = New Point(12, 166)
        Label4.Margin = New Padding(3)
        Label4.Name = "Label4"
        Label4.Size = New Size(287, 20)
        Label4.TabIndex = 10
        Label4.Text = "2. 选中字体文件, 右键单击选择「安装」"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 9F)
        Label5.Location = New Point(12, 192)
        Label5.Margin = New Padding(3, 3, 3, 6)
        Label5.Name = "Label5"
        Label5.Size = New Size(120, 20)
        Label5.TabIndex = 11
        Label5.Text = "3. 重新启动程序"
        ' 
        ' DialogCheck
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(382, 263)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(chkNever)
        Controls.Add(btnIgnore)
        Controls.Add(LinkLabel1)
        Controls.Add(Label3)
        Controls.Add(btnFolder)
        Controls.Add(splitDirectory)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Font = New Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "DialogCheck"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "自检程序"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents splitDirectory As Label
    Friend WithEvents btnFolder As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents btnIgnore As Button
    Friend WithEvents chkNever As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
End Class
