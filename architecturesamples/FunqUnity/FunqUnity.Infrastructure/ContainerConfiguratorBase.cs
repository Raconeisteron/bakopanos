using Microsoft.Practices.Unity;
using System.Configuration;

namespace FunqUnity.Infrastructure
{   
    public abstract class ContainerConfiguratorBase : ConfigurationSection, IContainerConfigurator
    {
        public abstract IUnityContainer Configure(IUnityContainer container);
    }
}
