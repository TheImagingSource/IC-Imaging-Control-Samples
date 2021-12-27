import ctypes 
import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")

tis.declareFunctions(ic)

ic.IC_InitLibrary(0)

hGrabber = tis.openDevice(ic)

if(ic.IC_IsDevValid(hGrabber)):
    ic.IC_StartLive(hGrabber, 1)
    key = ""
    while key != "q":
        print("s: Save an image")
        print("q: End program")
        key = input('Enter your choice:')
        if key == "s":
            if ic.IC_SnapImage(hGrabber, 2000) == tis.IC_SUCCESS:
                ic.IC_SaveImage(hGrabber, tis.T("test.jpg"),
                                tis.ImageFileTypes['JPEG'], 90)
                print("Image saved.")
            else:
                print("No frame received in 2 seconds.")

    ic.IC_StopLive(hGrabber)
else:
    ic.IC_MsgBox(tis.T("No device opened"), tis.T("Simple Live Video"))

ic.IC_ReleaseGrabber(hGrabber)
