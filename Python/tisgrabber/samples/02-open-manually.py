'''
This sample demonstrates, how to open a camera by its model name
set a video format and frame rate and show a live video stream.
Needed DLLs for 64 bit environment are
- tisgrabber_x64.dll
- TIS_UDSHL11_x64.dll
'''
import ctypes
import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)

ic.IC_InitLibrary(0)

hGrabber = ic.IC_CreateGrabber()

ic.IC_OpenVideoCaptureDevice(hGrabber, tis.T("DFK Z30GP031"))

if(ic.IC_IsDevValid(hGrabber)):
    ic.IC_SetVideoFormat(hGrabber, tis.T("RGB32 (640x480)"))
    ic.IC_SetFrameRate(hGrabber, ctypes.c_float(30.0))

    ic.IC_StartLive(hGrabber, 1)
    ic.IC_MsgBox(tis.T("Click OK to stop"), tis.T("Simple Live Video"))
    ic.IC_StopLive(hGrabber)
else:
    ic.IC_MsgBox(tis.T("No device opened"), tis.T("Simple Live Video"))

ic.IC_ReleaseGrabber(hGrabber)
