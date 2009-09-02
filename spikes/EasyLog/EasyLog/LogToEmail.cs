using System.Net.Mail;

namespace EasyLog
{
    /// <summary>
    /// Class that sends log events via email.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Observer.
    /// The Observer Design Pattern allows this class to attach itself to an
    /// the logger and 'listen' to certain events and be notified of the event. 
    /// </remarks>
    public class LogToEmail : ILog
    {
        private readonly string _from;
        private readonly string _to;
        private readonly string _subject;        
        private readonly SmtpClient _smtpClient;

        /// <summary>
        /// Constructor for the ObserverlogToEmail class
        /// </summary>
        /// <param name="from">From email address.</param>
        /// <param name="to">To email address.</param>
        /// <param name="subject">Email subject.</param>        
        /// <param name="smtpClient">Smtp email client.</param>
        public LogToEmail(string from, string to, string subject, SmtpClient smtpClient)
        {
            _from = from;
            _to = to;
            _subject = subject;            
            _smtpClient = smtpClient;
        }

        #region ILog Members

        /// <summary>
        /// Sends a log request via email.
        /// </summary>
        /// <remarks>
        /// Actual email 'Send' calls are commented out.
        /// Uncomment if you have the proper email privileges.
        /// </remarks>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        public void Log(object sender, LogEventArgs e)
        {
            string body = "Date = " + e.Date + "\r\n" +
                          "Category = " + e.Category + "\r\n" +
                          "Severity = " + e.SeverityString + "\r\n" +
                          "Message = " + e.Message;

            // Commented out for now. You need privileges to send email.
            _smtpClient.Send(_from, _to, _subject, body);
        }

        #endregion
    }
}