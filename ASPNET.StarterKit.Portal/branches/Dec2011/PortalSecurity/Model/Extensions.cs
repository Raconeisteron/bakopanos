using System;
using System.Data;

namespace ASPNETPortal.Security.Model
{
    internal static class Extensions
    {
        public static PortalRole ToPortalRole(this DataRow row)
        {
            var item = new PortalRole();
            item.Id = Convert.ToInt32(row["RoleId"]);
            item.Name = row["RoleName"] as string;
            return item;
        }

        public static PortalUser ToPortalUser(this DataRow row)
        {
            var item = new PortalUser();
            item.Id = Convert.ToInt32(row["UserId"]);
            item.Name = row["Name"] as string;
            item.Email = row["Email"] as string;
            return item;
        }
    }
}