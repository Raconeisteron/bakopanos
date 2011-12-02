namespace Portal
{
    partial class MainForm
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
            this.buttonCreateAnnouncement = new System.Windows.Forms.Button();
            this.buttonCreateLink = new System.Windows.Forms.Button();
            this.buttonCreateContact = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonAnnouncements = new System.Windows.Forms.Button();
            this.buttonLinks = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCreateAnnouncement
            // 
            this.buttonCreateAnnouncement.Location = new System.Drawing.Point(12, 76);
            this.buttonCreateAnnouncement.Name = "buttonCreateAnnouncement";
            this.buttonCreateAnnouncement.Size = new System.Drawing.Size(159, 23);
            this.buttonCreateAnnouncement.TabIndex = 0;
            this.buttonCreateAnnouncement.Text = "Create Announcement";
            this.buttonCreateAnnouncement.UseVisualStyleBackColor = true;
            this.buttonCreateAnnouncement.Click += new System.EventHandler(this.buttonCreateAnnouncement_Click);
            // 
            // buttonCreateLink
            // 
            this.buttonCreateLink.Location = new System.Drawing.Point(177, 76);
            this.buttonCreateLink.Name = "buttonCreateLink";
            this.buttonCreateLink.Size = new System.Drawing.Size(159, 23);
            this.buttonCreateLink.TabIndex = 0;
            this.buttonCreateLink.Text = "Create Link";
            this.buttonCreateLink.UseVisualStyleBackColor = true;
            this.buttonCreateLink.Click += new System.EventHandler(this.buttonCreateLink_Click);
            // 
            // buttonCreateContact
            // 
            this.buttonCreateContact.Location = new System.Drawing.Point(342, 76);
            this.buttonCreateContact.Name = "buttonCreateContact";
            this.buttonCreateContact.Size = new System.Drawing.Size(159, 23);
            this.buttonCreateContact.TabIndex = 0;
            this.buttonCreateContact.Text = "Create Contact";
            this.buttonCreateContact.UseVisualStyleBackColor = true;
            this.buttonCreateContact.Click += new System.EventHandler(this.buttonCreateContact_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 122);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1126, 392);
            this.dataGridView1.TabIndex = 1;
            // 
            // buttonAnnouncements
            // 
            this.buttonAnnouncements.Location = new System.Drawing.Point(12, 12);
            this.buttonAnnouncements.Name = "buttonAnnouncements";
            this.buttonAnnouncements.Size = new System.Drawing.Size(159, 23);
            this.buttonAnnouncements.TabIndex = 2;
            this.buttonAnnouncements.Text = "Announcements";
            this.buttonAnnouncements.UseVisualStyleBackColor = true;
            this.buttonAnnouncements.Click += new System.EventHandler(this.buttonAnnouncements_Click);
            // 
            // buttonLinks
            // 
            this.buttonLinks.Location = new System.Drawing.Point(177, 12);
            this.buttonLinks.Name = "buttonLinks";
            this.buttonLinks.Size = new System.Drawing.Size(159, 23);
            this.buttonLinks.TabIndex = 3;
            this.buttonLinks.Text = "Links";
            this.buttonLinks.UseVisualStyleBackColor = true;
            this.buttonLinks.Click += new System.EventHandler(this.buttonLinks_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 526);
            this.Controls.Add(this.buttonLinks);
            this.Controls.Add(this.buttonAnnouncements);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonCreateContact);
            this.Controls.Add(this.buttonCreateLink);
            this.Controls.Add(this.buttonCreateAnnouncement);
            this.Name = "MainForm";
            this.Text = "Portal Test Client";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateAnnouncement;
        private System.Windows.Forms.Button buttonCreateLink;
        private System.Windows.Forms.Button buttonCreateContact;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonAnnouncements;
        private System.Windows.Forms.Button buttonLinks;

    }
}

