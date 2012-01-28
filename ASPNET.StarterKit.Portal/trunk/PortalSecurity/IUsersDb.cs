using System;
using System.Collections.ObjectModel;

namespace ASPNET.StarterKit.Portal
{
    public interface IUsersDb
    {
        int AddUser(String fullName, string email, string password);
        void DeleteUser(int userId);
        void UpdateUser(int userId, string email, string password);
        Collection<PortalRole> GetRolesByUser(String email);
        PortalUserDetails GetSingleUser(String email);
        String[] GetRoles(String email);
        string Login(String email, string password);
    }
}