using System;
using System.Data;
using System.Data.Common;
using Framework.Data;

namespace ASPNETPortal.Security.DataAccess
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// Users, Roles and security settings values within the Portal database.
    /// </summary>
    internal class RolesDb : IRolesDb
    {
        private readonly IDbHelper _db;

        public RolesDb(IDbHelper db)
        {
            _db = db;
        }

        #region ROLES

        /// <summary>
        /// The GetPortalRoles method returns a list of all role names for the 
        /// specified portal.
        /// </summary>
        public DataTable GetPortalRoles(int portalId)
        {
            DbParameter parameterPortalId = _db.CreateParameter("@PortalID", portalId);

            return _db.GetDataTable("Portal_GetPortalRoles", parameterPortalId);
        }

        /// <summary>
        /// The AddRole method creates a new security role for the specified portal,
        /// and returns the new RoleID value.
        /// </summary>
        public int AddRole(int portalId, String roleName)
        {
            DbParameter parameterRoleId = _db.CreateOutputParameter("@RoleID", DbType.Int32, 4);
            DbParameter parameterPortalId = _db.CreateParameter("@PortalID", portalId);
            DbParameter parameterRoleName = _db.CreateParameter("@RoleName", roleName);

            return _db.ExecuteNonQuery<int>("Portal_AddRole", parameterRoleId, parameterPortalId, parameterRoleName);
        }

        /// <summary>
        /// The DeleteRole deletes the specified role from the portal database.
        /// </summary>
        public void DeleteRole(int roleId)
        {
            DbParameter parameterRoleId = _db.CreateParameter("@RoleID", roleId);

            _db.ExecuteNonQuery("Portal_DeleteRole", parameterRoleId);
        }

        /// <summary>
        /// The UpdateRole method updates the friendly name of the specified role.
        /// </summary>
        public void UpdateRole(int roleId, String roleName)
        {
            DbParameter parameterRoleId = _db.CreateParameter("@RoleID", roleId);
            DbParameter parameterRoleName = _db.CreateParameter("@RoleName", roleName);

            _db.ExecuteNonQuery("Portal_UpdateRole", parameterRoleId, parameterRoleName);
        }

        #endregion

        #region USER ROLES

        /// <summary>
        /// The GetRoleMembers method returns a list of all members in the specified
        /// security role.
        /// </summary>
        public DataTable GetRoleMembers(int roleId)
        {
            var parameterRoleId = _db.CreateParameter("@RoleID",  roleId);

            return _db.GetDataTable("Portal_GetRoleMembership", parameterRoleId);
        }

        /// <summary>
        /// The AddUserRole method adds the user to the specified security role.
        /// </summary>
        public void AddUserRole(int roleId, int userId)
        {
            DbParameter parameterRoleId = _db.CreateParameter("@RoleID", roleId);
            DbParameter parameterUserId = _db.CreateParameter("@UserID", userId);

            _db.ExecuteNonQuery("Portal_AddUserRole", parameterRoleId, parameterUserId);
        }

        /// <summary>
        /// The DeleteUserRole method deletes the user from the specified role.
        /// </summary>
        public void DeleteUserRole(int roleId, int userId)
        {
            var parameterRoleId = _db.CreateParameter("@RoleID", roleId);
            var parameterUserId = _db.CreateParameter("@UserID", userId);

            _db.ExecuteNonQuery("Portal_DeleteUserRole", parameterRoleId, parameterUserId);
        }

        #endregion

        #region USERS

        /// <summary>
        /// The GetUsers method returns returns the UserID, Name and Email for 
        /// all registered users.
        /// </summary>
        public DataTable GetUsers()
        {
            return _db.GetDataTable("Portal_GetUsers");
        }

        #endregion
    }
}