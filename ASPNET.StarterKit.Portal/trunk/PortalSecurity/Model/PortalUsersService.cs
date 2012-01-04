using System.Collections.Generic;
using System.Data;
using System.Linq;
using ASPNETPortal.Security.DataAccess;

namespace ASPNETPortal.Security.Model
{
    internal class PortalUsersService : IPortalUsersService
    {
        private IUsersDb _usersDb;
        public PortalUsersService(IUsersDb usersDb)
        {
            _usersDb = usersDb;
        }

        public int AddUser(string fullName, string email, string password)
        {
            return _usersDb.AddUser(fullName, email,password);
        }

        public void DeleteUser(int userId)
        {
            _usersDb.DeleteUser(userId);
        }

        public void UpdateUser(int userId, string email, string password)
        {
            _usersDb.UpdateUser(userId,email,password);
        }

        public IEnumerable<PortalRole> GetRolesByUser(string email)
        {
            return from DataRow row in _usersDb.GetRolesByUser(email).Rows select row.ToPortalRole();
        }

        public PortalUser GetSingleUser(string email)
        {
            return _usersDb.GetSingleUser(email).ToPortalUser();
        }

        public IEnumerable<string> GetRoles(string email)
        {
            return _usersDb.GetRoles(email);
        }

        public string Login(string email, string password)
        {
            return _usersDb.Login(email, password);
        }
    }
}