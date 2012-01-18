using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
    public class SqlRolesDb : IRolesDb
    {
        private readonly string _connectionString;

        public SqlRolesDb(string connectionString)
        {
            _connectionString = connectionString;
        }


        //*********************************************************************
        //
        // GetPortalRoles() Method <a name="GetPortalRoles"></a>
        //
        // The GetPortalRoles method returns a list of all role names for the 
        // specified portal.
        //
        // Other relevant sources:
        //     + <a href="GetRolesByUser.htm" style="color:green">GetPortalRoles Stored Procedure</a>
        //
        //*********************************************************************

        #region IRolesDb Members

        public List<PortalRole> GetPortalRoles(int portalId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetPortalRoles", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
            parameterPortalID.Value = portalId;
            command.Parameters.Add(parameterPortalID);

            // Open the database connection and execute the command
            connection.Open();
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            var list = new List<PortalRole>();

            while (dr.Read())
            {
                list.Add(dr.ToPortalRole(portalId));
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
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_AddRole", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
            parameterPortalID.Value = portalId;
            command.Parameters.Add(parameterPortalID);

            var parameterRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
            parameterRoleName.Value = roleName;
            command.Parameters.Add(parameterRoleName);

            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Direction = ParameterDirection.Output;
            command.Parameters.Add(parameterRoleID);

            // Open the database connection and execute the command
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            // return the role id 
            return (int) parameterRoleID.Value;
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
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_DeleteRole", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = roleId;
            command.Parameters.Add(parameterRoleID);

            // Open the database connection and execute the command
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
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
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_UpdateRole", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = roleId;
            command.Parameters.Add(parameterRoleID);

            var parameterRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
            parameterRoleName.Value = roleName;
            command.Parameters.Add(parameterRoleName);

            // Open the database connection and execute the command
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
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

        public List<PortalUser> GetRoleMembers(int roleId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetRoleMembership", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = roleId;
            command.Parameters.Add(parameterRoleID);

            // Open the database connection and execute the command
            connection.Open();
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            var list = new List<PortalUser>();

            while (dr.Read())
            {
                list.Add(dr.ToPortalUser());
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
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_AddUserRole", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = roleId;
            command.Parameters.Add(parameterRoleID);

            var parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = userId;
            command.Parameters.Add(parameterUserID);

            // Open the database connection and execute the command
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
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
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_DeleteUserRole", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = roleId;
            command.Parameters.Add(parameterRoleID);

            var parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = userId;
            command.Parameters.Add(parameterUserID);

            // Open the database connection and execute the command
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
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

        public List<PortalUser> GetUsers()
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetUsers", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Open the database connection and execute the command
            connection.Open();
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            var list = new List<PortalUser>();

            while (dr.Read())
            {
                list.Add(dr.ToPortalUser());
            }

            return list;
        }

        #endregion
    }
}