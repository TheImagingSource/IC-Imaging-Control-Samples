import ctypes 
import tisgrabber as tis
import time
import os.path

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")

tis.declareFunctions(ic)

ic.IC_InitLibrary(0)

hGrabber = tis.openDevice(ic)

if(ic.IC_IsDevValid(hGrabber)):
    # Set a "big" video format
    ic.IC_SetVideoFormat(hGrabber, tis.T("Y800 (1024x768)"))
    ic.IC_SetFrameRate(hGrabber, ctypes.c_float(30.0))

    ic.IC_StartLive(hGrabber, 1)
    ic.IC_MsgBox(tis.T("Click OK to to use a smaller format"),
                 tis.T("ROI Demo"))
    ic.IC_StopLive(hGrabber)

    # Set a an ROI on the sensor. Make sure, the width and height are valid
    #  for the used camera!
    ic.IC_SetVideoFormat(hGrabber, tis.T("Y800 (640x480)"))
    ic.IC_SetFrameRate(hGrabber, ctypes.c_float(30.0))

    # Moving the ROI on the sensor need to disable the Partial Scan Auto Center
    # property:
    ic.IC_SetPropertySwitch(hGrabber, tis.T("Partial scan"),
                            tis.T("Auto-center"), 0)

    ic.IC_StartLive(hGrabber, 1)

    # Move the ROI on the sensor by x and y coordinates. This can be done, 
    # while the live video is running.
    x = 0
    for y in range(0, 768-480):
        ic.IC_SetPropertyValue(hGrabber, tis.T("Partial scan"),
                               tis.T("Y Offset"), y)
        ic.IC_SetPropertyValue(hGrabber, tis.T("Partial scan"),
                               tis.T("X Offset"), x)
        x = x + 1
        time.sleep(0.05)

    ic.IC_MsgBox(tis.T("Click OK to to use a bigger format"), tis.T("ROI Demo"))
    ic.IC_StopLive(hGrabber)

    # Set original video format
    ic.IC_SetVideoFormat(hGrabber, tis.T("Y800 (1024x768)"))
    ic.IC_SetFrameRate(hGrabber, ctypes.c_float(30.0))

    # Enable Partial Scan Auto Center, so the ROI is centered on the sensor again. 
    ic.IC_SetPropertySwitch(hGrabber, tis.T("Partial scan"),
                            tis.T("Auto-center"), 1)

    ic.IC_StartLive(hGrabber, 1)
    ic.IC_MsgBox(tis.T("Click OK to to Stop"), tis.T("ROI Demo"))
    ic.IC_StopLive(hGrabber)
else:
    ic.IC_MsgBox(tis.T("No device opened"), tis.T("ROI Demo"))
ic.IC_ReleaseGrabber(hGrabber)
