using System.Diagnostics;

namespace DeadDevsSociety.UnityDependencyInjection.Library
{
    public interface ILogger
    {
        void Verbose(string message, string category);
        void Warning(string message, string category);
        void Information(string message, string category);
        void Error(string message, string category);
        TraceSwitch TraceSwitch
        {
            get;            
        }
    }

  
}