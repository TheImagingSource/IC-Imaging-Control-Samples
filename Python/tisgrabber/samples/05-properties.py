import ctypes 

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")

class HGRABBER(ctypes.Structure):
    '''
    This class is used to handle the pointer to the internal 
    Grabber class, which contains the camera. 
    A pointer to this class is used by tisgrabber DLL.
    '''
    _fields_ = [('unused', ctypes.c_int)]


# Functions returning a HGRABBER Handle must set their restype to POINTER(HGRABBER)
ic.IC_ShowDeviceSelectionDialog.restype = ctypes.POINTER(HGRABBER)
ic.IC_ReleaseGrabber.argtypes = (ctypes.POINTER(ctypes.POINTER(HGRABBER)),)
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
                            ctypes.POINTER(ctypes.c_float), )

ic.IC_GetPropertyAbsoluteValueRange.argtypes = (ctypes.POINTER(HGRABBER),
                            ctypes.c_char_p,
                            ctypes.c_char_p,
                            ctypes.POINTER(ctypes.c_float),
                            ctypes.POINTER(ctypes.c_float), )

ic.IC_GetPropertySwitch.argtypes=(ctypes.POINTER(HGRABBER),
                            ctypes.c_char_p,
                            ctypes.c_char_p,
                            ctypes.POINTER(ctypes.c_long), )



ic.IC_InitLibrary(0)

hGrabber = ic.IC_ShowDeviceSelectionDialog(None)
 
if( ic.IC_IsDevValid(hGrabber)): 
    exposureauto = ctypes.c_long()
    ic.IC_SetPropertySwitch(hGrabber, "Exposure".encode("utf-8"), "Auto".encode("utf-8"), exposureauto)
    print("Exposure Auto is {0}".format(exposureauto.value))

    ic.IC_SetPropertySwitch(hGrabber, "Exposure".encode("utf-8"), "Auto".encode("utf-8"), 0)
    ic.IC_SetPropertyAbsoluteValue(hGrabber, "Exposure".encode("utf-8"), "Value".encode("utf-8"), ctypes.c_float(0.0303))

    expmin = ctypes.c_float()
    expmax = ctypes.c_float()
    exposure = ctypes.c_float()
    ic.IC_GetPropertyAbsoluteValue(hGrabber, "Exposure".encode("utf-8"), "Value".encode("utf-8"), exposure )
    ic.IC_GetPropertyAbsoluteValueRange(hGrabber, "Exposure".encode("utf-8"), "Value".encode("utf-8"), expmin, expmax )
    print("Exposure is {0}, range is {1} - {2}".format(exposure.value, expmin.value, expmax.value))

    gainmin = ctypes.c_long()
    gainmax = ctypes.c_long()
    gain = ctypes.c_long()
    ic.IC_GetPropertyValue(hGrabber,"Gain".encode("utf-8"),"Value".encode("utf-8"),gain )
    ic.IC_GetPropertyValueRange(hGrabber,"Gain".encode("utf-8"),"Value".encode("utf-8"), gainmin, gainmax )
    print("Gain is {0} range is {1} - {2}".format(gain.value, gain.value,gain.value))


    ret = ic.IC_PropertyOnePush(hGrabber,"Focus".encode("utf-8"),"One Push".encode("utf-8") )
    if ret == -4:
        print("Your camera does not support Focus properties!")

else:
    print("No device opened,")

ic.IC_ReleaseGrabber(hGrabber)
