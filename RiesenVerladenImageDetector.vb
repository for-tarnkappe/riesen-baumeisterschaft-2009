Public Class frmRiesen
    Dim destinationBmp As New Drawing.Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height)
    Dim tempBitmap As New Bitmap(Me.Width, Me.Height)

    Dim BrettImage As Bitmap
    Dim BrettTol As Double = 99.58
    Dim BrettSize As New Size(180, 120)
    Dim PosFix(,) As Point = {{New Point(15, 1), New Point(22, 0)}, {New Point(19, 0), New Point(28, -1)}, {New Point(23, 0), New Point(29, -1)}, {New Point(19, 0), New Point(28, -1)}, {New Point(21, 0), New Point(30, -1)}}

    Dim NumImages(4, 1) As Bitmap

    Dim EmptyImage As Bitmap
    Dim WithoutImages(1) As Bitmap
    Dim WithImages(1) As Bitmap

    Dim AppPath As String
    Dim ImgPath As String

    Private Structure MySchub
        Dim Steine As Byte
        Dim Latten As Byte
        Dim Sand As Byte
        Dim Zement As Byte
    End Structure

    Dim MySchubKarr As MySchub

    'Private Enum ItemType
    '    Steine = 0
    '    Latten = 1
    '    Sand = 2
    '    Zement = 3
    'End Enum


    Dim LeftFix As Integer = 0 'Laptop -139
    Const StartLeftBase = 840
    Dim StartLeft = StartLeftBase
    Dim StartLeftCustom = StartLeft

    Dim TopFix As Integer
    Const StartTopFix = 2
    Const StartTopBase = 206
    Dim StartTop As Integer = StartTopBase
    Dim StartTopCustom As Integer = StartTop

    Const FirstRowLeft = 45
    Const FirstRowTop = 27

    Const SecondRowLeft = 48
    Const SecondRowTop = 49

    Const ThirdRowLeft = 49
    Const ThirdRowTop = 72

    Const FourthRowLeft = 51
    Const FourthRowTop = 94

    Dim LinePos() As Point = {New Point(FirstRowLeft, FirstRowTop), New Point(SecondRowLeft, SecondRowTop), New Point(ThirdRowLeft, ThirdRowTop), New Point(FourthRowLeft, FourthRowTop)}

    Private Sub frmDetect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Call DrawInfo(True)
    End Sub

    Private Sub frmDetect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AppPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) & System.IO.Path.DirectorySeparatorChar
        ImgPath = AppPath & "img" & System.IO.Path.DirectorySeparatorChar
        BrettImage = New Bitmap(ImgPath & "Brett-840_204.bmp")
        For I = 1 To 5
            NumImages(I - 1, 0) = New Bitmap(ImgPath & I & ".bmp")
            NumImages(I - 1, 1) = New Bitmap(ImgPath & I & "x.bmp")
        Next
        EmptyImage = New Bitmap(ImgPath & "0.bmp")

        cmbX.SelectedText = StartLeft
        cmbY.SelectedText = StartTop


        WithoutImages(0) = New Bitmap(ImgPath & "St.bmp")
        WithoutImages(1) = New Bitmap(ImgPath & "La.bmp")
        WithImages(0) = New Bitmap(ImgPath & "Sa.bmp")
        WithImages(1) = New Bitmap(ImgPath & "Ze.bmp")

        Me.BackColor = Color.Empty
        Call DrawInfo(True)
    End Sub

    Private Sub frmDetect_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        StartLeft = StartLeftCustom
        StartTop = StartTopCustom
        Call DrawInfo(True)
        Me.Invalidate()
    End Sub

    Private Sub frmDetect_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        'Call DrawInfo()
        e.Graphics.DrawImage(tempBitmap, 0, 0)
    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub 'OnPaintBackground

    Private Sub DrawInfo(ByVal SkipClick As Boolean)
        tempBitmap = New Bitmap(Me.Width, Me.Height)
        Dim tempGraphics As Graphics = Graphics.FromImage(tempBitmap)
        Dim g As Drawing.Graphics = Drawing.Graphics.FromImage(destinationBmp)
        g.CopyFromScreen(StartLeft, StartTop, 0, 0, BrettSize, CopyPixelOperation.SourceCopy)

        Dim DifferenceData As String
        Dim DifferenceID(3) As String
        Dim DifferencePercentage(3) As Double
        Dim TempPercentage As Double = 0
        Dim TestPercentage(3, 1) As Double
        Dim TempName As String

        MySchubKarr.Steine = 0
        MySchubKarr.Latten = 0
        MySchubKarr.Sand = 0
        MySchubKarr.Zement = 0

        With tempGraphics
            .DrawImageUnscaled(destinationBmp, 0, 0)
            .FillRectangle(New SolidBrush(Color.FromArgb(200, Color.White)), New Rectangle(0, 0, Me.Width, Me.Height))
            TempPercentage = ComparePictures(CopyImagePart(destinationBmp, New Rectangle(0, 0, BrettImage.Width, BrettImage.Height)), BrettImage)

            DifferenceData = "Brett: " & TempPercentage.ToString & vbNewLine & "__________________"

            If TempPercentage = 100 And SkipClick = False Then
                For J = 0 To 3
                    DifferenceID(J) = 0
                    TempName = ""
                    DifferenceData &= vbNewLine & "__________________"
                    For I = 1 To 5
                        TempPercentage = ComparePictures(CopyImagePart(destinationBmp, New Rectangle(LinePos(J).X, LinePos(J).Y, NumImages(I - 1, 0).Width, NumImages(I - 1, 0).Height)), NumImages(I - 1, 0))
                        TempPercentage += ComparePictures(CopyImagePart(destinationBmp, New Rectangle(LinePos(J).X, LinePos(J).Y, NumImages(I - 1, 0).Width, NumImages(I - 1, 0).Height)), NumImages(I - 1, 0), 1)

                        If DifferencePercentage(J) < TempPercentage Then
                            DifferencePercentage(J) = TempPercentage
                            DifferenceID(J) = I
                        End If
                        TempPercentage = ComparePictures(CopyImagePart(destinationBmp, New Rectangle(LinePos(J).X, LinePos(J).Y, NumImages(I - 1, 1).Width, NumImages(I - 1, 1).Height)), NumImages(I - 1, 1))
                        TempPercentage += ComparePictures(CopyImagePart(destinationBmp, New Rectangle(LinePos(J).X, LinePos(J).Y, NumImages(I - 1, 1).Width, NumImages(I - 1, 1).Height)), NumImages(I - 1, 1), 1)
                        If DifferencePercentage(J) < TempPercentage Then
                            DifferencePercentage(J) = TempPercentage
                            DifferenceID(J) = I & "x"
                        End If
                    Next
                    TempPercentage = ComparePictures(CopyImagePart(destinationBmp, New Rectangle(LinePos(J).X, LinePos(J).Y, EmptyImage.Width, EmptyImage.Height)), EmptyImage)
                    TempPercentage += ComparePictures(CopyImagePart(destinationBmp, New Rectangle(LinePos(J).X, LinePos(J).Y, EmptyImage.Width, EmptyImage.Height)), EmptyImage, 1)
                    If DifferencePercentage(J) < TempPercentage Then
                        DifferencePercentage(J) = TempPercentage
                        DifferenceID(J) = 0
                    End If

                    If Not DifferenceID(J) = "0" Then
                        If Not DifferenceID(J).Contains("x") Then
                            TestPercentage(J, 0) = ComparePictures(CopyImagePart(destinationBmp, New Rectangle(LinePos(J).X + PosFix(DifferenceID(J) - 1, 0).X, LinePos(J).Y + PosFix(DifferenceID(J) - 1, 0).Y, WithoutImages(0).Width, WithoutImages(0).Height)), WithoutImages(0), 1)
                            TestPercentage(J, 1) = ComparePictures(CopyImagePart(destinationBmp, New Rectangle(LinePos(J).X + PosFix(DifferenceID(J) - 1, 0).X, LinePos(J).Y + PosFix(DifferenceID(J) - 1, 0).Y, WithoutImages(1).Width, WithoutImages(1).Height)), WithoutImages(1), 1)
                            If TestPercentage(J, 0) > TestPercentage(J, 1) Then
                                TempName = "Steine"
                                MySchubKarr.Steine = DifferenceID(J)
                            Else
                                TempName = "Latten"
                                MySchubKarr.Latten = DifferenceID(J)
                            End If
                        Else
                            TestPercentage(J, 0) = ComparePictures(CopyImagePart(destinationBmp, New Rectangle(LinePos(J).X + PosFix(DifferenceID(J).Trim("x") - 1, 1).X, LinePos(J).Y + PosFix(DifferenceID(J).Trim("x") - 1, 1).Y, WithImages(0).Width, WithImages(0).Height)), WithImages(0), 1)
                            TestPercentage(J, 1) = ComparePictures(CopyImagePart(destinationBmp, New Rectangle(LinePos(J).X + PosFix(DifferenceID(J).Trim("x") - 1, 1).X, LinePos(J).Y + PosFix(DifferenceID(J).Trim("x") - 1, 1).Y, WithImages(1).Width, WithImages(1).Height)), WithImages(1), 1)
                            If TestPercentage(J, 0) > TestPercentage(J, 1) Then
                                TempName = "Sand"
                                MySchubKarr.Sand = DifferenceID(J).Trim("x")
                            Else
                                TempName = "Zement"
                                MySchubKarr.Zement = DifferenceID(J).Trim("x")
                            End If
                        End If
                    End If
                    DifferenceData &= vbNewLine & DifferenceID(J) & ": " & FormatNumber(DifferencePercentage(J) / 2, 2) & " - " & TempName
                Next
                For I = 1 To MySchubKarr.Steine
                    Mausklick(Windows.Forms.MouseButtons.Left, 414 + LeftFix, 517 + TopFix)
                    Threading.Thread.Sleep(1)
                Next
                For I = 1 To MySchubKarr.Latten
                    Mausklick(Windows.Forms.MouseButtons.Left, 580 + LeftFix, 455 + TopFix)
                    Threading.Thread.Sleep(1)
                Next
                For I = 1 To MySchubKarr.Sand
                    Mausklick(Windows.Forms.MouseButtons.Left, 779 + LeftFix, 526 + TopFix)
                    Threading.Thread.Sleep(1)
                Next
                For I = 1 To MySchubKarr.Zement
                    Mausklick(Windows.Forms.MouseButtons.Left, 1155 + LeftFix, 510 + TopFix)
                    Threading.Thread.Sleep(1)
                Next
                Mausklick(Windows.Forms.MouseButtons.Left, 1080 + LeftFix, 311 + TopFix)

                StartTop = StartTopCustom - StartTopFix
                Threading.Thread.Sleep(1000)
            End If
            DifferenceData = MySchubKarr.Steine & " Steine" & " - " & MySchubKarr.Latten & " Latten" & " - " & MySchubKarr.Sand & " Sand" & " - " & MySchubKarr.Zement & " Zement" & vbNewLine & vbNewLine & vbNewLine & DifferenceData

            .DrawString(DifferenceData, Me.Font, Brushes.Black, 0, 0)
        End With
    End Sub

    Private Function ComparePictures(ByVal Bitmap1 As Bitmap, ByVal Bitmap2 As Bitmap, Optional ByVal Mode As Byte = 0) As Integer
        Dim are_identical As Boolean = True
        Dim r1, g1, b1, r2, g2, b2, r3, g3, b3 As Integer
        Dim l1, l2, l3 As Integer
        Dim color1, color2 As Color
        Dim same As Double = 0
        Dim Bitmap1Colors As Double = 0
        Dim Bitmap2Colors As Double = 0

        For x As Integer = 0 To Bitmap1.Width - 1
            For y As Integer = 0 To Bitmap1.Height - 1
                color1 = Bitmap1.GetPixel(x, y)
                r1 = color1.R : g1 = color1.G : b1 = color1.B : l1 = color1.GetBrightness 'Math.Sqrt(r1 ^ 2 + g1 ^ 2 + b1 ^ 2)

                color2 = Bitmap2.GetPixel(x, y)
                r2 = color2.R : g2 = color2.G : b2 = color2.B : l2 = color2.GetBrightness 'Math.Sqrt(r2 ^ 2 + g2 ^ 2 + b2 ^ 2)

                If Mode = 0 Then 'Seems to work
                    'Calculates the average brightness levels of the images
                    l3 = (l1 + l2) \ 2

                    same += l3
                    Bitmap1Colors += l1
                    Bitmap2Colors += l2
                ElseIf Mode = 1 Then 'Seems to work
                    'Calculates the average difference of the brightness levels of the images
                    l3 = Math.Sqrt((l1 - l2) ^ 2) + 1
                    'If l3 < 1.5 Then l3 = 1

                    same += 1 / l3
                    Bitmap1Colors += 1 '+ l1
                    Bitmap2Colors += 1 '+ l2
                ElseIf Mode = 2 Then 'Does not work perfectly
                    'Compares the color + brightness level with brightness level of reference
                    r3 = (r1 + r2) \ 2
                    g3 = (g1 + g2) \ 2
                    b3 = (b1 + b2) \ 2
                    l3 = (l1 + l2) \ 2

                    same += (r3 + g3 + b3 + l3) / 4
                    Bitmap1Colors += (r1 + g1 + b1 + l1) / 4
                    Bitmap2Colors += (r2 + g2 + b2 + l2) / 4
                ElseIf Mode = 3 Then 'Does not work perfectly

                    r3 = Math.Sqrt((r1 - r2)) ^ 2 + 1
                    g3 = Math.Sqrt((g1 - g2)) ^ 2 + 1
                    b3 = Math.Sqrt((b1 - b2)) ^ 2 + 1
                    l3 = Math.Sqrt((l1 - l2)) ^ 2 + 1

                    same += 1 / r3 + 1 / g3 + 1 / b3 + 1 / l3
                    Bitmap1Colors += 4
                    Bitmap2Colors += 4
                End If
            Next y
        Next x

        Return Math.Min(same / Bitmap1Colors, same / Bitmap2Colors) * 100
    End Function

    Private Function CopyImagePart(ByVal srcBitmap As Bitmap, ByVal section As Rectangle) As Bitmap

        ' Create the new bitmap and associated graphics object
        Dim bmp As New Bitmap(section.Width, section.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)

        ' Draw the specified section of the source bitmap to the new one
        g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel)

        ' Clean up
        'g.Dispose()

        ' Return the bitmap
        Return bmp

    End Function 'Copy

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCheck.Tick
        Call DrawInfo(False)
        Me.Invalidate()
    End Sub

    Private Declare Sub mouse_event Lib "user32" ( _
  ByVal dwFlags As Long, _
  ByVal dx As Long, _
  ByVal dy As Long, _
  ByVal cButtons As Long, _
  ByVal dwExtraInfo As Long)

    Private Const MOUSEEVENTF_ABSOLUTE = &H8000
    Private Const MOUSEEVENTF_LEFTDOWN = &H2
    Private Const MOUSEEVENTF_LEFTUP = &H4
    Private Const MOUSEEVENTF_MIDDLEDOWN = &H20
    Private Const MOUSEEVENTF_MIDDLEUP = &H40
    Private Const MOUSEEVENTF_MOVE = &H1
    Private Const MOUSEEVENTF_RIGHTDOWN = &H8
    Private Const MOUSEEVENTF_RIGHTUP = &H10


    Public Sub Mausklick(Optional ByVal Button As  _
  MouseButtons = Windows.Forms.MouseButtons.Left, _
  Optional ByVal XPos As Long = -1, _
  Optional ByVal YPos As Long = -1)

        'Mauszeiger(positionieren)
        If XPos <> -1 Or YPos <> -1 Then
            Cursor.Position = New Point(XPos, YPos)
            'mouse_event(MOUSEEVENTF_ABSOLUTE + MOUSEEVENTF_MOVE, _
            'XPos / Screen.PrimaryScreen.Bounds.Width * 65535, _
            'YPos / Screen.PrimaryScreen.Bounds.Height * 65535, 0, 0)
        End If

        ' Mausklick simulieren
        Select Case Button
            ' linke Maustaste
            Case Windows.Forms.MouseButtons.Left
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)

                ' mittlere Maustaste
            Case Windows.Forms.MouseButtons.Middle
                mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0)
                mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0)

                ' rechte Maustaste
            Case Windows.Forms.MouseButtons.Right
                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
        End Select
    End Sub

    Private Sub cmbX_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbX.TextChanged
        If IsNumeric(cmbX.Text) Then
            StartLeftCustom = Int(cmbX.Text)
        Else
            StartLeftCustom = StartLeftBase
        End If

        Call frmDetect_MouseClick(vbNull, New MouseEventArgs(Windows.Forms.MouseButtons.None, 0, 0, 0, 0))
    End Sub
    Private Sub cmbX_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbX.SelectedIndexChanged
        Call cmbX_TextChanged(sender, e)
    End Sub

    Private Sub cmbY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbY.TextChanged
        If IsNumeric(cmbY.Text) Then
            StartTopCustom = Int(cmbY.Text)
        Else
            StartTopCustom = StartTopBase
        End If

        Call frmDetect_MouseClick(vbNull, New MouseEventArgs(Windows.Forms.MouseButtons.None, 0, 0, 0, 0))
    End Sub
    Private Sub cmbY_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbY.SelectedIndexChanged
        Call cmbY_TextChanged(sender, e)
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If btnGo.Text = "Go" Then
            btnGo.Text = "Stop"
            cmbX.Enabled = False
            cmbY.Enabled = False

            StartLeft = StartLeftCustom
            StartTop = StartTopCustom
            LeftFix = StartLeftCustom - StartLeftBase
            TopFix = StartTopCustom - StartTopBase

            tmrCheck.Enabled = True
        Else
            tmrCheck.Enabled = False
            cmbX.Enabled = True
            cmbY.Enabled = True
            btnGo.Text = "Go"
        End If
    End Sub
End Class
