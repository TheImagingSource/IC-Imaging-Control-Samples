using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIS.Imaging;

namespace DFGHDMI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            icImagingControl1.Device = "DFG/HDMI";

            var info = HDMIInputInfo.Query(icImagingControl1);
            UpdateHDMIInputUI(info);
            ConfigureVideoFormatFrameRate(info);

            icImagingControl1.BackColor = Color.FromArgb(32, 32, 32);
        }

        private void UpdateHDMIInputUI(HDMIInputInfo info)
        {
            chkSourceConnected.Checked = info.IsSourceConnected;
            chkSignalDetected.Checked = info.IsSignalDetected;
            if (info.IsSignalDetected)
            {
                txtInputWidth.Text = info.InputWidth.ToString();
                txtInputHeight.Text = info.InputHeight.ToString();
                txtInputFPS.Text = info.InputFPS.ToString("0.00");
                txtInputBits.Text = info.InputBitDepth.ToString();
            }
            else
            {
                txtInputWidth.Text = "";
                txtInputHeight.Text = "";
                txtInputFPS.Text = "";
                txtInputBits.Text = "";
            }
        }

        private void btnUpdateHDMIInput_Click(object sender, EventArgs e)
        {
            var info = HDMIInputInfo.Query(icImagingControl1);
            UpdateHDMIInputUI(info);
        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            var info = HDMIInputInfo.Query(icImagingControl1);
            ConfigureVideoFormatFrameRate(info);
        }

        private void ConfigureVideoFormatFrameRate(HDMIInputInfo info)
        {
            icImagingControl1.LiveStop();            

            if (info.InputWidth != 0 && info.InputHeight != 0 && info.InputFPS != 0)
            {
                // Calculate the bandwidth required when configuring the DFG/HDMI to send its input
                double bandwidthRGB32 = 8 * info.InputWidth * info.InputHeight * info.InputFPS;
                double bandwidthY411 = 12 * info.InputWidth * info.InputHeight * info.InputFPS;
                double bandwidthYUY2 = 16 * info.InputWidth * info.InputHeight * info.InputFPS;
                double bandwidthRGB24 = 24 * info.InputWidth * info.InputHeight * info.InputFPS;

                // Maximum allowed bandwidth is ~390 MB/s
                const double BANDWIDTH_LIMIT = 390e6 * 8;

                // Select highest-quality video format that fits into the bandwidth limit
                if (bandwidthRGB24 < BANDWIDTH_LIMIT)
                {
                    icImagingControl1.VideoFormat = string.Format("RGB24 ({0}x{1})", info.InputWidth, info.InputHeight);
                }
                else if (bandwidthYUY2 < BANDWIDTH_LIMIT)
                {
                    icImagingControl1.VideoFormat = string.Format("YUY2 ({0}x{1})", info.InputWidth, info.InputHeight);
                }
                else if (bandwidthY411 < BANDWIDTH_LIMIT)
                {
                    icImagingControl1.VideoFormat = string.Format("Y411 ({0}x{1})", info.InputWidth, info.InputHeight);
                }
                else
                {
                    icImagingControl1.VideoFormat = string.Format("RGB32 ({0}x{1})", info.InputWidth, info.InputHeight);
                }

                // Configure frame rate to match HDMI source
                icImagingControl1.DeviceFrameRate = info.InputFPS;

                txtVideoFormat.Text = icImagingControl1.VideoFormat.ToString();
                txtFrameRate.Text = icImagingControl1.DeviceFrameRate.ToString("0.00");

                icImagingControl1.LiveStart();
            }
            else
            {
                txtVideoFormat.Text = "<Input not detected>";
                txtFrameRate.Text = "<Input not detected>";
            }
        }

        private void chkFitToWindow_CheckedChanged(object sender, EventArgs e)
        {
            FitVideoToWindow();
        }

        private void FitVideoToWindow()
        {
            if (chkFitToWindow.Checked)
            {
                var videoRatio = icImagingControl1.VideoFormatCurrent.Width / (float)icImagingControl1.VideoFormatCurrent.Height;
                var windowRatio = icImagingControl1.Width / (float)icImagingControl1.Height;

                icImagingControl1.LiveDisplayDefault = false;
                if (videoRatio > windowRatio)
                {
                    icImagingControl1.LiveDisplayWidth = icImagingControl1.Width; 
                    icImagingControl1.LiveDisplayHeight = (int)(icImagingControl1.Width / videoRatio);
                }
                else
                {
                    icImagingControl1.LiveDisplayWidth = (int)(icImagingControl1.Height * videoRatio);
                    icImagingControl1.LiveDisplayHeight = icImagingControl1.Height; 
                }
            }
            else
            {
                icImagingControl1.LiveDisplayDefault = true;
            }
        }

        private void icImagingControl1_Resize(object sender, EventArgs e)
        {
            FitVideoToWindow();
        }
    }
}
