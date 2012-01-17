using System;
using System.Collections.Generic;
using System.Data;

namespace ASPNET.StarterKit.Portal.XmlFile
{
    public class XmlRolesDb : IRolesDb
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

        public IDataReader GetRoleMembers(int roleId)
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

        public IDataReader GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}