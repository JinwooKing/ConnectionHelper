using NLog;
using System;


namespace ConsoleApp1.Model.Helper
{
    public class NlogHelper
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public enum LogType
        {
            Info,
            Error,
            Debug
        }

        public static void LogWrite(String msg, LogType logtype = LogType.Info)
        {
            switch (logtype)
            {
                case LogType.Info:
                    logger.Info(msg);
                    break;
                case LogType.Error:
                    logger.Error(msg);
                    break;
                case LogType.Debug:
                    logger.Debug(msg);
                    break;
                default:
                    break;
            }
        }
    }
}
