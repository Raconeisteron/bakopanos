// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace UIComposition.Infrastructure.Services
{
    internal class LogService : ILogService
    {
        private static readonly LogWriter Writer;

        static LogService()
        {
            var formatter = new TextFormatter
                ("Timestamp: {timestamp}{newline}" +
                 "Message: {message}{newline}" +
                 "Category: {category}{newline}");

            //Create the Trace listeners
            var logFileListener = new FlatFileTraceListener(@"C:\temp\temp.log", "",
                                                            "", formatter);

            //Add the trace listeners to the source
            var mainLogSource =
                new LogSource("MainLogSource", SourceLevels.All);
            mainLogSource.Listeners.Add(logFileListener);

            var nonExistantLogSource = new LogSource("Empty");

            IDictionary<string, LogSource> traceSources =
                new Dictionary<string, LogSource>
                    {{"Info", mainLogSource}, {"Warning", mainLogSource}, {"Error", mainLogSource}};

            Writer = new LogWriterImpl(new ILogFilter[0],
                                       traceSources,
                                       nonExistantLogSource,
                                       nonExistantLogSource,
                                       mainLogSource,
                                       "Info",
                                       false,
                                       true);
        }

        #region ILogService Members

        public bool IsLoggingEnabled()
        {
            return Writer.IsLoggingEnabled();
        }

        public void WriteInfo(object message)
        {
            Writer.Write(message, "Info");
        }

        public void WriteWarning(object message)
        {
            Writer.Write(message, "Warning");
        }

        public void WriteError(object message)
        {
            Writer.Write(message, "Error");
        }

        #endregion
    }
}