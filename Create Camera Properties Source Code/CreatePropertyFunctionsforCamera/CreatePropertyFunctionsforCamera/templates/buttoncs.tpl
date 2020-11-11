	
	/// <summary>
    /// Check, whether #name# is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool #name#_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Button);

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

        VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Button);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" #name# Property is not supported by current device.");
    }

    /// <summary>
    /// Push #name#.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of #name#</returns>
	public static void #name#(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Button);

		if( Property != null )
		{
			Property.Push();
		}
		else
            throw new System.Exception("#name# Property is not supported by current device.");
	}
