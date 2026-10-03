Public Class FormAbout

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles lblLicense.Click
        DialogLicense.ShowDialog(Me)
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs)
        Try
            Dim psi As New ProcessStartInfo
            psi.FileName = "https://space.bilibili.com/220248940?spm_id_from=333.1007.0.0"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)
        Try
            Dim psi As New ProcessStartInfo
            psi.FileName = "https://github.com/ReGoMark/DDNet_ForeEdit"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click
        Try
            Dim psi As New ProcessStartInfo()
            psi.FileName = "https://github.com/be5invis/Sarasa-Gothic"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Try
            Dim psi As New ProcessStartInfo
            psi.FileName = "https://github.com/teeworlds"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        Try
            Dim psi As New ProcessStartInfo
            psi.FileName = "https://github.com/ddnet"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles lblSponsor.Click
        DialogSponsor.ShowDialog(Me)
    End Sub

    Private Sub lblMail_Click(sender As Object, e As EventArgs) Handles lblMail.Click
        Process.Start(New ProcessStartInfo("mailto:regmvks@outlook.com") With {.UseShellExecute = True})
    End Sub

    Private Sub lblThanks_Click(sender As Object, e As EventArgs) Handles lblThanks.Click
        DialogThanks.ShowDialog(Me)
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Close
    End Sub

    Private Sub FormAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblVersion.Text = $"版本 26H2 ({FormMain.BuildVersion})"
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Try
            Dim psi As New ProcessStartInfo()
            psi.FileName = "https://github.com/ReGoMark/"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        MessageBox.Show($"多个字重的家族名完全相同，程序无法区分。
粗体、斜体是独立文件，家族名与常规体相同，程序只保留一个路径，预览不生效。
TTC 内部多个字体共用家族名，程序无法拆分。
可变字体只能读到默认字重。
写入 index.json 时多个字重文件被覆盖，只写入一个。
──────
总之，简单的切换字体使用并不会产生影响。")
    End Sub
End Class