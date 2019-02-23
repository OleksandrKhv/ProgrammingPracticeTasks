using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Util
{
    //TODO add log file?
    public class Logg
    {
        public static LogLevel CurrentLevel = LogLevel.Debug;

        public static void Write(object message, LogLevel level = LogLevel.Debug)
        {
            if (level >= CurrentLevel) return;

            System.Diagnostics.Debug.WriteLine(message.ToString());
        }

        public static void Error(object mesage) => Write(mesage,LogLevel.Error);
        public static void Warn(object mesage) => Write(mesage, LogLevel.Error);
        public static void Info(object mesage) => Write(mesage, LogLevel.Error);
        public static void Debug(object mesage) => Write(mesage, LogLevel.Error);
        public static void Trace(object mesage) => Write(mesage, LogLevel.Error);
    }

    public enum LogLevel
    {
        Off,
        Error,
        Warn,
        Info,
        Debug,
        Trace
    }
}
