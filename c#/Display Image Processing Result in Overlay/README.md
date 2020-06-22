# Display Result of Image Processing in Overlay

This C# programming example shows how to display the result of an image processing in the Overlay Bitmap in the display path.

## Requirements
* The Imaging Source video capture device
* IC Imaging Control 3.5
* MS Visual Studio C# 2017 or above (The code can also be used in C# 2010.)

## Introduction
The sample uses the [FrameQueueSink](https://www.theimagingsource.com/support/documentation/ic-imaging-control-net/FrameQueueSink.htm), which was introduced by [IC Imaging Control 3.5](https://www.theimagingsource.com/support/downloads-for-windows/software-development-kits-sdks/icimagingcontrolcsharp/). It succeeds the former ImageAvailable event. It displays the the B, G, R and A values of the first pixel of the image on the live video display.


## Creating the sink

``` C#
// Setup up sink for continuous based image processing
// The pixel format in the image buffers will be 32bit.
icImagingControl1.Sink = new TIS.Imaging.FrameQueueSink((img) => ProcessImage(img), MediaSubtypes.RGB32, 5); 
```
This line creates the ```FrameQueueSink``` directly in the ICImagingControl object. It receives the name of the function, that is called for each image ```ProcessImage```, the pixel format, which the image buffers consist of and the number of queued image buffers. This number should be greater than one always.

## The FrameQueueSink Callback
``` C#
/// <summary>
/// The callback of the FrameQueueSink
/// </summary>
private TIS.Imaging.FrameQueuedResult ProcessImage(TIS.Imaging.IFrameQueueBuffer imgBuffer)
{
    try
    {
        var Overlay = icImagingControl1.OverlayBitmapAtPath[TIS.Imaging.PathPositions.Display];
        Overlay.Enable = true;
        // Start the image processing. It is contained in an "unsafe" section,
        // because a pointer to the image data is used.
        unsafe
        {
            // TODO: Insert your own image processing here.
            byte* pDatabyte = imgBuffer.Ptr;
            String Text = String.Format("Brigthness {0:X02} {1:X02} {2:X02} {3:X02}", pDatabyte[0], pDatabyte[1], pDatabyte[2], pDatabyte[3]);
            Overlay.DrawText(Color.Red, 10, 10, Text);
        }
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine(ex.Message);
    }
    return TIS.Imaging.FrameQueuedResult.ReQueue;
}
```
This function is called for each incoming frame by the FrameQueueSink, as long as there are image buffers queued. It draws the a text on the Overlay Bitmap only. It is intended to be very simple here, because this sample shows, how to use the ```FrameQueueSink``` and ```OverlayBitmap``` only.

# The OverlayBitmap
The OverlayBitmap is placed in the display path only, otherwise it changes the image data:
``` C#
icImagingControl1.OverlayBitmapPosition = TIS.Imaging.PathPositions.Display; 
```