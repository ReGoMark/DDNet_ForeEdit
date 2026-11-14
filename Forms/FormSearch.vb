Imports System.ComponentModel
Imports System.Resources
Imports System.Text.RegularExpressions

''' <summary>
''' 字体列表查找窗体（非模态）。
''' 支持：关键词查找、上一个/下一个/首个/末尾匹配、按序号跳转、全选搜索结果。
''' 支持三种匹配选项：全字段、区分大小写、正则表达式。
''' 目标列表由 FormMain 通过 TargetList 注入；列表随时可能增删，故每次查找都按当前列表重算。
''' </summary>
Public Class FormSearch

#Region "── 公开属性 ────────────────────────────────────────────"

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property TargetList As ListBox

#End Region

#Region "── 私有字段 ────────────────────────────────────────────"

    Private _matches As New List(Of Integer)
    Private _currentMatch As Integer = -1
    Private ReadOnly _ownedIcons As New List(Of Image)

#End Region

#Region "── 窗体生命周期 ────────────────────────────────────────"

    Private Sub FormSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyAwareIcons()

        If TargetList IsNot Nothing AndAlso TargetList.SelectedIndex >= 0 Then
            txtKeywords.Text = TargetList.SelectedItem.ToString()
        End If
        txtKeywords.Focus()
        txtKeywords.SelectAll()
        UpdateStatus("输入关键词进行查找")

        If String.IsNullOrEmpty(txtKeywords.Text.Trim()) Then
            Me.Text = "查找"
        Else
            Me.Text = $"查找 - {txtKeywords.Text.Trim()}"
        End If
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        CenterOnOwner()
    End Sub

    Private Sub FormSearch_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        For Each img In _ownedIcons
            Try : img.Dispose() : Catch : End Try
        Next
        _ownedIcons.Clear()
    End Sub

#End Region

#Region "── DPI 图标 ────────────────────────────────────────────"

    Private Sub ApplyAwareIcons()
        Dim rm As ResourceManager
        Try
            rm = New ResourceManager("DNFE.Icons", GetType(FormMain).Assembly)
        Catch ex As MissingManifestResourceException
            Return
        End Try

        Dim map As New Dictionary(Of ButtonBase, String) From {
            {btnPrior, "prior"},
            {btnHead, "head"},
            {btnTail, "tail"},
            {chkAllwords, "allwords"},
            {chkCases, "cases"},
            {chkFormal, "formal"}
        }

        AwareIcon.ApplyButtonIcons(Me, rm, map, _ownedIcons)
    End Sub

#End Region

#Region "── 核心搜索逻辑 ────────────────────────────────────────"

    Private Function RebuildMatches(keyword As String) As Boolean
        _matches.Clear()
        _currentMatch = -1

        If String.IsNullOrEmpty(keyword) Then
            Me.Text = "查找"
        Else
            Me.Text = $"查找 - {keyword}"
        End If

        If TargetList Is Nothing OrElse String.IsNullOrEmpty(keyword) Then Return False

        Dim total As Integer = TargetList.Items.Count

        Try
            If chkFormal.Checked Then
                Dim opts As RegexOptions = RegexOptions.None
                If Not chkCases.Checked Then opts = opts Or RegexOptions.IgnoreCase

                Dim pattern As String = keyword
                If chkAllwords.Checked Then pattern = "^(?:" & pattern & ")$"

                Dim rx As New Regex(pattern, opts, TimeSpan.FromSeconds(2))
                For i As Integer = 0 To total - 1
                    If rx.IsMatch(TargetList.Items(i).ToString()) Then
                        _matches.Add(i)
                    End If
                Next
            Else
                Dim cmp As StringComparison = If(chkCases.Checked,
                                                 StringComparison.Ordinal,
                                                 StringComparison.OrdinalIgnoreCase)
                For i As Integer = 0 To total - 1
                    Dim item As String = TargetList.Items(i).ToString()
                    Dim hit As Boolean
                    If chkAllwords.Checked Then
                        hit = String.Equals(item, keyword, cmp)
                    Else
                        hit = item.IndexOf(keyword, cmp) >= 0
                    End If
                    If hit Then _matches.Add(i)
                Next
            End If
        Catch ex As ArgumentException
            UpdateStatus($"正则表达式无效：{ex.Message}")
            Return False
        Catch ex As Exception
            UpdateStatus($"搜索出错：{ex.Message}")
            Return False
        End Try

        Return _matches.Count > 0
    End Function

    Private Function EnsureMatches() As Boolean
        RebuildMatches(txtKeywords.Text.Trim())

        If _matches.Count = 0 Then
            If String.IsNullOrEmpty(lblStatus.Text) OrElse
               Not lblStatus.Text.StartsWith("正则") AndAlso
               Not lblStatus.Text.StartsWith("搜索出错") Then
                UpdateStatus("未找到匹配项")
            End If
            Return False
        End If
        Return True
    End Function

    Private Sub SelectMatch(pos As Integer)
        If pos < 0 OrElse pos >= _matches.Count Then Return

        _currentMatch = pos
        Dim itemIndex = _matches(pos)
        If itemIndex < 0 OrElse itemIndex >= TargetList.Items.Count Then Return

        TargetList.ClearSelected()
        TargetList.SelectedIndex = itemIndex
        TargetList.TopIndex = itemIndex

        UpdateStatus()
    End Sub

    Private Function GetCurrentListIndex() As Integer
        If TargetList Is Nothing OrElse TargetList.SelectedIndices.Count = 0 Then Return -1
        Return TargetList.SelectedIndices(0)
    End Function

    Private Sub UpdateStatus(Optional custom As String = "")
        If Not String.IsNullOrEmpty(custom) Then
            lblStatus.Text = custom
        ElseIf _matches.Count = 0 Then
            lblStatus.Text = "未找到匹配项"
        Else
            lblStatus.Text = $"第 {_currentMatch + 1}/{_matches.Count} 项"
        End If
    End Sub

#End Region

#Region "── 搜索选项事件 ────────────────────────────────────────"

    Private Sub SearchOption_CheckedChanged(sender As Object, e As EventArgs) _
        Handles chkAllwords.CheckedChanged, chkCases.CheckedChanged, chkFormal.CheckedChanged

        Dim kw = txtKeywords.Text.Trim()
        If String.IsNullOrEmpty(kw) Then
            UpdateStatus("输入关键词进行查找")
            Return
        End If

        If RebuildMatches(kw) Then
            UpdateStatus()
        Else
            If Not lblStatus.Text.StartsWith("正则") AndAlso
               Not lblStatus.Text.StartsWith("搜索出错") Then
                UpdateStatus("未找到匹配项")
            End If
        End If
    End Sub

#End Region

#Region "── 按钮事件：查找系列 ──────────────────────────────────"

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If Not EnsureMatches() Then Return

        Dim curIdx As Integer = GetCurrentListIndex()
        If curIdx < 0 Then
            SelectMatch(0)
            Return
        End If

        Dim foundPos As Integer = -1
        For i As Integer = 0 To _matches.Count - 1
            If _matches(i) > curIdx Then
                foundPos = i
                Exit For
            End If
        Next

        If foundPos < 0 Then foundPos = 0
        SelectMatch(foundPos)
    End Sub

    Private Sub btnPrior_Click(sender As Object, e As EventArgs) Handles btnPrior.Click
        If Not EnsureMatches() Then Return

        Dim curIdx = GetCurrentListIndex()
        If curIdx < 0 Then
            SelectMatch(_matches.Count - 1)
            Return
        End If

        Dim foundPos = -1
        For i = _matches.Count - 1 To 0 Step -1
            If _matches(i) < curIdx Then
                foundPos = i
                Exit For
            End If
        Next

        If foundPos < 0 Then foundPos = _matches.Count - 1
        SelectMatch(foundPos)
    End Sub

    Private Sub btnHead_Click(sender As Object, e As EventArgs) Handles btnHead.Click
        If Not EnsureMatches() Then Return
        SelectMatch(0)
    End Sub

    Private Sub btnTail_Click(sender As Object, e As EventArgs) Handles btnTail.Click
        If Not EnsureMatches() Then Return
        SelectMatch(_matches.Count - 1)
    End Sub

#End Region

#Region "── 按钮事件：跳转与全选 ────────────────────────────────"

    Private Sub btnTurn_Click(sender As Object, e As EventArgs) Handles btnTurn.Click
        If TargetList Is Nothing Then Return

        Dim idx As Integer
        If Not Integer.TryParse(txtKeywords.Text.Trim(), idx) Then
            UpdateStatus("请输入有效的序号")
            Return
        End If
        If idx < 1 OrElse idx > TargetList.Items.Count Then
            UpdateStatus($"序号需在 1 ~ {TargetList.Items.Count} 之间")
            Return
        End If

        TargetList.ClearSelected()
        TargetList.SelectedIndex = idx - 1
        TargetList.TopIndex = idx - 1
        lblStatus.Text = $"已跳转到第 {idx} 项"
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        If TargetList Is Nothing OrElse TargetList.Items.Count = 0 Then
            UpdateStatus("列表为空")
            Return
        End If

        If Not EnsureMatches() Then Return

        TargetList.ClearSelected()
        For Each idx As Integer In _matches
            If idx >= 0 AndAlso idx < TargetList.Items.Count Then TargetList.SetSelected(idx, True)
        Next

        UpdateStatus($"全选 {_matches.Count} 项结果")
    End Sub

#End Region

#Region "── 窗口定位 ────────────────────────────────────────────"

    Private Sub CenterOnOwner()
        If Owner Is Nothing Then Return
        Me.Location = New Point(
            Owner.Left + (Owner.Width - Me.Width) \ 2,
            Owner.Top + (Owner.Height - Me.Height) \ 2)
    End Sub

#End Region

#Region "── 快捷键分发 ──────────────────────────────────────────"

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Dim ctrl As Boolean = (keyData And Keys.Control) = Keys.Control
        Dim shift As Boolean = (keyData And Keys.Shift) = Keys.Shift
        Dim alt As Boolean = (keyData And Keys.Alt) = Keys.Alt
        Dim key As Keys = keyData And Keys.KeyCode

        If Not ctrl AndAlso Not alt Then
            Select Case key
                Case Keys.Enter
                    If Not shift Then
                        btnNext.PerformClick()
                        Return True
                    End If

                Case Keys.Escape
                    If Not shift Then
                        Me.Close()
                        Return True
                    End If

                Case Keys.F2
                    If Not shift Then
                        btnHead.PerformClick()
                        Return True
                    End If

                Case Keys.F3
                    If shift Then
                        btnPrior.PerformClick()
                    Else
                        btnNext.PerformClick()
                    End If
                    Return True

                Case Keys.F4
                    If Not shift Then
                        btnTail.PerformClick()
                        Return True
                    End If
            End Select
        End If

        If ctrl AndAlso Not shift AndAlso key = Keys.G Then
            btnTurn.PerformClick()
            Return True
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

#End Region

End Class