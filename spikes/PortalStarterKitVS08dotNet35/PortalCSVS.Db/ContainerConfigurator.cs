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
            container.RegisterType<IDiscussionsDb,DiscussionsDb>();
            container.RegisterType<IDocumentsDb,DocumentsDb>();
            container.RegisterType<IEventsDb,EventsDb>();
            container.RegisterType<IHtmlTextsDb,HtmlTextsDb>();
            container.RegisterType<ILinksDb,LinksDb>();
            container.RegisterType<IRolesDb,RolesDb>();
            container.RegisterType<IUsersDb,UsersDb>();
            container.RegisterType<IConfigurationDb,ConfigurationDb>();            
        }        
    }
}
