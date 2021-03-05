	
	/// <summary>
    /// Check, whether #name# is available for current device.
    /// </summary>
    /// <returns>true : Available, false: not available</returns>
	static bool #name#_Abs_Available(DShowLib::Grabber* pGrabber )
	{
		bool bResult = false;
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDAbsoluteValuePropertyPtr  pProperty;
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
    static bool #name#_Abs_Readonly(DShowLib::Grabber* pGrabber )
    {
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDAbsoluteValuePropertyPtr  pProperty;
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
	/// Set #name# value.
	/// </summary>
	/// <param name="Value">Value to set</param>
	static void #name#_Abs(DShowLib::Grabber* pGrabber , double Value )
	{
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDAbsoluteValuePropertyPtr  pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
		     if( pProperty->getReadOnly())
                throw ICPropertyException("#name# Property is read only.");

            if (pProperty->getRangeMin() <= Value && pProperty->getRangeMax() >= Value)
                pProperty->setValue(Value);
            else
			{
				char szError[400];
				sprintf_s(szError,399,"#name# : Value %f is not in range %f - %f.", Value, pProperty->getRangeMin(), pProperty->getRangeMax() );
                throw ICPropertyException(szError);
			}
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
	}

    /// <summary>
    /// Get #name# value.
    /// </summary>
    /// <returns>Current value of #name#</returns>
	static double #name#_Abs(DShowLib::Grabber* pGrabber )
	{
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDAbsoluteValuePropertyPtr  pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
			return pProperty->getValue();
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
		
	}

		/// <summary>
    /// Get #name# default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of #name#</returns>
	static double #name#_Abs_Default(DShowLib::Grabber* pGrabber  )
	{
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDAbsoluteValuePropertyPtr  pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
			return pProperty->getDefault();
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get #name# minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of #name#</returns>
	static double #name#_Abs_Min(DShowLib::Grabber* pGrabber  )
	{
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDAbsoluteValuePropertyPtr  pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
			return pProperty->getRangeMin();
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get #name# maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of #name#</returns>
	static double #name#_Abs_Max(DShowLib::Grabber* pGrabber  )
	{
		if( !pGrabber->isDevValid())
		throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDAbsoluteValuePropertyPtr  pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);


		if( pProperty != NULL )
		{
			return pProperty->getRangeMax();
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
		
	}
