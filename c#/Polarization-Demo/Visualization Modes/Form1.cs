using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Visualization_Modes
{
    public partial class Form1 : Form
    {
        private TIS.Imaging.VCDMapStringsProperty visualizationMode;

        public Form1()
        {
            InitializeComponent();
        }

        private static TIS.Imaging.Device FindPolarizationCameraByName(TIS.Imaging.Device[] devices)
        {
            // We simply select the first polarization camera
            foreach (var dev in devices)
            {
                // Monochrome polarization cameras are DZK 33UX250 and DZK 33GX250
                if (dev.Name.StartsWith("DZK"))
                    return dev;
                // Color polarization cameras are DYK 33UX250 and DYK 33GX250
                if (dev.Name.StartsWith("DYK"))
                    return dev;
            }

            return null;
        }

        private static TIS.Imaging.VideoFormat FindLargestVideoFormat(TIS.Imaging.VideoFormat[] formats, params Guid[] subtypes)
        {
            // Select the largest video format that matches one of the supplied media subtypes
            return formats
                .Where(fmt => subtypes.Contains(fmt.FrameType.Subtype))
                .OrderByDescending(fmt => fmt.Width * fmt.Height)
                .First();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Select polarization (DZK or DYK) camera
            icImagingControl1.Device = FindPolarizationCameraByName(icImagingControl1.Devices);

            // Select RGB32 format, set maximum frame rate
            icImagingControl1.VideoFormat = FindLargestVideoFormat(icImagingControl1.VideoFormats, TIS.Imaging.MediaSubtypes.RGB32);
            icImagingControl1.DeviceFrameRate = icImagingControl1.DeviceFrameRates.Max();

            // Query interface to visualization mode selector property
            visualizationMode = icImagingControl1.VCDPropertyItems.Find<TIS.Imaging.VCDMapStringsProperty>(
                LibPolarizationCamera.VCDGUIDs.VCDID_PolarizationVisualizationMode,
                TIS.Imaging.VCDGUIDs.VCDElement_Value);

            // Initialize combo box with available visualization modes
            cboVisualizationMode.DataSource = visualizationMode.Strings;

            // Start live video
            icImagingControl1.LiveStart();
        }

        private void cboVisualizationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            visualizationMode.String = cboVisualizationMode.Text;
        }
    }
}
