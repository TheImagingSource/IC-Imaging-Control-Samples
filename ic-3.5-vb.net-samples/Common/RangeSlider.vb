Public Class RangeSlider
    Implements IControlBase
    Implements IControlSlider

    ' The interface this slider controls
    Dim RangeItf As TIS.Imaging.VCDRangeProperty

    ' Collection of sliders connected to interfaces of the same item
    ' These sliders have to be updated when this slider is changed
    Dim sisterSliders As System.Collections.ArrayList


    Private Sub Slider_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Slider.Scroll
        Try
            ' Assign the new value to the property
            If Not RangeItf.ReadOnly Then
                RangeItf.Value = Slider.Value * RangeItf.Delta
            End If

            ' Update the text box
            ValueText.Text = RangeItf.Value.ToString()

            ' If we know about possibly connected sliders, update them
            If Not sisterSliders Is Nothing Then
                For Each sld As IControlSlider In sisterSliders
                    If Not sld Is Me Then
                        sld.ScrollUpdate()
                    End If
                Next
            End If

        Catch ex As System.Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub UpdateControl() Implements IControlBase.UpdateControl

        ' Check whether the property is available
        If RangeItf.Available Then

            ' Enable slider and textbox
            Slider.Enabled = True
            ValueText.Enabled = True

            ' Update slider position
            ScrollUpdate()
        Else
            ' Disable slider
            Slider.Enabled = False

            ' Disable text
            ValueText.Text = ""
            ValueText.Enabled = False
        End If

    End Sub

    Private Sub InitialUpdate()

        Dim min, max As Integer

        ' Initialize the slider range
        min = RangeItf.RangeMin / RangeItf.Delta
        max = RangeItf.RangeMax / RangeItf.Delta

        Slider.TickFrequency = 1
        If max - min > 50 Then
            Slider.TickFrequency = 10
        End If
        If max - min > 500 Then
            Slider.TickFrequency = 100
        End If

        Slider.Minimum = min
        Slider.Maximum = max

        If min = max Then
            Slider.Enabled = False
        End If

    End Sub

    Public Sub ScrollUpdate() Implements IControlSlider.ScrollUpdate

        If Not RangeItf.Available Then Exit Sub

        ' Calculate the new slider position
        Dim pos As Integer
        pos = RangeItf.Value / RangeItf.Delta

        If pos < Slider.Minimum Or pos > Slider.Maximum Then
            Slider.Enabled = False
        Else
            Slider.Value = pos
            ValueText.Text = pos.ToString()
        End If

    End Sub

    Public Sub AssignItf(ByRef itf As TIS.Imaging.VCDRangeProperty)
        RangeItf = itf
        InitialUpdate()
        UpdateControl()
    End Sub

    Public Sub setSisterSliders(ByRef sliders As System.Collections.ArrayList)
        sisterSliders = sliders
    End Sub

    Private Sub RangeSlider_Layout(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles MyBase.Layout
        Slider.Width = Width * 80 / 100
        Slider.Height = Height
        ValueText.Left = Width * 80 / 100
        ValueText.Width = Width * 20 / 100

        ValueText.Top = (Height - 23) / 2
        ValueText.Height = 23
    End Sub

End Class
