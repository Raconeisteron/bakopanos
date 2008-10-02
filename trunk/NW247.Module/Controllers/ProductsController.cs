using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using NW247.Infrastructure;
using NW247.Module.Views;

namespace NW247.Module.Controllers
{
    public class ProductsController : IProductsController
    {
        private readonly IUnityContainer _Container;
        private readonly IRegionManager _Manager;                

        public ProductsController(IRegionManager RegionManager, IUnityContainer UnityContainer)
        {

            _Container = UnityContainer;
            _Manager = RegionManager;

            RegisterViewsAndServices();

            var view = _Container.Resolve<IProductsView>();

            IRegion mainRegion = _Manager.Regions[RegionNames.MainRegion];
            mainRegion.Add(view);
        }

        private void RegisterViewsAndServices()
        {
            _Container.RegisterType<IProductsView, ProductsView>();
            _Container.RegisterType<IProductsPresenter, ProductsPresenter>();
        }


    }
}