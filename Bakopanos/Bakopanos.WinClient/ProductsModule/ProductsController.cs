using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bakopanos.Framework.Composite;
using Bakopanos.NW.BusinessObjects;
using Bakopanos.NW.WinClient.ProductsModule.Views;
using Microsoft.Practices.Unity;

namespace Bakopanos.NW.WinClient.ProductsModule
{
    public class ProductsController
    {        
        public ProductsController([Dependency("MainWorkspace")]IWorkspace workspace,
            ProductListView productListView)
        {
            workspace.Show(productListView);
        }
    }
}
