using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class PortalModulesSqlClientUnit :IUnit
    {
        public PortalModulesSqlClientUnit(IUnityContainer container)
        {
            container.RegisterType<IAnnouncementsDb, SqlAnnouncementsDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContactsDb, SqlContactsDb>(new ContainerControlledLifetimeManager());
            
        }
    }
}