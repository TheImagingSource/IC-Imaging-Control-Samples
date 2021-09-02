#ifndef _TISGRABBER
#define _TISGRABBER

//////////////////////////////////////////////////////////////////////////
/*! @mainpage
	Please see "tisgrabber.h" for the function documentation.

	The functions of the library are documented under "Modules".

	A simple sample for live video:
@code
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include "..\tisgrabber.h"

if( IC_InitLibrary(0) )
{
	HGRABBER hGrabber; // The handle of the grabber object.

	hGrabber = IC_ShowDeviceSelectionDialog(NULL);
	if( hGrabber )
	{
		IC_StartLive(hGrabber,1);
		printf("Press any key to stop the live video\n" );
		_getch();
		IC_StopLive(hGrabber);
		IC_ReleaseGrabber(&hGrabber);
	}
}
@endcode

Open a camera, set video format and frame rate and set some properties:

@code
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include "..\tisgrabber.h"

if( IC_InitLibrary(0) )
{
	HGRABBER hGrabber = IC_CreateGrabber();

	IC_OpenVideoCaptureDevice(hGrabber,"DFK 33UX290");

	if (IC_IsDevValid(hGrabber))
	{
		IC_SetVideoFormat(hGrabber, "RGB25 (640x480)");
		IC_SetFrameRate(hGrabber,30.0f);

		IC_StartLive(hGrabber,1);

		// Reset all properties to "Factory" default.
		IC_ResetProperties(hGrabber);

		// Disable gain automatic
		IC_SetPropertySwitch(hGrabber, "Gain", "Auto", 0);
		// Set a gain value
		IC_SetPropertyValue(hGrabber, "Gain", "Value", 16);

		// Disable Exposure automatic
		IC_SetPropertySwitch(hGrabber, "Exposure", "Auto", 0);
		// Set an abslute exposure value in seconds uning
		IC_SetPropertyAbsoluteValue(hGrabber, "Exposure", "Value", 0.0303f);

		// Disable Whitebalance automatic
		IC_SetPropertySwitch(hGrabber, "WhiteBalance", "Auto", 0);

		// Set a white balance values
		IC_SetPropertyValue(hGrabber, "WhiteBalance", "White Balance Red", 64);
		IC_SetPropertyValue(hGrabber, "WhiteBalance", "White Balance Green", 128);
		IC_SetPropertyValue(hGrabber, "WhiteBalance", "White Balance Red", 32);

		printf("Press any key to stop the live video\n" );
		_getch();
		IC_StopLive(hGrabber);
		IC_ReleaseGrabber(&hGrabber);
	}
}
@endcode

*/

#include "TISGrabberGlobalDefs.h"

/**
 * \defgroup START Definitions and Constants
 * @{
 */

// WINAPI for Visual Basic
//#define AC WINAPI

//////////////////////////////////////////////////////////////////////////
/*! Calling convention of the DLL functions. For internal use only. 
*/
#define AC __stdcall ///< __stdcall for Borland C and Ansi C
//#define AC __cdecl ///< mingw


#ifndef _WINUSER_
//////////////////////////////////////////////////////////////////////////
/*! Workaround if we are in non windows environment.
*/
#define NOHWNDDEFINED 1
#define __HWND int	///< Workaround if we are in non windows environment.
#else
#define __HWND HWND ///<Workaround if we are in windows environment.
#endif

//////////////////////////////////////////////////////////////////////////
/*! This is the handle of an grabber object. Please use the HGRABBER type to access
	this object.
*/
typedef struct HGRABBER_t__ { int unused; } HGRABBER_t; ///<Internal structure of the grabber object handle.

//////////////////////////////////////////////////////////////////////////
/*! The HGRABBER type is used to hold a handle to a grabber object. Each variable of
	HGRABBER type can contain one video capture device. It is possible to create more
	than one variables of this type:
	HGRABBER camera1 = IC_CreateGrabber();
	HGRABBER camera2 = IC_CreateGrabber();
*/
#define	HGRABBER HGRABBER_t* ///< Type of grabber object handle. Used for all functions. 

struct ImageBuffer;
typedef struct ImageBuffer *HMEMBUFFER;


typedef int  _cdecl IC_ENUMCB( char* Name, void*);

/*! Structure for frame filter prameters.
*/
typedef struct FILTERPARAMETER_t__
{
	char Name[30];
	FRAMEFILTER_PARAM_TYPE Type;
}	FILTERPARAMETER_t;

/*! Structure for frame filter handles. 
*/
typedef struct HFRAMEFILTER_t__
{
	void *pFilter;
	int bHasDialog;
	int ParameterCount;
	FILTERPARAMETER_t *Parameters;
} HFRAMEFILTER_t;
/*! Structure for frame filter handle.
*/
#define HFRAMEFILTER HFRAMEFILTER_t
/* @} */
/**
 * \defgroup IntVariables Global integer variables
 * @{
 */

//////////////////////////////////////////////////////////////////////////
/*! A return value of IC_SUCCESS indicates that a function has been performed
    without an error.
*/
#define IC_SUCCESS   1		///< Return value for success.
//////////////////////////////////////////////////////////////////////////
/*! If a function returns IC_ERROR, then something went wrong.
*/
#define IC_ERROR     0		///< Return value that indicates an error.
//////////////////////////////////////////////////////////////////////////
/*! This error indicates, that an HGRABBER handle has not been created yet. Please
	see IC_CreateGrabber() for creating an HGRABBER handle. 
*/
#define IC_NO_HANDLE -1		///< No device handle. HGRABBER is NULL.
//////////////////////////////////////////////////////////////////////////
/*! This return values indicates that no device has been opened. Please refer to
    IC_OpenVideoCaptureDevice().
*/
#define IC_NO_DEVICE -2     ///< No device opened, but HGRABBER is valid.
//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that the video capture device is not in live mode,
    but live mode is for the current function call required. Please refer to
	IC_StartLive().
*/

#define IC_NOT_AVAILABLE -3     ///< Property not avaiable, but HGRABBER is valid.
//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that the video capture device does not support
	the specified property.
*/

#define IC_NO_PROPERTYSET -3     ///< The Propertyset was not queried.
//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that the porperty set was not queried for
	the current grabber handle. Please check, whether IC_QueryPropertySet() 
	was called once before using the function.
*/

#define IC_DEFAULT_WINDOW_SIZE_SET -3     ///< The live display window size could not be set
//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that setting of a custom live display window size
	failed, because IC_SetDefaultWindowPosition() was not called with parameter false
	somewhere before.
	@sa IC_SetDefaultWindowPosition
	@sa IC_SetWindowPosition
*/
#define IC_NOT_IN_LIVEMODE -3 ///< A device has been opened, but is is not in live mode.

//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that a device does not support the requested property, or
	the name of a property was written in wrong way.

	@sa IC_GetPropertyValueRange
*/
#define IC_PROPERTY_ITEM_NOT_AVAILABLE -4 ///< A requested property item is not available

//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that a device does not support the requested element property, or
	the name of an element was written in wrong way.

	@sa IC_GetPropertyValueRange
*/
#define IC_PROPERTY_ELEMENT_NOT_AVAILABLE -5 ///< A requested element of a given property item is not available

//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that a property element does not support
    the request, that is wanted. e.g. Exposure Auto has no range, therefore
	IC_GetPropertyValueRange(hGrabber, "Epxosure","Auto", &min, &max )
	will return IC_PROPERTY_ELEMENT_WRONG_INTERFACE.

	@sa IC_GetPropertyValueRange
*/
#define IC_PROPERTY_ELEMENT_WRONG_INTERFACE -6 ///< A requested element has not the interface, which is needed.

//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that there was an index passed, which 
	was out of range of the number of available elements

	@sa IC_ListDevicesbyIndex
*/
#define IC_INDEX_OUT_OF_RANGE -7 ///< A requested element has not the interface, which is needed.


//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that that the passed XML file contains no valid XML
	data.

	@sa IC_LoadDeviceStateFromFileEx
*/
#define IC_WRONG_XML_FORMAT -1
//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that  the passed XML file contains no compatible XML
	data.

	@sa IC_LoadDeviceStateFromFileEx
*/
#define IC_WRONG_INCOMPATIBLE_XML -3
//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that  not all properties have been restored
	as desired, but the camera itself was opened.

	@sa IC_LoadDeviceStateFromFileEx
*/
#define IC_NOT_ALL_PROPERTIES_RESTORED -4

//////////////////////////////////////////////////////////////////////////
/*! This return value indicates, that the device specified in the XML was not
	found. E.g. The same model, but different serial number, or no camera 
	connected at all.

	@sa IC_LoadDeviceStateFromFileEx
*/
#define IC_DEVICE_NOT_FOUND -5

//////////////////////////////////////////////////////////////////////////
/*! \def This return value indicates, that the passed file does not exist

	@sa IC_LoadDeviceStateFromFileEx
*/
#define IC_FILE_NOT_FOUND 35

/*! @} */


#if defined(__cplusplus)
extern "C"
{
#endif

/**
 * \defgroup Functions
 * @{
 */


//////////////////////////////////////////////////////////////////////////
/*! \fn Initialize the ICImagingControl class library. This function must be called
	only once before any other functions of this library are called.
	@param szLicenseKey IC Imaging Control license key or NULL if only a trial version is available.
	@retval IC_SUCCESS on success.
	@retval IC_ERROR on wrong license key or other errors.
	@sa IC_CloseLibrary

*/
int AC IC_InitLibrary( char* szLicenseKey );///<Initialize the library.


//////////////////////////////////////////////////////////////////////////
/*! \fn Creates a new grabber handle and returns it. A new created grabber should be
	release with a call to IC_ReleaseGrabber if it is no longer needed.
	@retval IC_SUCCESS on success.
	@retval IC_ERROR if an error occurred.
	@sa IC_ReleaseGrabber
*/
HGRABBER AC IC_CreateGrabber();///<Create a new grabber handle



//////////////////////////////////////////////////////////////////////////
/*! \fn Release the grabber object. Must be called, if the calling application
    does no longer need the grabber.
	@param hGrabber The handle to grabber to be released.
	@sa IC_CreateGrabber
*/
void AC IC_ReleaseGrabber( HGRABBER *hGrabber ); ///< Release an HGRABBER object.


//////////////////////////////////////////////////////////////////////////
/*	Releases allocated memory. Currently it is the device list.
	Is called automatically by IC_CloseLibrary() too.
	@sa IC_CloseLibrary
*/
void AC IC_TidyUP();


//////////////////////////////////////////////////////////////////////////
/*	Must be called at the of the application to release allocated memory.
	@sa IC_InitLibrary
*/
void AC IC_CloseLibrary(); ///< Closes the library, cleans up memory. 

//////////////////////////////////////////////////////////////////////////
/*! Open a video capture device. The hGrabber handle must have been created previously by
	a call to IC_CreateGrabber(). Once a hGrabber handle has been created it can be
	recycled to open different video capture devices in sequence. 
	@param hGrabber The handle to grabber object, that has been created by a call to IC_CreateGrabber
	@param szDeviceName Friendly name of the video capture device e.g. "DFK 21F04".
	@retval IC_SUCCESS on success.
	@retval IC_ERROR on errors.
	@sa IC_CloseVideoCaptureDevice

	@code
	#include "tisgrabber.h"
	void main()
	{
		HGRABBER hGrabber;
		if( IC_InitLibrary(0) == IC_SUCCESS )
		{
			hGrabber = IC_CreateGrabber();
			if( hGrabber )
			{
				if( IC_OpenVideoCaptureDevice(hGrabber,"DFK 21F04") == IC_SUCCESS )
				{

				// .. do something with the video capture device.

				// Now clean up.
				IC_CloseVideoCaptureDevice( hGrabber );
				IC_ReleaseGrabber( hGrabber );
			}
			IC_CloseLibrary();
		}
	}
	@endcode
*/
int AC IC_OpenVideoCaptureDevice( HGRABBER hGrabber, char *szDeviceName ); ///< Opens a video capture device.


//////////////////////////////////////////////////////////////////////////
/*! Close the current video capture device. The HGRABBER object will not be deleted.
	It can be used again for opening another video capture device.
	@param hGrabber The handle to the grabber object.
*/
void AC IC_CloseVideoCaptureDevice( HGRABBER hGrabber ); ///<Closes a video capture device.

//////////////////////////////////////////////////////////////////////////
/*! Retrieve the name of the current video capture device. If the device is
	invalid, NULL is returned.
	@param hGrabber The handle to the grabber object.
	@retval char* The name of the video capture device
	@retval NULL  If no video capture device is currently opened.
*/
char* AC IC_GetDeviceName(HGRABBER hGrabber ); ///<Returns the name of the current video capture device.

int AC IC_GetVideoFormatWidth( HGRABBER hGrabber); ///<Returns the width of the video format.
int AC IC_GetVideoFormatHeight( HGRABBER hGrabber);///<returns the height of the video format.

//////////////////////////////////////////////////////////////////////////
/*! Set the sink type. A sink type must be set before images can be snapped.
	The sink type basically describes the format of the buffer where the snapped 
	images are stored. 

	Possible values for format are:
	@li Y800	
	@li RGB24
	@li RGB32
	@li UYVY
	@li RGB64

	The sink type may differ from the currently set video format.

	@param hGrabber The handle to the grabber object.
	@param format The desired color format. Possible values for format are:
		@li Y800	
		@li RGB24
		@li RGB32
		@li UYVY
	@retval IC_SUCCESS on success
	@retval IC_ERROR if something went wrong.

	@note Please note that UYVY can only be used in conjunction with a UYVY video format.


*/
int AC IC_SetFormat( HGRABBER hGrabber, COLORFORMAT format ); ///< Sets the color format of the sink. 


//////////////////////////////////////////////////////////////////////////
/*! Retrieves the format of the sink type currently set (See IC_SetFormat()
	for possible formats). If no sink type is set
	or an error occurred, NONE is returned.
	The function returns a valid value only after IC_PreprareLive() or IC_StartLive()
	was called. Before these calls, NONE is returned.
	@param hGrabber The handle to the grabber object.
	@return The current sink color format.
*/
COLORFORMAT AC IC_GetFormat( HGRABBER hGrabber ); ///<Returns the current color format of the sink.


//////////////////////////////////////////////////////////////////////////
/*! Set a video format for the current video capture device. The video format
    must be supported by the current video capture device.
	@param hGrabber The handle to the grabber object.
	@param szFormat A string that contains the desired video format.
	@retval IC_SUCCESS on success
	@retval IC_ERROR if something went wrong.

	@code
	#include "tisgrabber.h"
	void main()
	{
		HGRABBER hGrabber;
		if( IC_InitLibrary(0) == IC_SUCCESS )
		{
			hGrabber = IC_CreateGrabber();
			if( hGrabber )
			{
				if( IC_OpenVideoCaptureDevice(hGrabber,"DFK 21F04") == IC_SUCCESS )
				{
					if( IC_SetVideoFormat(hGrabber,"UYVY (640x480)" == IC_SUCCESS )
					{
						// .. do something with the video capture device.
					}
					// Now clean up.
					IC_CloseVideoCaptureDevice( hGrabber );
					IC_ReleaseGrabber( hGrabber );
				}
				IC_CloseLibrary();
			}
		}
	}
	@endcode
*/
int AC IC_SetVideoFormat( HGRABBER hGrabber, char *szFormat ); ///<Sets the video format.


//////////////////////////////////////////////////////////////////////////
/*! Set a video norm for the current video capture device.
	@note  The current video capture device must support video norms. 
	@param hGrabber The handle to the grabber object.
	@param szNorm A string that contains the desired video format.
	@retval IC_SUCCESS on success
	@retval IC_ERROR if something went wrong.
*/
int AC IC_SetVideoNorm( HGRABBER hGrabber, char *szNorm ); ///<Set the video norm.


//////////////////////////////////////////////////////////////////////////
/*! Set a input channel for the current video capture device. 
	@note  The current video capture device must support input channels.. 
	@param hGrabber The handle to the grabber object.
	@param szChannel A string that contains the desired video format.
	@retval IC_SUCCESS on success
	@retval IC_ERROR if something went wrong.
*/
int AC IC_SetInputChannel( HGRABBER hGrabber, char *szChannel ); ///<Sets an input channel.


//////////////////////////////////////////////////////////////////////////
/*! Start the live video. 
	@param hGrabber The handle to the grabber object.
	@param iShow The parameter indicates:   @li 1 : Show the video	@li 0 : Do not show the video, but deliver frames. (For callbacks etc.)
	@retval IC_SUCCESS on success
	@retval IC_ERROR if something went wrong.
	@sa IC_StopLive
*/
int AC IC_StartLive( HGRABBER hGrabber, int iShow ); ///<Starts the live video.
int AC IC_PrepareLive( HGRABBER hGrabber, int iShow); ///<Prepare the grabber for starting the live video.
int AC IC_SuspendLive(HGRABBER hGrabber); ///<Suspends an image stream and puts it into prepared state. 
 
//////////////////////////////////////////////////////////////////////////
/*! Check, whether the passed grabber already provides are live video
	@param hGrabber The handle to the grabber object.
	@retval 1 : Livevideo is running, 0 : Livevideo is not running.
	@retval IC_NO_HANDLE   hGrabber is not a valid handle. GetGrabber was not called.
	@retval IC_NO_DEVICE   No device opened. Open a device, before this function can be used.

*/

int AC IC_IsLive( HGRABBER hGrabber );


//////////////////////////////////////////////////////////////////////////
/*! Stop the live video.
	@param hGrabber The handle to the grabber object.
	@sa IC_StartLive
*/
void AC IC_StopLive( HGRABBER hGrabber ); ///<Stops the live video.

//////////////////////////////////////////////////////////////////////////
/*! Check, whether a property is supported by the current video capture device.
	@param hGrabber The handle to the grabber object.
	@sa eProperty The cammera property to be checked
	@retval IC_SUCCESS	The property is supported.
	@retval IC_ERROR	The property is not supported.
	@retval IC_NO_HANDLE   hGrabber is not a valid handle. GetGrabber was not called.
	@retval IC_NO_DEVICE   No device opened. Open a device, before this function can be used.

*/
int AC IC_IsCameraPropertyAvailable( HGRABBER hGrabber, CAMERA_PROPERTY eProperty ); ///< Check whether a camera property is available.

//////////////////////////////////////////////////////////////////////////
/*! Set a camera property like exposure, zoom.
	
	@param hGrabber The handle to the grabber object.
	@param eProperty The property to be set. It can have following values:
		@li PROP_CAM_PAN	
		@li PROP_CAM_TILT,
		@li PROP_CAM_ROLL,
		@li PROP_CAM_ZOOM,
		@li PROP_CAM_EXPOSURE,
		@li PROP_CAM_IRIS,
		@li PROP_CAM_FOCUS
	@param lValue The value the property is to be set to.
	@retval IC_SUCCESS on success
	@retval IC_ERROR if something went wrong.

	@note  lValue should be in the range of the specified property.
	If the value could not be set (out of range, auto is currently enabled), the
	function returns 0. On success, the functions returns 1.
*/
int AC IC_SetCameraProperty( HGRABBER hGrabber, CAMERA_PROPERTY eProperty, long lValue ); ///< Set a camera property.

int AC IC_CameraPropertyGetRange( HGRABBER hGrabber, CAMERA_PROPERTY eProperty, long *lMin, long *lMax); ///<Get the minimum and maximum value of a camera property
int AC IC_GetCameraProperty( HGRABBER hGrabber, CAMERA_PROPERTY eProperty, long *lValue);  ///< Get a camera property's value.

//////////////////////////////////////////////////////////////////////////
/*! Enable or disable automatic for a camera property. 
	@param hGrabber The handle to the grabber object.
	@param iProperty  The property to be set. It can have following values:
	@li PROP_CAM_PAN	
	@li PROP_CAM_TILT,
	@li PROP_CAM_ROLL,
	@li PROP_CAM_ZOOM,
	@li PROP_CAM_EXPOSURE,
	@li PROP_CAM_IRIS,
	@li PROP_CAM_FOCUS
	@param iOnOFF Enables or disables the automation. Possible values ar
	@li 1 : Enable automatic
	@li 0 : Disable Automatic
	@retval IC_SUCCESS on success
	@retval IC_ERROR if something went wrong.

	@note If the property is not supported by the current video capture device or
	automation of the property is not available with the current video capture
	device, the function returns 0. On success, the function returns 1.
*/
int AC IC_EnableAutoCameraProperty( HGRABBER hGrabber, int iProperty, int iOnOff ); ///<Enables or disables property automation.

int AC IC_IsCameraPropertyAutoAvailable( HGRABBER hGrabber, CAMERA_PROPERTY iProperty ); ///<Check whether automation for a camera property is available.
int AC IC_GetAutoCameraProperty( HGRABBER hGrabber, int iProperty, int *iOnOff ); ///<Retrieve whether automatic is enabled for the specifield camera property.

int AC IC_IsVideoPropertyAvailable( HGRABBER hGrabber, VIDEO_PROPERTY eProperty ); ///<Check whether the specified video property is available. 
int AC IC_VideoPropertyGetRange( HGRABBER hGrabber, VIDEO_PROPERTY eProperty, long *lMin, long *lMax); ///<Retrieve the lower and upper limit of a video property.
int AC IC_GetVideoProperty( HGRABBER hGrabber, VIDEO_PROPERTY eProperty, long *lValue ); ///< Retrieve the the current value of the specified video property.

int AC IC_IsVideoPropertyAutoAvailable( HGRABBER hGrabber, VIDEO_PROPERTY eProperty ); ///<Check whether the specified video property supports automation.
int AC IC_GetAutoVideoProperty( HGRABBER hGrabber, int iProperty, int *iOnOff ); ///<Get the automation state of a video property.
//////////////////////////////////////////////////////////////////////////
/*! Set a video property like brightness, contrast.

	@param hGrabber The handle to the grabber object.
	@param eProperty The property to be set. It can have following values:
	@li PROP_VID_BRIGHTNESS ,
	@li PROP_VID_CONTRAST,
	@li PROP_VID_HUE,
	@li PROP_VID_SATURATION,
	@li PROP_VID_SHARPNESS,
	@li PROP_VID_GAMMA,
	@li PROP_VID_COLORENABLE,
	@li PROP_VID_WHITEBALANCE,
	@li PROP_VID_BLACKLIGHTCOMPENSATION,
	@li PROP_VID_GAIN
	@param lValue The value the property is to be set to.
	@retval IC_SUCCESS on success
	@retval IC_ERROR if something went wrong.

    @note lValue should be in the range of the specified property.
	If the value could not be set (out of range, auto is currently enabled), the
	function returns 0. On success, the functions returns 1.
*/
int AC IC_SetVideoProperty( HGRABBER hGrabber, VIDEO_PROPERTY eProperty, long lValue ); ///<Set a video property.


//////////////////////////////////////////////////////////////////////////
/*! Enable or disable automatic for a video propertery.
	@param hGrabber The handle to the grabber object.
	@param iProperty The property to be set. It can have following values:
	@li PROP_VID_BRIGHTNESS,
	@li PROP_VID_CONTRAST,
	@li PROP_VID_HUE,
	@li PROP_VID_SATURATION,
	@li PROP_VID_SHARPNESS,
	@li PROP_VID_GAMMA,
	@li PROP_VID_COLORENABLE,
	@li PROP_VID_WHITEBALANCE,
	@li PROP_VID_BLACKLIGHTCOMPENSATION,
	@li PROP_VID_GAIN
	@param iOnOFF Enables or disables the automation. Possible values ar
	@li 1 : Enable automatic
	@li 0 : Disable Automatic
	@retval IC_SUCCESS on success
	@retval IC_ERROR if something went wrong.

    @note If the property is not supported by the current video capture device or
	automation of the property is not available with the current video capture
	device, the function reurns 0. On success, the function returns 1.
*/
int AC IC_EnableAutoVideoProperty( HGRABBER hGrabber, int iProperty, int iOnOff ); ///< Switch automatition for a video property,


//////////////////////////////////////////////////////////////////////////
/*! Retrieve the properties of the current video format and sink type 
	@param hGrabber The handle to the grabber object.
	@param *lWidth  This recieves the width of the image buffer.
	@param *lHeight  This recieves the height of the image buffer.
	@param *iBitsPerPixel  This recieves the count of bits per pixel.
	@param *format  This recieves the current color format.
	@retval IC_SUCCESS on success
	@retval IC_ERROR if something went wrong.
*/
int AC IC_GetImageDescription( HGRABBER hGrabber, long *lWidth, long *lHeight, int *iBitsPerPixel, COLORFORMAT *format );///<Retrieve the properties of the current video format and sink typ.


//////////////////////////////////////////////////////////////////////////
/*! Snaps an image. The video capture device must be set to live mode and a 
	sink type has to be set before this call. The format of the snapped images depend on
	the selected sink type. 

	@param hGrabber The handle to the grabber object.
	@param iTimeOutMillisek The Timeout time is passed in milli seconds. A value of -1 indicates, that
							no time out is set.

	
	@retval IC_SUCCESS if an image has been snapped
	@retval IC_ERROR if something went wrong.
	@retval IC_NOT_IN_LIVEMODE if the live video has not been started.

	@sa IC_StartLive 
	@sa IC_SetFormat

*/
int AC IC_SnapImage( HGRABBER hGrabber, int iTimeOutMillisek); ///<Snaps an image from the live stream. 


//////////////////////////////////////////////////////////////////////////
/*! Save the contents of the last snapped image by IC_SnapImage into a file. 
	@param hGrabber The handle to the grabber object.
	@param szFileName String containing the file name to be saved to.
	@param ft File type if the image, It have be
		@li FILETYPE_BMP for bitmap files
		@li FILETYPE_JPEG for JPEG file.
	@param quality If the JPEG format is used, the image quality must be specified in a range from 0 to 100.
	@retval IC_SUCCESS if an image has been snapped
	@retval IC_ERROR if something went wrong.

	@remarks
	The format of the saved images depend on the sink type. If the sink type 
	is set to Y800, the saved image will be an 8 Bit grayscale image. In any
	other case the saved image will be a 24 Bit RGB image.

	@note IC Imaging Control 1.41 only supports FILETYPE_BMP.
	@sa IC_SnapImage
	@sa IC_SetFormat
*/
int AC IC_SaveImage( HGRABBER hGrabber, char *szFileName, IMG_FILETYPE ft, long quality ); ///< Saves an image to a file.


//////////////////////////////////////////////////////////////////////////
/*! Retrieve a byte pointer to the image data (pixel data) of the last snapped
	image (see SnapImage()). If the function fails, the return value is NULL
	otherwise the value is a pointer to the first byte in the lowest image line
	(the image is saved bottom up!).
	@param hGrabber The handle to the grabber object.
	@retval Nonnull Pointer to the image data
	@retval NULL Indicates that an error occurred.
	@sa IC_SnapImage
	@sa IC_SetFormat
*/
unsigned char* AC IC_GetImagePtr( HGRABBER hGrabber ); ///< Retuns a pointer to the image data


//////////////////////////////////////////////////////////////////////////
/*! Assign an Window handle to display the video in.
	@param hGrabber The handle to the grabber object.
	@param hWnd The handle of the window where to display the live video in.
	@retval IC_SUCCESS if an image has been snapped
	@retval IC_ERROR if something went wrong.
*/
int AC IC_SetHWnd( HGRABBER hGrabber, __HWND hWnd ); ///< Sets a window handle for live display


//////////////////////////////////////////////////////////////////////////
/*! Return the serialnumber of the current device. Memory for the serialnumber
    must has been allocated by the application:

	@code
	char szSerial[20];
	GetSerialNumber( hGrabber, szSerial );
	@endcode

	This function decodes the The Imaging Source serialnumbers.
	@param hGrabber The handle to the grabber object.
	@param szSerial char array that recieves the serial number.
	@retval IC_SUCCESS The serial number could be retrieved.
	@retval IC_IC_NOT_AVAILABLE The video capture device does not provide a serial number.
	@retval IC_NO_DEVICE No video capture device opened-
	@retval IC_NO_HANDLE hGrabber is NULL.
*/
int AC IC_GetSerialNumber( HGRABBER hGrabber, char* szSerial );///<Return the video capture device's serial number.


//////////////////////////////////////////////////////////////////////////
/*! Count all connected video capture devices. If the Parameter szDeviceList
    is NULL, only the number of devices is queried. The Parameter szDeviceList
	must be a two dimensional array of char. The iSize parameter specifies the
	length of the strings, that are used in the array.
	
	@param szDeviceList A two dimensional char array that recieves the list. Or NULL if only the count of devices is to be returned.
	@param iSize Not used.
	@retval >= 0 Success, count of found devices
	@retval <0 An error occurred.
	
	Simple sample to list the video capture devices:
	@code
    char szDeviceList[20][40];
	int iDeviceCount;

	iDeviceCount = IC_ListDevices( (char*)szDeviceList,40 );
	for( i = 0; i < iDeviceCount; i++ )
	{
		printf("%2d. %s\n",i+1,szDeviceList[i]);
	}
	@endcode
*/
int AC IC_ListDevices( char *szDeviceList, int iSize );///< Count and list devices.

//////////////////////////////////////////////////////////////////////////
/*! Simpler approach of enumerating devices. No 2D char array needed

	@code
    char szDeviceName[40]; // Use max 39 chars for a device name
	int iDeviceCount;

	iDeviceCount = IC_GetDeviceCount(); // Query number of connected devices
	for( i = 0; i < iDeviceCount; i++ )
	{
		IC_ListDevicesbyIndex(szDeviceName,39, i);
		printf("%2d. %s\n",i+1,szDeviceName);
	}
	@endcode
	@param szDeviceName Char memory, that receives the device name
	@param iSize Size of the char memory. If names are longer, they will be truncated.
	@param DeviceIndex Index of the device to be query. Must be between 0 and IC_GetDeviceCount.

	@retval >= 0 Success, count of found devices
	@retval <0 An error occurred.

*/
int AC IC_ListDevicesbyIndex( char *szDeviceName, int iSize, int DeviceIndex );

//////////////////////////////////////////////////////////////////////////
/*! Count all available video formats. If the Parameter szFormatList
    is NULL, only the number of formats is queried. The Parameter szFormatList
	must be a two dimensional array of char. The iSize parameter specifies the
	length of the strings, that are used in the array to store the format names.

	@param hGrabber The handle to the grabber object.
	@param szFormatList A two dimensional char array that recieves the list. Or NULL if only the count of formats is to be returned.

	@retval >= 0 Success, count of found video formats
	@retval <0 An error occurred.
	
	Simple sample to list the video capture devices:
	@code
    char szFormatList[80][40];
	int iFormatCount;
	HGRABBER hGrabber;
	hGrabber = IC_CreateGrabber();
	IC_OpenVideoCaptureDevice(hGrabber, "DFK 21F04" );
	iFormatCount = IC_ListDevices(hGrabber, (char*)szFormatList,40 );
	for( i = 0; i < min( iFormatCount, 80); i++ )
	{
		printf("%2d. %s\n",i+1,szFormatList[i]);
	}
	IC_ReleaseGrabber( hGrabber );
	@endcode
*/
int AC IC_ListVideoFormats( HGRABBER hGrabber, char *szFormatList, int iSize );///<List available video formats.

//////////////////////////////////////////////////////////////////////////
/*! Simpler approach of enumerating video formats. No 2D char array needed.

	@param hGrabber The handle to the grabber object.
	@param szFormatName char memory, that will receive the name of the video format. Should be big enough.
	@param iSize Size in byte of szFormatName
	@iIndex Index of the video format to query.

	@code
    char szVideoFormatName[40]; // Use max 39 chars for a video format name
	int FormatCount;
	HGRABBER hGrabber;
	hGrabber = IC_CreateGrabber();
	IC_OpenVideoCaptureDevice(hGrabber, "DFK 21AU04" );
	FormatCount = IC_GetVideoFormatCount(hGrabber); // Query number of connected devices
	for( i = 0; i < FormatCount; i++ )
	{
		IC_ListVideoFormatbyIndex(szVideoFormatName,39, i);
		printf("%2d. %s\n",i+1,szVideoFormatName);
	}
	@endcode
	@param szDeviceName Char memory, that receives the device name
	@param iSize Size of the char memory. If names are longer, they will be truncated.
	@param DeviceIndex Index of the device to be query. Must be between 0 and IC_GetDeviceCount.

	@retval IC_SUCCESS Success,
	@retval IC_NO_DEVICE No video capture device selected.
	@retval IC_NO_HANDLE No handle to the grabber object.

*/
int AC IC_ListVideoFormatbyIndex( HGRABBER hGrabber, char *szFormatName, int iSize, int iIndex);

//////////////////////////////////////////////////////////////////////////
/*! Get the number of the currently available devices. This function creates an
	internal array of all connected video capture devices. With each call to this 
	function, this array is rebuild. The name and the unique name can be retrieved 
	from the internal array using the functions IC_GetDevice() and IC_GetUniqueNamefromList.
	They are usefull for retrieving device names for opening devices.
	
	@retval >= 0 Success, count of found devices.
	@retval IC_NO_HANDLE Internal Error.

	@sa IC_GetDevice
	@sa IC_GetUniqueNamefromList
*/
int AC IC_GetDeviceCount(); ///<Get the number of the currently available devices. 


//////////////////////////////////////////////////////////////////////////
/*! Get a string representation of a device specified by iIndex. iIndex 
	must be between 0 and IC_GetDeviceCount(). IC_GetDeviceCount() must 
	have been called before this function, otherwise it will always fail.
	
	@param iIndex The number of the device whose name is to be returned. It must be
				  in the range from 0 to IC_GetDeviceCount(),
    @return Returns the string representation of the device on success, NULL
			otherwise.

	@sa IC_GetDeviceCount
	@sa IC_GetUniqueNamefromList
*/
char* AC IC_GetDevice( int iIndex ); ///< Get the name of a video capture device.

//////////////////////////////////////////////////////////////////////////
/*! Get unique device name of a device specified by iIndex. The unique device name
	consist from the device name and its serial number. It allows to differ between 
	more then one device of the same type connected to the computer. The unique device name
	is passed to the function IC_OpenDevByUniqueName

	@param iIndex The number of the device whose name is to be returned. It must be
				in the range from 0 to IC_GetDeviceCount(),
	@return Returns the string representation of the device on success, NULL
				otherwise.

	@sa IC_GetDeviceCount
	@sa IC_GetUniqueNamefromList
	@sa IC_OpenDevByUniqueName
*/
char* AC IC_GetUniqueNamefromList( int iIndex );///< Get the unique name of a video capture device.

//////////////////////////////////////////////////////////////////////////
/*! Get the number of the available input channels for the current device.
    A video	capture device must have been opened before this call.

	@param hGrabber The handle to the grabber object.

	@retval >= 0 Success
	@retval IC_NO_DEVICE No video capture device selected.
	@retval IC_NO_HANDLE No handle to the grabber object.

	@sa IC_GetInputChannel
*/
int AC IC_GetInputChannelCount( HGRABBER hGrabber ); ///<Get the number of the available input channels.


//////////////////////////////////////////////////////////////////////////
/*! Get a string representation of the input channel specified by iIndex. 
	iIndex must be between 0 and IC_GetInputChannelCount().
	IC_GetInputChannelCount() must have been called before this function,
	otherwise it will always fail.		
	@param hGrabber The handle to the grabber object.
	@param iIndex Number of the input channel to be used..

	@retval Nonnull The name of the specified input channel
	@retval NULL An error occured.
	@sa IC_GetInputChannelCount
*/
char* AC IC_GetInputChannel( HGRABBER hGrabber, int iIndex ); ///<Get the name of an input channel.


//////////////////////////////////////////////////////////////////////////
/*! Get the number of the available video norms for the current device. 
	A video capture device must have been opened before this call.
	
	@param hGrabber The handle to the grabber object.

	@retval >= 0 Success
	@retval IC_NO_DEVICE No video capture device selected.
	@retval IC_NO_HANDLE No handle to the grabber object.
	
	@sa IC_GetVideoNorm
*/
int AC IC_GetVideoNormCount( HGRABBER hGrabber ); ///<Get the count of available video norms.


//////////////////////////////////////////////////////////////////////////
/*! Get a string representation of the video norm specified by iIndex. 
	iIndex must be between 0 and IC_GetVideoNormCount().
	IC_GetVideoNormCount() must have been called before this function,
	otherwise it will always fail.		
	
	@param hGrabber The handle to the grabber object.
	@param iIndex Number of the video norm to be used.

	@retval Nonnull The name of the specified video norm.
	@retval NULL An error occured.
	@sa IC_GetVideoNormCount

*/
char* AC IC_GetVideoNorm( HGRABBER hGrabber, int iIndex ); ///<Get the name of a video norm.


//////////////////////////////////////////////////////////////////////////
/*! Get the number of the available video formats for the current device. 
	A video capture device must have been opened before this call.
	
	@param hGrabber The handle to the grabber object.

	@retval >= 0 Success
	@retval IC_NO_DEVICE No video capture device selected.
	@retval IC_NO_HANDLE No handle to the grabber object.

	@sa IC_GetVideoFormat
*/
int AC IC_GetVideoFormatCount( HGRABBER hGrabber ); ///< Returns the count of available video formats.


//////////////////////////////////////////////////////////////////////////
/*! Get a string representation of the video format specified by iIndex. 
	iIndex must be between 0 and IC_GetVideoFormatCount().
	IC_GetVideoFormatCount() must have been called before this function,
	otherwise it will always fail.	

	@param hGrabber The handle to the grabber object.
	@param iIndex Number of the video format to be used.

	@retval Nonnull The name of the specified video format.
	@retval NULL An error occured.
	@sa IC_GetVideoFormatCount
*/
char* AC IC_GetVideoFormat( HGRABBER hGrabber, int iIndex ); ///<Return the name of a video format.



//////////////////////////////////////////////////////////////////////////
/*! Save the state of a video capture device to a file. 
	
	@param hGrabber The handle to the grabber object.
	@param szFileName Name of the file where to save to.

	@retval IC_SUCCESS if an image has been snapped
	@retval IC_ERROR if something went wrong.

	@sa IC_LoadDeviceStateFromFile
*/
int AC IC_SaveDeviceStateToFile(HGRABBER hGrabber, char* szFileName);///<Save the state of a video capture device to a file. 

//////////////////////////////////////////////////////////////////////////
/*! Load a device settings file. On success the device is opened automatically.

	@param hGrabber The handle to the grabber object. If it is NULL then a new HGRABBER handle is
					created. This should be released by a call to IC_ReleaseGrabber when it is no longer needed.
	@param szFileName Name of the file where to save to.

	@return HGRABBER The handle of the grabber object, that contains the new opened video capture device.

	@sa IC_SaveDeviceStateToFile
	@sa IC_ReleaseGrabber
*/
HGRABBER AC IC_LoadDeviceStateFromFile(HGRABBER hGrabber, char* szFileName); ///<Load a device settings file.


//////////////////////////////////////////////////////////////////////////
/*! Load a device settings file. 

	@param hGrabber The handle to the grabber object. If it is NULL then a new HGRABBER handle is
					created, in case OpenDevice is true. If OpenDevice is set to false, the a device must be already
					open in the grabber handle. The properties in the passed XML file will be apllied to 
					the opened device. 
					This should be released by a call to IC_ReleaseGrabber when it is no longer needed.
	@param szFileName Name of the file where to save to.
	@param OpenDevice If 1, the device specified in the XML file is opened. If 0, then a device must be opened in the hGrabber. 
						The properties and video format specified in the XML file will be applied to the opened device.

	@return IC_SUCCESS The device was successfully opened and the settings saved in the XML file were set.
	@return IC_NO_DEVICE False was passed to OpenDevice, but no device was opened in the grabber handle or the handle is NULL
	@return IC_WRONG_XML_FORMAT No device opened.
	@return IC_WRONG_INCOMPATIBLE_XML No device opened.
	@return IC_DEVICE_NOT_FOUND No device opened.
	@return IC_FILE_NOT_FOUND Passed XML file does not exist.
	@return IC_NOT_ALL_PROPERTIES_RESTORED The device was opened, but not all properties could be set as wanted.

	@sa IC_SaveDeviceStateToFile
	@sa IC_ReleaseGrabber
*/
int  AC IC_LoadDeviceStateFromFileEx(HGRABBER hGrabber, char* szFileName, int OpenDevice); ///<Load a device settings file.





//////////////////////////////////////////////////////////////////////////
/*! Save the device settings to a file specified by szFilename. When used 
	with IC Imaging Control 1.41 the device name, the input channel, the 
	video norm and the video format are saved. When used with IC Imaging 
	Control 2.0, the VCDProperties are saved as well. Returns 1 on success,
	0 otherwise.
	Notice that in IC Imaging Control 1.41 the device name includes the trailing 
	number if there is more than one device of the same type available. This can
	cause IC_OpenDeviceBySettings() to fail if one of those devices is unplugged.
	When used with IC Imaging Control 2.0, this cannot happen because the device 
	name is stored without the trailing number. Instead the first device that 
	matches the type specified in the settings file is opened.

	@deprecated Use IC_SaveDeviceStateToFile instead.

*/
int AC IC_SaveDeviceSettings( HGRABBER hGrabber, char* szFilename );


//////////////////////////////////////////////////////////////////////////
/*! Open a device by a settings file specified by szFilename. If succedeed,
	1 is returned and a device specified in the settings file is opened and
	initialized with the settings data. If failed, 0 is returned. 

	@deprecated Use IC_LoadDeviceStateFromFile instead.
*/
int AC IC_OpenDeviceBySettings( HGRABBER hGrabber, char* szFilename );


//////////////////////////////////////////////////////////////////////////
/*! Load device settings from a file specified by szFilename. A device must 
	have been opened before this function is called. A check is performed whether
	the current device matches the device type stored in the settings file.
	If so, the settings are loaded and set.
	Returns 1 on success, 0 otherwise.
	Notice: This function will only work with IC Imaging Control 2.0. When used
	with IC Imaging Control 1.41, it will always return 0.

	@deprecated Use IC_LoadDeviceStateFromFile instead.
*/
int AC IC_LoadDeviceSettings( HGRABBER hGrabber, char* szFilename );

//////////////////////////////////////////////////////////////////////////
/*! Open a video capture by using its DisplayName. 
	@param hGrabber The handle to the grabber object.
	@param szDisplayname Displayname of the device. Can be retrieved by a call to IC_GetDisplayName().

	@retval IC_SUCCESS if an image has been snapped
	@retval IC_ERROR if something went wrong.

	@sa IC_GetDisplayName
*/
int AC IC_OpenDevByDisplayName( HGRABBER hGrabber, char *szDisplayname); ///<Open a video capture by using its DisplayName. 

//////////////////////////////////////////////////////////////////////////
/*! Get a DisplayName from a currently open device. The display name of a
	device can be another on different computer for the same video capture
	device. 
	
	@param hGrabber       Handle to a grabber object
	@param szDisplayName  Memory that will take the display name. If it is NULL, the
						  length of the display name will be returned.
	@param iLen           Size in Bytes of the memory allocated by szDisplayName.

	
	@retval IC_SUCCESS     On success. szDisplayName contains the display name of the device.
	@retval IC_ERROR	   iLen is less than the length of the retrieved display name. 
	@retval IC_NO_HANDLE   hGrabber is not a valid handle. GetGrabber was not called.
	@retval IC_NO_DEVICE   No device opened. Open a device, before this function can be used.
	@retval >1             Length of the display name, if szDisplayName is NULL.

	@sa IC_OpenDevByDisplayName
	@sa IC_ReleaseGrabber

*/
int AC IC_GetDisplayName( HGRABBER hGrabber, char *szDisplayname, int iLen); ///<Get the display name of a device.


//////////////////////////////////////////////////////////////////////////
/*! Open a video capture by using its UniqueName. Use IC_GetUniqueName() to
    retrieve the unique name of a camera.

	@param hGrabber       Handle to a grabber object
	@param szDisplayName  Memory that will take the display name.

	@sa IC_GetUniqueName
	@sa IC_ReleaseGrabber

*/
int AC IC_OpenDevByUniqueName( HGRABBER hGrabber, char *szDisplayname);


//////////////////////////////////////////////////////////////////////////
/*! Get a UniqueName from a currently open device.
	
	@param hGrabber      Handle to a grabber object
	@param szUniqueName  Memory that will take the Unique name. If it is NULL, the
						 length of the Unique name will be returned.
	@param iLen          Size in Bytes of the memory allocated by szUniqueName.

	
	@retval IC_SUCCESS    On success. szUniqueName contains the Unique name of the device.
	@retval IC_ERROR	  iLen is less than the length of the retrieved Unique name. 
	@retval IC_NO_HANDLE  hGrabber is not a valid handle. GetGrabber was not called.
	@retval IC_NO_DEVICE  No device opened. Open a device, before this function can be used.
	@retval >1            Length of the Unique name, if szUniqueName is NULL.
	
*/
int AC IC_GetUniqueName( HGRABBER hGrabber, char *szUniquename, int iLen); ///<Get a UniqueName from a currently open device.

//////////////////////////////////////////////////////////////////////////
/*! This returns 1, if a valid device has been opened, otherwise it is 0.

	@param hGrabber      Handle to a grabber object.

	@retval IC_ERROR There is no valid video capture device opened
	@retval IC_SUCCESS There is a valid video capture device openend.
*/
int AC IC_IsDevValid( HGRABBER hGrabber); ///<Returns whether a video capture device is valid.


//////////////////////////////////////////////////////////////////////////
/*! Show the VCDProperty dialog. 

	@param hGrabber      Handle to a grabber object.
	@retval IC_SUCCESS on success
	@retval IC_ERROR on error.
	@retval IC_NO_DEVICE No video capture device selected.
	@retval IC_NO_HANDLE Nullpointer.
*/
int AC IC_ShowPropertyDialog( HGRABBER hGrabber ); ///<Show the VCDProperty dialog. 


//////////////////////////////////////////////////////////////////////////
/*! Show the device selection dialog. This dialogs enables to select the 
	video capture device, the video norm, video format, input channel and
	frame rate.

	@param hGrabber      Handle to a grabber object.

	@return The passed hGrabber object or a new created if hGrabber was NULL.

	@code
    HGRABBER hTheGrabber;
	hTheGrabber = IC_ShowDeviceSelectionDialog( NULL );
	if( hTheGrabber != NULL )
	{
		IC_StartLive( hTheGrabber, 1 ); // Show the live video of this grabber
		IC_ShowPropertyDialog( hTheGrabber );	// Show the property page of this grabber
	}
	@endcode
*/
HGRABBER AC IC_ShowDeviceSelectionDialog( HGRABBER hGrabber ); ///<Show the device selection dialog.

//////////////////////////////////////////////////////////////////////////
/*!	
	Return whether the current video capture device supports an external 
	trigger. 

	@param hGrabber      Handle to a grabber object.
	@retval IC_SUCCESS An external trigger is supported
	@retval IC_ERROR No external trigger is supported.
	@retval IC_NO_DEVICE No video capture device selected.
	@retval IC_NO_HANDLE Internal Grabber does not exist.

	@sa IC_EnableTrigger
*/
int AC IC_IsTriggerAvailable( HGRABBER hGrabber ); ///<Check for external trigger support.


//////////////////////////////////////////////////////////////////////////
/*!	Enable or disable the external trigger. 
	@param hGrabber      Handle to a grabber object.
	@param iEnable 1 = enable the trigger, 0 = disable the trigger

	@retval IC_SUCCESS    Trigger was enabled or disabled successfully.
	@retval IC_NOT_AVAILABLE The device does not support triggering.
	@retval IC_NO_PROPERTYSET Failed to query the property set of the device.
	@retval IC_NO_DEVICE No video capture device selected.

	@retval IC_NO_HANDLE Internal Grabber does not exist or hGrabber is NULL.

	@sa IC_IsTriggerAvailable
*/
int AC IC_EnableTrigger( HGRABBER hGrabber, int iEnable );



//////////////////////////////////////////////////////////////////////////
/*!	Remove or insert the  the overlay bitmap to the grabber object. If
	Y16 format is used, the overlay must be removed,

	@param hGrabber      Handle to a grabber object.
	@param iEnable = 1 inserts overlay, 0 removes the overlay.
*/
void AC IC_RemoveOverlay( HGRABBER hGrabber, int iEnable );

//////////////////////////////////////////////////////////////////////////
/*!	Enable or disable the overlay bitmap on the live video
	@param hGrabber      Handle to a grabber object.
	@param iEnable = 1 enables the overlay, 0 disables the overlay.
*/
void AC IC_EnableOverlay( HGRABBER hGrabber, int iEnable ); ///<Enable or disable the overlay bitmap.

//////////////////////////////////////////////////////////////////////////
/*!  BeginPaint returns an HDC for GDI painting purposes (like TextOut() etc.)
	When the paintings are finished, the function IC_EndPaint must be called.

	@param hGrabber      Handle to a grabber object.

	@return HDC The function returns not NULL, if the HDC could be retrieved. If the HDC 
			could not be retrieved or an error has occured, the function returns 0.

	Sample code:
	@code
	HDC hPaintDC;
	hPaintDC = IC_BeginPaint(hGrabber);
	if( hPaintDC != NULL )
	{
	    TextOut( hPaintDC,10,10,"Text",4);
	}
	IC_EndPaint(hGrabber)
	@endcode

	@sa IC_EndPaint
*/
long AC IC_BeginPaint( HGRABBER hGrabber ); ///< BeginPaint returns an HDC for GDI painting purposes.

//////////////////////////////////////////////////////////////////////////
/*!  The EndPaint functions must be called, after BeginPaint has been called,
    and the painting operations have been finished.
	@param hGrabber      Handle to a grabber object.
	@sa IC_BeginPaint
*/
void AC IC_EndPaint( HGRABBER hGrabber ); ///< End painting functions on the overlay bitmap.
//////////////////////////////////////////////////////////////////////////
/*! Display a windows messagebox.
	@param szText Message text
	@param zsTitle Title of the messagebox.
*/
void AC IC_MsgBox( char * szText, char* szTitle ); ///<Display a windows messagebox.

/*! @} */

/**
 * \defgroup CALLBACK Callbacks Functions
 * @{
*/


//////////////////////////////////////////////////////////////////////////
/*! Type declaration for the new frame ready callback function.

	@sa IC_SetFrameReadyCallback
	@sa IC_SetCallbacks
	@retval IC_SUCCESS Callback was set successfully
	@retval IC_ERROR An error occurred, e.g. hGrabber is NULL.
*/
typedef void(*FRAME_READY_CALLBACK_EX)
(HGRABBER hGrabber, HMEMBUFFER hBuffer, unsigned long frameNumber, void*);

//////////////////////////////////////////////////////////////////////////
/*!	Enable frame ready callback.
	@param hGrabber      Handle to a grabber object.
	@param cb Callback function of type FRAME_READY_CALLBACK
	@param x1_argument_in_void_userdata Pointer to some userdata.

	@retval IC_SUCCESS Callback was set successfully
	@retval IC_ERROR An error occurred, e.g. hGrabber is NULL.

	@sa FRAME_READY_CALLBACK

*/
int AC IC_SetFrameReadyCallbackEx(
	HGRABBER				hGrabber,
	FRAME_READY_CALLBACK_EX	cb,
	void*					x1_argument_in_void_userdata);





//////////////////////////////////////////////////////////////////////////
/*! Type declaration for the frame ready callback function. 
	@sa IC_SetFrameReadyCallback
	@sa IC_SetCallbacks
	@retval IC_SUCCESS Callback was set successfully
	@retval IC_ERROR An error occurred, e.g. hGrabber is NULL.

*/
typedef void (*FRAME_READY_CALLBACK)
	(HGRABBER hGrabber, unsigned char* pData, unsigned long frameNumber, void* );



//////////////////////////////////////////////////////////////////////////
/*! Type declaration for the device lost callback function. 
	@sa IC_SetCallbacks
*/
typedef void (*DEVICE_LOST_CALLBACK)(HGRABBER hGrabber, void* );


//////////////////////////////////////////////////////////////////////////
/*!	Enable frame ready callback.
	@param hGrabber      Handle to a grabber object.
	@param cb Callback function of type FRAME_READY_CALLBACK
	@param x1_argument_in_void_userdata Pointer to some userdata.
	
	@retval IC_SUCCESS Callback was set successfully
	@retval IC_ERROR An error occurred, e.g. hGrabber is NULL.

	@sa FRAME_READY_CALLBACK

*/
int AC IC_SetFrameReadyCallback(
	HGRABBER				hGrabber,
	FRAME_READY_CALLBACK	cb,
	void*					x1_argument_in_void_userdata);


/*!	Set callback function
	@param hGrabber      Handle to a grabber object.
	@param cb Callback function of type FRAME_READY_CALLBACK, can be NULL, if no callback is needed
	@param dlcb Callback function of type DEVICE:LOST_CALLBACK, can be NULL, if no device lost handler is needed
	@param x1_argument_in_void_userdata Pointer to some userdata.

	@sa FRAME_READY_CALLBACK
*/
int AC IC_SetCallbacks(
	HGRABBER				hGrabber,
	FRAME_READY_CALLBACK	cb,
	void*					x1_argument_in_void_userdata,
	DEVICE_LOST_CALLBACK	dlCB,
	void*					x2_argument_in_void_userdata);




/////////////////////////////////////////////////////////////////////////
/*!	Set Continuous mode
 
 	In continuous mode, the callback is called for each frame,
 	so that there is no need to use IC_SnapImage etc.
 
	@param hGrabber      Handle to a grabber object.
	@param cont			0 : Snap continouos, 1 : do not automatically snap.

	@retval IC_SUCCESS Success
	@retval IC_NOT_IN_LIVEMODE The device is currently streaming, therefore setting continuous mode failed.
	@retval IC_NO_HANDLE Internal Grabber does not exist or hGrabber is NULL

	@remarks
 	Not available in live mode.
 
 */
int AC IC_SetContinuousMode( HGRABBER hGrabber, int cont ); ///<Set Continuous mode.


/*! @} */

/**
 * \defgroup FRAMERATE FrameRate Functions
 * @{
*/
///////////////////////////////////////////////////////////////////
/*! Sets a new frame rate. 
	@param hGrabber	Handle to a grabber object.
	@param FrameRate The new frame rate.
	@retval IC_SUCCESS : Success
	@retval IC_NOT_AVAILABLE : The property is not supported by the current device
	@retval IC_NO_HANDLE	Invalid grabber handle
	@retval IC_NO_DEVICE    No video capture device opened
	@retval IC_NOT_IN_LIVEMODE Frame rate can not set, while live video is shown. Stop Live video first!
*/
int AC IC_SetFrameRate(HGRABBER hGrabber,float FrameRate);

///////////////////////////////////////////////////////////////////
/*! Retrieves the current frame rate

	@param hGrabber	Handle to a grabber object.
	@retval The current frame rate. If it is 0.0, then frame rates are not supported.
*/
float AC IC_GetFrameRate(HGRABBER hGrabber);

///////////////////////////////////////////////////////////////////
/*! Retrieves available frame rates.
	The count of available frame rates depends on the used video capture
	device and the currently used video format. After a video was changed,
	the available frame rates usually are changed by the video capture device 
	too.
	@code
	int Index = 0;
	float fps = 0.0f;

	while( IC_GetAvailableFrameRates(hGrabber, Index, &fps ) == IC_SUCCESS )
	{
		printf("Frame rate %d : %f fps\n", Index, fps);
		Index++;
	}
	@endcode

	@param hGrabber	Handle to a grabber object.
	@param Index Index of the frame rates, starting at 0
	@param fps Pointer to a float variable, that will receive the frame rate of the passed index.
	@retval IC_SUCCESS, if the frame rate at Index exists, otherwise IC_ERROR,
*/
int AC IC_GetAvailableFrameRates(HGRABBER hGrabber, int Index, float *fps);


/*! @} */

/**
 * \defgroup UNSOECIFIED Unspecified Functions
 * @{
 */
///////////////////////////////////////////////////////////////////
/*! Show the internal property page of the camera
*/
int AC IC_ShowInternalPropertyPage(HGRABBER hGrabber);


///////////////////////////////////////////////////////////////////
/*! Resets the driver. Do not use, for internl purposes only.

	@param hGrabber	Handle to a grabber object.
	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
*/
int AC IC_ResetUSBCam(HGRABBER hGrabber);


///////////////////////////////////////////////////////////////////
/*! This function queries the internal property set (KsPropertySet) of the driver. 
	It allows an application to access all properties of a video capture devices
	using the enums and GUIDs from the header files fwcam1394propguid.h and 
	fwcam1394props.h.

	@param hGrabber	Handle to a grabber object.
	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_ERROR			The property could not have been retrieved

*/
int AC IC_QueryPropertySet(HGRABBER hGrabber);


///////////////////////////////////////////////////////////////////
/*! This function sets a value or structure to the internal property set
	of the video capture device. The properties and structures are defined
	in the header file fwcam1394props.h. Before using this function, the
	properties set must have been queried once using the function IC_QueryPropertySet().


	@param hGrabber	Handle to a grabber object.
	@retval IC_SUCCESS			Success
	@retval IC_ERROR			Setting of the values failed
	@retval IC_NO_PROPERTYSET	The property set was not retrieved or is not available.
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened

	@sa IC_QueryPropertySet
*/
//int AC IC_PropertySet_Set(HGRABBER hGrabber, FWCAM1394_CUSTOM_PROP prop, FWCAM1394_CUSTOM_PROP_S& rstruct );


/*! @} */

/**
 * \defgroup WINDOWFUNCTIONS Window Functions
	The window functions are used for displaying and resizing the live video in a window, e.g. a label or static of a GUI.
	@code
	 HWND hWnd = GetDlgItem( hDlg, IDC_STATICVIDEOWINDOW);
	 IC_SetHWnd(hGrabber, hWnd);
	 IC_SetDefaultWindowPosition( hGrabber, 0);
	 // Show the live video in resized to 160x120.
	 IC_SetWindowPosition( hGrabber, 0,0, 160, 120 );
	 @endcode

 * @{
 */

///////////////////////////////////////////////////////////////////
/*! Enables or disables the default window size lock of the video window. 

	@param hGrabber	Handle to a grabber object.
	@param Default	0 = disable, custome size can be set, 1 = enable, the standard size, which is video format, is used.

	@retval IC_SUCCESS			Success
	@retval IC_ERROR			Setting of the values failed
	@retval IC_NO_PROPERTYSET	The property set was not retrieved or is not available.
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened

*/
int AC IC_SetDefaultWindowPosition(HGRABBER hGrabber, int Default);

///////////////////////////////////////////////////////////////////
/*! This function gets the position and size of the video window. 

	@param hGrabber	Handle to a grabber object.
	@param PosX  Specifies the x-coordinate of the upper left hand corner of the video window. It defaults to 0. 
	@param PosY  Specifies the y-coordinate of the upper left hand corner of the video window. It defaults to 0. 
	@param width  Specifies the width of the video window. 
	@param height  Specifies the height of the video window. 

 	@retval IC_SUCCESS			Success
	@retval IC_ERROR			Setting of the values failed
	@retval IC_DEFAULT_WINDOW_SIZE_SET	The property set was not retrieved or is not available.
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened

*/
int AC IC_SetWindowPosition(HGRABBER hGrabber, int PosX, int PosY, int Width, int Height );

///////////////////////////////////////////////////////////////////
/*! This function gets the position and size of the video window.

@param hGrabber	Handle to a grabber object.
@param PosX  Receives the x-coordinate of the upper left hand corner of the video window. It defaults to 0.
@param PosY  Receives the y-coordinate of the upper left hand corner of the video window. It defaults to 0.
@param width  Receives the width of the video window.
@param height  Receives the height of the video window.

@retval IC_SUCCESS			Success
@retval IC_ERROR			Setting of the values failed
@retval IC_DEFAULT_WINDOW_SIZE_SET	The property set was not retrieved or is not available.
@retval IC_NO_HANDLE		Invalid grabber handle
@retval IC_NO_DEVICE		No video capture device opened

*/
int AC IC_GetWindowPosition(HGRABBER hGrabber, int *PosX, int *PosY, int *Width, int *Height);

/*! @} */

/**
 * \defgroup DEVICEPROPERTIES Device Property Functions
 * @{
 */
 int AC IC_SetWhiteBalanceAuto( HGRABBER hGrabber, int iOnOff);


 //////////////////////////////////////////////////////////////////////////
 /*! SignalDetected

	 Detects whether a video signal is available.

	 @param hGrabber      Handle to a grabber object.

	 @retval IC_SUCCESS   Signal detected
	 @retval IC_ERROR  No video signal detected
	 @retval IC_NO_HANDLE  Invalid grabber handle
	 @retval IC_NO_DEVICE    No video capture device opened
	 @retval IC_NOT_IN_LIVEMODE  No live mode, startlive was not called
	 @retval IC_NOT_AVAILABLE Signal Detected property is not available.
 */
 int AC IC_SignalDetected(HGRABBER hGrabber); ///<Detects whether a video signal is available.


 //////////////////////////////////////////////////////////////////////////
 /*! Get trigger modes.
	 Simple sample to list the video capture devices:

	 @param hGrabber      Handle to a grabber object.
	 @param szModeList	Twodimensional array of char that will recieve the mode list.
	 @param iSze			Size of the array (first dimension)

	 @retval 0 : No trigger modes available
	 @retval >0 : Count of available trigger modes
	 @retval IC_NO_HANDLE  Invalid grabber handle
	 @retval IC_NO_DEVICE  No video capture device opened

	 @code
	 char szModes[20][10];
	 int iModeCount;

	 iModeCount = IC_GetTriggerModes(hGrabber, (char*)szModes,20);
	 for( int i = 0; i < min( iModeCount, 20); i++ )
	 {
		 printf("%2d. %s\n",i+1,szModes[i]);
	 }
	 @endcode
 */
 int AC IC_GetTriggerModes(HGRABBER hGrabber, char *szModeList, int iSize); ///<Get trigger modes.


 //////////////////////////////////////////////////////////////////////////
 /*!  Set the trigger mode.
	 Sets the mode that has been retrieved  by a call to IC_GetTriggerModes.

	 @param hGrabber	Handle to a grabber object.
	 @param szMode	String containing the name of the mode.

	 @retval IC_SUCCESS		Success.
	 @retval IC_NOT_AVAILABLE Triggermode is not supported by the current device.
	 @retval IC_ERROR		An error occurred
	 @retval IC_NO_HANDLE	Invalid grabber handle
	 @retval IC_NO_DEVICE    No video capture device opened

 */
 int AC IC_SetTriggerMode(HGRABBER hGrabber, char* szMode); ///<Set the trigger mode.

 //////////////////////////////////////////////////////////////////////////
 /*! Set the trigger polarity

	 Sample:
	 @code
	 IC_SetTriggerPolarity(hGrabber, 0);
	 @endcode
	 or
	 @code
	 IC_SetTriggerPolarity(hGrabber, 1);
	 @endcode
	 @param hGrabber	Handle to a grabber object.
	 @param iPolarity
		 @li 0 : Polarity on direction
		 @li 1 : Polarity the other direction

	 @retval IC SUCCESS : Polarity could be set successfully
	 @retval IC_NOT_AVAILABLE Triggerpolarity is not supported by the current device.
	 @retval IC_NO_HANDLE	Invalid grabber handle
	 @retval IC_NO_DEVICE    No video capture device opened
 */
 int AC IC_SetTriggerPolarity(HGRABBER hGrabber, int iPolarity); ///< Set the trigger polarity.

 int AC IC_GetExpRegValRange(HGRABBER hGrabber, long *lMin, long *lMax); ///< Retrieve exposure register values lower and upper limits.
 int AC IC_GetExpRegVal(HGRABBER hGrabber, long *lValue); ///< Retrieve the current register value of exposure.
 int AC IC_SetExpRegVal(HGRABBER hGrabber, long lValue); ///<Set a register value for exposure.
 int AC IC_EnableExpRegValAuto(HGRABBER hGrabber, int iOnOff); ///<Enable or disable automatic of exposure.
 int AC IC_GetExpRegValAuto(HGRABBER hGrabber, int *iOnOff);///<Check whether automatic exposure is enabled.



 //////////////////////////////////////////////////////////////////////////
 /*! Functions for the absolute values interface of exposure.
 */
 int AC IC_IsExpAbsValAvailable(HGRABBER hGrabber);
 int AC IC_GetExpAbsValRange(HGRABBER hGrabber, float *fMin, float *fMax);
 int AC IC_GetExpAbsVal(HGRABBER hGrabber, float *fValue);
 int AC IC_SetExpAbsVal(HGRABBER hGrabber, float fValue);


 ///////////////////////////////////////////////////////////////////
 /*! Gets the current value of Colorenhancement property
	 Sample:
	 @code
	 int OnOFF
	 IC_GetColorEnhancement(hGrabber, &OnOFF);
	 @endcode
	 @param hGrabber	Handle to a grabber object.
	 @param OnOff
		 @li 0 : Color enhancement is off
		 @li 1 : Color enhancement is on

	 @retval IC_SUCCESS : Success
	 @retval IC_NOT:AVAILABLE : The property is not supported by the current device
	 @retval IC_NO_HANDLE	Invalid grabber handle
	 @retval IC_NO_DEVICE    No video capture device opened
 */
 int AC IC_GetColorEnhancement(HGRABBER hGrabber, int *OnOff);
 ///////////////////////////////////////////////////////////////////
 /*! Sets the  value of Colorenhancement property
	 Sample:
	 @code
	 int OnOFF = 1
	 IC_GetColorEnhancement(hGrabber, OnOFF);
	 @endcode
	 @param hGrabber	Handle to a grabber object.
	 @param OnOff
		 @li 0 : Color enhancement is off
		 @li 1 : Color enhancement is on

	 @retval IC_SUCCESS : Success
	 @retval IC_NOT:AVAILABLE : The property is not supported by the current device
	 @retval IC_NO_HANDLE	Invalid grabber handle
	 @retval IC_NO_DEVICE    No video capture device opened
 */
 int AC IC_SetColorEnhancement(HGRABBER hGrabber, int OnOff);

 ///////////////////////////////////////////////////////////////////
 /*! Sends a software trigger to the camera. The camera must support
	 external trigger. The external trigger has to be enabled previously

	 @param hGrabber	Handle to a grabber object.
	 @retval IC_SUCCESS : Success
	 @retval IC_NOT:AVAILABLE : The property is not supported by the current device
	 @retval IC_NO_HANDLE	Invalid grabber handle
	 @retval IC_NO_DEVICE    No video capture device opened

	 @sa IC_EnableTrigger

 */
 int AC IC_SoftwareTrigger(HGRABBER hGrabber);
///////////////////////////////////////////////////////////////////
/*! Sets the value for white balance red.

	@param hGrabber	Handle to a grabber object.
	@param Value	Value of the red white balance to be set
	@retval IC_SUCCESS			: Success
	@retval IC_NO_HANDLE		: Invalid grabber handle
	@retval IC_NO_DEVICE		: No video capture device opened
	@retval IC_NOT_AVAILABLE	: The property is not supported by the current device

*/
int AC IC_SetWhiteBalanceRed(HGRABBER hGrabber, long Value);
///////////////////////////////////////////////////////////////////
/*! Sets the value for white balance green.

	@param hGrabber	Handle to a grabber object.
	@param Value	Value of the green white balance to be set
	@retval IC_SUCCESS			: Success
	@retval IC_NO_HANDLE		: Invalid grabber handle
	@retval IC_NO_DEVICE		: No video capture device opened
	@retval IC_NOT_AVAILABLE	: The property is not supported by the current device

*/
int AC IC_SetWhiteBalanceGreen(HGRABBER hGrabber, long Value);
///////////////////////////////////////////////////////////////////
/*! Sets the value for white balance blue.

	@param hGrabber	Handle to a grabber object.
	@param Value	Value of the blue white balance to be set
	@retval IC_SUCCESS			: Success
	@retval IC_NO_HANDLE		: Invalid grabber handle
	@retval IC_NO_DEVICE		: No video capture device opened
	@retval IC_NOT_AVAILABLE	: The property is not supported by the current device

*/
int AC IC_SetWhiteBalanceBlue(HGRABBER hGrabber, long Value);

///////////////////////////////////////////////////////////////////
/*! Performs the one push  for Focus

	@param hGrabber	Handle to a grabber object.
	@param Value	Value of the blue white balance to be set
	@retval IC_SUCCESS			: Success
	@retval IC_NO_HANDLE		: Invalid grabber handle
	@retval IC_NO_DEVICE		: No video capture device opened
	@retval IC_NOT_AVAILABLE	: The property is not supported by the current device

*/
int AC IC_FocusOnePush(HGRABBER hGrabber);

///////////////////////////////////////////////////////////////////
/*! Enumerate the available properties of a video capture device.

	@param hGrabber	Handle to a grabber object.
	@param cb Callback functions called by the enum function.
	@param data User data

	@retval IC_SUCCESS No error otherwise an error occured, e.g. no device selected.
	
*/
int AC IC_enumProperties(HGRABBER hGrabber, IC_ENUMCB cb, void* data);

int AC IC_printItemandElementNames(HGRABBER hGrabber);

///////////////////////////////////////////////////////////////////
/*! Enumerate the available elements of a video capture device and a property.

	@param hGrabber	Handle to a grabber object.
	@param Property Name of the property
	@param cb Callback functions called by the enum function.
	@param data User data

	@retval IC_SUCCESS No error otherwise an error occured, e.g. no device selected.
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE The passed property in Property is not avaialble.
*/
int AC IC_enumPropertyElements(HGRABBER hGrabber, char* Property, IC_ENUMCB cb, void* data);


///////////////////////////////////////////////////////////////////
/*! Enumerate the available interfaces of of a video capture device, property and element.

	The string passed to the callback function can contain
	- Range
	- Switch
	- Button
	- Mapstrings
	- AbsoluteValues
	- Unknown

	@param hGrabber	Handle to a grabber object.
	@param Property Name of the property
	@param Property Name of the elemt
	@param cb Callback functions called by the enum function.
	@param data User data

	@retval IC_SUCCESS No error otherwise an error occured, e.g. no device selected.
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE The passed property in Property is not avaialble.
*/
int AC IC_enumPropertyElementInterfaces(HGRABBER hGrabber, char* Property, char* Element, IC_ENUMCB cb, void* data);




///////////////////////////////////////////////////////////////////
/*! Check, whether a property is available..  For a list of properties and elements
    use the VCDPropertyInspector of IC Imaging Control.

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. Gain, Exposure
	@param Element  The type of the interface, e.g. Value, Auto. If NULL, it is not checked.

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
	
	Simple call:
	@code
	if( IC_IsPropertyAvailable( hGrabber, "Brightness",NULL) == IC_SUCCESS )
	{
		printf("Brightness is supported\n");
	}
	else
	{
		printf("Brightness is not supported\n");
	}
	@endcode

	Complex call for a special element:
	@code
	if( IC_IsPropertyAvailable( hGrabber, "Trigger","Software Trigger") == IC_SUCCESS )
	{
		printf("Software trigger is supported\n");
	}
	else
	{
		printf("Software trigger is not supported\n");
	}
	@endcode
*/
int AC IC_IsPropertyAvailable(HGRABBER hGrabber, char* Property, char *Element );

///////////////////////////////////////////////////////////////////
/*! This returns the range of a property.  For a list of properties and elements
    use the VCDPropertyInspector of IC Imaging Control.

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. Gain, Exposure
	@param Element  The type of the interface, e.g. Value, Auto. If NULL, it is "Value".
	@param Min  Receives the min value of the property
	@param Max  Receives the max value of the property

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.

	@code
	HGRABBER hGrabber; // The handle of the grabber object.

	int Min;
	int Max;
	int Result = IC_ERROR;
	HGRABBER hGrabber;

	if( IC_InitLibrary(0) )
	{
		hGrabber = IC_CreateGrabber();
		IC_OpenVideoCaptureDevice(hGrabber, "DFx 31BG03.H");

		if( hGrabber )
		{
			Result = IC_GetPropertyValueRange(hGrabber,"Exposure","Auto Reference", &Min, &Max );

			if( Result == IC_SUCCESS )
				printf("Expsure Auto Reference Min %d, Max %d\n", Min, Max);

			Result = IC_GetPropertyValueRange(hGrabber,"Exposure",NULL, &Min, &Max );

			if( Result == IC_SUCCESS )
				printf("Exposure Value Min %d, Max %d\n", Min, Max);
	}
	IC_ReleaseGrabber( hGrabber );
	@endcode


*/
int AC IC_GetPropertyValueRange(HGRABBER hGrabber, char* Property, char *Element, int *Min, int *Max );



///////////////////////////////////////////////////////////////////
/*! This returns the current value of a property. For a list of properties and elements
    use the VCDPropertyInspector of IC Imaging Control.

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. Gain, Exposure
	@param Element  The type of the interface, e.g. Value, Auto. If NULL, it is "Value".
	@param Value  Receives the value of the property

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
*/
int AC IC_GetPropertyValue(HGRABBER hGrabber, char* Property, char *Element, int *Value );

///////////////////////////////////////////////////////////////////
/*! This sets a new value of a property.  For a list of properties and elements
    use the VCDPropertyInspector of IC Imaging Control.

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. Gain, Exposure
	@param Element  The type of the interface, e.g. Value, Auto. If NULL, it is "Value".
	@param Value  Receives the value of the property

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
*/
int AC IC_SetPropertyValue(HGRABBER hGrabber, char* Property, char *Element, int Value );

///////////////////////////////////////////////////////////////////
/*! This returns the range of an absolute value property. Usually it is used for exposure. 
	a list of properties and elements use the VCDPropertyInspector of IC Imaging Control.

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. Gain, Exposure
	@param Element  The type of the interface, e.g. Value, Auto. If NULL, it is "Value".
	@param Min  Receives the min value of the property
	@param Max  Receives the max value of the property

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
*/
int AC IC_GetPropertyAbsoluteValueRange(HGRABBER hGrabber, char* Property, char *Element, float *Min, float *Max );


///////////////////////////////////////////////////////////////////
/*! This returns the current value of an absolute value property.
	Usually it is used for exposure. For a list of properties and elements
    use the VCDPropertyInspector of IC Imaging Control.

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. Gain, Exposure
	@param Element  The type of the interface, e.g. Value, Auto. If NULL, it is "Value".
	@param Value  Receives the value of the property

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
*/
int AC IC_GetPropertyAbsoluteValue(HGRABBER hGrabber, char* Property, char *Element, float *Value );

///////////////////////////////////////////////////////////////////
/*! This sets a new value of an absolute value property. Usually it is used for exposure. 
	a list of properties and elements
    use the VCDPropertyInspector of IC Imaging Control.

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. Gain, Exposure
	@param Element  The type of the interface, e.g. Value, Auto. If NULL, it is "Value".
	@param Value  Receives the value of the property

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
*/
int AC IC_SetPropertyAbsoluteValue(HGRABBER hGrabber, char* Property, char *Element, float Value );

///////////////////////////////////////////////////////////////////
/*! This returns the current value of a switch property. Switch properties
	are usually used for enabling and disabling of automatics.
	 For a list of properties and elements
    use the VCDPropertyInspector of IC Imaging Control.

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. Gain, Exposure
	@param Element  The type of the interface, e.g. Value, Auto. If NULL, it is "Auto".
	@param On  Receives the value of the property

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
*/
int AC IC_GetPropertySwitch(HGRABBER hGrabber, char* Property, char *Element, int *On );

///////////////////////////////////////////////////////////////////
/*! This sets the  value of a switch property. Switch properties
	are usually used for enabling and disabling of automatics.
	 For a list of properties and elements
    use the VCDPropertyInspector of IC Imaging Control.

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. Gain, Exposure
	@param Element  The type of the interface, e.g. Value, Auto. If NULL, it is "Auto".
	@param On  Receives the value of the property

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
*/
int AC IC_SetPropertySwitch(HGRABBER hGrabber, char* Property, char *Element, int On );


//////////////////////////////////////////////////////////////////
/*! This executes the on push on a property. These properties are used
	for white balance one push or for software trigger.
	For a list of properties and elements
    use the VCDPropertyInspector of IC Imaging Control.

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. "Trigger"
	@param Element  The type of the interface, e.g. "Software Trigger" 
	@param On  Receives the value of the property

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
*/
int AC IC_PropertyOnePush(HGRABBER hGrabber, char* Property, char *Element  );


//////////////////////////////////////////////////////////////////
/*! This function is for enumerating the strings of a MapStrings interface
	e.g WhiteBalance Temperature.
	It has to be called twice:
	int StringCount = 0;
	int StringMaxLength = 0;
	char **TemperaturePresets = NULL;

	if (IC_GetPropertyMapStrings(hGrabber, "WhiteBalance", "Temperature Preset", &StringCount, &StringMaxLength, NULL) == IC_SUCCESS)
	{

		// Now the memory for the char * arryy can be created and the function is called again.

		TemperaturePresets = (char**)calloc(StringCount, sizeof(char*));

		for (i = 0; i < StringCount; i++)
		{
			TemperaturePresets[i] = (char*)calloc(StringCount, StringMaxLength);
		}

		// Second call, fill the created array:
		IC_GetPropertyMapStrings(hGrabber, "WhiteBalance", "Temperature Preset", &StringCount, &StringMaxLength, TemperaturePresets);

		for (i = 0; i < StringCount; i++)
			printf("%s\n", TemperaturePresets[i]);

		// Clean up

		for (i = 0; i < StringCount; i++)
			free(TemperaturePresets[i]);

		free(TemperaturePresets);
	}


	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. "Strobe"
	@param Element  The type of the interface, e.g. "Mode" 
	@param StringCount  Receives the count of strings, that is modes, availble
	@Param StringMaxLength Receives the maximum length of th strings
	@param Strings pointer to an array of char*, that will contain the mode strings. The array size should be StringCount * 20. Parameter can be null in order to query the number of strings

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
*/
int AC IC_GetPropertyMapStrings(HGRABBER hGrabber, char* Property, char *Element, int *StringCount, int *StringMaxLength, char **Strings);

// Removed int AC IC_GetPropertyMapStrings(HGRABBER hGrabber, char* Property, char *Element, int *StringCount, char **Strings  );


//////////////////////////////////////////////////////////////////
/*! Return the current set string of a mapstring interface
	It has to be called twice:

	int StringLength = 0;

	IC_GetPropertyMapString( hGrabber, "WhiteBalance", "Temperature  Preset", &StringLength, NULL);

	Now allocate the memory for the string:
	char *String = new char(Stringlength);

	IC_GetPropertyMapString( hGrabber, "WhiteBalance", "Temperature Preset", &StringLength, String);

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. "Strobe"
	@param Element  The type of the interface, e.g. "Mode" 
	@param StringLength Length of the string to be receuved
	@param String	 pointer to a char*. Size should be atleast 50. There is no check! This contains the result.

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
*/
int AC IC_GetPropertyMapString(HGRABBER hGrabber, char* Property, char *Element, int *StringLength, char *String);

// removed int AC  IC_GetPropertyMapString(HGRABBER hGrabber, char* Property, char *Element,  char *String );

//////////////////////////////////////////////////////////////////
/*! Set the string of a mapstring interface

	@param hGrabber	Handle to a grabber object.
	@param Property  The name of the property, e.g. "Strobe"
	@param Element  The type of the interface, e.g. "Mode" 
	@param String	 pointer to a char*. Size should be atleast 50. There is no check! This contains the result.

 	@retval IC_SUCCESS			Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE		A requested property item is not available
	@retval IC_PROPERTY_ELEMENT_NOT_AVAILABLE		A requested element of a given property item is not available
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE		requested element has not the interface, which is needed.
*/
int AC  IC_SetPropertyMapString(HGRABBER hGrabber, char* Property, char *Element,  char *String );

//////////////////////////////////////////////////////////////////
/*! Reset properties to "Factory Defaults"

	@param hGrabber	Handle to a grabber object.
	@retval IC_SUCCESS		Success
	@retval IC_NO_HANDLE		Invalid grabber handle
	@retval IC_NO_DEVICE		No video capture device opened
*/
int AC IC_ResetProperties(HGRABBER hGrabber);

/*! @} */

/**
 * \defgroup FRAMEFILTERS Frame Filter Functions
 * @{
 */
//////////////////////////////////////////////////////////////////
/*! Query number of avaialable frame filters

	@retval The count of found frame filters.
*/
int AC IC_GetAvailableFrameFilterCount();

//////////////////////////////////////////////////////////////////
/*! Query a list of framefilters

	@param szFilterList A two dimensional char array that recieves the list of found frame filters
	@param iSize The number of entries in the above list.
@code
    char szFilterList[80][40];
	int iCount;
    iCount = IC_GetAvailableFrameFilterCount();

	iFormatCount = IC_GetAvailableFrameFilters(szFormatList,iCount );

	for( i = 0; i < iCount; i++ )
	{
		printf("%2d. %s\n",i+1,szFormatList[i]);
	}
	@endcode
*/
int AC IC_GetAvailableFrameFilters(char **szFilterList, int iSize );

//////////////////////////////////////////////////////////////////
/*! Create a frame filter
	@param szFilterName Name of the filter to create
	@param FilterHandle Address of a pointer, that will receive the handle of the created filter

	@retval IC_SUCCESS	Success
	@retval IC_ERROR	If the filter creation failed.

	@sa IC_DeleteFrameFilter
*/

int AC IC_CreateFrameFilter(char *szFilterName, HFRAMEFILTER *FilterHandle );

//////////////////////////////////////////////////////////////////
/*! Add the frame filter to the device's filter list. It is possible to add several
	frame filter to a grabber, so a filter chain for image processing can be created.
	A frame filter is removed by a call to IC_RemoveFrameFilter

	@param hGrabber	Handle to a grabber object.
	@param FilterHandle	Handle to a frame filter object.

	@retval IC_SUCCESS	Success
	@retval IC_ERROR Either hGrabber or FilterHandle was NULL

	@sa IC_RemoveFrameFilterFromDevice
	@sa IC_CreateFrameFilter
*/
int AC IC_AddFrameFilterToDevice(HGRABBER hGrabber, HFRAMEFILTER FilterHandle );

//////////////////////////////////////////////////////////////////
/*! Removes a previously added frame filter from the frame filter list
	
	@param hGrabber	Handle to a grabber object, that uses the passed frame filter.
	@param FilterHandle	Handle to a frame filter object, to be removed

	@sa IC_AddFrameFilterToDevice
	@sa IC_CreateFrameFilter

*/
void AC IC_RemoveFrameFilterFromDevice(HGRABBER hGrabber, HFRAMEFILTER FilterHandle );


//////////////////////////////////////////////////////////////////
/*! Deletes a previously created frame filter. Make sure, the frame filter to be deleted 
	is not in use anymore, otherwise a null pointer access violation will occur.
	
	@param FilterHandle	Handle to a frame filter object.
*/
void AC IC_DeleteFrameFilter( HFRAMEFILTER FilterHandle );

///////////////////////////////////////////////////////////////
/* Delete the memory allocated by the HFRAMEFILTER structure. Please remove the frame filter from the HGrabber, 
   before deleting it.

	@param FilterHandle	Handle to a frame filter object.

	@retval IC_SUCCESS	Success
	@retval IC_ERROR Either hGrabber or FilterHandle was NULL or the frame filter has no dialog.

*/
int AC IC_FrameFilterShowDialog( HFRAMEFILTER FilterHandle );

///////////////////////////////////////////////////////////////
/*! Query a parameter value of a frame filter
	@param FilterHandle	Handle to a frame filter object.
	@param ParameterName Name of the parameter whose value is to be queried
	@param Data pointer to the data, that receives the value. Memory must be allocated before.

	@retval IC_SUCCESS	Success
	@retval IC_ERROR  Maybe the parameter name does not exist.

*/
int AC IC_FrameFilterGetParameter(HFRAMEFILTER FilterHandle, char* ParameterName, void* Data );

/*! Set an int parameter value of a frame filter
	@param FilterHandle	Handle to a frame filter object.
	@param ParameterName Name of the parameter whose value is to be set
	@param Data The data, that contains the value.

	@retval IC_SUCCESS	Success
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE the parameter givven by ParameterName does not exist
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE The data type, e.g. int does not match to the parameter type, e.g. float
	@retval IC_ERROR  Unknown error
*/
int AC IC_FrameFilterSetParameterInt(HFRAMEFILTER FilterHandle, char* ParameterName, int Data );

/*! Set a float parameter value of a frame filter
	@param FilterHandle	Handle to a frame filter object.
	@param ParameterName Name of the parameter whose value is to be set
	@param Data The data, that contains the value.

	@retval IC_SUCCESS	Success
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE the parameter givven by ParameterName does not exist
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE The data type, e.g. int does not match to the parameter type, e.g. float
	@retval IC_ERROR  Unknown error
*/
int AC IC_FrameFilterSetParameterFloat(HFRAMEFILTER FilterHandle, char* ParameterName, float Data );

/*! Set a boolean parameter value of a frame filter. boolean means int here.
	@param FilterHandle	Handle to a frame filter object.
	@param ParameterName Name of the parameter whose value is to be set
	@param Data The data, that contains the value.

	@retval IC_SUCCESS	Success
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE the parameter givven by ParameterName does not exist
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE The data type, e.g. int does not match to the parameter type, e.g. float
	@retval IC_ERROR  Unknown error
*/
int AC IC_FrameFilterSetParameterBoolean(HFRAMEFILTER FilterHandle, char* ParameterName, int Data );

/*! Set a string parameter value of a frame filter
	@param FilterHandle	Handle to a frame filter object.
	@param ParameterName Name of the parameter whose value is to be set
	@param Data The data, that contains the value.

	@retval IC_SUCCESS	Success
	@retval IC_PROPERTY_ITEM_NOT_AVAILABLE the parameter givven by ParameterName does not exist
	@retval IC_PROPERTY_ELEMENT_WRONG_INTERFACE The data type, e.g. int does not match to the parameter type, e.g. float
	@retval IC_ERROR  Unknown error
*/
int AC IC_FrameFilterSetParameterString(HFRAMEFILTER FilterHandle, char* ParameterName, char* Data );

////////////////////////////////////////////////////////////////////////////
/*! Remove all frame filters from the Grabber's device path
	@param hGrabber	Handle to a grabber object.

*/
int AC IC_FrameFilterDeviceClear(HGRABBER hGrabber );

////////////////////////////////////////////////////////////////////////////
/*! Structure of the codec handle
*/
typedef struct CODECHANDLE_t__ { int unused; } CODECHANDLE_t; ///<Internal structure of the grabber object handle.
#define	HCODEC CODECHANDLE_t* ///< Type of grabber object handle. Used for all functions.



/*! @} */

/**
 * \defgroup AVICAPTURE AVI Capture Functions
 * @{
 */

//////////////////////////////////////////////////////////////////////////
/*! 
*/


////////////////////////////////////////////////////////////////////////////
/*! Callback type definition for the codec enumenration callback called by
	IC_enumCodecs
	@retval 1 : Terminate the enumeration, 0 continue enumrating
*/

typedef int  _cdecl ENUMCODECCB( char* CodecName, void*);

////////////////////////////////////////////////////////////////////////////
/*! Enumerate all installed codecs. It calls the callback function passed by 
	the cb parameter. It ends, if cb returns 0 or all codecs have been enumerated.

	@param cb pallack function of type ENUMCODECCB
	@param data Pointer to user data
*/
void AC IC_enumCodecs(ENUMCODECCB cb, void* data);

////////////////////////////////////////////////////////////////////////////
/*! Creates the codec by the passed name

	@param Name Name of the codec to be created
	@retval NULL on error, otherwise the created HCODEC
*/
HCODEC IC_Codec_Create(char* Name);
void AC IC_Codec_Release(HCODEC Codec);

////////////////////////////////////////////////////////////////////////////
/*! Queries a name of a codec passed by _Codec

	@param _Codec Handle to the codec
	@param l Size in bytes of the memory allocated for name
	@param name String that will receive the name of the codec terminated by a \0

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if _Codec or Name is NULL
*/
int AC IC_Codec_getName(HCODEC Codec, int l, char* Name);

////////////////////////////////////////////////////////////////////////////
/*! Return whether a codec passed by _Codec has a property dialog

	@param _Codec Handle to the codec

	@retval IC_SUCCESS The codec has a dialog
	@retval IC_ERROR The codec has no dialog
	@retval IC_NO_HANDLE  _Codec is NULL
*/
int AC IC_Codec_hasDialog(HCODEC Codec);

////////////////////////////////////////////////////////////////////////////
/*! Shows the property dialog of a codec passed by _Codec

	@param name String that will receive the name of the codec terminated by a \0

	@retval IC_SUCCESS on success
	@retval IC_ERROR On error, e.g. something went wrong with the codec's dialog.
	@retval IC_NO_HANDLE if _Codec or Name is NULL
*/
int AC IC_Codec_showDialog(HCODEC Codec);


////////////////////////////////////////////////////////////////////////////
/*! Assigns the selected Codec to the Grabber. AVI Capture is prepared. Image
    capture does not work anymore.

	After doing so, a call to IC_Startlive() starts AVI Capture and IC_Stoplive stopps it,

	@param hlGrabber Handle to a grabber with a valid device
	@param Codec Handle to the selected codec.

	@retval IC_SUCCESS on success
*/
int AC IC_SetCodec(HGRABBER hlGrabber,HCODEC Codec);


////////////////////////////////////////////////////////////////////////////
/*! Set the file name for the AVI file

	After doing so, a call to IC_Startlive() starts AVI Capture and IC_Stoplive stopps it,

	@param hlGrabber Handle to a grabber with a valid device
	@param FileName Filename

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if the grabber is invalid

*/
int IC_SetAVIFileName(HGRABBER hlGrabber,char * FileName);

////////////////////////////////////////////////////////////////////////////
/*! Pauses or continues AVI Capture. This allows, to start the stream and see the live video
    but images are not saved into the AVI file.

	
	@param hlGrabber Handle to a grabber with a valid device
	@param pause  1 = Pause, nothing saved, 0 = save images!

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if the grabber is invalid
*/

int IC_enableAVICapturePause(HGRABBER hlGrabber, int Pause );
/*! @} */

/**
 * \defgroup IMAGEBUFFERS Memory Buffer Functions
 The memory buffer represents a grabbed image from the device stream. It contains information about width, height and pixel format.
 @code
	HMEMBUFFER hBuffer;
	int BufferWidth = 0;
	int BufferHeight = 0;
	int BufferBitsPerPixel = 0;
	unsigned char *pData;
  
	IC_OpenVideoCaptureDevice(hGrabber,"DFK 33UX290");
	IC_SetFormat(hGrabber, Y800);

	IC_StartLive(hGrabber, 0);

	IC_SnapImage(hGrabber, 500);
	IC_GetMemBufferLastAcq(hGrabber, &hBuffer);
	IC_MemBufferGetIndex(hBuffer, &BufferIndex);

	// Get width, height and bits per pixel
	IC_GetMemBufferDescription(hBuffer, &BufferWidth, &BufferHeight, &BufferBitsPerPixel);

	// Get the pointer to the image data
	IC_MemBufferGetDataPtr(hBuffer, &pData);

	// Here image processing can run. 
 @endcode
 * @{
 */

/*   *** Image Buffers  ****/

////////////////////////////////////////////////////////////////////////////
/*! Set the number of image buffers of the internal ring buffer. Recommended
	is 2, default ist 5. The live video must be stopped, before setting the
	value.
	Calling this functions disables AVI capture.

	@param hlGrabber Handle to a grabber with a valid device
	@param Count  Number of buffers.

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if the grabber is invalid
	@retval IC_ERROR if the size is wrong or the grabber is in live mode.

	@sa IC_SetFormat
*/
int AC IC_SetRingBufferSize(HGRABBER hGrabber, int Count);

////////////////////////////////////////////////////////////////////////////
/*! Get the number of image buffers of the internal ring buffer. 

	@param hlGrabber Handle to a grabber with a valid device
	@param Count  Pointer to an int, that receives number of buffers.

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if the grabber is invalid
*/
int AC IC_GetRingBufferSize(HGRABBER hGrabber, int *pCount);


////////////////////////////////////////////////////////////////////////////
/*! Get the a handle of an image buffer

	@param hlGrabber Handle to a grabber with a valid device
	@param Index  Index to a buffer, must be between 0 and IC_GetRingBufferSize
	@param pBuffer  Pointer to an HUIMAGEBUFFER, that receives the pointer to the image the buffer.

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if the grabber is invalid
	@retval IC_ERROR if the sink has not been created yet
*/
int AC IC_GetMemBuffer(HGRABBER hGrabber, int Index, HMEMBUFFER *pBuffer);

////////////////////////////////////////////////////////////////////////////
/*! Get the abuffer handle of the last captured (acquired) image buffer 

	@param hlGrabber Handle to a grabber with a valid device
	@param Index  Index to a buffer, must be between 0 and IC_GetRingBufferSize
	@param pBuffer  Pointer to an HUIMAGEBUFFER, that receives the pointer to the image the buffer.

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if the grabber is invalid
	@retval IC_ERROR if the sink has not been created yet
*/
int AC IC_GetMemBufferLastAcq(HGRABBER hGrabber,  HMEMBUFFER *pBuffer);

////////////////////////////////////////////////////////////////////////////
/*! Releases an HMEMBUFFER Hande
	Must be called in the callback functions.

	@param pBuffer Handle of the image buffer to release


	@sa IC_GetMemBuffer, IC_GetMemBufferLastAcq

*/
void AC IC_ReleaseMemBuffer(HMEMBUFFER *pBuffer);
////////////////////////////////////////////////////////////////////////////
/*! Get the descriptio of an image buffer

	@param hBuffer Handle to an image buffer retrieved by IC_GetMemBuffer
	@param pWidth  Pointer to an int, that receives the width of the image the buffer.
	@param pHeight  Pointer to an int, that receives the height of the image the buffer.
	@param pBitsPerPixel Pointer to an int, that receives the bits per pixel of the image the buffer.

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if the buffer handle is invalid
*/
int AC IC_GetMemBufferDescription(HMEMBUFFER hBuffer, int *pWidth, int *pHeight, int *pBitsPerPixel);

////////////////////////////////////////////////////////////////////////////
/*! Lock and unlock an image buffer. A locked image buffer will not be 
    overwritten by new images.

	@param hBuffer Handle to an image buffer retrieved by IC_GetMemBuffer
	@param lock   1 = lock, 0 = unlock

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if the buffer handle is invalid
*/
int AC IC_MemBufferLock(HMEMBUFFER hBuffer, int lock);

////////////////////////////////////////////////////////////////////////////
/*! Check, whether an image buffer is locked.

	@param hBuffer Handle to an image buffer retrieved by IC_GetMemBuffer
	@param lock   pointer to an int, that receivres : 1 = locked, 0 = unlocked

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if the buffer handle is invalid
*/
int AC IC_MemBufferisLocked(HMEMBUFFER hBuffer, int *plocked);

////////////////////////////////////////////////////////////////////////////
/*! Get the index in the ring buffer of the passed buffer.

	@param hBuffer Handle to an image buffer retrieved by IC_GetMemBuffer
	@param pIndex  pointer to an int, that receives the index

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if the buffer handle is invalid
*/
int AC IC_MemBufferGetIndex(HMEMBUFFER hBuffer,int *pIndex);

////////////////////////////////////////////////////////////////////////////
/*! Check, whether an image buffer is locked.

	@param hBuffer Handle to an image buffer retrieved by IC_GetMemBuffer
	@param pData  pointer to a unsigned char pointer, that receives the pointer to the image data

	@retval IC_SUCCESS on success
	@retval IC_NO_HANDLE if the buffer handle is invalid
*/
int AC IC_MemBufferGetDataPtr(HMEMBUFFER hBuffer, unsigned char **pData);



#if defined(__cplusplus)
}
#endif 

#endif
