using Bakopanos.BusinessObjects;
using Bakopanos.Facade.Products;
using Bakopanos.Framework.Composite;

namespace Bakopanos.WinClient.ProductsModule.Views
{
    public interface IProductListPresenter : IPresenter
    {
        ProductAggregate Product { set; }
        IProductsFacade Facade { set; }
    }
}