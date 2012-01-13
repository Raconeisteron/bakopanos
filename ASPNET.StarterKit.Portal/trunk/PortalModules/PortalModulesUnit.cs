using System;
using ASPNET.StarterKit.Portal.SqlClient;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public class PortalModulesUnit :IUnit
    {
        private readonly IUnityContainer _container;

        public PortalModulesUnit(IUnityContainer container)
        {
            _container = container;
        }

        public void Configure(string adapter)
        {
            switch (adapter)
            {
                case "SqlClient":
                     _container.RegisterType<IAnnouncementsDb, SqlAnnouncementsDb>(new ContainerControlledLifetimeManager());
                    _container.RegisterType<IContactsDb, SqlContactsDb>(new ContainerControlledLifetimeManager());
                    break;
            }
           
        }
    }
}