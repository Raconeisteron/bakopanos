using System;
using System.Data;
using Portal.Modules.Data;

namespace Portal.Modules.Model
{
    public class AnnouncementRepository : Repository<Announcement>
    {
        private readonly IAnnouncementDb _db;

        public AnnouncementRepository(IAnnouncementDb db)
            : base(db)
        {
            _db = db;
        }

        protected override Announcement ToItem(IDataRecord dataRecord)
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

        public override Announcement Save(Announcement item)
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