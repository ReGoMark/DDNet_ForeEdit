''' <summary>
''' 字体预览窗体，用于实时显示五种语言（西文、日文、韩文、简中、繁中）的字体效果。 支持自定义预览文本、字号切换（10/12/16/32 点），并可通过 UpdatePreview 方法由主窗体动态更新各语言字体。
''' </summary>
Public Class FormPreview

    ' ─── 私有字段 ──────────────────────────────────────────────────────

    ''' <summary>
    ''' RadioButton 到字号（磅值）的映射。
    ''' </summary>
    Private ReadOnly _sizeMap As New Dictionary(Of RadioButton, Single)

    ''' <summary>
    ''' 记录各标签的初始文本（用于还原）。
    ''' </summary>
    Private ReadOnly _defaultTexts As New Dictionary(Of Label, String)

    ''' <summary>
    ''' 当前生效的字号（磅值）。
    ''' </summary>
    Private _currentSize As Single = 9.0F

    ' ─── 窗体生命周期 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 窗体加载时初始化字号映射、默认文本，并禁用"确认"按钮。
    ''' </summary>
    Private Sub FormPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _sizeMap(rdoFont9) = 9.0F
        _sizeMap(rdoFont10) = 10.0F
        _sizeMap(rdoFont12) = 12.0F
        _sizeMap(rdoFont16) = 16.0F
        _sizeMap(rdoFont22) = 22.0F
        rdoFont9.Checked = True

        ' 记录各标签的初始显示文本
        For Each lbl In GetPreviewLabels()
            _defaultTexts(lbl) = lbl.Text
        Next
        btnConfirm.Enabled = False
    End Sub

    ' ─── 自定义文本控制 ──────────────────────────────────────────────

    ''' <summary>
    ''' 将自定义文本应用到所有预览标签。
    ''' </summary>
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim customText As String = tbCustom.Text
        ' 若为空则设为空格，防止标签高度塌陷
        If String.IsNullOrEmpty(customText) Then customText = " "

        For Each lbl In GetPreviewLabels()
            lbl.Text = customText
        Next
    End Sub

    ''' <summary>
    ''' 将所有预览标签恢复为初始文本。
    ''' </summary>
    Private Sub btnRecovery_Click(sender As Object, e As EventArgs) Handles btnRecovery.Click
        For Each lbl In GetPreviewLabels()
            If _defaultTexts.ContainsKey(lbl) Then
                lbl.Text = _defaultTexts(lbl)
            End If
        Next
    End Sub

    ' ─── 字号切换 ──────────────────────────────────────────────────────

    ''' <summary>
    ''' 字号单选按钮切换时更新所有预览标签的字号。
    ''' </summary>
    Private Sub rbFontSize_CheckedChanged(sender As Object, e As EventArgs) _
        Handles rdoFont9.CheckedChanged, rdoFont10.CheckedChanged, rdoFont12.CheckedChanged, rdoFont16.CheckedChanged, rdoFont22.CheckedChanged

        Dim rb = CType(sender, RadioButton)
        If Not rb.Checked Then Return

        _currentSize = _sizeMap(rb)
        For Each lbl In GetPreviewLabels()
            Dim oldFont = lbl.Font
            lbl.Font = New Font(lbl.Font.FontFamily, _currentSize, lbl.Font.Style)
            oldFont?.Dispose()
        Next
    End Sub

    ' ─── 公开接口（供主窗体调用） ──────────────────────────────────

    ''' <summary>
    ''' 由主窗体调用，更新指定语言的预览标签字体。
    ''' </summary>
    ''' <param name="family">主窗体 PrivateFontCollection 中的 FontFamily；Nothing 时恢复为窗体默认字体。</param>
    ''' <param name="style">已确认该 family 可用的 FontStyle。</param>
    Public Sub UpdatePreview(langKey As String, family As FontFamily, style As FontStyle)
        Dim lbl As Label = GetLabelByLang(langKey)
        If lbl Is Nothing Then Return

        Dim oldFont = lbl.Font
        If family IsNot Nothing Then
            Try
                lbl.Font = New Font(family, _currentSize, style, GraphicsUnit.Point)
                oldFont?.Dispose()
                Return
            Catch
            End Try
        End If

        ' 兜底：恢复窗体默认字体（当前字号）
        lbl.Font = New Font(Me.Font.FontFamily, _currentSize)
        oldFont?.Dispose()
    End Sub

    ' ─── 私有辅助方法 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 获取所有预览标签的集合。
    ''' </summary>
    Private Function GetPreviewLabels() As IEnumerable(Of Label)
        Return {lblLA, lblJP, lblKR, lblSC, lblTC}
    End Function

    ''' <summary>
    ''' 根据语言键返回对应的预览标签。
    ''' </summary>
    Private Function GetLabelByLang(langKey As String) As Label
        Select Case langKey.ToUpper()
            Case "LA" : Return lblLA
            Case "JP" : Return lblJP
            Case "KR" : Return lblKR
            Case "SC" : Return lblSC
            Case "TC" : Return lblTC
            Case Else : Return Nothing
        End Select
    End Function

    ''' <summary>
    ''' 自定义文本框内容变化时，控制"确认"按钮的启用状态。
    ''' </summary>
    Private Sub tbCustom_TextChanged(sender As Object, e As EventArgs) Handles tbCustom.TextChanged
        btnConfirm.Enabled = Not String.IsNullOrEmpty(tbCustom.Text)
    End Sub
    ''' <summary>
    ''' 点击分组标签：切换对应预览标签的显隐。
    ''' tag 本身保留（用于再次点击切换回来），隐藏时 tag 变浅色作为视觉反馈。
    ''' </summary>
    Private Sub Tag_Click(sender As Object, e As EventArgs) _
        Handles tagLA.Click, tagJP.Click, tagKR.Click, tagSC.Click, tagTC.Click

        Dim tag = CType(sender, Label)
        Dim lbl As Label = Nothing

        Select Case tag.Name
            Case "tagLA" : lbl = lblLA
            Case "tagJP" : lbl = lblJP
            Case "tagKR" : lbl = lblKR
            Case "tagSC" : lbl = lblSC
            Case "tagTC" : lbl = lblTC
            Case Else : Return
        End Select

        Dim willShow As Boolean = Not lbl.Visible
        lbl.Visible = willShow
    End Sub
End Class