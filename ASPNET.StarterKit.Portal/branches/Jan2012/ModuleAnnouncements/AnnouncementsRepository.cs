using System;
using System.Collections.Generic;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public class AnnouncementsRepository : IAnnouncementsRepository
    {
        readonly IAnnouncementsDb _db;
        public AnnouncementsRepository(IAnnouncementsDb db)
        {
            _db = db;
        }

        private static Announcement ToAnnouncement(IDataRecord dataRecord)
        {
            var item = new Announcement
                           {
                               ItemId = Convert.ToInt32(dataRecord["ItemId"]),
                               ModuleId = Convert.ToInt32(dataRecord["ModuleId"]),
                               Description = dataRecord["Description"] as string,
                               Title = dataRecord["Title"] as string,
                               CreatedByUser = dataRecord["CreatedByUser"] as string,
                               CreatedDate = Convert.ToDateTime(dataRecord["CreatedDate"]),
                               ExpireDate = Convert.ToDateTime(dataRecord["ExpireDate"]),
                               MobileMoreLink = dataRecord["MobileMoreLink"] as string,
                               MoreLink = dataRecord["MoreLink"] as string
                           };
            return item;
        }

        public List<Announcement> GetAnnouncements(int moduleId)
        {
            IDataReader dr = _db.GetAnnouncements(moduleId);
            var list = new List<Announcement>();

            while (dr.Read())
            {
                list.Add(ToAnnouncement(dr));
            }

            dr.Close();
            return list;
        }

        public Announcement GetSingleAnnouncement(int itemId)
        {
            IDataReader dr = _db.GetSingleAnnouncement(itemId);
            var item = new Announcement();

            while (dr.Read())
            {
                item = ToAnnouncement(dr);
                break;
            }

            dr.Close();

            return item;
        }

        public void DeleteSingleAnnouncement(int itemId)
        {
            _db.DeleteAnnouncement(itemId);
        }

        public Announcement SaveAnnouncement(int itemId, int moduleId, String userName, String title, DateTime expireDate,
                                   String description, String moreLink, String mobileMoreLink)
        {
            if (itemId == 0)
            {
                itemId = _db.AddAnnouncement(moduleId, userName, title, expireDate,
                                           description, moreLink, mobileMoreLink);

            }
            else
            {
                _db.UpdateAnnouncement(itemId, userName, title, expireDate,
                                       description, moreLink, mobileMoreLink);
            }
            return GetSingleAnnouncement(itemId);
        }
    }
}