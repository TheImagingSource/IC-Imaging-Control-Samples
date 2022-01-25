'''
This sample demonstrates how to create a callback, which is automatically
called for each incoming frame.
'''
import ctypes

import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)


class CallbackUserdata(ctypes.Structure):
    """ Example for user data passed to the callback function. """
    def __init__(self):
        self.Value1 = 42
        self.Value2 = 0
        self.camera = None      # Reference to a camera/grabber object


def FrameCallback(hGrabber, pBuffer, framenumber, pData):
    """ This is an example callback function 
         The image is saved in test.jpg and the pData.Value1 is 
         incremented by one.

    :param: hGrabber: This is the real pointer to the grabber object. Do not use.
    :param: pBuffer : Pointer to the first pixel's first byte
    :param: framenumber : Number of the frame since the stream started
    :param: pData : Pointer to additional user data structure
    """
    print("Callback called", pData.Value1)
    pData.Value1 = pData.Value1 + 1


Userdata = CallbackUserdata()
# Create the function pointer.
Callbackfuncptr = ic.FRAMEREADYCALLBACK(FrameCallback)

ic.IC_InitLibrary(0)

hGrabber = ic.IC_ShowDeviceSelectionDialog(None)

if(ic.IC_IsDevValid(hGrabber)):
    ic.IC_SetFrameReadyCallback(hGrabber, Callbackfuncptr, Userdata)
    ic.IC_SetContinuousMode(hGrabber, 0)
    ic.IC_StartLive(hGrabber, 1)
    ic.IC_MsgBox(tis.T("Click OK to stop"), tis.T("Callback"))
    ic.IC_StopLive(hGrabber)
else:
    ic.IC_MsgBox(tis.T("No device opened"), tis.T("Callback"))

ic.IC_ReleaseGrabber(hGrabber)
