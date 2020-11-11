	
	/// <summary>
    /// Check, whether #name# is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool #name#_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_AbsoluteValue);

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
    public static bool #name#_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_AbsoluteValue);

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
	public static void #name#_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_AbsoluteValue);

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
	public static double #name#_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_AbsoluteValue);

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
    /// <returns>Minimum value of #name#</returns>
	public static double #name#_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_AbsoluteValue);

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
	public static double #name#_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("#name# Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get #name# maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of #name#</returns>
	public static double #name#_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("#name# Property is not supported by current device.");
		
	}
