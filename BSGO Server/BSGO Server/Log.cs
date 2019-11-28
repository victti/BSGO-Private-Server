using System;
using System.Threading.Tasks;

namespace BSGO_Server
{
    internal static class Log
    {
        public enum LogDir
        {
            Out,
            In
        }
        public static void Add(LogSeverity Severity, LogDir logDir, string text)
        {
            Task.Factory.StartNew(() =>
            {
                string finalText = "";

                switch (Severity)
                {
                    case LogSeverity.SERVERINFO:
                        finalText += "[SERVER INFO]";
                        break;
                    case LogSeverity.INFO:
                        finalText += "[INFO]";
                        break;
                    case LogSeverity.WARNING:
                        finalText += "[WARN]";
                        break;
                    case LogSeverity.ERROR:
                        finalText += "[ERR]";
                        break;
                }

                finalText += "[" + DateTime.Now + "]";
                finalText += "[" + logDir + "] ";

                finalText += text;

                Console.WriteLine(finalText);
            });
        }

        public static void Add(LogSeverity Severity, string text)
        {
            Task.Factory.StartNew(() =>
            {
                string finalText = "";

                switch (Severity)
                {
                    case LogSeverity.SERVERINFO:
                        finalText += "[SERVER INFO]";
                        break;
                    case LogSeverity.INFO:
                        finalText += "[INFO]";
                        break;
                    case LogSeverity.WARNING:
                        finalText += "[WARN]";
                        break;
                    case LogSeverity.ERROR:
                        finalText += "[ERR]";
                        break;
                }

                finalText += "[" + DateTime.Now + "] ";

                finalText += text;

                Console.WriteLine(finalText);
            });
        }
    }
}
