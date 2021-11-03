'''
This sample demonstrates, how to open a camera with the 
built in dialog and show a live video stream.
Needed DLLs for 64 bit environment are 
- tisgrabber_x64.dll
- TIS_UDSHL11_x64.dll
'''
import ctypes 

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")

class HGRABBER(ctypes.Structure):
    '''
    This class is used to handle the pointer to the internal 
    Grabber class, which contains the camera. 
    A pointer to this class is used by tisgrabber DLL.
    '''
    _fields_ = [('unused', ctypes.c_int)]

class CallbackUserdata(ctypes.Structure):
    """ Example for user data passed to the callback function. """
    def __init__(self):
        self.Value1 = 42
        self.Value2 = 0
        self.camera = None # Reference to the camera object


def Callback(hGrabber, pBuffer, framenumber, pData):
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


# Functions returning a HGRABBER Handle must set their restype to POINTER(HGRABBER)
ic.IC_ShowDeviceSelectionDialog.restype = ctypes.POINTER(HGRABBER)
ic.IC_ReleaseGrabber.argtypes = (ctypes.POINTER(ctypes.POINTER(HGRABBER)),)

# definition of the frameready callback
FRAMEREADYCALLBACK = ctypes.CFUNCTYPE(ctypes.c_void_p, ctypes.c_int, ctypes.POINTER( ctypes.c_ubyte), ctypes.c_ulong, ctypes.py_object )
ic.IC_SetFrameReadyCallback.argtypes = [ctypes.POINTER(HGRABBER), FRAMEREADYCALLBACK, ctypes.py_object]

Userdata = CallbackUserdata()    
# Create the function pointer.
Callbackfunc = FRAMEREADYCALLBACK(Callback)

ic.IC_InitLibrary(0)

hGrabber = ic.IC_ShowDeviceSelectionDialog(None)
 
if( ic.IC_IsDevValid(hGrabber)): 
    ic.IC_SetFrameReadyCallback(hGrabber, Callbackfunc, Userdata)
    ic.IC_SetContinuousMode(hGrabber, 0)
    ic.IC_StartLive(hGrabber,1)
    ic.IC_MsgBox( "Click OK to stop".encode("utf-8"),"Callback".encode("utf-8"))
    ic.IC_StopLive(hGrabber)
else:
    ic.IC_MsgBox("No device opened".encode("utf-8"), "Callback".encode("utf-8"),)
