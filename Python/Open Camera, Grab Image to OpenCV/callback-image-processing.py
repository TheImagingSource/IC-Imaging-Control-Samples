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
        self.width = 0
        self.height = 0
        self.iBitsPerPixel = 0
        self.buffer_size = 0
        self.oldbrightness = 0
    

def Callback(hGrabber, pBuffer, framenumber, pData):
    """ This is an example callback function for image processig  with 
        opencv. The image data in pBuffer is converted into a cv Matrix
        and with cv.mean() the average brightness of the image is
        measuered.

    :param: hGrabber: This is the real pointer to the grabber object.
    :param: pBuffer : Pointer to the first pixel's first byte
    :param: framenumber : Number of the frame since the stream started
    :param: pData : Pointer to additional user data structure
    """
    if pData.buffer_size > 0:
        image = C.cast(pBuffer, C.POINTER(C.c_ubyte * pData.buffer_size))

        cvMat = np.ndarray(buffer = image.contents,
                        dtype = np.uint8,
                        shape = (pData.height,
                                pData.width,
                                pData.iBitsPerPixel))
        brightness = cv2.mean(cvMat)
        b = int( brightness[0] )
        if b != pData.oldbrightness:
            print( b)
            pData.oldbrightness = b
        


# Create the function pointer.
Callbackfunc = IC.TIS_GrabberDLL.FRAMEREADYCALLBACK(Callback)

ImageDescription = CallbackUserdata()    

# Create the camera object.
Camera = IC.TIS_CAM()

# Open a camera. 
Camera.openVideoCaptureDevice("DFx 72BUC02")
Camera.SetVideoFormat("RGB32 (2592x1944)")
Camera.SetFrameRate(1.0)
# Noe pass the function pointer and our user data to the library.
Camera.SetFrameReadyCallback(Callbackfunc, ImageDescription )

# Handle each incoming frame automatically.
Camera.SetContinuousMode(0)

# Start live video.
Camera.StartLive(1)    

Camera.SetPropertySwitch("Exposure","Auto",0)
Camera.SetPropertySwitch("Gain","Auto",0)
Camera.SetPropertyValue("Gain","Value",10)


Imageformat = Camera.GetImageDescription()[:3]
ImageDescription.width = Imageformat[0]
ImageDescription.height= Imageformat[1]
ImageDescription.iBitsPerPixel=Imageformat[2]//8
ImageDescription.buffer_size = ImageDescription.width * ImageDescription.height * ImageDescription.iBitsPerPixel

ExposureTime=[0]

try:
    while ( True ):
        Camera.SetPropertyAbsoluteValue("Exposure","Value",5e-4) # 6e-4 funktionier, 5e-4 funktioniert nicht
        time.sleep(0.5)
        Camera.GetPropertyAbsoluteValue("Exposure","Value",ExposureTime)
        print("Exposure time abs: ", ExposureTime[0])
        time.sleep(2)

        Camera.SetPropertyAbsoluteValue("Exposure","Value",6e-4) # 6e-4 funktionier, 5e-4 funktioniert nicht
        time.sleep(0.5)
        Camera.GetPropertyAbsoluteValue("Exposure","Value",ExposureTime)
        print("Exposure time abs: ", ExposureTime[0])
        time.sleep(2)
        
except KeyboardInterrupt:
    Camera.StopLive()    

