using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyLog
{
    /// <summary>
    /// Singleton logger class through which all log events are processed.
    /// </summary>
    /// <remarks>
    /// GoF Design Patterns: Singleton, Observer.
    /// </remarks>
    public sealed class SingletonLogger : Logger
    {
        #region The Singleton definition

        /// <summary>
        /// The one and only Singleton Logger instance. 
        /// </summary>
        private static readonly Logger instance = new SingletonLogger();

        /// <summary>
        /// Private constructor. Initializes default severity to "Error".
        /// </summary>
        private SingletonLogger()
        {
            // Default severity is Error level
            Severity = LogSeverity.Error;
        }

        /// <summary>
        /// Gets the instance of the singleton logger object.
        /// </summary>
        public static Logger Instance
        {
            get { return instance; }
        }

        #endregion

    }
}