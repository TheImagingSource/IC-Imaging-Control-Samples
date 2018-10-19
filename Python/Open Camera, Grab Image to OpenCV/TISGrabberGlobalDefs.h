

#ifndef __GLOBALDEFS_H__
#define __GLOBALDEFS_H__


typedef enum
{
	FILETYPE_BMP,
	FILETYPE_JPEG,
	FILETYPE_MEGA=65536 // Borland C++ 6 compatibility
} IMG_FILETYPE;


typedef enum
{
	Y800,
	RGB24,
	RGB32,
	UYVY,
	Y16,
	NONE,	// used as return value
	COLORFORMAT_MEGA=65536 // Borland C++ 6 compatibility
} COLORFORMAT;

typedef enum
{
	PROP_VID_BRIGHTNESS = 0,
	PROP_VID_CONTRAST,
	PROP_VID_HUE,
	PROP_VID_SATURATION,
	PROP_VID_SHARPNESS,
	PROP_VID_GAMMA,
	PROP_VID_COLORENABLE,
	PROP_VID_WHITEBALANCE,
	PROP_VID_BLACKLIGHTCOMPENSATION,
	PROP_VID_GAIN,
	PROP_VID_MEGA=65536 // Borland C++ 6 compatibility
} VIDEO_PROPERTY;


typedef enum
{
	PROP_CAM_PAN = 0,
	PROP_CAM_TILT,
	PROP_CAM_ROLL,
	PROP_CAM_ZOOM,
	PROP_CAM_EXPOSURE,
	PROP_CAM_IRIS,
	PROP_CAM_FOCUS,
	PROP_CAM_MEGA=65536 // Borland C++ 6 compatibility
} CAMERA_PROPERTY;


typedef	enum 
{
	eParamLong,
	eParamBoolean,
	eParamFloat,
	eParamString,
	eParamData
} FRAMEFILTER_PARAM_TYPE;

typedef enum
{
	ePropertyRange,
	ePropertyAbsoluteValue,
	ePropertySwitch,
	ePropertyButton,
	ePropertyMapStrings,
	ePropertyUnknown
} PROPERTY_INTERFACE_TYPE;
#endif