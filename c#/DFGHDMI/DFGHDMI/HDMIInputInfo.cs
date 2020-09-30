using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIS.Imaging;

namespace DFGHDMI
{
    class HDMIInputInfo
    {
        public bool IsSourceConnected { get; private set; }
        public bool IsSignalDetected { get; private set; }

        public int InputWidth { get; private set; }
        public int InputHeight { get; private set; }
        public float InputFPS { get; private set; }
        public int InputBitDepth { get; private set; }

        private static Guid VCDID_SourceConnected = new Guid("{81806615-7E14-4DC3-BDE4-4F04AE69AC98}");
        private static Guid VCDElement_InputWidth = new Guid("{170729B3-EAF2-4CD6-B537-D37626CBFDEC}");
        private static Guid VCDElement_InputHeight = new Guid("{D6A428F4-743D-451A-BB3E-D3CB67637249}");
        private static Guid VCDElement_InputFp1ks = new Guid("{A432D2CA-704D-40F8-AC32-F2A99551AB20}");
        private static Guid VCDElement_InputBitDepth = new Guid("{7A7A0B05-0EAE-44FA-A58E-AA498DBEBEE5}");

        public static HDMIInputInfo Query( ICImagingControl ic )
        {
            if (!ic.DeviceValid)
                throw new InvalidOperationException("No device opened");

            var sourceConnected = ic.VCDPropertyItems.Find<VCDSwitchProperty>(VCDID_SourceConnected, VCDGUIDs.VCDElement_Value);
            var signalDetected = ic.VCDPropertyItems.Find<VCDSwitchProperty>(VCDGUIDs.VCDID_SignalDetected, VCDGUIDs.VCDElement_Value);
            var inputWidth = ic.VCDPropertyItems.Find<VCDRangeProperty>(VCDGUIDs.VCDID_SignalDetected, VCDElement_InputWidth);
            var inputHeight = ic.VCDPropertyItems.Find<VCDRangeProperty>(VCDGUIDs.VCDID_SignalDetected, VCDElement_InputHeight);
            var inputFp1ks = ic.VCDPropertyItems.Find<VCDRangeProperty>(VCDGUIDs.VCDID_SignalDetected, VCDElement_InputFp1ks);
            var inputBitDepth = ic.VCDPropertyItems.Find<VCDRangeProperty>(VCDGUIDs.VCDID_SignalDetected, VCDElement_InputBitDepth);

            return new HDMIInputInfo
            {
                IsSourceConnected = sourceConnected.Switch,
                IsSignalDetected = signalDetected.Switch,
                InputWidth = inputWidth.Value,
                InputHeight = inputHeight.Value,
                InputFPS = inputFp1ks.Value / 1e3f,
                InputBitDepth = inputBitDepth.Value
            };
        }
    }
}
