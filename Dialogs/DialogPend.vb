Public Class DialogPend

    ''' <summary>
    ''' 更新任务名称、进度和分数。
    ''' </summary>
    Public Sub UpdateTask(taskName As String, current As Integer, total As Integer)
        If Me.IsDisposed OrElse Me.Disposing Then Return

        Label1.Text = taskName
        ProgressBar1.Visible = True

        If total > 0 Then
            ProgressBar1.Style = ProgressBarStyle.Continuous
            ProgressBar1.Maximum = total
            ProgressBar1.Value = Math.Min(current, total)
            Label2.Text = $"{current}/{total}"
        Else
            ProgressBar1.Style = ProgressBarStyle.Marquee
            Label2.Text = "-/-"
        End If

        Application.DoEvents()
    End Sub

    Private Sub DialogPend_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    ' 禁用窗口右上角的关闭按钮（防止用户误关导致中断）
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or &H200 ' CS_NOCLOSE
            Return cp
        End Get
    End Property

End Class