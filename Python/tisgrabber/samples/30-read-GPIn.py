import ctypes 
import tisgrabber as tis
import time

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)

ic.IC_InitLibrary(0)

hGrabber = ic.IC_CreateGrabber()

ic.IC_OpenVideoCaptureDevice(hGrabber,tis.T("DMK 33UX290"))

if ic.IC_IsDevValid(hGrabber): 
    inn = ctypes.c_long()
    while True:
        ic.IC_PropertyOnePush(hGrabber,tis.T("GPIO"),tis.T("Read") )
        ic.IC_GetPropertyValue(hGrabber,tis.T("GPIO"),tis.T("GP In"),inn )
        print( inn.value )
        time.sleep(1)


else:
    ic.IC_MsgBox(tis.T("No device opened"), tis.T("Simple Live Video"))

ic.IC_ReleaseGrabber(hGrabber)
