using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    //*********************************************************************
    //
    // UsersDB Class
    //
    // The UsersDB class encapsulates all data logic necessary to add/login/query
    // users within the Portal Users database.
    //
    // Important Note: The UsersDB class is only used when forms-based cookie
    // authentication is enabled within the portal.  When windows based
    // authentication is used instead, then either the Windows SAM or Active Directory
    // is used to store and validate all username/password credentials.
    //
    //*********************************************************************

    internal class UsersDB : IUsersDB
    {
        private readonly string _connectionString;

        public UsersDB(string connectionString)
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

        #region IUsersDB Members

        public int AddUser(String fullName, String email, String password)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_AddUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterFullName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
            parameterFullName.Value = fullName;
            myCommand.Parameters.Add(parameterFullName);

            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            var parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
            parameterPassword.Value = password;
            myCommand.Parameters.Add(parameterPassword);

            var parameterUserId = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserId.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserId);

            // Execute the command in a try/catch to catch duplicate username errors
            try
            {
                // Open the connection and execute the Command
                myConnection.Open();
                myCommand.ExecuteNonQuery();
            }
            catch
            {
                // failed to create a new user
                return -1;
            }
            finally
            {
                // Close the Connection
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
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
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            var parameterUserId = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserId.Value = userId;
            myCommand.Parameters.Add(parameterUserId);

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
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

        public void UpdateUser(int userId, String email, String password)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            var parameterUserId = new SqlParameter("@UserID", SqlDbType.Int);
            parameterUserId.Value = userId;
            myCommand.Parameters.Add(parameterUserId);

            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            var parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
            parameterPassword.Value = password;
            myCommand.Parameters.Add(parameterPassword);

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
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

        public SqlDataReader GetRolesByUser(String email)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetRolesByUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            // Open the database connection and execute the command
            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

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

        public SqlDataReader GetSingleUser(String email)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            // Open the database connection and execute the command
            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

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
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetRolesByUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            // Open the database connection and execute the command
            SqlDataReader dr;

            myConnection.Open();
            dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // create a String array from the data
            var userRoles = new ArrayList();

            while (dr.Read())
            {
                userRoles.Add(dr["RoleName"]);
            }

            dr.Close();

            // Return the String array of roles
            return (String[]) userRoles.ToArray(typeof (String));
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

        public String Login(String email, String password)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UserLogin", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

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