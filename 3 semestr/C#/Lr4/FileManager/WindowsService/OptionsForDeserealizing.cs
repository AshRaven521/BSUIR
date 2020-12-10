namespace Lr2WindowsService
{
    public class OptionsForDeserealizing
    {
        public string TargetDirectoryPath { get; set; }

        public string SourceDirectoryPath { get; set; }

        public string ExtractDirectoryPath { get; set; }

        public string BeginFolder { get; set; }

        public string EndFolderForXml { get; set; }

        public string EndFolderForJson { get; set; }

        public string connectionString { get; set; }

        public string connectionStringToErrors { get; set; }

        public OptionsForDeserealizing()
        {
            TargetDirectoryPath = @"C:\PTUIR\3 semestr\ISP\Lr3\archive";

            SourceDirectoryPath = @"C:\PTUIR\3 semestr\ISP\Lr3\source";

            ExtractDirectoryPath = @"C:\PTUIR\3 semestr\ISP\Lr3\extract";

            BeginFolder = @"C:\PTUIR\3 semestr\ISP\Lr4";

            EndFolderForXml = @"C:\PTUIR\3 semestr\ISP\Lr3\source\config2.xml";

            EndFolderForJson = @"C:\PTUIR\3 semestr\ISP\Lr3\source\appsettings2.json";

            connectionString = "Server=LAPTOP-VAJ5IJTR;Database=AdventureWorks2019;Trusted_Connection=True;Connect Timeout = 30";

            connectionStringToErrors = "Server=LAPTOP-VAJ5IJTR;Database=ErrorLogs;Trusted_Connection=True";
        }

    }

}