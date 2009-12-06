using System;
using System.Configuration;
using FunqUnity.Infrastructure;
using Microsoft.Practices.Unity;

namespace FunqUnity.Application
{


    public class ContainerConfigurator : ContainerConfiguratorBase
    {
        public override IUnityContainer Configure(IUnityContainer container)
        {

            IUnityContainer childContainer = container.CreateChildContainer();

            childContainer.RegisterType<IProductService, ProductService>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<Func<IProductService>>(() => childContainer.Resolve<IProductService>());
            
            return container;
        }        

    }
}
