using Microsoft.Practices.Unity;

namespace NW247.Module.Controllers
{
    public class ProductsController : IProductsController
    {
        public IUnityContainer container;
        //private readonly IRegionManager regionManager;

        [Dependency]
        public IUnityContainer Container
        {
            set { container = value; }
        }

        /*public ProductsController(IRegionManager regionManager)
        {            
            this.regionManager = regionManager;
        }*/
    }
}