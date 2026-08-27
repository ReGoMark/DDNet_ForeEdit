Imports System.Windows.Forms
Module AwareListHeight
    ''' <summary>
    ''' DPI 缩放辅助类，用于统一计算和设置控件的 DPI 相关属性。
    ''' </summary>

    ''' <summary>基准 DPI（100% 缩放）。</summary>
    Private Const BaseDpi As Single = 96.0F

    ''' <summary>基准行高（100% 缩放时的推荐值）。</summary>
    Private Const BaseItemHeight As Integer = 20

    ''' <summary>获取当前窗体的 DPI 缩放比例。</summary>
    Public Function GetDpiScale(form As Form) As Single
        Using g As Graphics = form.CreateGraphics()
            Return g.DpiX / BaseDpi
        End Using
    End Function

    ''' <summary>根据当前 DPI 计算合适的行高。</summary>
    Public Function GetScaledItemHeight(form As Form) As Integer
        Dim scale = GetDpiScale(form)
        Return CInt(Math.Round(BaseItemHeight * scale))
    End Function

    ''' <summary>为 ListBox 设置适配 DPI 缩放的行高。</summary>
    Public Sub ApplyScaledItemHeight(listBox As ListBox)
        Dim form = listBox.FindForm()
        If form IsNot Nothing Then
            listBox.ItemHeight = GetScaledItemHeight(form)
        End If
    End Sub

    ''' <summary>为多个 ListBox 设置适配 DPI 缩放的行高。</summary>
    Public Sub ApplyScaledItemHeight(listBoxes As IEnumerable(Of ListBox))
        For Each lb In listBoxes
            ApplyScaledItemHeight(lb)
        Next
    End Sub
End Module
