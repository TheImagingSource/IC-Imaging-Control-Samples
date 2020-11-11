	
	''' <summary>
    ''' Check, whether #name# is available for current device.
    ''' </summary>
    ''' <param name="ic">>IC Imaging Control object</param>
    ''' <Returns>true : Available, false: not available</Returns>
	Public Function #name#_Available(ic As TIS.Imaging.ICImagingControl  ) As Boolean
		Dim DevProperty As VCDRangeProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range)

		If Not DevProperty Is Nothing Then
			Return True
		Else
			Return False
		End If
	End Function


	''' <summary>
    ''' Returns, whether #name# is readonly.
    ''' </summary>
    ''' <param name="ic">>IC Imaging Control object</param>
    ''' <Returns>true: DevProperty is ready only, false: DevProperty is writeable</Returns>
    Public Function #name#_Readonly(ic As TIS.Imaging.ICImagingControl) As Boolean
		Dim DevProperty As VCDRangeProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range)

		If Not DevProperty Is Nothing Then
            Return DevProperty.ReadOnly
        Else
            Throw new System.Exception(" #name# Property is not supported by current device.")
		End If
		Return False
	End Function

	''' <summary>
	''' Set #name# value.
	''' </summary>
	''' <param name="ic">IC Imaging Control object</param>
	''' <param name="Value">Value to set</param>
	Public Sub  #name#(ic As TIS.Imaging.ICImagingControl,  Value as Integer)
		Dim DevProperty As VCDRangeProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range)

		If Not DevProperty Is Nothing Then
			If DevProperty.ReadOnly Then
                Throw new System.Exception("#name# DevProperty is read only.")
			Else
				If DevProperty.RangeMin <= Value And DevProperty.RangeMax >= Value Then 
					DevProperty.Value = Value
				Else
					Throw new System.Exception(System.String.Format( "#name# : Value {0} is not in range {1} - {2}.", Value, DevProperty.RangeMin, DevProperty.RangeMax ))
				End If
			End If
		Else
            Throw new System.Exception("#name# Property is not supported by current device.")
		End If
	End Sub

    ''' <summary>
    ''' Get #name# value.
    ''' </summary>
	''' <param name="ic">IC Imaging Control object</param>
    ''' <Returns>Current value of #name#</Returns>
	Public Function #name#(ic As TIS.Imaging.ICImagingControl ) As Integer
		Dim DevProperty As VCDRangeProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range)

		If Not DevProperty Is Nothing Then
			Return DevProperty.Value
		Else
            Throw new System.Exception("#name# Property is not supported by current device.")
		End If
		Return 0
	End Function

	''' <summary>
    ''' Get #name# default value.
    ''' </summary>
	''' <param name="ic">IC Imaging Control object</param>
    ''' <Returns>Default value of #name#</Returns>
	Public Function #name#_Default(ic As TIS.Imaging.ICImagingControl ) As Integer
		Dim DevProperty As VCDRangeProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range)

		If Not DevProperty Is Nothing Then
			Return DevProperty.Default
		Else
            Throw new System.Exception("#name# Property is not supported by current device.")
		End If
		Return 0
	End Function

		''' <summary>
    ''' Get #name# minimum value.
    ''' </summary>
	''' <param name="ic">IC Imaging Control object</param>
    ''' <Returns>Minimum value of #name#</Returns>
	Public Function #name#_Min(ic As TIS.Imaging.ICImagingControl ) As Integer
		Dim DevProperty As VCDRangeProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range)

		If Not DevProperty Is Nothing Then
			Return DevProperty.RangeMin
		Else
            Throw new System.Exception("#name# Property is not supported by current device.")
		End If
		Return 0
	End Function



	''' <summary>
    ''' Get #name# maximum value.
    ''' </summary>
	''' <param name="ic">IC Imaging Control object</param>
    ''' <Returns>Maximum value of #name#</Returns>
	Public Function #name#_Max(ic As TIS.Imaging.ICImagingControl ) As Integer
		Dim DevProperty As VCDRangeProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range)

		If Not DevProperty Is Nothing Then
			Return DevProperty.RangeMax
		Else
            Throw new System.Exception("#name# Property is not supported by current device.")
		End If
		Return 0
	End Function