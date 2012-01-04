using System.Collections.Generic;

namespace ASPNETPortal.Security.Model
{
    public interface IPortalRolesService
    {
        IEnumerable<PortalRole> GetPortalRoles(int portalId);
        int AddRole(int portalId, string roleName);
        void DeleteRole(int roleId);
        void UpdateRole(int roleId, string roleName);
        IEnumerable<PortalUser> GetRoleMembers(int roleId);
        void AddUserRole(int roleId, int userId);
        void DeleteUserRole(int roleId, int userId);
        IEnumerable<PortalUser> GetUsers();
    }
}