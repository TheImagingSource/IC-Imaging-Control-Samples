Imports TIS.Imaging
Imports TIS.Imaging.VCDHelpers

Public Class Form1

    Private VCDProp As VCDSimpleProperty
    Private _GPIn As VCDRangeProperty
    Private _GPInRead As VCDButtonProperty
    Private _GPOut As VCDRangeProperty
    Private _GPOutRead As VCDButtonProperty


        Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' If no device is selected yet, show the selection dialog
        If Not IcImagingControl1.DeviceValid Then
            IcImagingControl1.ShowDeviceSettingsDialog()

            If Not IcImagingControl1.DeviceValid Then
                MsgBox("No device was selected.", MsgBoxStyle.Information, "DigitalIO")

                Me.Close()
                Exit Sub
            End If
        End If


        ' Query GP in and out properties.
        _GPIn = IcImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_GPIO, VCDGUIDs.VCDElement_GPIOIn, VCDGUIDs.VCDInterface_Range)
        If Not _GPIn Is Nothing Then
            _GPInRead = IcImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_GPIO, VCDGUIDs.VCDElement_GPIORead, VCDGUIDs.VCDInterface_Button)
            cmdReadDigitalInput.Enabled = True
            ReadDigitalInput()
        Else
            cmdReadDigitalInput.Enabled = False
        End If

        _GPOut = IcImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_GPIO, VCDGUIDs.VCDElement_GPIOOut, VCDGUIDs.VCDInterface_Range)
        If Not _GPOut Is Nothing Then
            _GPOutRead = IcImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_GPIO, VCDGUIDs.VCDElement_GPIOWrite, VCDGUIDs.VCDInterface_Button)
            cmdWriteDigitalOutput.Enabled = True
            chkDigitalOutputState.Enabled = True

            If _GPOut.Value = 1 Then
                chkDigitalOutputState.CheckState = CheckState.Checked
            Else
                chkDigitalOutputState.CheckState = CheckState.Unchecked
            End If
        Else
            cmdWriteDigitalOutput.Enabled = False
            chkDigitalOutputState.Enabled = False
        End If

        ' start live mode
        IcImagingControl1.LiveStart()

    End Sub
    
    Private Sub cmdReadDigitalInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReadDigitalInput.Click
        ReadDigitalInput()
    End Sub

    ''' <summary>
    ''' cmdReadDigitalInput_Click
    '''
    ''' Read the current state of the digital input and set the checkbox
    ''' chkDigitalInputState to this state.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadDigitalInput()
        ' Read the digital input states of the video capture device.
        _GPInRead.Push()

        If _GPIn.Value = 1 Then
            chkDigitalInputState.CheckState = CheckState.Checked
        Else
            chkDigitalInputState.CheckState = CheckState.Unchecked
        End If

    End Sub

    ''' <summary>
    ''' cmdWriteDigitalOutput_Click
    '''
    ''' Set the digital outputs of the video capture device to the state of the
    ''' chkDigitalOutputState check box and write this state to the video capture
    ''' device.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdWriteDigitalOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWriteDigitalOutput.Click
        ' Set the state, that will be send to the video capture device.
        If chkDigitalOutputState.CheckState = CheckState.Checked Then
            _GPOut.Value = 1
        Else
            _GPOut.Value = 0
        End If

        ' Send / write the digital output state to the video capture device.
        _GPOutRead.Push()
    End Sub
End Class
