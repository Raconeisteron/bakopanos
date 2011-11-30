using System;
using System.Configuration;

namespace Portal.Modules.DAL
{
    /// <summary>
    /// This class shields the client from the details of database specific 
    /// data-access objects. It returns the appropriate data-access objects 
    /// according to the configuration in web.config.
    /// </summary>
    /// <remarks>
    /// GoF Design Patterns: Factory, Singleton, Proxy.
    /// 
    /// This class makes extensive use of the Factory pattern in determining which 
    /// database specific DAOs (Data Access Objects) to return.
    /// 
    /// This class is like a Singleton -- it is a static class and 
    /// therefore only one 'instance' will ever exist.
    /// 
    /// This class is a Proxy as it 'stands in' for the actual Data Access Object Factory.
    /// </remarks>
    public static class ModulesDataAccess
    {
        private static readonly IDaoFactory Factory;

        static ModulesDataAccess()
        {
            Type type = Type.GetType(ConfigurationManager.AppSettings["Modules.DAL"]);
            Factory = (IDaoFactory) Activator.CreateInstance(type);
        }

        public static IPortalDb PortalDb
        {
            get { return Factory.PortalDb; }
        }

        public static IAnnouncementsDb AnnouncementsDb
        {
            get { return Factory.AnnouncementsDb; }
        }

        public static IContactsDb ContactsDb
        {
            get { return Factory.ContactsDb; }
        }

        public static IDiscussionsDb DiscussionDb
        {
            get { return Factory.DiscussionDb; }
        }

        public static IDocumentsDb DocumentDb
        {
            get { return Factory.DocumentDb; }
        }

        public static IEventsDb EventsDb
        {
            get { return Factory.EventsDb; }
        }

        public static ILinksDb LinkDb
        {
            get { return Factory.LinkDb; }
        }

        public static IHtmlTextDb HtmlTextDb
        {
            get { return Factory.HtmlTextDb; }
        }
    }
}