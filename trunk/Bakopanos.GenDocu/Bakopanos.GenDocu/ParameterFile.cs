using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;

namespace Ciba.Utils.GenDocu
{
	/// <summary>
	/// The class ParameterFile allows to parse and store values
	/// for placeholders following the (EBNF) syntax given below.
	/// 
	/// parameters         ::= («simple_var»|«complex_var»|«comment»)*
	/// 
	/// simple_var         ::= '&'«variable_name»';='«parameter_value»«newline»
	/// 
	/// complex_var        ::= '&&'«variable_name»';='«parameter_value»«newline»
	///                         «parameters»
	///                        '&&&'«variable_name»';'«newline»
	///                    
	/// comment            ::= «any_text_string»«newline»
	/// 
	/// variable_name      ::= «any_text_string»
	/// 
	/// parameter_value    ::= «any_text_string»
	/// 
	/// any_text_string    ::= «any_printable_char»*
	/// 
	/// newline            ::= [CR]LF
	/// 
	/// any_printable_char ::= {32 .. 255}
	/// 
	/// The values are taken from a text stream containing ANSI or UNICODE
	/// (UTF-8/UTF-16) characters.
	/// </summary>
	public class ParameterFile
	{
		#region Lexical Analysis
		private enum TokenType
		{
			ttUnknown   = 0,
			ttSimple    = 1,
			ttCompOpen  = 2,
			ttCompClose = 3,
			ttComment   = 4
		}

		private class Token
		{
			int       mnPos;
			string    msText;
			string    msName;
			string    msVal;
			TokenType mnType;

			public Token()
			{
				mnPos  = 0;
				msText = "";
				msName = "";
				msVal  = "";
				mnType = TokenType.ttUnknown;
			}

			public Token(string sText)
			{
				mnPos  = 0;
				msText = sText;
				msName = "";
				msVal  = "";
				mnType = TokenType.ttUnknown;
			}

			public string    Text
			{ 
				get { return msText; }
				set 
				{ 
					mnPos  = 0;
					msText = value;
					msName = "";
					msVal  = "";
					mnType = TokenType.ttUnknown;
				}
			}

			public int       Pos   { get { return mnPos;  } }
			public string    Name  { get { return msName; } }
			public string    Value { get { return msVal;  } }
			public TokenType Type  { get { return mnType; } }

			public bool Next()
			{
				string s = "";
				int    p =  0;
				msName   = "";
				msVal    = "";
				mnType   = TokenType.ttUnknown;
				if (!GetLine(out s)) return false;

				s = s.Trim();
				if (s.StartsWith("&&&") && s.EndsWith(";"))
				{
					msName = s.Substring(3, s.Length-4);
					mnType = TokenType.ttCompClose;
				}
				else if (s.StartsWith("&&"))
				{
					if ((p = s.IndexOf(";"))    < 0) return false;
					msName = s.Substring(2, p-2);
					if ((p = s.IndexOf("=", p)) < 0) return false;
					msVal  = s.Substring(p+1);
					mnType = TokenType.ttCompOpen;
				}
				else if (s.StartsWith("&"))
				{
					if ((p = s.IndexOf(";"))    < 0) return false;
					msName = s.Substring(1, p-1);
					if ((p = s.IndexOf("=", p)) < 0) return false;
					msVal  = s.Substring(p+1);
					mnType = TokenType.ttSimple;
				}
				else
				{
					msVal  = s;
					mnType = TokenType.ttComment;
				}

				return true;
			}

			public bool GetLine(out string s)
			{
				int p = mnPos; s = "";
				while (mnPos < msText.Length && !IsEOL(msText[mnPos])) mnPos++;
				if    (mnPos == p) return false;

				s = msText.Substring(p, mnPos-p);
				if (msText[mnPos] == '\r') mnPos++;
				if (msText[mnPos] == '\n') mnPos++;
				return true;
			}

			public bool IsEOL(int c)
			{
				return (c == '\r' || c == '\n');
			}
		}
		#endregion

		XmlDocument mDoc   = null;
		Token       mToken = null;
		LogFile     mLog   = null;
		ArrayList   mList  = null;

		public ParameterFile()
		{
			mDoc   = new XmlDocument();
			mDoc.AppendChild(mDoc.CreateElement("gendocu"));
			mToken = new Token();
			mList  = new ArrayList(800);
		}

		public LogFile LogFile
		{
			get { return mLog;  }
			set { mLog = value; }
		}

		public XmlDocument Document
		{ 
			get { return mDoc; }
		}

		public int Count
		{ 
			get { return mList.Count; }
		}

		public XmlNode this[int i]
		{
			get { return (XmlNode)mList[i]; }
		}

		public string GetParameterName(int i)
		{
			XmlNode nod = (XmlNode)mList[i];
			if (nod.Attributes == null) return "";
			XmlNode att = nod.Attributes.GetNamedItem("name");
			if (att == null) return "";
			return att.Value;
		}

		public string GetParameterValue(int i)
		{
			XmlNode nod = (XmlNode)mList[i];
			return  EncodeRTF(nod.InnerText);
		}

		public string GetParameterType(int i)
		{
			XmlNode nod = (XmlNode)mList[i];
			if (nod.Attributes == null) return "";
			XmlNode att = nod.Attributes.GetNamedItem("type");
			if (att == null) return "";
			return att.Value;
		}

		public bool Load(string sFileName)
		{
			try
			{
				FileStream fs = new FileStream(sFileName, FileMode.Open, FileAccess.Read);
				Encoding   en = Encoding.GetEncoding(1252); // Windows Western European
				if (fs.ReadByte() == 255 && fs.ReadByte() == 254) en = Encoding.Unicode;
				fs.Seek(0, SeekOrigin.Begin);
				mToken.Text = new StreamReader(fs, en).ReadToEnd();
				fs.Close();
			}
			catch(Exception)
			{
				return false;
			}

			return Parse() && Serialize();
		}

		public bool Load(string sFileName, Encoding encoding)
		{
			try
			{
				Encoding en = (encoding == null ? Encoding.GetEncoding(1252) : encoding);
				FileStream fs = new FileStream(sFileName, FileMode.Open, FileAccess.Read);
				mToken.Text = new StreamReader(fs, en).ReadToEnd();
				fs.Close();
			}
			catch(Exception)
			{
				return false;
			}

			return Parse() && Serialize();
		}

		public bool LoadXml(string sFileName)
		{
			try
			{
				mDoc.Load(sFileName);
			}
			catch(Exception)
			{
				return false;
			}

			return Serialize();
		}

		private bool Parse()
		{
			XmlNode   nod = null;
			XmlNode   prt = mDoc.DocumentElement;
			Stack     stk = new Stack();

			mLog.Write("Parsing parameter file ...");

			while (mToken.Next())
			{
				switch (mToken.Type)
				{
				case TokenType.ttSimple:
					nod = mDoc.CreateElement("simple");
					AddAttribute(nod, "name", mToken.Name);
					AddAttribute(nod, "type", "simple");
					nod.InnerText = mToken.Value;
					prt.AppendChild(nod);
					break;

				case TokenType.ttCompOpen:
					if (!IsTopName(stk, mToken.Name))
					{
						nod = mDoc.CreateElement("complex");
						AddAttribute(nod, "name", mToken.Name);
						AddAttribute(nod, "type", "complexBegin");
						prt = prt.AppendChild(nod); stk.Push(prt);
						mLog.Write("complex var " + mToken.Name + " opened");
					}
					else
					{
						mLog.Write("complex var " + mToken.Name + " reopened");
					}
					
					nod = mDoc.CreateElement("element");
					AddAttribute(nod, "name", mToken.Name);
					AddAttribute(nod, "type", "complex");
					nod.InnerText = mToken.Value;
					prt.AppendChild(nod);
					break;

				case TokenType.ttCompClose:
					if (IsTopName(stk, mToken.Name))
					{
						nod = mDoc.CreateElement("sentinel");
						AddAttribute(nod, "name", mToken.Name + ".");
						AddAttribute(nod, "type", "complexEnd");
						prt.AppendChild(nod);

						prt = prt.ParentNode; stk.Pop();
						mLog.Write("complex var " + mToken.Name + " closed");
					}
					else
					{
						mLog.Write("Warning: end of complex var "
						+ mToken.Name + " found without beginning");
					}
					break;
				}
			}

			if (stk.Count > 0)
			{
				mLog.Write("unexpected end of parameter file expanding complex var");
				mLog.Write("generation of document aborted!");
				return false;
			}

			return true;
		}

		private bool IsTopName(Stack stk, string sName)
		{
			if (stk.Count < 1) return false;

			XmlNode nod = (XmlNode)stk.Peek();
			XmlNode att = nod.Attributes.GetNamedItem("name");
			if (att == null)        return false;
			if (att.Value != sName) return false;

			return true;
		}

		private void AddAttribute(XmlNode nod, string sName, string sValue)
		{
			XmlAttribute att = mDoc.CreateAttribute(sName);
			att.Value = sValue;
			nod.Attributes.Append(att);
		}

		private bool Serialize()
		{
			Stack stk = new Stack();
			mList.Clear();
			mList.Capacity = 800;

			XmlNode nod = mDoc.DocumentElement;
			for (int i = nod.ChildNodes.Count-1; i >= 0; i--) stk.Push(nod.ChildNodes[i]);

			while (stk.Count > 0)
			{
				nod = (XmlNode)stk.Pop();
				if (nod.NodeType == XmlNodeType.Text)
				{
					continue;
				}
				else if (nod.Name == "simple" || nod.Name == "element" || nod.Name == "sentinel")
				{
					mList.Add(nod);
				}
				else if (nod.Name == "complex")
				{
					for (int i = nod.ChildNodes.Count-1; i >= 0; i--) stk.Push(nod.ChildNodes[i]);
				}
			}

			return (mList.Count > 0);
		}

		/*-EncodeRTF----------------------------------------------------------------------------*
		   The function encodes UNICODE character strings for the RTF format. The encoded
		   sequence has the form \u<integer><ascii character for replacement>. An RTF reader
		   will output the glyph denoted by the UNICODE encoding sequence if possible,
		   otherwise it will output the ascii character. Here the replacement character is
		   always the underscore sign (_).
		*--------------------------------------------------------------------------------------*/

		private string EncodeRTF(string str)
		{
			const char BLANK = ' ';
			const char SHARP = '#';
			const char AMP   = '&';
			const char ZERO  = '0';
			const char SEMIC = ';';

			string buf = "";
			int    i;
			int    n;

			for (i = 0; i < str.Length; i++)
			{
				int c = str[i];
				int d = (i+1 < str.Length ? str[i+1] : BLANK);

				if (c == AMP && d == SHARP)
				{
					for (n = 0, i += 2; i < str.Length && str[i] != SEMIC; i++)
						 n = 10*n + (str[i]-ZERO);

					buf += ("\\u" + n.ToString() + "_");
				}
				else if (c < 128)
				{
					buf += (char)c;
				}
				else
				{
					buf += ("\\u" + c.ToString() + "_");
				}
			}

			return buf;
		}
	}
}
