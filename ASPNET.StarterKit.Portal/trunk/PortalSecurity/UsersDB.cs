using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Framework.Data;

namespace ASPNETPortal
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
    internal class UsersDb : IUsersDb
    {
        private readonly IDbHelper _db;

        public UsersDb(IDbHelper db)
        {
            _db = db;
        }

        #region IUsersDb Members

        /// <summary>
        /// The AddUser method inserts a new user record into the "Users" database table.
        /// </summary>
        public int AddUser(String fullName, String email, String password)
        {
            var parameterUserId = new SqlParameter("@UserID", SqlDbType.Int) {Direction = ParameterDirection.Output};

            var parameterFullName = new SqlParameter("@Name", SqlDbType.NVarChar, 50) {Value = fullName};
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
            var parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50) {Value = password};

            // Execute the command in a try/catch to catch duplicate username errors
            try
            {
                return _db.ExecuteNonQuery<int>("Portal_AddUser", parameterUserId, parameterFullName, parameterEmail,
                                                parameterPassword);
            }
            catch
            {
                // failed to create a new user
                return -1;
            }
        }

        /// <summary>
        /// The DeleteUser method deleted a  user record from the "Users" database table.
        /// </summary>
        public void DeleteUser(int userId)
        {
            var parameterUserId = new SqlParameter("@UserID", SqlDbType.Int) {Value = userId};

            _db.ExecuteNonQuery("Portal_DeleteUser", parameterUserId);
        }

        /// <summary>
        /// The UpdateUser method deleted a  user record from the "Users" database table.
        /// </summary>
        public void UpdateUser(int userId, String email, String password)
        {
            var parameterUserId = new SqlParameter("@UserID", SqlDbType.Int) {Value = userId};
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
            var parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50) {Value = password};

            _db.ExecuteNonQuery("Portal_UpdateUser", parameterUserId, parameterEmail, parameterPassword);
        }

        /// <summary>
        /// The DeleteUser method deleted a  user record from the "Users" database table.
        /// </summary>
        public DataTable GetRolesByUser(String email)
        {
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};

            return _db.GetDataTable("Portal_GetRolesByUser", parameterEmail);
        }

        /// <summary>
        /// The GetSingleUser method returns a SqlDataReader containing details
        /// about a specific user from the Users database table.
        /// </summary>
        public DataRow GetSingleUser(String email)
        {
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};

            return _db.GetDataRow("Portal_GetSingleUser", parameterEmail);
        }

        /// <summary>
        /// The GetRoles method returns a list of role names for the user.
        /// </summary>
        public List<string> GetRoles(String email)
        {
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};

            DataTable userRoles = _db.GetDataTable("Portal_GetRolesByUser", parameterEmail);

            // Return the String array of roles
            return userRoles.Rows.Cast<DataRow>().Select(userRole => userRole["RoleName"] as string).ToList();
        }

        /// <summary>
        /// The Login method validates a email/password pair against credentials
        /// stored in the users database.  If the email/password pair is valid,
        /// the method returns user's name.
        /// </summary>
        public String Login(String email, String password)
        {
            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100)
                                        {Direction = ParameterDirection.Output};
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
            var parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 50) {Value = password};

            _db.ExecuteNonQuery<string>("Portal_UserLogin", parameterUserName, parameterEmail, parameterPassword);

            if ((parameterUserName.Value != null) && (parameterUserName.Value != DBNull.Value))
            {
                return ((String) parameterUserName.Value).Trim();
            }
            return String.Empty;
        }

        #endregion
    }
}