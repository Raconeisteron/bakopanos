using System.Collections.Generic;

namespace ASPNETPortal.Security.Model
{
    public interface IPortalUsersService
    {
        int AddUser(string fullName, string email, string password);
        void DeleteUser(int userId);
        void UpdateUser(int userId, string email, string password);
        IEnumerable<PortalRole> GetRolesByUser(string email);
        PortalUser GetSingleUser(string email);
        IEnumerable<string> GetRoles(string email);
        string Login(string email, string password);
    }
}
