# Python samples for Windows NET

## Requirements
* IC Imaging Control 3.5
* The Imaging Source camera or video capture device
* Python 3.8
* pyqt5
* pythonnet http://pythonnet.github.io/

These samples show how to use IC Imaging Control 3.5.

The qt5-simple.py sample simply displays the live video in a QT5 widget. For doing so, a frame filter is inserted into the display path of IC Imaging Control. The filter converts the IFrame of IC Imaging control into a QImage and mirros it. 
Then a signal is sent to the main thread.

The qt5-imageprocessing.py sample enhances the qt5-simple.py with a frame callback and some simple image processing. The results are signaled to the main thread again.

In order to run the samples, the IC Imaging Control 3.5 component and its depenedendcies must be copied from

Documents\IC Imaging Control 3.5\redist\dotnet\x64\ for 64 bit

or

Documents\IC Imaging Control 3.5\redist\dotnet\x86\ for 32bit 


The snap-save-image.py sample shows in a very simply way how to
* open a video capture device
* save and restore a device state of a video capture device
* set automatic and value properties, such as gain and exposure
* snap a frame from the live stream
* save the snapped frame into a JPEG file.

The sample has no GUI, therefore, there is no live display from the video capture device shown.

## Camera Properties
The usual VCD Properties of IC Imaging Control are used here. Please refer to [VCDProperties](https://www.theimagingsource.com/support/documentation/ic-imaging-control-cpp/tech_VCDProperties.htm)

If IC Imaging Control .NET SDK is installed on the computer, then the VCDProperty Inspector program can be used to query the VCDIDs, VCDElements and VCDInterfaces of the camera properties.

Getting an interface to a property is simple:
``` Python
# Get the Gain value.
GainValue = ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Gain,
                TIS.Imaging.VCDIDs.VCDElement_Value,TIS.Imaging.VCDIDs.VCDInterface_Range)
```
Setting the value is also simple:
``` Python
# Set minimum gain
if GainValue is not None:
    GainValue.Value = 10
```
Setting the minumum value or maximim value:
``` Python
# Set minimum gain
if GainValue is not None:
    GainValue.Value = GainValue.RangeMin
```
This is similar for all numeric video capture device properties. Take care of properties, that also have an automatic. That must be disabled before a value can be set manually. For gain this is:

``` Python
# Get the Gain automatic property
GainAuto = ic.VCDPropertyItems.FindInterface(TIS.Imaging.VCDIDs.VCDID_Gain,
                TIS.Imaging.VCDIDs.VCDElement_Auto,TIS.Imaging.VCDIDs.VCDInterface_Switch)

# Disable Gain automatic                                
if GainAuto is not None:
    GainAuto.Switch = False                
```
# Snap and save a frame
For snapping a frame we use the [FrameSnapSink](https://www.theimagingsource.com/support/documentation/ic-imaging-control-cpp/FrameSnapSink.htm). It is created and passed to the IC Imaging Control instance before the live video stream is started. This can be done even before a video capture device is opened.

``` Python
snapsink = TIS.Imaging.FrameSnapSink(TIS.Imaging.MediaSubtypes.RGB32)
```
The pixel format in the sink is RGB32 as we passed this format to the constructor. Now the sink must be assigned to the IC Imaging Control instance:
``` Python
ic.Sink = snapsink
```
Now the video capture device can opened, if not already done. The live stream can be started now.

A frame is snapped from the live stream by a call to:
``` Python
frame = snapsink.SnapSingle(TimeSpan.FromSeconds(5))
```
This frame can be converted to a numpy array (see the QT5 samples) or we can simply save it to a file:
``` Python
# Save the frame as JPG with 75% quality.
TIS.Imaging.FrameExtensions.SaveAsJpeg( frame,"image.jpg", 75)
```


