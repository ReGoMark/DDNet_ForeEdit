''' <summary>
''' 恢复默认选项对话框，用于选择"恢复默认字体和配置"或"仅恢复配置"。
''' </summary>
Public Class DialogRestore

    ' ─── 枚举 ──────────────────────────────────────────────────────────

    ''' <summary>
    ''' 用户选择的恢复模式。
    ''' </summary>
    Public Enum RestoreMode

        ''' <summary>恢复默认字体和默认配置</summary>
        FontAndConfig

        ''' <summary>仅恢复默认配置</summary>
        ConfigOnly

    End Enum

    ' ─── 私有字段 ──────────────────────────────────────────────────────

    Private _selectedMode As RestoreMode = RestoreMode.FontAndConfig

    ' ─── 公开属性 ──────────────────────────────────────────────────────

    ''' <summary>获取用户选择的恢复模式。</summary>
    Public ReadOnly Property SelectedMode As RestoreMode
        Get
            Return _selectedMode
        End Get
    End Property

    ' ─── 窗体事件 ──────────────────────────────────────────────────────

    ''' <summary>窗体加载时默认选中"恢复默认字体和默认配置"。</summary>
    Private Sub DialogDefault_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rbFontConfig.Checked = True
    End Sub

    ''' <summary>点击确定按钮，根据选中的 RadioButton 返回对应模式。</summary>
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If rbFontConfig.Checked Then
            _selectedMode = RestoreMode.FontAndConfig
        ElseIf rbConfig.Checked Then
            _selectedMode = RestoreMode.ConfigOnly
        Else
            _selectedMode = RestoreMode.FontAndConfig
        End If
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>点击取消按钮，放弃选择。</summary>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>点击窗体关闭按钮（✕）时视为取消。</summary>
    Private Sub DialogDefault_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = DialogResult.None Then
            Me.DialogResult = DialogResult.Cancel
        End If
    End Sub

End Class