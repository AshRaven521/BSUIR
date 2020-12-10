using System.Collections.Generic;
using System.IO;

namespace Lr2WindowsService
{
    public class ConfigurationProvider
    {
        private readonly JSONParser jsonObject;
        private readonly XMLParser xmlObject;
        private List<OptionsForDeserealizing> option;

        public ConfigurationProvider()
        {
            jsonObject = new JSONParser();
            xmlObject = new XMLParser();
            option = new List<OptionsForDeserealizing>();
        }

        public List<OptionsForDeserealizing> GetOption()
        {
            string[] Files = new string[100];
            Files = Directory.GetFiles(@"C:\PTUIR\3 semestr\ISP\Lr4\FileManager\WindowsService");
            foreach (string s in Files)
            {

                if (Path.GetExtension(s) == ".json")
                {
                    option = jsonObject.Parse();
                }
                else
                {
                    if (Path.GetExtension(s) == ".xml")
                    {
                        option = xmlObject.Parse();
                    }
                }
            }

            return option;
        }

    }
}
