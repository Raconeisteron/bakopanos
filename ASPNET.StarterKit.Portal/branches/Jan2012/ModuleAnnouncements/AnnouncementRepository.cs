using System;
using System.Collections.Generic;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public class AnnouncementRepository : IRepository<Announcement>
    {
        readonly IAnnouncementDb _db;
        public AnnouncementRepository(IAnnouncementDb db)
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

        public List<Announcement> GetList(int moduleId)
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

        public Announcement GetSingle(int itemId)
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

        public void Delete(int itemId)
        {
            // Only attempt to delete the item if it is an existing item
            // (new items will have "ItemId" of 0)

            if (itemId != 0)
            {
                _db.DeleteAnnouncement(itemId);
            }
        }

        public Announcement Save(Announcement item)
        {
            if (item.ItemId == 0)
            {
                item.ItemId = _db.AddAnnouncement(item.ModuleId, item.CreatedByUser, item.Title, item.ExpireDate,
                                           item.Description, item.MoreLink, item.MobileMoreLink);

            }
            else
            {
                _db.UpdateAnnouncement(item.ItemId, item.CreatedByUser, item.Title, item.ExpireDate,
                                           item.Description, item.MoreLink, item.MobileMoreLink);
            }
            return GetSingle(item.ItemId);
        }
    }
}