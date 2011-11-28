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

        public IPortalDb PortalDb
        {
            get { return new PortalDb(_connectionString); }
        }

        public IAnnouncementsDb AnnouncementsDb
        {
            get { return new AnnouncementsDb(_connectionString); }
        }

        public IContactsDb ContactsDb
        {
            get { return new ContactsDb(_connectionString); }
        }

        public IDiscussionsDb DiscussionDb
        {
            get { return new DiscussionsDb(_connectionString); }
        }

        public IDocumentsDb DocumentDb
        {
            get { return new DocumentsDb(_connectionString); }
        }

        public IEventsDb EventsDb
        {
            get { return new EventsDb(_connectionString); }
        }

        public IHtmlTextDb HtmlTextDb
        {
            get { return new HtmlTextDb(_connectionString); }
        }

        public ILinksDb LinkDb
        {
            get { return new LinkDb(_connectionString); }
        }

        public IRolesDb RolesDb
        {
            get { return new RolesDb(_connectionString); }
        }

        public IUsersDb UsersDb
        {
            get { return new UsersDb(_connectionString); }
        }

        #endregion
    }
}