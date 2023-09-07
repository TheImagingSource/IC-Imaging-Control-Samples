/*
 * This sample demonstrates, how to save images on a trigger event
 * to a file. It uses a FrameQueueSink for this task.
 */ 

using System;
using TIS.Imaging;

namespace Save_Triggered_Image
{
    class Program
    {
        static int imagecount = 0;
        static void Main(string[] args)
        {
            ICImagingControl ic = new ICImagingControl();

            // Create our FrameQueueSink with RGB32 pixelformat and 5 
            // Buffers.
            ic.Sink = new TIS.Imaging.FrameQueueSink((img) => ProcessImage(img),
                TIS.Imaging.MediaSubtypes.RGB32,
                5);

            // IC does not show a live video in a console program.
            ic.LiveDisplay = false; 

            // Select a camera,
            ic.ShowDeviceSettingsDialog();
            if (!ic.DeviceValid)
                return;

            // Enable trigger mode
            var triggerMode = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_TriggerMode, VCDGUIDs.VCDElement_Value);
            if (triggerMode != null) 
                triggerMode.Switch = true;

            // Start capturing.
            ic.LiveStart();
            while(imagecount < 10)
                System.Threading.Thread.Sleep(100);

            // Clean up.
            ic.LiveStop();
            // Disable trigger mode
            if (triggerMode != null)
                triggerMode.Switch = false;

            Console.WriteLine("Done. Press key to end.");
            Console.Read();
        }
        /// <summary>
        /// Callback of the FrameQueuesink. It is called, as long
        /// as there are frames in the queue.
        /// </summary>
        /// <param name="imgBuffer">Contains the image data.</param>
        /// <returns></returns>
        static TIS.Imaging.FrameQueuedResult ProcessImage(TIS.Imaging.IFrameQueueBuffer imgBuffer)
        {
            imagecount++;
            // Create a file name and save the image.
            var filename = $"img{imagecount:0000}.bmp";
            FrameExtensions.SaveAsBitmap(imgBuffer, filename );

            Console.WriteLine(imagecount);
            return TIS.Imaging.FrameQueuedResult.ReQueue;
        }
    }
}
