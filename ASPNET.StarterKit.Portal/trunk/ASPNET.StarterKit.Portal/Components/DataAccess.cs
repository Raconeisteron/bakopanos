
using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Factory and Service Locator
    /// </summary>
    public static class DataAccess
    {
        private static readonly Dictionary<Type, object> Locator = new Dictionary<Type, object>();

        static DataAccess()
        {
            Announcements = new AnnouncementsDb();
            Discussions = new DiscussionDb();
            Documents = new DocumentDb();
            Links = new LinkDb();
            Users = new UsersDb();

            Locator.Add(typeof(IAnnouncementsDb), Announcements);
            Locator.Add(typeof(IContactsDb), Contacts);
            Locator.Add(typeof(IDiscussionDb), Discussions);
            Locator.Add(typeof(IDocumentDb), Documents);
            Locator.Add(typeof(ILinkDb), Links);
        }

        public static T Resolve<T>()
        {
            return (T)Locator[typeof(T)];
        }

        public static IAnnouncementsDb Announcements { get; private set; }
        public static IContactsDb Contacts { get; private set; }
        public static IDiscussionDb Discussions { get; private set; }
        public static IDocumentDb Documents { get; private set; }
        public static ILinkDb Links { get; private set; }
        public static IUsersDb Users { get; private set; }

    }
}