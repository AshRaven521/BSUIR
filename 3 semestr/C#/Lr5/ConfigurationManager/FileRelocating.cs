﻿using Lr2WindowsService;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace ConfigurationManager
{
    public class FileRelocating
    {
        private ConfigurationProvider provide = new ConfigurationProvider();
        public List<OptionsForDeserealizing> opt = new List<OptionsForDeserealizing>();
        private string beginRelocate;
        private string endXmlRelocate;
        private string endJsonRelocate;
        private object obj = new object();
        
        public FileRelocating()
        {
            opt = provide.GetOption();
            beginRelocate = opt[0].BeginFolder;
            endXmlRelocate = opt[0].EndFolderForXml;
            endJsonRelocate = opt[0].EndFolderForJson;
        }

        public async Task RelocateAsync()
        {
            await Task.Run(() => Relocate());
        }

        public void Relocate()
        {
             
            string[] Files = new string[100];
            Files = Directory.GetFiles(beginRelocate);
            
                foreach (string s in Files)
                {

                    if (Path.GetExtension(s) == ".json")
                    {
                        if (File.Exists(endJsonRelocate))
                        {

                            //Thread.Sleep(1000);
                            File.Delete(endJsonRelocate);
                            File.Move(s, endJsonRelocate);
                        }
                        else
                        {
                            File.Move(s, endJsonRelocate);
                        }


                    }
                    else
                    {
                        if (Path.GetExtension(s) == ".xml")
                        {
                            if (File.Exists(endXmlRelocate))
                            {
                                File.Delete(endXmlRelocate);
                                File.Move(s, endXmlRelocate);
                            }
                            else
                            {
                                File.Move(s, endXmlRelocate);
                            }
                    }
                        
                    }
                }
            
        }
    }
}
