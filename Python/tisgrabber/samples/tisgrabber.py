import ctypes

from enum import Enum


class SinkFormats(Enum):
    Y800 = 0
    RGB24 = 1
    RGB32 = 2
    UYVY = 3
    Y16 = 4


class FRAMEFILTER_PARAM_TYPE(Enum):
    eParamLong = 0
    eParamBoolean = 1
    eParamFloat = 2
    eParamString = 3
    eParamData = 4


ImageFileTypes = {'BMP': 0, 'JPEG': 1}

IC_SUCCESS = 1
IC_ERROR = 0
IC_NO_HANDLE = -1
IC_NO_DEVICE = -2
IC_NOT_AVAILABLE = -3
IC_NO_PROPERTYSET = -3
IC_DEFAULT_WINDOW_SIZE_SET = -3
IC_NOT_IN_LIVEMODE = -3
IC_PROPERTY_ITEM_NOT_AVAILABLE = -4
IC_PROPERTY_ELEMENT_NOT_AVAILABLE = -5
IC_PROPERTY_ELEMENT_WRONG_INTERFACE = -6
IC_INDEX_OUT_OF_RANGE = -7
IC_WRONG_XML_FORMAT = -1
IC_WRONG_INCOMPATIBLE_XML = -3
IC_NOT_ALL_PROPERTIES_RESTORED = -4
IC_DEVICE_NOT_FOUND = -5
IC_FILE_NOT_FOUND = 35


class HGRABBER(ctypes.Structure):
    '''
    This class is used to handle the pointer to the internal
    Grabber class, which contains the camera. 
    A pointer to this class is used by tisgrabber DLL.
    '''
    _fields_ = [('unused', ctypes.c_int)]


class HCODEC(ctypes.Structure):
    '''
    This class is used to handle the pointer to the internal
    codec class for AVI capture
    A pointer to this class is used by tisgrabber DLL.
    '''
    _fields_ = [('unused', ctypes.c_int)]


class FILTERPARAMETER(ctypes.Structure):
    '''
    This class implements the structure of a frame filter
    parameter used by the HFRAMEFILTER class
    '''
    _fields_ = [
        ('Name', ctypes.c_char*30),
        ('Type', ctypes.c_int)
    ]


class HFRAMEFILTER(ctypes.Structure):
    '''
    This class implements the structure of a frame filter used
    by the tisgrabber.dll.
    '''
    _fields_ = [
        ('pFilter', ctypes.c_void_p),
        ('bHasDialog', ctypes.c_int),
        ('ParameterCount', ctypes.c_int),
        ('Parameters', ctypes.POINTER(FILTERPARAMETER))
    ]


def declareFunctions(ic):
    '''
    Functions returning a HGRABBER Handle must set their restype to POINTER(HGRABBER)
    :param ic: The loaded tisgrabber*.dll
    '''
    ic.IC_ShowDeviceSelectionDialog.restype = ctypes.POINTER(HGRABBER)
    ic.IC_ReleaseGrabber.argtypes = (ctypes.POINTER(ctypes.POINTER(HGRABBER)),)

    ic.IC_LoadDeviceStateFromFile.restype = ctypes.POINTER(HGRABBER)
    ic.IC_CreateGrabber.restype = ctypes.POINTER(HGRABBER)

    ic.IC_GetPropertyValueRange.argtypes = (ctypes.POINTER(HGRABBER),
                                ctypes.c_char_p,
                                ctypes.c_char_p,
                                ctypes.POINTER(ctypes.c_long),
                                ctypes.POINTER(ctypes.c_long), )

    ic.IC_GetPropertyValue.argtypes = (ctypes.POINTER(HGRABBER),
                                ctypes.c_char_p,
                                ctypes.c_char_p,
                                ctypes.POINTER(ctypes.c_long), )


    ic.IC_GetPropertyAbsoluteValue.argtypes = (ctypes.POINTER(HGRABBER),
                                ctypes.c_char_p,
                                ctypes.c_char_p,
                                ctypes.POINTER(ctypes.c_float),)

    ic.IC_GetPropertyAbsoluteValueRange.argtypes = (ctypes.POINTER(HGRABBER),
                                ctypes.c_char_p,
                                ctypes.c_char_p,
                                ctypes.POINTER(ctypes.c_float),
                                ctypes.POINTER(ctypes.c_float),)

    ic.IC_GetPropertySwitch.argtypes = (ctypes.POINTER(HGRABBER),
                                ctypes.c_char_p,
                                ctypes.c_char_p,
                                ctypes.POINTER(ctypes.c_long), )

    ic.IC_GetImageDescription.argtypes = (ctypes.POINTER(HGRABBER),
                                    ctypes.POINTER(ctypes.c_long),
                                    ctypes.POINTER(ctypes.c_long),
                                    ctypes.POINTER(ctypes.c_int),
                                    ctypes.POINTER(ctypes.c_int),)

    ic.IC_GetImagePtr.restype = ctypes.c_void_p

    ic.IC_SetHWnd.argtypes = (ctypes.POINTER(HGRABBER), ctypes.c_int)
    # definition of the frameready callback
    ic.FRAMEREADYCALLBACK = ctypes.CFUNCTYPE(ctypes.c_void_p, ctypes.POINTER(HGRABBER), ctypes.POINTER(ctypes.c_ubyte), ctypes.c_ulong, ctypes.py_object)
    ic.DEVICELOSTCALLBACK = ctypes.CFUNCTYPE(ctypes.c_void_p, ctypes.POINTER(HGRABBER), ctypes.py_object)

    ic.IC_SetFrameReadyCallback.argtypes = [ctypes.POINTER(HGRABBER), ic.FRAMEREADYCALLBACK, ctypes.py_object]
    ic.IC_SetCallbacks.argtypes = [ctypes.POINTER(HGRABBER),
                                   ic.FRAMEREADYCALLBACK,
                                   ctypes.py_object,
                                   ic.DEVICELOSTCALLBACK,
                                   ctypes.py_object]

    ic.IC_Codec_Create.restype = ctypes.POINTER(HCODEC)

    ic.ENUMCODECCB = ctypes.CFUNCTYPE(ctypes.c_void_p, ctypes.c_char_p, ctypes.py_object)
    ic.IC_enumCodecs.argtypes = (ic.ENUMCODECCB, ctypes.py_object)

    ic.IC_GetDeviceName.restype = ctypes.c_char_p
    ic.IC_GetDevice.restype = ctypes.c_char_p
    ic.IC_GetUniqueNamefromList.restype = ctypes.c_char_p

    ic.IC_CreateFrameFilter.argtypes = (ctypes.c_char_p, ctypes.POINTER(HFRAMEFILTER))


def T(instr):
    ''' Helper function
    Encodes the input string to utf-8
    :param instr: Python string to be converted
    :return: converted string
    '''
    return instr.encode("utf-8")


def D(instr):
    ''' Helper function
    Decodes instr utf-8
    :param instr: Python string to be converted
    :return: converted string
    '''
    return instr.decode('utf-8', 'ignore')


def openDevice(ic):
    ''' Helper functions
    Open a camera. If a file with a device state exists, it will be used.
    If not, the device selection dialog is shown and if a valid devices
    was selected, the device state file is created.
    :return: a HGRABBER
    '''
    try:
        hGrabber = ic.IC_LoadDeviceStateFromFile(None, T("device.xml"))
        if not ic.IC_IsDevValid(hGrabber):
            hGrabber = ic.IC_ShowDeviceSelectionDialog(None)
    except Exception as ex:
        hGrabber = ic.IC_ShowDeviceSelectionDialog(None)

    if(ic.IC_IsDevValid(hGrabber)):
        ic.IC_SaveDeviceStateToFile(hGrabber, T("device.xml"))
    return hGrabber
