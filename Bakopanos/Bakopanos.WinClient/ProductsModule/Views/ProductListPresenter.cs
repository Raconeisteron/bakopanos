using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bakopanos.Framework.Composite;
using Bakopanos.NW.BusinessObjects;
using Bakopanos.NW.Facade.Products;
using Microsoft.Practices.Unity;

namespace Bakopanos.NW.WinClient.ProductsModule.Views
{

    public interface IProductListPresenter:IPresenter{}

    public class ProductListPresenter: IProductListPresenter
    {
        private ProductAggregate _product;
        private IProductsFacade _facade;
        
        [Dependency]
        public ProductAggregate Product
        {            
            set { _product = value; }
        }

        [Dependency]
        public IProductsFacade Facade
        {            
            set { _facade = value; }
        }

        public void Run()
        {
            _product.ProductList = _facade.Products();
        }
    }
}
