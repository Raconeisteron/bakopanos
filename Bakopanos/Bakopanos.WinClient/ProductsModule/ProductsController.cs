using System.Windows.Forms;
using Bakopanos.Framework.Composite;
using Bakopanos.WinClient.ProductsModule.Views;
using Microsoft.Practices.Unity;

namespace Bakopanos.WinClient.ProductsModule
{
    public class ProductsController : IProductsController
    {
        private ProductListView _view1;
        private TestView _view2;
        private Panel _workspace;

        public ProductsController(IUnityContainer container)
        {
            container.RegisterType<IProductListPresenter, ProductListPresenter>();
            container.RegisterType<ITestPresenter, TestPresenter>();
        }

        #region IProductsController Members

        [Dependency("MainWorkspace")]
        public Panel Workspace
        {
            set { _workspace = value; }
        }

        [Dependency]
        public ProductListView View1
        {
            get { return _view1; }
            set { _view1 = value; }
        }

        [Dependency]
        public TestView View2
        {
            set { _view2 = value; }
        }

        public void Run()
        {
            _workspace.ShowView(_view1);

            _view2.Caption = "test";
            _view1.Placeholders["test"].ShowView(_view2);
        }

        #endregion
    }
}