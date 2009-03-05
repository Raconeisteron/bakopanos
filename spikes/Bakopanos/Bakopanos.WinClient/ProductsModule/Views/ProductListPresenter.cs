using Bakopanos.BusinessObjects;
using Bakopanos.Facade.Products;
using Microsoft.Practices.Unity;

namespace Bakopanos.WinClient.ProductsModule.Views
{
    public class ProductListPresenter : IProductListPresenter
    {
        private IProductsFacade _facade;
        private ProductAggregate _product;

        #region IProductListPresenter Members

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

        #endregion
    }
}