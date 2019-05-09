// Include this file into your source code:
// Example:
// #include "icproperties.h"
//
// Example for setting a property ("Hue"):
/*
		try
		{
			ICProperties::Gain_Auto(&m_Grabber, false);
			ICProperties::Exposure_Auto(&m_Grabber, false);
			ICProperties::GainAbs(&m_Grabber, 0.0);
			double e = ICProperties::ExposureAbs(&m_Grabber);
			ICProperties::ExposureAbs(&m_Grabber,0.01);
			ICProperties::WhiteBalance_Auto(&m_Grabber, false);
			ICProperties::WhiteBalance_Blue(&m_Grabber, 10);
			ICProperties::WhiteBalance_One_Push(&m_Grabber);

			std::vector<std::wstring> wbpresets;

			wbpresets = ICProperties::WhiteBalance_Auto_Preset_GetStringsW(&m_Grabber );
		}
		catch (ICPropertyException ICPropex)
		{
			MessageBoxA(NULL, ICPropex.what(), "Property Error", MB_OK);
		}
*/

#ifndef __ICPROPERTIES__
	#define __ICPROPERTIES__

#include <exception>
#include "tisudshl.h"
#include <vector>

// C# Implemention of camera properties 

class ICPropertyException : public std::runtime_error
{
public:
	ICPropertyException(std::string const& msg) :
		std::runtime_error(msg)
	{}
};

class ICProperties
{
public:
#code#
};

#endif //__ICPROPERTIES__