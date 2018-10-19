# -*- coding: utf-8 -*-
"""
Created on Mon Nov 21 09:46:46 2016

Sample for tisgrabber to OpenCV Sample 2

Open a camera by name
Set a video format hard coded (not recommended, but some peoples insist on this)
Set properties exposure, gain, whitebalance
"""
import ctypes as C
import tisgrabber as IC
import cv2
import numpy as np


lWidth=C.c_long()
lHeight= C.c_long()
iBitsPerPixel=C.c_int()
COLORFORMAT=C.c_int()


# Create the camera object.
Camera = IC.TIS_CAM()

# List availabe devices as uniqe names. This is a combination of camera name and serial number
Devices = Camera.GetDevices()
for i in range(len( Devices )):
    print( str(i) + " : " + str(Devices[i]))

# Open a device with hard coded unique name:
#Camera.open("DFK 33UX290 18810256")
# or show the IC Imaging Control device page:

Camera.ShowDeviceSelectionDialog()

if Camera.IsDevValid() == 1:
    #cv2.namedWindow('Window', cv2.cv.CV_WINDOW_NORMAL)
    print( 'Press ctrl-c to stop' )

    # Set a video format
    #Camera.SetVideoFormat("RGB32 (640x480)")
    
    #Set a frame rate of 30 frames per second
    #Camera.SetFrameRate( 30.0 )
    
    # Start the live video stream, but show no own live video window. We will use OpenCV for this.
    Camera.StartLive(1)    

    # Set some properties
    # Exposure time

    ExposureAuto=[1]
    
    Camera.GetPropertySwitch("Exposure","Auto",ExposureAuto)
    print("Exposure auto : ", ExposureAuto[0])

    
    # In order to set a fixed exposure time, the Exposure Automatic must be disabled first.
    # Using the IC Imaging Control VCD Property Inspector, we know, the item is "Exposure", the
    # element is "Auto" and the interface is "Switch". Therefore we use for disabling:
    Camera.SetPropertySwitch("Exposure","Auto",0)
    # "0" is off, "1" is on.

    ExposureTime=[0]
    Camera.GetPropertyAbsoluteValue("Exposure","Value",ExposureTime)
    print("Exposure time abs: ", ExposureTime[0])

    
    # Set an absolute exposure time, given in fractions of seconds. 0.0303 is 1/30 second:
    Camera.SetPropertyAbsoluteValue("Exposure","Value",0.0303)

    # Proceed with Gain, since we have gain automatic, disable first. Then set values.
    Gainauto=[0]
    Camera.GetPropertySwitch("Gain","Auto",Gainauto)
    print("Gain auto : ", Gainauto[0])
    
    Camera.SetPropertySwitch("Gain","Auto",0)
    Camera.SetPropertyValue("Gain","Value",10)

    WhiteBalanceAuto=[0]
    # Same goes with white balance. We make a complete red image:
    Camera.SetPropertySwitch("WhiteBalance","Auto",1)
    Camera.GetPropertySwitch("WhiteBalance","Auto",WhiteBalanceAuto)
    print("WB auto : ", WhiteBalanceAuto[0])

    Camera.SetPropertySwitch("WhiteBalance","Auto",0)
    Camera.GetPropertySwitch("WhiteBalance","Auto",WhiteBalanceAuto)
    print("WB auto : ", WhiteBalanceAuto[0])
    
    Camera.SetPropertyValue("WhiteBalance","White Balance Red",64)
    Camera.SetPropertyValue("WhiteBalance","White Balance Green",64)
    Camera.SetPropertyValue("WhiteBalance","White Balance Blue",64)
    
    try:
        while ( True ):
            # Snap an image
            Camera.SnapImage()
            # Get the image
            image = Camera.GetImage()
            # Apply some OpenCV function on this image
            image = cv2.flip(image,0)
            image = cv2.erode(image,np.ones((11, 11)))
            cv2.imshow('Window', image)
            cv2.waitKey(10)
           
    except KeyboardInterrupt:
        Camera.StopLive()    
        cv2.destroyWindow('Window')

    
else:
    print( "No device selected")
    
    
 
