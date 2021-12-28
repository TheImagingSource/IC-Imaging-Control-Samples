"""
This samples shows how to enable trigger mode, use software trigger
and how to use a callback, that is called automatically for each
incoming image on a trigger signal.
"""
import ctypes 
import tisgrabber as tis
import cv2 as cv2
import numpy as np


ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)

ic.IC_InitLibrary(0)

hGrabber = ic.IC_CreateGrabber()


class CallbackUserdata(ctypes.Structure):
    """ Example for user data passed to the callback function. 
    """
    def __init__(self, ):
        self.unsused = ""


def frameReadyCallback(hGrabber, pBuffer, framenumber, pData):
    Width = ctypes.c_long()
    Height = ctypes.c_long()
    BitsPerPixel = ctypes.c_int()
    colorformat = ctypes.c_int()

    # Query the image description values
    ic.IC_GetImageDescription(hGrabber, Width, Height, BitsPerPixel,
                              colorformat)

    # Calculate the buffer size
    bpp = int(BitsPerPixel.value/8.0)
    buffer_size = Width.value * Height.value * bpp

    if buffer_size > 0:
        image = ctypes.cast(pBuffer,
                            ctypes.POINTER(
                                ctypes.c_ubyte * buffer_size))

        cvMat = np.ndarray(buffer=image.contents,
                           dtype=np.uint8,
                           shape=(Height.value,
                                  Width.value,
                                  bpp))

        # Do something with cvMat here.


# Manage the callbacks
# Create the function pointer.
frameReadyCallbackfunc = ic.FRAMEREADYCALLBACK(frameReadyCallback)
userdata = CallbackUserdata()

hGrabber = ic.IC_ShowDeviceSelectionDialog(None)

if ic.IC_IsDevValid(hGrabber):
    ic.IC_SetFrameReadyCallback(hGrabber, frameReadyCallbackfunc, userdata)
    ic.IC_SetContinuousMode(hGrabber, 0)
    ic.IC_SetPropertySwitch(hGrabber, tis.T("Trigger"), tis.T("Enable"), 1)
    ic.IC_StartLive(hGrabber, 1)
    ic.IC_MsgBox(tis.T("Click OK to trigger"), tis.T("Simple Live Video"))
    ic.IC_PropertyOnePush(hGrabber, tis.T("Trigger"), tis.T("Software Trigger"))

    ic.IC_MsgBox(tis.T("Click OK to stop"), tis.T("Simple Live Video"))
    ic.IC_StopLive(hGrabber)
else:
    ic.IC_MsgBox(tis.T("No device opened"), tis.T("Simple Live Video"))

ic.IC_ReleaseGrabber(hGrabber)
