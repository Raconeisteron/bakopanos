using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    public interface IRolesDb
    {
        List<PortalRole> GetPortalRoles(int portalId);
        int AddRole(int portalId, string roleName);
        void DeleteRole(int roleId);
        void UpdateRole(int roleId, string roleName);
        List<PortalUser> GetRoleMembers(int roleId);
        void AddUserRole(int roleId, int userId);
        void DeleteUserRole(int roleId, int userId);
        List<PortalUser> GetUsers();
    }
}