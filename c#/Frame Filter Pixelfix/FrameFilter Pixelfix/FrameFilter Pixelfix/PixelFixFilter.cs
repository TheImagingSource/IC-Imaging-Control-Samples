using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TIS.Imaging;

/* 
 * Using the filter
 * 
 * 1.) In your main form create a variable of this class type:
 * 

 *     private TIS.Imaging.PixelFixFilter MyPixelFixFilter;

 *
 * 2.) Instantiate the class in the form load method of your main form:
 * 

 *		MyPixelFixFilter = new TIS.Imaging.PixelFixFilter();

 * 
 * 3.) Insert the filter in IC Imaging Control, e.g. as device filter. This can be
 *     done in the form load event too:
 * 
 *     icImagingControl1.DeviceFrameFilters.Add(  icImagingControl1.FrameFilterCreate( MyPixelFixFilter ) );
 * 
 * 4.) To see the result of the filter, enable it:
 * 
 *     MyPixelFixFilter.setEnable(true);
 * 
 * A form load method could look like the following code:
 * 
 *      private void Form1_Load(object sender, System.EventArgs e)
 *		{

 *          MyPixelFixFilter = new TIS.Imaging.PixelFixFilter();

 *			icImagingControl1.DeviceFrameFilters.Add(  icImagingControl1.FrameFilterCreate( MyPixelFixFilter ) );
 *			MyPixelFixFilter.setEnable(true);
 *		}
 */

namespace TIS.Imaging
{
    public class PixelFixFilter : FrameFilterImpl
    {
        string dataname;
        string serialnumber;
        int highestWidth, highestHeight;
        VCDRangeProperty offsetX, offsetY;
        private List<System.Drawing.Point> listOfCoordinates = new List<System.Drawing.Point>();
        private bool m_bEnabled = false; // This variable is used to enable and disable the image processing.
        ICImagingControl ic;

        public PixelFixFilter(ICImagingControl icImagingControl1)
        {
            //TODO: Insert your own initializations here.
            ic = icImagingControl1;
        }

        /*
         *	Enables or disables the filter.
         *
         *	Only call this method in a beginParamTransfer/endParamTransfer block.
         */
        public void setEnable(bool bEnable)
        {
            m_bEnabled = bEnable;
        }

        /*
         *	Get the current enabled state of the binarization filter.
         *
         *	Only call this method in a beginParamTransfer/endParamTransfer block.
         */
        public bool getEnable()
        {
            return m_bEnabled;
        }

        /*
         * This method fills the ArrayList arr with the frame types this filter
         * accepts as input.
         */
        public override void GetSupportedInputTypes(System.Collections.ArrayList frameTypes)
        {
            // This filter works for all color formats
            frameTypes.Add(new TIS.Imaging.FrameType(TIS.Imaging.MediaSubtypes.Y800));
            frameTypes.Add(new TIS.Imaging.FrameType(TIS.Imaging.MediaSubtypes.RGB24));
            frameTypes.Add(new TIS.Imaging.FrameType(TIS.Imaging.MediaSubtypes.RGB32));
            frameTypes.Add(new TIS.Imaging.FrameType(TIS.Imaging.MediaSubtypes.RGB64));
            frameTypes.Add(new TIS.Imaging.FrameType(TIS.Imaging.MediaSubtypes.Y16));
        }

        /*
         * The NotifyStart method is called, when the live video is to be started.
         */
        public override void NotifyStart(FrameType inType, FrameType outType)
        {
            //TODO: Insert your own initializations here.
            // ToDo: Pixel Liste Laden
            base.NotifyStart(inType, outType);
            listOfCoordinates.Clear();
            serialnumber = ic.DeviceCurrent.GetSerialNumber();
            readXML();
            getHighestResolution();
            offsetX = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_PartialScanOffset, VCDGUIDs.VCDElement_PartialScanOffsetX, VCDGUIDs.VCDInterface_Range);
            offsetY = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_PartialScanOffset, VCDGUIDs.VCDElement_PartialScanOffsetY, VCDGUIDs.VCDInterface_Range);

        }

        /*
         * The NotifyStop method is called, when the live video is to be stopped.
         */
        public override void NotifyStop()
        {
            base.NotifyStop();

        }



        /*
         *	This method returns the output frame type for a given input frame type.
         *
         */
        public override bool GetTransformOutputTypes(FrameType inType, System.Collections.ArrayList outTypes)
        {
            outTypes.Add(inType);

            return true;
        }

        /*
         *	This method is called to copy image data from the src frame to the dest frame.
         *
         *	Depending on the value of m_bEnabled, this implementation applies a binarization or
         *	copies the image data without modifying it.
         */
        public override bool Transform(IFrame src, IFrame dest)
        {
            unsafe
            {
                // Check whether the destination frame is available
                if (dest.Ptr == null) return false;

                // Copy the member variables to the function's stack, to protect them from being
                // overwritten by parallel calls to setThreshold() etc.
                //
                // beginParamTransfer/endParamTransfer makes sure that the values from various
                // member variables are consistent, because the user of this filter must enclose
                // writing parameter access into beginParamTransfer/endParamTransfer, too.
                BeginParameterTransfer();
                bool enabled = m_bEnabled;
                EndParameterTransfer();

                dest.CopyFrom(src);

                byte* pIn = src.Ptr;
                byte* pOut = dest.Ptr;

                // Correction of the Hotpixel
                byte* Hotpixel;
                int bytesPerPixel = dest.FrameType.BitsPerPixel / 8;
                if (enabled)
                {
                    int pixelsLeftX = highestWidth - dest.FrameType.Width;
                    int pixelsLeftY = highestHeight - dest.FrameType.Height;
                    int oX = offsetX.Value;
                    int oY = offsetY.Value;
                    if (offsetX.Value > pixelsLeftX)
                    {
                        oX = pixelsLeftX;
                    }
                    if (offsetY.Value > pixelsLeftY)
                    {
                        oY = pixelsLeftY;
                    }

                    foreach (var p in listOfCoordinates)
                    {
                        if (p.X < oX)
                            continue;
                        if (p.Y < oY)
                            continue;
                        if (p.X >= dest.FrameType.Width + oX)
                            continue;
                        if (p.Y >= dest.FrameType.Height + oY)
                            continue;

                        if (dest.FrameType.IsBottomUp)
                        {
                            Hotpixel = pOut + ((dest.FrameType.Height - 1 - (p.Y - oY)) * dest.FrameType.Width + (p.X - oX)) * bytesPerPixel;
                        }
                        else
                        {
                            Hotpixel = pOut + ((p.Y - oY) * dest.FrameType.Width + (p.X - oX)) * bytesPerPixel;
                        }
                        if (Hotpixel < pOut + dest.FrameType.BufferSize - bytesPerPixel)
                        {
                            for (int i = 0; i < bytesPerPixel; i++)
                                Hotpixel[i] = Hotpixel[i + bytesPerPixel];
                        }
                    }

                }


            }

            return true;
        }

        /// <summary>
        /// Read the nessecary XML with the list of Hotpixels.
        /// </summary>
        private void readXML()
        {
            var pathOfExecutable = Environment.CurrentDirectory;
            
            dataname = String.Format("pixelfix-{0}.xml", serialnumber);
            String filepath = System.IO.Path.Combine(pathOfExecutable, dataname);
            if (File.Exists(filepath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<System.Drawing.Point>));
                FileStream fs = new FileStream(filepath, FileMode.Open);
                List<System.Drawing.Point> list;
                list = (List<System.Drawing.Point>)serializer.Deserialize(fs);
                listOfCoordinates = list;
                fs.Close();
            }
        }


        /// <summary>
        /// Finds the Highest Resolution of the used Pixelformat
        /// 
        /// </summary>
        private void getHighestResolution()
        {
            foreach (VideoFormatDesc VFD in ic.VideoFormatDescs)
            {
                highestWidth = 1;
                highestHeight = 1;

                highestWidth = (ic.Width / VFD.StepSize.Width) * VFD.StepSize.Width;
                highestHeight = (ic.Height / VFD.StepSize.Height) * VFD.StepSize.Height;

                if (highestWidth > VFD.MaxSize.Width)
                    highestWidth = VFD.MaxSize.Width;

                if (highestHeight > VFD.MaxSize.Height)
                    highestHeight = VFD.MaxSize.Height;

                highestWidth = VFD.MaxSize.Width;
                highestHeight = VFD.MaxSize.Height;

            }
        }
    }
}
