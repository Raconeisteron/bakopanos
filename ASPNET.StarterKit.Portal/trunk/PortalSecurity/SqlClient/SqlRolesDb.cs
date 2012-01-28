using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using ASPNET.StarterKit.Portal.Data;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    /// <summary>
    /// Encapsulates all data logic necessary to add/query/delete
    /// Users, Roles and security settings values within the Portal
    /// </summary>
    public class SqlRolesDb : Db, IRolesDb
    {
        public SqlRolesDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
        }

        #region ROLES

        public Collection<PortalRole> GetPortalRoles(int portalId)
        {
            DbParameter parameterPortalId = CreateParameter("@PortalID", portalId);

            IDataReader reader = ExecuteReader("Portal_GetPortalRoles", CommandType.StoredProcedure, parameterPortalId);

            var list = new Collection<PortalRole>();

            while (reader.Read())
            {
                list.Add(reader.ToPortalRole(portalId));
            }

            return list;
        }

        public int AddRole(int portalId, string roleName)
        {
            DbParameter parameterPortalId = CreateParameter("@PortalID", portalId);
            DbParameter parameterRoleName = CreateParameter("@RoleName", roleName);
            DbParameter parameterRoleId = CreateParameter("@RoleID");
            parameterRoleId.Direction = ParameterDirection.Output;
            parameterRoleId.Size = 4;

            ExecuteNonQuery("Portal_AddRole", CommandType.StoredProcedure,
                            parameterPortalId, parameterRoleName, parameterRoleId);

            return int.Parse(parameterRoleId.Value.ToString());
        }

        public void DeleteRole(int roleId)
        {
            DbParameter parameterRoleId = CreateParameter("@RoleID", roleId);

            ExecuteNonQuery("Portal_DeleteRole", CommandType.StoredProcedure, parameterRoleId);
        }

        /// <summary>
        /// Updates the friendly name of the specified role
        /// </summary>
        public void UpdateRole(int roleId, string roleName)
        {
            DbParameter parameterRoleId = CreateParameter("@RoleID", roleId);
            DbParameter parameterRoleName = CreateParameter("@RoleName", roleName);

            ExecuteNonQuery("Portal_UpdateRole", CommandType.StoredProcedure, parameterRoleId, parameterRoleName);
        }

        #endregion

        #region USER ROLES

        /// <summary>
        /// Returns a list of all members in the specified security role
        /// </summary>
        public Collection<PortalUser> GetRoleMembers(int roleId)
        {
            DbParameter parameterRoleId = CreateParameter("@RoleID", roleId);
            parameterRoleId.Value = roleId;

            IDataReader reader = ExecuteReader("Portal_GetRoleMembership", CommandType.StoredProcedure, parameterRoleId);

            var list = new Collection<PortalUser>();

            while (reader.Read())
            {
                list.Add(reader.ToPortalUser());
            }

            return list;
        }

        /// <summary>
        /// Adds the user to the specified security role
        /// </summary>
        public void AddUserRole(int roleId, int userId)
        {
            DbParameter parameterRoleId = CreateParameter("@RoleID", roleId);
            DbParameter parameterUserId = CreateParameter("@UserID", userId);

            ExecuteNonQuery("Portal_AddUserRole", CommandType.StoredProcedure,
                            parameterRoleId, parameterUserId);
        }

        /// <summary>
        /// Deletes the user from the specified role
        /// </summary>
        public void DeleteUserRole(int roleId, int userId)
        {
            DbParameter parameterRoleId = CreateParameter("@RoleID", roleId);
            DbParameter parameterUserId = CreateParameter("@UserID", userId);

            ExecuteNonQuery("Portal_DeleteUserRole", CommandType.StoredProcedure, parameterRoleId, parameterUserId);
        }

        #endregion

        #region USERS

        /// <summary>
        /// Returns all registered users
        /// </summary>
        public Collection<PortalUser> GetUsers()
        {
            IDataReader reader = ExecuteReader("Portal_GetUsers", CommandType.StoredProcedure);

            var list = new Collection<PortalUser>();

            while (reader.Read())
            {
                list.Add(reader.ToPortalUser());
            }

            return list;
        }

        #endregion
    }
}