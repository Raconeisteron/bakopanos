using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public static class PortalSecurityExtensions
    {
        public static PortalRole ToPortalRole(this IDataRecord record, int portalId)
        {
            var portalRole = new PortalRole
                                 {
                                     PortalId = portalId,
                                     RoleId = Convert.ToInt32(record["RoleID"]),
                                     RoleName = record["RoleName"] as string,
                                 };
            return portalRole;
        }
    }
}