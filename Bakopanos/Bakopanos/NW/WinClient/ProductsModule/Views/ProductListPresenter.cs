using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bakopanos.NW.BusinessObjects;
using Bakopanos.NW.Facade.Products;
using Microsoft.Practices.Unity;

namespace Bakopanos.NW.WinClient.ProductsModule.Views
{
    public class ProductListPresenter
    {
        public ProductListPresenter(ProductAggregate productAggregate, IProductsFacade facade)
        {
            productAggregate.ProductList = facade.Products();
        }
    }
}
