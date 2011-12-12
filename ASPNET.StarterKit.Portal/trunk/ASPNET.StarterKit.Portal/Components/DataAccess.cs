
using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    public static class DataAccess
    {
        private static readonly Dictionary<Type, object> Locator = new Dictionary<Type, object>();

        static DataAccess()
        {
            Locator.Add(typeof(IAnnouncementsDb), new AnnouncementsDb());
            Locator.Add(typeof(IContactsDb), new ContactsDb());
            Locator.Add(typeof(IDiscussionDb), new DiscussionDb());
            Locator.Add(typeof(IDocumentDb), new DocumentDb());
            Locator.Add(typeof(ILinkDb), new LinkDb());
        }

        public static T Resolve<T> ()
        {
            return (T)Locator[typeof (T)];
        }
    }
}