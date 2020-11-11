	
	/// <summary>
    /// Check, whether #name# is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool #name#_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Switch);

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
            throw new System.Exception("#name# : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Switch);

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
	/// <param name="OnOff">Value to set</param>
	public static void #name#(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("#name# Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("#name# Property is not supported by current device.");
	}

    /// <summary>
    /// Get #name# value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of #name#</returns>
	public static bool #name#(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("#name# : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("#name# Property is not supported by current device.");
	}
