using System;

namespace ASPNET.StarterKit.Portal.Db
{
    public class DbFactory
    {
        private static readonly DbFactory _instance = new DbFactory();

        private DbFactory()
        {
        }

        public static DbFactory Instance
        {
            get { return _instance; }
        }

        public IUsersDB GetUsersDB()
        {
            throw new NotImplementedException();
        }

        public IContactsDB GetContactsDB()
        {
            throw new NotImplementedException();
        }

        public IDiscussionDB GetDiscussionDB()
        {
            throw new NotImplementedException();
        }

        public IRolesDB GetRolesDB()
        {
            throw new NotImplementedException();
        }

        public ILinkDB GetLinkDB()
        {
            throw new NotImplementedException();
        }

        public IHtmlTextDB GetHtmlTextDB()
        {
            throw new NotImplementedException();
        }

        public IEventsDB GetEventsDB()
        {
            throw new NotImplementedException();
        }

        public IDocumentDB GetDocumentDB()
        {
            throw new NotImplementedException();
        }

        public IAnnouncementsDB GetAnnouncementsDB()
        {
            throw new NotImplementedException();
        }
    }
}