Public Class Form1

    ''' <summary>
    ''' Form_Load
    '''
    ''' Initializes the buttons and sets the size of the control
    ''' to the size of the currently selected video format.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Check if a valid video capture device has been selected, otherwise
        ' show the device selection dialog of Imaging Control.
        If Not IcImagingControl1.DeviceValid Then
            IcImagingControl1.ShowDeviceSettingsDialog()

            If Not IcImagingControl1.DeviceValid Then
                MessageBox.Show("No device was selected.", "Display Buffer Sample")
                Me.Close()
                Exit Sub
            End If
        End If
        IcImagingControl1.Sink = New TIS.Imaging.FrameQueueSink(Function(img)
                                                                    Return ShowImage(img)
                                                                End Function, New TIS.Imaging.FrameType(TIS.Imaging.MediaSubtypes.RGB32), 5)
        cmdStop.Enabled = False
        IcImagingControl1.LiveDisplayDefault = False
        IcImagingControl1.LiveDisplaySize = IcImagingControl1.Size
        IcImagingControl1.LiveDisplay = False
    End Sub
    
    ''' <summary>
    ''' cmdStart
    '''
    ''' Starts the Display.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        IcImagingControl1.LiveStart()
        cmdStart.Enabled = False
        cmdStop.Enabled = True
    End Sub
    
    ''' <summary>
    ''' cmdStop
    '''
    ''' Stops the Display.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        cmdStart.Enabled = True
        cmdStop.Enabled = False
        IcImagingControl1.LiveStop()
    End Sub
    
    Function ShowImage(img As TIS.Imaging.IFrameQueueBuffer)
        IcImagingControl1.DisplayImageBuffer(img)

        Return TIS.Imaging.FrameQueuedResult.ReQueue
    End Function
End Class
