using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraRolesDb:IRolesDb
    {
        public List<PortalRole> GetPortalRoles(int portalId)
        {
            throw new NotImplementedException();
        }

        public int AddRole(int portalId, string roleName)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRole(int roleId, string roleName)
        {
            throw new NotImplementedException();
        }

        public List<PortalUser> GetRoleMembers(int roleId)
        {
            throw new NotImplementedException();
        }

        public void AddUserRole(int roleId, int userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserRole(int roleId, int userId)
        {
            throw new NotImplementedException();
        }

        public List<PortalUser> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}