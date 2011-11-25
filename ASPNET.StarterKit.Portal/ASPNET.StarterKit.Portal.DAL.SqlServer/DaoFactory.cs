namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// Sql Server specific factory that creates Sql Server specific data access objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    internal class DaoFactory : IDaoFactory
    {
        private readonly string _connectionString;

        public DaoFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region IDaoFactory Members

        public IPortalDB PortalDB
        {
            get { return new PortalDB(_connectionString); }
        }

        public IAnnouncementsDB AnnouncementsDB
        {
            get { return new AnnouncementsDB(_connectionString); }
        }

        public IContactsDB ContactsDB
        {
            get { return new ContactsDB(_connectionString); }
        }

        public IDiscussionDB DiscussionDB
        {
            get { return new DiscussionDB(_connectionString); }
        }

        public IDocumentDB DocumentDB
        {
            get { return new DocumentDB(_connectionString); }
        }

        public IEventsDB EventsDB
        {
            get { return new EventsDB(_connectionString); }
        }

        public IHtmlTextDB HtmlTextDB
        {
            get { return new HtmlTextDB(_connectionString); }
        }

        public ILinkDB LinkDB
        {
            get { return new LinkDB(_connectionString); }
        }

        public IRolesDB RolesDB
        {
            get { return new RolesDB(_connectionString); }
        }

        public IUsersDB UsersDB
        {
            get { return new UsersDB(_connectionString); }
        }

        #endregion
    }
}