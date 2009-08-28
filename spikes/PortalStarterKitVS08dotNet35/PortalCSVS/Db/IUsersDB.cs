using System;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal
{
    public interface IUsersDb
    {
        int AddUser(String fullName, String email, String password);
        void DeleteUser(int userId);
        void UpdateUser(int userId, String email, String password);
        DbDataReader GetRolesByUser(String email);
        DbDataReader GetSingleUser(String email);
        String[] GetRoles(String email);
        String Login(String email, String password);
    }
}