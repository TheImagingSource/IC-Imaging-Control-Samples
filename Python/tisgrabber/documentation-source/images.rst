
============
Using Images
============

After device handling the next step is getting images. 
This chapter shows how to query images from the camera and how to process them by :index:`OpenCV`.

Snap and Save an Image from LiveStream
--------------------------------------

Code : 10-save-image.py

This sample shows how to snap an image from the live stream of a video capture device and save it as JPEG image. It also uses the above mentioned 
tisgrabber.py. That reduces the code of the sample. It is started by

.. code-block:: python

    import ctypes 
    import tisgrabber as tis

    ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")

    tis.declareFunctions(ic)

The program is ready to start now. A video capture device is opened and checked whether it is valid by

.. code-block:: python

    hGrabber = tis.openDevice(ic)

    if( ic.IC_IsDevValid(hGrabber)): 
    
The stream is started an a little menu is shown:

.. code-block:: python
    
        ic.IC_StartLive(hGrabber,1)
        key = ""
        while key != "q":
            print("s: Save an image")
            print("q: End program")
            key = input('Enter your choice:')

Snapping and saving a frame is done with two functions:

.. code-block:: python

            ic.IC_SnapImage(hGrabber, 2000) 

This waits for an image with a 2 seconds timeout and returns an error, if no frame was received. If a frame was received, it can be saved by

.. code-block:: python

                ic.IC_SaveImage(hGrabber, tis.T("test.jpg"),
                                tis.ImageFileTypes['JPEG'], 90)


The parameter ``tis.ImageFileTypes['JPEG']`` indicates, that the image is to be saved as :index:`JPEG` image. The other possibility is Bitmap: ``tis.ImageFileTypes['BMP']``.

The last parameter, here "90" sets the JPEG quality. It is ignored, if the image is saved as bitmap.

It is used in the menu loop as follows:

.. code-block:: python

    ic.IC_InitLibrary(0)
    hGrabber = tis.openDevice(ic)

    if( ic.IC_IsDevValid(hGrabber)): 
        ic.IC_StartLive(hGrabber,1)
        key = ""
        while key != "q":
            print("s: Save an image")
            print("q: End program")
            key = input('Enter your choice:')
            if  key == "s":
                if ic.IC_SnapImage(hGrabber, 2000) == tis.IC_SUCCESS:
                    ic.IC_SaveImage(hGrabber, tis.T("test.jpg"),
                                    tis.ImageFileTypes['JPEG'] , 90)
                    print("Image saved.")
                else:
                    print("No frame received in 2 seconds.")


        ic.IC_StopLive(hGrabber)
    else:
        ic.IC_MsgBox(tis,T("No device opened"), tis.T("Simple Live Video"))

    ic.IC_ReleaseGrabber(hGrabber)


The function ``IC_SnapImage()`` is not suitable, if the camera is in :index:`trigger` mode, because 
frames can be missed. This function also blocks the program execution while no frame is delivered. 
Please use a frame delivery callback function for triggered cameras . How to do this is explained later.

Snapping an Image and Process it with OpenCV 
--------------------------------------------

Code : 11-image-processing

This sample shows, how to

* get an image descriptions of width, height and bits per pixel
* get a pointer to the image data and convert it to Python use
* create a ``numpy`` array for :index:`OpenCV` image processing

OpenCV uses ``numpy``, therefore ``numpy`` must be installed and imported.

The related functions in the tisgrabber DLL are

* IC_GetImageDescription
* IC_GetImagePtr

The function ``IC_GetImageDescription`` receives pointers to variables, therefore, they must
be declared first:

.. code-block:: python

                Width = ctypes.c_long()
                Height = ctypes.c_long()
                BitsPerPixel = ctypes.c_int()
                colorformat = ctypes.c_int()

``BitsPerPixel`` is 8 on Y800, 24 on RGB24 and 32  on RGB32 formats. The default format in 
memory is RGB24. The format used by camera will be converted automatically to RGB24.

Next step is querying these values and calculate the buffer size of an image:

.. code-block:: python

                # Query values of image description
                ic.IC_GetImageDescription(hGrabber, Width, Height,
                                          BitsPerPixel, colorformat)

                # Calculate the buffer size
                bpp = int(BitsPerPixel.value / 8.0)
                buffer_size = Width.value * Height.value * BitsPerPixel.value

Then the pointer to the image data is queried and casted into something Python can handle:

.. code-block:: python

                # Get the image data
                imagePtr = ic.IC_GetImagePtr( hGrabber )

                imagedata = ctypes.cast(imagePtr,
                                        ctypes.POINTER(ctypes.c_ubyte *
                                                       buffer_size))


With ``imagedata`` the ``numpy`` array is created:

.. code-block:: python

                # Create the numpy array
                image = np.ndarray(buffer=imagedata.contents,
                                   dtype=np.uint8,
                                   shape=(Height.value,
                                          Width.value,
                                          bpp))

The ``image`` can be used by OpenCV functions now:

.. code-block:: python

                # Apply some OpenCV functions on the image
                image = cv2.flip(image, 0)
                image = cv2.erode(image, np.ones((11, 11)))

                cv2.imshow('Window', image)
                cv2.waitKey(10)

The complete sample code is:

.. code-block:: python

    import ctypes 
    import tisgrabber as tis
    import cv2
    import numpy as np

    ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")

    tis.declareFunctions(ic)

    ic.IC_InitLibrary(0)

    hGrabber = tis.openDevice(ic)

    if(ic.IC_IsDevValid(hGrabber)):
        ic.IC_StartLive(hGrabber, 1)
        key = ""
        while key != "q":
            print("p: Process an image")
            print("q: End program")
            key = input('Enter your choice:')
            if key == "p":
                if ic.IC_SnapImage(hGrabber, 2000) == tis.IC_SUCCESS:
                    # Declare variables of image description
                    Width = ctypes.c_long()
                    Height = ctypes.c_long()
                    BitsPerPixel = ctypes.c_int()
                    colorformat = ctypes.c_int()

                    # Query the values of image description
                    ic.IC_GetImageDescription(hGrabber, Width, Height,
                                            BitsPerPixel, colorformat)

                    # Calculate the buffer size
                    bpp = int(BitsPerPixel.value / 8.0)
                    buffer_size = Width.value * Height.value * BitsPerPixel.value

                    # Get the image data
                    imagePtr = ic.IC_GetImagePtr(hGrabber)

                    imagedata = ctypes.cast(imagePtr,
                                            ctypes.POINTER(ctypes.c_ubyte *
                                                        buffer_size))

                    # Create the numpy array
                    image = np.ndarray(buffer=imagedata.contents,
                                    dtype=np.uint8,
                                    shape=(Height.value,
                                            Width.value,
                                            bpp))

                    # Apply some OpenCV functions on the image
                    image = cv2.flip(image, 0)
                    image = cv2.erode(image, np.ones((11, 11)))

                    cv2.imshow('Window', image)
                    cv2.waitKey(10)
                else:
                    print("No frame received in 2 seconds.")

        ic.IC_StopLive(hGrabber)

        cv2.destroyWindow('Window')

    else:
        ic.IC_MsgBox(tis.T("No device opened"), tis.T("Simple Live Video"))

    ic.IC_ReleaseGrabber(hGrabber)


16 bit support is currently not implemented. 

Frameready Callback
-------------------

Code: 20-Callback.py

IC Imaging Control and *tisgrabber.dll* can call a :index:`callback` function for each incoming 
frame (image) from a video capture device.

In case a camera is used in trigger mode, the usage of a callback is recommended. The 
program does not need to poll for new frames. 

A callback function runs in the :index:`thread` of the ``hGrabber`` object, not in the main thread 
of the program. 

A callback using program needs three things at least:

callback function
    The function to be called.

callback user data
    A simple class with user data to be passed to the callback function.

callback user function pointer
    A pointer to the callback function. Sounds complicate, but is one line of code only.

------------------
Callback User data
------------------
The :index:`user data` class is forwarded to the callback. In the callback the user data
can be used to store information about image processing for the main thread. It can be
create as

.. code-block:: python

    class CallbackUserdata(ctypes.Structure):
        """ Example for user data passed to the callback function. """
        def __init__(self):
            self.Value1 = 42
            self.Value2 = 0
            self.camera = None      # Reference to a camera/grabber object

------------------
Callback Function
------------------

The :index:`callback function` has a fixed parameter list and must be implement as 
follows:

.. code-block:: python

    def FrameCallback(hGrabber, pBuffer, framenumber, pData):
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

At least the definition must be implemented in this way.

-------------------------
Callback Function Pointer
-------------------------

The callback function pointer declaration is in *tisgrabber.py* and it is used in the main 
script as:

.. code-block:: python

    # Create the function pointer.
    Callbackfuncptr = tis.FRAMEREADYCALLBACK(FrameCallback)

Also do not forget to instatiate the user data object:

.. code-block:: python

    Userdata = CallbackUserdata()

After the ``hGrabber`` object has been created, the callback variables can be passed 
to it:

.. code-block:: python

    ic.IC_SetFrameReadyCallback(hGrabber, Callbackfuncptr, Userdata)
    ic.IC_SetContinuousMode(hGrabber, 0)

The ``ic.IC_SetContinuousMode(hGrabber, 0)`` advises the ``hGrabber`` object to call the 
frame callback function automatically, which means without an ``ic.IC_SnapImage()`` call.
(Yes, I know, I switched "0" and "1", when writing the *tisgrabber.dll* in the 
``ic.IC_SetContinuousMode`` function. I am sorry for that.)

The following callback sample shows, how to create a ``numpy`` array for :index:`OpenCV`:

.. code-block:: python

    def LeftCallback(hGrabber, pBuffer, framenumber, pData):
        """ This is an example callback function for image processing  with 
            opencv. The image data in pBuffer is converted into a cv Matrix.
            

        :param: hGrabber: This is the real pointer to the grabber object.
        :param: pBuffer : Pointer to the first pixel's first byte
        :param: framenumber : Number of the frame since the stream started
        :param: pData : Pointer to additional user data structure
        """
        if pData.getNextImage == 1:
            pData.getNextImage = 2
            print("    Left")
            if pData.buffer_size > 0:
                image = ctypes.cast(pBuffer, ctypes.POINTER(ctypes.c_ubyte * pData.buffer_size))

                pData.cvMat = np.ndarray(buffer=image.contents,
                                        dtype=np.uint8,
                                        shape=(pData.height.value,
                                                pData.width.value,
                                                pData.BytesPerPixel))
            pData.getNextImage = 0

It is copied from the *40-qt-stereo.py* example. The *41-qt-triggering.py* sample shows 
how to :index:`signal` the main :index:`Qt` thread from within the callback. The sample 
shows also, how to use the :index:`trigger` correctly.

It is recommended to perform image processing in the callback, because each ``hGrabber`` 
object has its own thread and if many cameras are used, the callback run in own :index:`threads`.
That saves CPU load.

