using System;
using System.Collections.Generic;
using System.Data;

namespace ASPNETPortal.Security
{
    public interface IUsersDb
    {
        /// <summary>
        /// The AddUser method inserts a new user record into the "Users" database table.
        /// </summary>
        int AddUser(String fullName, String email, String password);

        /// <summary>
        /// The DeleteUser method deleted a  user record from the "Users" database table.
        /// </summary>
        void DeleteUser(int userId);

        /// <summary>
        /// The UpdateUser method deleted a  user record from the "Users" database table.
        /// </summary>
        void UpdateUser(int userId, String email, String password);

        /// <summary>
        /// The DeleteUser method deleted a  user record from the "Users" database table.
        /// </summary>
        DataTable GetRolesByUser(String email);

        /// <summary>
        /// The GetSingleUser method returns a SqlDataReader containing details
        /// about a specific user from the Users database table.
        /// </summary>
        DataRow GetSingleUser(String email);

        /// <summary>
        /// The GetRoles method returns a list of role names for the user.
        /// </summary>
        List<string> GetRoles(String email);

        /// <summary>
        /// The Login method validates a email/password pair against credentials
        /// stored in the users database.  If the email/password pair is valid,
        /// the method returns user's name.
        /// </summary>
        String Login(String email, String password);
    }
}