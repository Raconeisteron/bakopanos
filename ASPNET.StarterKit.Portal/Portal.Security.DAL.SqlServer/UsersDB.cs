using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Portal.Security.DAL.SqlServer
{
    /// <summary>
    /// The UsersDB class encapsulates all data logic necessary to add/login/query
    /// users within the Portal Users database.
    /// </summary>
    /// <remarks>
    /// The UsersDB class is only used when forms-based cookie
    /// authentication is enabled within the portal.  When windows based
    /// authentication is used instead, then either the Windows SAM or Active Directory
    /// is used to store and validate all username/password credentials.
    /// </remarks>
    internal class UsersDb : SqlDbHelper, IUsersDb
    {
        #region IUsersDb Members

        /// <summary>
        /// The AddUser method inserts a new user record into the "Users" database table.
        /// </summary>
        /// <returns>New user Id</returns>
        public int AddUser(string fullName, string email, string password)
        {
            int retValue;

            // Execute the command in a try/catch to catch duplicate username errors);
            try
            {
                retValue = CreateItem("Portal_AddUser", ReturnValueUserId(), InputName(fullName), InputEmail(email), InputPassword(password));
            }
            catch
            {
                // failed to create a new user
                return -1;
            }

            return retValue;
        }

        /// <summary>
        /// The DeleteUser method deleted a  user record from the "Users" database table.
        /// </summary>
        public void DeleteUser(int userId)
        {
            ExecuteNonQuery("Portal_DeleteUser", InputUserId(userId));
        }

        /// <summary>
        /// The UpdateUser method updates a  user record from the "Users" database table.
        /// </summary>
        public void UpdateUser(int userId, string email, string password)
        {
            ExecuteNonQuery("Portal_UpdateUser", InputUserId(userId), InputEmail(email), InputPassword(password));
        }

        public IDataReader GetRolesByUser(string email)
        {
            return GetItems("Portal_GetRolesByUser", InputEmail(email));         
        }

        /// <summary>
        /// The GetSingleUser method returns a IDataReader containing details
        // about a specific user from the Users database table.
        /// </summary>
        public IDataReader GetSingleUser(string email)
        {
            return GetSingleItem("Portal_GetSingleUser", InputEmail(email));
        }

        /// <summary>
        /// The GetRoles method returns a list of role names for the user.
        /// </summary>        
        public string[] GetRoles(string email)
        {
            IDataReader dr = GetItems("Portal_GetRolesByUser", InputEmail(email));

            // create a String array from the data
            var userRoles = new List<string>();

            while (dr.Read())
            {
                userRoles.Add(dr["RoleName"] as string);
            }

            dr.Close();

            // Return the String array of roles
            return userRoles.ToArray();
        }

        /// <summary>
        /// The Login method validates a email/password pair against credentials
        /// stored in the users database.  If the email/password pair is valid,
        /// the method returns user's name.
        /// </summary>        
        public string Login(string email, string password)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_UserLogin", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputEmail(email));

            var parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
            parameterPassword.Value = password;
            myCommand.Parameters.Add(parameterPassword);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserName);

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            if ((parameterUserName.Value != null) && (parameterUserName.Value != DBNull.Value))
            {
                return ((string) parameterUserName.Value).Trim();
            }
            return string.Empty;
        }

        /// <summary>
        /// The GetUsers method returns returns the UserID, Name and Email for 
        /// all registered users.
        /// </summary>        
        public IDataReader GetUsers()
        {
            return GetItems("Portal_GetUsers");            
        }

        #endregion
    }
}