	
	/// <summary>
    /// Check, whether #name# is available for current device.
    /// </summary>
    /// <returns>true : Available, false: not available</returns>
	static bool #name#_Available(DShowLib::Grabber* pGrabber )
	{
		bool bResult = false;
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDButtonPropertyPtr   pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
			bResult = true;
		}
	
		return bResult;
	}

	/// <summary>
    /// Returns, whether #name# is readonly.
    /// </summary>
    static bool #name#_Readonly(DShowLib::Grabber* pGrabber )
    {
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDButtonPropertyPtr   pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		  pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

        if (pProperty != NULL)
        {
            return pProperty->getReadOnly();
        }
        else
            throw ICPropertyException(" #name# Property is not supported by current device.");
    }

    /// <summary>
    /// Get #name# value.
    /// </summary>
    /// <returns>Current value of #name#</returns>
	static void #name#(DShowLib::Grabber* pGrabber )
	{
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDButtonPropertyPtr   pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		  pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
			return pProperty->push();
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
		
	}
