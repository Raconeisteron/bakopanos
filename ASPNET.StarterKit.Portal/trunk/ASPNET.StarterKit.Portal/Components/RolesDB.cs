using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// Users, Roles and security settings values within the Portal database.
    /// </summary>
    public class RolesDb : DbHelper
    {
        #region ROLES
        /// <summary>
        /// The GetPortalRoles method returns a list of all role names for the 
        /// specified portal.
        /// </summary>
        public static DataTable GetPortalRoles(int portalId)
        {
            var parameterPortalId = CreateParameter("@PortalID", portalId);

            return GetDataTable("Portal_GetPortalRoles", parameterPortalId);
        }

        /// <summary>
        /// The AddRole method creates a new security role for the specified portal,
        /// and returns the new RoleID value.
        /// </summary>
        public static int AddRole(int portalId, String roleName)
        {
            var parameterRoleId = CreateOutputParameter("@RoleID");
            var parameterPortalId = CreateParameter("@PortalID", portalId);
            var parameterRoleName = CreateParameter("@RoleName", roleName);

            return ExecuteNonQuery<int>("Portal_AddRole", parameterRoleId, parameterPortalId, parameterRoleName);
        }

        /// <summary>
        /// The DeleteRole deletes the specified role from the portal database.
        /// </summary>
        public static void DeleteRole(int roleId)
        {
            var parameterRoleId = CreateParameter("@RoleID", roleId);

            ExecuteNonQuery("Portal_DeleteRole", parameterRoleId);
        }

        /// <summary>
        /// The UpdateRole method updates the friendly name of the specified role.
        /// </summary>
        public static void UpdateRole(int roleId, String roleName)
        {
            var parameterRoleId = CreateParameter("@RoleID", roleId);
            var parameterRoleName = CreateParameter("@RoleName", roleName);

            ExecuteNonQuery("Portal_UpdateRole", parameterRoleId, parameterRoleName);
        }
        
        #endregion

        #region USER ROLES
        /// <summary>
        /// The GetRoleMembers method returns a list of all members in the specified
        /// security role.
        /// </summary>
        public static DataTable GetRoleMembers(int roleId)
        {
            var parameterRoleId = new SqlParameter("@RoleID", SqlDbType.Int, 4) { Value = roleId };

            return GetDataTable("Portal_GetRoleMembership", parameterRoleId);
        }

        /// <summary>
        /// The AddUserRole method adds the user to the specified security role.
        /// </summary>
        public static void AddUserRole(int roleId, int userId)
        {
            var parameterRoleId = CreateParameter("@RoleID", roleId);
            var parameterUserId = CreateParameter("@UserID", userId);

            ExecuteNonQuery("Portal_AddUserRole", parameterRoleId, parameterUserId);
        }

        /// <summary>
        /// The DeleteUserRole method deletes the user from the specified role.
        /// </summary>
        public static void DeleteUserRole(int roleId, int userId)
        {
            var parameterRoleId = new SqlParameter("@RoleID", SqlDbType.Int, 4) { Value = roleId };
            var parameterUserId = new SqlParameter("@UserID", SqlDbType.Int, 4) { Value = userId };

            ExecuteNonQuery("Portal_DeleteUserRole", parameterRoleId, parameterUserId);
        }
        
        #endregion

        #region USERS
        /// <summary>
        /// The GetUsers method returns returns the UserID, Name and Email for 
        /// all registered users.
        /// </summary>
        public static DataTable GetUsers()
        {
            return GetDataTable("Portal_GetUsers");
        } 
        #endregion
    }
}