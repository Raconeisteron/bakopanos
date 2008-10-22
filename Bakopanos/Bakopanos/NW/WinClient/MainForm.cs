﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bakopanos.NW.Facade.Products;

namespace Bakopanos.NW.WinClient
{
    public partial class MainForm : Form
    {
        public MainForm(ProductsFacade facade)
        {
            InitializeComponent();
            
            dataGridView1.DataSource = facade.Products();
        }
    }
}
