using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using NW247.Module.Controllers;
using NW247.Module.Views;

namespace NW247.Module
{
    public class Module : IModule
    {
        private readonly IRegionManager regionManager;
        public IUnityContainer container;

        public Module(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        [Dependency]
        public IUnityContainer Container
        {
            set { container = value; }
        }

        #region IModule Members

        public void Initialize()
        {
            RegisterViewsAndServices();

            var presenter = container.Resolve<IProductsPresenter>();

            IRegion mainRegion = regionManager.Regions["MainRegion"];
            mainRegion.Add(presenter.View);
        }

        #endregion

        protected void RegisterViewsAndServices()
        {
            container.RegisterType<IProductsController, ProductsController>();

            container.RegisterType<IProductsView, ProductsView>();
            container.RegisterType<IProductsPresenter, ProductsPresenter>();
        }
    }
}