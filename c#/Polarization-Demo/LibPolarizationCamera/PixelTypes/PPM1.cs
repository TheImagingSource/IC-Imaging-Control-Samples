using System;

namespace LibPolarizationCamera.PixelTypes
{
#pragma warning disable CS0649  // Field '' is never assigned to, and will always have its default value 0
    public struct PPM1
    {
        public byte P0;
        public byte P45;
        public byte P90;
        public byte P135;

        public static Guid MediaSubtype = MediaSubtypes.PPM1;
    }
#pragma warning restore CS0649
}
