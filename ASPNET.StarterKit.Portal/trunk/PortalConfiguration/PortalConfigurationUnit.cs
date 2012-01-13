using System;
using ASPNET.StarterKit.Portal.XmlFile;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public class PortalConfigurationUnit :IUnit
    {
        private readonly IUnityContainer _container;

        public PortalConfigurationUnit(IUnityContainer container)
        {
            _container = container;
        }

        public void Configure(string adapter)
        {
            switch (adapter)
            {
                case "XmlFile":
                    _container.RegisterType<IConfigurationDb, XmlConfigurationDb>(
                        new ContainerControlledLifetimeManager());
                    _container.RegisterType<IModuleConfigurationDb, XmlModuleConfigurationDb>(
                        new ContainerControlledLifetimeManager());
                    _container.RegisterType<IModuleDefConfigurationDb, XmlModuleDefConfigurationDb>(
                        new ContainerControlledLifetimeManager());
                    _container.RegisterType<IPortalConfigurationDb, XmlPortalConfigurationDb>(
                        new ContainerControlledLifetimeManager());
                    _container.RegisterType<ITabConfigurationDb, XmlTabConfigurationDb>(
                        new ContainerControlledLifetimeManager());
                    break;
            }
        }
    }
}