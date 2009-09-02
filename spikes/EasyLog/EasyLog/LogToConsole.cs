using System;
using System.Globalization;

namespace EasyLog
{
    /// <summary>
    /// Class that writes log events to the console.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Observer.
    /// The Observer Design Pattern allows this class to attach itself to an
    /// the logger and 'listen' to certain events and be notified of the event. 
    /// </remarks>
    public class LogToConsole : ILog
    {
        /// <summary>
        /// Write a log request to the console.
        /// </summary>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        public void Log(object sender, LogEventArgs e)
        {
            string message = string.Format(CultureInfo.InvariantCulture, "[{0},{1},{2},{3}]{4}", e.SeverityString, e.Date.ToString(CultureInfo.InvariantCulture),e.Date.Ticks,
                               e.Category, e.Message);

            Console.WriteLine(message);

            if (e.Exception!=null)
            {
                if (e.Exception.Source != null)
                {
                    Console.WriteLine("Source: "+e.Exception.Source);
                }
                if (e.Exception.Message != null)
                {
                    Console.WriteLine("Message: "+e.Exception.Message);
                }

                if (e.Exception.TargetSite != null)
                {
                    Console.WriteLine("TargetSite: "+e.Exception.TargetSite.Name);
                }                
                
                if (e.Exception.StackTrace != null)
                {
                    Console.WriteLine("StackTrace: " + e.Exception.StackTrace);
                }        
        
                

            }
        }
    }
}