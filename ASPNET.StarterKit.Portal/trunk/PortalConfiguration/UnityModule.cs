using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    internal class UnityModule
    {
        public UnityModule(IUnityContainer container)
        {
            container.RegisterType<IConfigurationDb, ConfigurationDb>(new ContainerControlledLifetimeManager());
        }
    }
}