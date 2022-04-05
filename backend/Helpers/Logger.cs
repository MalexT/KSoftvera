using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class Logger
    {
        public void LogError(string message)
        {
            using (StreamWriter file = File.AppendText(Environment.CurrentDirectory+"/log.txt"))
            {
                Log(message, file);
            }
        }

        private static void Log(string logMessage, TextWriter w)
        {
            w.WriteLine("\r\nLog Entry: ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("---------------------");
            w.Flush();
        }
    }
}
