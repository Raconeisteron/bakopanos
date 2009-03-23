using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Evolutil.Library.Log
{
    /// <summary>
    /// Logger class through which all log events are processed.
    /// </summary>
    /// <remarks>
    /// GoF Design Patterns: Observer.
    /// </remarks>
    internal sealed class Logger : ILogger
    {
        public Logger(LogConfigurationSection logConfig)
        {
            Severity = (LogSeverity)Enum.Parse(typeof(LogSeverity), logConfig.Severity, true);
            
            Attach(new ObserverLogToFile(logConfig.FileName));

            if (logConfig.Eventlog)
            {
                Attach(new ObserverLogToEventlog());
            }
        }

        /// <summary>
        /// Delegate event handler that hooks up requests.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        /// <remarks>
        /// GoF Design Pattern: Observer.
        /// The Observer Design Pattern allows Observer classes to attach itself to 
        /// this Logger class and be notified if certain events occur.         
        /// </remarks>
        public delegate void LogEventHandler(object sender, LogEventArgs e);

        /// <summary>
        /// The Log event.
        /// </summary>
        public event LogEventHandler Log;    

        private LogSeverity _severity;

        // These booleans are used strictly for performance improvement.
        private bool _isDebug;
        private bool _isInfo;
        private bool _isWarning;
        private bool _isError;
        private bool _isFatal;

        /// <summary>
        /// Gets and sets the severity level of logging activity.
        /// </summary>
        public LogSeverity Severity
        {
            get { return _severity; }
            set
            {
                _severity = value;

                // Set booleans to help improve performance
                int severity = (int)_severity;

                _isDebug = ((int)LogSeverity.Debug) >= severity ? true : false;
                _isInfo = ((int)LogSeverity.Info) >= severity ? true : false;
                _isWarning = ((int)LogSeverity.Warning) >= severity ? true : false;
                _isError = ((int)LogSeverity.Error) >= severity ? true : false;
                _isFatal = ((int)LogSeverity.Fatal) >= severity ? true : false;
            }
        }

        /// <summary>
        /// Log a message when severity level is "Debug" or higher.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Debug(string message)
        {
            if (_isDebug)
                Debug(message, null);
        }

        /// <summary>
        /// Log a message when severity level is "Debug" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public void Debug(string message, Exception exception)
        {
            if (_isDebug)
                OnLog(new LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now));
        }

        /// <summary>
        /// Log a message when severity level is "Info" or higher.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Info(string message)
        {
            if (_isInfo)
                Info(message, null);
        }

        /// <summary>
        /// Log a message when severity level is "Info" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public void Info(string message, Exception exception)
        {
            if (_isInfo)
                OnLog(new LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now));
        }

        /// <summary>
        /// Log a message when severity level is "Warning" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        public void Warning(string message)
        {
            if (_isWarning)
                Warning(message, null);
        }

        /// <summary>
        /// Log a message when severity level is "Warning" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public void Warning(string message, Exception exception)
        {
            if (_isWarning)
                OnLog(new LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now));
        }

        /// <summary>
        /// Log a message when severity level is "Error" or higher.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Error(string message)
        {
            if (_isError)
                Error(message, null);
        }

        /// <summary>
        /// Log a message when severity level is "Error" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public void Error(string message, Exception exception)
        {
            if (_isError)
                OnLog(new LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now));
        }

        /// <summary>
        /// Log a message when severity level is "Fatal"
        /// </summary>
        /// <param name="message">Log message</param>
        public void Fatal(string message)
        {
            if (_isFatal)
                Fatal(message, null);
        }

        /// <summary>
        /// Log a message when severity level is "Fatal"
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public void Fatal(string message, Exception exception)
        {
            if (_isFatal)
                OnLog(new LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now));
        }

        /// <summary>
        /// Invoke the Log event.
        /// </summary>
        /// <param name="e">Log event parameters.</param>
        public void OnLog(LogEventArgs e)
        {
            if (Log != null)
            {
                Log(this, e);
            }
        }

        /// <summary>
        /// Attach a listening observer logging device to logger.
        /// </summary>
        /// <param name="observer">Observer (listening device).</param>
        public void Attach(ILog observer)
        {
            Log += observer.Log;
        }

        /// <summary>
        /// Detach a listening observer logging device from logger.
        /// </summary>
        /// <param name="observer">Observer (listening device).</param>
        public void Detach(ILog observer)
        {
            Log -= observer.Log;
        }
    }
}