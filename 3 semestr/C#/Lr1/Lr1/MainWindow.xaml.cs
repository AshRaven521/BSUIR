using System;
using System.Windows;
using Microsoft.Win32;

namespace Lr1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private TxtFile txtFile = new TxtFile();
        private BinaryFile binFile = new BinaryFile();
        private ArchivingFile archiveFile = new ArchivingFile();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtFile.ReadingFromTextFile();
                foreach (string s in txtFile.str)
                {
                    lbFiles.Items.Add(s);
                }
                MessageBox.Show("Считывание завершено успешно!");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void BtnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtFile.WritingToTextFile();
                MessageBox.Show("Запись завершена успешно!");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void BinFileRead_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                binFile.ReadingFromBinFile();
                foreach (int bin in binFile.num)
                {
                    lbFiles.Items.Add(bin);
                }
                MessageBox.Show("Считывание завершено успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void BinFileSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                binFile.WritingToBinFile(binFile.num);
                MessageBox.Show("Запись завершена успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void BtnArchive_Click(object sender, RoutedEventArgs e)
        {
            string sourceFileForArchiving;
            string unzippedFile;
            try
            {
                OpenFileDialog openRarFileDialog = new OpenFileDialog
                {
                    Multiselect = true,
                    Filter = "All files (*.*)|*.*",
                };
                SaveFileDialog saveRarFileDialog = new SaveFileDialog
                {
                    Filter = "All files (*.*)|*.*",
                };
                if (openRarFileDialog.ShowDialog() == true)
                {
                    sourceFileForArchiving = openRarFileDialog.FileName;

                    if (saveRarFileDialog.ShowDialog() == true)
                    {
                        unzippedFile = saveRarFileDialog.FileName;
                        archiveFile.Compress(sourceFileForArchiving, unzippedFile);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void BtnUnarchive_Click(object sender, RoutedEventArgs e)
        {
            string fileAfterarchiving;
            string unzippedFile;
            try
            {
                OpenFileDialog openRarFileDialog = new OpenFileDialog
                {
                    Multiselect = true,
                    Filter = "All files (*.*)|*.*",
                };
                SaveFileDialog saveRarFileDialog = new SaveFileDialog
                {
                    Filter = "All files (*.*)|*.*",
                };
                if (openRarFileDialog.ShowDialog() == true)
                {
                    unzippedFile = openRarFileDialog.FileName;

                    if (saveRarFileDialog.ShowDialog() == true)
                    {
                        fileAfterarchiving = saveRarFileDialog.FileName;
                        archiveFile.Decompress(unzippedFile, fileAfterarchiving);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка :" + ex.Message);
            }
        }
    }
}
