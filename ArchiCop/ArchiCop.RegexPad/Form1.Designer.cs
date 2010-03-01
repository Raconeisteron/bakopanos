namespace ArchiCop.RegexPad
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
            this.richTextBoxInput = new System.Windows.Forms.RichTextBox();
            this.richTextBoxPattern = new System.Windows.Forms.RichTextBox();
            this.buttonIsMatch = new System.Windows.Forms.Button();
            this.comboBoxRegexOptions = new System.Windows.Forms.ComboBox();
            this.checkBoxAutoIsMatch = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // richTextBoxInput
            // 
            this.richTextBoxInput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxInput.Location = new System.Drawing.Point(12, 12);
            this.richTextBoxInput.Name = "richTextBoxInput";
            this.richTextBoxInput.Size = new System.Drawing.Size(366, 96);
            this.richTextBoxInput.TabIndex = 0;
            this.richTextBoxInput.Text = "";
            this.richTextBoxInput.TextChanged += new System.EventHandler(this.richTextBoxInput_TextChanged);
            // 
            // richTextBoxPattern
            // 
            this.richTextBoxPattern.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxPattern.Location = new System.Drawing.Point(12, 114);
            this.richTextBoxPattern.Name = "richTextBoxPattern";
            this.richTextBoxPattern.Size = new System.Drawing.Size(366, 96);
            this.richTextBoxPattern.TabIndex = 1;
            this.richTextBoxPattern.Text = "";
            this.richTextBoxPattern.TextChanged += new System.EventHandler(this.richTextBoxPattern_TextChanged);
            // 
            // buttonIsMatch
            // 
            this.buttonIsMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIsMatch.Location = new System.Drawing.Point(12, 225);
            this.buttonIsMatch.Name = "buttonIsMatch";
            this.buttonIsMatch.Size = new System.Drawing.Size(75, 23);
            this.buttonIsMatch.TabIndex = 2;
            this.buttonIsMatch.Text = "IsMatch";
            this.buttonIsMatch.UseVisualStyleBackColor = true;
            this.buttonIsMatch.Click += new System.EventHandler(this.buttonIsMatch_Click);
            // 
            // comboBoxRegexOptions
            // 
            this.comboBoxRegexOptions.FormattingEnabled = true;
            this.comboBoxRegexOptions.Location = new System.Drawing.Point(93, 227);
            this.comboBoxRegexOptions.Name = "comboBoxRegexOptions";
            this.comboBoxRegexOptions.Size = new System.Drawing.Size(156, 21);
            this.comboBoxRegexOptions.TabIndex = 3;
            // 
            // checkBoxAutoIsMatch
            // 
            this.checkBoxAutoIsMatch.AutoSize = true;
            this.checkBoxAutoIsMatch.Checked = true;
            this.checkBoxAutoIsMatch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoIsMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAutoIsMatch.Location = new System.Drawing.Point(255, 229);
            this.checkBoxAutoIsMatch.Name = "checkBoxAutoIsMatch";
            this.checkBoxAutoIsMatch.Size = new System.Drawing.Size(101, 17);
            this.checkBoxAutoIsMatch.TabIndex = 4;
            this.checkBoxAutoIsMatch.Text = "Auto IsMatch";
            this.checkBoxAutoIsMatch.UseVisualStyleBackColor = true;
            this.checkBoxAutoIsMatch.CheckedChanged += new System.EventHandler(this.checkBoxAutoIsMatch_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 260);
            this.Controls.Add(this.checkBoxAutoIsMatch);
            this.Controls.Add(this.comboBoxRegexOptions);
            this.Controls.Add(this.buttonIsMatch);
            this.Controls.Add(this.richTextBoxPattern);
            this.Controls.Add(this.richTextBoxInput);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxInput;
        private System.Windows.Forms.RichTextBox richTextBoxPattern;
        private System.Windows.Forms.Button buttonIsMatch;
        private System.Windows.Forms.ComboBox comboBoxRegexOptions;
        private System.Windows.Forms.CheckBox checkBoxAutoIsMatch;
    }
}

