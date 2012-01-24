using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public static class PortalSecurityExtensions
    {
        public static PortalRole ToPortalRole(this IDataRecord record, int portalId)
        {
            var item = new PortalRole
                           {
                               PortalId = portalId,
                               RoleId = Convert.ToInt32(record["RoleID"]),
                               RoleName = record["RoleName"] as string,
                           };
            return item;
        }


        public static PortalUser ToPortalUser(this IDataRecord record)
        {
            var item = new PortalUser
                           {
                               UserId = Convert.ToInt32(record["UserID"]),
                               Name = record["Name"] as string,
                               Email = record["Email"] as string
                           };
            return item;
        }

        public static PortalUserDetails ToPortalUserDetails(this IDataRecord record, string email)
        {
            var item = new PortalUserDetails
                            {
                                UserId = Convert.ToInt32(record["UserID"]),
                                Name = record["Name"] as string,
                                Email = email,//record["Email"] as string,
                                Password = record["Password"] as string
                            };
            return item;
        }
    }
}