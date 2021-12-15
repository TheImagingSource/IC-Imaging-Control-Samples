"""
This sample shows, how to get a list of connected
video capture devices.
"""

import ctypes
import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)

ic.IC_InitLibrary(0)
devicecount = ic.IC_GetDeviceCount()
for i in range(0, devicecount):
    print("Device {}".format(tis.D(ic.IC_GetDevice(i))))
    print("Unique Name : {}".format(tis.D(ic.IC_GetUniqueNamefromList(i))))