Imports System.ComponentModel
Imports System.Drawing

''' <summary>
''' 挂起任务进度对话框（纯展示，无交互）。
''' 
''' 展示任务名称、进度条、进度计数和说明文本；任务在后台运行时由调用方持续推送进度。
''' 窗口本身不提供暂停/继续/取消操作——挂起任务的处理是短时的 IO 操作，
''' 用户在窗口出现到关闭之间来不及交互，直接展示进度等待完成即可。
''' 
''' 使用方式：
'''   Using dlg As New DialogPending()
'''       dlg.SetNote("任务执行完毕后将启动 DDNet ForeEdit")
'''       dlg.Show()
'''       For i = 1 To total
'''           ' ... 执行任务 ...
'''           dlg.UpdateTask("正在执行: xxx", i, total)
'''       Next
'''       dlg.Finish()
'''   End Using
''' 
''' 注意：窗体为非模态（Show），进度更新依赖任务循环内的 Application.DoEvents 才能实时显示。
''' </summary>
Public Class Pending

#Region "── 公开接口 ──"

    ''' <summary>更新任务名称、进度和分数。</summary>
    ''' <param name="taskName">任务名称，显示在左上角。</param>
    ''' <param name="current">当前进度。</param>
    ''' <param name="total">总进度。&gt; 0：确定进度条 + "current/total"；&lt;= 0：Marquee + "-/-"。</param>
    Public Sub UpdateTask(taskName As String, current As Integer, total As Integer)
        If Me.IsDisposed OrElse Me.Disposing Then Return

        lblTask.Text = taskName

        If total > 0 Then
            ProgressBar1.Style = ProgressBarStyle.Continuous
            ProgressBar1.Maximum = total
            ProgressBar1.Value = Math.Min(Math.Max(current, 0), total)
            lblCount.Text = $"{current}/{total}"
        Else
            ProgressBar1.Style = ProgressBarStyle.Marquee
            lblCount.Text = "-/-"
        End If

        Application.DoEvents()
    End Sub

    ''' <summary>设置底部说明文本（如"任务执行完毕后将启动……"）。</summary>
    Public Sub SetNote(text As String)
        lblNote.Text = text
    End Sub

    ''' <summary>任务结束，关闭窗口。调用方在任务完成后调用。</summary>
    Public Sub Finish()
        Close()
    End Sub

#End Region

#Region "── 窗体生命周期 ──"

    Private Sub DialogTask_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterOnOwner()
    End Sub

#End Region

#Region "── 窗口位置与关闭控制 ──"

    ''' <summary>将窗口居中于 Owner；无 Owner 时居中于屏幕。</summary>
    Private Sub CenterOnOwner()
        If Me.Owner IsNot Nothing Then
            Me.Location = New Point(
                Owner.Left + (Owner.Width - Me.Width) \ 2,
                Owner.Top + (Owner.Height - Me.Height) \ 2)
        Else
            Dim wa = Screen.PrimaryScreen.WorkingArea
            Me.Location = New Point(
                wa.Left + (wa.Width - Me.Width) \ 2,
                wa.Top + (wa.Height - Me.Height) \ 2)
        End If
    End Sub

    ' 禁用窗口右上角的关闭按钮：任务执行中不允许用户关窗（进度对话框由调用方 Finish 关闭）
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or &H200 ' CS_NOCLOSE
            Return cp
        End Get
    End Property

#End Region

End Class