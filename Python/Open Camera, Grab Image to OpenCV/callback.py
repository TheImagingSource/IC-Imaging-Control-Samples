# This sample shows, how to create  a callback, that is called
# automatically each time a new frame comes in.
#
# It shows also, how to pass a user data class to the callback
# function. 

import ctypes as C
import tisgrabber as IC
import time as time
import cv2 as cv2
import numpy as np
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
    # Get the used image format from our camera object
    Imageformat = pData.camera.GetImageDescription()[:3]

    buffersize= Imageformat[0] * Imageformat[1] * int(Imageformat[2]/8)
    if buffersize > 0:
        # Convert image to OpenCV for saving to JPEG file
        image = C.cast(pBuffer, C.POINTER(C.c_ubyte * buffersize))

        cvMat = np.ndarray(buffer = image.contents,
                            dtype = np.uint8,
                            shape = (Imageformat[1], # Width
                                    Imageformat[0],  # Height
                                    Imageformat[2]//8))
        cvMat = cv2.flip(cvMat,0)
        cv2.imwrite("test" +str(framenumber)+".jpg",cvMat )

# Create the function pointer.
Callbackfunc = IC.TIS_GrabberDLL.FRAMEREADYCALLBACK(Callback)

Userdata = CallbackUserdata()    

# Create the camera object.
Camera = IC.TIS_CAM()
Userdata.camera = Camera

# Open a camera. 
Camera.open("DFK 33UX290 18810256")
# Set a video format
Camera.SetVideoFormat("RGB32 (640x480)")
#Set a frame rate of 30 frames per second
Camera.SetFrameRate( 30.0 )

# Now pass the function pointer and our user data to the library.
Camera.SetFrameReadyCallback(Callbackfunc, Userdata )

# Handle each incoming frame automatically.
Camera.SetContinuousMode(0)

# Start live video.
Camera.StartLive(1)    
time.sleep(1)
Camera.StopLive()    


