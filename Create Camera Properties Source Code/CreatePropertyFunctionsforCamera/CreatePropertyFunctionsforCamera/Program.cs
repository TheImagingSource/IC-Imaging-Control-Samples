using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIS.Imaging;
using System.IO;
using System.Xml;

namespace CreatePropertyFunctionsforCamera
{

    class ICPropertiesCreator
    {
        private ICImagingControl _ic = new ICImagingControl();

        private List<Project> _projects = new List<Project>();
        private Dictionary<string, string> _InterfaceMap = new Dictionary<string, string>();

        public ICPropertiesCreator()
        {
            if (selectDevice())
            {
                EnumProperties();
            }
        }

        private bool selectDevice()
        {
            int i = 0;
            Console.WriteLine("Please select your device:");

            foreach (var dev in _ic.Devices)
            {
                i++;
                Console.WriteLine("{0} : {1}", i, dev.Name);
            }
            Console.WriteLine("0: End");

            string Selection = Console.ReadLine();
            try
            {
                int Sel = System.Convert.ToInt32(Selection);
                if (Sel > 0 && Sel <= _ic.Devices.Length)
                {
                    _ic.Device = _ic.Devices[Sel - 1];
                    Console.WriteLine("Selected device is {0}", _ic.DeviceCurrent.Name);
                    return true;
                }else
                    Console.WriteLine("Invalid input");


            }
            catch
            {
                Console.WriteLine("Nothing selected.");
            }


            return false;
        }

        private bool LoadProjects()
        {
            try
            {
                XmlDocument Projects = new XmlDocument();
                Projects.Load("projects.xml");

                XmlNodeList InterfaceMaps = Projects.SelectNodes("/projects/itfmaps/itfmap");
                foreach (XmlElement InterfaceMap in InterfaceMaps)
                {
                    _InterfaceMap.Add(InterfaceMap.Attributes["id"].Value.ToString(), InterfaceMap.Attributes["name"].Value.ToString());
                }

                XmlNodeList Projectlist = Projects.SelectNodes("/projects/project");
                foreach (XmlNode node in Projectlist)
                {
                    XmlElement ProjectNode = node as XmlElement;
                    CreateProject(ProjectNode);
                }

                foreach(var p in _projects)
                {
                    Console.WriteLine(p._MainFileName);
                }

            }
            catch (XmlException eex)
            {
                Console.WriteLine(eex.Message);
                return false;
            }
            return true;
        }

        private void CreateProject(XmlElement projectNode)
        {
            XmlElement main = projectNode.SelectSingleNode("main") as XmlElement;

            Project p = new Project(main.Attributes["name"].Value.ToString(), main.Attributes["template"].Value.ToString());

            XmlNodeList InterfaceList = projectNode.SelectNodes("interfaces/interface");
            foreach(XmlElement InterfaceNode in InterfaceList)
            {
               p.AddTemplate(InterfaceNode.Attributes["name"].Value.ToString(), InterfaceNode.Attributes["template"].Value.ToString());
            }

            _projects.Add(p);

        }

        /// <summary>
        /// 
        /// </summary>
        private void EnumProperties()
        {

            if( !LoadProjects() )
                return;

            foreach (VCDPropertyItem item in _ic.VCDPropertyItems)
            {
                QueryVCDPropertyElements(item);
            }

            foreach (var Project in _projects)
            {
                Project.Save();
            }
        }

        private void QueryVCDPropertyElements(VCDPropertyItem item)
        {
            foreach (TIS.Imaging.VCDPropertyElement elem in item.Elements)
            {
                QueryVCDPropertyInterface( elem);
            }
        }

        private void QueryVCDPropertyInterface(VCDPropertyElement elem)
        {
            foreach (VCDPropertyInterface itf in elem)
            {
                foreach (var Project in _projects)
                {
                    CreateCode(itf, Project);
                }
            }
        }

        private void CreateCode(VCDPropertyInterface itf, Project project)
        {
            string Templatename = "";

            if (_InterfaceMap.TryGetValue(itf.InterfaceGUID.ToString().ToUpper(), out Templatename))
            {
                Templatename = project.GetTemplateFileName(_InterfaceMap[itf.InterfaceGUID.ToString().ToUpper()]);
                String PropertyName = getPropertyName(itf.Parent);

                string Template = File.ReadAllText(Templatename);
                Template = Template.Replace("#name#", PropertyName);
                Template = Template.Replace("#itemid#", itf.Parent.Parent.ItemGUID.ToString());

                Template = Template.Replace("#elementid#", itf.Parent.ElementGUID.ToString());

                project.AddCode(Template);
            }
        }

        private string getPropertyName(VCDPropertyElement Element)
        {
            String Name = "";
            var map = new Dictionary<string, string>();
            map.Add( "284C0E0D-010B-45BF-8291-09D90A459B28:B57D3002-0AC6-4819-A609-272A33140ACA", "WhiteBalance_One_Push");
            map.Add( "284C0E0D-010B-45BF-8291-09D90A459B28:6519038B-1AD8-4E91-9021-66D64090CC85", "WhiteBalance_Red");
            map.Add( "284C0E0D-010B-45BF-8291-09D90A459B28:8407E480-175A-498C-8171-08BD987CC1AC","WhiteBalance_Green");
            map.Add( "284C0E0D-010B-45BF-8291-09D90A459B28:6519038A-1AD8-4E91-9021-66D64090CC85","WhiteBalance_Blue");
            map.Add( "90D5702E-E43B-4366-AAEB-7A7A10B448B4:65190390-1AD8-4E91-9021-66D64090CC85", "Exposure_MaxAutoAuto");

            string mapValue;
            if (map.TryGetValue(Element.Parent.ItemGUID.ToString().ToUpper() + ":" + Element.ElementGUID.ToString().ToUpper(), out mapValue))
            {
                Name = mapValue;
            }
            else
            {
                if (Element.Name == "Value")
                    Name = Element.Parent.Name;
                else
                {
                    Name = Element.Parent.Name + " " + Element.Name.Replace(Element.Parent.Name,"").Trim();
                }
            }
            String AllowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwyxz0123456789_";
            for( int i = 0; i < Name.Length; i++)
            {
                if (AllowedChars.IndexOf(Name[i]) == -1)
                   Name = Name.Replace(Name[i], '_');
            }
            return Name;
        }
    }

    /*******************************************************************************/
    class Program
    {
        static void Main(string[] args)
        {
            var x = new ICPropertiesCreator();
            Console.WriteLine("Done. Press Enter key to quit");
            Console.ReadKey();
        }
    }
}
