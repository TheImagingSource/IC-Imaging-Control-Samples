# Python tisgrabber Auto Focus One Push
Thes sample show, how to use the auto Focus of The Imaging Source Zoom and Auto Focus cameras, e.g.
* DFK Z12GP031
* DFK Z30GP031
* DFK 39GP265-Z20
* DFK 72AUC02-F
* DFK AFUX178-M12

The DFK AFU420-L62 has the "Focus One Push" but not the "Focus One Push Running" property.

Programming Language : Python

| File | Content |
| --- | --- |
| auto-focus-onepush.py | The main program to be executed |
| tisgrabber.py | Wrapper around the tisgrabber.dll, which encapsulates IC Imaging Control. This file wil be enhanced time by time. |
| tisgrabber.dll | 32bit version  |
| TIS_UDSHL11.dll | 32 bit version of IC Imaging Control |
| tisgrabber_x64.dll | 64bit version  |
| TIS_UDSHL11_64.dll | 64 bit version of IC Imaging Control |
| tisgrabber-focus-onepush.zip | Zip file with all project files for direct download  

In the tisgrabber.py it is automatically checked, whether to use the 32 or 64 bit versions of the IC Imaging Control DLLs

# Introduction
Please make sure the working directory of the script is set to the source directory in which the DLLs reside. Otherwise the DLLs wont be found. 

The DLLs and the tisgrabber.py differ from other versions.
The used DLL allows to distinguish between
Exposure Auto Max Max Value
Exposure Auto.

If Exposure Automatic is to be set, you use
```Python
Camera.SetPropertySwitch("Exposure","Auto", 1)
```
If Exposure Automatic Max is to be set, you use
```Python
Camera.SetPropertySwitch("Exposure","auto max auto", 1)
```

The availability check of properties has been added with the element:
```Python
if Camera.PropertyAvailable("Focus","Value") == 1:
    print("Availalbe!")

if Camera.PropertyAvailable("Focus","One Push Running") == 1:
    print("Availalbe!")
 ```
The element "Value" should always be availed. However, you may use the VCD Property Inspector of IC Imaging Control 3.5 to check, which properties are available and which names are used for item (property) and element.

## Auto Focus
This sample shows how to start the auto focus and how to wait for the auto focus to be finished.

The sample works only, if the camera has a Focus property at all. Therefore, it is checked, whether it exists:
```Python
if Camera.IsDevValid() :
    if Camera.PropertyAvailable("Focus","Value") == 1:
```

If there is a focus property, then the live video stream is started. A focus value is set, so the image is (hopefully) unfocused and the working of auto focus is shown.
```Python
        Camera.StartLive()

        # Set a focus value in order to get a blurred image.
        Camera.SetPropertyValue("Focus","Value", 100)

        # Must wait, because we can not run auto focus immediately after setting Focus.
        time.sleep(0.1) 
```

Now the Auto Focus is started by calling the PropertyOnePush() function:
```Python
        # Push Auto Focus now.
        print("Auto Focus started")
        Camera.PropertyOnePush("Focus","One Push")
```
It may have been a good idea to check the return value of this function. If it is not 1, then the auto focus does not exist. 

In the next step it is checked, whether the "One Push Running" element of Focus is available. If so, the program can wait for auto focus being finished:

```Python
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
```            
The auto focus runs on the incoming images from the camera and it checks for edges in the image. Therefore, the auto focus can fail and has to be called again. Unfortunately, the program does not know, whether the image is sharp or not.

Depending on images, the auto focus wont work, if the camera is in trigger mode but not triggered.














