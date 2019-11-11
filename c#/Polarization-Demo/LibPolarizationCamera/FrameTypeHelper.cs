using LibPolarizationCamera.PixelTypes;

namespace LibPolarizationCamera
{
    public static class FrameTypeHelper
    {
        public static int BitsPerPixel(TIS.Imaging.FrameType frameType)
        {
            // Older versions of IC Imaging Control don't know the ADI1/ADI2 image formats,
            // so the BitsPerPixel information is not available

            if (frameType.BitsPerPixel > 0)
            {
                return frameType.BitsPerPixel;
            }

            if (frameType.Subtype == ADI1.MediaSubtype)
                return System.Runtime.InteropServices.Marshal.SizeOf(typeof(ADI1)) * 8;
            if (frameType.Subtype == ADC1.MediaSubtype)
                return System.Runtime.InteropServices.Marshal.SizeOf(typeof(ADC1)) * 8;

            return 0;
        }

        public static bool IsBottomUpFrameType(TIS.Imaging.FrameType frameType)
        {
            // Older versions of IC Imaging Control don't know the ADI1/ADI2 image formats,
            // so the IsBottomUp information is not available

            if (frameType.Subtype == ADI1.MediaSubtype)
                return false;
            if (frameType.Subtype == ADC1.MediaSubtype)
                return false;

            return frameType.IsBottomUp;
        }
    }
}
