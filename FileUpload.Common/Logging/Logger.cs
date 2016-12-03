using System;
using System.Diagnostics.CodeAnalysis;
using log4net;

namespace FileUpload.Common.Logging
{
    [ExcludeFromCodeCoverage]
    public class Logger : ILogger
    {
        private readonly ILog _logger = LogManager.GetLogger("FileUpload");

        public void Log(string message, LogLevel level, Exception exception = null)
        {
            switch (level)
            {
                case LogLevel.Error:
                    _logger.Error(message, exception);
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(message, exception);
                    break;
                case LogLevel.Info:
                    _logger.Info(message, exception);
                    break;
                case LogLevel.Warn:
                    _logger.Warn(message, exception);
                    break;
                default:
                    _logger.Debug(message, exception);
                    break;
            }
        }
    }
}
