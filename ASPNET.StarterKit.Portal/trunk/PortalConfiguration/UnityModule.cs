using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    internal class UnityModule
    {
        public UnityModule(IUnityContainer container)
        {
            container.RegisterType<ConfigurationDb, ConfigurationDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IConfigurationDb, ConfigurationDb>();
            container.RegisterType<IAccessRolesDb, ConfigurationDb>();
        }
    }
}