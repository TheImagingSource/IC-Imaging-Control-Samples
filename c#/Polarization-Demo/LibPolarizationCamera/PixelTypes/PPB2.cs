using System;

namespace LibPolarizationCamera.PixelTypes
{
#pragma warning disable CS0649  // Field '' is never assigned to, and will always have its default value 0
    public struct PPB2
    {
        public ushort P0;
        public ushort P45;
        public ushort P90;
        public ushort P135;

        public static Guid MediaSubtype = MediaSubtypes.PPB2;
    }
#pragma warning restore CS0649
}
