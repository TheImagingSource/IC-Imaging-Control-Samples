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
