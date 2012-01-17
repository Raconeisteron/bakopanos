using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ASPNET.StarterKit.Portal.XmlFile
{
    public class XmlRolesDb : IRolesDb
    {
        private readonly SecurityDataSet _dataSet = new SecurityDataSet();

        public XmlRolesDb(string fileName)
        {
            // Retrieve the location of the XML configuration file
            fileName = HttpContext.Current.Server.MapPath(fileName);
            _dataSet.ReadXml(fileName);
        }

        public List<PortalRole> GetPortalRoles(int portalId)
        {

            return _dataSet.PortalRoles.Select(item=> new PortalRole
                                                          {
                                                              PortalId = portalId,
                                                              RoleId = item.RoleID,
                                                              RoleName = item.RoleName
                                                          }).ToList();
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