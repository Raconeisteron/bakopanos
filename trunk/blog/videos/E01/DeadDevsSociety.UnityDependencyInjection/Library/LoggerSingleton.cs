using System.Diagnostics;
using System.IO;

namespace DeadDevsSociety.UnityDependencyInjection.Library
{
    public class LoggerSingleton
    {
        private static readonly Logger _logger = 
            new Logger(new TraceSwitch("", "") {Level = TraceLevel.Info});

        private LoggerSingleton(){}

        static LoggerSingleton()
        {
            //setup listeners
            //Debug.Listeners.Add(new ConsoleTraceListener());            
            string file = Path.GetTempFileName();
            Trace.Listeners.Add(new TextWriterTraceListener(file));
            Trace.AutoFlush = true;

        }

        public static ILogger Instance { get { return _logger; } }
    }

   
}