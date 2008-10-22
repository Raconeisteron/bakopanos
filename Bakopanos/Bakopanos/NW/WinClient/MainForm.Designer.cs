using Bakopanos.Framework.Composite;

namespace Bakopanos.NW.WinClient
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
            this.workspace = new PanelWorkspace();
            this.workspace.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.workspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspace.Location = new System.Drawing.Point(0, 0);
            this.workspace.Name = "workspace";
            this.workspace.Size = new System.Drawing.Size(659, 427);
            this.workspace.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 427);
            this.Controls.Add(this.workspace);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.workspace.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PanelWorkspace workspace;

    }
}