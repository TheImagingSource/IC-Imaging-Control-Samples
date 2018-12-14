using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace CaptureImageSequence1
{
    public partial class Form1 : Form
    {
        // The variable "StartImage" is used to save the index of the oldest image in the image ring buffer.
        int StartImage;

        // The variable "TimerCurrentImage" is used for the index of the currently displayed image,
        // due auto repeat of the image sequence is running.
        int TimerCurrentImage;

        // "Images" will contain a copy of the image ring buffer, in order to get faster acces to the images. 
        TIS.Imaging.ImageBuffer[] Images;

        // The variable "FrameCount" is used to count the capture frames. If "FrameCount"
        // is greater than the image ring buffer size, the "btnCapture" will be enabled and
        // the user knows the image sequence in the ring buffer is now filled up completely.
        // FrameCount will be set to 0 in btnLiveVideo_Click after the live video has been started.
        // It is incremented in the ImageAvailable event handler.
        int FrameCount = 0;

        // "Seconds" determins the length of the image ring buffer in seconds. If the image sequence should 
        // contain more or less seconds, only the value of "Seconds" need to be changed here.
        int Seconds = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            icImagingControl1.LiveCaptureLastImage = false;
            // In order to capture all delivered frames the property "LiveCaptureContinuous" must be set 
            // to true.
            icImagingControl1.LiveCaptureContinuous = true;
            timer1.Enabled = false;

            // Since no video capture device has been selected now, all 
            // buttons except the "Device" button are disabled.
            sldImages.Enabled = false;
            btnAutoRepeat.Enabled = false;
            btnCapture.Enabled = false;
            btnLiveVideo.Enabled = false;
            btnSettings.Enabled = false;


            // Resize the live display to the size of IC Imaging Control in order to display
            // the complete live video.
            icImagingControl1.LiveDisplayDefault = false;
            icImagingControl1.LiveDisplayHeight = icImagingControl1.Height;
            icImagingControl1.LiveDisplayWidth = icImagingControl1.Width;
        }


        /// <summary>
        /// btnDevice_Click()
        /// 
        /// Show device selection dialog in order to select a video capture device.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDevice_Click(object sender, EventArgs e)
        {
            // Make sure no live video is running before the device selection dialog is shown.
            if (icImagingControl1.LiveVideoRunning)
            {
                icImagingControl1.LiveStop();
                btnLiveVideo.Text = "Start Live";
            }

            // Disable the "capture related" buttons
            btnLiveVideo.Enabled = false;
            btnSettings.Enabled = false;
            btnCapture.Enabled = false;

            icImagingControl1.ShowDeviceSettingsDialog();

            if (icImagingControl1.DeviceValid)
            {
                // The last seconds before the capture button has been
                // clicked should be saved in the image ring buffer. Thus
                // the currently set frame rate multiplied by the value of "Seconds".
                icImagingControl1.ImageRingBufferSize = Convert.ToInt32(icImagingControl1.DeviceFrameRate 
                                                                        * Seconds);
                // Now the range of the slider is set to the image buffers size:
                sldImages.Maximum = icImagingControl1.ImageRingBufferSize - 1;
                sldImages.Minimum = 0;

                // Show the number of images in the ring buffer.
                lblSliderEnd.Text = icImagingControl1.ImageRingBufferSize.ToString();

                btnLiveVideo.Enabled = true;
                btnSettings.Enabled = true;
            }
        }


        /// <summary>
        /// btnSetting_Click
        /// 
        /// Show the device settings dialog in order to adjust the video capture device's 
        /// properties. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            // Show device settings dialog.
            if (icImagingControl1.DeviceValid)
            {
                icImagingControl1.ShowPropertyDialog();
            }
        }

        /// <summary>
        /// btnLiveVideo_Click
        /// 
        /// Start and stop the live video stream.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLiveVideo_Click(object sender, EventArgs e)
        {
            // Start Live video.
            if (icImagingControl1.DeviceValid)
            {
                if (icImagingControl1.LiveVideoRunning)
                {
                    icImagingControl1.LiveStop();
                    btnLiveVideo.Text = "Start Live";
                    btnCapture.Enabled = false; ;
                }
                else
                {
                    icImagingControl1.LiveDisplay = true; //show live video
                    timer1.Enabled = false;
                    sldImages.Enabled = false;
                    btnAutoRepeat.Enabled = false;
                    icImagingControl1.LiveStart();
                    btnLiveVideo.Text = "Stop Live";
                    FrameCount = 0;                     
                    btnCapture.Enabled = false;         // Disable the capture button. It will be enabled
                                                        // after a enough frames have been captured for
                                                        // a complete image sequence. This is indicated
                                                        // by the value of "FrameCount".
                }
            }
        }



        /// <summary>
        /// btnCapture_Click
        /// 
        /// This is the event handler for the "Capture" button. If the "Capture" button
        /// has been clicked, the currently running live video is stopped. Then the oldest image
        /// is searched in the image ring buffer of IC Imaging Control. The oldest image
        /// is the first image to be displayed when the slider's position is set to 0. The
        /// index in the ring buffer of the oldest image is saved in the form global variable
        /// "StartImage".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (icImagingControl1.DeviceValid)
            {
                // Disable the "Capture" button after it has been clicked.
                btnCapture.Enabled = false;
                // If the image sequence should contain images captures after the "Capture" 
                // button has been clicked, a call to "Sleep()" should be inserted before
                // "LiveStop()" is called, e.g.:
                //  System.Threading.Thread.Sleep(1000);
                // This would the application continue capture one second after the "Capture"
                // button has been clicked. 

                // Now stop the live video stream.
                icImagingControl1.LiveStop();
                btnLiveVideo.Text = "Start Live";
                // Copy the reference of the ImageBuffers to get a faster access to the images.
                Images = icImagingControl1.ImageBuffers;
                // The following line is necessary to enable the display of the image buffers
                // in the ring buffer.
                icImagingControl1.LiveDisplay = false;

                // Find the oldest image in the ring buffer. Its index is saved in
                // the variable "StartImage".
                double MinStartTime;

                StartImage = 0;
                MinStartTime = icImagingControl1.ImageBuffers[StartImage].SampleStartTime;

                for (int i = 1; i < (icImagingControl1.ImageRingBufferSize - 1); i++)
                {
                    if (Images[i].SampleStartTime < MinStartTime)
                    {
                        StartImage = i;
                        MinStartTime = Images[StartImage].SampleStartTime;
                        break;
                    }
                }

                sldImages.Enabled = true;
                btnAutoRepeat.Enabled = true;
            }
        }


        /// <summary>
        /// sldImages_Scroll
        /// 
        /// The slider's position determines the image in the ring buffer to be
        /// displayed. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sldImages_Scroll(object sender, EventArgs e)
        {
            DisplayTheImage(sldImages.Value);
        }



        /// <summary>
        /// btnAutoRepeat_Click
        /// 
        /// Enables the automatic play of the captured sequence by using a timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoRepeat_Click(object sender, EventArgs e)
        {
            if (!icImagingControl1.LiveVideoRunning)
            {
                if (timer1.Enabled)
                {
                    timer1.Enabled = false;
                }
                else
                {
                    timer1.Interval = 10;
                    timer1.Enabled = true;
                    TimerCurrentImage = 0;
                }
            }
        }

        /// <summary>
        /// timer1_Tick
        /// 
        /// If the automatically replay of the image sequence is in abled, the 
        /// images are iterated in this timer event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            DisplayTheImage(TimerCurrentImage);

            TimerCurrentImage = TimerCurrentImage + 1;
            if (TimerCurrentImage >= icImagingControl1.ImageRingBufferSize)
            {
                TimerCurrentImage = 0;
            }

            sldImages.Value = TimerCurrentImage;

        }


        /// <summary>
        /// DisplayTheImage
        /// 
        /// "DisplayTheImage()" displayed the image in the ring buffer specified by
        /// the parameter "index". The specified "index" will be mapped to the sequence
        /// of images in the ring buffer. This means "index" is added to "StartIndex". If
        /// "Index" plus "StartIndex" is greater than the ImageRingBufferSize, then the
        /// new index in the ring buffer must start at 0. 
        /// </summary>
        /// <param name="Index">The index of the image to be displayed.</param>
        private void DisplayTheImage(int Index)
        {
            int i;
            if (!icImagingControl1.LiveVideoRunning)
            {
                i = StartImage + Index;
                // Handling a possible overflow, in case the i is greater then the ring buffer size.
                if (i >= icImagingControl1.ImageRingBufferSize)
                {
                    i = i - icImagingControl1.ImageRingBufferSize;
                }
                icImagingControl1.DisplayImageBuffer(Images[i]);
            }
        }


        /// <summary>
        /// EnableCaptureDelegate
        /// 
        /// This function is called from within "icImagingControl1_ImageAvailable()" as delegate.
        /// It is used for calling the private method "EnableCaptureButton()" that only enables the
        /// "Capture" button.
        /// </summary>
        public delegate void EnableCaptureDelegate();
        /// <summary>
        /// EnableCaptureButton
        /// 
        /// This method is called by the "EnableCaptureDelegate()" from "icImagingControl1_ImageAvailable()"
        /// event handler after a sufficient number of frames have been captured into the image ring 
        /// buffer. It only enables the "Capture" button to inform the user that enough images for a new
        /// image sequence have been captured.
        /// </summary>
        private void EnableCaptureButton()
        {
            btnCapture.Enabled = true;
        }
        /// <summary>
        /// icImagingControl1_ImageAvailable
        /// 
        /// This is the ImageAvailable event handler. The number of captured frames is counted by this method. 
        /// If the number of frames is higher than the ring buffer's size, the delegate "EnableCaptureDelegate"
        ///  is called in order to enable the "Capture" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icImagingControl1_ImageAvailable(object sender, TIS.Imaging.ICImagingControl.ImageAvailableEventArgs e)
        {
            if (btnCapture.Enabled == false)
            {
                if (FrameCount > icImagingControl1.ImageRingBufferSize)
                {
                    // Now it is time to enable the capture button. 
                    BeginInvoke(new EnableCaptureDelegate(EnableCaptureButton));
                }
                else
                {
                    FrameCount++;
                }
            }
        }
    }
}
