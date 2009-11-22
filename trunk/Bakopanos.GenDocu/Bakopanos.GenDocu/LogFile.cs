using System;
using System.IO;

namespace Ciba.Utils.GenDocu
{
	/// <summary>
	/// LogFile.
	/// </summary>
	public class LogFile
	{
		private string msLog = "";
		private FileStream mLog  = null;

		public LogFile(string sFileName)
		{
			msLog = sFileName;
		}

		public void Close()
		{
			mLog.Close();
		}

		public void Write(string s)
		{
			if (mLog == null && msLog != "")
			{
				mLog = new FileStream(msLog, 
				FileMode.Create, FileAccess.Write, FileShare.Read);
			}
			if (mLog != null)
			{
				StreamWriter sw = new StreamWriter(mLog);
				sw.WriteLine(s); sw.Flush();
			}
		}

	}
}
