using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.XmlFile
{
    public class XmlUsersDb : IUsersDb
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

        public IDataReader GetRolesByUser(string email)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetSingleUser(string email)
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