
============
Using Images
============

After device handling the next step is getting images. 
This chapter shows how to query images from the camera and how to process them by OpenCV.

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


The parameter ``tis.ImageFileTypes['JPEG']`` indicates, that the image is to be saved as JPEG image. The other possibility is Bitmap: ``tis.ImageFileTypes['BMP']``.

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


The function ``IC_SnapImage()`` is not suitable, if the camera is in trigger mode, because 
frames can be missed. This function also blocks the program execution, if no frame is delivered. 
Please use a frame delivery callback function for triggered cameras . How to do this is explained later.

Snapping an Image and Process it with OpenCV 
--------------------------------------------

Code : 11-image-processing

This sample shows, how to

* get an image descriptions of width, height and bits per pixel
* get a pointer to the image data and convert it to Python use
* create a ``numpy`` array for OpenCV image processing

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



