using LibPolarizationCamera;
using LibPolarizationCamera.PixelTypes;
using System;
using System.Collections;
using TIS.Imaging;

namespace PolarizationPlanes
{
    class DisplayFrameFilter : TIS.Imaging.FrameFilterImpl
    {
        public enum DisplayMode
        {
            P0,
            P45,
            P90,
            P135
        }

        public DisplayMode OutputMode { get; set; }

        public override void GetSupportedInputTypes(ArrayList frameTypes)
        {
            frameTypes.Add(new FrameType(PPM1.MediaSubtype));
            frameTypes.Add(new FrameType(PPB1.MediaSubtype));
        }

        public override bool GetTransformOutputTypes(FrameType inType, ArrayList outTypes)
        {
            outTypes.Add(new FrameType(MediaSubtypes.Y800, inType.Width, inType.Height));

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

        private static unsafe void ExtractPolarizationPlanePPM1(IFrame srcPPM1, IFrame destY800, DisplayMode mode)
        {
            int width = srcPPM1.FrameType.Width;
            int height = srcPPM1.FrameType.Height;

            for (int y = 0; y < height; ++y)
            {
                var pSrcLine = (PPM1*)LineStart(srcPPM1, y);
                var pDestLine = (byte*)LineStart(destY800, y);

                for (int x = 0; x < width; ++x)
                {
                    switch( mode )
                    {
                        case DisplayMode.P0:
                            pDestLine[x] = pSrcLine[x].P0;
                            break;
                        case DisplayMode.P45:
                            pDestLine[x] = pSrcLine[x].P45;
                            break;
                        case DisplayMode.P90:
                            pDestLine[x] = pSrcLine[x].P90;
                            break;
                        case DisplayMode.P135:
                            pDestLine[x] = pSrcLine[x].P135;
                            break;
                    }
                }
            }
        }

        private static unsafe void ExtractPolarizationPlanePPB1(IFrame srcPPB1, IFrame destY800, DisplayMode mode)
        {
            int width = srcPPB1.FrameType.Width;
            int height = srcPPB1.FrameType.Height;

            for (int y = 0; y < height; ++y)
            {
                var pSrcLine = (PPM1*)LineStart(srcPPB1, y);
                var pDestLine = (byte*)LineStart(destY800, y);

                for (int x = 0; x < width; ++x)
                {
                    switch (mode)
                    {
                        case DisplayMode.P0:
                            pDestLine[x] = pSrcLine[x].P0;
                            break;
                        case DisplayMode.P45:
                            pDestLine[x] = pSrcLine[x].P45;
                            break;
                        case DisplayMode.P90:
                            pDestLine[x] = pSrcLine[x].P90;
                            break;
                        case DisplayMode.P135:
                            pDestLine[x] = pSrcLine[x].P135;
                            break;
                    }
                }
            }
        }

        public override bool Transform(IFrame src, IFrame dest)
        {
            if (dest == null)
                return false;

            if (src.FrameType.Subtype == PPM1.MediaSubtype)
            {
                ExtractPolarizationPlanePPM1(src, dest, OutputMode);
                return true;
            }
            else if (src.FrameType.Subtype == PPB1.MediaSubtype)
            {
                ExtractPolarizationPlanePPB1(src, dest, OutputMode);
                return true;
            }

            return false;
        }
    }
}
