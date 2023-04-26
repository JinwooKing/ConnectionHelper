using NLog;

namespace ConsoleApp2.Model.Helper
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
            Console.WriteLine(msg);

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
