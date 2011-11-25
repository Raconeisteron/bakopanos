using System;
using System.Configuration;

namespace ASPNET.StarterKit.Portal.DAL
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
    /// This class is like a Singleton -- it is a static class (Shared in VB) and 
    /// therefore only one 'instance' will ever exist.
    /// 
    /// This class is a Proxy as it 'stands in' for the actual Data Access Object Factory.
    /// </remarks>
    public static class DataAccess
    {
        // The static field initializers below are thread safe.
        // Furthermore, they are executed in the order in which they appear
        // in the class declaration. Note: if a static constructor
        // is present you want to initialize these in that constructor.        
        private static readonly IDaoFactory Factory;

        static DataAccess()
        {
            Type type = Type.GetType(ConfigurationManager.AppSettings["DAL"]);
            var args =
                new object[]
                    {
                        ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString
                    };
            Factory = (IDaoFactory) Activator.CreateInstance(type, args);
        }

        public static IPortalDB PortalDB
        {
            get { return Factory.PortalDB; }
        }

        /// <summary>
        /// Gets a provider-specific customer data access object.
        /// </summary>
        public static IAnnouncementsDB AnnouncementsDB
        {
            get { return Factory.AnnouncementsDB; }
        }

        public static IContactsDB ContactsDB
        {
            get { return Factory.ContactsDB; }
        }

        public static IDiscussionDB DiscussionDB
        {
            get { return Factory.DiscussionDB; }
        }

        public static IDocumentDB DocumentDB
        {
            get { return Factory.DocumentDB; }
        }

        public static IEventsDB EventsDB
        {
            get { return Factory.EventsDB; }
        }

        public static ILinkDB LinkDB
        {
            get { return Factory.LinkDB; }
        }

        public static IHtmlTextDB HtmlTextDB
        {
            get { return Factory.HtmlTextDB; }
        }

        public static IRolesDB RolesDB
        {
            get { return Factory.RolesDB; }
        }

        public static IUsersDB UsersDB
        {
            get { return Factory.UsersDB; }
        }
    }
}