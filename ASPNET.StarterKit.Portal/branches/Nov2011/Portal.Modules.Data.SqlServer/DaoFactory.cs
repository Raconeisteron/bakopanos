using System.Configuration;

namespace Portal.Modules.Data.SqlServer
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
            get { return new PortalDb {ConnectionString = _connectionString}; }
        }

        public IAnnouncementsDb AnnouncementsDb
        {
            get { return new AnnouncementsDb {ConnectionString = _connectionString}; }
        }

        public IContactsDb ContactsDb
        {
            get { return new ContactsDb {ConnectionString = _connectionString}; }
        }

        public IDiscussionsDb DiscussionDb
        {
            get { return new DiscussionsDb {ConnectionString = _connectionString}; }
        }

        public IDocumentsDb DocumentDb
        {
            get { return new DocumentsDb {ConnectionString = _connectionString}; }
        }

        public IEventsDb EventsDb
        {
            get { return new EventsDb {ConnectionString = _connectionString}; }
        }

        public IHtmlTextDb HtmlTextDb
        {
            get { return new HtmlTextDb {ConnectionString = _connectionString}; }
        }

        public ILinksDb LinkDb
        {
            get { return new LinkDb {ConnectionString = _connectionString}; }
        }

        #endregion
    }
}