using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bakopanos.Framework.Composite;
using Bakopanos.NW.Facade.Products;
using Microsoft.Practices.Unity;

namespace Bakopanos.NW.WinClient
{
    public partial class MainForm : Form
    {
        public MainForm(IUnityContainer container)
        {
            InitializeComponent();

            container.RegisterInstance<IWorkspace>("MainWorkspace", workspace);
        }
    }
}
