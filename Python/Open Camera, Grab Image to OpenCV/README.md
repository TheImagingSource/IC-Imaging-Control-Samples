# Open Camera, Grab Image to OpenCV
This sample shows, how to use a The Imaging Source camera in Python Windows, grab images and forward them to OpenCV

Programming Language : Python

| File | Content |
| --- | --- |
| tis-OpenCV.py | The main program to be executed |
| tisgrabber.py | Wrapper around the tisgrabber.dll, which encapsulates IC Imaging Control. This file wil be enhanced time by time. |
| tisgrabber.dll | 32bit version  |
| TIS_UDSHL11.dll | 32 bit version of IC Imaging Control |
| tisgrabber_x64.dll | 64bit version  |
| TIS_UDSHL11_64.dll | 64 bit version of IC Imaging Control |

In the tisgrabber.py it is automatically checked, whether to use the 32 or 64 bit versions of the IC Imaging Control DLLs

## Documentation
### Creating the tisgrabber object
``` Python
import tisgrabber as IC
Camera = IC.TIS_CAM()
```

### Open a video capture Device (Camera)
There are two ways for opening a camera.
#### Use built in Dialog
``` Python
Camera.ShowDeviceSelectionDialog()
```
#### Open camera without dialog
``` Python
Camera.open("DFK 33UX290 18810256")
# Set a video format
Camera.SetVideoFormat("RGB32 (640x480)")
#Set a frame rate of 30 frames per second
Camera.SetFrameRate( 30.0 )
```

### Start and stop live video
Starting the stream is done by
``` Python
Camera.StartLive(1)    
```
The "1" passed to StartLive() leads to a window on the desktop showing the live video. If a "0" is passed, no live video is shown

The live stream is stopped by
``` Python
Camera.StopLive()    
```

### Handle the images in OpenCV
An image is snapped from the live stream by follwing two function calls:
``` Python
Camera.SnapImage()
# Get the image
image = Camera.GetImage()
```
Now the image can be handled by OpenCV
``` Python
image = cv2.flip(image,0)
image = cv2.erode(image,np.ones((11, 11)))
```

### Camera properties
Camera properties are set as follows:
``` Python
Camera.SetPropertySwitch("Gain","Auto",0)
Camera.SetPropertyValue("Gain","Value",10)
```
| Parameter | Description|
| --- | --- |
| Property | Name of a property e.g "Gain", Exposure" etc. |
| Element | The element of a property, e.g "Auto" for automatic, "Value" for a value to be set |

Use the IC Imaging Control VCD Property Inspector to get a list of supported properties an elements. It is installed with the [IC Imaging Control SDK](https://www.theimagingsource.com/support/downloads-for-windows/software-development-kits-sdks/icimagingcontrol/)








