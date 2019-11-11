using System;

namespace LibPolarizationCamera.PixelTypes
{
#pragma warning disable CS0649  // Field '' is never assigned to, and will always have its default value 0
    public struct RGB32
    {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Reserved;

        public static Guid MediaSubtype = TIS.Imaging.MediaSubtypes.RGB32;
    }
#pragma warning restore CS0649
}
