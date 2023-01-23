using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TIS.Imaging;

namespace Image_And_AVI_Capture
{
    public partial class Form1 : Form
    {
        // This attribute controls whether the live display is scaled in order
        // to match the size of the IC Imaging Control window on the form.
        private bool m_bFitImageToWindow = true;
        private FrameFilter _saveImageFrameFilter;
        private int imageCounter = 0;
        private MediaStreamSink _aviSink;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _aviSink = new MediaStreamSink();
            foreach (AviCompressor codec in AviCompressor.AviCompressors)
            {
                comboBox1.Items.Add(codec);
                if (codec.Name == "DV Video Encoder")
                {
                    _aviSink.Codec = codec;
                    comboBox1.SelectedIndex = comboBox1.FindStringExact("DV Video Encoder");

                }
            }

            _saveImageFrameFilter = FrameFilter.Create("Save Image");
            if (_saveImageFrameFilter == null)
            {
                MessageBox.Show($"Failed to load the \"Save Image\" frame filter!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            icImagingControl1.DeviceFrameFilters.Add(_saveImageFrameFilter);

            // Try to load the previously used device. 
            try
            {
                icImagingControl1.LoadDeviceStateFromFile("device.xml", true);
                _aviSink.Filename = $"{icImagingControl1.Device.ToString()}.avi";
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
            foreach(AviCompressor codec in AviCompressor.AviCompressors)
            {
                if (codec.ToString() == comboBox1.SelectedItem.ToString())
                {
                    _aviSink.Codec = codec;
                }
            }
                icImagingControl1.Sink = _aviSink;
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
            buttonAVIProperties.Enabled = !icImagingControl1.LiveVideoRunning;
            comboBox1.Enabled = !icImagingControl1.LiveVideoRunning;
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
                _aviSink.Filename = $"{icImagingControl1.Device.ToString()}.avi";
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

        private void buttonSnap_Click(object sender, EventArgs e)
        {
            if(_saveImageFrameFilter != null && icImagingControl1.DeviceValid && icImagingControl1.LiveVideoRunning)
            {
                imageCounter++;
                string imageFileName = String.Format("Image{0}.jpg",imageCounter);
                _saveImageFrameFilter.SetStringParameter("ImageName", imageFileName);
            }
        }

        private void checkBoxAVIPause_CheckedChanged(object sender, EventArgs e)
        {
            icImagingControl1.Sink.SinkModeRunning = !icImagingControl1.Sink.SinkModeRunning;
        }

        private void buttonAVIProperties_Click(object sender, EventArgs e)
        {
            try
            {
                _aviSink.Codec.ShowPropertyPage();
            } catch //catch nothing. Some Codecs seem to break the program.
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (AviCompressor codec in AviCompressor.AviCompressors)
            {
                if (codec.Name == comboBox1.SelectedItem.ToString())
                {
                    _aviSink.Codec = codec;
                }
            }
        }
    }
}
