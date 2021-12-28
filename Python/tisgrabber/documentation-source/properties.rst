
==========
Properties
==========

Properties are values for e.g. :index:`exposure` time, :index:`gain`, :index:`white balance` and so on. 
Which properties are available depend on the used video capture device. Therefore, 
IC Imaging Control and *tisgrabber.DLL* do not contain simple functions 
like ``setExposure``. Not all devices have an exposure property.

Built in Property Dialog
------------------------
IC Imaging Control and *tisgrabber.DLL* provide a property dialog with all available 
properties of a video capture device:

.. code-block:: python

        ic.IC_ShowPropertyDialog(hGrabber)

Therefore, it is not necessary to write an own dialog.

List available properties
-------------------------

Code: 04-list-properties.py

In *tisgrabber.DLL* properties are identified by the combination of  their names and elements.

In order to get a list of available properties, their names, elements and interfaces, 
following function is called:

.. code-block:: python

     ic.IC_printItemandElementNames(hGrabber)

In the program it is used as

.. code-block:: python

    ic.IC_InitLibrary(0)

    hGrabber = ic.IC_ShowDeviceSelectionDialog(None)
    
    if( ic.IC_IsDevValid(hGrabber)): 
        ic.IC_printItemandElementNames(hGrabber)
    else:
        print("No device opened,")

    ic.IC_ReleaseGrabber(hGrabber)

The output is like

.. code-block:: text

    1.0.0.0
    The following is an example of available property items and elements. The used camera is a DFK 33GR0521:
    Startup Behavior
            Save Current State
                    Interface : Button
            Restore Factory Default
                    Interface : Button
            Status
                    Interface : MapStrings
    Brightness
            Value
                    Interface : Range
    Contrast
            Value
                    Interface : Range
    Hue
            Value
                    Interface : Range
    Saturation
            Value
                    Interface : AbsoluteValue
                    Interface : Range
    Sharpness
            Value
                    Interface : Range
    Gamma
            Value
                    Interface : AbsoluteValue
                    Interface : Range
    WhiteBalance
            Auto
                    Interface : Switch
            One Push
                    Interface : Button
            WhiteBalance Mode
                    Interface : MapStrings
            Auto Preset
                    Interface : MapStrings
            Temperature Preset
                    Interface : MapStrings
            Temperature
                    Interface : Range
            White Balance Red
                    Interface : Range
                    Interface : AbsoluteValue
            White Balance Green
                    Interface : Range
                    Interface : AbsoluteValue
            White Balance Blue
                    Interface : Range
                    Interface : AbsoluteValue
    Gain
            Value
                    Interface : AbsoluteValue
                    Interface : Range
            Auto
                    Interface : Switch
            Auto Max Value
                    Interface : AbsoluteValue
                    Interface : Range
    Exposure
            Value
                    Interface : Range
                    Interface : AbsoluteValue
            Auto
                    Interface : Switch
            Auto Reference
                    Interface : Range
            Auto Max Value
                    Interface : Range
                    Interface : AbsoluteValue
            Auto Max Auto
                    Interface : Switch
    Trigger
            Enable
                    Interface : Switch
            Software Trigger
                    Interface : Button
            Polarity
                    Interface : Switch
            Delay
                    Interface : AbsoluteValue
            Debounce Time
                    Interface : AbsoluteValue
            Mask Time
                    Interface : AbsoluteValue
            Noise Suppression Time
                    Interface : AbsoluteValue
            Burst Count
                    Interface : Range
            Burst Interval
                    Interface : Range
            Overlap
                    Interface : MapStrings
    Denoise
            Value
                    Interface : Range
    Flip Horizontal
            Enable
                    Interface : Switch
    Flip Vertical
            Enable
                    Interface : Switch
    GPIO
            GP IN
                    Interface : Range
            Read
                    Interface : Button
            GP Out
                    Interface : Range
            Write
                    Interface : Button
    Binning factor
            Value
                    Interface : MapStrings
    Highlight Reduction
            Enable
                    Interface : Switch
    Tone Mapping
            Enable
                    Interface : Switch
            Intensity
                    Interface : AbsoluteValue
            Global Brightness Factor
                    Interface : AbsoluteValue
            Auto
                    Interface : Switch
            a
                    Interface : AbsoluteValue
            b
                    Interface : AbsoluteValue
            c
                    Interface : AbsoluteValue
            lum_avg
                    Interface : AbsoluteValue
    Partial scan
            Auto-center
                    Interface : Switch
            X Offset
                    Interface : Range
            Y Offset
                    Interface : Range
    Strobe
            Enable
                    Interface : Switch
            Mode
                    Interface : MapStrings
            Polarity
                    Interface : Switch
    Color Correction Matrix
            Enabled
                    Interface : Switch
            RR
                    Interface : AbsoluteValue
            RG
                    Interface : AbsoluteValue
            RB
                    Interface : AbsoluteValue
            GR
                    Interface : AbsoluteValue
            GG
                    Interface : AbsoluteValue
            GB
                    Interface : AbsoluteValue
            BR
                    Interface : AbsoluteValue
            BG
                    Interface : AbsoluteValue
            BB
                    Interface : AbsoluteValue
    Auto Functions ROI
            Enabled
                    Interface : Switch
            Preset
                    Interface : MapStrings
            Left
                    Interface : AbsoluteValue
            Top
                    Interface : AbsoluteValue
            Width
                    Interface : AbsoluteValue
            Height
                    Interface : AbsoluteValue

The output shows the used interfaces. They are used by the below listed functions
of the *tisgrabber.dll*

* Range

    Functions:

    * IC_SetPropertyValue
    * IC_GetPropertyValue
    * IC_GetPropertyValueRange

* AbsoluteValue

    Functions:

    * IC_GetPropertyAbsoluteValue
    * IC_SetPropertyAbsoluteValue
    * IC_SetPropertyAbsoluteValueRange

* Switch

    Functions: 

    * IC_GetPropertySwitch
    * IC_SetPropertySwitch
     
* MapStrings

    Functions: 

    * IC_GetPropertyMapStrings
    * IC_SetPropertyMapStrings (*Is that implemented?*)
     
* Button

    Functions:

    * IC_PropertyOnePush

The *Range* functions accept and receive integer values. The *AbsoluteValue* functions use 
float and the *Switch* functions uses integer 0 and 1. 

All of these functions have following return values:

.. list-table:: Return values of property functions
   :widths: 25 10 50
   :header-rows: 1

   * - Name
     - Value
     - Meaning
   * - IC_SUCCESS
     - 1
     - Success
   * - IC_NO_DEVICE
     - -2
     - No video capture device opened  
   * - IC_PROPERTY_ITEM_NOT_AVAILABLE
     - -4
     - A requested property item is not available
   * - IC_PROPERTY_ELEMENT_NOT_AVAILABLE
     - -5
     - A requested element of a given property item is not available  
   * - IC_PROPERTY_ELEMENT_WRONG_INTERFACE
     - -6
     - The requested element has not the interface, which is needed.  


Setting properties
------------------

Code: 05-properties.py

Disable Exposure automatic and set 1/30 second exposure time:

.. code-block:: python

    ic.IC_SetPropertySwitch(hGrabber, "Exposure".encode("utf-8"), "Auto".encode("utf-8"), 0)
    ic.IC_SetPropertyAbsoluteValue(hGrabber, "Exposure".encode("utf-8"), "Value".encode("utf-8"), ctypes.c_float(0.0303))

Setting a property value is much easier than getting property values. For getting property values the argument types of the functions must be set first:

.. code-block:: python

    ic.IC_GetPropertyValueRange.argtypes = (ctypes.POINTER(HGRABBER),
                                ctypes.c_char_p,
                                ctypes.c_char_p,
                                ctypes.POINTER(ctypes.c_long),
                                ctypes.POINTER(ctypes.c_long), )

    ic.IC_GetPropertyValue.argtypes = (ctypes.POINTER(HGRABBER),
                                ctypes.c_char_p,
                                ctypes.c_char_p,
                                ctypes.POINTER(ctypes.c_long), )


    ic.IC_GetPropertyAbsoluteValue.argtypes = (ctypes.POINTER(HGRABBER),
                                ctypes.c_char_p,
                                ctypes.c_char_p,
                                ctypes.POINTER(ctypes.c_float), )

    ic.IC_GetPropertyAbsoluteValueRange.argtypes = (ctypes.POINTER(HGRABBER),
                                ctypes.c_char_p,
                                ctypes.c_char_p,
                                ctypes.POINTER(ctypes.c_float),
                                ctypes.POINTER(ctypes.c_float), )

    ic.IC_GetPropertySwitch.argtypes=(ctypes.POINTER(HGRABBER),
                                ctypes.c_char_p,
                                ctypes.c_char_p,
                                ctypes.POINTER(ctypes.c_long), )

The C functions of the DLL receive pointers of the variables to store the values in.
This is not really a concept of Python. Therefore, we have to create the value receiving 
variables first. The simple sample is getting the value of a switch property, in this 
case it is Exposure Auto:

.. code-block:: python

    exposureauto = ctypes.c_long()
    ic.IC_SetPropertySwitch(hGrabber, tis.T("Exposure"), tis.T("Auto"),
                            exposureauto)
    print("Exposure Auto is {0}".format(exposureauto.value))

The exposure time range and value is retrieved as follows:

.. code-block:: python

    expmin = ctypes.c_float()
    expmax = ctypes.c_float()
    exposure = ctypes.c_float()
    ic.IC_GetPropertyAbsoluteValue(hGrabber, tis.T("Exposure"), tis.T("Value"),
                                   exposure)
    ic.IC_GetPropertyAbsoluteValueRange(hGrabber, tis.T("Exposure"), tis.T("Value"),
                                        expmin, expmax)
    print("Exposure is {0}, range is {1} - {2}".format(exposure.value,
                                                       expmin.value, expmax.value))

The Absolute Value interface uses float as data type. The Range interface uses long and it is used as follows:

.. code-block:: python

    gainmin = ctypes.c_long()
    gainmax = ctypes.c_long()
    gain = ctypes.c_long()
    ic.IC_GetPropertyValue(hGrabber, tis.T("Gain"), tis.T("Value"), gain)
    ic.IC_GetPropertyValueRange(hGrabber, tis.T("Gain"), tis.T("Value"),
                                gainmin, gainmax)
    print("Gain is {0} range is {1} - {2}".format(gain.value, gain.value,gain.value))

With this code all value and switch properties of a video capture device can be handled, it is always the same. 

The last property to be handled is the Button property. A Button property executes an operation, such as One Push Auto Focus and One Push White Balance.

.. code-block:: python

    ret = ic.IC_PropertyOnePush(hGrabber, tis.T("Focus"), tis.T("One Push"))
    if ret == -4:
        print("Your camera does not support Focus properties!")

The function declaration becomes more text now, thus it will be moved into another Python file named "tisgrabber.py". Doing so, these declarations can be used simply in other projects too. However, the goal is to keep the tisgrabber.py as small as possible, so maintenance will be easy.
