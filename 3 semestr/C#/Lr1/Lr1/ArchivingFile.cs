using System;
using System.IO;
using System.IO.Compression;
using System.Windows;

namespace Lr1
{
    internal class ArchivingFile
    {
        internal void Compress(string sourceFile, string compressedFile)
        {
            try
            {

                // поток для чтения исходного файла
                using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open))
                {
                    // поток для записи сжатого файла
                    using (FileStream targetStream = File.Create(compressedFile))
                    {
                        // поток архивации
                        using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                        {
                            sourceStream.CopyTo(compressionStream);
                            MessageBox.Show("Сжатие " + sourceFile + " произошло успешно!" + "\n" +
                                "Размер первоначального файла равен: " + sourceStream.Length + "." + "\n" +
                                "Размер сжатого файла равен: " + targetStream.Length + ".");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        internal void Decompress(string compressedFile, string targetFile)
        {
            try
            {
                // поток для чтения из сжатого файла
                using (FileStream sourceStream = new FileStream(compressedFile, FileMode.Open))
                {
                    // поток для записи восстановленного файла
                    using (FileStream targetStream = File.Create(targetFile))
                    {
                        // поток разархивации
                        using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(targetStream);
                            MessageBox.Show("Разархивированный файл: " + targetFile);
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
