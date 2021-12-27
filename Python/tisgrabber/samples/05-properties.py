import ctypes 
import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)

ic.IC_InitLibrary(0)

hGrabber = ic.IC_ShowDeviceSelectionDialog(None)

if(ic.IC_IsDevValid(hGrabber)):
    exposureauto = ctypes.c_long()
    ic.IC_SetPropertySwitch(hGrabber, tis.T("Exposure"), tis.T("Auto"),
                            exposureauto)
    print("Exposure Auto is {0}".format(exposureauto.value))

    ic.IC_SetPropertySwitch(hGrabber, tis.T("Exposure"), tis.T("Auto"), 0)
    ic.IC_SetPropertyAbsoluteValue(hGrabber, tis.T("Exposure"), tis.T("Value"),
                                   ctypes.c_float(0.0303))

    expmin = ctypes.c_float()
    expmax = ctypes.c_float()
    exposure = ctypes.c_float()
    ic.IC_GetPropertyAbsoluteValue(hGrabber, tis.T("Exposure"), tis.T("Value"),
                                   exposure)
    ic.IC_GetPropertyAbsoluteValueRange(hGrabber, tis.T("Exposure"), tis.T("Value"),
                                        expmin, expmax)
    print("Exposure is {0}, range is {1} - {2}".format(exposure.value,
                                                       expmin.value, expmax.value))

    gainmin = ctypes.c_long()
    gainmax = ctypes.c_long()
    gain = ctypes.c_long()
    ic.IC_GetPropertyValue(hGrabber, tis.T("Gain"), tis.T("Value"), gain)
    ic.IC_GetPropertyValueRange(hGrabber, tis.T("Gain"), tis.T("Value"),
                                gainmin, gainmax)
    print("Gain is {0} range is {1} - {2}".format(gain.value, gain.value, gain.value))

    ret = ic.IC_PropertyOnePush(hGrabber, tis.T("Focus"), tis.T("One Push"))
    if ret == -4:
        print("Your camera does not support Focus properties!")

else:
    print("No device opened,")

ic.IC_ReleaseGrabber(hGrabber)
