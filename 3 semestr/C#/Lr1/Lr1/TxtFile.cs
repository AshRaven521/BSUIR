using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Lr1
{
    internal class TxtFile
    {
        internal List<string> str = new List<string>();
        private string ReadFromTxtFile { get; set; }
        internal List<string> ReadingFromTextFile()
        {
            try
            {
                OpenFileDialog openTextFileDialog = new OpenFileDialog
                {
                    Multiselect = true,
                    Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                };
                if (openTextFileDialog.ShowDialog() == true)
                {
                    string readingTxtFileName = openTextFileDialog.FileName;

                    using (var streamRead = new StreamReader(readingTxtFileName))
                    {
                        while ((ReadFromTxtFile = streamRead.ReadLine()) != null)
                        {
                            str.Add(ReadFromTxtFile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            return str;
        }

        internal void WritingToTextFile()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string writingTextFileName = saveFileDialog.FileName;

                    FileStream fileStream = File.Create(writingTextFileName);
                    fileStream.Dispose();

                    using (var streamWrite = new StreamWriter(writingTextFileName))
                    {
                        foreach (string s in str)
                        {
                            streamWrite.WriteLine(s);
                        }
                        //после каждой записи очищаем список,чтобы избежать дублирования
                        str.Clear();
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
