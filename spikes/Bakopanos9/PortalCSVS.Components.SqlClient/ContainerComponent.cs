using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public class ContainerComponent : IContainerComponent
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterType<IAnnouncementsDB, AnnouncementsDB>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContactsDB, ContactsDB>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDiscussionDB, DiscussionDB>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDocumentDB, DocumentDB>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventsDB, EventsDB>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHtmlTextDB, HtmlTextDB>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILinkDB, LinkDB>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRolesDB, RolesDB>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUsersDB, UsersDB>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPortalDB, PortalDB>(new ContainerControlledLifetimeManager());
        }
    }
}