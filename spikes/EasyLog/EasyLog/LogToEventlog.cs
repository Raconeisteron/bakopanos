using System.Diagnostics;

namespace EasyLog
{
    /// <summary>
    /// Class that writes log events to the event log.
    /// </summary>
    /// <remarks>
    ///  GoF Design Pattern: Observer.
    /// The Observer Design Pattern allows this class to attach itself to an
    /// the logger and 'listen' to certain events and be notified of the event. 
    /// </remarks>
    public class LogToEventlog : ILog
    {
        /// <summary>
        /// Write a log request to the event log.
        /// </summary>
        /// <remarks>
        /// Actual event log write entry statements are commented out.
        /// Uncomment if you have the proper privileges.
        /// </remarks>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        public void Log(object sender, LogEventArgs e)
        {
            string message = e.SeverityString + " [" + e.Date + "] " +
                             e.Category + ": " + e.Message;

            var eventLog = new EventLog {Source = "Patterns In Action"};

            // Map severity level to an event log entry type
            EventLogEntryType type = EventLogEntryType.Error; 
            if (e.Severity < LogSeverity.Warning) type = EventLogEntryType.Information;
            if (e.Severity < LogSeverity.Error) type = EventLogEntryType.Warning;

            // In try catch. You will need proper privileges to write to eventlog.
            try { eventLog.WriteEntry(message, type); }
            catch { /* do nothing */ }
        }

    }
}