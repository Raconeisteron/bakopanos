using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ASPNET.StarterKit.Portal
{
    public interface IRolesDb
    {
        Collection<PortalRole> GetPortalRoles(int portalId);
        int AddRole(int portalId, string roleName);
        void DeleteRole(int roleId);
        void UpdateRole(int roleId, string roleName);
        Collection<PortalUser> GetRoleMembers(int roleId);
        void AddUserRole(int roleId, int userId);
        void DeleteUserRole(int roleId, int userId);
        Collection<PortalUser> GetUsers();
    }
}