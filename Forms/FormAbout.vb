Public Class FormAbout

    Private Sub lblLicense_Click(sender As Object, e As EventArgs) Handles lblLicense.Click
        DialogLicense.ShowDialog(Me)
    End Sub

    Private Sub lblSarasaSans_Click(sender As Object, e As EventArgs) Handles lblSarasaSans.Click
        Try
            Dim psi As New ProcessStartInfo()
            psi.FileName = "https://github.com/be5invis/Sarasa-Gothic"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblTeeworlds_Click(sender As Object, e As EventArgs) Handles lblTeeworlds.Click
        Try
            Dim psi As New ProcessStartInfo
            psi.FileName = "https://github.com/teeworlds"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblDDNet_Click(sender As Object, e As EventArgs) Handles lblDDNet.Click
        Try
            Dim psi As New ProcessStartInfo
            psi.FileName = "https://github.com/ddnet"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblSponsor_Click(sender As Object, e As EventArgs) Handles lblSponsor.Click
        DialogSponsor.ShowDialog(Me)
    End Sub

    Private Sub lblMail_Click(sender As Object, e As EventArgs) Handles lblMail.Click
        Process.Start(New ProcessStartInfo("mailto:regmvks@outlook.com") With {.UseShellExecute = True})
    End Sub

    Private Sub lblThanks_Click(sender As Object, e As EventArgs) Handles lblThanks.Click
        DialogThanks.ShowDialog(Me)
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Close()
    End Sub

    Private Sub FormAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblVersion.Text = $"版本 26H2 ({FormMain.BuildVersion})"
    End Sub

    Private Sub lblGithub_Click(sender As Object, e As EventArgs) Handles lblGithub.Click
        Try
            Dim psi As New ProcessStartInfo()
            psi.FileName = "https://github.com/ReGoMark/DDNet_ForeEdit"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lbldotNET10_Click(sender As Object, e As EventArgs) Handles lbldotNET10.Click
        Try
            Dim psi As New ProcessStartInfo()
            psi.FileName = "https://dotnet.microsoft.com/"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblGitee_Click(sender As Object, e As EventArgs) Handles lblGitee.Click
        Try
            Dim psi As New ProcessStartInfo()
            psi.FileName = "https://gitee.com/regomark/DDNet_ForeEdit"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class