# Open The Imaging Source GigE Cameras by IP Address
The Gige Camera driver from [TIS GigEDriver](https://www.theimagingsource.com/support/downloads-for-windows/device-drivers/icwdmgigetis/) comes with an API for camera and IP configuration. The API can be installed optionally.
It conists of two DLLs:
 * ipconfig_api_x64.dll
 * ipconfig_api_win32.dll

These DLLs can be imported into a C# program. Using the API, a program can query camera name, serial number, IP adress, firmware version and so on.
The class ```IPConfigAPI``` in IPConfigAPI.cs implements that. The DLLs are unmanaged, therefore, the project settings must allow ```unsafe code```. It is used as follows:
``` C#
UInt32 CamerasFound;
IPConfigAPI.IPConfigAPI ipconfigapi = new IPConfigAPI.IPConfigAPI();
CamerasFound = ipconfigapi.GetDeviceCount();
Console.WriteLine("{0} Cameras found.", CamerasFound);

for (int i = 0; i < CamerasFound; i++)
{
    Console.WriteLine("Model             :{0}",ipconfigapi.GetModelName(i));
    Console.WriteLine("Manufacturer      :{0}", ipconfigapi.GetManufacturerName(i));
    Console.WriteLine("Serial Number     :{0}", ipconfigapi.GetSerialnumber(i));
    Console.WriteLine("User defined Name :{0}", ipconfigapi.GetUserDefinedName(i));
    Console.WriteLine("Firmware Version  :{0}", ipconfigapi.GetFirmwareversion(i));
    Console.WriteLine("Adapter GUID      :{0}", ipconfigapi.GetAdapterName(i));
    Console.WriteLine("IP Address        :{0}", ipconfigapi.GetIPAddress(i).ToString());
    Console.WriteLine("Subnet Mask       :{0}", ipconfigapi.GetSubnetmask(i).ToString());
    Console.WriteLine("Gateway           :{0}", ipconfigapi.GetGateway(i).ToString());
    Console.WriteLine("");
}
```
It also contains a function for getting the serial number of a camera with a specific IP address:
``` C#
IPAddress IP = IPAddress.Parse("192.168.2.2");
string SerialNumber = ipconfigapi.FindSerialNumberforIPaddress(IP);
```
The found serial number is used for opening the specific camera in IC Imaging Control:
``` C#
           if( SerialNumber != "")
            {
                foreach(Device dev in ic.Devices)
                {
                    String DevSerial;
                    dev.GetSerialNumber(out DevSerial);
                    // DevSerial can be shorter than 8 chars, 
                    // therefore, fill the string with "0" from
                    // beginning. (Leading zeros)
                    while (DevSerial.Length < 8)
                        DevSerial = "0" + DevSerial;

                    if(DevSerial == SerialNumber)
                    {
                        ic.Device = dev;
                        break;
                    }
                }
            }

            if( ic.DeviceValid)
            {
                Console.WriteLine("Camera opened!");
                ic.Device = "";
            }
```
The IPConfigAPI class can be used unchanged in Win32, x64 and anyCPU builds. For this, there is a little trick used:
``` C#
        [DllImport(".\\ipconfig_api_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IPConfig_Init")]
        private static extern int IPConfig_Init64();
        [DllImport(".\\ipconfig_api_win32.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IPConfig_Init")]
        private static extern int IPConfig_Init32();
```
The entry point, which means function name, is still "IPConfig_Init" in the 32 and 64 bit version of the DLL. But for C# it is  declared as IPConfig_Init64() and IPConfig_Init32();
By probing the size of the IntPtr you can determine, whether the program is currently 32 or 64 bit compiled and choose the 32 or 64 bit function call accordingly:

``` C#
            int a = 0;
            if (Marshal.SizeOf(typeof(IntPtr)) == 4)
                a =  IPConfig_Init32();
            else
                a = IPConfig_Init64();
``` 