using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Models;
using System.IO;
using DataAccessLayer;

namespace ServiceLayer
{
    public class XMLFile : IConfigGenerating
    {
        private string xmlPath = @"C:\PTUIR\3 semestr\ISP\Lr4\config2.xml";
        private List<Person> person = new List<Person>();
        private ConnectionToDataBase connect = new ConnectionToDataBase();
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));

        public XMLFile()
        {
            
        }

        public async Task GenerateAsync()
        {
            await Task.Run(() => Generate());
        }
        public async void Generate() 
        {
            person = await connect.ConnectAndInteractAsync();

            using (var xmlStream = new FileStream(xmlPath, FileMode.OpenOrCreate))
            {
                 xmlSerializer.Serialize(xmlStream, person);
            }
        }
            
    }
}
