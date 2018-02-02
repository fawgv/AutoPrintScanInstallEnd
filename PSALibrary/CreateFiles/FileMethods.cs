using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSALibrary.CreateFiles
{
    public class FileMethods
    {
        public static string CreateFileSettings(string fileName, string pathToFile, string[] arrayStrings)
        {
            
            try
            {
                var fullPath = System.IO.Path.Combine(pathToFile, fileName);

                using (StreamWriter streamWriter = new StreamWriter(fullPath, false, Encoding.Default))
                {
                    foreach (var curString in arrayStrings)
                    {
                        streamWriter.WriteLine(curString);
                    }
                }
                
                return $"Создание файла {fileName} выполнено корректно";
            }
            catch (Exception)
            {
                return "При формировании файла {fileName} произошли ошибки.";
            }
            

        }
    }
}
