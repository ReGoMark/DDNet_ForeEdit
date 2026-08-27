Imports System.ComponentModel

''' <summary>
''' 用户目录选择对话框，用于在 DDNet 和 Teeworlds 目录之间二选一。
''' </summary>
Public Class DialogDirectory

    ' ─── 私有字段 ──────────────────────────────────────────────────────

    Private _ddnetPath As String = ""
    Private _teeworldsPath As String = ""
    Private _selectedPath As String = ""

    ' ─── 公开属性 ──────────────────────────────────────────────────────

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <DefaultValue("")>
    Public Property DDNetPath As String
        Get
            Return _ddnetPath
        End Get
        Set(value As String)
            _ddnetPath = value
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <DefaultValue("")>
    Public Property TeeworldsPath As String
        Get
            Return _teeworldsPath
        End Get
        Set(value As String)
            _teeworldsPath = value
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property SelectedPath As String
        Get
            Return _selectedPath
        End Get
    End Property

    ' ─── 窗体事件 ──────────────────────────────────────────────────────

    ''' <summary>窗体加载时默认选中 DDNet 目录。</summary>
    Private Sub DialogLocal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rbDDNet.Checked = True
    End Sub

    ''' <summary>点击确定按钮，根据选中的 RadioButton 返回对应路径。</summary>
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If rbDDNet.Checked Then
            _selectedPath = _ddnetPath
        ElseIf rbTeeworlds.Checked Then
            _selectedPath = _teeworldsPath
        Else
            ' 理论上不会发生，因为默认已选中
            _selectedPath = ""
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
            Return
        End If
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>点击取消按钮，放弃选择。</summary>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        _selectedPath = ""
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>点击窗体关闭按钮（✕）时视为取消。</summary>
    Private Sub DialogLocal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = DialogResult.None Then
            Me.DialogResult = DialogResult.Cancel
        End If
    End Sub

End Class