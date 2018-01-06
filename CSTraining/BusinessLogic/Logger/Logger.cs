using Contracts;
using System;
using NLog;

namespace BusinessLogic
{
    public class Logger : Contracts.ILogger
    {
        public void LogException(string message, Exception exception)
        {
            var logger = LogManager.GetLogger(exception.Source);
            logger.Error(message, exception);
        }

        public void LogDebugInfo(string message)
        {
            var logger = LogManager.GetLogger("DebugInfo");
            logger.Log(LogLevel.Debug, message);
        }

        public void Log(string message)
        {
            var logger = LogManager.GetLogger("Logger");
            logger.Log(LogLevel.Info, message);
        }
    }
}
