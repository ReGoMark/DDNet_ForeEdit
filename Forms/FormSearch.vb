Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class FormSearch

    ''' <summary>
    ''' 搜索目标列表（由 FormMain 提供）。
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property TargetList As ListBox

    ''' <summary>
    ''' 匹配项在目标列表中的索引集合（升序）。
    ''' </summary>
    Private _matches As New List(Of Integer)

    ''' <summary>
    ''' 当前匹配在 _matches 中的位置。
    ''' </summary>
    Private _currentMatch As Integer = -1

    ''' <summary>
    ''' 上一次搜索使用的关键词，避免重复计算匹配。 三个复选框状态改变时会被重置，强制下次重算。
    ''' </summary>
    Private _lastKeyword As String = ""

    ' ─── 生命周期 ──────────────────────────────────────────────────

    Private Sub FormSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 用设计器里的 ToolTip1（不是局部变量，否则 Load 结束后就被 GC）
        ToolTip1.SetToolTip(btnSelectAll, "全选所有匹配结果")
        ToolTip1.SetToolTip(cmbAllwords, "全字匹配")
        ToolTip1.SetToolTip(cmbCases, "区分大小写")
        ToolTip1.SetToolTip(cmbFormal, "正则表达式")

        ' 用当前列表选中项预填关键词
        If TargetList IsNot Nothing AndAlso TargetList.SelectedIndex >= 0 Then
            txtKeywords.Text = TargetList.SelectedItem.ToString()
        End If
        txtKeywords.Focus()
        txtKeywords.SelectAll()
        UpdateStatus("输入关键词进行查找")
    End Sub

    ' ─── 核心搜索逻辑 ─────────────────────────────────────────────

    ''' <summary>
    ''' 重新计算匹配项索引集合。 依据三个开关（全字匹配 / 区分大小写 / 正则）组合计算：
    ''' - 普通模式：IndexOf / Equals（由 _allfields 决定）
    ''' - 正则模式：Regex.IsMatch，IgnoreCase 由 _cases 决定，全字匹配时自动加 ^(?:...)$
    ''' </summary>
    Private Function RebuildMatches(keyword As String) As Boolean
        _matches.Clear()
        _currentMatch = -1
        _lastKeyword = keyword

        If TargetList Is Nothing OrElse String.IsNullOrEmpty(keyword) Then Return False

        Dim total As Integer = TargetList.Items.Count

        Try
            If cmbFormal.Checked Then
                ' ── 正则模式 ──
                Dim opts As RegexOptions = RegexOptions.None
                If Not cmbCases.Checked Then opts = opts Or RegexOptions.IgnoreCase

                Dim pattern As String = keyword
                If cmbAllwords.Checked Then pattern = "^(?:" & pattern & ")$"

                Dim rx As New Regex(pattern, opts, TimeSpan.FromSeconds(2))
                For i As Integer = 0 To total - 1
                    If rx.IsMatch(TargetList.Items(i).ToString()) Then
                        _matches.Add(i)
                    End If
                Next
            Else
                ' ── 普通模式 ──
                Dim cmp As StringComparison = If(cmbCases.Checked,
                                                 StringComparison.Ordinal,
                                                 StringComparison.OrdinalIgnoreCase)
                For i As Integer = 0 To total - 1
                    Dim item As String = TargetList.Items(i).ToString()
                    Dim hit As Boolean
                    If cmbAllwords.Checked Then
                        hit = String.Equals(item, keyword, cmp)
                    Else
                        hit = item.IndexOf(keyword, cmp) >= 0
                    End If
                    If hit Then _matches.Add(i)
                Next
            End If
        Catch ex As ArgumentException
            ' 正则语法错误：给出提示，不抛出
            UpdateStatus($"正则表达式无效：{ex.Message}")
            Return False
        Catch ex As Exception
            UpdateStatus($"搜索出错：{ex.Message}")
            Return False
        End Try

        Return _matches.Count > 0
    End Function

    ''' <summary>
    ''' 确保匹配项针对当前关键词已经计算过。
    ''' </summary>
    Private Function EnsureMatches() As Boolean
        Dim kw = txtKeywords.Text.Trim()
        If kw <> _lastKeyword Then RebuildMatches(kw)

        If _matches.Count = 0 Then
            ' 若上一次 Rebuild 已经写过错误状态，则不覆盖
            If String.IsNullOrEmpty(lblStatus.Text) OrElse
               Not lblStatus.Text.StartsWith("正则") AndAlso
               Not lblStatus.Text.StartsWith("搜索出错") Then
                UpdateStatus("未找到匹配项")
            End If
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 选中并滚动到指定位置的匹配项。
    ''' </summary>
    Private Sub SelectMatch(pos As Integer)
        If pos < 0 OrElse pos >= _matches.Count Then Return

        _currentMatch = pos
        Dim itemIndex = _matches(pos)

        ' ── 兼容 MultiExtended：先清空所有已选项，再单选目标项 ──
        TargetList.ClearSelected()
        TargetList.SelectedIndex = itemIndex
        TargetList.TopIndex = itemIndex

        UpdateStatus()
    End Sub

    ''' <summary>
    ''' 获取列表中当前第一个选中项的索引；无选中返回 -1。 用作「下一项 / 上一项」的参照点。
    ''' </summary>
    Private Function GetCurrentListIndex() As Integer
        If TargetList Is Nothing OrElse TargetList.SelectedIndices.Count = 0 Then Return -1
        Return TargetList.SelectedIndices(0)
    End Function

    ''' <summary>
    ''' 更新状态标签。
    ''' </summary>
    Private Sub UpdateStatus(Optional custom As String = "")
        If Not String.IsNullOrEmpty(custom) Then
            lblStatus.Text = custom
        ElseIf _matches.Count = 0 Then
            lblStatus.Text = "未找到匹配项"
        Else
            lblStatus.Text = $"第 {_currentMatch + 1}/{_matches.Count} 项"
        End If
    End Sub

    ' ─── 搜索选项事件 ─────────────────────────────────────────────

    ''' <summary>
    ''' 三个搜索选项开关变化时，重置缓存并立即重算匹配。
    ''' </summary>
    Private Sub SearchOption_CheckedChanged(sender As Object, e As EventArgs) _
        Handles cmbAllwords.CheckedChanged, cmbCases.CheckedChanged, cmbFormal.CheckedChanged

        ' 关键：清空关键词缓存，强制下次重算
        _lastKeyword = ""

        Dim kw = txtKeywords.Text.Trim()
        If String.IsNullOrEmpty(kw) Then
            UpdateStatus("输入关键词进行查找")
            Return
        End If

        If RebuildMatches(kw) Then
            UpdateStatus()
        Else
            ' RebuildMatches 内部已 UpdateStatus 时不要覆盖
            If Not lblStatus.Text.StartsWith("正则") AndAlso
               Not lblStatus.Text.StartsWith("搜索出错") Then
                UpdateStatus("未找到匹配项")
            End If
        End If
    End Sub

    ' ─── 按钮事件 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 查找下一个：从列表当前选中项之后开始找匹配；到末尾循环。
    ''' </summary>
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If Not EnsureMatches() Then Return

        Dim curIdx As Integer = GetCurrentListIndex()
        If curIdx < 0 Then
            ' 列表中没有选中项 → 从第一个匹配开始
            SelectMatch(0)
            Return
        End If

        ' 在 _matches（升序）中找第一个 > curIdx 的位置
        Dim foundPos As Integer = -1
        For i As Integer = 0 To _matches.Count - 1
            If _matches(i) > curIdx Then
                foundPos = i
                Exit For
            End If
        Next

        ' 没找到则循环到第一个
        If foundPos < 0 Then foundPos = 0
        SelectMatch(foundPos)
    End Sub

    ''' <summary>
    ''' 查找上一个：从列表当前选中项之前开始找匹配；到开头循环。
    ''' </summary>
    Private Sub btnPrior_Click(sender As Object, e As EventArgs) Handles btnPrior.Click
        If Not EnsureMatches() Then Return

        Dim curIdx = GetCurrentListIndex()
        If curIdx < 0 Then
            ' 列表中没有选中项 → 从最后一个匹配开始
            SelectMatch(_matches.Count - 1)
            Return
        End If

        ' 在 _matches（升序）中找最后一个 < curIdx 的位置
        Dim foundPos = -1
        For i = _matches.Count - 1 To 0 Step -1
            If _matches(i) < curIdx Then
                foundPos = i
                Exit For
            End If
        Next

        ' 没找到则循环到最后一个
        If foundPos < 0 Then foundPos = _matches.Count - 1
        SelectMatch(foundPos)
    End Sub

    ''' <summary>
    ''' 跳转到第一个匹配。
    ''' </summary>
    Private Sub btnHead_Click(sender As Object, e As EventArgs) Handles btnHead.Click
        If Not EnsureMatches() Then Return
        SelectMatch(0)
    End Sub

    ''' <summary>
    ''' 跳转到最后一个匹配。
    ''' </summary>
    Private Sub btnTail_Click(sender As Object, e As EventArgs) Handles btnTail.Click
        If Not EnsureMatches() Then Return
        SelectMatch(_matches.Count - 1)
    End Sub

    ''' <summary>
    ''' 按序号跳转到列表项（1 起始）。
    ''' </summary>
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

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        CenterOnOwner()
    End Sub

    ''' <summary>
    ''' 非模态窗体手动居中于 Owner。
    ''' </summary>
    Private Sub CenterOnOwner()
        If Owner Is Nothing Then Return
        Me.Location = New Point(
            Owner.Left + (Owner.Width - Me.Width) \ 2,
            Owner.Top + (Owner.Height - Me.Height) \ 2)
    End Sub

    ''' <summary>
    ''' 全选【搜索结果】：只选中当前关键词匹配的项。
    ''' </summary>
    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        If TargetList Is Nothing OrElse TargetList.Items.Count = 0 Then
            UpdateStatus("列表为空")
            Return
        End If

        ' 确保针对当前关键词的匹配已计算
        If Not EnsureMatches() Then Return

        ' 只全选搜索结果：先清空旧选择，再逐项选中 _matches
        TargetList.ClearSelected()
        For Each idx As Integer In _matches
            TargetList.SetSelected(idx, True)
        Next

        UpdateStatus($"全选 {_matches.Count} 项结果")
    End Sub

    ' ─── 快捷键分发 ────────────────────────────────────────────────

    ''' <summary>
    ''' 全局快捷键（FormSearch 为活动窗口时生效）：
    '''   Enter       → 查找下一个
    '''   Esc         → 关闭
    '''   Ctrl+G      → 按输入框的序号跳转
    '''   F2          → 首个匹配
    '''   F3          → 下一个匹配
    '''   Shift+F3    → 上一个匹配
    '''   F4          → 末尾匹配
    ''' 所有按键统一在此处理，控件级事件不再重复拦截，避免冗余。
    ''' </summary>
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Dim ctrl As Boolean = (keyData And Keys.Control) = Keys.Control
        Dim shift As Boolean = (keyData And Keys.Shift) = Keys.Shift
        Dim alt As Boolean = (keyData And Keys.Alt) = Keys.Alt
        Dim key As Keys = keyData And Keys.KeyCode

        ' ── 无修饰键：Enter / Esc / F2 / F3 / F4 ──
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

        ' ── Ctrl+G：按输入框序号跳转 ──
        If ctrl AndAlso Not shift AndAlso key = Keys.G Then
            btnTurn.PerformClick()
            Return True
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

End Class