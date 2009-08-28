using System;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal
{
    public interface IRolesDb
    {
        DbDataReader GetPortalRoles(int portalId);
        int AddRole(int portalId, String roleName);
        void DeleteRole(int roleId);
        void UpdateRole(int roleId, String roleName);
        DbDataReader GetRoleMembers(int roleId);
        void AddUserRole(int roleId, int userId);
        void DeleteUserRole(int roleId, int userId);
        DbDataReader GetUsers();
    }
}