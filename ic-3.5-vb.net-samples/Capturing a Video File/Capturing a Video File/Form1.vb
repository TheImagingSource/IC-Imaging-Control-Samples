Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IcImagingControl1.DeviceValid Then

            IcImagingControl1.ShowDeviceSettingsDialog()

            If Not IcImagingControl1.DeviceValid Then
                Close()
                Exit Sub
            End If

        End If

        IcImagingControl1.LiveStart()

    End Sub

        Private Sub btnStartLive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartLive.Click
        IcImagingControl1.LiveStart()
    End Sub
    
        Private Sub btnStopLive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopLive.Click
        IcImagingControl1.LiveStop()
    End Sub
    
    Private Sub btnCaptureVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCaptureVideo.Click
        Dim frm As New SaveVideoForm(IcImagingControl1)
        frm.ShowDialog()
    End Sub
End Class
