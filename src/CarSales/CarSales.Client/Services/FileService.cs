using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Client.Services
{
    public static class FileService
    {
        public static void SaveFile(byte[] content, string defaultFileName)
        {
            var dialog = new SaveFileDialog
            {
                FileName = defaultFileName,
                Filter = "Excel files (*.xlsx)|*.xlsx"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllBytes(dialog.FileName, content);
            }
        }
    }
}
