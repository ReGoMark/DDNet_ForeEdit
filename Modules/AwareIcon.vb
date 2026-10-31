Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Resources

''' <summary>
''' DPI 图标切换辅助模块（PNG 双套资源 + 运行时缩放）。
''' 
''' 资源命名约定：{根命名空间}.Icons 下的条目为 {短名}_{16|20}
'''   · 100%  → 取 _16 资源，缩放到 14px
'''   · 125%  → 取 _16 资源，尺寸匹配不缩放
'''   · 150%+ → 取 _20 资源，尺寸匹配不缩放
''' 
''' 生成的 Image 由调用方持有，由窗体在 FormClosed 中统一释放（见 _ownedIcons）。
''' LoadIcon 始终返回新 Bitmap（精确匹配也复制），调用方负责 Dispose。
''' </summary>
Module AwareIcon

    ''' <summary>基准 DPI（100% 缩放）。</summary>
    Private Const BaseDpi As Single = 96.0F

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

    ''' <summary>
    ''' 按 DPI 返回图标目标像素：
    '''   100%  → 14
    '''   125%  → 16
    '''   150%+ → 20
    ''' </summary>
    Public Function GetIconSize(form As Form) As Integer
        Dim scale As Single = GetDpiScale(form)
        If scale >= 1.5F Then Return 20
        If scale >= 1.25F Then Return 16
        Return 16
    End Function

    ''' <summary>
    ''' 按 DPI 返回图标资源后缀：
    '''   100% / 125% → _16
    '''   150%+       → _20
    ''' </summary>
    Public Function GetIconSuffix(form As Form) As String
        Return If(GetDpiScale(form) >= 1.5F, "_20", "_16")
    End Function

#End Region

#Region "── 缩放 ──"

    ''' <summary>
    ''' 高质量缩放到指定像素。
    ''' 返回新 Bitmap，调用方负责 Dispose。
    ''' </summary>
    Public Function ResizeIcon(src As Image, targetPx As Integer) As Bitmap
        If src Is Nothing OrElse targetPx <= 0 Then Return Nothing
        If src.Width = targetPx AndAlso src.Height = targetPx Then Return New Bitmap(src)

        Dim bmp As New Bitmap(targetPx, targetPx, PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            g.SmoothingMode = SmoothingMode.HighQuality
            g.CompositingQuality = CompositingQuality.HighQuality
            g.Clear(Color.Transparent)
            g.DrawImage(src, New Rectangle(0, 0, targetPx, targetPx))
        End Using
        Return bmp
    End Function

#End Region

#Region "── 资源加载与批量应用 ──"

    ''' <summary>
    ''' 从资源取图标，按 targetPx 决定是否缩放。
    ''' 资源条目名 = shortName &amp; suffix。
    ''' 返回新 Bitmap（精确匹配也复制），调用方负责 Dispose。
    ''' 资源缺失时返回 Nothing，不抛异常。
    ''' </summary>
    ''' <param name="targetPx">
    ''' 目标像素；&lt;= 0 表示不缩放（返回资源原尺寸，用于菜单图标）。
    ''' &gt; 0 且与资源尺寸不一致时用 HighQualityBicubic 缩放。
    ''' </param>
    Public Function LoadIcon(rm As ResourceManager,
                             shortName As String,
                             suffix As String,
                             Optional targetPx As Integer = 0) As Image
        If rm Is Nothing OrElse String.IsNullOrEmpty(shortName) Then Return Nothing

        Try
            Dim src = TryCast(rm.GetObject($"{shortName}{suffix}"), Image)
            If src Is Nothing Then Return Nothing

            ' 需要缩放
            If targetPx > 0 AndAlso (src.Width <> targetPx OrElse src.Height <> targetPx) Then
                Return ResizeIcon(src, targetPx)
            End If

            ' 尺寸匹配（或 targetPx <= 0）：复制一份，避免调用方 Dispose 掉 resx 共享资源
            Return New Bitmap(src)
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
        Dim targetPx As Integer = GetIconSize(form)
        For Each kvp In map
            Dim img = LoadIcon(rm, kvp.Value, suffix, targetPx)
            If img IsNot Nothing Then
                kvp.Key.Image = img
                owned.Add(img)
            End If
        Next
    End Sub

#End Region

End Module