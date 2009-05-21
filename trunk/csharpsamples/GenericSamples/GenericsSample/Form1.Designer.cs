namespace GenericsSample
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label3 = new System.Windows.Forms.Label();
			this.exceptionLog = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.IterateUnsafeListMs = new System.Windows.Forms.TextBox();
			this.LoadUnsafeListMs = new System.Windows.Forms.TextBox();
			this.iterateSafeListMs = new System.Windows.Forms.TextBox();
			this.LoadSafeListMs = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.startSpeedTest = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.AddUnsafeList = new System.Windows.Forms.Button();
			this.AddSafeList = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.safeObjectList = new System.Windows.Forms.CheckedListBox();
			this.unsafeObjectList = new System.Windows.Forms.CheckedListBox();
			this.masterObjectList = new System.Windows.Forms.CheckedListBox();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(265, 128);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Safe List";
			// 
			// exceptionLog
			// 
			this.exceptionLog.Location = new System.Drawing.Point(14, 20);
			this.exceptionLog.Multiline = true;
			this.exceptionLog.Name = "exceptionLog";
			this.exceptionLog.Size = new System.Drawing.Size(446, 111);
			this.exceptionLog.TabIndex = 8;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.exceptionLog);
			this.groupBox2.Location = new System.Drawing.Point(12, 412);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(475, 140);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Exception Log";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.IterateUnsafeListMs);
			this.groupBox3.Controls.Add(this.LoadUnsafeListMs);
			this.groupBox3.Controls.Add(this.iterateSafeListMs);
			this.groupBox3.Controls.Add(this.LoadSafeListMs);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.startSpeedTest);
			this.groupBox3.Location = new System.Drawing.Point(12, 280);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(475, 126);
			this.groupBox3.TabIndex = 13;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Speed Test";
			// 
			// IterateUnsafeListMs
			// 
			this.IterateUnsafeListMs.Location = new System.Drawing.Point(378, 55);
			this.IterateUnsafeListMs.Name = "IterateUnsafeListMs";
			this.IterateUnsafeListMs.Size = new System.Drawing.Size(51, 20);
			this.IterateUnsafeListMs.TabIndex = 8;
			// 
			// LoadUnsafeListMs
			// 
			this.LoadUnsafeListMs.Location = new System.Drawing.Point(378, 25);
			this.LoadUnsafeListMs.Name = "LoadUnsafeListMs";
			this.LoadUnsafeListMs.Size = new System.Drawing.Size(51, 20);
			this.LoadUnsafeListMs.TabIndex = 7;
			// 
			// iterateSafeListMs
			// 
			this.iterateSafeListMs.Location = new System.Drawing.Point(141, 55);
			this.iterateSafeListMs.Name = "iterateSafeListMs";
			this.iterateSafeListMs.Size = new System.Drawing.Size(51, 20);
			this.iterateSafeListMs.TabIndex = 6;
			// 
			// LoadSafeListMs
			// 
			this.LoadSafeListMs.Location = new System.Drawing.Point(141, 25);
			this.LoadSafeListMs.Name = "LoadSafeListMs";
			this.LoadSafeListMs.Size = new System.Drawing.Size(51, 20);
			this.LoadSafeListMs.TabIndex = 5;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(261, 58);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(111, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = "Iterate Unsafe List(ms):";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(267, 28);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(105, 13);
			this.label6.TabIndex = 3;
			this.label6.Text = "Load Unsafe List(ms):";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(33, 58);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Iterate Safe List(ms):";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(39, 28);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Load Safe List(ms):";
			// 
			// startSpeedTest
			// 
			this.startSpeedTest.Location = new System.Drawing.Point(174, 92);
			this.startSpeedTest.Name = "startSpeedTest";
			this.startSpeedTest.Size = new System.Drawing.Size(117, 23);
			this.startSpeedTest.TabIndex = 0;
			this.startSpeedTest.Text = "Start Speed test";
			this.startSpeedTest.Click += new System.EventHandler(this.startSpeedTest_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.AddUnsafeList);
			this.groupBox1.Controls.Add(this.AddSafeList);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.safeObjectList);
			this.groupBox1.Controls.Add(this.unsafeObjectList);
			this.groupBox1.Controls.Add(this.masterObjectList);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(475, 262);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Type Safety";
			// 
			// AddUnsafeList
			// 
			this.AddUnsafeList.Location = new System.Drawing.Point(141, 228);
			this.AddUnsafeList.Name = "AddUnsafeList";
			this.AddUnsafeList.Size = new System.Drawing.Size(118, 23);
			this.AddUnsafeList.TabIndex = 14;
			this.AddUnsafeList.Text = "Add to unsafe List";
			this.AddUnsafeList.Click += new System.EventHandler(this.AddUnsafeList_Click);
			// 
			// AddSafeList
			// 
			this.AddSafeList.Location = new System.Drawing.Point(7, 228);
			this.AddSafeList.Name = "AddSafeList";
			this.AddSafeList.Size = new System.Drawing.Size(116, 23);
			this.AddSafeList.TabIndex = 13;
			this.AddSafeList.Text = "Add to Safe List";
			this.AddSafeList.Click += new System.EventHandler(this.AddSafeList_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(265, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Unsafe List";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(6, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Master List";
			// 
			// safeObjectList
			// 
			this.safeObjectList.FormattingEnabled = true;
			this.safeObjectList.Location = new System.Drawing.Point(265, 144);
			this.safeObjectList.Name = "safeObjectList";
			this.safeObjectList.Size = new System.Drawing.Size(195, 72);
			this.safeObjectList.TabIndex = 10;
			// 
			// unsafeObjectList
			// 
			this.unsafeObjectList.FormattingEnabled = true;
			this.unsafeObjectList.Location = new System.Drawing.Point(265, 42);
			this.unsafeObjectList.Name = "unsafeObjectList";
			this.unsafeObjectList.Size = new System.Drawing.Size(195, 72);
			this.unsafeObjectList.TabIndex = 9;
			// 
			// masterObjectList
			// 
			this.masterObjectList.CheckOnClick = true;
			this.masterObjectList.FormattingEnabled = true;
			this.masterObjectList.Location = new System.Drawing.Point(6, 42);
			this.masterObjectList.Name = "masterObjectList";
			this.masterObjectList.Size = new System.Drawing.Size(253, 174);
			this.masterObjectList.TabIndex = 8;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(497, 564);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Generics Sample";
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox exceptionLog;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button startSpeedTest;
		private System.Windows.Forms.TextBox IterateUnsafeListMs;
		private System.Windows.Forms.TextBox LoadUnsafeListMs;
		private System.Windows.Forms.TextBox iterateSafeListMs;
		private System.Windows.Forms.TextBox LoadSafeListMs;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button AddUnsafeList;
		private System.Windows.Forms.Button AddSafeList;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckedListBox safeObjectList;
		private System.Windows.Forms.CheckedListBox unsafeObjectList;
		private System.Windows.Forms.CheckedListBox masterObjectList;
	}
}

