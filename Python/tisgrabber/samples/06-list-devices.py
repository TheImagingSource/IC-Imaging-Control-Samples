"""
This sample shows, how to get a list of connected
video capture devices.
In the commented section is shown, how to use grabber
objects in an array and how to open them all in a 
loop.
"""

import ctypes
import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)

ic.IC_InitLibrary(0)
devicecount = ic.IC_GetDeviceCount()
grabbers = []
for i in range(0, devicecount):
    print("Device {}".format(tis.D(ic.IC_GetDevice(i))))
    uniquename = tis.D(ic.IC_GetUniqueNamefromList(i))
    print("Unique Name : {}".format(uniquename))

"""
    # Uncomment this section, if you want to open all
    # video capture devices and show their live streams.

    g = ic.IC_CreateGrabber()
    ic.IC_OpenDevByUniqueName(g, tis.T(uniquename))
    grabbers.append(g)

for grabber in grabbers:
    if(ic.IC_IsDevValid(grabber)):
        ic.IC_StartLive(grabber, 1)

ic.IC_MsgBox(tis.T("Stop'em all!"), tis.T("Live Video"))

for grabber in grabbers:
    if(ic.IC_IsDevValid(grabber)):
        ic.IC_StopLive(grabber)

for grabber in grabbers:
    if(ic.IC_IsDevValid(grabber)):
        ic.IC_ReleaseGrabber(grabber)

"""
