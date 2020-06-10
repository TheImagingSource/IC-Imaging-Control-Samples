Public Class Switch
    Implements IControlBase

    ' The interface this switch controls
    Dim SwitchItf As TIS.Imaging.VCDSwitchProperty

    Dim updating As Boolean

    ' Collection of controls connected to interfaces of the same item
    ' These controls have to be updated when this button is pushed
    Dim sisterControls As System.Collections.ArrayList

    Private Sub Check_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check.CheckedChanged
        Try
            ' Only change the property if the click event was caused by the user
            If updating Then Exit Sub

            If Not SwitchItf.ReadOnly Then
                ' Assign the new value to the property
                SwitchItf.Switch = Check.Checked
            End If

            ' If we know about controls of the same item, update them
            If Not sisterControls Is Nothing Then
                For Each chk As IControlBase In sisterControls
                    chk.UpdateControl()
                Next
            End If

        Catch ex As System.Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Switch_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        ' The button fills the whole user control
        Check.Width = Width
        Check.Height = Height
    End Sub

    Public Sub UpdateControl() Implements IControlBase.UpdateControl

        updating = True

        Check.Enabled = SwitchItf.Available
        Check.Checked = SwitchItf.Switch

        updating = False
    End Sub

    Public Sub AssignItf(ByRef itf As TIS.Imaging.VCDSwitchProperty)
        SwitchItf = itf
        Check.Text = itf.Parent.Name
        UpdateControl()
    End Sub

    Public Sub setSisterControls(ByRef ctrls As System.Collections.ArrayList)
        sisterControls = ctrls
    End Sub

End Class
