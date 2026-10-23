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

    ''' <summary>搜索目标列表（由 FormMain 提供）。</summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property TargetList As ListBox

#End Region

#Region "── 私有字段 ────────────────────────────────────────────"

    ''' <summary>匹配项在目标列表中的索引集合（升序）。</summary>
    Private _matches As New List(Of Integer)

    ''' <summary>当前匹配在 _matches 中的位置（-1 表示尚未选中任何匹配）。</summary>
    Private _currentMatch As Integer = -1

    ''' <summary>
    ''' 上一次搜索使用的关键词。
    ''' 注：当前实现中 EnsureMatches 每次都重算匹配，本字段仅在 RebuildMatches / 
    ''' SearchOption_CheckedChanged 里赋值，未参与分支判断，保留以维持原有结构。
    ''' </summary>
    Private _lastKeyword As String = ""

    ''' <summary>本窗体本次会话内生成的图标，FormClosed 时统一 Dispose。</summary>
    Private ReadOnly _ownedIcons As New List(Of Image)

#End Region

#Region "── 窗体生命周期 ────────────────────────────────────────"

    Private Sub FormSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyAwareIcons()

        ' 用当前列表选中项预填关键词
        If TargetList IsNot Nothing AndAlso TargetList.SelectedIndex >= 0 Then
            txtKeywords.Text = TargetList.SelectedItem.ToString()
        End If
        txtKeywords.Focus()
        txtKeywords.SelectAll()
        UpdateStatus("输入关键词进行查找")

        ' 初始化标题：有预填文本时带上关键词，否则纯显示「查找」
        If String.IsNullOrEmpty(txtKeywords.Text.Trim()) Then
            Me.Text = "查找"
        Else
            Me.Text = $"查找 - {txtKeywords.Text.Trim()}"
        End If
    End Sub

    ''' <summary>首次显示时手动居中于 Owner。</summary>
    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        CenterOnOwner()
    End Sub

    ''' <summary>释放本次会话生成的图标。</summary>
    Private Sub FormSearch_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        For Each img In _ownedIcons
            Try : img.Dispose() : Catch : End Try
        Next
        _ownedIcons.Clear()
    End Sub

#End Region

#Region "── DPI 图标 ────────────────────────────────────────────"

    ''' <summary>
    ''' 从 Icons.resx 应用本窗体的图标（含 CheckBox 形式的选项按钮）。
    ''' 资源缺失时静默跳过（控件保持 Designer 原图）。
    ''' </summary>
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

    ''' <summary>
    ''' 重新计算匹配项索引集合。
    ''' 依据三个开关（全字段 / 区分大小写 / 正则）组合：
    ''' · 普通模式：IndexOf / Equals（由 chkAllwords 决定）
    ''' · 正则模式：Regex.IsMatch，IgnoreCase 由 chkCases 决定，全字段匹配时自动加 ^(?:...)$
    ''' </summary>
    Private Function RebuildMatches(keyword As String) As Boolean
        _matches.Clear()
        _currentMatch = -1
        _lastKeyword = keyword

        ' 更新标题栏（不截断）
        If String.IsNullOrEmpty(keyword) Then
            Me.Text = "查找"
        Else
            Me.Text = $"查找 - {keyword}"
        End If

        If TargetList Is Nothing OrElse String.IsNullOrEmpty(keyword) Then Return False

        Dim total As Integer = TargetList.Items.Count

        Try
            If chkFormal.Checked Then
                ' ── 正则模式 ──
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
                ' ── 普通模式 ──
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
    ''' 本窗口是非模态的，目标列表随时可能增删（卸载/安装/重载目录），缓存的索引会过期，
    ''' 所以每次都按当前列表重算（几百项字符串比较，成本可忽略）。
    ''' </summary>
    Private Function EnsureMatches() As Boolean
        RebuildMatches(txtKeywords.Text.Trim())

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

    ''' <summary>选中并滚动到指定位置的匹配项。</summary>
    Private Sub SelectMatch(pos As Integer)
        If pos < 0 OrElse pos >= _matches.Count Then Return

        _currentMatch = pos
        Dim itemIndex = _matches(pos)
        If itemIndex < 0 OrElse itemIndex >= TargetList.Items.Count Then Return

        ' 兼容 MultiExtended：先清空所有已选项，再单选目标项
        TargetList.ClearSelected()
        TargetList.SelectedIndex = itemIndex
        TargetList.TopIndex = itemIndex

        UpdateStatus()
    End Sub

    ''' <summary>获取列表中当前第一个选中项的索引；无选中返回 -1。用作「下一项/上一项」的参照点。</summary>
    Private Function GetCurrentListIndex() As Integer
        If TargetList Is Nothing OrElse TargetList.SelectedIndices.Count = 0 Then Return -1
        Return TargetList.SelectedIndices(0)
    End Function

    ''' <summary>更新状态标签；传 custom 则直接显示 custom。</summary>
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

    ''' <summary>三个搜索选项开关变化时，重置缓存并立即重算匹配。</summary>
    Private Sub SearchOption_CheckedChanged(sender As Object, e As EventArgs) _
        Handles chkAllwords.CheckedChanged, chkCases.CheckedChanged, chkFormal.CheckedChanged

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

#End Region

#Region "── 按钮事件：查找系列 ──────────────────────────────────"

    ''' <summary>查找下一个：从列表当前选中项之后开始找匹配；到末尾循环。</summary>
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

        If foundPos < 0 Then foundPos = 0
        SelectMatch(foundPos)
    End Sub

    ''' <summary>查找上一个：从列表当前选中项之前开始找匹配；到开头循环。</summary>
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

        If foundPos < 0 Then foundPos = _matches.Count - 1
        SelectMatch(foundPos)
    End Sub

    ''' <summary>跳转到第一个匹配。</summary>
    Private Sub btnHead_Click(sender As Object, e As EventArgs) Handles btnHead.Click
        If Not EnsureMatches() Then Return
        SelectMatch(0)
    End Sub

    ''' <summary>跳转到最后一个匹配。</summary>
    Private Sub btnTail_Click(sender As Object, e As EventArgs) Handles btnTail.Click
        If Not EnsureMatches() Then Return
        SelectMatch(_matches.Count - 1)
    End Sub

#End Region

#Region "── 按钮事件：跳转与全选 ────────────────────────────────"

    ''' <summary>按序号跳转到列表项（1 起始）。</summary>
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

    ''' <summary>全选【搜索结果】：只选中当前关键词匹配的项，不影响其他列表项。</summary>
    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        If TargetList Is Nothing OrElse TargetList.Items.Count = 0 Then
            UpdateStatus("列表为空")
            Return
        End If

        ' 确保针对当前关键词的匹配已计算
        If Not EnsureMatches() Then Return

        ' 先清空旧选择，再逐项选中 _matches
        TargetList.ClearSelected()
        For Each idx As Integer In _matches
            If idx >= 0 AndAlso idx < TargetList.Items.Count Then TargetList.SetSelected(idx, True)
        Next

        UpdateStatus($"全选 {_matches.Count} 项结果")
    End Sub

#End Region

#Region "── 窗口定位 ────────────────────────────────────────────"

    ''' <summary>非模态窗体手动居中于 Owner（Owner 为空时使用系统默认位置）。</summary>
    Private Sub CenterOnOwner()
        If Owner Is Nothing Then Return
        Me.Location = New Point(
            Owner.Left + (Owner.Width - Me.Width) \ 2,
            Owner.Top + (Owner.Height - Me.Height) \ 2)
    End Sub

#End Region

#Region "── 快捷键分发 ──────────────────────────────────────────"

    ''' <summary>
    ''' 全局快捷键（FormSearch 为活动窗口时生效）：
    ''' Enter → 查找下一个；Esc → 关闭；Ctrl+G → 按输入框序号跳转；
    ''' F2 → 首个匹配；F3 → 下一个匹配；Shift+F3 → 上一个匹配；F4 → 末尾匹配。
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

#End Region

End Class