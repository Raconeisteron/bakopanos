using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class PortalSecuritySqlClientUnit :IUnit
    {
        public PortalSecuritySqlClientUnit(IUnityContainer container)
        {
            container.RegisterType<IPortalSecurity, PortalSecurity>(new ContainerControlledLifetimeManager());
        }
    }
}