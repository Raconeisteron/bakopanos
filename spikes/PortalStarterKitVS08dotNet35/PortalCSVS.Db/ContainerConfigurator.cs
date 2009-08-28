using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public class ContainerConfigurator : DatabaseConfigurationSection
    {
        public override void Configure(IUnityContainer container)
        {            
            container.RegisterInstance<IDatabaseConfiguration>(this);

            container.RegisterType<IAnnouncementsDb,AnnouncementsDb>();
            container.RegisterType<IContactsDb,ContactsDb>();
            container.RegisterType<IDiscussionDb,DiscussionDb>();
            container.RegisterType<IDocumentDb,DocumentDb>();
            container.RegisterType<IEventsDb,EventsDb>();
            container.RegisterType<IHtmlTextDb,HtmlTextDb>();
            container.RegisterType<ILinkDb,LinkDb>();
            container.RegisterType<IRolesDb,RolesDb>();
            container.RegisterType<IUsersDb,UsersDb>();
            container.RegisterType<IConfigurationDb,ConfigurationDb>();            
        }        
    }
}
