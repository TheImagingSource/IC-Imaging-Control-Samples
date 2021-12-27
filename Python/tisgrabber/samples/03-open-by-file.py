"""
This sample shows, how to use a XML configuration file 
for opening a video capture device.
"""

import ctypes
import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)

ic.IC_InitLibrary(0)

hGrabber = ic.IC_CreateGrabber()

ic.IC_OpenVideoCaptureDevice(hGrabber,"DFK 33GR0521".encode("utf-8"))

if( ic.IC_IsDevValid(hGrabber)): 
    ic.IC_SetVideoFormat(hGrabber, "RGB32 (640x480)".encode("utf-8"))
    ic.IC_SetFrameRate(hGrabber, ctypes.c_float( 30.0))

    ic.IC_StartLive(hGrabber,1)
    ic.IC_MsgBox( "Click OK to stop".encode("utf-8"),"Simple Live Video".encode("utf-8"))
    ic.IC_StopLive(hGrabber)
else:
    ic.IC_MsgBox("No device opened".encode("utf-8"), "Simple Live Video".encode("utf-8"),)

ic.IC_ReleaseGrabber(hGrabber)
