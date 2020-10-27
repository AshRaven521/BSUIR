using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Lr1
{
    internal class BinaryFile
    {
        internal List<int> num = new List<int>();
        internal List<int> ReadingFromBinFile()
        {
            try
            {
                OpenFileDialog openBinaryFileDialog = new OpenFileDialog
                {
                    Multiselect = true,
                    Filter = "Binary files (*.dat)|*.dat|All files (*.*)|*.*",
                };

                if (openBinaryFileDialog.ShowDialog() == true)
                {
                    string readingBinFileName = openBinaryFileDialog.FileName;

                    using (var streamRead = new BinaryReader(File.Open(readingBinFileName, FileMode.Open)))
                    {
                        while (streamRead.PeekChar() > -1)
                        {
                            num.Add(streamRead.ReadByte());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            return num;
        }

        internal void WritingToBinFile(List<int> number)
        {
            try
            {
                SaveFileDialog saveBinaryFileDialog = new SaveFileDialog
                {
                    Filter = "Binary files (*.dat)|*.dat|All files (*.*)|*.*",
                };

                if (saveBinaryFileDialog.ShowDialog() == true)
                {
                    string writingBinFileName = saveBinaryFileDialog.FileName;

                    using (var streamWrite = new BinaryWriter(File.Open(writingBinFileName, FileMode.OpenOrCreate)))
                    {
                        foreach (int n in number)
                        {
                            streamWrite.Write(n);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
