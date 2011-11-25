using System;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IRolesDB
    {
        SqlDataReader GetPortalRoles(int portalId);
        int AddRole(int portalId, String roleName);
        void DeleteRole(int roleId);
        void UpdateRole(int roleId, String roleName);
        SqlDataReader GetRoleMembers(int roleId);
        void AddUserRole(int roleId, int userId);
        void DeleteUserRole(int roleId, int userId);
        SqlDataReader GetUsers();
    }
}