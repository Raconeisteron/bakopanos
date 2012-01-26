using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraRolesDb : IRolesDb
    {
        #region IRolesDb Members

        public Collection<PortalRole> GetPortalRoles(int portalId)
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

        public Collection<PortalUser> GetRoleMembers(int roleId)
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

        public Collection<PortalUser> GetUsers()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}