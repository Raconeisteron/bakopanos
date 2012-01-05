using System;
using System.Data;

namespace ASPNETPortal.Security.DataAccess
{
    internal interface IRolesDb
    {
        /// <summary>
        /// The GetPortalRoles method returns a list of all role names for the 
        /// specified portal.
        /// </summary>
        DataTable GetPortalRoles(int portalId);

        /// <summary>
        /// The AddRole method creates a new security role for the specified portal,
        /// and returns the new RoleID value.
        /// </summary>
        int AddRole(int portalId, String roleName);

        /// <summary>
        /// The DeleteRole deletes the specified role from the portal database.
        /// </summary>
        void DeleteRole(int roleId);

        /// <summary>
        /// The UpdateRole method updates the friendly name of the specified role.
        /// </summary>
        void UpdateRole(int roleId, String roleName);

        /// <summary>
        /// The GetRoleMembers method returns a list of all members in the specified
        /// security role.
        /// </summary>
        DataTable GetRoleMembers(int roleId);

        /// <summary>
        /// The AddUserRole method adds the user to the specified security role.
        /// </summary>
        void AddUserRole(int roleId, int userId);

        /// <summary>
        /// The DeleteUserRole method deletes the user from the specified role.
        /// </summary>
        void DeleteUserRole(int roleId, int userId);

        /// <summary>
        /// The GetUsers method returns returns the UserID, Name and Email for 
        /// all registered users.
        /// </summary>
        DataTable GetUsers();
    }
}