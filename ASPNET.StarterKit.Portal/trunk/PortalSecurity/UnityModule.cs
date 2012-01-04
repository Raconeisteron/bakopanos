using ASPNETPortal.Security.DataAccess;
using ASPNETPortal.Security.Model;
using Microsoft.Practices.Unity;

namespace ASPNETPortal.Security
{
    internal class UnityModule
    {
        public UnityModule(IUnityContainer container)
        {
            container.RegisterType<IUsersDb, UsersDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRolesDb, RolesDb>(new ContainerControlledLifetimeManager());

            container.RegisterType<IPortalSecurity, PortalSecurity>(new ContainerControlledLifetimeManager());

            container.RegisterType<IPortalRolesService, PortalRolesService>(new ContainerControlledLifetimeManager());
        }
    }
}