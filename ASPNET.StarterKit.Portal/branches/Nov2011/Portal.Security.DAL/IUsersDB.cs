using System.Data;

namespace Portal.Security.Data
{
    public interface IUsersDb
    {
        int AddUser(string fullName, string email, string password);
        void DeleteUser(int userId);
        void UpdateUser(int userId, string email, string password);
        IDataReader GetRolesByUser(string email);
        IDataReader GetSingleUser(string email);
        string[] GetRoles(string email);
        string Login(string email, string password);
        IDataReader GetUsers();
    }
}