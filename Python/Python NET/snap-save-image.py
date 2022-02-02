import ctypes as C
import numpy as np
import sys,os
# Import PyhtonNet
import clr
# Load IC Imaging Control .NET 
sys.path.append(os.getenv('IC35PATH') + "/redist/dotnet/x64")

clr.AddReference('TIS.Imaging.ICImagingControl35')
clr.AddReference('System')


# Import the IC Imaging Control namespace.
import TIS.Imaging
from System import TimeSpan



# Create the IC Imaging Control object.
ic = TIS.Imaging.ICImagingControl()

# Create the sink for snapping images on demand.
snapsink = TIS.Imaging.FrameSnapSink(TIS.Imaging.MediaSubtypes.RGB32)
ic.Sink = snapsink

ic.LiveDisplay = False

# Try to open the last used video capture device.
try:
    ic.LoadDeviceStateFromFile("device.xml",True)
    if ic.DeviceValid is True:
        ic.LiveStart()

except Exception as ex:
    ic.ShowDeviceSettingsDialog()
    if ic.DeviceValid is True:
        ic.SaveDeviceStateToFile("device.xml")
        ic.LiveStart()
    pass

# Query some properties

ExposureAuto = ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Exposure,
                TIS.Imaging.VCDIDs.VCDElement_Auto,TIS.Imaging.VCDIDs.VCDInterface_Switch)

#Absolute Values interface for exposure, so we can set exposure times in seconds
ExposureValue = ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Exposure,
                TIS.Imaging.VCDIDs.VCDElement_Value,TIS.Imaging.VCDIDs.VCDInterface_AbsoluteValue)                

# Get the Gainautomatic propertiy
GainAuto = ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Gain,
                TIS.Imaging.VCDIDs.VCDElement_Auto,TIS.Imaging.VCDIDs.VCDInterface_Switch)

# Get the Gain value.
GainValue = ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Gain,
                TIS.Imaging.VCDIDs.VCDElement_Value,TIS.Imaging.VCDIDs.VCDInterface_Range)

# Disable Gain automatic                                
if GainAuto is not None:
    GainAuto.Switch = False

# Set minimum gain
if GainValue is not None:
    GainValue.Value = GainValue.RangeMin

# Disable Exposure automatic
if ExposureAuto is not None:
    ExposureAuto.Switch = False

# Set 1/30 second exosure time
if ExposureValue is not None:
    ExposureValue.Value = 0.0303

# Snap two frames in order to be sure, the set values became effective in the sensor.
# It is not necessary, if we do not change properties directly before snapping an image.
# use the second frame
snapsink.SnapSingle(TimeSpan.FromSeconds(5))
frame = snapsink.SnapSingle(TimeSpan.FromSeconds(5))

# Save the frame as JPG with 75% quality.
TIS.Imaging.FrameExtensions.SaveAsJpeg( frame,"image.jpg", 75)

print("Image saved.")
#End the live video
ic.LiveStop()
ic.Dispose()

