using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    internal class UnityModule
    {
        public UnityModule(IUnityContainer container)
        {
            container.RegisterType<IUsersDb, UsersDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRolesDb, RolesDb>(new ContainerControlledLifetimeManager());

            container.RegisterType<IPortalSecurity, PortalSecurity>(new ContainerControlledLifetimeManager());
        }
    }
}