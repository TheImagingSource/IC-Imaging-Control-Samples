# Lens Control

This sample code shows how to implement Auto Focus One Push, Focus and Zoom functions.

## Focus
### Focus value
The focus is a simple range property. That means it is an interger value, which can be got and set. The get and set functions
are:

C++
```C++
/////////////////////////////////////////////////////////////
// Get focus value
long Get_Focus_Value(Grabber* pGrabber)
{
	if( pGrabber->isDevValid() )
	{
		tIVCDRangePropertyPtr pRangeItf;
		tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(VCDID_Focus, VCDElement_Value ,pRangeItf);

		if( pRangeItf != NULL )
		{
			return pRangeItf->getValue();
		}
	}

	return 0;
}

/////////////////////////////////////////////////////////////
// Set focus value
void Set_Focus_Value_(Grabber* pGrabber, long Value)
{
	if( pGrabber->isDevValid() )
	{
		tIVCDRangePropertyPtr pRangeItf;
		tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(VCDID_Focus, VCDElement_Value ,pRangeItf);

		if( pRangeItf != NULL )
		{
			pRangeItf->setValue(Value);
		}
	}
}
```
C#
```C#
int GetFocusValueValue(TIS.Imaging.ICImagingControl ic)
{
	if( ic.DeviceValid  == true )
	{
		TIS.Imaging.VCDRangeProperty Property;
		Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Focus, TIS.Imaging.VCDIDs.VCDElement_Value, TIS.Imaging.VCDIDs.VCDInterface_Range);
		if (Property != null)
		{
			return Property.Value;
		}
	}

	return 0;
}

void SetFocusValueValue(TIS.Imaging.ICImagingControl ic, int Value)
{
	if( ic.DeviceValid  == true )
	{
		TIS.Imaging.VCDRangeProperty Property;
		Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Focus, TIS.Imaging.VCDIDs.VCDElement_Value, TIS.Imaging.VCDIDs.VCDInterface_Range);
		if (Property != null)
		{
			Property.Value = Value;
		}
	}
}

```

The range of the focus property uses follwing functions:

C++
```C++
long Get_Focus_Value_Min(Grabber* pGrabber)
{
	if( pGrabber->isDevValid() )
	{
		tIVCDRangePropertyPtr pRangeItf;
		tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(VCDID_Focus, VCDElement_Value ,pRangeItf);

		if( pRangeItf != NULL )
		{
			return pRangeItf->getRangeMin();
		}
	}

	return 0;
}

long Get_Focus_Value_Max(Grabber* pGrabber)
{
	if( pGrabber->isDevValid() )
	{
		tIVCDRangePropertyPtr pRangeItf;
		tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(VCDID_Focus, VCDElement_Value ,pRangeItf);

		if( pRangeItf != NULL )
		{
			return pRangeItf->getRangeMax();
		}
	}

	return 0;
}
```

C#
```C#
int GetFocusValueMin(TIS.Imaging.ICImagingControl ic)
{
	if( ic.DeviceValid  == true )
	{
		TIS.Imaging.VCDRangeProperty Property;
		Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Focus, TIS.Imaging.VCDIDs.VCDElement_Value, TIS.Imaging.VCDIDs.VCDInterface_Range);
		if (Property != null)
		{
			return Property.RangeMin;
		}
	}

	return 0;
}

int GetFocusValueMax(TIS.Imaging.ICImagingControl ic)
{
	if( ic.DeviceValid  == true )
	{
		TIS.Imaging.VCDRangeProperty Property;
		Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Focus, TIS.Imaging.VCDIDs.VCDElement_Value, TIS.Imaging.VCDIDs.VCDInterface_Range);
		if (Property != null)
		{
			return Property.RangeMax;
		}
	}
	return 0;
}
```

The range functions are useful in case sliders are programmed. Usually they should also be used for checking whether new focus values are within focus range. 

The usage is as follows:

C++
```C++
#include <tisudshl.h>
DShowLib::InitLibrary();

atexit( ExitLibrary );
long CurrentFocus = 0;

Grabber grabber;

grabber.openDev("DFK Z30GP031");
// Get current focus value.
CurrentFocus = Get_Focus_Value(&grabber);

// Create a new focus value.
CurrentFocus += 10;

// Set the new focus value.
Set_Focus_Value(&grabber, CurrentFocus);
```

C#
```C#
int CurrentFocus;
IcImagingControl1.Device = "DFK Z30GP031";

// Get current focus value.
CurrentFocus = GetFocusValueValue(IcImagingControl1);

// Create a new focus value.
CurrentFocus += 10;

// Set the new focus value.
SetFocusValueValue(IcImagingControl1,CurrentFocus);
```

### Auto Focus One Push
The The Imaging Source cameras have no permanent auto focus, because there is no extra hardware for distance measurement inside the cameras. The auto focus estimates the sharpness of an image using edges in the image and tries different focus values until a sharp image is achieved.
Therefore, there is the auto focus on demand only, the so called "one push". The function for auto focus one push is:

C++
```C++
void Push_Focus_One_Push(Grabber* pGrabber)
{
	if( pGrabber->isDevValid() )
	{
		tIVCDButtonPropertyPtr pButtonItf;
		tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(VCDID_Focus, VCDElement_OnePush ,pButtonItf);

		if( pButtonItf != NULL )
		{
			pButtonItf->push();
		}
	}
}
```

C#
```C#
void PushFocusOne_Push(TIS.Imaging.ICImagingControl ic)
{
	if( ic.DeviceValid  == true )
	{
		TIS.Imaging.VCDButtonProperty Property;
		Property = (TIS.Imaging.VCDButtonProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Focus, TIS.Imaging.VCDIDs.VCDElement_OnePush, TIS.Imaging.VCDIDs.VCDInterface_Button);
		if (Property != null)
		{
			Property.Push();
		}
	}
}
```
Some cameras provide the "One Push Running" property, which returns ```true``` while the auto focus algorthim runs.

C++
```C++
bool Get_Focus_One_Push_Running(Grabber* pGrabber)
{
	if( pGrabber->isDevValid() )
	{
		tIVCDSwitchPropertyPtr pSwitchItf;
		tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(VCDID_Focus, StringToGUID(L"{07D2DD39-3F10-4E0F-8EE5-3EED067A53C6}") ,pSwitchItf);

		if( pSwitchItf != NULL )
		{
			return pSwitchItf->getSwitch();
		}
	}

	return false;
}
```

C#
```C#
private bool Get_Focus_One_Push_Running_Switch(TIS.Imaging.ICImagingControl IC )
{
	if( IC.DeviceValid)
	{
		TIS.Imaging.VCDSwitchProperty SwitchItf;
		SwitchItf = (TIS.Imaging.VCDSwitchProperty)IC.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Focus, 
													"{07D2DD39-3F10-4E0F-8EE5-3EED067A53C6}",
													TIS.Imaging.VCDIDs.VCDInterface_Switch);
		if( SwitchItf != null )
		{
			return SwitchItf.Switch;
		}
	}
	return false;
}
```

Usage is:

C++
```C++
#include <tisudshl.h>
DShowLib::InitLibrary();

atexit( ExitLibrary );
long CurrentFocus = 0;

Grabber grabber;

grabber.openDev("DFK Z30GP031");
// Live video must run, otherwise there are no images for
// auto focus.
grabber.startLive();

// Start the auto focus algorithm.
Push_Focus_One_Push(&grabber);

// Wait until it has finished.
while( Get_Focus_One_Push_Running(&grabber) )
{
    Sleep(50);
}
```

C#
```C#
int CurrentFocus;
IcImagingControl1.Device = "DFK Z30GP031";

// Live video must run, otherwise there are no images for
// auto focus.
IcImagingControl1.LiveStart();

// Start the auto focus algorithm.
PushFocusOne_Push(IcImagingControl1);

// Wait until it has finished.
while( Get_Focus_One_Push_Running_Switch(IcImagingControl1) )
{
    System.Threading.Thread.Sleep(50);
}
```

The time needed for auto focus depends on the frame rate and the complexity of the scene. Also it matters, whether the image was already sharp or not.


## Zoom
As the Focus property, the Zoom property is an integer range property. The functions for get and set are:

C++
```C++
long Get_Zoom_Value(Grabber* pGrabber)
{
	if( pGrabber->isDevValid() )
	{
		tIVCDRangePropertyPtr pRangeItf;
		tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(VCDID_Zoom, VCDElement_Value ,pRangeItf);

		if( pRangeItf != NULL )
		{
			return pRangeItf->getValue();
		}
	}

	return 0;
}

void Set_Zoom_Value(Grabber* pGrabber, long Value)
{
	if( pGrabber->isDevValid() )
	{
		tIVCDRangePropertyPtr pRangeItf;
		tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(VCDID_Zoom, VCDElement_Value ,pRangeItf);

		if( pRangeItf != NULL )
		{
			pRangeItf->setValue(Value);
		}
	}
}
```

C#
```C#
int GetZoomValueValue(TIS.Imaging.ICImagingControl ic)
{
	if( ic.DeviceValid  == true )
	{
		TIS.Imaging.VCDRangeProperty Property;
		Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Zoom, TIS.Imaging.VCDIDs.VCDElement_Value, TIS.Imaging.VCDIDs.VCDInterface_Range);
		if (Property != null)
		{
			return Property.Value;
		}
	}

	return 0;
}

void SetZoomValueValue(TIS.Imaging.ICImagingControl ic, int Value)
{
	if( ic.DeviceValid  == true )
	{
		TIS.Imaging.VCDRangeProperty Property;
		Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Zoom, TIS.Imaging.VCDIDs.VCDElement_Value, TIS.Imaging.VCDIDs.VCDInterface_Range);
		if (Property != null)
		{
			Property.Value = Value;
		}
	}
}
```

The range functions for min and max zoom are:

C++
```C++
long Get_Zoom_Value_Min(Grabber* pGrabber)
{
	if( pGrabber->isDevValid() )
	{
		tIVCDRangePropertyPtr pRangeItf;
		tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(VCDID_Zoom, VCDElement_Value ,pRangeItf);

		if( pRangeItf != NULL )
		{
			return pRangeItf->getRangeMin();
		}
	}

	return 0;
}

long Get_Zoom_Value_Max(Grabber* pGrabber)
{
	if( pGrabber->isDevValid() )
	{
		tIVCDRangePropertyPtr pRangeItf;
		tIVCDPropertyItemsPtr pItems = pGrabber->getAvailableVCDProperties();
		pItems->findInterfacePtr(VCDID_Zoom, VCDElement_Value ,pRangeItf);

		if( pRangeItf != NULL )
		{
			return pRangeItf->getRangeMax();
		}
	}

	return 0;
}
```

C#
```C#
int GetZoomValueMin(TIS.Imaging.ICImagingControl ic)
{
	if( ic.DeviceValid  == true )
	{
		TIS.Imaging.VCDRangeProperty Property;
		Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Zoom, TIS.Imaging.VCDIDs.VCDElement_Value, TIS.Imaging.VCDIDs.VCDInterface_Range);
		if (Property != null)
		{
			return Property.RangeMin;
		}
	}

	return 0;
}

int GetZoomValueMax(TIS.Imaging.ICImagingControl ic)
{
	if( ic.DeviceValid  == true )
	{
		TIS.Imaging.VCDRangeProperty Property;
		Property = (TIS.Imaging.VCDRangeProperty)ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Zoom, TIS.Imaging.VCDIDs.VCDElement_Value, TIS.Imaging.VCDIDs.VCDInterface_Range);
		if (Property != null)
		{
			return Property.RangeMax;
		}
	}
	return 0;
}
```

The usage is

C++
```C++
#include <tisudshl.h>
DShowLib::InitLibrary();

atexit( ExitLibrary );
long CurrentZoom = 0;

Grabber grabber;

grabber.openDev("DFK Z30GP031");
// Get current zoom value.
CurrentZoom = Get_Zoom_Value(&grabber);

// Create a new focus value.
CurrentZoom += 10;

// Set the new Zooms value.
Set_Zoom_Value(&grabber, CurrentZoom);
```

C#
```C#
int CurrentZoom;
IcImagingControl1.Device = "DFK Z30GP031";

// Get current zoom value.
CurrentZoom = GetZoomValueValue(IcImagingControl1);

// Create a new zoom value.
CurrentZoom += 10;

// Set the new zoom value.
SetZoomValueValue(IcImagingControl1,CurrentFocus);
```

