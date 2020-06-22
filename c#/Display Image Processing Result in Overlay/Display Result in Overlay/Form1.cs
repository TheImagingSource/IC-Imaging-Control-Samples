using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TIS.Imaging;
namespace Display_Result_in_Overlay
{
    public partial class Form1 : Form
    {
        // This attribute controls whether the live display is scaled in order
        // to match the size of the IC Imaging Control window on the form.
        private bool m_bFitImageToWindow = true;
        
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
            }
            catch
            {
                // Either the xml file does not exist or the device
                // could not be loaded. In both cases we do nothing and proceed.
            }

            if (m_bFitImageToWindow)
            {
                // Adjust live display size to the control size.
                icImagingControl1.LiveDisplayDefault = false;
                icImagingControl1.LiveDisplaySize = icImagingControl1.Size;
            }

            // Setup up sink for continuous based image processing
            // The pixel format in the image buffers will be 32bit.
            icImagingControl1.Sink = new TIS.Imaging.FrameQueueSink((img) => ProcessImage(img), MediaSubtypes.RGB32, 5); ;
            icImagingControl1.OverlayBitmapPosition = TIS.Imaging.PathPositions.Display;

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
    }
}
