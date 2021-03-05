	
	/// <summary>
    /// Check, whether #name# is available for current device.
    /// </summary>
    /// <returns>true : Available, false: not available</returns>
	static bool #name#_Available(DShowLib::Grabber* pGrabber )
	{
		bool bResult = false;
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDMapStringsPropertyPtr   pProperty;
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

		DShowLib::tIVCDMapStringsPropertyPtr   pProperty;
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
	static void #name#(DShowLib::Grabber* pGrabber, std::string Value )
	{
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDMapStringsPropertyPtr   pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
			if( pProperty->getReadOnly())
                throw ICPropertyException("#name# Property is read only.");
			else
			{
				std::string ErrorMessage = "#name# : String \""+ Value + "\" is not in the list of available values: \"";
				bool bStringAllowed = false; 
				std::vector<std::string> StringList = pProperty->getStrings();
				for (unsigned int i = 0; i < StringList.size() && !bStringAllowed; i++)
				{
					if (i > 0)
						ErrorMessage += ", ";
					ErrorMessage += StringList[i] + " " ;

					if( Value == StringList[i])
						bStringAllowed = true;
				}
				if (bStringAllowed)
					pProperty->setString(Value);
				else
				{
					ErrorMessage += "\".";
					throw ICPropertyException(ErrorMessage);
				}
			}
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
	}

		/// <summary>
	/// Set #name# value.
	/// </summary>
	/// <param name="Value">Value to set</param>
	static void #name#(DShowLib::Grabber* pGrabber, std::wstring Value )
	{
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDMapStringsPropertyPtr   pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
			if( pProperty->getReadOnly())
                throw ICPropertyException("#name# Property is read only.");
			else
			{
				std::wstring ErrorMessage = L"#name# : Tralala String \"" + Value;
				ErrorMessage += L"\" is not in the list of available values: \"";

				bool bStringAllowed = false;
				std::vector<std::wstring> StringList = pProperty->getStringsW();
				for (unsigned int i = 0; i < StringList.size() && !bStringAllowed; i++)
				{
					if (i > 0)
						ErrorMessage += L", ";
					ErrorMessage += StringList[i] + L" ";

					if (Value == StringList[i])
						bStringAllowed = true;
				}
				if (bStringAllowed)
					pProperty->setString(Value);
				else
				{
					ErrorMessage += L"\".";
					std::string s(DShowLib::wstoas(ErrorMessage.c_str()));

					throw ICPropertyException(s);
				}
			}
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
	}



    /// <summary>
    /// Get #name# value.
    /// </summary>
    /// <returns>Current value of #name#</returns>
	static  std::string #name#(DShowLib::Grabber* pGrabber)
	{
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDMapStringsPropertyPtr   pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
			return pProperty->getString();
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
		
	}
    /// <summary>
    /// Get #name# value.
    /// </summary>
    /// <returns>Current value of #name#</returns>
	static  std::wstring #name#W(DShowLib::Grabber* pGrabber )
	{
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDMapStringsPropertyPtr   pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
			return pProperty->getStringW();
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get #name# value.
    /// </summary>
    /// <returns>Current value of #name#</returns>
	static  std::vector<std::string> #name#_GetStrings(DShowLib::Grabber* pGrabber  )
	{
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDMapStringsPropertyPtr   pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
			return pProperty->getStrings(); //tStringVec(Stringlist);
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get #name# value.
    /// </summary>
    /// <returns>Current value of #name#</returns>
	static  std::vector<std::wstring> #name#_GetStringsW(DShowLib::Grabber* pGrabber )
	{
		if( !pGrabber->isDevValid())
			throw ICPropertyException("#name# : No device selected");

		DShowLib::tIVCDMapStringsPropertyPtr   pProperty;
		DShowLib::tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(DShowLib::StringToGUID(L"{#itemid#}"), DShowLib::StringToGUID(L"{#elementid#}"),pProperty);

		if( pProperty != NULL )
		{
			return pProperty->getStringsW(); //tStringVecW(Stringlist);
		}
		else
            throw ICPropertyException("#name# Property is not supported by current device.");
		
	}
