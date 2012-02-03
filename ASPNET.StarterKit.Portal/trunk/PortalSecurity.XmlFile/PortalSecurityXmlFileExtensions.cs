namespace ASPNET.StarterKit.Portal.XmlFile
{
    internal static class PortalSecurityXmlFileExtensions
    {
        public static PortalRole ToPortalRole(this SecurityDataSet.PortalRolesRow row, int portalId)
        {
            return new PortalRole
                       {
                           PortalId = portalId,
                           RoleId = row.RoleID,
                           RoleName = row.RoleName
                       };
        }

        public static PortalUser ToPortalUser(this SecurityDataSet.PortalUsersRow row)
        {
            return new PortalUser
                       {
                           UserId = row.UserID,
                           Email = row.Email,
                           Name = row.Name
                       }
                ;
        }
    }
}