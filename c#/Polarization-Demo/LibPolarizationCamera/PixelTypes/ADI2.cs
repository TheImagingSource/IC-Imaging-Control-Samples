using System;

namespace LibPolarizationCamera.PixelTypes
{
#pragma warning disable CS0649  // Field '' is never assigned to, and will always have its default value 0
    public struct ADI2
    {
        public ushort AoLP;
        public ushort DoLP;
        public ushort Intensity;
        public ushort Reserved;

        public static Guid MediaSubtype = MediaSubtypes.ADI2;
    }
#pragma warning restore CS0649
}
