using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IUsersDB
    {
        int AddUser(String fullName, String email, String password);
        void DeleteUser(int userId);
        void UpdateUser(int userId, String email, String password);
        IDataReader GetRolesByUser(String email);
        IDataReader GetSingleUser(String email);
        String[] GetRoles(String email);
        String Login(String email, String password);
    }
}