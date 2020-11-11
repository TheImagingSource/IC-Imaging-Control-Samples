	
	''' <summary>
    ''' Check, whether #name# is available for current device.
    ''' </summary>
    ''' <param name="ic">>IC Imaging Control object</param>
    ''' <Returns>true : Available, false: not available</Returns>
	Public Function #name#_Available(ic As TIS.Imaging.ICImagingControl  ) As Boolean
		Dim DevProperty As VCDMapStringsProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_MapStrings)

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
		Dim DevProperty As VCDMapStringsProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_MapStrings)

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
	Public Sub  #name#(ic As TIS.Imaging.ICImagingControl,  Value as String)
		Dim DevProperty As VCDMapStringsProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_MapStrings)

		If Not DevProperty Is Nothing Then
			If DevProperty.ReadOnly Then
                Throw new System.Exception("#name# DevProperty is read only.")
			Else
                Dim AllowedString As String = ""
                Dim StringList As String()
                Dim i As Integer
                Dim ValueOK As Boolean = False

                StringList = DevProperty.Strings
                For i = 0 To StringList.Length - 1
                    AllowedString = AllowedString + StringList(i) + ", "
                    If Value = StringList(i) Then
                        ValueOK = True
                    End If
                Next

                If ValueOK Then
					DevProperty.String = Value
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
	Public Function #name#(ic As TIS.Imaging.ICImagingControl ) As String
		Dim DevProperty As VCDMapStringsProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_MapStrings)

		If Not DevProperty Is Nothing Then
			Return DevProperty.String
		Else
            Throw new System.Exception("#name# Property is not supported by current device.")
		End If
		Return ""
	End Function

	''' <summary>
    ''' Get string list of #name#.
    ''' </summary>
	''' <param name="ic">IC Imaging Control object</param>
    ''' <Returns>String list of #name#</Returns>
		Public Function #name#_GetStrings(ic As TIS.Imaging.ICImagingControl ) As String()
			Dim DevProperty As VCDMapStringsProperty 
		If Not ic.DeviceValid Then
			Throw new System.Exception("#name# : No device selected")
		End If 

		DevProperty = ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_MapStrings)

		If Not DevProperty Is Nothing Then
			Return DevProperty.Strings
		Else
            Throw new System.Exception("#name# Property is not supported by current device.")
		End If
		Return Nothing
	End Function
