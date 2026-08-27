Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Public Class Win32ContextMenu
    Implements IDisposable

#Region "Win32"

    <DllImport("user32.dll")>
    Private Shared Function CreatePopupMenu() As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function AppendMenu(hMenu As IntPtr, uFlags As UInteger, uIDNewItem As UIntPtr, lpNewItem As String) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function TrackPopupMenuEx(hMenu As IntPtr, uFlags As UInteger, x As Integer, y As Integer, hwnd As IntPtr, lptpm As IntPtr) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function DestroyMenu(hMenu As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function SetMenuItemInfo(hMenu As IntPtr, uItem As UInteger, fByPosition As Boolean, ByRef lpmii As MENUITEMINFO) As Boolean
    End Function

    <DllImport("gdi32.dll")>
    Private Shared Function DeleteObject(hObject As IntPtr) As Boolean
    End Function

    <DllImport("gdi32.dll")>
    Private Shared Function CreateDIBSection(hdc As IntPtr, ByRef pbmi As BITMAPINFO, usage As UInteger, ByRef ppvBits As IntPtr, hSection As IntPtr, offset As UInteger) As IntPtr
    End Function

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Private Structure MENUITEMINFO
        Public cbSize As UInteger
        Public fMask As UInteger
        Public fType As UInteger
        Public fState As UInteger
        Public wID As UInteger
        Public hSubMenu As IntPtr
        Public hbmpChecked As IntPtr
        Public hbmpUnchecked As IntPtr
        Public dwItemData As IntPtr
        Public dwTypeData As String
        Public cch As UInteger
        Public hbmpItem As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure BITMAPINFOHEADER
        Public biSize As UInteger
        Public biWidth As Integer
        Public biHeight As Integer
        Public biPlanes As UShort
        Public biBitCount As UShort
        Public biCompression As UInteger
        Public biSizeImage As UInteger
        Public biXPelsPerMeter As Integer
        Public biYPelsPerMeter As Integer
        Public biClrUsed As UInteger
        Public biClrImportant As UInteger
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure BITMAPINFO
        Public bmiHeader As BITMAPINFOHEADER
        Public bmiColors As UInteger
    End Structure

    Private Const MF_STRING As UInteger = &H0
    Private Const MF_SEPARATOR As UInteger = &H800
    Private Const MF_GRAYED As UInteger = &H1
    Private Const MF_POPUP As UInteger = &H10
    Private Const TPM_RETURNCMD As UInteger = &H100
    Private Const TPM_LEFTBUTTON As UInteger = &H0
    Private Const TPM_RIGHTBUTTON As UInteger = &H2
    Private Const TPM_LEFTALIGN As UInteger = &H0
    Private Const TPM_RIGHTALIGN As UInteger = &H8
    Private Const TPM_TOPALIGN As UInteger = &H0
    Private Const TPM_BOTTOMALIGN As UInteger = &H20
    Private Const MIIM_BITMAP As UInteger = &H80
    Private Const MIIM_SUBMENU As UInteger = &H4
    Private Const BI_RGB As UInteger = 0
    Private Const DIB_RGB_COLORS As UInteger = 0

#End Region

#Region "MenuItem"

    Public Class MenuItem
        Public Property Id As Integer
        Public Property Text As String
        Public Property Icon As Bitmap
        Public Property Enabled As Boolean = True
        Public Property IsSeparator As Boolean = False
        Public Property OnClick As Action
        Public Property SubItems As List(Of MenuItem)

        Public Shared ReadOnly Property Separator As MenuItem
            Get
                Return New MenuItem() With {.IsSeparator = True}
            End Get
        End Property

    End Class

#End Region

    Private _items As New List(Of MenuItem)
    Private _iconCache As New Dictionary(Of Integer, IntPtr)
    Private _subMenuHandles As New List(Of IntPtr)

    Public Sub Add(item As MenuItem)
        _items.Add(item)
    End Sub

    Public Sub AddRange(items As IEnumerable(Of MenuItem))
        _items.AddRange(items)
    End Sub

    Public Sub Clear()
        FlushIconCache()
        _items.Clear()
    End Sub

    ''' <summary>鼠标位置弹出（右键触发用）</summary>
    Public Function Show(owner As Control, mouseLocation As Point) As MenuItem
        Dim screenPos As Point = owner.PointToScreen(mouseLocation)
        Return ShowAt(owner, screenPos, TPM_RETURNCMD Or TPM_RIGHTBUTTON)
    End Function

    ''' <summary>指定方向弹出，button 控制左键/右键触发</summary>
    Public Function Show(owner As Control, location As Point, direction As ToolStripDropDownDirection, Optional button As MouseButtons = MouseButtons.Left) As MenuItem
        Dim screenPos As Point = owner.PointToScreen(location)
        Dim flags As UInteger = TPM_RETURNCMD
        If button = MouseButtons.Right Then flags = flags Or TPM_RIGHTBUTTON

        Select Case direction
            Case ToolStripDropDownDirection.AboveLeft : flags = flags Or TPM_RIGHTALIGN Or TPM_BOTTOMALIGN
            Case ToolStripDropDownDirection.AboveRight : flags = flags Or TPM_LEFTALIGN Or TPM_BOTTOMALIGN
            Case ToolStripDropDownDirection.BelowLeft : flags = flags Or TPM_RIGHTALIGN Or TPM_TOPALIGN
            Case ToolStripDropDownDirection.BelowRight : flags = flags Or TPM_LEFTALIGN Or TPM_TOPALIGN
        End Select

        Return ShowAt(owner, screenPos, flags)
    End Function

    Private Function ShowAt(owner As Control, screenPos As Point, flags As UInteger) As MenuItem
        Dim hMenu As IntPtr = CreatePopupMenu()
        If hMenu = IntPtr.Zero Then Return Nothing

        Try
            _subMenuHandles.Clear()
            Dim posIndex As UInteger = 0
            For Each item In _items
                BuildMenuRecursive(hMenu, item, posIndex)
                posIndex += 1
            Next

            Dim cmd As Integer = TrackPopupMenuEx(
                hMenu, flags,
                screenPos.X, screenPos.Y,
                owner.FindForm().Handle,
                IntPtr.Zero)

            Dim result = FindMenuItemById(_items, cmd)
            result?.OnClick?.Invoke()
            Return result
        Finally
            DestroyMenu(hMenu)
            DestroySubMenus()
        End Try
    End Function

    Private Sub BuildMenuRecursive(hMenu As IntPtr, item As MenuItem, posIndex As UInteger)
        If item.IsSeparator Then
            AppendMenu(hMenu, MF_SEPARATOR, UIntPtr.Zero, Nothing)
        Else
            Dim f As UInteger = MF_STRING
            If Not item.Enabled Then f = f Or MF_GRAYED

            ' 如果有子项，创建子菜单
            If item.SubItems IsNot Nothing AndAlso item.SubItems.Count > 0 Then
                Dim hSubMenu As IntPtr = CreatePopupMenu()
                If hSubMenu <> IntPtr.Zero Then
                    _subMenuHandles.Add(hSubMenu)

                    Dim subPosIndex As UInteger = 0
                    For Each subItem In item.SubItems
                        BuildMenuRecursive(hSubMenu, subItem, subPosIndex)
                        subPosIndex += 1
                    Next

                    ' 添加带子菜单的菜单项
                    AppendMenu(hMenu, f Or MF_POPUP, New UIntPtr(CUInt(hSubMenu)), item.Text)

                    ' 如果有图标，附加图标
                    If item.Icon IsNot Nothing Then
                        AttachIcon(hMenu, posIndex, item.Id, item.Icon)
                    End If
                End If
            Else
                ' 添加普通菜单项
                AppendMenu(hMenu, f, New UIntPtr(CUInt(item.Id)), item.Text)

                ' 如果有图标，附加图标
                If item.Icon IsNot Nothing Then
                    AttachIcon(hMenu, posIndex, item.Id, item.Icon)
                End If
            End If
        End If
    End Sub

    Private Function FindMenuItemById(items As List(Of MenuItem), id As Integer) As MenuItem
        For Each item In items
            If item.Id = id Then Return item
            If item.SubItems IsNot Nothing AndAlso item.SubItems.Count > 0 Then
                Dim result = FindMenuItemById(item.SubItems, id)
                If result IsNot Nothing Then Return result
            End If
        Next
        Return Nothing
    End Function

    Private Sub AttachIcon(hMenu As IntPtr, posIndex As UInteger, id As Integer, icon As Bitmap)
        If Not _iconCache.ContainsKey(id) Then
            _iconCache(id) = ToPremultipliedHBitmap(icon)
        End If

        Dim mii As New MENUITEMINFO()
        mii.cbSize = CUInt(Marshal.SizeOf(mii))
        mii.fMask = MIIM_BITMAP
        mii.hbmpItem = _iconCache(id)
        SetMenuItemInfo(hMenu, posIndex, True, mii)
    End Sub

    Private Shared Function ToPremultipliedHBitmap(src As Bitmap) As IntPtr
        Dim bmp As New Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.Clear(Color.Transparent)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.DrawImage(src, 0, 0, src.Width, src.Height)
        End Using

        Dim rect As New Rectangle(0, 0, bmp.Width, bmp.Height)
        Dim bd As BitmapData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb)
        Dim bytes(bd.Stride * bd.Height - 1) As Byte
        Marshal.Copy(bd.Scan0, bytes, 0, bytes.Length)
        bmp.UnlockBits(bd)

        For i As Integer = 0 To bytes.Length - 1 Step 4
            Dim a As Byte = bytes(i + 3)
            Dim r As Byte = CByte(CInt(bytes(i + 2)) * a \ 255)
            Dim gC As Byte = CByte(CInt(bytes(i + 1)) * a \ 255)
            Dim b As Byte = CByte(CInt(bytes(i)) * a \ 255)
            bytes(i) = b
            bytes(i + 1) = gC
            bytes(i + 2) = r
            bytes(i + 3) = a
        Next

        Dim bmi As New BITMAPINFO()
        bmi.bmiHeader.biSize = CUInt(Marshal.SizeOf(bmi.bmiHeader))
        bmi.bmiHeader.biWidth = bmp.Width
        bmi.bmiHeader.biHeight = -bmp.Height
        bmi.bmiHeader.biPlanes = 1
        bmi.bmiHeader.biBitCount = 32
        bmi.bmiHeader.biCompression = BI_RGB

        Dim pBits As IntPtr
        Dim hBmp As IntPtr = CreateDIBSection(IntPtr.Zero, bmi, DIB_RGB_COLORS, pBits, IntPtr.Zero, 0)
        If hBmp = IntPtr.Zero OrElse pBits = IntPtr.Zero Then Return IntPtr.Zero

        Marshal.Copy(bytes, 0, pBits, bytes.Length)
        Return hBmp
    End Function

    Private Sub FlushIconCache()
        For Each hBmp In _iconCache.Values
            If hBmp <> IntPtr.Zero Then DeleteObject(hBmp)
        Next
        _iconCache.Clear()
    End Sub

    Private Sub DestroySubMenus()
        For Each hSubMenu In _subMenuHandles
            If hSubMenu <> IntPtr.Zero Then DestroyMenu(hSubMenu)
        Next
        _subMenuHandles.Clear()
    End Sub

#Region "IDisposable"

    Private _disposed As Boolean

    Public Sub Dispose() Implements IDisposable.Dispose
        If Not _disposed Then
            FlushIconCache()
            DestroySubMenus()
            _disposed = True
        End If
    End Sub

#End Region

End Class
