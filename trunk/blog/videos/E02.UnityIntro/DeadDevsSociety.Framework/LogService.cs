using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DeadDevsSociety.Framework
{
    public class LogServiceSingleton
    {
        private static readonly LogService _logService = new LogService();
        private LogServiceSingleton(){}

        public static LogService LogService
        {
            get
            {
                return _logService;
            }
        }

    }

    public class LogService
    {
       public void WriteLine(string message)
       {
           Debug.WriteLine(message);
       }
    }
}
