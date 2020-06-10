Imports TIS.Imaging
Imports TIS.Imaging.VCDHelpers

Public Class Form1
    Private strobeProperty As VCDSwitchProperty

        Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' If no device is selected yet, show the selection dialog
        If Not IcImagingControl1.DeviceValid Then
            IcImagingControl1.ShowDeviceSettingsDialog()

            If Not IcImagingControl1.DeviceValid Then
                MsgBox("No device was selected.", MsgBoxStyle.Information, "Strobe")

                Me.Close()
                Exit Sub
            End If
        End If

        ' Query the strobe property
        strobeProperty = IcImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_Strobe, VCDGUIDs.VCDElement_Value, VCDGUIDs.VCDInterface_Switch)

        If Not strobeProperty Is Nothing Then
            chkStrobe.Enabled = True
            chkStrobe.CheckState = strobeProperty.Switch
        Else
            chkStrobe.Enabled = False
            System.Windows.Forms.MessageBox.Show("Strobe property is not available.", Me.Text, System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        ' start live mode
        IcImagingControl1.LiveStart()
    End Sub
    
    ''' <summary>
    ''' chkStrobe_CheckedChanged
    '''
    ''' If the user changes the contents of the strobe check box, the strobe of
    ''' the camera is to be enabled or disabled.
    ''' To handle the strobe, the "switch" property is set.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkStrobe_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStrobe.CheckedChanged
        strobeProperty.Switch = chkStrobe.Checked
    End Sub
End Class
