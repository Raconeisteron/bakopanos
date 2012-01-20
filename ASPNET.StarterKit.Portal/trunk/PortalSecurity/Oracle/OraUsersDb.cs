using System;
using System.Data;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraUsersDb : IUsersDb
    {
        #region IUsersDb Members

        public int AddUser(string fullName, string email, string password)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(int userId, string email, string password)
        {
            throw new NotImplementedException();
        }

        public List<PortalRole> GetRolesByUser(string email)
        {
            throw new NotImplementedException();
        }

        public PortalUser GetSingleUser(string email)
        {
            throw new NotImplementedException();
        }

        public string[] GetRoles(string email)
        {
            throw new NotImplementedException();
        }

        public string Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}