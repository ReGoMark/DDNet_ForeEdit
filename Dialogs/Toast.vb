Imports System.Media
Imports System.Runtime.InteropServices

Public Class Toast

#Region "── 常量 ──"

    Private Const FadeStepMs As Integer = 15
    Private Const FadeDurationMs As Integer = 250
    Private Const DefaultShowDurationMs As Integer = 2000
    Private Const TargetOpacity As Single = 1.0F

    Private Const MoveStepMs As Integer = 15
    Private Const MoveEasingFactor As Single = 0.3F
    Private Const MoveThreshold As Integer = 1

    Private Const AnchorLeft As Integer = 20
    Private Const AnchorBottom As Integer = 20
    Private Const ToastGap As Integer = 6
    Private Const MaxToasts As Integer = 3

    Private Const MaxToastWidth As Integer = 350
    Private Const ContentPadding As Integer = 12

    ' 强调色背景的混合比例（0.0 = 纯白，1.0 = 纯 Highlight）
    Private Const AccentBlendFactor As Single = 0.1F

    ' 提示音最短间隔（避免连续触发时声音叠加）
    Private Const SoundThrottleMs As Integer = 400

#End Region

#Region "── Win32 API ──"

    Private Const SWP_NOSIZE As UInteger = &H1
    Private Const SWP_NOMOVE As UInteger = &H2
    Private Const SWP_NOACTIVATE As UInteger = &H10
    Private Shared ReadOnly HWND_TOPMOST As New IntPtr(-1)

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetWindowPos(
        hWnd As IntPtr, hWndInsertAfter As IntPtr,
        X As Integer, Y As Integer, cx As Integer, cy As Integer,
        uFlags As UInteger) As Boolean
    End Function

#End Region

#Region "── 颜色工具 ──"

    Private Shared Function BlendColors(baseColor As Color, overlayColor As Color, overlayAlpha As Single) As Color
        Dim a = Math.Max(0.0F, Math.Min(1.0F, overlayAlpha))
        Dim r = CInt(baseColor.R * (1 - a) + overlayColor.R * a)
        Dim g = CInt(baseColor.G * (1 - a) + overlayColor.G * a)
        Dim b = CInt(baseColor.B * (1 - a) + overlayColor.B * a)
        Return Color.FromArgb(r, g, b)
    End Function

#End Region

#Region "── 系统提示音 ──"

    Private Shared _lastSoundTime As DateTime = DateTime.MinValue

    ''' <summary>
    ''' 播放系统提示音（Exclamation）。多次触发时按 SoundThrottleMs 节流，避免声音叠加。 系统关闭声音时静默不播，不抛异常。
    ''' </summary>
    Private Shared Sub PlayNotificationSound()
        Try
            If (DateTime.Now - _lastSoundTime).TotalMilliseconds < SoundThrottleMs Then Return
            _lastSoundTime = DateTime.Now
            SystemSounds.Exclamation.Play()
        Catch
        End Try
    End Sub

#End Region

#Region "── 静态管理 ──"

    Private Shared ReadOnly _activeToasts As New List(Of Toast)()

    ''' <summary>
    ''' 在主窗口左下角显示一条 Toast 通知。
    ''' </summary>
    ''' <param name="owner">主窗口，Toast 位置相对于它计算</param>
    ''' <param name="text">通知文本</param>
    ''' <param name="durationMs">完全显示状态的停留时长（毫秒），默认 2000</param>
    ''' <param name="withSound">是否播放系统提示音，默认 False（静默）</param>
    Public Shared Sub ShowToast(owner As Form, text As String,
                                Optional durationMs As Integer = DefaultShowDurationMs,
                                Optional withSound As Boolean = False)
        If owner Is Nothing OrElse owner.IsDisposed Then Return

        While _activeToasts.Count >= MaxToasts
            Dim oldest = _activeToasts(0)
            _activeToasts.RemoveAt(0)
            If oldest IsNot Nothing AndAlso Not oldest.IsDisposed Then
                Try
                    oldest.Close()
                Catch
                End Try
            End If
        End While

        If withSound Then PlayNotificationSound()

        Dim t As New Toast()
        t.ShowInternal(owner, text, durationMs)
        _activeToasts.Add(t)

        RelayoutToasts(owner)
    End Sub

    Private Shared Sub RelayoutToasts(owner As Form)
        If owner Is Nothing OrElse owner.IsDisposed Then Return

        Dim bottomY = owner.Bottom - AnchorBottom
        For i = _activeToasts.Count - 1 To 0 Step -1
            Dim t = _activeToasts(i)
            If t Is Nothing OrElse t.IsDisposed OrElse t.Disposing Then Continue For

            Dim targetTop = bottomY - t.Height
            t.MoveTo(targetTop)
            bottomY -= t.Height + ToastGap
        Next
    End Sub

#End Region

#Region "── 状态字段 ──"

    Private _owner As Form = Nothing
    Private _showDurationMs As Integer = DefaultShowDurationMs
    Private _fadeDirection As Integer = 0
    Private _fadeTimer As Timer = Nothing
    Private _stayTimer As Timer = Nothing

    Private _targetTop As Integer = 0
    Private _moveTimer As Timer = Nothing

    Private _fixedHeight As Integer = 0

#End Region

#Region "── 窗体行为 ──"

    Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H80   ' WS_EX_TOOLWINDOW
            Return cp
        End Get
    End Property

#End Region

#Region "── 强调色外观 ──"

    Private Sub ApplyAccentAppearance()
        Me.BackColor = BlendColors(Color.White, SystemColors.Highlight, AccentBlendFactor)
        Label1.BackColor = Color.Transparent
        Label1.ForeColor = SystemColors.Highlight
    End Sub

#End Region

#Region "── 显示流程 ──"

    Private Sub ShowInternal(owner As Form, text As String, durationMs As Integer)
        _owner = owner
        _showDurationMs = Math.Max(200, durationMs)

        Me.StartPosition = FormStartPosition.Manual
        Me.ShowInTaskbar = False
        Me.Opacity = 0.0

        _fixedHeight = Me.ClientSize.Height

        ApplyAccentAppearance()

        Label1.Text = text
        ResizeWidthToFit()

        Dim initialY = owner.Bottom - AnchorBottom - Me.Height
        Me.Location = New Point(owner.Left + AnchorLeft, initialY)
        _targetTop = initialY

        Me.Show(owner)

        Try
            SetWindowPos(Me.Handle, HWND_TOPMOST, 0, 0, 0, 0,
                         SWP_NOMOVE Or SWP_NOSIZE Or SWP_NOACTIVATE)
        Catch
        End Try

        StartFade(+1)
    End Sub

    Private Sub ResizeWidthToFit()
        Label1.AutoSize = False
        Label1.AutoEllipsis = True

        Label1.MaximumSize = New Size(0, 0)
        Dim naturalSize = Label1.GetPreferredSize(Size.Empty)

        Dim targetClientWidth = Math.Min(naturalSize.Width + ContentPadding * 2, MaxToastWidth)

        Me.ClientSize = New Size(targetClientWidth, _fixedHeight)

        Label1.Location = New Point(ContentPadding, ContentPadding)
        Label1.Size = New Size(
            Me.ClientSize.Width - ContentPadding * 2,
            Me.ClientSize.Height - ContentPadding * 2)
    End Sub

    Friend Sub MoveTo(targetTop As Integer)
        _targetTop = targetTop

        If Math.Abs(Me.Top - targetTop) <= MoveThreshold Then
            Me.Top = targetTop
            _moveTimer?.Stop()
            Return
        End If

        StartMove()
    End Sub

#End Region

#Region "── 位置移动动画 ──"

    Private Sub StartMove()
        If _moveTimer Is Nothing Then
            _moveTimer = New Timer()
            _moveTimer.Interval = MoveStepMs
            AddHandler _moveTimer.Tick, AddressOf OnMoveTick
        End If
        _moveTimer.Start()
    End Sub

    Private Sub OnMoveTick(sender As Object, e As EventArgs)
        If Me.IsDisposed OrElse Me.Disposing Then
            _moveTimer?.Stop()
            Return
        End If

        Dim dy = _targetTop - Me.Top
        If Math.Abs(dy) <= MoveThreshold Then
            Me.Top = _targetTop
            _moveTimer.Stop()
            Return
        End If

        Dim step0 = CInt(dy * MoveEasingFactor)
        If step0 = 0 Then step0 = If(dy > 0, 1, -1)
        Me.Top += step0
    End Sub

#End Region

#Region "── 淡入淡出动画 ──"

    Private Sub StartFade(direction As Integer)
        _fadeDirection = direction
        If _fadeTimer Is Nothing Then
            _fadeTimer = New Timer()
            _fadeTimer.Interval = FadeStepMs
            AddHandler _fadeTimer.Tick, AddressOf OnFadeTick
        End If
        _fadeTimer.Start()
    End Sub

    Private Sub OnFadeTick(sender As Object, e As EventArgs)
        If Me.IsDisposed OrElse Me.Disposing Then
            _fadeTimer?.Stop()
            Return
        End If

        Dim stepValue = FadeStepMs / CDbl(FadeDurationMs)
        Dim nextOpacity = Me.Opacity + _fadeDirection * stepValue

        If _fadeDirection > 0 Then
            If nextOpacity >= TargetOpacity Then
                Me.Opacity = TargetOpacity
                _fadeTimer.Stop()
                StartStay()
            Else
                Me.Opacity = nextOpacity
            End If
        Else
            If nextOpacity <= 0.0 Then
                Me.Opacity = 0.0
                _fadeTimer.Stop()
                Me.Close()
            Else
                Me.Opacity = nextOpacity
            End If
        End If
    End Sub

    Private Sub StartStay()
        If _stayTimer Is Nothing Then
            _stayTimer = New Timer()
            AddHandler _stayTimer.Tick, AddressOf OnStayTick
        End If
        _stayTimer.Interval = _showDurationMs
        _stayTimer.Start()
    End Sub

    Private Sub OnStayTick(sender As Object, e As EventArgs)
        _stayTimer.Stop()
        StartFade(-1)
    End Sub

#End Region

#Region "── 点击关闭 ──"

    Private Sub Toast_Click(sender As Object, e As EventArgs) _
        Handles MyBase.Click, Label1.Click
        If _fadeDirection < 0 Then Return
        _stayTimer?.Stop()
        _fadeTimer?.Stop()
        StartFade(-1)
    End Sub

#End Region

#Region "── 释放 ──"

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        If _fadeTimer IsNot Nothing Then
            _fadeTimer.Stop() : _fadeTimer.Dispose() : _fadeTimer = Nothing
        End If
        If _stayTimer IsNot Nothing Then
            _stayTimer.Stop() : _stayTimer.Dispose() : _stayTimer = Nothing
        End If
        If _moveTimer IsNot Nothing Then
            _moveTimer.Stop() : _moveTimer.Dispose() : _moveTimer = Nothing
        End If

        _activeToasts.Remove(Me)
        Dim ownerRef = _owner
        _owner = Nothing

        If ownerRef IsNot Nothing AndAlso Not ownerRef.IsDisposed Then
            Try
                RelayoutToasts(ownerRef)
            Catch
            End Try
        End If

        MyBase.OnFormClosed(e)
    End Sub

#End Region

End Class