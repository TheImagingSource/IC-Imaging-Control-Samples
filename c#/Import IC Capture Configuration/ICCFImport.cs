using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TIS.Imaging;

namespace ICCFImport
{
    class ICCFImport
    {
        private static void AddXmlAttribute(XmlDocument xmldoc, XmlNode xmlnode, string Name, string Value)
        {
            XmlAttribute newAttribute = xmldoc.CreateAttribute(Name);
            newAttribute.Value = Value;
            xmlnode.Attributes.Append(newAttribute);
        }

        public static string Import(string ICCFFileName)
        {
            return Import(ICCFFileName, "");
        }

        public static string Import(string ICCFFileName, string UniqueDevice)
        {
            bool deviceimported = false;

            XmlDocument xmldoc = new XmlDocument();
            XmlNodeList xmlnode;

            XmlDocument device_xml = new XmlDocument();
            XmlNode deviceroot = device_xml.CreateElement("device_state");

            string AllowedNames = "videoformat fps vcdproperties";

            xmldoc.Load(ICCFFileName);
            xmlnode = xmldoc.GetElementsByTagName("devices");

            AddXmlAttribute(device_xml, deviceroot, "libver", "3.4");
            AddXmlAttribute(device_xml, deviceroot, "filemajor", "1");
            AddXmlAttribute(device_xml, deviceroot, "fileminor", "0");

            device_xml.AppendChild(deviceroot);

            for (int i = 0; i < xmlnode.Count; i++)
            {
                for (int d = 0; d < xmlnode[i].ChildNodes.Count && !deviceimported; d++)
                {
                    if (xmlnode[i].ChildNodes[d].Name == "device")
                    {

                        if (CheckForDeviceName(UniqueDevice, xmlnode, i, d))
                        {
                            XmlNode newdevicenode = device_xml.CreateElement("device");
                            AddXmlAttribute(device_xml, newdevicenode, "name", xmlnode[i].ChildNodes[d].Attributes["name"].Value.ToString());
                            deviceroot.AppendChild(newdevicenode);

                            for (int o = 0; o < xmlnode[i].ChildNodes[d].ChildNodes.Count; o++)
                            {
                                if (xmlnode[i].ChildNodes[d].ChildNodes[o].Name == "unique")
                                {
                                    AddXmlAttribute(device_xml, newdevicenode, "unique_name", xmlnode[i].ChildNodes[d].ChildNodes[o].InnerText);
                                }
                                else
                                {
                                    if (AllowedNames.Contains(xmlnode[i].ChildNodes[d].ChildNodes[o].Name))
                                    {
                                        if (xmlnode[i].ChildNodes[d].ChildNodes[o].Name == "vcdproperties")
                                            newdevicenode.AppendChild(device_xml.ImportNode(xmlnode[i].ChildNodes[d].ChildNodes[o].ChildNodes[0], true));
                                        else
                                            newdevicenode.AppendChild(device_xml.ImportNode(xmlnode[i].ChildNodes[d].ChildNodes[o], true));
                                    }
                                }
                            }

                            deviceimported = true;
                        }
                    }
                }
            }
            if (deviceimported)
                return device_xml.InnerXml;

            return "";
        }


        private static bool CheckForDeviceName(string UniqueDevice, XmlNodeList xmlnode, int i, int d)
        {
            bool ImportDevice = true;

            if (UniqueDevice != "")
            {
                ImportDevice = false;
                for (int o = 0; o < xmlnode[i].ChildNodes[d].ChildNodes.Count; o++)
                {
                    if (xmlnode[i].ChildNodes[d].ChildNodes[o].Name == "unique")
                    {
                        if (UniqueDevice == xmlnode[i].ChildNodes[d].ChildNodes[o].InnerText)
                        {
                            ImportDevice = true;
                        }
                    }
                }
            }

            return ImportDevice;
        }

        public static List<string> GetDeviceList(string ICCFFileName)
        {
            List<string> Devlist = new List<string>();

            XmlDocument xmldoc = new XmlDocument();
            XmlNodeList xmlnode;

            xmldoc.Load(ICCFFileName);
            xmlnode = xmldoc.GetElementsByTagName("devices");

            for (int i = 0; i < xmlnode.Count; i++)
            {
                for (int d = 0; d < xmlnode[i].ChildNodes.Count; d++)
                {
                    if (xmlnode[i].ChildNodes[d].Name == "device")
                    {
                        for (int o = 0; o < xmlnode[i].ChildNodes[d].ChildNodes.Count; o++)
                        {
                            if (xmlnode[i].ChildNodes[d].ChildNodes[o].Name == "unique")
                            {
                                Devlist.Add(xmlnode[i].ChildNodes[d].ChildNodes[o].InnerText);
                            }
                        }
                    }
                }
            }
            return Devlist;
        }

    }
}
