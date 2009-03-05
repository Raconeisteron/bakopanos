using System.Collections.Generic;
using System.Windows.Forms;
using Bakopanos.BusinessObjects;
using Bakopanos.Framework.Composite;
using Microsoft.Practices.Unity;

namespace Bakopanos.WinClient.ProductsModule.Views
{
    public partial class ProductListView : UserControl, IView<IProductListPresenter>
    {
        private readonly Dictionary<string, Control> _Placeholders =
            new Dictionary<string, Control>();

        private IProductListPresenter _presenter;
        private ProductAggregate _product;

        public ProductListView()
        {
            InitializeComponent();

            Placeholders.Add("test", tabControl1);
        }

        [Dependency]
        public ProductAggregate Product
        {
            set { _product = value; }
        }

        #region IView<IProductListPresenter> Members

        [Dependency]
        public IProductListPresenter Presenter
        {
            set { _presenter = value; }
        }

        public string Caption { get; set; }

        public Dictionary<string, Control> Placeholders
        {
            get { return _Placeholders; }
        }


        public void Run()
        {
            _presenter.Run();

            dataGridView1.DataSource = _product.ProductList;
        }

        #endregion
    }
}