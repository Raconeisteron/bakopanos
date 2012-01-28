using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace ASPNET.StarterKit.Portal.XmlFile
{
    public class XmlRolesDb : IRolesDb
    {
        private readonly SecurityDataSet _dataSet = new SecurityDataSet();
        private readonly string _fileName;

        public XmlRolesDb(string fileName,IPortalServerUtility serverUtility)
        {
            // Retrieve the location of the XML configuration file
            _fileName = serverUtility.MapPath(fileName);
            _dataSet.ReadXml(_fileName);
        }

        #region IRolesDb Members

        public Collection<PortalRole> GetPortalRoles(int portalId)
        {
            return new Collection<PortalRole>(_dataSet.PortalRoles.Select(item => item.ToPortalRole(portalId)).ToList());
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
            int userId =
                _dataSet.PortalUserRoles.FirstOrDefault(item => item.Portal_RolesRow.RoleID == roleId).PortalUsersRow.
                    UserID;
            IEnumerable<SecurityDataSet.PortalUsersRow> users = _dataSet.PortalUsers.Where(item => item.UserID == userId);
            return new Collection<PortalUser>( users.Select(item => item.ToPortalUser()).ToList());
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
            IEnumerable<SecurityDataSet.PortalUsersRow> users = _dataSet.PortalUsers.OrderBy(item => item.Email);
            return new Collection<PortalUser>( users.Select(item => item.ToPortalUser()).ToList());
        }

        #endregion
    }
}