# This sample shows, how to create  a callback, that is called
# automatically each time a new frame comes in.
#
# It shows also, how to pass a user data class to the callback
# function. 

import ctypes as C
import tisgrabber as IC
import time as time

import sys

def s(strin):
    if sys.version[0] == "2":
        return strin
    if type(strin) == "byte":
        return strin
    return strin.encode("utf-8")

class CallbackUserdata(C.Structure):
    """ Example for user data passed to the callback function. """
    def __init__(self):
        self.Value1 = 42
        self.Value2 = 0
    

def Callback(hGrabber, pBuffer, framenumber, pData):
    """ This is an example callback function 
         The image is saved in test.jpg and the pData.Value1 is 
         incremented by one.

    :param: hGrabber: This is the real pointer to the grabber object.
    :param: pBuffer : Pointer to the first pixel's first byte
    :param: framenumber : Number of the frame since the stream started
    :param: pData : Pointer to additional user data structure
    """
    print("Callback called", pData.Value1)
    pData.Value1 = pData.Value1 + 1
    IC.TIS_GrabberDLL.SaveImage(hGrabber, s("test" +str(framenumber)+".jpg"), IC.ImageFileTypes["JPEG"], 75)

# Create the function pointer.
Callbackfunc = IC.TIS_GrabberDLL.FRAMEREADYCALLBACK(Callback)


Userdata = CallbackUserdata()    

# Create the camera object.
Camera = IC.TIS_CAM()

# Open a camera. 
Camera.open("DFK 37UX290 42719953")

# Noe pass the function pointer and our user data to the library.
Camera.SetFrameReadyCallback(Callbackfunc, Userdata )

# Handle each incoming frame automatically.
Camera.SetContinuousMode(0)

# Start live video.
Camera.StartLive(1)    
time.sleep(0.2)
Camera.StopLive()    
exit(1)
