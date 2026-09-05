Imports System.IO
Imports System.Text

Module FontInfoReader
    ' ─── 语言代码 → 中文名称映射 ─────────────────────────────────────────

    Private ReadOnly LanguageNameMap As New Dictionary(Of String, String) From {
            {"LA", "西文"},
            {"JP", "日文"},
            {"KR", "韩文"},
            {"CN", "中文"}
        }

    ' ─── 公开接口 ────────────────────────────────────────────────────────

    ''' <summary>
    ''' 读取字体文件中所有子字体的名称信息。
    ''' TTF/OTF 返回 1 个元素；TTC 返回 N 个（每个子字体一个）。
    ''' </summary>
    ''' <param name="filePath">字体文件路径</param>
    ''' <returns>字体名称信息列表</returns>
    Public Function ReadAllFontNames(filePath As String) As List(Of FontInfoTable)
        Dim result As New List(Of FontInfoTable)
        Try
            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)
                Using br As New BinaryReader(fs)
                    If Path.GetExtension(filePath).ToLower() = ".ttc" Then
                        ' TTC 格式：读取头部偏移表
                        If New String(br.ReadChars(4)) <> "ttcf" Then Return result
                        ReadUInt32BE(br) ' version
                        Dim fontCount As Integer = CInt(ReadUInt32BE(br))
                        Dim offsets(fontCount - 1) As UInteger
                        For i = 0 To fontCount - 1
                            offsets(i) = ReadUInt32BE(br)
                        Next
                        For Each off In offsets
                            result.Add(ReadNameInfoAt(br, CLng(off)))
                        Next
                    Else
                        ' TTF / OTF
                        result.Add(ReadNameInfoAt(br, 0))
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' 忽略读取错误
        End Try
        Return result
    End Function

    ''' <summary>
    ''' 通过 cmap 表检测字体支持的语言覆盖范围，返回中文本地化名称。
    ''' </summary>
    ''' <param name="filePath">字体文件路径</param>
    ''' <param name="fontIndex">TTC 子字体索引（TTF/OTF 忽略此参数）</param>
    ''' <returns>逗号分隔的中文语言名称，如 "西文, 日文, 中文"；无支持则返回 "-"</returns>
    Public Function ReadLanguageCoverage(filePath As String,
                                         Optional fontIndex As Integer = 0) As String
        Try
            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)
                Using br As New BinaryReader(fs)
                    Dim offset As Long = 0

                    If Path.GetExtension(filePath).ToLower() = ".ttc" Then
                        If New String(br.ReadChars(4)) <> "ttcf" Then Return "-"
                        ReadUInt32BE(br) ' version
                        Dim fontCount As Integer = CInt(ReadUInt32BE(br))
                        Dim offsets(fontCount - 1) As UInteger
                        For i = 0 To fontCount - 1
                            offsets(i) = ReadUInt32BE(br)
                        Next
                        If fontIndex >= fontCount Then Return "-"
                        offset = CLng(offsets(fontIndex))
                    End If

                    ' 获取原始语言代码并本地化
                    Dim rawCoverage = CheckCoverage(br, offset)
                    Return LocalizeLanguageCoverage(rawCoverage)

                End Using
            End Using
        Catch ex As Exception
            Return "-"
        End Try
    End Function

    ' ─── 语言代码本地化 ──────────────────────────────────────────────────

    ''' <summary>
    ''' 将语言代码字符串（如 "LA, JP, CN"）转换为中文名称。
    ''' </summary>
    Private Function LocalizeLanguageCoverage(coverage As String) As String
        If String.IsNullOrEmpty(coverage) OrElse coverage = "-" Then
            Return "-"
        End If

        Dim parts = coverage.Split(","c).Select(Function(s) s.Trim())
        Dim localized = New List(Of String)

        For Each code In parts
            Dim name As String = ""
            If LanguageNameMap.TryGetValue(code.ToUpper(), name) Then
                localized.Add(name)
            Else
                localized.Add(code)  ' 未知代码原样保留
            End If
        Next

        Return String.Join(", ", localized)
    End Function

    ' ─── 内部：表目录解析 ───────────────────────────────────────────────

    ''' <summary>从指定偏移处读取单个子字体的 name / OS/2 表信息。</summary>
    Private Function ReadNameInfoAt(br As BinaryReader, tableOffset As Long) As FontInfoTable
        Dim info As New FontInfoTable()
        Try
            br.BaseStream.Seek(tableOffset, SeekOrigin.Begin)
            ReadUInt32BE(br) ' sfnt version
            Dim numTables As UShort = ReadUInt16BE(br)
            ReadUInt16BE(br) : ReadUInt16BE(br) : ReadUInt16BE(br) ' searchRange, entrySelector, rangeShift

            Dim nameOff As UInteger = 0
            Dim os2Off As UInteger = 0

            For i = 0 To numTables - 1
                Dim tag As String = New String(br.ReadChars(4))
                ReadUInt32BE(br) ' checksum
                Dim off As UInteger = ReadUInt32BE(br)
                ReadUInt32BE(br) ' length
                Select Case tag
                    Case "name" : nameOff = off
                    Case "OS/2" : os2Off = off
                End Select
            Next

            If nameOff <> 0 Then ReadNameTable(br, nameOff, info)
            If os2Off <> 0 Then ReadOs2Table(br, os2Off, info)
        Catch ex As Exception
            ' 忽略错误
        End Try
        Return info
    End Function

    ''' <summary>解析 name 表，填充 FamilyName、AllRawNames 及 Version。</summary>
    Private Sub ReadNameTable(br As BinaryReader, nameTableOffset As UInteger, info As FontInfoTable)
        br.BaseStream.Seek(nameTableOffset, SeekOrigin.Begin)
        ReadUInt16BE(br) ' format
        Dim count As UShort = ReadUInt16BE(br)
        Dim stringOffset As UShort = ReadUInt16BE(br)
        Dim storageBase As Long = nameTableOffset + stringOffset

        Dim rawNames As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim typoWinEn As String = ""    ' Name ID=16, 英文
        Dim typoWinOther As String = "" ' Name ID=16, 其他语言
        Dim famWinEn As String = ""     ' Name ID=1, 英文
        Dim famWinOther As String = ""  ' Name ID=1, 其他语言
        Dim typoMac As String = ""      ' Mac 平台兜底
        Dim famMac As String = ""

        For i = 0 To count - 1
            Dim platformID As UShort = ReadUInt16BE(br)
            Dim encodingID As UShort = ReadUInt16BE(br)
            Dim languageID As UShort = ReadUInt16BE(br)
            Dim nameID As UShort = ReadUInt16BE(br)
            Dim length As UShort = ReadUInt16BE(br)
            Dim strOff As UShort = ReadUInt16BE(br)

            ' 读取 ID=1（族名）、ID=5（版本）、ID=16（排版族名）
            If nameID <> 1 AndAlso nameID <> 5 AndAlso nameID <> 16 Then Continue For

            Dim savedPos = br.BaseStream.Position
            br.BaseStream.Seek(storageBase + strOff, SeekOrigin.Begin)
            Dim bytes = br.ReadBytes(length)
            br.BaseStream.Seek(savedPos, SeekOrigin.Begin)

            If platformID = 3 Then
                ' Windows Unicode (UTF-16 BE)
                Dim decoded = Encoding.BigEndianUnicode.GetString(bytes).Trim(Chr(0))
                If String.IsNullOrEmpty(decoded) Then Continue For

                Dim isEnglish = (languageID = &H409 OrElse languageID = &H0)

                If nameID = 1 Then
                    rawNames.Add(decoded)
                    If isEnglish AndAlso String.IsNullOrEmpty(famWinEn) Then famWinEn = decoded
                    If Not isEnglish AndAlso String.IsNullOrEmpty(famWinOther) Then famWinOther = decoded
                ElseIf nameID = 16 Then
                    If isEnglish AndAlso String.IsNullOrEmpty(typoWinEn) Then typoWinEn = decoded
                    If Not isEnglish AndAlso String.IsNullOrEmpty(typoWinOther) Then typoWinOther = decoded
                ElseIf nameID = 5 Then
                    ' 版本号：优先取英文，已有则不覆盖
                    If isEnglish AndAlso String.IsNullOrEmpty(info.Version) Then
                        info.Version = decoded
                    End If
                End If

            ElseIf platformID = 1 Then
                ' Mac Latin-1（兜底）
                Dim decoded = Encoding.Latin1.GetString(bytes).Trim(Chr(0))
                If nameID = 1 AndAlso String.IsNullOrEmpty(famMac) Then famMac = decoded
                If nameID = 16 AndAlso String.IsNullOrEmpty(typoMac) Then typoMac = decoded
                ' Mac 版本只在 Windows 平台未读到时作为兜底
                If nameID = 5 AndAlso String.IsNullOrEmpty(info.Version) Then
                    info.Version = decoded
                End If
            End If
        Next

        info.AllRawNames.AddRange(rawNames)
        ' FamilyName 优先级：Name ID=16 英文 > 其他 > Mac，再回退 Name ID=1
        info.FamilyName = FirstNonEmpty(typoWinEn, typoWinOther, typoMac,
                                        famWinEn, famWinOther, famMac)
    End Sub

    ''' <summary>解析 OS/2 表，读取字重、字宽和斜体标志。</summary>
    Private Sub ReadOs2Table(br As BinaryReader, os2Offset As UInteger, info As FontInfoTable)
        Try
            br.BaseStream.Seek(os2Offset, SeekOrigin.Begin)
            ReadUInt16BE(br) ' version
            ReadUInt16BE(br) ' xAvgCharWidth
            info.WeightClass = CInt(ReadUInt16BE(br))  ' +4
            info.WidthClass = CInt(ReadUInt16BE(br))   ' +6
            ' fsSelection 位于 OS/2 表偏移 +62；bit 0 = ITALIC
            br.BaseStream.Seek(os2Offset + 62, SeekOrigin.Begin)
            Dim fsSelection As UShort = ReadUInt16BE(br)
            info.IsItalic = (fsSelection And &H1US) <> 0
        Catch ex As Exception
            ' 保持默认值
        End Try
    End Sub

    ''' <summary>返回第一个非空字符串，否则返回空。</summary>
    Private Function FirstNonEmpty(ParamArray candidates As String()) As String
        For Each s In candidates
            If Not String.IsNullOrEmpty(s) Then Return s
        Next
        Return ""
    End Function

    ' ─── 内部：cmap 语言覆盖检测 ────────────────────────────────────────

    ''' <summary>在字体表目录中定位 cmap 表并检测 Format 4 子表覆盖的语言码位。</summary>
    Private Function CheckCoverage(br As BinaryReader, tableOffset As Long) As String
        br.BaseStream.Seek(tableOffset, SeekOrigin.Begin)
        ReadUInt32BE(br) ' sfnt version
        Dim numTables As UShort = ReadUInt16BE(br)
        ReadUInt16BE(br) : ReadUInt16BE(br) : ReadUInt16BE(br)

        Dim cmapOffset As UInteger = 0
        For i = 0 To numTables - 1
            Dim tag As String = New String(br.ReadChars(4))
            ReadUInt32BE(br) ' checksum
            Dim off As UInteger = ReadUInt32BE(br)
            ReadUInt32BE(br) ' length
            If tag = "cmap" Then cmapOffset = off : Exit For
        Next
        If cmapOffset = 0 Then Return "-"

        ' 在 cmap 子表目录中找 Platform 3 Encoding 1（Windows Unicode BMP，Format 4）
        br.BaseStream.Seek(cmapOffset, SeekOrigin.Begin)
        ReadUInt16BE(br) ' version
        Dim numSubtables As UShort = ReadUInt16BE(br)

        Dim format4Offset As UInteger = 0
        For i = 0 To numSubtables - 1
            Dim pid As UShort = ReadUInt16BE(br)
            Dim eid As UShort = ReadUInt16BE(br)
            Dim subOff As UInteger = ReadUInt32BE(br)
            If pid = 3 AndAlso eid = 1 Then
                format4Offset = subOff
                Exit For
            End If
        Next
        If format4Offset = 0 Then Return "-"

        ' 解析 Format 4：读取 startCode / endCode 段
        br.BaseStream.Seek(cmapOffset + format4Offset, SeekOrigin.Begin)
        If ReadUInt16BE(br) <> 4 Then Return "-" ' format check
        ReadUInt16BE(br) ' length
        ReadUInt16BE(br) ' language
        Dim segCount As Integer = ReadUInt16BE(br) \ 2
        ReadUInt16BE(br) : ReadUInt16BE(br) : ReadUInt16BE(br) ' searchRange, entrySelector, rangeShift

        Dim endCodes(segCount - 1) As UShort
        For i = 0 To segCount - 1 : endCodes(i) = ReadUInt16BE(br) : Next
        ReadUInt16BE(br) ' reservedPad
        Dim startCodes(segCount - 1) As UShort
        For i = 0 To segCount - 1 : startCodes(i) = ReadUInt16BE(br) : Next

        ' 用代表码位检查各语言是否被覆盖
        ' LA = U+0041('A')  JP = U+3042('あ')  KR = U+AC00('가')  CN = U+4E2D('中')
        Dim checkPoints As (tag As String, code As Integer)() = {
            ("LA", &H41), ("JP", &H3042), ("KR", &HAC00), ("CN", &H4E2D)
        }

        Dim supported As New List(Of String)
        For Each pt In checkPoints
            For i = 0 To segCount - 1
                If pt.code >= startCodes(i) AndAlso pt.code <= endCodes(i) Then
                    supported.Add(pt.tag)
                    Exit For
                End If
            Next
        Next

        Return If(supported.Count = 0, "-", String.Join(", ", supported))
    End Function

    ' ─── 内部：大端字节序读取 ───────────────────────────────────────────

    ''' <summary>以大端字节序读取 2 字节无符号整数。</summary>
    Private Function ReadUInt16BE(br As BinaryReader) As UShort
        Dim b0 = br.ReadByte()
        Dim b1 = br.ReadByte()
        Return CUShort((CUShort(b0) << 8) Or b1)
    End Function

    ''' <summary>以大端字节序读取 4 字节无符号整数。</summary>
    Private Function ReadUInt32BE(br As BinaryReader) As UInteger
        Dim b0 = br.ReadByte()
        Dim b1 = br.ReadByte()
        Dim b2 = br.ReadByte()
        Dim b3 = br.ReadByte()
        Return (CUInt(b0) << 24) Or (CUInt(b1) << 16) Or (CUInt(b2) << 8) Or b3
    End Function

    ' ─── 内部数据类 ──────────────────────────────────────────────────────
End Module