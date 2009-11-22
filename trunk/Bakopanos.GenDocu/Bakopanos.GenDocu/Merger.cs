using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace Ciba.Utils.GenDocu
{
	/// <summary>
	/// Merger.
	/// </summary>
	public class Merger
	{
		public string   TemplateFileName;
		public string   ParameterFileName;
		public Encoding ParameterEncoding;
		public string   OutputFileName;
		public string   LogFileName;

		public Merger()
		{
			TemplateFileName  = "";
			ParameterFileName = "";
			ParameterEncoding = Encoding.GetEncoding(1252);
			OutputFileName    = "";
			LogFileName       = "";
		}

		public bool Generate()
		{
			TemplateFile  t = new TemplateFile();
			ParameterFile p = new ParameterFile();
			LogFile       l = new LogFile(LogFileName);

			File.Delete(OutputFileName);

			l.Write("Ciba.Utils.GenDocu 1.0 Copyright (c) 2003 Ciba Specialty Chemicals Inc.");
			l.Write("");
			l.Write("started: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
			l.Write("");
			l.Write("template  file: " + TemplateFileName);
			if (ParameterEncoding == null)
			l.Write("parameter file: " + ParameterFileName); else
			l.Write("parameter file: " + ParameterFileName
				+ " (encoding: " + ParameterEncoding.EncodingName + ")");
			l.Write("document  file: " + OutputFileName);
			l.Write("");

			t.LogFile = l;
			p.LogFile = l;

			if (t.Load(TemplateFileName)
			&&  p.Load(ParameterFileName, ParameterEncoding)
			&&  Merge(t, p, l))
			{
				l.Write("successfully generated " + OutputFileName);
			}

			l.Close();
			return true;
		}

		private bool Merge(TemplateFile tf, ParameterFile pf, LogFile lf)
		{
			FileStream   fs = new FileStream(OutputFileName, FileMode.Create, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(1252));

			int i = 0;
			int j = 0;

			sw.Write(tf.Text.Substring(0, tf[i].Begin));
			for (; i < pf.Count; i++)
			{
				if ((j = tf.Find(pf.GetParameterName(i), 0)) < 0) continue;

				sw.Write(pf.GetParameterValue(i));

				if (pf.GetParameterType(i)   == "simple"
				||  pf.GetParameterType(i)   == "complexEnd"
				||  pf.GetParameterType(i+1) != "complexEnd")
				sw.Write(tf.Text.Substring(tf[j].End, tf[j+1].Begin-tf[j].End));
			}

			sw.Flush();
			fs.Close();
			return true;
		}
	}
}
