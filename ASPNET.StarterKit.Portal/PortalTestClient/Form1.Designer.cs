namespace Portal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAnnouncements = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonContacts = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLinks = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAnnouncements,
            this.toolStripButtonContacts,
            this.toolStripButtonLinks});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(824, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAnnouncements
            // 
            this.toolStripButtonAnnouncements.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAnnouncements.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAnnouncements.Image")));
            this.toolStripButtonAnnouncements.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAnnouncements.Name = "toolStripButtonAnnouncements";
            this.toolStripButtonAnnouncements.Size = new System.Drawing.Size(99, 22);
            this.toolStripButtonAnnouncements.Text = "Announcements";
            this.toolStripButtonAnnouncements.Click += new System.EventHandler(this.toolStripButtonAnnouncements_Click);
            // 
            // toolStripButtonContacts
            // 
            this.toolStripButtonContacts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonContacts.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonContacts.Image")));
            this.toolStripButtonContacts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonContacts.Name = "toolStripButtonContacts";
            this.toolStripButtonContacts.Size = new System.Drawing.Size(58, 22);
            this.toolStripButtonContacts.Text = "Contacts";
            this.toolStripButtonContacts.Click += new System.EventHandler(this.toolStripButtonContacts_Click);
            // 
            // toolStripButtonLinks
            // 
            this.toolStripButtonLinks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonLinks.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLinks.Image")));
            this.toolStripButtonLinks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLinks.Name = "toolStripButtonLinks";
            this.toolStripButtonLinks.Size = new System.Drawing.Size(38, 22);
            this.toolStripButtonLinks.Text = "Links";
            this.toolStripButtonLinks.Click += new System.EventHandler(this.toolStripButtonLinks_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(824, 447);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewResults);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(816, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResults.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.Size = new System.Drawing.Size(810, 415);
            this.dataGridViewResults.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 472);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.ToolStripButton toolStripButtonAnnouncements;
        private System.Windows.Forms.ToolStripButton toolStripButtonContacts;
        private System.Windows.Forms.ToolStripButton toolStripButtonLinks;
    }
}

