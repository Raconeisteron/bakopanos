using System.Collections.Generic;
using System.Data;
using System.Linq;
using ASPNETPortal.Security.DataAccess;

namespace ASPNETPortal.Security.Model
{
    internal class PortalRolesService : IPortalRolesService
    {
        private readonly IRolesDb _rolesDb;

        public PortalRolesService(IRolesDb rolesDb)
        {
            _rolesDb = rolesDb;
        }

        public IEnumerable<PortalRole> GetPortalRoles(int portalId)
        {
            return from DataRow row in _rolesDb.GetPortalRoles(portalId).Rows select row.ToPortalRole();
        }

        public int AddRole(int portalId, string roleName)
        {
            return _rolesDb.AddRole(portalId, roleName);
        }

        public void DeleteRole(int roleId)
        {
            _rolesDb.DeleteRole(roleId);
        }

        public void UpdateRole(int roleId, string roleName)
        {
            _rolesDb.UpdateRole(roleId, roleName);
        }

        public IEnumerable<PortalUser> GetRoleMembers(int roleId)
        {
            return from DataRow row in _rolesDb.GetRoleMembers(roleId).Rows select row.ToPortalUser();
        }

        public void AddUserRole(int roleId, int userId)
        {
            _rolesDb.AddUserRole(roleId, userId);
        }

        public void DeleteUserRole(int roleId, int userId)
        {
            _rolesDb.DeleteUserRole(roleId,userId);
        }

        public IEnumerable<PortalUser> GetUsers()
        {
            return from DataRow row in _rolesDb.GetUsers().Rows select row.ToPortalUser();
        }
    }
}