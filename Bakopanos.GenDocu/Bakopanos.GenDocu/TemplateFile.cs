using System;
using System.Collections;
using System.IO;

namespace Ciba.Utils.GenDocu
{
	public class ReplaceInfo
	{
		public string Symbol    = "";
		public bool   Complex   = false;
		public int    Begin     = 0;
		public int    End       = 0;
		public string InnerText = "";

		public ReplaceInfo() {}
	}

	/// <summary>
	/// The class TemplateFile allows to parse a RTF template file
	/// containing placeholders to be replaced with actual values.
	/// The result of the parsing process is a dictionary storing the
	/// placeholder names along with their positions within the document.
	/// 
	/// template           ::= («any_text_string»|«simple_var»|«complex_var»|«remove_nl»)*
	/// 
	/// simple_var         ::= '&'«variable_name»';'
	/// 
	/// complex_var        ::= '&&'«variable_name»';'«template»'&&&'«variable_name»';'
	///                    
	/// remove_nl          ::= '&&&&'«variable_name»';'
	/// 
	/// variable_name      ::= «any_text_string»
	/// 
	/// any_text_string    ::= «any_printable_char»*
	/// 
	/// any_printable_char ::= {32 .. 255}
	/// 
	/// </summary>
	public class TemplateFile
	{
		#region Lexical Analysis
		private enum TokenType
		{
			ttUnknown   = 0,
			ttSimple    = 1,
			ttCompOpen  = 2,
			ttCompClose = 3,
			ttRemove    = 4,
			ttString    = 5
		}

		private class Token
		{
			int       mnPos;
			int       mnPPs;
			string    msText;
			string    msVal;
			TokenType mnType;

			public Token()
			{
				mnPos  = 0;
				mnPPs  = 0;
				msText = "";
				msVal  = "";
				mnType = TokenType.ttUnknown;
			}

			public Token(string sText)
			{
				mnPos  = 0;
				mnPPs  = 0;
				msText = sText;
				msVal  = "";
				mnType = TokenType.ttUnknown;
			}

			public string Text
			{ 
				get { return msText; }
				set 
				{ 
					mnPos  = 0;
					mnPPs  = 0;
					msText = value;
					msVal  = "";
					mnType = TokenType.ttUnknown;
				}
			}

			public int       Pos      { get { return mnPos;  } }
			public int       PriorPos { get { return mnPPs;  } }
			public string    Value    { get { return msVal;  } }
			public TokenType Type     { get { return mnType; } }

			public bool Next()
			{
				char[] tc = {'&',';'};
				int c;
				int e;

				msVal = "";
			    mnPPs = mnPos;  mnType = TokenType.ttString;    if (!GetChar(out c)) return false;
				if (c == '&') { mnType = TokenType.ttSimple;    if (!GetChar(out c)) return false; }
				if (c == '&') { mnType = TokenType.ttCompOpen;  if (!GetChar(out c)) return false; }
				if (c == '&') { mnType = TokenType.ttCompClose; if (!GetChar(out c)) return false; }
				if (c == '&') { mnType = TokenType.ttRemove;    if (!GetChar(out c)) return false; }

				e = (mnType == TokenType.ttString ? '&' : ';');
				while (c != e && GetChar(out c)) ;

				if (mnType == TokenType.ttString) { UngetChar();
				msVal = msText.Substring(mnPPs, mnPos-mnPPs); } else
				msVal = msText.Substring(mnPPs, mnPos-mnPPs).Trim(tc);

				return (c == e);
			}

			public bool GetChar(out int c)
			{
				c = -1; if (mnPos >= msText.Length) return false;
				c =  msText[mnPos]; mnPos++;		return true;
			}

			public void UngetChar()
			{
				mnPos--;
			}
		}
		#endregion

		private LogFile   mLog   = null;
		private Token     mToken = null;
		private Stack     mStack = null;
		private ArrayList mInfo  = null;

		public TemplateFile()
		{
			mToken = new Token();
			mStack = new Stack();
			mInfo  = new ArrayList();
		}

		public LogFile LogFile
		{
			get { return mLog;  }
			set { mLog = value; }
		}

		public string Text
		{
			get { return mToken.Text; }
		}

		public int Count
		{
			get { return mInfo.Count; }
		}

		public ReplaceInfo this[int i]
		{
			get { return (ReplaceInfo)mInfo[i]; }
		}

		public bool Load(string sFileName)
		{
			FileStream   fs = new FileStream(sFileName, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(fs);
			mToken.Text = sr.ReadToEnd();
			fs.Close();

			ReplaceInfo en;

			while (mToken.Next())
			{
				switch (mToken.Type)
				{
				case TokenType.ttString:
					break;

				case TokenType.ttSimple:
					en = new ReplaceInfo();
					en.Symbol  = mToken.Value;
					en.Begin   = mToken.PriorPos;
					en.End     = mToken.Pos;
					mInfo.Add(en);
					break;

				case TokenType.ttCompOpen:
					en = new ReplaceInfo();
					en.Symbol  = mToken.Value;
					en.Begin   = mToken.PriorPos;
					en.End     = mToken.Pos;
					mInfo.Add(en);
					break;

				case TokenType.ttCompClose:
					en = new ReplaceInfo();
					en.Symbol  = mToken.Value + ".";
					en.Begin   = mToken.PriorPos;
					en.End     = mToken.Pos;
					mInfo.Add(en);
					break;

				case TokenType.ttRemove:
					break;
				}
			}

			// Sentinel
			en = new ReplaceInfo();
			en.Symbol  = ":";
			en.Begin   = mToken.Text.Length;
			en.End     = mToken.Text.Length;
			mInfo.Add(en);

			PrintList("d:\\list.txt");
			return true;
		}

		public int Find(string sName, int nPos)
		{
			for (int i = nPos; i < mInfo.Count; i++)
			if  (((ReplaceInfo)mInfo[i]).Symbol == sName) return i;
			return -1;
		}

		private void PrintList(string sFileName)
		{
			FileStream   fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);

			for (int i = 0; i < mInfo.Count; i++)
			{
				ReplaceInfo ri = (ReplaceInfo)mInfo[i];
				string s = ri.Symbol + (ri.Complex ? "*" : "")
					+ " (" + ri.Begin.ToString() + ", " + ri.End.ToString() + ")";
				sw.WriteLine(s);
			}

			sw.Flush();
			fs.Close();
		}
	}
}
