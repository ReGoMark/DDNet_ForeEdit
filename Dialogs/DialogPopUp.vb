Imports System.ComponentModel
Imports System.Drawing.Text

Public Class DialogPopUp

    ' ── 公开属性（由调用方设置）────────────────────────────────────

    ''' <summary>标题标签文本（lblTitle）</summary>
    <DefaultValue("")>
    Public Property TitleText As String
        Get
            Return lblTitle.Text
        End Get
        Set(value As String)
            lblTitle.Text = value
        End Set
    End Property

    ''' <summary>说明文本（lblNote，分隔线下方）；为空时自动隐藏分隔线和 lblNote</summary>
    <DefaultValue("")>
    Public Property DescriptionText As String
        Get
            Return lblNote.Text
        End Get
        Set(value As String)
            lblNote.Text = value
            Dim hasDesc As Boolean = Not String.IsNullOrWhiteSpace(value)
            splitNote.Visible = hasDesc
            lblNote.Visible = hasDesc
        End Set
    End Property

    ''' <summary>确定按钮文本，默认"确定(&amp;O)"</summary>
    <DefaultValue("确定")>
    Public Property ConfirmText As String
        Get
            Return btnOK.Text
        End Get
        Set(value As String)
            btnOK.Text = value
        End Set
    End Property

    ''' <summary>取消按钮文本，默认"取消(&amp;C)"</summary>
    <DefaultValue("取消")>
    Public Property CancelText As String
        Get
            Return btnCancel.Text
        End Get
        Set(value As String)
            btnCancel.Text = value
        End Set
    End Property

    ''' <summary>列表项目集合（只读展示，不支持选中）</summary>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public ReadOnly Property Items As ListBox.ObjectCollection
        Get
            Return lstbPopup.Items
        End Get
    End Property

    ' ── 列表自绘（与 lstFallback_DrawItem 完全一致的风格）────────
    Private Sub lstbFonts_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstbPopup.DrawItem
        If e.Index < 0 OrElse e.Index >= lstbPopup.Items.Count Then Return
        e.DrawBackground()

        Dim itemText As String = lstbPopup.Items(e.Index).ToString()
        Dim textColor As Color = If((e.State And DrawItemState.Selected) <> 0,
                                SystemColors.HighlightText, SystemColors.WindowText)

        Dim textRect As New Rectangle(e.Bounds.X + 2, e.Bounds.Y,
                                  e.Bounds.Width - 8, e.Bounds.Height)
        TextRenderer.DrawText(e.Graphics, itemText, e.Font, textRect, textColor,
                          TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or
                          TextFormatFlags.NoPrefix)

        ' 右侧序号
        Dim tag As String = $"{e.Index + 1}"
        Using tagFont As New Font("Consolas", 7.5F, FontStyle.Regular, GraphicsUnit.Point)
            Using tagBrush As New SolidBrush(
                If((e.State And DrawItemState.Selected) <> 0,
                   Color.FromArgb(200, 255, 255, 255), Color.Gray))
                Dim tagSize As SizeF = e.Graphics.MeasureString(tag, tagFont)
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
                e.Graphics.DrawString(tag, tagFont, tagBrush,
                New PointF(e.Bounds.Right - tagSize.Width - 4,
                           e.Bounds.Y + (e.Bounds.Height - tagSize.Height) / 2 + 1))
            End Using
        End Using

        e.DrawFocusRectangle()
    End Sub

    ' ── 按钮事件 ──────────────────────────────────────────────────

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub DialogPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lstbPopup.ItemHeight = FormMain.SharedItemHeight
    End Sub

End Class