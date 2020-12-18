﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lr2WindowsService
{
    internal class XMLParser : IConfigurationParser
    {
        public async Task<List<OptionsForDeserealizing>> ParseAsync()
        {
            return await Task.Run(() => Parse());
        }

        public virtual List<OptionsForDeserealizing> Parse()
        {
            string pathToXmlFile = @"C:\PTUIR\3 semestr\ISP\Lr4\FileManager\WindowsService\config.xml";
            List<OptionsForDeserealizing> xmlOptionsAfterParsing = new List<OptionsForDeserealizing>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(OptionsForDeserealizing));
            OptionsForDeserealizing parametrsAfterXmlParsing = new OptionsForDeserealizing();

            using (var xmlRead = new FileStream(pathToXmlFile, FileMode.OpenOrCreate))
            {
                parametrsAfterXmlParsing = (OptionsForDeserealizing)xmlSerializer.Deserialize(xmlRead);
            }

            if (parametrsAfterXmlParsing != null)
            {
                xmlOptionsAfterParsing.Add(parametrsAfterXmlParsing);
            }
            else
            {
                throw new NullReferenceException();
            }

            return xmlOptionsAfterParsing;
        }

    }
}
