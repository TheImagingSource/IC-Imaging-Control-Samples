using System;
using System.Runtime.InteropServices;
using System.Net;

namespace IPConfigAPI
{
    class IPConfigAPI
    {
        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct CameraInfo
        {
            public UInt64 Mac;
            public fixed byte ManufacturerName[64];
            public fixed byte ModelName[64];
            public fixed byte Serialnumber[64];
            public fixed byte UserDefinedName[64];
            public fixed byte ManufacturerSpecificInformation[64];

            public fixed byte AdapterName[64];
            public fixed byte FirmwareVersion[64];

            public UInt32 IPAddress;
            public UInt32 Subnetmask;
            public UInt32 Gateway;
        }


        [DllImport(".\\ipconfig_api_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IPConfig_Init")]
        private static extern int IPConfig_Init64();
        [DllImport(".\\ipconfig_api_win32.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IPConfig_Init")]
        private static extern int IPConfig_Init32();

        [DllImport(".\\ipconfig_api_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IPConfig_Exit")]
        private static extern void IPConfig_Exit64();
        [DllImport(".\\ipconfig_api_win32.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IPConfig_Exit")]
        private static extern void IPConfig_Exit32();

        [DllImport(".\\ipconfig_api_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IPConfig_GetDeviceList")]
        private unsafe static extern int IPConfig_GetDeviceList64(UInt32 count_buffer_entries, CameraInfo* buffer_list, ref UInt32 count_avaialble_entries);
        [DllImport(".\\ipconfig_api_win32.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IPConfig_GetDeviceList")]
        private unsafe static extern int IPConfig_GetDeviceList32(UInt32 count_buffer_entries, CameraInfo* buffer_list, ref UInt32 count_avaialble_entries );

        private UInt32 _CamerasCount = 0;
        CameraInfo[] _CI = null;

        public IPConfigAPI()
        {
            int a = 0;
            if (Marshal.SizeOf(typeof(IntPtr)) == 4)
                a =  IPConfig_Init32();
            else
                a = IPConfig_Init64();

            Console.WriteLine(a);
        }

        ~IPConfigAPI()
        {
            if (Marshal.SizeOf(typeof(IntPtr)) == 4)
                IPConfig_Exit32();
            else
                IPConfig_Exit64();
        }

        public UInt32 GetDeviceCount()
        {
            unsafe
            {
                CameraInfo[] CI1 = new CameraInfo[1];
                fixed (CameraInfo* x = &CI1[0])
                {
                    if (Marshal.SizeOf(typeof(IntPtr)) == 4)
                        IPConfig_GetDeviceList32(0, x, ref _CamerasCount);
                    else
                        IPConfig_GetDeviceList64(0, x, ref _CamerasCount);

                }
                if (_CamerasCount > 0)
                {
                    _CI = new CameraInfo[_CamerasCount];
                    fixed (CameraInfo* x = &_CI[0])
                    {
                        if (Marshal.SizeOf(typeof(IntPtr)) == 4)
                            IPConfig_GetDeviceList32(_CamerasCount, x, ref _CamerasCount);
                        else
                            IPConfig_GetDeviceList64(_CamerasCount, x, ref _CamerasCount);
                    }

                }
            }
            return _CamerasCount;
        }
        private unsafe string DecodeAsciiZ( byte* buffer)
        {
            return new string((sbyte*)buffer);
        }

        public string GetModelName(int Index )
        {
            string ret = "";
            if (Index < _CamerasCount)
            {
                CameraInfo a = _CI[Index];
                unsafe
                {
                    ret = DecodeAsciiZ(a.ModelName);
                }
            }
            return ret;
        }

        public string GetManufacturerName(int Index)
        {
            string ret = "";
            if (Index < _CamerasCount)
            {
                CameraInfo a = _CI[Index];
                unsafe
                {
                    ret = DecodeAsciiZ(a.ManufacturerName);
                }
            }
            return ret;
        }
        public string GetUserDefinedName(int Index)
        {
            string ret = "";
            if (Index < _CamerasCount)
            {
                CameraInfo a = _CI[Index];
                unsafe
                {
                    ret = DecodeAsciiZ(a.UserDefinedName);
                }
            }
            return ret;
        }

        public string GetAdapterName(int Index)
        {
            string ret = "";
            if (Index < _CamerasCount)
            {
                CameraInfo a = _CI[Index];
                unsafe
                {
                    ret = DecodeAsciiZ(a.AdapterName);
                }
            }
            return ret;
        }


        public string GetFirmwareversion(int Index)
        {
            string ret = "";
            if (Index < _CamerasCount)
            {
                CameraInfo a = _CI[Index];
                unsafe
                {
                    ret = DecodeAsciiZ(a.FirmwareVersion);
                }
            }
            return ret;
        }

        public string GetSerialnumber(int Index)
        {
            string ret = "";
            if (Index < _CamerasCount)
            {
                CameraInfo a = _CI[Index];
                unsafe
                {
                    ret = DecodeAsciiZ(a.Serialnumber);
                }
            }
            return ret;
        }

        public string GetFirmwareVersion(int Index)
        {
            string ret = "";
            if (Index < _CamerasCount)
            {
                CameraInfo a = _CI[Index];
                unsafe
                {
                    ret = DecodeAsciiZ(a.FirmwareVersion);
                }
            }
            return ret;
        }

        public UInt64 GetMac(int Index)
        {
            UInt64 ret = 0;
            if (Index < _CamerasCount)
            {
               ret = _CI[Index].Mac;
            }
            return ret;
        }


        public IPAddress GetIPAddress(int Index)
        {
            IPAddress ipaddress = new IPAddress(0);
            if (Index < _CamerasCount)
            {
                UInt32 address = _CI[Index].IPAddress;
                ipaddress = new IPAddress(new byte[] {
                    (byte)((address>>24) & 0xFF) ,
                    (byte)((address>>16) & 0xFF) ,
                    (byte)((address>>8)  & 0xFF) ,
                    (byte)( address & 0xFF)});
            }
            return ipaddress;
        }

        public IPAddress GetSubnetmask(int Index)
        {
            IPAddress ipaddress = new IPAddress(0);
            if (Index < _CamerasCount)
            {
                UInt32 address = _CI[Index].Subnetmask;
                ipaddress = new IPAddress(new byte[] {
                    (byte)((address>>24) & 0xFF) ,
                    (byte)((address>>16) & 0xFF) ,
                    (byte)((address>>8)  & 0xFF) ,
                    (byte)( address & 0xFF)});
            }
            return ipaddress;
        }

        public IPAddress GetGateway(int Index)
        {
            IPAddress ipaddress = new IPAddress(0);
            if (Index < _CamerasCount)
            {
                UInt32 address = _CI[Index].Gateway;
                ipaddress = new IPAddress(new byte[] {
                    (byte)((address>>24) & 0xFF) ,
                    (byte)((address>>16) & 0xFF) ,
                    (byte)((address>>8)  & 0xFF) ,
                    (byte)( address & 0xFF)});
            }
            return ipaddress;
        }

        public IPAddress FindIPAdressForSerialnumber(string Serial)
        {
            IPAddress ipaddress = new IPAddress(0);
            if (_CamerasCount == 0)
                GetDeviceCount();

            for ( int i = 0; i < _CamerasCount; i++)
            {
                if (GetSerialnumber(i) == Serial)
                    ipaddress = GetIPAddress(i);
            }

            return ipaddress;
        }

        public string FindSerialNumberforIPaddress(IPAddress ip)
        {
            string Serial = "";
            if (_CamerasCount == 0)
                GetDeviceCount();

            for (int i = 0; i < _CamerasCount; i++)
            {
                if (GetIPAddress(i).Equals(ip))
                    Serial  = GetSerialnumber(i);
            }

            return Serial;
        }
    }
}
