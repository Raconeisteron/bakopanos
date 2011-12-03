using System.Data;

namespace Portal.Security.Data.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// Users, Roles and security settings values within the Portal database.
    /// </summary>
    internal class RolesDb : SqlDbHelper, IRolesDb
    {
        #region IRolesDb Members

        /// <summary>
        /// The GetPortalRoles method returns a list of all role names for the 
        /// specified portal.
        /// </summary>        
        public IDataReader GetPortalRoles(int portalId)
        {
            return GetItems("Portal_GetPortalRoles", InputPortalId(portalId));
        }

        /// <summary>
        /// The AddRole method creates a new security role for the specified portal,
        /// and returns the new RoleID value.
        /// </summary>
        public int AddRole(int portalId, string roleName)
        {
            return CreateItem("Portal_AddRole", ReturnValueRoleId(), InputRoleName(roleName));
        }

        /// <summary>
        /// // The DeleteRole deletes the specified role from the portal database.
        /// </summary>
        public void DeleteRole(int roleId)
        {
            ExecuteNonQuery("Portal_DeleteRole", InputRoleId(roleId));
        }

        /// <summary>
        /// The UpdateRole method updates the friendly name of the specified role.
        /// </summary>        
        public void UpdateRole(int roleId, string roleName)
        {
            ExecuteNonQuery("Portal_UpdateRole", InputRoleId(roleId), InputRoleName(roleName));
        }


        //
        // USER ROLES
        //

        /// <summary>
        /// The GetRoleMembers method returns a list of all members in the specified
        // security role.
        /// </summary>        
        public IDataReader GetRoleMembers(int roleId)
        {
            return GetItems("Portal_GetRoleMembership", InputRoleId(roleId));
        }

        /// <summary>
        /// The AddUserRole method adds the user to the specified security role.
        /// </summary>
        public void AddUserRole(int roleId, int userId)
        {
            ExecuteNonQuery("Portal_AddUserRole", InputRoleId(roleId), InputUserId(userId));
        }

        /// <summary>
        /// The DeleteUserRole method deletes the user from the specified role.
        /// </summary>
        public void DeleteUserRole(int roleId, int userId)
        {
            ExecuteNonQuery("Portal_DeleteUserRole", InputRoleId(roleId), InputUserId(userId));
        }

        #endregion
    }
}