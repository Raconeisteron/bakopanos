using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
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
    public class UsersDb : DbHelper
    {
        /// <summary>
        /// The AddUser method inserts a new user record into the "Users" database table.
        /// </summary>
        public static int AddUser(String fullName, String email, String password)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_AddUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterFullName = new SqlParameter("@Name", SqlDbType.NVarChar, 50) {Value = fullName};
            myCommand.Parameters.Add(parameterFullName);

            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
            myCommand.Parameters.Add(parameterEmail);

            var parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50) {Value = password};
            myCommand.Parameters.Add(parameterPassword);

            var parameterUserId = new SqlParameter("@UserID", SqlDbType.Int) {Direction = ParameterDirection.Output};
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

        /// <summary>
        /// The DeleteUser method deleted a  user record from the "Users" database table.
        /// </summary>
        public static void DeleteUser(int userId)
        {
            var parameterUserId = new SqlParameter("@UserID", SqlDbType.Int) {Value = userId};
           
            ExecuteNonQuery("Portal_DeleteUser", parameterUserId);
        }

       /// <summary>
        /// The UpdateUser method deleted a  user record from the "Users" database table.
       /// </summary>
        public static void UpdateUser(int userId, String email, String password)
       {
           var parameterUserId = new SqlParameter("@UserID", SqlDbType.Int) {Value = userId};
           var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
           var parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50) {Value = password};

           ExecuteNonQuery("Portal_UpdateUser", parameterUserId, parameterEmail, parameterPassword);
       }

        /// <summary>
        /// The DeleteUser method deleted a  user record from the "Users" database table.
        /// </summary>
        public static DataTable GetRolesByUser(String email)
        {
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
            
            return GetDataTable("Portal_GetRolesByUser", parameterEmail);
        }

        /// <summary>
        /// The GetSingleUser method returns a SqlDataReader containing details
        /// about a specific user from the Users database table.
        /// </summary>
        public static DataRow GetSingleUser(String email)
        {
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
            
            return GetDataRow("Portal_GetSingleUser", parameterEmail);
        }

        /// <summary>
        /// The GetRoles method returns a list of role names for the user.
        /// </summary>
        public static String[] GetRoles(String email)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetRolesByUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
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

        /// <summary>
        /// The Login method validates a email/password pair against credentials
        /// stored in the users database.  If the email/password pair is valid,
        /// the method returns user's name.
        /// </summary>
        public static String Login(String email, String password)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_UserLogin", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
            myCommand.Parameters.Add(parameterEmail);

            var parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50) {Value = password};
            myCommand.Parameters.Add(parameterPassword);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100)
                                        {Direction = ParameterDirection.Output};
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
    }
}