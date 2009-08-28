using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ASPNET.StarterKit.Portal
{
    public static class DbDataReaderExtensions
    {
        public static AnnouncementItem ToAnnouncement(this DbDataReader dr)
        {            
            var item = new AnnouncementItem();
            item.Title = (String)dr["Title"];
            item.MoreLink = (String)dr["MoreLink"];
            item.MobileMoreLink = (String)dr["MobileMoreLink"];
            item.Description = (String)dr["Description"];
            item.ExpireDate = ((DateTime)dr["ExpireDate"]);
            item.CreatedByUser = (String)dr["CreatedByUser"];
            item.CreatedDate = ((DateTime)dr["CreatedDate"]);
            return item;
        }

        public static ContactItem ToContactItem(this DbDataReader dr)
        {
            var item = new ContactItem();
            item.Name = (String)dr["Name"];
            item.Role = (String)dr["Role"];
            item.Email = (String)dr["Email"];
            item.Contact1 = (String)dr["Contact1"];
            item.Contact2 = (String)dr["Contact2"];
            item.CreatedByUser = (String)dr["CreatedByUser"];
            item.CreatedDate = ((DateTime)dr["CreatedDate"]);
            return item;
        }

    }
}
