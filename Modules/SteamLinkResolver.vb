Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.Win32

Public Class SteamLinkResolver

    ''' <summary>
    ''' 提供通过 Steam .url 文件解析游戏安装目录的静态方法。
    ''' </summary>

    ' ─── 公开接口 ────────────────────────────────────────────────────────

    ''' <summary>
    ''' 通过 Steam 快捷方式 (.url) 获取游戏的物理安装目录。
    ''' </summary>
    ''' <param name="urlFilePath">.url 文件的完整路径</param>
    ''' <returns>游戏安装目录完整路径；未找到则返回空字符串</returns>
    Public Shared Function GetInstallDirectory(urlFilePath As String) As String
            Try
                If Not File.Exists(urlFilePath) OrElse
                   Not Path.GetExtension(urlFilePath).Equals(".url", StringComparison.OrdinalIgnoreCase) Then
                    Return String.Empty
                End If

                Dim appId As String = ParseAppId(urlFilePath)
                If String.IsNullOrEmpty(appId) Then Return String.Empty

                Dim steamRoot As String = GetSteamRoot()
                If String.IsNullOrEmpty(steamRoot) Then Return String.Empty

                For Each libPath As String In GetSteamLibraries(steamRoot)
                    Dim manifestFile As String = Path.Combine(libPath, "steamapps", $"appmanifest_{appId}.acf")
                    If Not File.Exists(manifestFile) Then Continue For

                    Dim installDirName As String = ParseInstallDirName(manifestFile)
                    If String.IsNullOrEmpty(installDirName) Then Continue For

                    Dim finalPath As String = Path.Combine(libPath, "steamapps", "common", installDirName)
                    If Directory.Exists(finalPath) Then Return finalPath
                Next
            Catch ex As Exception
                ' 忽略异常
            End Try
            Return String.Empty
        End Function

        ' ─── 内部解析 ────────────────────────────────────────────────────────

        ''' <summary>从 .url 文件内容中提取 Steam AppID。</summary>
        Private Shared Function ParseAppId(filePath As String) As String
            Dim content As String = File.ReadAllText(filePath)
            Dim m As Match = Regex.Match(content, "rungameid/(\d+)", RegexOptions.IgnoreCase)
            Return If(m.Success, m.Groups(1).Value, String.Empty)
        End Function

        ''' <summary>从 Steam .acf 清单文件中提取 installdir 字段。</summary>
        Private Shared Function ParseInstallDirName(acfPath As String) As String
            Dim content As String = File.ReadAllText(acfPath)
            Dim m As Match = Regex.Match(content, """installdir""\s+""([^""]+)""", RegexOptions.IgnoreCase)
            Return If(m.Success, m.Groups(1).Value, String.Empty)
        End Function

        ''' <summary>返回所有 Steam 库路径（主库 + 扩展库）。</summary>
        Private Shared Function GetSteamLibraries(steamRoot As String) As List(Of String)
            Dim paths As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {steamRoot}
            Dim vdfPath As String = Path.Combine(steamRoot, "steamapps", "libraryfolders.vdf")
            If File.Exists(vdfPath) Then
                Dim content As String = File.ReadAllText(vdfPath)
                For Each m As Match In Regex.Matches(content, """path""\s+""([^""]+)""")
                    paths.Add(m.Groups(1).Value.Replace("\\", "\"))
                Next
            End If
            Return New List(Of String)(paths)
        End Function

        ''' <summary>从注册表读取 Steam 安装根目录。</summary>
        Private Shared Function GetSteamRoot() As String
            Dim path As String = Registry.GetValue("HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", "")?.ToString()
            Return If(Not String.IsNullOrEmpty(path), path.Replace("/", "\"), String.Empty)
        End Function

End Class