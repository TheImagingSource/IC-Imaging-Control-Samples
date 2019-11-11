using System;

namespace LibPolarizationCamera.PixelTypes
{
#pragma warning disable CS0649  // Field '' is never assigned to, and will always have its default value 0
    public struct ADC2
    {
        public ushort AoLP;
        public ushort DoLPRed;
        public ushort DoLPGreen;
        public ushort DoLPBlue;
        public ushort IntensityRed;
        public ushort IntensityGreen;
        public ushort IntensityBlue;
        public ushort Reserved;

        public static Guid MediaSubtype = MediaSubtypes.ADC2;
    }
#pragma warning restore CS0649
}
