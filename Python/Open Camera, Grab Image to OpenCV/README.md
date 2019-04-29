# Python Samples for Windows and IC Imaging Control
Thes samples show, how to use a The Imaging Source camera in Python Windows, grab images and forward them to OpenCV and use callbacks

Programming Language : Python

| File | Content |
| --- | --- |
| tis-OpenCV.py | The main program to be executed |
| tisgrabber.py | Wrapper around the tisgrabber.dll, which encapsulates IC Imaging Control. This file wil be enhanced time by time. |
| callback.py | Simple callback implementation for saving an image |
| callback-image-processing.py | Callback sample, which uses OpenCV for measuring image brightness |
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

## Callbacks
IC Imaging Control can call a callback function for new incoming frames. The callback function is declared wit
``` Python
def Callback(hGrabber, pBuffer, framenumber, pData):
```

| Parameter | Description|
| --- | --- |
| hGrabber | Pointer to the grabber object, from which the callback was called. |
| pBuffer | Unsinged char (BYTE) pointer to the image data |
| framenumber | Number of the recevied frames sinnce start of video |
| pDdata | Pointer to a user data struct |

A very simple callback function is

``` Python
def Callback(hGrabber, pBuffer, framenumber, pData):
    print("Callback called")
```

For passing it to the tisgrabber dll, a pointer to this function is needed:
``` Python
Callbackfunc = IC.TIS_GrabberDLL.FRAMEREADYCALLBACK(Callback)
```

Also we may use some userdata, thus create a struct.

``` Python
class CallbackUserdata(C.Structure):
    def __init__(self):
        self.Value1 = 42
        self.Value2 = 0

Userdata = CallbackUserdata()    
```

Now the callback can be passed 
``` Python
Camera.SetFrameReadyCallback(Callbackfunc, Userdata )
```

At least, the trisgrabber DLL must know, whether the callback is to be called for each incoming frame or whether it is called for an extra "snapimages()" call only:
``` Python
# Handle each incoming frame automatically.
Camera.SetContinuousMode(0)
```
Now the live video can be started and the callback is called now.

### Callback sample for using the hGrabber parameter:
``` Python
def Callback(hGrabber, pBuffer, framenumber, pData):
    """ This is an example callback function 
         The image is saved in test.jpg and the pData.Value1 is 
         incremented by one.
    """
    pData.Value1 = pData.Value1 + 1
    IC.TIS_GrabberDLL.SaveImage(hGrabber, s("test" +str(framenumber)+".jpg"), IC.ImageFileTypes["JPEG"], 75)
```
In this sample, the functions in the TIS_GrabberDLL are called directly, passing by the camera class. Also there is something done with the user data.


### Callback sample for using the pBuffer parameter for image processing
This sample converts the data of pBuffer into a cv::Mat for image processing. Therefore, the information about the iamge size and type is saved in user data. A struct is created for this first:
``` Python
class CallbackUserdata(C.Structure):
    """ Example for user data passed to the callback function. """
    def __init__(self):
        self.width = 0
        self.height = 0
        self.iBitsPerPixel = 0
        self.buffer_size = 0
        self.oldbrightness = 0

ImageDescription = CallbackUserdata()    
```
The "oldbrightnes" member is used for saving measured data and compare it with the next incoming image. 

After the live video has been started, the description is available and will be saved:
``` Python
Imageformat = Camera.GetImageDescription()[:3]
ImageDescription.width = Imageformat[0]
ImageDescription.height= Imageformat[1]
ImageDescription.iBitsPerPixel=Imageformat[2]//8
ImageDescription.buffer_size = ImageDescription.width * ImageDescription.height * ImageDescription.iBitsPerPixel
```

The callback is following:
``` Python
def Callback(hGrabber, pBuffer, framenumber, pData):
    """ This is an example callback function for image processig  with 
        opencv. The image data in pBuffer is converted into a cv Matrix
        and with cv.mean() the average brightness of the image is
        measuered.
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

```
With the check on buffer_size > 0 we are sure, that our user data in pData contains valid data.
Then some magic cast is done so the numpy array can be created, which is used for brightness measurement by a call to cv2.mean()

### Using Y16 as video and sink format
The Using-Y16.py sample shows, how to use Y16 as video and pixel format in memory (sink). The first ten pixel values are printed to output.
The camera's Y16 format contains, dependent on the sensor, 12 or 10 valid bits. The upper bits contain valid content, the lower 4 or 6 bits are not connected.
For this sample a new function 'TIS.GetImageEx()' is added to tisgrabber.py. This function returns a numpy array. It has uint8 for Y800, RGB32 and RGB32 and uint16 for Y16 pixel formats. RGB64 is not supported currently.









