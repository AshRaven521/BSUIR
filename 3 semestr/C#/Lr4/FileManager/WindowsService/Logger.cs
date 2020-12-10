using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Lr2WindowsService
{
    public class Logger
    {
        private FileSystemWatcher sourceFolderWatcher;
        private FileSystemWatcher targetFolderWatcher;

        private ConfigurationProvider config;
        private List<OptionsForDeserealizing> listOfOptions = new List<OptionsForDeserealizing>();
        private Archivator archivation;
        private Encryptor encryption;
        private readonly string targetDirectoryPath;
        private readonly string sourceDirectoryPath;
        private readonly string extractDirectoryPath;
        private bool enabled = true;

        public Logger()
        {
            config = new ConfigurationProvider();
            listOfOptions = config.GetOption();
            archivation = new Archivator();
            encryption = new Encryptor();

            if (listOfOptions.Count >= 0)
            {
                sourceDirectoryPath = listOfOptions[0].SourceDirectoryPath;
                targetDirectoryPath = listOfOptions[0].TargetDirectoryPath;
                extractDirectoryPath = listOfOptions[0].ExtractDirectoryPath;
                sourceFolderWatcher = new FileSystemWatcher(sourceDirectoryPath);
                sourceFolderWatcher.Created += SourceFolderWatcher_Created;
                targetFolderWatcher = new FileSystemWatcher(targetDirectoryPath);
                targetFolderWatcher.Created += TargetFolderWatcher_Created;
            }
        }

        public void Start()
        {
            sourceFolderWatcher.EnableRaisingEvents = true;

            targetFolderWatcher.EnableRaisingEvents = true;

            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            sourceFolderWatcher.EnableRaisingEvents = false;

            targetFolderWatcher.EnableRaisingEvents = false;

            enabled = false;
        }

        private void SourceFolderWatcher_Created(object sender, FileSystemEventArgs e)
        {
            archivation.Archive(e.FullPath, targetDirectoryPath);
        }

        private void TargetFolderWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(1000);
            archivation.Dearchive(e.FullPath, extractDirectoryPath);
        }
    }
}
