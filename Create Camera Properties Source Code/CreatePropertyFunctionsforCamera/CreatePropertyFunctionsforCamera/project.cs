using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CreatePropertyFunctionsforCamera
{
    class Project
    {
        public String _MainFileName;
        private String _MainFileTemplate;
        private Dictionary<string, string> ItfTemplate = new Dictionary<string, string>();
        private String _Code = "";
        public Project(String MainFileName, string MainFileTemplate)
        {
            _MainFileName = MainFileName;
            _MainFileTemplate = MainFileTemplate;
        }
        public void  AddTemplate( String Interface, String TemplateFile)
        {
            ItfTemplate.Add(Interface, TemplateFile);
        }

        public String GetTemplateFileName(String Interface)
        {
            return ItfTemplate[Interface];
        }

        public void AddCode(string code)
        {
            _Code += code;
        }

        public void Save()
        {
            string Template = File.ReadAllText(_MainFileTemplate);

            Template = Template.Replace("#code#", _Code);
            File.WriteAllText(_MainFileName, Template);
            Console.WriteLine("Wrote file " + _MainFileName);
        }
    }
}
