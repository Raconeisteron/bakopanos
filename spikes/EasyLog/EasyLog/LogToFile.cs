using System.Globalization;
using System.IO;

namespace EasyLog
{
    /// <summary>
    /// Class that writes log events to a local file.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Observer.
    /// The Observer Design Pattern allows this class to attach itself to an
    /// the logger and 'listen' to certain events and be notified of the event. 
    /// </remarks>
    public class LogToFile : ILog
    {
        private readonly string _fileName;

        /// <summary>
        /// Constructor of ObserverLogToFile.
        /// </summary>
        /// <param name="fileName">File log to.</param>
        public LogToFile(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Write a log request to a file.
        /// </summary>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        public void Log(object sender, LogEventArgs e)
        {
            string message = string.Format(CultureInfo.InvariantCulture, "[{0},{1},{2},{3}]{4}", e.SeverityString, e.Date.ToString(CultureInfo.InvariantCulture), e.Date.Ticks,
                               e.Category, e.Message);            
            FileStream fileStream;

            // Create directory, if necessary
            try
            {
                fileStream = new FileStream(_fileName, FileMode.Append);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory((new FileInfo(_fileName)).DirectoryName);
                fileStream = new FileStream(_fileName, FileMode.Append);
            }

            // NOTE: Be sure you have write privileges to folder
            var writer = new StreamWriter(fileStream);
            try
            {
                writer.WriteLine(message);
            }
            catch { /* do nothing for now */}
            finally
            {
                try
                {
                    writer.Close();
                }
                catch { /* do nothing for now */}
            }
        }
    }
}