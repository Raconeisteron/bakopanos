using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.Security.DAL
{
    public interface IRolesDb
    {
        IDataReader GetPortalRoles(int portalId);
        int AddRole(int portalId, string roleName);
        void DeleteRole(int roleId);
        void UpdateRole(int roleId, string roleName);
        IDataReader GetRoleMembers(int roleId);
        void AddUserRole(int roleId, int userId);
        void DeleteUserRole(int roleId, int userId);
    }
}