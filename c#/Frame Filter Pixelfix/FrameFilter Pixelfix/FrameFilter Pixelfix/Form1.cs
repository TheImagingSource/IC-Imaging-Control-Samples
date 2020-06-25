using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TIS.Imaging;

namespace FrameFilter_Pixelfix
{
    public partial class Form1 : Form
    {
        // This attribute controls whether the live display is scaled in order
        // to match the size of the IC Imaging Control window on the form.
        private bool m_bFitImageToWindow = true;
        private List<System.Drawing.Point> listOfCoordinates = new List<System.Drawing.Point>();
        bool _applyPixelFix = false;
        private TIS.Imaging.PixelFixFilter MyPixelFixFilter;



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Try to load the previously used device. 
            try
            {
                icImagingControl1.LoadDeviceStateFromFile("device.xml", true);

                Console.WriteLine(icImagingControl1.VideoFormatCurrent.FrameType.FourCC);
                icImagingControl1.LiveDisplay = true;
            }
            catch
            {
                // Either the xml file does not exist or the device
                // could not be loaded. In both cases we do nothing and proceed.
            }

            // Create the FrameFilter 'PixelFixFilter'
            MyPixelFixFilter = new PixelFixFilter(icImagingControl1);

            // Adding the FrameFilter to our IC Imaging Control
            icImagingControl1.DeviceFrameFilters.Add(icImagingControl1.FrameFilterCreate(MyPixelFixFilter));

            MyPixelFixFilter.setEnable(_applyPixelFix);

            if (m_bFitImageToWindow)
            {
                // Adjust live display size to the control size.
                icImagingControl1.LiveDisplayDefault = false;
                icImagingControl1.LiveDisplaySize = icImagingControl1.Size;
            }
            if (icImagingControl1.DeviceValid)
            {

            }


            UpdateControls();
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
        }

        /// <summary>
        /// Show the device's property dialog for exposure, brightness etc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdProperties_Click(object sender, EventArgs e)
        {
            ShowDeviceProperties();
        }

        /// <summary>
        /// Start/stop the live video and update the state of the start/stop button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLive_Click(object sender, EventArgs e)
        {
            if (icImagingControl1.LiveVideoRunning)
            {
                StopLiveVideo();
            }
            else
            {
                StartLiveVideo();
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Start the live video and update the state of the start/stop button.
        /// </summary>
        private void StartLiveVideo()
        {
            icImagingControl1.LiveStart();
            UpdateControls();
        }

        /// <summary>
        /// Stop the live video and update the state of the start/stop button.
        /// </summary>
        private void StopLiveVideo()
        {
            icImagingControl1.LiveStop();
            UpdateControls();
        }

        private void UpdateControls()
        {
            cmdLive.Enabled = icImagingControl1.DeviceValid;
            cmdProperties.Enabled = icImagingControl1.DeviceValid;
            if (icImagingControl1.LiveVideoRunning)
            {
                cmdLive.Text = "Stop Live";
            }
            else
            {
                cmdLive.Text = "Start Live";
            }
        }

        /// <summary>
        /// Show the device selection dialog of IC Imaging Control.
        /// </summary>
        private void OpenNewVideoCaptureDevice()
        {
            if (icImagingControl1.DeviceValid)
            {
                StopLiveVideo();
            }
            else
            {
                icImagingControl1.Device = "";
            }
            icImagingControl1.ShowDeviceSettingsDialog();
            cmdLive.Enabled = icImagingControl1.DeviceValid;
            cmdProperties.Enabled = icImagingControl1.DeviceValid;
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
            UpdateControls();
        }

        private void icImagingControl1_DeviceLost(object sender, TIS.Imaging.ICImagingControl.DeviceLostEventArgs e)
        {
            // We BeginInvoke this so that showing the modal dialog does not block actually closing the device in IC
            BeginInvoke(new Action(() => DeviceLost()));
        }

        /// <summary>
        /// Eventhandler to Enable the Pixelfix FrameFilter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chBox_ApplyPixelFix_CheckedChanged(object sender, EventArgs e)
        {
            _applyPixelFix = chBox_ApplyPixelFix.Checked;
            MyPixelFixFilter.setEnable(_applyPixelFix);
        }
    }
}
