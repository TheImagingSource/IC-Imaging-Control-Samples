using System;
using System.Net;
using TIS.Imaging;


namespace IPConfigAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            ICImagingControl ic = new ICImagingControl();

            UInt32 CamerasFound;
            IPConfigAPI ipconfigapi = new IPConfigAPI();
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

            IPAddress IP = ipconfigapi.FindIPAdressForSerialnumber("00001234");
            Console.WriteLine("Found IP " + IP.ToString());

            IP = IPAddress.Parse("192.168.2.2");
            string SerialNumber = ipconfigapi.FindSerialNumberforIPaddress(IP);
            Console.WriteLine("Found serial number " + SerialNumber);

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

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
