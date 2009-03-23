using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Evolutil.Library.Log;
using Evolutil.ServiceContracts;
using Microsoft.Practices.Unity;

namespace Evolutil.WinForms
{
    public partial class Form1 : Form
    {
        public Form1([Dependency("log1")]ILogger log)
        {
            InitializeComponent();

            log.Debug("testtesttest");
            listBox1.ValueMember = "ProductID";
            listBox1.DisplayMember = "ProductName";
        }

        [Dependency]
        public IProductService ProductService { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = ProductService.Read();
        }
    }
}
