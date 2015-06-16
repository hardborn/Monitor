using System;
using System.IO;
using log4net;

namespace Log4NetLibrary
{
    public sealed class FileLogService : ILogService
    {
        readonly ILog _logger;

        static FileLogService()
        {
            var log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, "log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));
        }

        public FileLogService(Type logClass)
        {
            _logger = LogManager.GetLogger(logClass);
        }


        public void Fatal(string errorMessage)
        {
            if (_logger.IsFatalEnabled)
            {
                try
                {
                    _logger.Fatal(errorMessage);
                }
                catch (Exception)
                {

                }
            }
        }

        public void Error(string errorMessage)
        {
            if (_logger.IsErrorEnabled)
            {
                try
                {
                    _logger.Error(errorMessage);
                }
                catch (Exception)
                {

                }
            }
        }

        public void Warn(string message)
        {
            if (_logger.IsWarnEnabled)
            {
                try
                {
                    _logger.Warn(message);
                }
                catch (Exception)
                {

                }
            }

        }

        public void Info(string message)
        {
            if (_logger.IsInfoEnabled)
            {
                try
                {
                    _logger.Info(message);
                }
                catch (Exception)
                {

                }
            }

        }

        public void Debug(string message)
        {
            if (_logger.IsDebugEnabled)
            {
                try
                {
                    _logger.Debug(message);
                }
                catch (Exception)
                {

                }
            }

        }
    }
}
