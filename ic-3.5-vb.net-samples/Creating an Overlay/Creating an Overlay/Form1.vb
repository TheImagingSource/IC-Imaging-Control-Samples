Imports TIS.Imaging

Public Class Form1

	Dim SinkDisplay As Form
    Dim _sink As FrameQueueSink


    ''' <summary>
    ''' Form1_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IcImagingControl1.OverlayBitmapPosition = TIS.Imaging.PathPositions.Device

        _sink = New FrameQueueSink(Function(img)
                                       Return ShowImage(img)
                                   End Function, New FrameType(MediaSubtypes.RGB32), 5)


        IcImagingControl1.Sink = _sink

        chkPPDevice.Checked = True
        chkPPSink.Checked = False
        chkPPDisplay.Checked = False

        btnBestFit.Checked = True
    End Sub

    Function ShowImage(img As IFrameQueueBuffer)
        If Not SinkDisplay Is Nothing Then
            If Not SinkDisplay.IsDisposed Then
                SinkDisplay.BackgroundImage = img.CreateBitmapCopy()
            End If
        End If
        Return FrameQueuedResult.ReQueue
    End Function

    ''' <summary>
    ''' cmdSelectDevice_Click
    '''
    ''' A click on the Device button opens the device selection dialog.
    ''' After a valid video captue device has been selected, the Settings and
    ''' Start/Stop buttons are enabled. The device settings are saved to the
    ''' registry.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub cmdDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDevice.Click
        cmdStartStop.Text = "Start"

        Dim wasLive As Boolean = IcImagingControl1.LiveVideoRunning
        If wasLive Then IcImagingControl1.LiveStop()

        IcImagingControl1.ShowDeviceSettingsDialog()

        If IcImagingControl1.DeviceValid Then

            If wasLive Then
                IcImagingControl1.LiveStart()
                cmdStartStop.Text = "Stop"
            End If

            cmdStartStop.Enabled = True
            cmdSettings.Enabled = True
        End If
    End Sub
    
    ''' <summary>
    ''' cmdDeviceSettings_Click
    '''
    ''' A click on the Settings buttons opens the camera and video properties
    ''' dialog.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub cmdSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSettings.Click
        If IcImagingControl1.DeviceValid Then
            IcImagingControl1.ShowPropertyDialog()
        End If
    End Sub
    
    ''' <summary>
    ''' cmdStartStop_Click
    '''
    ''' Start or stop the live video.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub cmdStartStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartStop.Click
        With IcImagingControl1
            If .LiveVideoRunning Then
                .LiveStop()
                cmdStartStop.Text = "Start"
            Else
                .LiveStart()
                cmdStartStop.Text = "Stop"
            End If
        End With
    End Sub
    
    ''' <summary>
    ''' SetupOverlay
    '''
    ''' Enables an OverlayBitmap object, sets a dropOutColor (magenta) and fills it with
    ''' that color
    ''' </summary>
    ''' <param name="ob"></param>
    ''' <remarks></remarks>
        Private Sub SetupOverlay(ByVal ob As TIS.Imaging.OverlayBitmap)

        ' Enable the overlay bitmap for drawing.
        ob.Enable = True

        ' Set magenta as dropout color.
        ob.DropOutColor = Color.Magenta

        ' Fill the overlay bitmap with the dropout color.
        ob.Fill(ob.DropOutColor)

        Dim OldFont As Font = ob.Font
        ob.Font = New Font("Arial", 10)

        ' Print text in red.
        ob.FontTransparent = True
        ob.DrawText(Color.Red, 10, 10, "IC Imaging Control 3.5")

        ob.Font = OldFont
    End Sub
    
    ''' <summary>
    ''' DrawOverlayInfo
    ''' </summary>
    ''' <param name="ob"></param>
    ''' <remarks></remarks>
    Private Sub DrawOverlayInfo(ByVal ob As OverlayBitmap)

        Dim OldFont As Font = ob.Font
        ob.Font = New Font("Arial", 10)

        Select Case ob.PathPosition
            Case PathPositions.Device
                ob.DrawFrameRect(Color.Red, 90, 90, 200, 130)
                ob.DrawText(Color.Red, 100, 100, "Device Overlay")
            Case PathPositions.Sink
                ob.DrawFrameRect(Color.Red, 390, 90, 500, 130)
                ob.DrawText(Color.Red, 400, 100, "Sink Overlay")
            Case PathPositions.Display
                ob.DrawFrameRect(Color.Red, 90, 290, 200, 330)
                ob.DrawText(Color.Red, 100, 300, "Display Overlay")
        End Select

        ob.Font = OldFont
    End Sub

    ''' <summary>
    ''' DrawCoordinatesystem
    ''' Draw a coordinate plane on the overlay.
    ''' </summary>
    ''' <param name="ob"></param>
    ''' <remarks></remarks>
        Private Sub DrawCoordinatesystem(ByVal ob As TIS.Imaging.OverlayBitmap)

        ' Calculate the center of the video image.

        Dim Col As Integer = _sink.OutputFrameType.Width / 2
        Dim Row As Integer = _sink.OutputFrameType.Height / 2

        Dim OldFont As Font = ob.Font
        ob.Font = New Font("Arial", 8)

        ob.DrawLine(Color.Red, Col, 0, Col, _sink.OutputFrameType.Height)
        ob.DrawLine(Color.Red, 0, Row, _sink.OutputFrameType.Width, Row)

        Dim i As Integer
        For i = 0 To Row Step 20
            ob.DrawLine(Color.Red, Col - 5, Row - i, Col + 5, Row - i)
            ob.DrawLine(Color.Red, Col - 5, Row + i, Col + 5, Row + i)
            If i > 0 Then
                ob.DrawText(Color.Red, Col + 10, Row - i - 7, i / 10)
                ob.DrawText(Color.Red, Col + 10, Row + i - 7, -i / 10)
            End If
        Next

        For i = 0 To Col Step 20
            ob.DrawLine(Color.Red, Col - i, Row - 5, Col - i, Row + 5)
            ob.DrawLine(Color.Red, Col + i, Row - 5, Col + i, Row + 5)
            If i > 0 Then
                ob.DrawText(Color.Red, Col + i - 5, Row - 17, i / 10)
                ob.DrawText(Color.Red, Col - i - 10, Row - 17, -i / 10)
            End If
        Next
        ob.Font = OldFont

    End Sub
    

    ''' <summary>
    ''' ShowBitmap
    '''
    ''' This sub demonstrates how to use OverlayBitmap.GetGraphics to draw a bitmap
    ''' from a file on the live video.
    ''' The bitmap will be drawn with transparency on the live video because
    ''' it's background color is magenta (load the image "Hardware.BMP"
    ''' with "Paint.exe" to verify this). Magenta is the currently set
    ''' dropout color. 
    ''' </summary>
    ''' <param name="ob"></param>
    ''' <remarks></remarks>
        Private Sub ShowBitmap(ByVal ob As TIS.Imaging.OverlayBitmap)

        Try
            ' Load the sample bitmap from the application's directory.
            Dim TheBitmap As Image = Bitmap.FromFile(Application.StartupPath & "\..\..\hardware.bmp")

            ' Calculate the column to display the bitmap in the
            ' upper right corner of Imaging Control.
            Dim Col As Integer = _sink.OutputFrameType.Width - 5 - TheBitmap.Width

            'Retrieve the Graphics object of the OverlayBitmap.
            Dim g As Graphics = ob.GetGraphics()

            g.DrawImage(TheBitmap, Col, 5)

            ' Release the Graphics after drawing is finished.
            ob.ReleaseGraphics(g)

        Catch Ex As System.Exception
            MsgBox("File not found: " + Ex.Message, MsgBoxStyle.Information, "ShowBitmap")
        End Try
    End Sub
    
    ''' <summary>
    ''' Timer1_Timer
    '''
    ''' In the timer event, the current system time is drawn on the live
    ''' video stream.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim ob = IcImagingControl1.OverlayBitmapAtPath(PathPositions.Device)

        If IcImagingControl1.LiveVideoRunning Then
            Dim OldFont As Font = ob.Font
            ob.Font = New Font("Arial", 14, FontStyle.Bold)

            ' Draw the time in the lower left corner of the video window.
            Dim Col As Integer = _sink.OutputFrameType.Width - 81
            Dim Row As Integer = _sink.OutputFrameType.Height - 20

            ' Setup the background color and drawing mode.
            ob.FontTransparent = False
            ob.FontBackColor = Color.Black

            ' Draw the text with white color.
            ob.DrawText(Color.White, Col, Row, DateTime.Now.ToString("T"))

            ' Reset the font and drawing mode.
            ob.FontTransparent = True
            ob.Font = OldFont
        End If
    End Sub
    
    ''' <summary>
    ''' ICImagingControl1_OverlayUpdate
    '''
    ''' In the overlay update event, the frames are counted in
    ''' the variable FrameCount. FrameCount is used to draw a
    ''' rising triangle to show the frame count. If FrameCount
    ''' is greater than 25, the drawn triangle is deleted by
    ''' drawing a solid rectangle with the dropout color over it.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub IcImagingControl1_OverlayUpdate(ByVal sender As System.Object, ByVal e As TIS.Imaging.ICImagingControl.OverlayUpdateEventArgs) Handles IcImagingControl1.OverlayUpdate
        Dim ob As TIS.Imaging.OverlayBitmap = e.overlay
        Dim info = IcImagingControl1.DriverFrameDropInformation

        Dim frameCount As Integer = info.FramesDelivered

        Dim lineIndex As Integer = frameCount Mod 25

		If lineIndex = 0 Then
			' Delete the triangle.
            ob.DrawSolidRect(ob.DropOutColor, 10, _
                     _sink.OutputFrameType.Height - 70, _
                     62, _
                     _sink.OutputFrameType.Height - 9)
		End If

        ' Draw the new triangle line.
        ob.DrawLine(Color.Yellow, lineIndex * 2 + 10, _
         _sink.OutputFrameType.Height - 10, _
         lineIndex * 2 + 10, _
         _sink.OutputFrameType.Height - 10 - lineIndex)

        ' Print the current frame number:

        ' Set the background color to the current dropout color
        ' and make the font opaque.
        ob.FontBackColor = ob.DropOutColor
        ob.FontTransparent = False

        ' Save the current font.
        Dim OldFont As Font = ob.Font

        ' Set the new created font.
        ob.Font = New Font("Arial", 8)

        ' Draw the text.
        ob.DrawText(Color.Yellow, 70, _sink.OutputFrameType.Height - 19, Str(frameCount) + "  ")

        ' Restore the previously used font.
        ob.Font = OldFont
    End Sub
    
    ''' <summary>
    ''' EnableOverlayBitmapAtPath
    ''' 
    '''   Enables or disables the overlay bitmap for a specified path position.
    ''' </summary>
    ''' <param name="pos"></param>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
        Private Sub EnableOverlayBitmapAtPath(ByVal pos As TIS.Imaging.PathPositions, ByVal enabled As Boolean)

        Dim wasLive As Boolean = IcImagingControl1.LiveVideoRunning
        If wasLive Then IcImagingControl1.LiveStop()

        Dim oldPos As TIS.Imaging.PathPositions = IcImagingControl1.OverlayBitmapPosition
        If enabled Then
            IcImagingControl1.OverlayBitmapPosition = oldPos Or pos
        Else
            IcImagingControl1.OverlayBitmapPosition = oldPos And Not pos
        End If

        If wasLive Then IcImagingControl1.LiveStart()
    End Sub
    
    Private Sub chkPPDevice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPPDevice.CheckedChanged
        EnableOverlayBitmapAtPath(PathPositions.Device, chkPPDevice.Checked)
    End Sub

    Private Sub chkPPSink_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPPSink.CheckedChanged
        EnableOverlayBitmapAtPath(PathPositions.Sink, chkPPSink.Checked)

        If chkPPSink.Checked Then

            If SinkDisplay Is Nothing OrElse SinkDisplay.IsDisposed Then

                SinkDisplay = New Form
                SinkDisplay.Text = "Sink"

            End If

            If IcImagingControl1.DeviceValid Then
                Dim dx As Integer = SinkDisplay.Size.Width - SinkDisplay.ClientSize.Width
                Dim dy As Integer = SinkDisplay.Size.Height - SinkDisplay.ClientSize.Height

                SinkDisplay.Width = _sink.OutputFrameType.Width + dx
                SinkDisplay.Height = _sink.OutputFrameType.Height + dy
            End If

            SinkDisplay.Show()
        End If
    End Sub

    Private Sub chkPPDisplay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPPDisplay.CheckedChanged
        EnableOverlayBitmapAtPath(PathPositions.Display, chkPPDisplay.Checked)
    End Sub


    ''' <summary>
    ''' SetOverlayColorMode
    '''
    ''' Sets the color modes of all overlay bitmaps to 'mode'
    ''' </summary>
    ''' <param name="mode"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub setOverlayBitmapColorModes(ByVal mode As TIS.Imaging.OverlayColorModes)

        Dim wasLive As Boolean = IcImagingControl1.LiveVideoRunning
        If wasLive Then IcImagingControl1.LiveStop()

        IcImagingControl1.OverlayBitmapAtPath(TIS.Imaging.PathPositions.Device).ColorMode = mode
        IcImagingControl1.OverlayBitmapAtPath(TIS.Imaging.PathPositions.Display).ColorMode = mode
        IcImagingControl1.OverlayBitmapAtPath(TIS.Imaging.PathPositions.Sink).ColorMode = mode

        If wasLive Then IcImagingControl1.LiveStart()
    End Sub

    Private Sub btnColor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColor.CheckedChanged
        setOverlayBitmapColorModes(OverlayColorModes.Color)
    End Sub

    Private Sub btnGrayscale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrayscale.CheckedChanged
        setOverlayBitmapColorModes(OverlayColorModes.Grayscale)
    End Sub

    Private Sub btnBestFit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBestFit.CheckedChanged
        setOverlayBitmapColorModes(OverlayColorModes.BestFit)
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not SinkDisplay Is Nothing AndAlso Not SinkDisplay.IsDisposed Then
            SinkDisplay.Close()
        End If
    End Sub

    ''' <summary>
    ''' After the filtergraph has bee prepared, text and graphics can be drawn
    ''' on the overlay bitmap.
        Private Sub IcImagingControl1_LivePrepared(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IcImagingControl1.LivePrepared

		If IcImagingControl1.DeviceValid Then


			SetupOverlay(IcImagingControl1.OverlayBitmapAtPath(PathPositions.Device))
			SetupOverlay(IcImagingControl1.OverlayBitmapAtPath(PathPositions.Sink))
			SetupOverlay(IcImagingControl1.OverlayBitmapAtPath(PathPositions.Display))

			' Display a coordinate system on the device overlay
			DrawCoordinatesystem(IcImagingControl1.OverlayBitmapAtPath(PathPositions.Device))

			' Load a  bitmap file and display it on the display overlay
			If IcImagingControl1.OverlayBitmapPosition.Equals(PathPositions.Display) Then
                ShowBitmap(IcImagingControl1.OverlayBitmapAtPath(PathPositions.Display))
            End If

			' Draw overlay info boxes
			DrawOverlayInfo(IcImagingControl1.OverlayBitmapAtPath(PathPositions.Device))
			DrawOverlayInfo(IcImagingControl1.OverlayBitmapAtPath(PathPositions.Sink))
			DrawOverlayInfo(IcImagingControl1.OverlayBitmapAtPath(PathPositions.Display))
		End If
    End Sub
    End Class
