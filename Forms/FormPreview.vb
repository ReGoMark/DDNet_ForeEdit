''' <summary>
''' 字体预览窗体，用于实时显示五种语言（西文、日文、韩文、简中、繁中）的字体效果。
''' 支持自定义预览文本、字号切换（10/12/16 点），并可通过 UpdatePreview 方法由主窗体动态更新各语言字体。
''' </summary>
Public Class FormPreview

    ' ─── 私有字段 ──────────────────────────────────────────────────────

    ''' <summary>RadioButton 到字号（磅值）的映射。</summary>
    Private ReadOnly _sizeMap As New Dictionary(Of RadioButton, Single)

    ''' <summary>记录各标签的初始文本（用于还原）。</summary>
    Private ReadOnly _defaultTexts As New Dictionary(Of Label, String)

    ''' <summary>当前生效的字号（磅值）。</summary>
    Private _currentSize As Single = 10.0F

    ' ─── 窗体生命周期 ──────────────────────────────────────────────────

    ''' <summary>窗体加载时初始化字号映射、默认文本，并禁用“确认”按钮。</summary>
    Private Sub FormPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _sizeMap(rbFont10) = 10.0F
        _sizeMap(rbFont12) = 12.0F
        _sizeMap(rbFont16) = 16.0F
        rbFont10.Checked = True

        ' 记录各标签的初始显示文本
        For Each lbl In GetPreviewLabels()
            _defaultTexts(lbl) = lbl.Text
        Next
        btnApply.Enabled = False
    End Sub

    ' ─── 自定义文本控制 ──────────────────────────────────────────────

    ''' <summary>将自定义文本应用到所有预览标签。</summary>
    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Dim customText As String = tbCustom.Text
        ' 若为空则设为空格，防止标签高度塌陷
        If String.IsNullOrEmpty(customText) Then customText = " "

        For Each lbl In GetPreviewLabels()
            lbl.Text = customText
        Next
    End Sub

    ''' <summary>将所有预览标签恢复为初始文本。</summary>
    Private Sub btnRecovery_Click(sender As Object, e As EventArgs) Handles btnRecovery.Click
        For Each lbl In GetPreviewLabels()
            If _defaultTexts.ContainsKey(lbl) Then
                lbl.Text = _defaultTexts(lbl)
            End If
        Next
    End Sub

    ' ─── 字号切换 ──────────────────────────────────────────────────────

    ''' <summary>字号单选按钮切换时更新所有预览标签的字号。</summary>
    Private Sub rbFontSize_CheckedChanged(sender As Object, e As EventArgs) _
        Handles rbFont10.CheckedChanged, rbFont12.CheckedChanged, rbFont16.CheckedChanged

        Dim rb = CType(sender, RadioButton)
        If Not rb.Checked Then Return

        _currentSize = _sizeMap(rb)
        For Each lbl In GetPreviewLabels()
            lbl.Font = New Font(lbl.Font.FontFamily, _currentSize, lbl.Font.Style)
        Next
    End Sub

    ' ─── 公开接口（供主窗体调用） ──────────────────────────────────

    ''' <summary>
    ''' 由主窗体调用，更新指定语言的预览标签字体。
    ''' </summary>
    ''' <param name="langKey">语言键（"LA","JP","KR","SC","TC"）。</param>
    ''' <param name="fontName">GDI+ 字体名称（由主窗体提供）。</param>
    ''' <param name="applyFontFunc">主窗体传入的字体应用委托，负责根据 fontName 创建 Font 对象并赋予标签。</param>
    Public Sub UpdatePreview(langKey As String, fontName As String,
                             applyFontFunc As Action(Of Label, String))
        Dim lbl As Label = GetLabelByLang(langKey)
        If lbl Is Nothing Then Return

        If Not String.IsNullOrEmpty(fontName) AndAlso fontName <> "(不指定)" Then
            applyFontFunc(lbl, fontName)
            ' 统一应用当前字号
            lbl.Font = New Font(lbl.Font.FontFamily, _currentSize, lbl.Font.Style)
        Else
            lbl.Font = New Font(Me.Font.FontFamily, _currentSize)
        End If
    End Sub

    ' ─── 私有辅助方法 ──────────────────────────────────────────────────

    ''' <summary>获取所有预览标签的集合。</summary>
    Private Function GetPreviewLabels() As IEnumerable(Of Label)
        Return {lblLA, lblJP, lblKR, lblSC, lblTC}
    End Function

    ''' <summary>根据语言键返回对应的预览标签。</summary>
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

    ''' <summary>自定义文本框内容变化时，控制“确认”按钮的启用状态。</summary>
    Private Sub tbCustom_TextChanged(sender As Object, e As EventArgs) Handles tbCustom.TextChanged
        btnApply.Enabled = Not String.IsNullOrEmpty(tbCustom.Text)
    End Sub

End Class