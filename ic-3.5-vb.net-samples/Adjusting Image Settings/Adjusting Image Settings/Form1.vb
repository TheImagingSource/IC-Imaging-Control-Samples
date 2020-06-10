Imports System.Windows.Forms
Imports TIS.Imaging
Imports TIS.Imaging.VCDHelpers

Public Class Form1
    Dim BrightnessProperty As VCDRangeProperty
    
    ''' <summary>
    ''' All preparations are performed here.
    ''' - selecting a device
    ''' - starting the live video
    ''' - fetching the properties and initializing the controls
    ''' </summary>
        Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If IcImagingControl1.ShowDeviceSettingsDialog = DialogResult.OK Then
            IcImagingControl1.LiveStart()

            BrightnessProperty = IcImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_Brightness, VCDGUIDs.VCDElement_Value, VCDGUIDs.VCDInterface_Range)

            If Not BrightnessProperty Is Nothing Then
                ' Setup the range of the brightness slider.
                trackBar1.Minimum = BrightnessProperty.RangeMin
                trackBar1.Maximum = BrightnessProperty.RangeMax
                ' Set the slider to the current brightness value.
                trackBar1.Value = BrightnessProperty.Value
            Else
                System.Windows.Forms.MessageBox.Show("Brightness property is not available.", Me.Text, System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Information)
                trackBar1.Enabled = False
            End If
        Else
            System.Windows.Forms.MessageBox.Show("No device was selected.", Me.Text, System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If

    End Sub
    
    Private Sub trackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trackBar1.Scroll
        BrightnessProperty.Value = trackBar1.Value
    End Sub
    
End Class
