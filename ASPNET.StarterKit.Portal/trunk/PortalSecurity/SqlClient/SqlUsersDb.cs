using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public int AddUser(String fullName, string email, string password)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_AddUser", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            DbParameter parameterFullName = CreateParameter("@Name", fullName);
            //command.Parameters.Add(parameterFullName);

            DbParameter parameterEmail = CreateParameter("@Email", email);
            //command.Parameters.Add(parameterFullName);

            DbParameter parameterPassword = CreateParameter("@Password", password);
            //command.Parameters.Add(parameterFullName);

            DbParameter parameterUserId = CreateParameter("@UserID");
            parameterUserId.Direction = ParameterDirection.Output;
            //command.Parameters.Add(parameterFullName);

            // Execute the command in a try/catch to catch duplicate username errors
            try
            {
                // Open the connection and execute the Command
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch
            {
                // failed to create a new user
                return -1;
            }
            finally
            {
                // Close the Connection
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return (int) parameterUserId.Value;
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
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_UpdateUser", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            DbParameter parameterUserId = CreateParameter("@UserID", userId);
            //command.Parameters.Add(parameterUserId);

            DbParameter parameterEmail = CreateParameter("@Email", email);
            //command.Parameters.Add(parameterEmail);

            DbParameter parameterPassword = CreateParameter("@Password", password);
            //command.Parameters.Add(parameterPassword);

            // Open the database connection and execute the command
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
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

        public IDataReader GetRolesByUser(String email)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetRolesByUser", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            DbParameter parameterEmail = CreateParameter("@Email", email);
            command.Parameters.Add(parameterEmail);

            // Open the database connection and execute the command
            connection.Open();
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        //*********************************************************************
        //
        // GetSingleUser Method
        //
        // The GetSingleUser method returns a SqlDataReader containing details
        // about a specific user from the Users database table.
        //
        //*********************************************************************

        public IDataReader GetSingleUser(String email)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetSingleUser", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            DbParameter parameterEmail = CreateParameter("@Email", email);
            //command.Parameters.Add(parameterEmail);

            // Open the database connection and execute the command
            connection.Open();
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
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
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetRolesByUser", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            DbParameter parameterEmail = CreateParameter("@Email", email);
            //command.Parameters.Add(parameterEmail);

            // Open the database connection and execute the command
            SqlDataReader dr;

            connection.Open();
            dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            // create a string array from the data
            var userRoles = new List<string>();

            while (dr.Read())
            {
                userRoles.Add(dr["RoleName"] as string);
            }

            dr.Close();

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
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_UserLogin", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            DbParameter parameterEmail = CreateParameter("@Email", email);
            //command.Parameters.Add(parameterEmail);

            DbParameter parameterPassword = CreateParameter("@Password",password);
            //command.Parameters.Add(parameterPassword);

            DbParameter parameterUserName = CreateParameter("@UserName");
            parameterUserName.Direction = ParameterDirection.Output;
            //command.Parameters.Add(parameterUserName);

            // Open the database connection and execute the command
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

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