using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Ciba.Utils;
using Ciba.Utils.GenDocu;

namespace Ciba.Utils.GenDocuTest
{
	/// <summary>
	/// Summary description for frmMain.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private const short SW_SHOW = 5;

		[DllImport("shell32.dll", CharSet=CharSet.Unicode)]
		internal static extern int ShellExecute(int hwnd,
			string sOperation,  string sFile,
			string sParameters, string sDirectory, 
			short  nShowCmd);

		private const string csRegGenDocu = "Software\\Ciba\\Utils\\GenDocu";

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label    label1;
		private System.Windows.Forms.TextBox  txtTpl;
		private System.Windows.Forms.Button   bnTpl;
		private System.Windows.Forms.Label    label2;
		private System.Windows.Forms.TextBox  txtPar;
		private System.Windows.Forms.Button   bnPar;
		private System.Windows.Forms.Label    label3;
		private System.Windows.Forms.TextBox  txtOut;
		private System.Windows.Forms.Button   bnOut;
		private System.Windows.Forms.Label    label4;
		private System.Windows.Forms.TextBox  txtLog;
		private System.Windows.Forms.Button   bnLog;
		private System.Windows.Forms.Label    label5;
		private System.Windows.Forms.ComboBox cboEncoding;
		private System.Windows.Forms.Button   bnEditTpl;
		private System.Windows.Forms.Button   bnEditPar;
		private System.Windows.Forms.Button   bnGenOut;
		private System.Windows.Forms.Button   bnShowLog;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;

		private string msTplPath;
		private string msTplName;
		private string msParPath;
		private string msParName;
		private string msOutPath;
		private string msOutName;
		private string msLogPath;
		private System.Windows.Forms.CheckBox chkEncoding;
		private string msLogName;

		[STAThread] static void Main() 
		{
			Application.Run(new frmMain());
		}
		
		public frmMain()
		{
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null) components.Dispose();
			base.Dispose(disposing);
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			msTplName = "";
			msParName = "";

			RegistryKey k = Registry.CurrentUser.OpenSubKey(csRegGenDocu);
			if (k != null)
			{
				msTplPath = (string)k.GetValue("TemplatePath");
				msParPath = (string)k.GetValue("ParameterPath");
				msOutPath = (string)k.GetValue("OutputPath");
				msOutName = (string)k.GetValue("OutputName");
				msLogPath = (string)k.GetValue("LogPath");
				msLogName = (string)k.GetValue("LogName");
				k.Close();
			}
			else
			{
				msTplPath =  Directory.GetCurrentDirectory();
				msParPath =  Directory.GetCurrentDirectory();
				msOutPath =  Directory.GetCurrentDirectory();
				msOutName = "output.rtf";
				msLogPath =  Directory.GetCurrentDirectory();
				msLogName = "gendocu.log";
			}

			cboEncoding.DisplayMember = "EncodingName";
			cboEncoding.Items.Add(Encoding.GetEncoding(1252));
			cboEncoding.Items.Add(Encoding.GetEncoding(1250));
			cboEncoding.Items.Add(Encoding.GetEncoding(1251));
			cboEncoding.Items.Add(Encoding.GetEncoding(1253));
			cboEncoding.Items.Add(Encoding.GetEncoding(1254));
			cboEncoding.Items.Add(Encoding.GetEncoding(1255));
			cboEncoding.Items.Add(Encoding.GetEncoding(1256));
			cboEncoding.Items.Add(Encoding.GetEncoding(1257));
			cboEncoding.Items.Add(Encoding.GetEncoding(1258));
			cboEncoding.Items.Add(Encoding.GetEncoding(874));
			cboEncoding.Items.Add(Encoding.GetEncoding(65001));
			cboEncoding.Items.Add(Encoding.GetEncoding(1200));
		}

		private void frmMain_Closed(object sender, System.EventArgs e)
		{
			RegistryKey k;

			try { k = Registry.CurrentUser.CreateSubKey(csRegGenDocu); }
			catch(Exception) { k = null; }
			if (k != null)
			{
				k.SetValue("TemplatePath",  msTplPath);
				k.SetValue("ParameterPath", msParPath);
				k.SetValue("OutputPath",    msOutPath);
				k.SetValue("OutputName",    msOutName);
				k.SetValue("LogPath",       msLogPath);
				k.SetValue("LogName",       msLogName);
				k.Close();
			}
		}

		private void bnTpl_Click(object sender, System.EventArgs e)
		{
			dlgOpenFile.FileName = msTplPath + "\\.rtf";
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.DefaultExt = "rtf";
			dlgOpenFile.Filter = "RTF Files (*.rtf)|*.rtf|All Files (*.*)|*.*";
			if (dlgOpenFile.ShowDialog() == DialogResult.Cancel) return;

			int p = dlgOpenFile.FileName.LastIndexOf("\\");
			msTplPath = dlgOpenFile.FileName.Substring(0,  p);
			msTplName = dlgOpenFile.FileName.Substring(p + 1);
			txtTpl.Text = msTplPath + "\\" + msTplName;
			
			SetButtonState();
		}

		private void bnPar_Click(object sender, System.EventArgs e)
		{
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.FileName = msParPath + "\\.txt";
			dlgOpenFile.DefaultExt = "txt";
			dlgOpenFile.Filter = "Parameter Files (*.txt; *.xml)|*.txt;*.xml|"
			+ "Text Files (*.txt)|*.txt|XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
			if (dlgOpenFile.ShowDialog() == DialogResult.Cancel) return;

			int p = dlgOpenFile.FileName.LastIndexOf("\\");
			msParPath = dlgOpenFile.FileName.Substring(0,  p);
			msParName = dlgOpenFile.FileName.Substring(p + 1);
			txtPar.Text = msParPath + "\\" + msParName;

			SetEncoding();
			SetButtonState();
		}

		private void bnOut_Click(object sender, System.EventArgs e)
		{
			dlgOpenFile.CheckFileExists = false;
			dlgOpenFile.FileName = msOutPath + "\\.rtf";
			dlgOpenFile.DefaultExt = "rtf";
			dlgOpenFile.Filter = "RTF Files (*.rtf)|*.rtf|All Files (*.*)|*.*";
			if (dlgOpenFile.ShowDialog() == DialogResult.Cancel) return;

			int p = dlgOpenFile.FileName.LastIndexOf("\\");
			msOutPath = dlgOpenFile.FileName.Substring(0,  p);
			msOutName = dlgOpenFile.FileName.Substring(p + 1);
			txtOut.Text = msOutPath + "\\" + msOutName;
			
			SetButtonState();
		}

		private void bnLog_Click(object sender, System.EventArgs e)
		{
			dlgOpenFile.CheckFileExists = false;
			dlgOpenFile.FileName = msLogPath + "\\.log";
			dlgOpenFile.DefaultExt = "log";
			dlgOpenFile.Filter = "Log Files (*.log)|*.log|All Files (*.*)|*.*";
			if (dlgOpenFile.ShowDialog() == DialogResult.Cancel) return;

			int p = dlgOpenFile.FileName.LastIndexOf("\\");
			msLogPath = dlgOpenFile.FileName.Substring(0,  p);
			msLogName = dlgOpenFile.FileName.Substring(p + 1);
			txtLog.Text = msLogPath + "\\" + msLogName;
			
			SetButtonState();
		}

		private void txtTpl_TextChanged(object sender, System.EventArgs e)
		{
			string s = Directory.GetCurrentDirectory();
			int    p = txtTpl.Text.LastIndexOf("\\");
			msTplPath = p < 0 ? s :           txtTpl.Text.Substring(0,  p);
			msTplName = p < 0 ? txtTpl.Text : txtTpl.Text.Substring(p + 1);
			
			SetButtonState();
		}

		private void txtPar_TextChanged(object sender, System.EventArgs e)
		{
			string s = Directory.GetCurrentDirectory();
			int    p = txtPar.Text.LastIndexOf("\\");
			msParPath = p < 0 ? s :           txtPar.Text.Substring(0,  p);
			msParName = p < 0 ? txtPar.Text : txtPar.Text.Substring(p + 1);
			
			SetButtonState();
		}

		private void txtOut_TextChanged(object sender, System.EventArgs e)
		{
			string s = Directory.GetCurrentDirectory();
			int    p = txtOut.Text.LastIndexOf("\\");
			msOutPath = p < 0 ? s :           txtOut.Text.Substring(0,  p);
			msOutName = p < 0 ? txtOut.Text : txtOut.Text.Substring(p + 1);
			
			SetButtonState();
		}

		private void txtLog_TextChanged(object sender, System.EventArgs e)
		{
			string s = Directory.GetCurrentDirectory();
			int    p = txtLog.Text.LastIndexOf("\\");
			msLogPath = p < 0 ? s :           txtLog.Text.Substring(0,  p);
			msLogName = p < 0 ? txtLog.Text : txtLog.Text.Substring(p + 1);
			
			SetButtonState();
		}

		private void bnEditTpl_Click(object sender, System.EventArgs e)
		{
			int r = ShellExecute(Handle.ToInt32(), "open", 
				msTplPath + "\\" + msTplName, null, msTplPath, SW_SHOW);
		}

		private void bnEditPar_Click(object sender, System.EventArgs e)
		{
			int r = ShellExecute(Handle.ToInt32(), "open", 
				msParPath + "\\" + msParName, null, msParPath, SW_SHOW);
		}

		private void bnGenOut_Click(object sender, System.EventArgs e)
		{
			Merger m = new Merger();

			m.TemplateFileName  = txtTpl.Text;
			m.ParameterFileName = txtPar.Text;
			if (!chkEncoding.Checked)
			m.ParameterEncoding = null; else
			m.ParameterEncoding = (Encoding)cboEncoding.SelectedItem;
			m.OutputFileName    = txtOut.Text;
			m.LogFileName       = txtLog.Text;
			
			if (!m.Generate()) return;

			int r = ShellExecute(Handle.ToInt32(), "open", 
				msOutPath + "\\" + msOutName, null, msOutPath, SW_SHOW);
		}

		private void bnShowLog_Click(object sender, System.EventArgs e)
		{
			int r = ShellExecute(Handle.ToInt32(), "open", 
				msLogPath + "\\" + msLogName, null, msLogPath, SW_SHOW);
		}

		private void SetEncoding()
		{
			if (txtPar.Text.EndsWith(".xml") || txtPar.Text.EndsWith(".XML"))
			{
				cboEncoding.SelectedIndex = -1;
				cboEncoding.Enabled = false;
				return;
			}
			try
			{
				FileStream fs = new FileStream(txtPar.Text, FileMode.Open, FileAccess.Read);
				if (fs.ReadByte() == 255 && fs.ReadByte() == 254)
				cboEncoding.SelectedIndex = cboEncoding.FindStringExact("Unicode"); else
				cboEncoding.SelectedIndex = cboEncoding.FindString("Western European");
				cboEncoding.Enabled = true;
			}
			catch(Exception x)
			{
				MessageBox.Show("Parameter file is not accessible.\r\n" + x.Message,
					"GenDocuTest", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtPar.Text = "";
			}
		}

		private void SetButtonState()
		{
			if (txtTpl.Text != "") bnEditTpl.Enabled = true;
			if (txtPar.Text != "") bnEditPar.Enabled = true;
			if (txtLog.Text != "") bnShowLog.Enabled = true;
			if (txtTpl.Text != ""
			&&  txtPar.Text != ""
			&&  txtOut.Text != "") bnGenOut.Enabled  = true;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.label1 = new System.Windows.Forms.Label();
			this.txtTpl = new System.Windows.Forms.TextBox();
			this.txtPar = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.bnTpl = new System.Windows.Forms.Button();
			this.bnPar = new System.Windows.Forms.Button();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.bnOut = new System.Windows.Forms.Button();
			this.txtOut = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.bnLog = new System.Windows.Forms.Button();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.bnEditTpl = new System.Windows.Forms.Button();
			this.bnEditPar = new System.Windows.Forms.Button();
			this.bnGenOut = new System.Windows.Forms.Button();
			this.bnShowLog = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.cboEncoding = new System.Windows.Forms.ComboBox();
			this.chkEncoding = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "&Template:";
			// 
			// txtTpl
			// 
			this.txtTpl.Location = new System.Drawing.Point(82, 12);
			this.txtTpl.Name = "txtTpl";
			this.txtTpl.Size = new System.Drawing.Size(248, 20);
			this.txtTpl.TabIndex = 1;
			this.txtTpl.Text = "";
			this.txtTpl.TextChanged += new System.EventHandler(this.txtTpl_TextChanged);
			// 
			// txtPar
			// 
			this.txtPar.Location = new System.Drawing.Point(82, 44);
			this.txtPar.Name = "txtPar";
			this.txtPar.Size = new System.Drawing.Size(248, 20);
			this.txtPar.TabIndex = 5;
			this.txtPar.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "&Parameter:";
			// 
			// bnTpl
			// 
			this.bnTpl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bnTpl.Location = new System.Drawing.Point(336, 12);
			this.bnTpl.Name = "bnTpl";
			this.bnTpl.Size = new System.Drawing.Size(28, 20);
			this.bnTpl.TabIndex = 2;
			this.bnTpl.Text = "...";
			this.bnTpl.Click += new System.EventHandler(this.bnTpl_Click);
			// 
			// bnPar
			// 
			this.bnPar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bnPar.Location = new System.Drawing.Point(336, 44);
			this.bnPar.Name = "bnPar";
			this.bnPar.Size = new System.Drawing.Size(28, 20);
			this.bnPar.TabIndex = 6;
			this.bnPar.Text = "...";
			this.bnPar.Click += new System.EventHandler(this.bnPar_Click);
			// 
			// bnOut
			// 
			this.bnOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bnOut.Location = new System.Drawing.Point(336, 134);
			this.bnOut.Name = "bnOut";
			this.bnOut.Size = new System.Drawing.Size(28, 20);
			this.bnOut.TabIndex = 12;
			this.bnOut.Text = "...";
			this.bnOut.Click += new System.EventHandler(this.bnOut_Click);
			// 
			// txtOut
			// 
			this.txtOut.Location = new System.Drawing.Point(82, 134);
			this.txtOut.Name = "txtOut";
			this.txtOut.Size = new System.Drawing.Size(248, 20);
			this.txtOut.TabIndex = 11;
			this.txtOut.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 138);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "&Output:";
			// 
			// bnLog
			// 
			this.bnLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.bnLog.Location = new System.Drawing.Point(336, 166);
			this.bnLog.Name = "bnLog";
			this.bnLog.Size = new System.Drawing.Size(28, 20);
			this.bnLog.TabIndex = 16;
			this.bnLog.Text = "...";
			this.bnLog.Click += new System.EventHandler(this.bnLog_Click);
			// 
			// txtLog
			// 
			this.txtLog.Location = new System.Drawing.Point(82, 166);
			this.txtLog.Name = "txtLog";
			this.txtLog.Size = new System.Drawing.Size(248, 20);
			this.txtLog.TabIndex = 15;
			this.txtLog.Text = "";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 170);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "[ &Log ]:";
			// 
			// bnEditTpl
			// 
			this.bnEditTpl.Enabled = false;
			this.bnEditTpl.Location = new System.Drawing.Point(388, 12);
			this.bnEditTpl.Name = "bnEditTpl";
			this.bnEditTpl.Size = new System.Drawing.Size(110, 20);
			this.bnEditTpl.TabIndex = 3;
			this.bnEditTpl.Text = "&Edit Template";
			this.bnEditTpl.Click += new System.EventHandler(this.bnEditTpl_Click);
			// 
			// bnEditPar
			// 
			this.bnEditPar.Enabled = false;
			this.bnEditPar.Location = new System.Drawing.Point(388, 44);
			this.bnEditPar.Name = "bnEditPar";
			this.bnEditPar.Size = new System.Drawing.Size(110, 20);
			this.bnEditPar.TabIndex = 7;
			this.bnEditPar.Text = "E&dit Parameter";
			this.bnEditPar.Click += new System.EventHandler(this.bnEditPar_Click);
			// 
			// bnGenOut
			// 
			this.bnGenOut.Enabled = false;
			this.bnGenOut.Location = new System.Drawing.Point(388, 134);
			this.bnGenOut.Name = "bnGenOut";
			this.bnGenOut.Size = new System.Drawing.Size(110, 20);
			this.bnGenOut.TabIndex = 13;
			this.bnGenOut.Text = "&Generate Output";
			this.bnGenOut.Click += new System.EventHandler(this.bnGenOut_Click);
			// 
			// bnShowLog
			// 
			this.bnShowLog.Enabled = false;
			this.bnShowLog.Location = new System.Drawing.Point(388, 166);
			this.bnShowLog.Name = "bnShowLog";
			this.bnShowLog.Size = new System.Drawing.Size(110, 20);
			this.bnShowLog.TabIndex = 17;
			this.bnShowLog.Text = "&Show Log";
			this.bnShowLog.Click += new System.EventHandler(this.bnShowLog_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 80);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "En&coding:";
			// 
			// cboEncoding
			// 
			this.cboEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboEncoding.Enabled = false;
			this.cboEncoding.Location = new System.Drawing.Point(82, 76);
			this.cboEncoding.Name = "cboEncoding";
			this.cboEncoding.Size = new System.Drawing.Size(282, 21);
			this.cboEncoding.TabIndex = 9;
			// 
			// chkEncoding
			// 
			this.chkEncoding.Checked = true;
			this.chkEncoding.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkEncoding.Location = new System.Drawing.Point(388, 78);
			this.chkEncoding.Name = "chkEncoding";
			this.chkEncoding.Size = new System.Drawing.Size(108, 18);
			this.chkEncoding.TabIndex = 18;
			this.chkEncoding.Text = "&Use Encoding";
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(510, 203);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.chkEncoding,
																		  this.cboEncoding,
																		  this.label5,
																		  this.label4,
																		  this.label3,
																		  this.label2,
																		  this.label1,
																		  this.bnShowLog,
																		  this.bnGenOut,
																		  this.bnEditPar,
																		  this.bnEditTpl,
																		  this.bnLog,
																		  this.txtLog,
																		  this.bnOut,
																		  this.txtOut,
																		  this.bnPar,
																		  this.bnTpl,
																		  this.txtPar,
																		  this.txtTpl});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GenDocuTest";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.Closed += new System.EventHandler(this.frmMain_Closed);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
