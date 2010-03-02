using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ArchiCop.RegexPad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonIsMatch.Enabled = richTextBoxInput.Text.Length > 0;
        }

        private void buttonIsMatch_Click(object sender, EventArgs e)
        {
            IsMatch();
        }

        private void IsMatch()
        {
            var options = (RegexOptions)Enum.Parse(typeof(RegexOptions), comboBoxRegexOptions.Text);
            try
            {
                bool isMatch = Regex.IsMatch(richTextBoxInput.Text, richTextBoxPattern.Text, options);
                Text = string.Format("{0}", isMatch);
            }
            catch (Exception error)
            {
                Text = string.Format("{0}: {1}", error.Source, error.Message); 
            }            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxRegexOptions.Items.AddRange(Enum.GetNames(typeof(RegexOptions)));
            comboBoxRegexOptions.SelectedIndex = 0;
            comboBoxRegexOptions.SelectedIndexChanged +=
                delegate
                    {
                        if (checkBoxAutoIsMatch.Checked)
                        {
                            IsMatch();
                        }
                    };
        }

        private void richTextBoxInput_TextChanged(object sender, EventArgs e)
        {
            buttonIsMatch.Enabled = richTextBoxInput.Text.Length > 0;
            if (checkBoxAutoIsMatch.Checked)
            {
                IsMatch();
            }            
        }

        private void richTextBoxPattern_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoIsMatch.Checked)
            {
                IsMatch();
            }            
        }

        private void checkBoxAutoIsMatch_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoIsMatch.Checked)
            {
                IsMatch();
            }  
        }
    }
}
