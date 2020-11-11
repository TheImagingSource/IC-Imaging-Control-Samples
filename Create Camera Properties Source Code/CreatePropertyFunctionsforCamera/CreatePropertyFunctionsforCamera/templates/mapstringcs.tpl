    /// <summary>
    /// Check, whether #name# is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
    public static bool #name#_Avaialble(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name# : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_MapStrings);

        if (Property != null)
            return true;
        else
            return false;

    }

    /// <summary>
    /// Returns, whether #name# is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool #name#_Readonly(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception("#name# Property is not supported by current device.");

    }

    /// <summary>
    /// Get the current String value of #name#
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <param name="StringValue">New value.</param>

    public static System.String #name#(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.String;
        }
        else
            throw new System.Exception("#name# Property is not supported by current device.");

    }

    /// <summary>
    /// Set a new String value to #name#
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>Current string</returns>
    public static void #name#(ICImagingControl ic, System.String StringValue)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            bool ok = false;
            string AllowedValues = "";
            for( int i = 0; i < Property.Strings.Length && !ok; i++)
            {
                AllowedValues += " \"" + Property.Strings[i] + "\"";
                ok = (StringValue == Property.Strings[i]);
            }
            if( !ok)
                throw new System.Exception(System.String.Format("#name# Property: Value \"{0}\" is not in list of {1}.", StringValue, AllowedValues));
            Property.String = StringValue;
        }
        else
            throw new System.Exception("#name# Property is not supported by current device.");

    }

    /// <summary>
    /// Returns a String array with the list of avaialble Strings of #name#
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>String []</returns>
    public static string[] #name#_GetStrings(ICImagingControl ic )
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface(new Guid("#itemid#"), new Guid("#elementid#"), VCDGUIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.Strings;
        }
        else
            throw new System.Exception("#name# Property is not supported by current device.");

    }

