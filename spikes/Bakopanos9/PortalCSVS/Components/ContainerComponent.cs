using System.Configuration;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public class ContainerComponent : ConfigurationSection, IContainerComponent
    {
        public void Configure(IUnityContainer container)
        {
            var configuration = container.Resolve<GlobalConfig>();
            container.RegisterInstance<IGlobalConfig>(configuration);
            container.RegisterInstance<IPortalConfig>(configuration);
            container.RegisterInstance<ITabsConfig>(configuration);
            container.RegisterInstance<IModulesConfig>(configuration);
            container.RegisterInstance<IModuleDefConfig>(configuration);
        }
    }
}