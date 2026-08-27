Imports System.Drawing.Text
Imports System.IO

''' <summary>
''' 启动自检对话框：检测系统是否安装必备字体，提供安装指引。
''' </summary>
Public Class DialogCheck

#Region "常量"

    ''' <summary>需要检测的字体名称。</summary>
    Private Const FontName As String = "等距更纱黑体 Slab SC"

    ''' <summary>字体文件名（用于本地打开定位）。</summary>
    Private Const FontFileName As String = "SarasaMonoSlabSC-Regular.ttf"

#End Region

#Region "字体检测（公开）"

    ''' <summary>检测目标字体是否已安装。</summary>
    Public Shared Function IsFontInstalled() As Boolean
        Using fc As New InstalledFontCollection()
            Return fc.Families.Any(Function(f) f.Name.Equals(FontName, StringComparison.OrdinalIgnoreCase))
        End Using
    End Function

#End Region

#Region "按钮事件"

    ''' <summary>打开字体文件所在文件夹并高亮该文件。</summary>
    Private Sub btnFolder_Click(sender As Object, e As EventArgs) Handles btnFolder.Click
        Dim fontFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", FontFileName)

        If File.Exists(fontFile) Then
            Dim psi As New ProcessStartInfo()
            psi.FileName = "explorer.exe"
            psi.Arguments = $"/select,""{fontFile}"""
            psi.UseShellExecute = True
            Process.Start(psi)
        Else
            MessageBox.Show($"未找到字体文件：{vbCrLf}{fontFile}",
                            "文件缺失", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ''' <summary>忽略提示，继续进入程序。</summary>
    Private Sub btnIgnore_Click(sender As Object, e As EventArgs) Handles btnIgnore.Click
        Me.DialogResult = DialogResult.Ignore
        Me.Close()
    End Sub

    ''' <summary>点击链接，打开更纱黑体 GitHub 发布页。</summary>
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Dim psi As New ProcessStartInfo()
            psi.FileName = "https://github.com/be5invis/Sarasa-Gothic/releases"
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}", "打开链接失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

End Class