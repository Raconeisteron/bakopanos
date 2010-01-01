using System.Diagnostics;

namespace DeadDevsSociety.UnityDependencyInjection.Library
{
    public class Logger : ILogger
    {
        protected Logger()
        {
        }

        public Logger(TraceSwitch traceSwitch)
        {
            TraceSwitch = traceSwitch;             
        }

        public TraceSwitch TraceSwitch
        {
            get; private set;
        }

        public void Verbose(string message, string category)
        {
            if (TraceSwitch.TraceVerbose)
            {
                Trace.WriteLine(message, category);
            }
        }
        
        public void Warning(string message, string category)
        {
            if (TraceSwitch.TraceWarning)
            {
                Trace.WriteLine(message, category);
            }
        }

        public void Information(string message, string category)
        {
            if (TraceSwitch.TraceInfo)
            {               
                Trace.WriteLine(message, category);                     
            }
        }

        public void Error(string message, string category)
        {
            if (TraceSwitch.TraceError)
            {
                Trace.WriteLine(message, category);                
            }
        }
    }

}