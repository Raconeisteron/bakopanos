using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using ASPNET.StarterKit.Portal.Data;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    //*********************************************************************
    //
    // RolesDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // Users, Roles and security settings values within the Portal database.
    //
    //*********************************************************************
    public class SqlRolesDb : Db, IRolesDb
    {
        private readonly string _connectionString;

        public SqlRolesDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
            _connectionString = connectionString;
        }

        #region IRolesDb Members

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

        //*********************************************************************
        //
        // AddRole() Method <a name="AddRole"></a>
        //
        // The AddRole method creates a new security role for the specified portal,
        // and returns the new RoleID value.
        //
        // Other relevant sources:
        //     + <a href="AddRole.htm" style="color:green">AddRole Stored Procedure</a>
        //
        //*********************************************************************

        public int AddRole(int portalId, string roleName)
        {
            // Add Parameters to SPROC
            DbParameter parameterPortalID = CreateParameter("@PortalID", portalId);

            DbParameter parameterRoleName = CreateParameter("@RoleName", roleName);

            DbParameter parameterRoleID = CreateParameter("@RoleID");
            parameterRoleID.Direction = ParameterDirection.Output;
            parameterRoleID.Size = 4;

            // Execute the command
            ExecuteNonQuery("Portal_AddRole", CommandType.StoredProcedure, parameterPortalID, parameterRoleName,
                            parameterRoleID);

            // return the role id 
            return int.Parse(parameterRoleID.Value.ToString());
        }

        //*********************************************************************
        //
        // DeleteRole() Method <a name="DeleteRole"></a>
        //
        // The DeleteRole deletes the specified role from the portal database.
        //
        // Other relevant sources:
        //     + <a href="DeleteRole.htm" style="color:green">DeleteRole Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteRole(int roleId)
        {
            // Add Parameters to SPROC
            DbParameter parameterRoleID = CreateParameter("@RoleID", roleId);

            // Execute the command
            ExecuteNonQuery("Portal_DeleteRole", CommandType.StoredProcedure, parameterRoleID);
        }

        //*********************************************************************
        //
        // UpdateRole() Method <a name="UpdateRole"></a>
        //
        // The UpdateRole method updates the friendly name of the specified role.
        //
        // Other relevant sources:
        //     + <a href="UpdateRole.htm" style="color:green">UpdateRole Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateRole(int roleId, string roleName)
        {
            // Add Parameters to SPROC
            DbParameter parameterRoleID = CreateParameter("@RoleID", roleId);
            DbParameter parameterRoleName = CreateParameter("@RoleName", roleName);

            // Execute the command
            ExecuteNonQuery("Portal_UpdateRole", CommandType.StoredProcedure, parameterRoleID, parameterRoleName);
        }


        //
        // USER ROLES
        //

        //*********************************************************************
        //
        // GetRoleMembers() Method <a name="GetRoleMembers"></a>
        //
        // The GetRoleMembers method returns a list of all members in the specified
        // security role.
        //
        // Other relevant sources:
        //     + <a href="GetRoleMembers.htm" style="color:green">GetRoleMembers Stored Procedure</a>
        //
        //*********************************************************************

        public Collection<PortalUser> GetRoleMembers(int roleId)
        {
            // Add Parameters to SPROC
            DbParameter parameterRoleID = CreateParameter("@RoleID", roleId);
            parameterRoleID.Value = roleId;

            // Execute the command
            IDataReader reader = ExecuteReader("Portal_GetRoleMembership", CommandType.StoredProcedure, parameterRoleID);

            var list = new Collection<PortalUser>();

            while (reader.Read())
            {
                list.Add(reader.ToPortalUser());
            }

            return list;
        }

        //*********************************************************************
        //
        // AddUserRole() Method <a name="AddUserRole"></a>
        //
        // The AddUserRole method adds the user to the specified security role.
        //
        // Other relevant sources:
        //     + <a href="AddUserRole.htm" style="color:green">AddUserRole Stored Procedure</a>
        //
        //*********************************************************************

        public void AddUserRole(int roleId, int userId)
        {
            // Add Parameters to SPROC
            DbParameter parameterRoleID = CreateParameter("@RoleID", roleId);
            DbParameter parameterUserID = CreateParameter("@UserID", userId);

            // Execute the command
            ExecuteNonQuery("Portal_AddUserRole", CommandType.StoredProcedure, parameterRoleID, parameterUserID);
        }

        //*********************************************************************
        //
        // DeleteUserRole() Method <a name="DeleteUserRole"></a>
        //
        // The DeleteUserRole method deletes the user from the specified role.
        //
        // Other relevant sources:
        //     + <a href="DeleteUserRole.htm" style="color:green">DeleteUserRole Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteUserRole(int roleId, int userId)
        {
            // Add Parameters to SPROC
            DbParameter parameterRoleID = CreateParameter("@RoleID", roleId);
            DbParameter parameterUserID = CreateParameter("@UserID", userId);

            // Execute the command
            ExecuteNonQuery("Portal_DeleteUserRole", CommandType.StoredProcedure, parameterRoleID, parameterUserID);
        }


        //
        // USERS
        //

        //*********************************************************************
        //
        // GetUsers() Method <a name="GetUsers"></a>
        //
        // The GetUsers method returns returns the UserID, Name and Email for 
        // all registered users.
        //
        // Other relevant sources:
        //     + <a href="GetUsers.htm" style="color:green">GetUsers Stored Procedure</a>
        //
        //*********************************************************************

        public Collection<PortalUser> GetUsers()
        {
            // Execute the command
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