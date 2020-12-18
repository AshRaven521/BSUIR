using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lr2WindowsService
{
    internal class JSONParser : IConfigurationParser
    {
        private string jsonString = "";
        private List<OptionsForDeserealizing> optionsAfterJsonParsing = new List<OptionsForDeserealizing>();

        public async Task<List<OptionsForDeserealizing>> ParseAsync()
        {
            return await Task.Run(() => Parse());
        }

        public virtual List<OptionsForDeserealizing> Parse()
        {
            using (var jsonStream = new StreamReader(@"C:\PTUIR\3 semestr\ISP\Lr4\FileManager\WindowsService\appsettings.json"))
            {
                jsonString = jsonStream.ReadToEnd();
            }
            OptionsForDeserealizing parametrsAfterJsonParsing = JsonSerializer.Deserialize<OptionsForDeserealizing>(jsonString);

            if (parametrsAfterJsonParsing != null)
            {
                optionsAfterJsonParsing.Add(parametrsAfterJsonParsing);
            }
            else
            {
                throw new NullReferenceException();
            }

            return optionsAfterJsonParsing;
        }
    }
}
