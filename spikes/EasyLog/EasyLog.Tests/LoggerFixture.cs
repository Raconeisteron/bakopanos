using System;
using System.IO;
using NUnit.Framework;

namespace EasyLog
{
    [TestFixture]
    public class LoggerFixture
    {
        [Test,Explicit]
        public void LogToFile()
        {
            string filename = Path.GetTempFileName();
            ILog logF = new LogToFile( filename);
            ILog logC = new LogToConsole();
            SingletonLogger.Instance.Attach(logF);
            SingletonLogger.Instance.Attach(logC);

            SingletonLogger.Instance.Category = "test";

            SingletonLogger.Instance.Error(filename);

            try
            {
                int k=1, l=0;
                int i = k/l;
            }
            catch (Exception error)
            {
                SingletonLogger.Instance.Error("test error", error);                
            }            

            SingletonLogger.Instance.Detach(logF);
            SingletonLogger.Instance.Detach(logC);
        }
    }
}
