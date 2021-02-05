using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Capstone
{
    class Audit
    {
        static StreamWriter sw; // declare variable
        public static void Log(string logMessage)
        {
            try
            {
                string directory = Environment.CurrentDirectory;
                string filename = "Log.txt";
                string fullPath = Path.Combine(directory, filename);

                if (sw == null) // first time through this should be null, so proceed with the streamWriter
                {
                    // string filename = @"logs/search " + DateTime.Now.ToString("yyyy-MM-dd") + ".log"; // new filename to include date
                    sw = new StreamWriter(fullPath, true);
                }
                // string logMessage = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n" + stringInput + "\n";
                sw.WriteLine(logMessage);
                sw.Flush(); // call to write the data from the buffer
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to write to file");
            }
        }


    }
}
