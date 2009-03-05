using Bakopanos.Framework.Composite;

namespace Bakopanos.WinClient.ProductsModule
{
    public interface IMainModule : IModule
    {
        IProductsController ProductsController { set; }
    }
}