# Save an Image on Software Trigger

This C# sample demonstrates, how to save an image from a camera on software trigger. If a camera is used in trigger mode, then it is recommended to use a callback for handling incoming images. This avoids a blocking wait for a new image. The callback is the IC Imaging Control [ImageAvailable](https://www.theimagingsource.com/support/documentation/ic-imaging-control-net/event_descICImagingControl_ImageAvailable.htm) event.

In order to call the ImageAvailable event automatically, the [LiveCaptureContinous](https://www.theimagingsource.com/support/documentation/ic-imaging-control-net/prop_descICImagingControl_LiveCaptureContinuous.htm) property must be set to true. 

This sample shows also, how to query the Trigger Mode property and Software Trigger property using the IC Imaging Control VCDProperty class.

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIS.Imaging;

namespace SoftwareTrigger
{
    class SoftwareTrigger
    {
        // Counter for images to be saved.
        private int counter = 1; 
        // Create a new IC Imaging Control object,
        private ICImagingControl ic = new ICImagingControl();

        public SoftwareTrigger()
        {
            ic.LiveCaptureContinuous = true; //Call ImageAvailable event for new images.
            ic.LiveCaptureLastImage = false; // Do not save an image on live stop.

            // Add the ImageAvailable handler to the IC Imaging Control object.
            ic.ImageAvailable += new EventHandler<ICImagingControl.ImageAvailableEventArgs>(ic_ImageAvailable);

            ic.ShowDeviceSettingsDialog(); // Select a video capture device
            if (!ic.DeviceValid)
                return;

            // Query the trigger mode property for enabling the trigger mode    
            VCDSwitchProperty TriggerMode = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface(VCDIDs.VCDID_TriggerMode, VCDIDs.VCDElement_Value, VCDIDs.VCDInterface_Switch);
            if (TriggerMode == null)
                return;

            // If trigger mode is available, query the software trigger property
            VCDButtonProperty SoftwareTrigger = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface(VCDIDs.VCDID_TriggerMode, VCDIDs.VCDElement_SoftwareTrigger, VCDIDs.VCDInterface_Button);
            if (SoftwareTrigger == null)
                return;
                
            TriggerMode.Switch = true; // Enable trigger mode,

            ic.LiveStart(); // start the camera. No images are streamed, because trigger mode is enabled
            System.Threading.Thread.Sleep(1000);
            SoftwareTrigger.Push(); // Do a software trigger.
            System.Threading.Thread.Sleep(1000);
            SoftwareTrigger.Push(); // Do another software trigger.
            System.Threading.Thread.Sleep(1000);

            ic.LiveStop(); // Stop live video.
            TriggerMode.Switch = false; // Disable trigger mode again. 

        }

        // ImageAvailable event handler. The parameter "e" contains the imagebuffer with the
        // currently snapped image.
        // The image is saved as JPEG file.
        void ic_ImageAvailable(object sender, ICImagingControl.ImageAvailableEventArgs e)
        {
            string filename = String.Format("test{0}.jpg", counter);
            e.ImageBuffer.SaveAsJpeg(filename, 70);
            counter++;
            Console.WriteLine("Saved: " + filename);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new SoftwareTrigger();                        
        }
    }
}
```
