using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bakopanos.Framework.Composite;
using Bakopanos.NW.BusinessObjects;
using Bakopanos.NW.WinClient.ProductsModule.Views;
using Bakopanos.WinClient.ProductsModule.Views;
using Microsoft.Practices.Unity;

namespace Bakopanos.NW.WinClient.ProductsModule
{

    public interface IProductsController : IController
    {
        Panel Workspace { set; }
        ProductListView View1 { set; }
        TestView View2 { set; }
       
    }

    public class ProductsController : IProductsController
    {        

        private Panel _workspace;
        private ProductListView _view1;
        private TestView _view2;
        
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
                
        public ProductsController(IUnityContainer container)
        {
            container.RegisterType<IProductListPresenter, ProductListPresenter>();
            container.RegisterType<ITestPresenter, TestPresenter>();     
        }

        public void Run()
        {            
            _workspace.ShowView(_view1);

            _view2.Caption = "test";
            _view1.Placeholders["test"].ShowView(_view2);            
        }
    }
}
