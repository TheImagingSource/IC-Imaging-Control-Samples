using System;

namespace LibPolarizationCamera.PixelTypes
{
#pragma warning disable CS0649  // Field '' is never assigned to, and will always have its default value 0
    public struct ADI1
    {
        public byte AoLP;
        public byte DoLP;
        public byte Intensity;
        public byte Reserved;

        public static Guid MediaSubtype = MediaSubtypes.ADI1;
    }
#pragma warning restore CS0649
}
