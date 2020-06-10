Public Class AbsValSlider
    Implements IControlBase
    Implements IControlSlider


    ' The interface this slider controls
    Dim AbsValItf As TIS.Imaging.VCDAbsoluteValueProperty

    ' Flag to indicate that we are updating ourself, so that we have to ignore changes to the slider
    Dim updating As Boolean

    ' Flag to indicate that this control was changed by the user, so that we do not have to update it
    Dim selfClicked As Boolean

    ' Collection of sliders connected to interfaces of the same item
    ' These sliders have to be updated when this slider is changed
    Dim sisterSliders As System.Collections.ArrayList

    Private Sub Slider_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Slider.Scroll
        Try

            ' Only change the property if the scroll event was caused by the user
            If Not updating Then

                ' Get the new absolute value depending on the slider position
                ' and set the new value to the interface
                If Not AbsValItf.ReadOnly Then
                    AbsValItf.Value = GetAbsVal()
                End If

                ' Read the new value back from the absolute value interface
                ' This has to be done because we do not know the granularity of the absolute values
                ' and the value that has really been set normally differs from the value we
                ' assigned to the property
                ScrollUpdate()

                ' If we know about possibly connected sliders, update them
                selfClicked = True
                If Not sisterSliders Is Nothing Then
                    For Each sld As IControlSlider In sisterSliders
                        sld.ScrollUpdate()
                    Next
                End If
                selfClicked = False

            End If

        Catch ex As System.Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub UserControl_Initialize()
        updating = False
        selfClicked = False
    End Sub

    Private Sub AbsValSlider_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If AbsValItf Is Nothing Then Exit Sub

        ' Determine the length of the describing text at some points to estimate
        ' a good width for the edit box
        Dim lenmin As Integer
        Dim lenmid As Integer
        Dim lenmax As Integer

        Dim g As System.Drawing.Graphics
        g = Me.CreateGraphics()

        lenmin = g.MeasureString(GetText(AbsValItf.RangeMin), Me.Font).Width
        lenmid = g.MeasureString(GetText((AbsValItf.RangeMax - AbsValItf.RangeMin) / 2), Me.Font).Width
        lenmax = g.MeasureString(GetText(AbsValItf.RangeMax), Me.Font).Width
        g.Dispose()

        Dim textlen As Integer
        textlen = lenmin
        If lenmid > textlen Then textlen = lenmid
        If lenmax > textlen Then textlen = lenmax

        ' Resize the slider and the edit box
        Slider.Width = Width - (textlen + 20)
        Slider.Height = Height
        ValueText.Left = Width - (textlen + 20)
        ValueText.Width = textlen + 20

        ValueText.Top = (Height - 23) / 2 - 2
        ValueText.Height = 23
    End Sub

    ' This function calculates the needed position of the slider based on the current absolute value
    Private Function GetSliderPos() As Integer
        Dim rmin As Double
        Dim rmax As Double
        Dim absval As Double
        Dim rangelen As Double
        Dim p As Double

        ' Get the property data from the interface
        rmin = AbsValItf.RangeMin
        rmax = AbsValItf.RangeMax
        absval = AbsValItf.Value

        ' Do calculation depending of the dimension function of the property
        If AbsValItf.DimFunction = TIS.Imaging.AbsDimFunction.eAbsDimFunc_Log Then

            rangelen = System.Math.Log(rmax) - System.Math.Log(rmin)
            p = 100 / rangelen * (System.Math.Log(absval) - System.Math.Log(rmin))
        Else ' AbsValItf.DimFunction = AbsDimFunction.eAbsDimFunc_Linear
            rangelen = rmax - rmin
            p = 100 / rangelen * (absval - rmin)
        End If

        ' Round to integer
        GetSliderPos = System.Math.Round(p, 0)
    End Function

    ' This function calculates the current absolute value based on the position of the slider
    Private Function GetAbsVal() As Double

        Dim rmin As Double
        Dim rmax As Double
        Dim rangelen As Double
        Dim value As Double

        ' Get the property data from the interface
        rmin = AbsValItf.RangeMin
        rmax = AbsValItf.RangeMax

        ' Do calculation depending of the dimension function of the property
        If AbsValItf.DimFunction = TIS.Imaging.AbsDimFunction.eAbsDimFunc_Log Then

            rangelen = System.Math.Log(rmax) - System.Math.Log(rmin)
            value = System.Math.Exp(System.Math.Log(rmin) + rangelen / 100 * Slider.Value)

        Else ' AbsValItf.DimFunction = AbsDimFunction.eAbsDimFunc_Linear

            rangelen = rmax - rmin
            value = rmin + rangelen / 100 * Slider.Value

        End If

        ' Correct the value if it is out of bounds
        If value > rmax Then value = rmax
        If value < rmin Then value = rmin

        GetAbsVal = value

    End Function

    ' This function creates a text to represent the current absolute value
    Private Function GetText(ByRef val As Double) As String

        ' Just take the number with some precision
        GetText = System.Math.Round(val, 4) & " " & AbsValItf.DimType

    End Function

    Public Sub UpdateControl() Implements IControlBase.UpdateControl

        updating = True

        ' Check whether the property is available
        If AbsValItf.Available Then

            ' Enable the slider
            Slider.Enabled = True

            ScrollUpdate()
        Else
            ' Disable the slider
            Slider.Enabled = False

            ' Disable the text box
            ValueText.Text = ""
            ValueText.Enabled = False
        End If

        updating = False
    End Sub

    Private Sub InitialUpdate()

        Slider.Minimum = 0
        Slider.Maximum = 100

    End Sub

    Public Sub ScrollUpdate() Implements IControlSlider.ScrollUpdate

        ' Do not update if this event was caused by this control
        If selfClicked Then Exit Sub

        ' Do not update if the property is not avilable
        If Not AbsValItf.Available Then Exit Sub

        updating = True

        ' Assign a text representation of the current value to the text box
        ValueText.Text = GetText((AbsValItf.Value))
        If Not ValueText.Enabled Then ValueText.Enabled = True

        ' Set the slider position
        Slider.Value = GetSliderPos()

        updating = False
    End Sub

    Public Sub AssignItf(ByRef itf As TIS.Imaging.VCDAbsoluteValueProperty)

        AbsValItf = itf

        ' Setup the control to match the interface
        InitialUpdate()

        ' Read the initial values
        UpdateControl()
    End Sub

    Public Sub setSisterSliders(ByRef sliders As System.Collections.ArrayList)
        sisterSliders = sliders
    End Sub

    Private Sub AbsValSlider_Layout(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles MyBase.Layout
        If AbsValItf Is Nothing Then Exit Sub

        ' Determine the length of the describing text at some points to estimate
        ' a good width for the edit box
        Dim lenmin As Integer
        Dim lenmid As Integer
        Dim lenmax As Integer

        Dim g As System.Drawing.Graphics
        g = Me.CreateGraphics()

        lenmin = g.MeasureString(GetText(AbsValItf.RangeMin), Me.Font).Width
        lenmid = g.MeasureString(GetText((AbsValItf.RangeMax - AbsValItf.RangeMin) / 2), Me.Font).Width
        lenmax = g.MeasureString(GetText(AbsValItf.RangeMax), Me.Font).Width
        g.Dispose()

        Dim textlen As Integer
        textlen = lenmin
        If lenmid > textlen Then textlen = lenmid
        If lenmax > textlen Then textlen = lenmax

        ' Resize the slider and the edit box
        Slider.Width = Width - (textlen + 20)
        Slider.Height = Height
        ValueText.Left = Width - (textlen + 20)
        ValueText.Width = textlen + 20

        ValueText.Top = (Height - 23) / 2 - 2
        ValueText.Height = 23
    End Sub
End Class
