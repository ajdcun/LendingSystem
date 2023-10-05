using LendingSystemUI.Core;
using System;
using System.Diagnostics;
using System.IO;

namespace LendingSystemUI.Services
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception exception = null);
    }

    public class LoggerService : BaseViewModel, ILoggerService
    {
        private const string LogFilePath = "C:/temp/Log.log";

        public LoggerService()
        {
            InitializeLogger();
        }

        private void InitializeLogger()
        {
            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath).Close();
            }

            var textListener = new TextWriterTraceListener(LogFilePath);
            Trace.Listeners.Add(textListener);
            Trace.AutoFlush = true;
        }

        private string InitEntry(string message)
        {
            return $"{DateTime.Now.ToString()}: {message}";
        }

        public void LogInfo(string message)
        {
            Trace.TraceInformation(InitEntry(message));
        }

        public void LogWarning(string message)
        {
            Trace.TraceWarning(InitEntry(message));
        }

        public void LogError(string message, Exception exception = null)
        {
            Trace.TraceError($"{InitEntry(message)} Exception: {exception?.ToString()}");
        }
    }
}
