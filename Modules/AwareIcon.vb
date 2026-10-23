Imports System.Resources

''' <summary>
''' DPI 图标切换辅助模块。
''' 命名约定：{根命名空间}.Icons 下的资源条目为 icon_{短名}_{16|20}
''' 选择策略：DPI &lt; 150% 用 _16，&gt;= 150% 用 _20。
''' 生成的 Image 由调用方持有，由窗体在 FormClosed 中统一释放（见 _ownedIcons）。
''' </summary>
Module AwareIcon

    ''' <summary>基准 DPI（100% 缩放）。</summary>
    Private Const BaseDpi As Single = 96.0F

    ''' <summary>图标基准尺寸（100% 时按钮内应显示的像素数）。</summary>
    Public Const BaseIconSize As Integer = 16

#Region "── DPI 与档位 ──"

    ''' <summary>
    ''' 取当前窗体的 DPI 缩放比。
    ''' 优先使用 DeviceDpi（.NET 4.7+）；失败时退回 CreateGraphics。
    ''' </summary>
    Public Function GetDpiScale(form As Form) As Single
        If form Is Nothing Then Return 1.0F
        Try
            Dim devDpi As Integer = form.DeviceDpi
            If devDpi > 0 Then Return devDpi / BaseDpi
        Catch
        End Try
        Using g As Graphics = form.CreateGraphics()
            Return g.DpiX / BaseDpi
        End Using
    End Function

    ''' <summary>按 DPI 返回图标资源后缀：_16 或 _20。</summary>
    Public Function GetIconSuffix(form As Form) As String
        Return If(GetDpiScale(form) >= 1.5F, "_20", "_16")
    End Function

#End Region

#Region "── 资源加载与批量应用 ──"

    ''' <summary>
    ''' 从资源取图标。资源条目不存在时返回 Nothing（不抛异常）。
    ''' 资源条目名 = "icon_" &amp; shortName &amp; suffix。
    ''' </summary>
    Public Function LoadIcon(rm As ResourceManager, shortName As String, suffix As String) As Image
        If rm Is Nothing OrElse String.IsNullOrEmpty(shortName) Then Return Nothing
        Try
            Return TryCast(rm.GetObject($"{shortName}{suffix}"), Image)
        Catch ex As MissingManifestResourceException
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 批量应用到按钮（含 Button / CheckBox / RadioButton，只要继承 ButtonBase 均可）。
    ''' map: 控件 → 短名（如 btnPreview → "preview"）。
    ''' 生成的 Image 追加进 owned，由调用方在窗体关闭时统一释放。
    ''' </summary>
    Public Sub ApplyButtonIcons(Of T As ButtonBase)(form As Form,
                                                    rm As ResourceManager,
                                                    map As IDictionary(Of T, String),
                                                    owned As IList(Of Image))
        If form Is Nothing OrElse rm Is Nothing OrElse map Is Nothing Then Return
        Dim suffix As String = GetIconSuffix(form)
        For Each kvp In map
            Dim img = LoadIcon(rm, kvp.Value, suffix)
            If img IsNot Nothing Then
                kvp.Key.Image = img
                owned.Add(img)
            End If
        Next
    End Sub

#End Region

End Module