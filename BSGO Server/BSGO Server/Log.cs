using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class Log
    {
        public enum LogDir
        {
            Out,
            In
        }
        public static void Add(LogSeverity Severity, LogDir logDir, string text)
        {
            string finalText = "";

            switch (Severity)
            {
                case LogSeverity.SERVERINFO:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    finalText += "[SERVER INFO]";
                    break;
                case LogSeverity.INFO:
                    Console.ForegroundColor = ConsoleColor.Green;
                    finalText += "[INFO]";
                    break;
                case LogSeverity.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    finalText += "[WARN]";
                    break;
                case LogSeverity.ERROR:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    finalText += "[ERR]";
                    break;
            }

            finalText += "[" + System.DateTime.Now + "]";
            finalText += "[" + logDir +"] ";

            finalText += text;

            Console.WriteLine(finalText);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Add(LogSeverity Severity, string text)
        {
            string finalText = "";

            switch (Severity)
            {
                case LogSeverity.SERVERINFO:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    finalText += "[SERVER INFO]";
                    break;
                case LogSeverity.INFO:
                    Console.ForegroundColor = ConsoleColor.Green;
                    finalText += "[INFO]";
                    break;
                case LogSeverity.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    finalText += "[WARN]";
                    break;
                case LogSeverity.ERROR:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    finalText += "[ERR]";
                    break;
            }

            finalText += "[" + System.DateTime.Now + "] ";

            finalText += text;

            Console.WriteLine(finalText);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
