using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using Models;
using DataAccessLayer;
using System;
using LoggerToDB;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class JsonFile
    {
        private List<Person> list = new List<Person>();
        private ConnectionToDataBase connect = new ConnectionToDataBase();
        private ErrorLogger logger = new ErrorLogger();
        private object locker = new object();

        public async Task GenerateAsync()
        {
            await Task.Run(() => Generate());
        }

        public async Task Generate()
        {
            try
            {
                list = await connect.ConnectAndInteractAsync();

                var json = JsonSerializer.Serialize(list);

                using (var jsonStream = new StreamWriter(new FileStream(@"C:\PTUIR\3 semestr\ISP\Lr4\appsettings2.json", FileMode.OpenOrCreate)))
                {
                    jsonStream.WriteLine(json);
                }
            }
            catch(Exception e)
            {
                await logger.WriteErrorsToDataBaseAsync(e.Message);
            }

                
            

        }
    }
}
