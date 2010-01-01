using System.Diagnostics;

namespace DeadDevsSociety.UnityDependencyInjection.Library
{
    public class Logger : ILogger
    {
        private static TraceSwitch _traceSwitch;

        public Logger(TraceSwitch traceSwitch)
        {
            _traceSwitch = traceSwitch;            
        }

        public void Information(string message, string category)
        {
            if (_traceSwitch.TraceInfo)
            {               
                Trace.WriteLine(message, category);
                Debug.WriteLine(message, category);                
            }
        }

        public void Error(string message, string category)
        {
            if (_traceSwitch.TraceError)
            {
                Trace.WriteLine(message, category);
                Debug.WriteLine(message, category);
            }
        }
    }

}