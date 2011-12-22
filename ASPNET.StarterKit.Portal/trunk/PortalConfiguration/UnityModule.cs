using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    internal class UnityModule
    {
        public UnityModule(IUnityContainer container)
        {
            container.RegisterType<ConfigurationDb, ConfigurationDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IModuleDefinitionDb, ConfigurationDb>();
            container.RegisterType<IAuthorizationDb, ConfigurationDb>();
            container.RegisterType<IGlobalDb, ConfigurationDb>();
            container.RegisterType<ITabDb, ConfigurationDb>();
            container.RegisterType<IModuleDb, ConfigurationDb>();
            container.RegisterType<IModuleDefinitionDb, ConfigurationDb>();
        }
    }
}