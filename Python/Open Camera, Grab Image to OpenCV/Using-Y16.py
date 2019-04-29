# -*- coding: utf-8 -*-
"""
Created on Mon Nov 21 09:46:46 2016

@author: Daniel Vassmer, Stefan_Geissler

Sample for tisgrabber to OpenCV Sample 2

Open a camera by name
Set a video format hard coded (not recommended, but some peoples insist on this)
Set properties exposure, gain, whitebalance

"""
import ctypes as C
import tisgrabber as IC
import numpy as np
import sys

lWidth=C.c_long()
lHeight= C.c_long()
iBitsPerPixel=C.c_int()
COLORFORMAT=C.c_int()
# Create the camera object.
Camera = IC.TIS_CAM()

Camera.openVideoCaptureDevice("DFK 33UX290")

if Camera.IsDevValid() == 1:
    print( 'Press ctrl-c to stop' )

    # Set a video format
    Camera.SetVideoFormat("Y16 (640x480)")
    
    # Set a frame rate of 1 frame per second, thus we can read the 
    # output of the pixe values
    Camera.SetFrameRate( 1.0 )
    
    #Set the pixel format in the sink (memory) to Y16
    Camera.SetFormat(IC.SinkFormats.Y16)

    # Start the live video stream, but show no own live video window. We will use OpenCV for this.
    Camera.StartLive(1)    

    
    try:
        while ( True ):
            # Snap an image
            Camera.SnapImage()
            # Get the image
            image = Camera.GetImageEx()
            
            # Print the pixel values. The lower 4 bits are unused
            for i in range(0,10):
                sys.stdout.write( str( image.item(i) >> 4) )
                sys.stdout.write(' ')

            print( "")

    except KeyboardInterrupt:
        Camera.StopLive()
else:
    print( "No device selected")
    
    
 
