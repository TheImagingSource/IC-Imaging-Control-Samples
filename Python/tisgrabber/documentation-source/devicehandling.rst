
###############
Device Handling
###############

This chapter shows, how to open a video capture :index:`device`, set a video format and a frame rate. 
The tisgrabber DLL offers different ways in order to open a video capture device. 

IC_ShowDeviceSelectionDialog
----------------------------

Code : 01-Live.py

This shows a built in dialog for selecting a video capture device, a video format and frame rate. It also has a button, which shows the device property dialog. The function returns a valid HGRABBER handle, even if no device was selected:

.. code-block:: python

    hGrabber = ic.IC_ShowDeviceSelectionDialog(None)


Open a Device Manually by Model Name
------------------------------------

Code : 02-open-manually.py

This means, that a ``HGRABBER`` is created and the device name, video format and frame rate a passed hard coded to the handle. That means, the programmer knows camera name, video format and frame rate to be used already.
The function ``IC_CreateGrabber()`` of *tisgrabber.dll* is used to create a ``HGRABBER`` object:

.. code-block:: python

    hGrabber = ic.IC_CreateGrabber()

The video capture device is opened by

.. code-block:: python

    ic.IC_OpenVideoCaptureDevice(hGrabber, tis.T("DFK 33GR0521"))

It is always a good idea to check, whether that was successful:

.. code-block:: python

    if( ic.IC_IsDevValid(hGrabber)): 

If it was successful, the video format and the frame rate can be set:

.. code-block:: python

    ic.IC_SetVideoFormat(hGrabber, tis.T("RGB32 (640x480)"))
    ic.IC_SetFrameRate(hGrabber, ctypes.c_float( 30.0))


The video capture device is ready to show a live video now.

.. code-block:: python

    ic.IC_StartLive(hGrabber,1)


This code sets the format directly on the sensor. It is often called "setting an ROI" (Region of Interest). Most cameras will provide higher frame rates, the smaller the format is.
There are limitations for with and height. Usually both should be dividable by 4. And sensors have minimum sizes,e.g. 256x4.
ic.IC_SetVideoFormat() returns 1 if the format could be set successfully and 0, if an invalid format was passed, e.g. "RGB32 (255x255)".

Open a Device by Model Name and Serial Number
---------------------------------------------

If many device of the same model are in use, then the :index:`serial number` of a device identifies it unique. This is used 
in the function ``ic.IC_OpenDevByUniqueName(g, tis.T(uniquename))``. The unique name is built from device model and and its 
serial number separated by a space:

.. code-block:: python

    g = ic.IC_CreateGrabber()
    ic.IC_OpenDevByUniqueName(g, tis.T("DFK Z30GP031 41910044"))

How to get a list of devices and their serial numbers is shown later in this tutorial.




Using Device Configuration Files
--------------------------------

IC Imaging Control and therefore the tisgrabber DLL can use configuration files for device state saving and loading. These files are XML files and contain the complete device state, such as video format, frame rate and properties. They also contain the serial number of the device it was created with, so in multi camera applications a specific file configures always the same camera. There are two functions relevant:

* ``IC_LoadDeviceStateFromFile``

* ``IC_SaveDeviceStateToFile``


------------------------
Creating the Device file
------------------------

Code : 03-save-to-file.py

``IC_SaveDeviceStateToFile`` saves the current device state of an opened video capture device into an XML :index:`configuration file`. 
The file contains:
* Device name and :index:`serial number`
* Video format
* Frame rate
* All properties, such as exposure, gain and so on.

The following code shows, how to use the ``IC_SaveDeviceStateToFile`` function:

.. code-block:: python

    ic.IC_InitLibrary(0)

    hGrabber = ic.IC_ShowDeviceSelectionDialog(None)

    if(ic.IC_IsDevValid(hGrabber)):
        ic.IC_SaveDeviceStateToFile(hGrabber, tis.T("device.xml"))

-----------------------------------
Open a Device by Configuration File
-----------------------------------

Code : 03-open-by-file.py

The file "device.xml" should exist now. 

.. code-block:: python

    hGrabber = ic.IC_LoadDeviceStateFromFile(None,tis.T("device.xml)))

If this was successful, ``ic.IC_IsDevValid( hGrabber )`` returns true.

In order to open the last used video capture device at e.g. program start, a little helper  ``openDevice(ic)`` exists in *tisgrabber.py*
This saves a lot of time during programming, because the video capture device must not selected always again for each debugging start. 

List Devices
------------

Code : 06-list-devices.py

This sample shows, how to get a list of available video capture devices.

At first, the number of available devices is queried:

.. code-block:: python
    
    devicecount = ic.IC_GetDeviceCount()

Then the names are listed by an index from 0 to devicecount.

.. code-block:: python

    for i in range(0, devicecount):
        print("Device {}".format(tis.D(ic.IC_GetDevice(i))))
        uniquename = tis.D(ic.IC_GetUniqueNamefromList(i))
        print("Unique Name : {}".format(tis.D(ic.IC_GetUniqueNamefromList(i))))

The loop lists the device name and also the device unique name. The ``tis.D()`` function is used to encode the returned strings
to UTF8 for Python. 

If each connected video capture device shall be opened, a list of ``HGRABBER`` objects is needed:

.. code-block:: python

    grabbers = []

The loop is extended to

.. code-block:: python

    devicecount = ic.IC_GetDeviceCount()
    grabbers = []
    for i in range(0, devicecount):
        uniquename = tis.D(ic.IC_GetUniqueNamefromList(i))

        g = ic.IC_CreateGrabber()
        ic.IC_OpenDevByUniqueName(g, tis.T(uniquename))
        grabbers.append(g)

Now the grabbers can be started:

.. code-block:: python

    for grabber in grabbers:
        if(ic.IC_IsDevValid(grabber)):
            ic.IC_StartLive(grabber, 1)

and stopped:

.. code-block:: python

    for grabber in grabbers:
        if(ic.IC_IsDevValid(grabber)):
            ic.IC_StopLive(grabber)

At lease release the  ``HGRABBER`` objects 

.. code-block:: python
    
    for grabber in grabbers:
        if(ic.IC_IsDevValid(grabber)):
            ic.IC_ReleaseGrabber(grabber)

Detect Device Lost
------------------

Code: 33-device-lost-event.py

It is a good idea to have a notification if a video capture device is
lost, e.g. by disconnection or something weird in the network. This 
enables a program to react, e.g. try to reconnect the video capture device
or notify the operator.

The :index:`device lost` event uses a callback. The callback is implemented as

.. code-block:: python

    def deviceLostCallback(hGrabber, userdata):
        userdata.connected = False
        print("Device {} lost".format(userdata.devicename))

It receives as parameters a Grabber and a user data object. The a possible user data class could 
be implemented as follows:

.. code-block:: python

    class CallbackUserdata(ctypes.Structure):
        """ Example for user data passed to the callback function. 
        """
        def __init__(self, ):
            self.unsused = ""
            self.devicename = ""
            self.connected = False

The device name attribute can be set, when the video capture device is opened.

Unfortunately a frameready callback is needed too, but this can be an empty function.

.. code-block:: python

    def frameReadyCallback(hGrabber, pBuffer, framenumber, pData):
        # Maybe do something here.
        return

(It is needed, because I do not know, how to pass a NULL value to the set callback function.)

The function pointers are declared as

.. code-block:: python

    frameReadyCallbackfunc = ic.FRAMEREADYCALLBACK(frameReadyCallback)

    userdata = CallbackUserdata()
    devicelostcallbackfunc = ic.DEVICELOSTCALLBACK(deviceLostCallback)


After the video capture device has been opened, the callbacks can be set 
to the ``hGrabber`` object

.. code-block:: python

    # Prepare the callback user data.
    userdata.devicename = ic.IC_GetDeviceName(hGrabber).decode('utf-8', 'ignore')
    userdata.connected = True

    ic.IC_SetCallbacks(hGrabber,
                       frameReadyCallbackfunc, None,
                       devicelostcallbackfunc, userdata)


In this simple script the  ``userdata.connected`` variable is used to terminate the 
main program in case the device is lost:

.. code-block:: python    

    while(userdata.connected):
            time.sleep(0.5)

The callbacks always run in the ``hGrabber`` thread, therefore, it might be necessary to 
implement message handling. For Qt5 it is shown in 41-qt-triggering.py


ROI, Binning & Co
-----------------

Code: 07-roi.py

--------------
Setting an ROI
--------------
Most sensors allow to set a physical Region of Interest (:index:`ROI`) directly on the sensor.
The camera sends this small ROI only, which usually results in higher frame rates.

The width and height increments are usually restricted. In most cases, both values 
must be divisible by 4 or 8. Likewise, there is a minimum height and width.

Currently the *tisgrabber.dll* does not implement the IC Imaging Control VideoFormatDesc 
object, therefore, the increments and minimum values must be determined manually. the
built in Device Selection dialog ``ic.IC_ShowDeviceSelectionDialog(None)`` can be used
for this. A click on the "Customize" button allows to choose an ROI, if supported. 
The increments can be checked there.

An ROI is set in code by the video format with the function 
``ic.IC_SetVideoFormat(hGrabber, tis.T("RGB32 (640x480)"))``. The format string has
always the same format:

- Pixel format in the camera, e.g. Y800, Y16, RGB24, RGB32, YUY2, Y411 etc
 
- A space
  
- Width and height separated by an "x" in brackets

Please keep in mind, a video format can be set only, while the camera does not stream.
The ROI is always a part of a sensor, therefore, the field of view will always be smaller
than the full sensor.

--------------------
Binning and Skipping
--------------------

Some sensors support :index:`binning` and :index:`skipping`.

Binning
    Binning means that two or more pixels are combined. This can be a simple addition of the brightness values or the average of the brightness values. This is sensor dependent.

Skipping
    Skipping simply skips a corresponding number of pixels.

On some sensors binning and skipping and be done horizontally and vertically only too.

In order to enable binning or skipping the text "[Binning 2x]" or "[Skipping 2x]" etc must
be added to the video format string. For example:

.. code-block:: python    

    ic.IC_SetVideoFormat(hGrabber, tis.T("RGB32 (640x480) [Skipping 2x]"))

The width and the height must be small enough to enable binning and skipping. If 2x 
is used, then the maximum useable width and height is the sensor's width / 2 and height / 2.

---------------------------
Moving an ROI on the Sensor
---------------------------

An ROI can be moved on the sensor. The "Partial Scan" properties are used for that.
Per default it is centered on the sensor by the driver. In order to move a an ROI, The
``Auto-center`` property must be disabled:

.. code-block:: python    

    # Moving the ROI on the sensor need to disable the Partial Scan Auto Center
    # property:
    ic.IC_SetPropertySwitch(hGrabber, tis.T("Partial scan"),
                            tis.T("Auto-center"), 0)

Then the ROI can be moved:

.. code-block:: python    

        ic.IC_SetPropertyValue(hGrabber, tis.T("Partial scan"),
                               tis.T("Y Offset"), 110)
        ic.IC_SetPropertyValue(hGrabber, tis.T("Partial scan"),
                               tis.T("X Offset"), 20)

The camera driver internally adjusts the specified values to valid coordinates. The 
ROI an be moved, while the camera streams. 

In case the application needs a fixed center position, I recommend to use a smaller 
ROI and move it, until the center position is correct. This is ofter simpler, than 
moving the camera mechanically. 







