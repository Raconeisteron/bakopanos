using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    internal class UnityModule
    {
        public UnityModule(IUnityContainer container)
        {
            container.RegisterType<IAnnouncementsDb, AnnouncementsDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IContactsDb, ContactsDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILinkDb, LinkDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDiscussionDb, DiscussionDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDocumentDb, DocumentDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventsDb, EventsDb>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHtmlTextDb, HtmlTextDb>(new ContainerControlledLifetimeManager());
        }
    }
}