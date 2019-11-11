using LibPolarizationCamera;
using LibPolarizationCamera.PixelTypes;
using System;
using System.Collections;
using TIS.Imaging;

namespace DoLPSegmentation
{
    class DisplayFrameFilter : TIS.Imaging.FrameFilterImpl
    {
        public int DoLPThreshold { get; set; } = 100;
        public int IntensityThreshold { get; set; } = 32;

        public override void GetSupportedInputTypes(ArrayList frameTypes)
        {
            frameTypes.Add(new FrameType(ADI1.MediaSubtype));
            frameTypes.Add(new FrameType(ADC1.MediaSubtype));
        }

        public override bool GetTransformOutputTypes(FrameType inType, ArrayList outTypes)
        {
            outTypes.Add(new FrameType(MediaSubtypes.RGB32, inType.Width, inType.Height));

            return true;
        }

        private static IntPtr LineStart(IFrame frame, int y)
        {
            var frameType = frame.FrameType;
            var stride = frameType.Width * FrameTypeHelper.BitsPerPixel(frameType) / 8;

            if (FrameTypeHelper.IsBottomUpFrameType(frameType))
            {
                return frame.GetIntPtr() + (frameType.Height - y - 1) * stride;
            }
            else
            {
                return frame.GetIntPtr() + y * stride;
            }
        }

        private static unsafe void ThresholdADI1(IFrame srcADI1, IFrame destRGB32, int dolpThreshold, int intensityThreshold)
        {
            int width = srcADI1.FrameType.Width;
            int height = srcADI1.FrameType.Height;

            for (int y = 0; y < height; ++y)
            {
                var pSrcLine = (ADI1*)LineStart(srcADI1, y);
                var pDestLine = (RGB32*)LineStart(destRGB32, y);

                for (int x = 0; x < width; ++x)
                {
                    if (pSrcLine[x].DoLP > dolpThreshold && pSrcLine[x].Intensity > intensityThreshold)
                    {
                        pDestLine[x].Blue = 0x00;
                        pDestLine[x].Green = 0x00;
                        pDestLine[x].Red = 0xFF;
                        pDestLine[x].Reserved = 0xFF;
                    }
                    else
                    {
                        pDestLine[x].Blue = pSrcLine[x].Intensity;
                        pDestLine[x].Green = pSrcLine[x].Intensity;
                        pDestLine[x].Red = pSrcLine[x].Intensity;
                        pDestLine[x].Reserved = 0xFF;
                    }
                }
            }
        }

        private static unsafe void ThresholdADC1(IFrame srcADC1, IFrame destRGB32, int dolpThreshold, int intensityThreshold)
        {
            int width = srcADC1.FrameType.Width;
            int height = srcADC1.FrameType.Height;

            for (int y = 0; y < height; ++y)
            {
                var pSrcLine = (ADC1*)LineStart(srcADC1, y);
                var pDestLine = (RGB32*)LineStart(destRGB32, y);

                for (int x = 0; x < width; ++x)
                {
                    int avgDolp = (pSrcLine[x].DoLPRed + pSrcLine[x].DoLPGreen + pSrcLine[x].DoLPBlue) / 3;
                    int avgIntensity = (pSrcLine[x].IntensityRed + pSrcLine[x].IntensityGreen + pSrcLine[x].IntensityBlue) / 3;

                    if (avgDolp > dolpThreshold && avgIntensity > intensityThreshold)
                    {
                        pDestLine[x].Blue = 0x00;
                        pDestLine[x].Green = 0x00;
                        pDestLine[x].Red = 0xFF;
                        pDestLine[x].Reserved = 0xFF;
                    }
                    else
                    {
                        pDestLine[x].Blue = pSrcLine[x].IntensityBlue;
                        pDestLine[x].Green = pSrcLine[x].IntensityGreen;
                        pDestLine[x].Red = pSrcLine[x].IntensityRed;
                        pDestLine[x].Reserved = 0xFF;
                    }
                }
            }
        }

        public override bool Transform(IFrame src, IFrame dest)
        {
            if (dest == null)
                return false;

            if (src.FrameType.Subtype == ADI1.MediaSubtype)
            {
                ThresholdADI1(src, dest, DoLPThreshold, IntensityThreshold);
                return true;
            }
            else if (src.FrameType.Subtype == ADC1.MediaSubtype)
            {
                ThresholdADC1(src, dest, DoLPThreshold, IntensityThreshold);
                return true;
            }

            return false;
        }
    }
}
