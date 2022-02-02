'''
This sample demonstrates how to use a frame filter. It loads
the IC Imaging Control built in RotateFlip filter.

The IC Imaging Control standard frame filters are documented at
https://www.theimagingsource.com/support/documentation/ic-imaging-control-cpp/ref_stdfilter.htm
'''
import time
import ctypes
import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)

ic.IC_InitLibrary(0)

# Get count of available frame filters
FilterCount = ic.IC_GetAvailableFrameFilterCount()
print("{} Frame Filters are available.".format(FilterCount))

# Get the names of available frame filters
string_buffers = [ctypes.create_string_buffer(50) for i in range(FilterCount)]
pointers = (ctypes.c_char_p*FilterCount)(*map(ctypes.addressof, string_buffers))
ic.IC_GetAvailableFrameFilters(pointers, FilterCount)
Filterlist = [s.value for s in string_buffers]
print("Available Frame Filters")
for filter in Filterlist:
    print(tis.D(filter))

# Create the handle of one frame filter, we want to use.
FilterHandle = tis.HFRAMEFILTER()

'''
For using the Rotate Flip frame filter, please refer to
https://www.theimagingsource.com/support/documentation/ic-imaging-control-cpp/stdfilterRotateFlip.htm

For frame filter parameter types see FRAMEFILTER_PARAM_TYPE class in
tisgrabber.py
'''

# Create the frame filter and list its parameters
if ic.IC_CreateFrameFilter(tis.T("Rotate Flip"), FilterHandle) == tis.IC_SUCCESS:
    print("Frame filter loaded. Available Parameters are:")
    for i in range(0, FilterHandle.ParameterCount):
        print("Name: {}, Type: {}".format(FilterHandle.Parameters[i].Name, FilterHandle.Parameters[i].Type))
else:
    print("Frame filter load failed")
    quit()

hGrabber = ic.IC_ShowDeviceSelectionDialog(None)

if(ic.IC_IsDevValid(hGrabber)):
    # Add the frame filter to our hGrabber.
    ic.IC_AddFrameFilterToDevice(hGrabber, FilterHandle)

    # Set the frame filter parameters
    ic.IC_FrameFilterSetParameterInt(FilterHandle, tis.T("Rotation Angle"), 90)
    ic.IC_StartLive(hGrabber, 1)
    time.sleep(2)
    ic.IC_StopLive(hGrabber)

    ic.IC_FrameFilterSetParameterInt(FilterHandle, tis.T("Rotation Angle"), 180)
    ic.IC_StartLive(hGrabber, 1)
    time.sleep(2)
    ic.IC_StopLive(hGrabber)

    ic.IC_FrameFilterSetParameterInt(FilterHandle, tis.T("Rotation Angle"), 270)
    ic.IC_StartLive(hGrabber, 1)
    time.sleep(2)
    ic.IC_StopLive(hGrabber)

    ic.IC_FrameFilterSetParameterInt(FilterHandle, tis.T("Rotation Angle"), 0)
    ic.IC_StartLive(hGrabber, 1)
    time.sleep(2)

    ic.IC_FrameFilterSetParameterBoolean(FilterHandle, tis.T("Flip H"), 1)
    time.sleep(2)

    ic.IC_FrameFilterSetParameterBoolean(FilterHandle, tis.T("Flip V"), 1)
    time.sleep(2)

    ic.IC_FrameFilterSetParameterBoolean(FilterHandle, tis.T("Flip H"), 0)
    time.sleep(2)

    ic.IC_FrameFilterSetParameterBoolean(FilterHandle, tis.T("Flip V"), 0)
    time.sleep(2)
    ic.IC_StopLive(hGrabber)

    ic.IC_FrameFilterDeviceClear(hGrabber)

else:
    ic.IC_MsgBox(tis.T("No device opened"), tis.T("Callback"))

ic.IC_ReleaseGrabber(hGrabber)
