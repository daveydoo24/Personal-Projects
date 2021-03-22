using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Capstone
{
    abstract class Audit
    {
        static StreamWriter sw;
        public static void Log(string logMessage)
        {
            try
            {
                string directory = Environment.CurrentDirectory;
                string filename = "Log.txt";
                string fullPath = Path.Combine(directory, filename);

                if (sw == null)
                {
                    sw = new StreamWriter(fullPath, true);
                }
                sw.WriteLine(logMessage);
                sw.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to write to file");
            }
        }
    }
}
