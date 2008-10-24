using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bakopanos.Framework.Composite;
using Microsoft.Practices.Unity;

namespace Bakopanos.WinClient.ProductsModule.Views
{    
    public partial class TestView : UserControl, IView<ITestPresenter>
    {
        private ITestPresenter _presenter;
        
        [Dependency]
        public ITestPresenter Presenter
        {
            get { return _presenter; }
            set { _presenter = value; }
        }

        public TestView()
        {
            InitializeComponent();
        }


        public void Run()
        {
            _presenter.Run();
        }

        public string Caption { get; set; }

        Dictionary<string, Control> _Placeholders= 
            new Dictionary<string, Control>();

        public Dictionary<string, Control> Placeholders
        {
            get { return _Placeholders; }
        }

       
    }
}
