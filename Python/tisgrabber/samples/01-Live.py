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


# Functions returning a HGRABBER Handle must set their restype to POINTER(HGRABBER)
ic.IC_ShowDeviceSelectionDialog.restype = ctypes.POINTER(HGRABBER)
ic.IC_ReleaseGrabber.argtypes = (ctypes.POINTER(ctypes.POINTER(HGRABBER)),)

ic.IC_InitLibrary(0)

hGrabber = ic.IC_ShowDeviceSelectionDialog(None)
 
if( ic.IC_IsDevValid(hGrabber)): 
    ic.IC_StartLive(hGrabber,1)
    ic.IC_MsgBox( "Click OK to stop".encode("utf-8"),"Simple Live Video".encode("utf-8"))
    ic.IC_StopLive(hGrabber)
else:
    ic.IC_MsgBox("No device opened".encode("utf-8"), "Simple Live Video".encode("utf-8"),)

ic.IC_ReleaseGrabber(hGrabber)
