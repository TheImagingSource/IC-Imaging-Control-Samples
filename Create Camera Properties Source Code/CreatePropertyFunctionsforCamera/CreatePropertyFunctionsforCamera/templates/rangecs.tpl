	
	/// <summary>
    /// Check, whether #name# is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool #name#_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether #name# is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool #name#_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" #name# Property is not supported by current device.");
    }

	/// <summary>
	/// Set #name# value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void #name#(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("#name# Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "#name# : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("#name# Property is not supported by current device.");
	}

    /// <summary>
    /// Get #name# value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of #name#</returns>
	public static int #name#(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("#name# Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get #name# default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of #name#</returns>
	public static int #name#_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("#name# Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get #name# minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of #name#</returns>
	public static int #name#_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("#name# Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get #name# maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of #name#</returns>
	public static int #name#_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("#name# Property is not supported by current device.");
		
	}
