using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using log4net;

namespace LibCommon
{
    public class Log
    {
        public static void Debug(string info)
        {
            LogManager.GetLogger("MyLogger").Debug(info);
        }

        public static void Error(string info)
        {
            LogManager.GetLogger("MyLogger").Error(info);
        }

        public static void Info(string info)
        {
            LogManager.GetLogger("MyLogger").Info(info);
        }

        public static void Warn(string info)
        {
            LogManager.GetLogger("MyLogger").Warn(info);
        }

        public static void Fatal(string info)
        {
            LogManager.GetLogger("MyLogger").Fatal(info);
        }

        public static ILog GetLogger()
        {
            return LogManager.GetLogger("MyLogger");
        }
    }
}
