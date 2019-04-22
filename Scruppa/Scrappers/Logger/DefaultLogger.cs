using System;
using System.IO;
using System.Threading.Tasks;

namespace Scruppa.Scrappers.Logger
{
    public class DefaultLogger : ILogger
    {

        private string _logFile;
        private const string LogsPath = "logs";

        public DefaultLogger()
        {
            var now = DateTime.Now;
            _logFile = Path.Combine(LogsPath, "log_")
                + now.Year + now.Month
                + now.Day + now.Hour
                + now.Minute + now.Second
                + ".log";

            if (!Directory.Exists(LogsPath))
                Directory.CreateDirectory(LogsPath);
        }
        public void Log(string message)
        {
            Console.WriteLine(message);
            File.AppendAllLines(_logFile, new[] { message });
        }
    }
}