Imports System.Drawing.Text

''' <summary>
''' 单个子字体的元数据，由 FontInfoReader.ReadAllFontNames 填充。
''' 字重、字宽和版本号已提供本地化或格式化属性。
''' </summary>
Public Class FontInfoTable

    ''' <summary>Name ID=1 所有语言版本，用于和 GDI+ FontFamily.Name 匹配。</summary>
    Public Property AllRawNames As New List(Of String)

    ''' <summary>index.json 使用的字体族名称（Name ID=16 优先，回退 Name ID=1）。</summary>
    Public Property FamilyName As String = ""

    ''' <summary>字重值（OS/2.usWeightClass，100-900）；0 表示读取失败。</summary>
    Public Property WeightClass As Integer = 0

    ''' <summary>字宽值（OS/2.usWidthClass，1-9）；0 表示读取失败。</summary>
    Public Property WidthClass As Integer = 0

    ''' <summary>版本字符串原始值（Name ID=5），如 "Version 2.003"；未读到则为空。</summary>
    Public Property Version As String = ""

    ''' <summary>字体是否为斜体（来自 OS/2.fsSelection bit 0）。</summary>
    Public Property IsItalic As Boolean = False

    ''' <summary>
    ''' 精简版本号：去掉 "Version " 前缀并截断分号后的内容。
    ''' 例："Version 2.003;PS 001" → "2.003"；未读到则返回空字符串。
    ''' </summary>
    Public ReadOnly Property VersionShort As String
        Get
            If String.IsNullOrEmpty(Version) Then Return ""
            Dim v = Version.Replace("Version ", "").Trim()
            Dim semi = v.IndexOf(";"c)
            Return If(semi > 0, v.Substring(0, semi).Trim(), v)
        End Get
    End Property

    ''' <summary>
    ''' 根据 WeightClass 和 IsItalic 推导 GDI+ FontStyle。
    ''' Bold 阈值：WeightClass >= 600。
    ''' </summary>
    Public ReadOnly Property GdiFontStyle As FontStyle
        Get
            Dim style As FontStyle = FontStyle.Regular
            If WeightClass >= 600 Then style = style Or FontStyle.Bold
            If IsItalic Then style = style Or FontStyle.Italic
            Return style
        End Get
    End Property

    ''' <summary>
    ''' 尝试用 AllRawNames 中的 GDI 族名创建 Font 对象。
    ''' GDI+ 只认 Name ID=1 的家族名；此方法逐一尝试直到成功。
    ''' 调用方负责 Dispose 返回的 Font。
    ''' </summary>
    Public Function TryCreateFont(size As Single) As Font
        Dim style As FontStyle = GdiFontStyle
        For Each name In AllRawNames
            Try
                Dim installed As New InstalledFontCollection()
                Dim found = installed.Families.Any(
                    Function(f) String.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase))
                If found Then
                    Dim ff As New FontFamily(name)
                    If Not ff.IsStyleAvailable(style) Then style = FontStyle.Regular
                    Return New Font(ff, size, style)
                End If
            Catch
            End Try
        Next
        ' 兜底：用排版族名（适用于已加载到 PrivateFontCollection 的情况）
        Try
            Return New Font(FamilyName, size, style)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>字重的中文名称。</summary>
    Public ReadOnly Property WeightName As String
        Get
            Select Case WeightClass
                Case 100 : Return "特细"
                Case 200 : Return "极细"
                Case 300 : Return "细"
                Case 400 : Return "常规"
                Case 500 : Return "中等"
                Case 600 : Return "半粗"
                Case 700 : Return "粗"
                Case 800 : Return "极粗"
                Case 900 : Return "特粗"
                Case 0 : Return ""
                Case Else : Return WeightClass.ToString()
            End Select
        End Get
    End Property

    ''' <summary>字宽的中文名称。</summary>
    Public ReadOnly Property WidthName As String
        Get
            Select Case WidthClass
                Case 1 : Return "超窄"
                Case 2 : Return "极窄"
                Case 3 : Return "窄"
                Case 4 : Return "半窄"
                Case 5 : Return "正常"
                Case 6 : Return "半宽"
                Case 7 : Return "宽"
                Case 8 : Return "极宽"
                Case 9 : Return "超宽"
                Case Else : Return ""
            End Select
        End Get
    End Property

End Class