using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal.XmlFile
{
    public class PortalConfigurationXmlUnit :IUnit
    {
        public PortalConfigurationXmlUnit(IUnityContainer container)
        {
            container.RegisterType<IConfigurationDb, XmlConfigurationDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IModuleConfigurationDb, XmlModuleConfigurationDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IModuleDefConfigurationDb, XmlModuleDefConfigurationDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPortalConfigurationDb, XmlPortalConfigurationDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITabConfigurationDb, XmlTabConfigurationDb>(new ContainerControlledLifetimeManager());
        }
    }
}