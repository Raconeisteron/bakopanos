using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IUsersDb
    {
        int AddUser(String fullName, string email, string password);
        void DeleteUser(int userId);
        void UpdateUser(int userId, string email, string password);
        IDataReader GetRolesByUser(String email);
        IDataReader GetSingleUser(String email);
        String[] GetRoles(String email);
        string Login(String email, string password);
    }
}