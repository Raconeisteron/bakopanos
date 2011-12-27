using Microsoft.Practices.Unity;

namespace ASPNETPortal
{
    internal class UnityModule
    {
        public UnityModule(IUnityContainer container)
        {
            container.RegisterType<ConfigurationDb, ConfigurationDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IModuleDefinitionDb, ConfigurationDb>();
            container.RegisterType<IGlobalDb, ConfigurationDb>();
            container.RegisterType<ITabDb, ConfigurationDb>();
            container.RegisterType<IModuleDb, ConfigurationDb>();
            container.RegisterType<IModuleDefinitionDb, ConfigurationDb>();
        }
    }
}