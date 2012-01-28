using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using ASPNET.StarterKit.Portal.Data;

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
        public SqlUsersDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
        }

        #region IUsersDb Members

        public int AddUser(string fullName, string email, string password)
        {
            DbParameter parameterFullName = CreateParameter("@Name", fullName);
            DbParameter parameterEmail = CreateParameter("@Email", email);
            DbParameter parameterPassword = CreateParameter("@Password", password);

            DbParameter parameterUserId = CreateParameter("@UserID");
            parameterUserId.Direction = ParameterDirection.Output;
            parameterUserId.Size = 50;

            // Execute the command in a try/catch to catch duplicate username errors
            try
            {
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

        public void DeleteUser(int userId)
        {
            DbParameter parameterUserId = CreateParameter("@UserID", userId);

            ExecuteNonQuery("Portal_DeleteUser", CommandType.StoredProcedure, parameterUserId);
        }

        public void UpdateUser(int userId, string email, string password)
        {
            DbParameter parameterUserId = CreateParameter("@UserID", userId);

            DbParameter parameterEmail = CreateParameter("@Email", email);

            DbParameter parameterPassword = CreateParameter("@Password", password);

            ExecuteNonQuery("Portal_UpdateUser", CommandType.StoredProcedure, parameterUserId, parameterEmail,
                            parameterPassword);
        }

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

        public PortalUserDetails GetSingleUser(String email)
        {
            DbParameter parameterEmail = CreateParameter("@Email", email);

            IDataReader reader = ExecuteReader("Portal_GetSingleUser", CommandType.StoredProcedure, parameterEmail);

            while (reader.Read())
            {
                return reader.ToPortalUserDetails(email);
            }
            return null;
        }

        /// <summary>
        /// returns a list of role names for the user
        /// </summary>
        public String[] GetRoles(String email)
        {
            DbParameter parameterEmail = CreateParameter("@Email", email);

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

        /// <summary>
        /// Validates a email/password pair against credentials
        /// stored in the users database.  
        /// </summary>
        /// <returns>If the email/password pair is valid,
        /// the method returns user's name otherwise an empty string</returns>
        public string Login(String email, string password)
        {
            // Add Parameters to SPROC
            DbParameter parameterEmail = CreateParameter("@Email", email);

            DbParameter parameterPassword = CreateParameter("@Password", password);

            DbParameter parameterUserName = CreateParameter("@UserName");
            parameterUserName.Direction = ParameterDirection.Output;
            parameterUserName.Size = 50;

            ExecuteNonQuery("Portal_UserLogin", CommandType.StoredProcedure, parameterEmail, parameterPassword,
                            parameterUserName);

            if ((parameterUserName.Value != null) && (parameterUserName.Value != DBNull.Value))
            {
                return ((String) parameterUserName.Value).Trim();
            }
            return String.Empty;
        }

        #endregion
    }
}