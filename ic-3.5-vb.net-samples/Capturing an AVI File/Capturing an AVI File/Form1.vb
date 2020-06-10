Public Class Form1

    ''' <summary>
    ''' Form1_Load
    '''
    ''' If no video capture device was selected in the properties window of IC
    ''' Imaging Control, the device settings dialog of IC Imaging Control is shown.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IcImagingControl1.DeviceValid Then
            IcImagingControl1.ShowDeviceSettingsDialog()

            If Not IcImagingControl1.DeviceValid Then
                MessageBox.Show("No device was selected.", "Capturing an AVI File")
                Me.Close()
                Exit Sub
            End If
        End If
    End Sub

    ''' <summary>
    ''' cmdStartLive_Click
    '''
    ''' Start the live video.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub cmdStartLive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartLive.Click
        IcImagingControl1.LiveStart()
    End Sub
    
    ''' <summary>
    ''' cmdStopLive_Click
    '''
    ''' Stop the live video.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub cmdStopLive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStopLive.Click
        IcImagingControl1.LiveStop()
    End Sub
    
    Private Sub cmdCaptureAVI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCaptureAVI.Click
        Dim AVIDlg As writeavi = New writeavi(IcImagingControl1)
        AVIDlg.ShowDialog()
        AVIDlg.Dispose()
        AVIDlg = Nothing

    End Sub
End Class
