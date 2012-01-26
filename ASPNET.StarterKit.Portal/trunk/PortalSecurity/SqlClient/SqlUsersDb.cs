using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    /// <summary>
    /// The UsersDB class encapsulates all data logic necessary to add/login/query
    /// users within the Portal Users database.
    ///
    /// Important Note: The UsersDB class is only used when forms-based cookie
    /// authentication is enabled within the portal.  When windows based
    /// authentication is used instead, then either the Windows SAM or Active Directory
    /// is used to store and validate all username/password credentials.
    /// </summary>
    public class SqlUsersDb : Db, IUsersDb
    {
        private readonly string _connectionString;

        public SqlUsersDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
            _connectionString = connectionString;
        }

        //*********************************************************************
        //
        // UsersDB.AddUser() Method <a name="AddUser"></a>
        //
        // The AddUser method inserts a new user record into the "Users" database table.
        //
        // Other relevant sources:
        //     + <a href="AddUser.htm" style="color:green">AddUser Stored Procedure</a>
        //
        //*********************************************************************

        #region IUsersDb Members

        public int AddUser(string fullName, string email, string password)
        {
            // Add Parameters to SPROC
            DbParameter parameterFullName = CreateParameter("@Name", fullName);

            DbParameter parameterEmail = CreateParameter("@Email", email);

            DbParameter parameterPassword = CreateParameter("@Password", password);

            DbParameter parameterUserId = CreateParameter("@UserID");
            parameterUserId.Direction = ParameterDirection.Output;
            parameterUserId.Size = 50;

            // Execute the command in a try/catch to catch duplicate username errors
            try
            {
                // Open the connection and execute the Command
                ExecuteNonQuery("Portal_AddUser", CommandType.StoredProcedure, parameterFullName, parameterEmail,
                                parameterPassword, parameterUserId);
            }
            catch
            {
                // failed to create a new user
                return -1;
            }
            return Int32.Parse(parameterUserId.Value.ToString());
        }

        //*********************************************************************
        //
        // UsersDB.DeleteUser() Method <a name="DeleteUser"></a>
        //
        // The DeleteUser method deleted a  user record from the "Users" database table.
        //
        // Other relevant sources:
        //     + <a href="DeleteUser.htm" style="color:green">DeleteUser Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteUser(int userId)
        {
            DbParameter parameterUserId = CreateParameter("@UserID", userId);

            ExecuteNonQuery("Portal_DeleteUser", CommandType.StoredProcedure, parameterUserId);
        }

        //*********************************************************************
        //
        // UsersDB.UpdateUser() Method <a name="DeleteUser"></a>
        //
        // The UpdateUser method deleted a  user record from the "Users" database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateUser.htm" style="color:green">UpdateUser Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateUser(int userId, string email, string password)
        {
            DbParameter parameterUserId = CreateParameter("@UserID", userId);

            DbParameter parameterEmail = CreateParameter("@Email", email);

            DbParameter parameterPassword = CreateParameter("@Password", password);

            ExecuteNonQuery("Portal_UpdateUser", CommandType.StoredProcedure, parameterUserId, parameterEmail,
                            parameterPassword);
        }

        //*********************************************************************
        //
        // UsersDB.GetRolesByUser() Method <a name="GetRolesByUser"></a>
        //
        // The DeleteUser method deleted a  user record from the "Users" database table.
        //
        // Other relevant sources:
        //     + <a href="GetRolesByUser.htm" style="color:green">GetRolesByUser Stored Procedure</a>
        //
        //*********************************************************************

        public Collection<PortalRole> GetRolesByUser(String email)
        {
            DbParameter parameterEmail = CreateParameter("@Email", email);

            IDataReader reader = ExecuteReader("Portal_GetRolesByUser", CommandType.StoredProcedure, parameterEmail);

            var list = new Collection<PortalRole>();
            var userDetails = new PortalUser();

            while (reader.Read())
            {
                userDetails = reader.ToPortalUser();
                list.Add(reader.ToPortalRole(userDetails.UserId));
            }

            return list;
        }

        //*********************************************************************
        //
        // GetSingleUser Method
        //
        // The GetSingleUser method returns a SqlDataReader containing details
        // about a specific user from the Users database table.
        //
        //*********************************************************************

        public PortalUserDetails GetSingleUser(String email)
        {
            // Add Parameters to SPROC
            DbParameter parameterEmail = CreateParameter("@Email", email);

            //Execute the command
            IDataReader reader = ExecuteReader("Portal_GetSingleUser", CommandType.StoredProcedure, parameterEmail);

            var user = new PortalUserDetails();

            if (reader.Read())
                user = reader.ToPortalUserDetails(email);

            // Return the datareader
            return user;
        }

        //*********************************************************************
        //
        // GetRoles() Method <a name="GetRoles"></a>
        //
        // The GetRoles method returns a list of role names for the user.
        //
        // Other relevant sources:
        //     + <a href="GetRolesByUser.htm" style="color:green">GetRolesByUser Stored Procedure</a>
        //
        //*********************************************************************

        public String[] GetRoles(String email)
        {
            // Add Parameters to SPROC
            DbParameter parameterEmail = CreateParameter("@Email", email);

            // Execute the command
            IDataReader reader = ExecuteReader("Portal_GetRolesByUser", CommandType.StoredProcedure, parameterEmail);

            // create a string array from the data
            var userRoles = new List<String>();


            while (reader.Read())
            {
                userRoles.Add(reader["RoleName"] as string);
            }

            // Return the string array of roles
            return userRoles.ToArray();
        }

        //*********************************************************************
        //
        // UsersDB.Login() Method <a name="Login"></a>
        //
        // The Login method validates a email/password pair against credentials
        // stored in the users database.  If the email/password pair is valid,
        // the method returns user's name.
        //
        // Other relevant sources:
        //     + <a href="UserLogin.htm" style="color:green">UserLogin Stored Procedure</a>
        //
        //*********************************************************************

        public string Login(String email, string password)
        {
            // Add Parameters to SPROC
            DbParameter parameterEmail = CreateParameter("@Email", email);

            DbParameter parameterPassword = CreateParameter("@Password", password);

            DbParameter parameterUserName = CreateParameter("@UserName");
            parameterUserName.Direction = ParameterDirection.Output;
            parameterUserName.Size = 50;

            // Execute the command
            ExecuteNonQuery("Portal_UserLogin", CommandType.StoredProcedure, parameterEmail, parameterPassword,
                            parameterUserName);

            if ((parameterUserName.Value != null) && (parameterUserName.Value != DBNull.Value))
            {
                return ((String) parameterUserName.Value).Trim();
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion
    }
}