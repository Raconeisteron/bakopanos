using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace NW247.Module.Controllers
{
    public interface IProductsController
    {
    }

    public class ProductsController : IProductsController
    {
        private IUnityContainer container;
        private IRegionManager regionManager;

        public ProductsController(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }
    }
}