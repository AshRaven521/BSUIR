using System;
using System.Windows;
using Microsoft.Win32;

namespace Lr2WpfApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConnectionWithWindowService connect = new ConnectionWithWindowService();
      
        public MainWindow()
        {
            InitializeComponent();
            sourceText.Text = @"C:\PTUIR\3 semestr\ISP\Lr2\source";
            archiveText.Text = @"C:\PTUIR\3 semestr\ISP\Lr2\archive";
            extractText.Text = @"C:\PTUIR\3 semestr\ISP\Lr2\extract";
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connect.StopService();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderWatcher sourceWatcher = new FolderWatcher(sourceText.Text);
            FolderWatcher archiveWatcher = new FolderWatcher(archiveText.Text);
            FolderWatcher extractWatcher = new FolderWatcher(extractText.Text);
            sourceWatcher.StartWatching();
            archiveWatcher.StartWatching();
            extractWatcher.StartWatching();
            connect.StartService();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            connect.StopService();
        }
    }
}
