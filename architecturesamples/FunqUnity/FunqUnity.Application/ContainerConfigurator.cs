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
            container.RegisterType<IProductService, ProductService>(new ContainerControlledLifetimeManager());
            
            return container;
        }        

    }
}
