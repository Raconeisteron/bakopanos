using System;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.Components
{
    public interface IUsersDB
    {
        int AddUser(String fullName, String email, String password);
        void DeleteUser(int userId);
        void UpdateUser(int userId, String email, String password);
        SqlDataReader GetRolesByUser(String email);
        SqlDataReader GetSingleUser(String email);
        String[] GetRoles(String email);
        String Login(String email, String password);
    }
}