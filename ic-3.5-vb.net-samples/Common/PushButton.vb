Public Class PushButton
    Implements IControlBase

    ' The interface this button controls
    Dim PushItf As TIS.Imaging.VCDButtonProperty

    ' Collection of controls connected to interfaces of the same item
    ' These controls have to be updated when this button is pushed
    Dim sisterControls As System.Collections.ArrayList

    Public Sub UpdateControl() Implements IControlBase.UpdateControl
        ' Check whether the button property is available
        Button.Enabled = PushItf.Available
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button.Click
        Try

            ' Notify the interface
            PushItf.Push()

            ' If we know about controls of the same item, update them
            If Not sisterControls Is Nothing Then
                For Each ctl As IControlBase In sisterControls
                    ctl.UpdateControl()
                Next
            End If

        Catch ex As System.Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button.Resize
        ' The button fills the whole user control
        Button.Width = Width
        Button.Height = Height
    End Sub

    Public Sub AssignItf(ByRef itf As TIS.Imaging.VCDButtonProperty)
        PushItf = itf
        Button.Text = itf.Parent.Name
        UpdateControl()
    End Sub

    Public Sub setSisterControls(ByRef controls As System.Collections.ArrayList)
        sisterControls = controls
    End Sub

End Class
