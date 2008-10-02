using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using NW247.Infrastructure;
using NW247.Module.Controllers;
using NW247.Module.Views;

namespace NW247.Module
{
    public class Module : IModule
    {        
        public IUnityContainer container;

        public Module(IUnityContainer UnityContainer)
        {
            container = UnityContainer;
        }

        #region IModule Members

        public void Initialize()
        {
            container.RegisterType<IProductsController, ProductsController>().
                Resolve<IProductsController>();                        
        }

        #endregion

    }
}