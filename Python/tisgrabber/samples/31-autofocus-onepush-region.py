import ctypes 
import tisgrabber as tis
import time

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)

ic.IC_InitLibrary(0)

hGrabber = ic.IC_ShowDeviceSelectionDialog(None)

if ic.IC_IsDevValid(hGrabber): 
    # Set a smaller video format, so the result can be seen on screen 
    if ic.IC_SetVideoFormat(hGrabber, tis.T("RGB32 (1920x1080)")) == tis.IC_ERROR:
        print("Failed to set video format.")
        ic.IC_ReleaseGrabber(hGrabber)
        quit()

    ic.IC_SetFrameRate(hGrabber, ctypes.c_float(30.0))
    ic.IC_StartLive(hGrabber, 1)
 
    # Enable and set an auto focus region
    if ic.IC_SetPropertySwitch(hGrabber, tis.T("Focus"), tis.T("Enable Region of Interest"), 1) == tis.IC_SUCCESS:
        ic.IC_SetPropertyValue(hGrabber, tis.T("Focus"), tis.T("Left"), 100)
        ic.IC_SetPropertyValue(hGrabber, tis.T("Focus"), tis.T("Top"), 100) 
        ic.IC_SetPropertyValue(hGrabber, tis.T("Focus"), tis.T("Right"), 400)
        ic.IC_SetPropertyValue(hGrabber, tis.T("Focus"), tis.T("Bottom"), 400)
    else:
        print("Focus region is not supported.")

    # Push the auto focus
    if ic.IC_PropertyOnePush(hGrabber, tis.T("Focus"), tis.T("One Push")) == tis.IC_SUCCESS:
        print("Focussing started")

        onepushrunning = ctypes.c_long()   

        # Wait until the auto focus has finished
        if ic.IC_GetPropertySwitch(hGrabber, tis.T("Focus"), tis.T("One Push Running"), onepushrunning) == tis.IC_SUCCESS:
            while onepushrunning.value == 1:
                time.sleep(0.5)
                ic.IC_GetPropertySwitch(hGrabber, tis.T("Focus"), tis.T("One Push Running"), onepushrunning)
        else:
            print("One Push Running is not supported.")

        print("Focussing ended")
    else:
        print("Auto focus one push is not supported.")

    ic.IC_MsgBox(tis.T("Click OK to stop"), tis.T("Simple Live Video"))
    ic.IC_StopLive(hGrabber)
else:
    ic.IC_MsgBox(tis.T("No device opened"), tis.T("Simple Live Video"))

ic.IC_ReleaseGrabber(hGrabber)
