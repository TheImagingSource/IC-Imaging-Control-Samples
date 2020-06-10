Public Class StringCombo
    Implements IControlBase
    Implements IControlSlider

    ' The interface this combo controls
    Dim MapStringsItf As TIS.Imaging.VCDMapStringsProperty

    ' Flag to indicate that we are updating ourself, so that we have to ignore changes to the slider
    Dim updating As Boolean

    ' Collection of sliders connected to interfaces of the same item
    ' These sliders have to be updated when this combo is changed
    Dim sisterSliders As System.Collections.ArrayList


    Private Sub Combo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combo.SelectedIndexChanged
        Try

            ' Only change the property if the click event was caused by the user
            If Not updating Then

                ' Assign the new string
                If Not MapStringsItf.ReadOnly Then
                    MapStringsItf.String = Combo.Text
                End If

                ' If we know about possibly connected sliders, update them
                If Not sisterSliders Is Nothing Then
                    For Each ctl As IControlBase In sisterSliders
                        ctl.UpdateControl()
                    Next
                End If

            End If

        Catch ex As System.Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Public Sub UpdateControl() Implements IControlBase.UpdateControl
        updating = True

        ' Check whether the property is available
        Combo.Enabled = MapStringsItf.Available

        ' Select the current string
        ScrollUpdate()

        updating = False
    End Sub

    Private Sub InitialUpdate()

        ' Fill the combo box with the available strings
        Combo.Items.Clear()
        For Each s As String In MapStringsItf.Strings
            Combo.Items.Add(s)
        Next

    End Sub

    Public Sub ScrollUpdate() Implements IControlSlider.ScrollUpdate

        updating = True

        ' Calculate the new position
        Combo.SelectedIndex = MapStringsItf.Value - MapStringsItf.RangeMin

        updating = False

    End Sub

    Private Sub StringCombo_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Combo.Width = Width
    End Sub

    Public Sub AssignItf(ByRef itf As TIS.Imaging.VCDMapStringsProperty)
        MapStringsItf = itf
        InitialUpdate()
        UpdateControl()
    End Sub

    Public Sub setSisterSliders(ByRef sliders As System.Collections.ArrayList)
        sisterSliders = sliders
    End Sub

End Class
