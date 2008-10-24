using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bakopanos.Framework.Composite;
using Bakopanos.NW.BusinessObjects;
using Microsoft.Practices.Unity;

namespace Bakopanos.NW.WinClient.ProductsModule.Views
{
    public partial class ProductListView : UserControl, IView<IProductListPresenter>
    {               
        private ProductAggregate _product;
        private IProductListPresenter _presenter;

        [Dependency]
        public ProductAggregate Product
        {
            set { _product = value; }
        }

        [Dependency]
        public IProductListPresenter Presenter
        {
            set { _presenter = value; }
        }

        public ProductListView()
        {
            InitializeComponent();
            
            Placeholders.Add("test", tabControl1);
        }

        public string Caption { get;set; }

        private Dictionary<string, Control> _Placeholders = 
            new Dictionary<string, Control>();

        public Dictionary<string, Control> Placeholders
        {
            get { return _Placeholders; }            
        }

        

        public void Run()
        {
            _presenter.Run();

            dataGridView1.DataSource = _product.ProductList;
        }
    }
}
