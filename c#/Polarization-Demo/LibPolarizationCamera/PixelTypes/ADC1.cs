using System;

namespace LibPolarizationCamera.PixelTypes
{
#pragma warning disable CS0649  // Field '' is never assigned to, and will always have its default value 0
    public struct ADC1
    {
        public byte AoLP;
        public byte DoLPRed;
        public byte DoLPGreen;
        public byte DoLPBlue;
        public byte IntensityRed;
        public byte IntensityGreen;
        public byte IntensityBlue;
        public byte Reserved;

        public static Guid MediaSubtype = MediaSubtypes.ADC1;
    }
#pragma warning restore CS0649
}
