using System;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public class PortalSecurityUnit :IUnit
    {
        private readonly IUnityContainer _container;

        public PortalSecurityUnit(IUnityContainer container)
        {
            _container = container;
        }

        public void Configure(string adapter)
        {
            switch (adapter)
            {
                case "SqlClient":
                    _container.RegisterType<IPortalSecurity, PortalSecurity>(new ContainerControlledLifetimeManager());
                    break;
            }
        }
    }
}