"""
This samples shows how to use the device lost event
handler
"""
import ctypes 
import tisgrabber as tis
import cv2 as cv2
import numpy as np
import time


ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)

ic.IC_InitLibrary(0)

hGrabber = ic.IC_CreateGrabber()


class CallbackUserdata(ctypes.Structure):
    """ Example for user data passed to the callback function. 
    """
    def __init__(self, ):
        self.unsused = ""
        self.devicename = ""
        self.connected = False


def frameReadyCallback(hGrabber, pBuffer, framenumber, pData):
    # Maybe do something here.
    return


def deviceLostCallback(hGrabber, userdata):
    """ This a device lost callback function. Called, if the camera disconnects. 
    This function runs in the Grabber thread, not in the main thread.
    :param: hGrabber: This is the real pointer to the grabber object. Do not use.
    :param: pData : Pointer to additional user data structure
    """
    userdata.connected = False
    print("Device {} lost".format(userdata.devicename))


# Manage the callbacks
# Create the function pointer.
frameReadyCallbackfunc = ic.FRAMEREADYCALLBACK(frameReadyCallback)
userdata = CallbackUserdata()
devicelostcallbackfunc = ic.DEVICELOSTCALLBACK(deviceLostCallback)

hGrabber = ic.IC_ShowDeviceSelectionDialog(None)

if ic.IC_IsDevValid(hGrabber):
    userdata.devicename = ic.IC_GetDeviceName(hGrabber).decode('utf-8', 'ignore')
    userdata.connected = True

    ic.IC_SetCallbacks(hGrabber,
                       frameReadyCallbackfunc, None,
                       devicelostcallbackfunc, userdata)

    ic.IC_StartLive(hGrabber, 1)
    print("Disconnect camera now!")

    # Wait for disconnect
    while(userdata.connected):
        time.sleep(0.5)

    ic.IC_StopLive(hGrabber)
else:
    ic.IC_MsgBox(tis.T("No device opened"), tis.T("Device Lost Example"))

ic.IC_ReleaseGrabber(hGrabber)
