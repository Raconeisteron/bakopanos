using System.Configuration;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// Sql Server specific factory that creates Sql Server specific data access objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public class DaoFactory : IDaoFactory
    {
        private readonly string _connectionString;

        public DaoFactory()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

        #region IDaoFactory Members

        public IPortalDb PortalDB
        {
            get { return new PortalDb(_connectionString); }
        }

        public IAnnouncementsDb AnnouncementsDB
        {
            get { return new AnnouncementsDb(_connectionString); }
        }

        public IContactsDb ContactsDB
        {
            get { return new ContactsDb(_connectionString); }
        }

        public IDiscussionsDb DiscussionDB
        {
            get { return new DiscussionsDb(_connectionString); }
        }

        public IDocumentsDb DocumentDB
        {
            get { return new DocumentsDb(_connectionString); }
        }

        public IEventsDb EventsDB
        {
            get { return new EventsDb(_connectionString); }
        }

        public IHtmlTextDb HtmlTextDB
        {
            get { return new HtmlTextDb(_connectionString); }
        }

        public ILinksDb LinkDB
        {
            get { return new LinkDb(_connectionString); }
        }

        public IRolesDb RolesDB
        {
            get { return new RolesDb(_connectionString); }
        }

        public IUsersDb UsersDB
        {
            get { return new UsersDb(_connectionString); }
        }

        #endregion
    }
}