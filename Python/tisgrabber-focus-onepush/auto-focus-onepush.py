import ctypes as C
import tisgrabber as IC
import numpy as np
import time
import msvcrt as m

def wait(waittext):
    print( waittext )
    m.getch()

IC.TIS_GrabberDLL.InitLibrary(None)

Camera = IC.TIS_CAM()
Camera.ShowDeviceSelectionDialog()

if Camera.IsDevValid() :
    if Camera.PropertyAvailable("Focus","Value") == 1:
        Camera.StartLive()

        # Set a focus value in order to get a blurred image.
        Camera.SetPropertyValue("Focus","Value", 100)

        # Must wait, because we can not run auto focus immediately after setting Focus.
        time.sleep(0.1) 
        # Push Auto Focus now.
        print("Auto Focus started")
        Camera.PropertyOnePush("Focus","One Push")

        # Check, whether One Push Running is available.
        if Camera.PropertyAvailable("Focus","One Push Running") == 1:
            # Wait for One Push Running of Auto Focus ends.
            autofocusrunning = [0]
            autofocusrunning[0] = 1
            while( autofocusrunning[0] == 1 ):
                Camera.GetPropertySwitch("Focus","One Push Running",autofocusrunning )
                time.sleep(0.1)

            print("Focus One Push ended.")
        else:
            print("Focus One Push Running property is not available.")

        wait("Press any key to stop live video.")
        Camera.StopLive()
    else:
        print("Your camera does not have a Focus property.")





