using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public class ContainerConfigurator : DatabaseConfigurationSection
    {
        public override void Configure(IUnityContainer container)
        {            
            container.RegisterInstance<IDatabaseConfiguration>(this);

            container.RegisterType<IAnnouncementsDB,AnnouncementsDB>();
            container.RegisterType<IContactsDB,ContactsDB>();
            container.RegisterType<IDiscussionDB,DiscussionDB>();
            container.RegisterType<IDocumentDB,DocumentDB>();
            container.RegisterType<IEventsDB,EventsDB>();
            container.RegisterType<IHtmlTextDB,HtmlTextDB>();
            container.RegisterType<ILinkDB,LinkDB>();
            container.RegisterType<IRolesDB,RolesDB>();
            container.RegisterType<IUsersDB,UsersDB>();
            container.RegisterType<IConfigurationDB,ConfigurationDB>();            
        }        
    }
}
