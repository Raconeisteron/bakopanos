using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ASPNET.StarterKit.Portal.XmlFile
{
    internal static class PortalSecurityXmlFileExtensions
    {
        public static PortalRole ToPortalRole(this SecurityDataSet.PortalRolesRow row, int portalId)
        {
            return new PortalRole
                       {
                           PortalId = portalId,
                           RoleId = row.RoleID,
                           RoleName = row.RoleName
                       };
        }

        public static PortalUser ToPortalUser(this SecurityDataSet.PortalUsersRow row)
        {
            return new PortalUser
                       {
                           UserId = row.UserID,
                           Email = row.Email,
                           Name = row.Name
                       }
                ;
        }

    }

    public class XmlRolesDb : IRolesDb
    {
        private readonly SecurityDataSet _dataSet = new SecurityDataSet();
        private string _fileName;

        public XmlRolesDb(string fileName)
        {   
            _fileName = fileName;
            // Retrieve the location of the XML configuration file
            if (HttpContext.Current != null)
            {
                _fileName = HttpContext.Current.Server.MapPath(fileName);
            }
            _dataSet.ReadXml(_fileName);
        }

        public List<PortalRole> GetPortalRoles(int portalId)
        {
            return _dataSet.PortalRoles.Select(item => item.ToPortalRole(portalId)).ToList();
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
            int userId = _dataSet.PortalUserRoles.FirstOrDefault(item => item.Portal_RolesRow.RoleID == roleId).PortalUsersRow.UserID;
            IEnumerable<SecurityDataSet.PortalUsersRow> users = _dataSet.PortalUsers.Where(item => item.UserID == userId);
            return users.Select(item => item.ToPortalUser()).ToList();
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