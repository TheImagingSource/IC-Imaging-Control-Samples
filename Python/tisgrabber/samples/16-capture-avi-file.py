"""
This sample shows, how to capture an AVI video file.
Currently installed Windows codecs are supported only. 
Uncompressed AVI capture is not available at this moment.

In order to query a list of installed codecs use the 
15-list-codecs.py sample

The sample shows, how to show the codec's property dialog
Set the file name
Pause and unpause AVI capture
"""

import ctypes 
import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")

tis.declareFunctions(ic)

ic.IC_InitLibrary(0)

hGrabber = tis.openDevice(ic)

if(ic.IC_IsDevValid(hGrabber)):
    codec = ic.IC_Codec_Create(tis.T("MJPEG Compressor"))
    """ Show the codec's property page, if it has one. """
    if ic.IC_Codec_hasDialog(codec):
        ic.IC_Codec_showDialog(codec)

    ic.IC_SetCodec(hGrabber, codec)                         # Assign the selected codec to the grabber
    ic.IC_SetAVIFileName(hGrabber, tis.T("test.avi"))       # Assign file name
    ic.IC_enableAVICapturePause(hGrabber, 1)                # Pause avi capture.
    
    if ic.IC_StartLive(hGrabber, 1) == tis.IC_SUCCESS:

        ic.IC_MsgBox(tis.T("Click to start capture"), tis.T("AVI Capture"))
        ic.IC_enableAVICapturePause(hGrabber, 0)         # Unpause avi capture.

        ic.IC_MsgBox(tis.T("Click to stop capture"), tis.T("AVI Capture"))
        ic.IC_StopLive(hGrabber)
    else:
        ic.IC_MsgBox(tis.T("Failed to start stream."), tis.T("AVI Capture"))

    ic.IC_Codec_Release(codec)
else:
    ic.IC_MsgBox(tis.T("No device opened"), tis.T("Simple Live Video"))

ic.IC_ReleaseGrabber(hGrabber)
