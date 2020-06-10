Public Class Form1

    ''' <summary>
    ''' cmdDevice_Click
    '''
    ''' Show the frmDeviceSettings dialog to select a video capture
    ''' device. If the new device is valid, the controls in the form
    ''' are enabled and initialized.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub cmdDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDevice.Click
        If IcImagingControl1.DeviceValid Then
            If IcImagingControl1.LiveVideoRunning Then
                IcImagingControl1.LiveStop()
            End If
        End If
        IcImagingControl1.ShowDeviceSettingsDialog()
        If IcImagingControl1.DeviceValid Then
            cmdStart.Enabled = True
            cmdStop.Enabled = True
            cmdImageSettings.Enabled = True
            chkDisplayDefault.Enabled = True
            chkScrollbarsEnable.Enabled = True
            sldZoom.Enabled = True
            lblZoom.Enabled = True

            IcImagingControl1.LiveDisplayDefault = False
            chkDisplayDefault.Checked = False
            IcImagingControl1.LiveDisplaySize = IcImagingControl1.LiveDisplayOutputSize

            chkScrollbarsEnable.Checked = IcImagingControl1.ScrollbarsEnabled

            ' Enable or disable the slider for the zoom factor, depending
            ' on the LiveDisplayDefault property.
            sldZoom.Enabled = Not IcImagingControl1.LiveDisplayDefault
            sldZoom.Value = IcImagingControl1.LiveDisplayZoomFactor * 10
            lblZoomPercent.Text = Str(sldZoom.Value * 10) + "%"
        End If
    End Sub
    
    ''' <summary>
    ''' sldZoom_Scroll
    '''
    ''' Set a new zoom factor.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub sldZoom_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sldZoom.Scroll
        If IcImagingControl1.LiveDisplayDefault = False Then
            IcImagingControl1.LiveDisplayZoomFactor = sldZoom.Value / 10
            lblZoomPercent.Text = Str(sldZoom.Value * 10) + "%"
        Else
            MsgBox("The zoom factor can only be set" + Chr(13) + _
                   "if LiveDisplayDefault returns False!")
        End If
    End Sub
    
    ''' <summary>
    ''' cmdImageSettings_Click
    '''
    ''' Show the image settings dialog.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub cmdImageSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImageSettings.Click
        IcImagingControl1.ShowPropertyDialog()
    End Sub
    
    ''' <summary>
    ''' cmdStart_Click
    ''' Start the live video and draw a rectangle around the live video.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        With IcImagingControl1
            If .DeviceValid Then
                .LiveStart()
                ' Draw a rectangle around the whole image to visualize its perimeter.
                .OverlayBitmap.Enable = True
                .OverlayBitmap.DrawLine(Color.FromArgb(255, 0, 0), 0, 0, .LiveDisplayOutputSize.Width - 1, 0)
                .OverlayBitmap.DrawLine(Color.FromArgb(255, 0, 0), .LiveDisplayOutputSize.Width - 1, 0, .LiveDisplayOutputSize.Width - 1, .LiveDisplayOutputSize.Height - 1)
                .OverlayBitmap.DrawLine(Color.FromArgb(255, 0, 0), .LiveDisplayOutputSize.Width - 1, .LiveDisplayOutputSize.Height - 1, 0, .LiveDisplayOutputSize.Height - 1)
                .OverlayBitmap.DrawLine(Color.FromArgb(255, 0, 0), 0, 0, 0, .LiveDisplayOutputSize.Height)
                .OverlayBitmap.DrawText(Color.FromArgb(255, 0, 0), 5, 5, "Scroll and Zoom Sample")
            End If
        End With
    End Sub
    
    ''' <summary>
    ''' cmdStop_Click
    '''
    ''' Stop the live video.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        IcImagingControl1.LiveStop()
    End Sub
    
    ''' <summary>
    ''' chkDisplayDefault_CheckedChanged
    '''
    ''' Enable or disable the LiveDisplayDefault property.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub chkDisplayDefault_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDisplayDefault.CheckedChanged
        IcImagingControl1.LiveDisplayDefault = chkDisplayDefault.Checked
        sldZoom.Value = IcImagingControl1.LiveDisplayZoomFactor * 10
        lblZoomPercent.Text = Str(sldZoom.Value * 10) + "%"
        sldZoom.Enabled = Not IcImagingControl1.LiveDisplayDefault
    End Sub
    
    ''' <summary>
    ''' chkScrollbarsEnable_CheckedChanged
    '''
    ''' Enable or disable the scroll bars.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub chkScrollbarsEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScrollbarsEnable.CheckedChanged
        IcImagingControl1.ScrollbarsEnabled = chkScrollbarsEnable.Checked
    End Sub
    
    ''' <summary>
    ''' When the user uses a scroll bar to move the live image, update
    ''' lblScrollPosition.Text
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub IcImagingControl1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles IcImagingControl1.Scroll
        Dim p As Point = IcImagingControl1.AutoScrollPosition
        lblScrollPosition.Text = String.Format("{0}/{1}", p.X, p.Y)
    End Sub
    
End Class
