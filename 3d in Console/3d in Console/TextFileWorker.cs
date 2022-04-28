using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _3d_in_Console
{
    class TextFileWorker
    {
        public static string GetReadMeFile(string fileName)
        {
            try
            {
                return System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + @"/" + fileName);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
