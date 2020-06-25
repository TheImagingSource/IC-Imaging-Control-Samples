using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using TIS.Imaging;

namespace Build_Pixelfix
{
    public partial class Form1 : Form
    {
        // This attribute controls whether the live display is scaled in order
        // to match the size of the IC Imaging Control window on the form.
        public event EventHandler<BitmapEventArgs> DisplayEvent;
        private bool m_bFitImageToWindow = true;
        private bool _findHotPixel = false;
        private List<System.Drawing.Point> listOfCoordinates = new List<System.Drawing.Point>();

        private FrameQueueSink _sink;
        private int highestWidth;
        private int highestHeight;
        VCDRangeProperty _gain;
        private int _Threshold = 100;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _sink = new FrameQueueSink(ProcessBuffer, MediaSubtypes.Y800, 20);
            icImagingControl1.Sink = _sink;
            icImagingControl1.LiveDisplay = false;

            if (m_bFitImageToWindow)
            {
                // Adjust live display size to the control size.
                icImagingControl1.LiveDisplayDefault = false;
                icImagingControl1.LiveDisplaySize = icImagingControl1.Size;
            }
            tbThreshold.Value = _Threshold;
            tbGain.Enabled = false;
            btnSave.Enabled = false;
            DisplayEvent += OnDisplay;
        }

        /// <summary>
        /// Stop the live video stream, if the application is closed.
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (icImagingControl1.DeviceValid)
            {
                icImagingControl1.LiveStop();
            }
        }

        /// <summary>
        /// Adjust live display size to the control size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (m_bFitImageToWindow)
            {
                if (icImagingControl1.DeviceValid)
                {
                    icImagingControl1.LiveDisplaySize = icImagingControl1.Size;
                }
            }
        }

        /// <summary>
        /// Show the device selection dialog of IC Imaging Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDevice_Click(object sender, EventArgs e)
        {
            OpenNewVideoCaptureDevice();
            getHighestResolution();
            icImagingControl1.VideoFormat = String.Format("Y800 " + "({0}x{1})", highestWidth, highestHeight);
            if (icImagingControl1.DeviceValid)
            {
                _gain = (VCDRangeProperty)icImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_Gain, VCDGUIDs.VCDElement_Value, VCDGUIDs.VCDInterface_Range);
                if (_gain != null)
                {
                    _gain.Value =  (int)((double)_gain.RangeMax * 0.3);
                    tbGain.Minimum = _gain.RangeMin;
                    tbGain.Maximum = _gain.RangeMax;
                    tbGain.Value = _gain.Value;
                    tbGain.Enabled = true;
                }
                icImagingControl1.LiveStart();
                prepareProperties(false);
                _findHotPixel = true;
                btnSave.Enabled = true;
            }
        }

        /// <summary>
        /// Show the device selection dialog of IC Imaging Control.
        /// </summary>
        private void OpenNewVideoCaptureDevice()
        {
            if (icImagingControl1.DeviceValid)
            {
                icImagingControl1.LiveStop();
            }
            else
            {
                icImagingControl1.Device = "";
            }
            icImagingControl1.ShowDeviceSettingsDialog();

            if (icImagingControl1.DeviceValid)
            {
                // Save the currently used device into a file in order to be able to open it
                // automatically at the next program start.
                icImagingControl1.SaveDeviceStateToFile("device.xml");
            }
        }

        /// <summary>
        /// Show the device's property dialog for modifying exposure, brightness etc. 
        /// All changes are saved to the file 'device.xml'.
        /// </summary>
        private void ShowDeviceProperties()
        {
            if (icImagingControl1.DeviceValid)
            {
                icImagingControl1.ShowPropertyDialog();
                icImagingControl1.SaveDeviceStateToFile("device.xml");
            }
        }


        /// <summary>
        /// Handle the DeviceLost event.
        /// </summary>
        private void DeviceLost()
        {
            MessageBox.Show("Device Lost!");
        }

        private void icImagingControl1_DeviceLost(object sender, TIS.Imaging.ICImagingControl.DeviceLostEventArgs e)
        {
            // We BeginInvoke this so that showing the modal dialog does not block actually closing the device in IC
            BeginInvoke(new Action(() => DeviceLost()));
        }

        /// <summary>
        /// Callback of our FrameQueueSink. 
        /// </summary>
        /// <param name="imgBuffer"></param>
        /// <returns></returns>
        private TIS.Imaging.FrameQueuedResult ProcessBuffer(IFrameQueueBuffer imgBuffer)
        {
            try
            {
                if (_findHotPixel)
                {
                    buildHotPixelList(imgBuffer);
                    //_findHotPixel = false;
                }
                if (radioBtn_ShowHotpixel.Checked == true)
                    DisplayBufferInPictureBox(imgBuffer);
                else
                {
                    fixHotPixel(imgBuffer);
                    DisplayBufferInPictureBox(imgBuffer);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return TIS.Imaging.FrameQueuedResult.ReQueue;
        }

        /// <summary>
        /// Using the list of hotpixels in order to substitute the hotpixels. This 
        /// is the same function as in the Hotpixel Frame Filter.
        /// </summary>
        /// <param name="imgBuffer"></param>
        private void fixHotPixel(IFrameQueueBuffer imgBuffer)
        {
            unsafe
            {
                byte* Hotpixel;
                int bytesPerPixel = imgBuffer.FrameType.BitsPerPixel / 8;
                VCDRangeProperty offsetX = (VCDRangeProperty)icImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_PartialScanOffset, VCDGUIDs.VCDElement_PartialScanOffsetX, VCDGUIDs.VCDInterface_Range);
                VCDRangeProperty offsetY = (VCDRangeProperty)icImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_PartialScanOffset, VCDGUIDs.VCDElement_PartialScanOffsetY, VCDGUIDs.VCDInterface_Range);
                int pixelsLeftX = highestWidth - imgBuffer.FrameType.Width;
                int pixelsLeftY = highestHeight - imgBuffer.FrameType.Height;
                int oX = offsetX.Value;
                int oY = offsetY.Value;
                byte* pstart = imgBuffer.Ptr;
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
                    if (p.X >= imgBuffer.FrameType.Width + oX)
                        continue;
                    if (p.Y >= imgBuffer.FrameType.Height + oY)
                        continue;

                    if (imgBuffer.FrameType.IsBottomUp)
                    {
                        Hotpixel = pstart + ((imgBuffer.FrameType.Height - 1 - (p.Y - oY)) * imgBuffer.FrameType.Width + (p.X - oX)) * bytesPerPixel;
                    }
                    else
                    {
                        Hotpixel = pstart + ((p.Y - oY) * imgBuffer.FrameType.Width + (p.X - oX)) * bytesPerPixel;
                    }
                    if (Hotpixel < pstart + imgBuffer.FrameType.BufferSize - bytesPerPixel)
                    {
                        for (int i = 0; i < bytesPerPixel; i++)
                            Hotpixel[i] = Hotpixel[i + bytesPerPixel];
                    }
                }
            }
        }

        /// <summary>
        /// Create the list of hotpixels.
        /// </summary>
        /// <param name="buffer"></param>
        private void buildHotPixelList(IFrameQueueBuffer buffer)
        {
            try
            {
                listOfCoordinates.Clear();

                int x, y;
                unsafe
                {
                    byte* start = buffer.Ptr;
                    for (y = 0; y < buffer.FrameType.Height; y++)
                    {
                        int y1 = buffer.FrameType.Height - y;
                        for (x = 0; x < buffer.FrameType.Width; x++)
                        {
                            var pixel = start + (y * buffer.FrameType.Width + x);

                            if ((*pixel) >= _Threshold)
                            {
                                listOfCoordinates.Add(new Point(x, y));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Draw the image buffer into a System.Drawing.Bitmap. If the hotpixels 
        /// shall be displayed, red circles are drawn around them.
        /// Then the Display Event is called for displayin in  the Picturebox.
        /// </summary>
        /// <param name="imgBuffer">IFrame containing the handled image.</param>
        private void DisplayBufferInPictureBox(IFrameQueueBuffer imgBuffer)
        {
            Bitmap DisplayImage = new Bitmap(imgBuffer.FrameType.Width, imgBuffer.FrameType.Height);

            using (Graphics graphics = Graphics.FromImage(DisplayImage))
            {
                graphics.DrawImage(imgBuffer.CreateBitmapWrap(), 0, 0);
                Pen pen = Pens.Red;
                if (radioBtn_ShowHotpixel.Checked == true)
                {
                    foreach (var p in listOfCoordinates)
                    {
                        graphics.DrawEllipse(pen, p.X - 5, p.Y - 5, 10, 10);
                    }
                }
                
            }

            DisplayEvent?.BeginInvoke(this, new BitmapEventArgs(DisplayImage), null, null);
        }
        
        /// <summary>
        /// Disable the camera automatics for gain and exposure. Set exposure time to at least 0.25 seconds.
        /// </summary>
        /// <param name="value">Automatics on and off. True = on, False = off</param>
        private void prepareProperties(bool value)
        {
            VCDSwitchProperty exposureauto = (VCDSwitchProperty)icImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_Exposure, VCDGUIDs.VCDElement_Auto);
            if (exposureauto != null)
                exposureauto.Switch = value;

            VCDSwitchProperty gainauto = (VCDSwitchProperty)icImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_Gain, VCDGUIDs.VCDElement_Auto);
            if (gainauto != null)
                gainauto.Switch = value;

            if (!value)
            {
                VCDAbsoluteValueProperty exposureTime = (VCDAbsoluteValueProperty)icImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_Exposure,
                                        VCDGUIDs.VCDElement_Value, VCDGUIDs.VCDInterface_AbsoluteValue);
                if (exposureTime != null)
                    exposureTime.Value = Math.Min(exposureTime.RangeMax, 0.25);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Help_Click(object sender, EventArgs e)
        {
            var pathOfExecutable = Application.ExecutablePath;
            string pathOfExecutableDirectory = System.IO.Path.GetDirectoryName(pathOfExecutable);

            string filename = "Getting-Started.html";
            string pathOfHelpFile = System.IO.Path.Combine(pathOfExecutableDirectory, "help", filename);
            if (System.IO.File.Exists(pathOfHelpFile))
            {
                System.Diagnostics.Process.Start(pathOfHelpFile);
            }
            else
            {
                throw new FileNotFoundException(pathOfHelpFile);
            }
        }

        /// <summary>
        /// Query the highest resolution supported by the current camera model.
        /// </summary>
        private void getHighestResolution()
        {
            foreach (VideoFormatDesc VFD in icImagingControl1.VideoFormatDescs)
            {
                //tbResults.Text += String.Format("Maximum Resolution : {0}\r\n",VFD.ToString());
                highestWidth = 1;
                highestHeight = 1;

                highestWidth = (icImagingControl1.Width / VFD.StepSize.Width) * VFD.StepSize.Width;
                highestHeight = (icImagingControl1.Height / VFD.StepSize.Height) * VFD.StepSize.Height;

                if (highestWidth > VFD.MaxSize.Width)
                    highestWidth = VFD.MaxSize.Width;

                if (highestHeight > VFD.MaxSize.Height)
                    highestHeight = VFD.MaxSize.Height;

                highestWidth = VFD.MaxSize.Width;
                highestHeight = VFD.MaxSize.Height;

            }
        }

        /// <summary>
        /// Gain trackbar event handler,  sets a new gain value in the camera.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbGain_ValueChanged(object sender, EventArgs e)
        {
            if(icImagingControl1.DeviceValid && _gain != null)
            {
                _gain.Value = tbGain.Value;
            }
        }

        /// <summary>
        /// On click on button "Save" the coordinates of the hotpixels are saved into an XML file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (icImagingControl1.DeviceValid)
            {
                var xmlSer = new XmlSerializer(
                                listOfCoordinates.GetType(),
                                new Type[] { typeof(int) }
                                );

                var pathOfExecutable = Application.ExecutablePath;
                string pathOfExecutableDirectory = System.IO.Path.GetDirectoryName(pathOfExecutable);
                String filename = String.Format("pixelfix-{0}.xml", icImagingControl1.DeviceCurrent.GetSerialNumber());
                TextWriter writer = new StreamWriter(System.IO.Path.Combine(pathOfExecutableDirectory, filename));
                xmlSer.Serialize(writer, listOfCoordinates);
                writer.Close();

                MessageBox.Show((String.Format("Created: pixelfix-{0}.xml at " + pathOfExecutableDirectory, icImagingControl1.DeviceCurrent.GetSerialNumber())),"Hot pixel list saved");
            }

        }

        /// <summary>
        /// The Threshold set the minimum brightness, which qualifies a pixel for a hot pixel. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbThreshold_ValueChanged(object sender, EventArgs e)
        {
            _Threshold = tbThreshold.Value;
            Console.WriteLine(_Threshold);
        }

        /// <summary>
        /// Event handler for displaying the live image in a Picturebox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">This parameter contains the bitmap to be displayed.</param>
        private void OnDisplay(object sender, BitmapEventArgs e)
        {
            if (!pbLiveDisplay.Disposing)
            {
                pbLiveDisplay.Image = e.Bitmap;
            }
        }
    }

    /// <summary>
    /// From EventArgs inherited class, that forwards a System.Drawing.Bitmap to 
    /// an event handler.
    /// </summary>
    public class BitmapEventArgs : EventArgs
    {
        
        public BitmapEventArgs(Bitmap b)
        {
            Bitmap = b;
        }

        public Bitmap Bitmap { get; private set; }
    }
}
