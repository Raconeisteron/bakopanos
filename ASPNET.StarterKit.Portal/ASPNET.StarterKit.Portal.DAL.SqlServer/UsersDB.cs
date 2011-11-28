using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
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
    internal class UsersDb : IUsersDb
    {
        private readonly string _connectionString;

        public UsersDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region IUsersDb Members

        /// <summary>
        /// The AddUser method inserts a new user record into the "Users" database table.
        /// </summary>
        /// <returns>New user Id</returns>
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

            myCommand.AddParameterEmail(email);

            var parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
            parameterPassword.Value = password;
            myCommand.Parameters.Add(parameterPassword);

            SqlParameter parameterUserId = myCommand.AddParameterUserId();

            // Execute the command in a try/catch to catch duplicate username errors);
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

        /// <summary>
        /// The DeleteUser method deleted a  user record from the "Users" database table.
        /// </summary>
        public void DeleteUser(int userId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.AddParameterUserId(userId);

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        /// <summary>
        /// The UpdateUser method updates a  user record from the "Users" database table.
        /// </summary>
        public void UpdateUser(int userId, String email, String password)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;
            
            myCommand.AddParameterUserId(userId);
            myCommand.AddParameterEmail(email);

            var parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
            parameterPassword.Value = password;
            myCommand.Parameters.Add(parameterPassword);

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        public IDataReader GetRolesByUser(String email)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetRolesByUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.AddParameterEmail(email);

            // Open the database connection and execute the command
            myConnection.Open();
            IDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        /// <summary>
        /// The GetSingleUser method returns a IDataReader containing details
        // about a specific user from the Users database table.
        /// </summary>
        public IDataReader GetSingleUser(String email)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterEmail(email);

            // Open the database connection and execute the command
            myConnection.Open();
            IDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        /// <summary>
        /// The GetRoles method returns a list of role names for the user.
        /// </summary>        
        public String[] GetRoles(String email)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetRolesByUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterEmail(email);

            // Open the database connection and execute the command

            myConnection.Open();
            IDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

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

        /// <summary>
        /// The Login method validates a email/password pair against credentials
        /// stored in the users database.  If the email/password pair is valid,
        /// the method returns user's name.
        /// </summary>        
        public String Login(String email, String password)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UserLogin", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterEmail(email);

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
            return String.Empty;
        }

        /// <summary>
        /// The GetUsers method returns returns the UserID, Name and Email for 
        /// all registered users.
        /// </summary>        
        public IDataReader GetUsers()
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetUsers", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Open the database connection and execute the command
            myConnection.Open();
            IDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        #endregion
    }
}