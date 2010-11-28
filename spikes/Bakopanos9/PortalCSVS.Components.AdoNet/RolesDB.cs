using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    //*********************************************************************
    //
    // RolesDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // Users, Roles and security settings values within the Portal database.
    //
    //*********************************************************************
    public class RolesDB : IRolesDB
    {
        private readonly string _connectionString;

        public RolesDB(ConnectionStringSettings settings)
        {
            _connectionString = settings.ConnectionString;
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

        #region IRolesDB Members

        public IDataReader GetPortalRoles(int portalId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetPortalRoles", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
            parameterPortalID.Value = portalId;
            myCommand.Parameters.Add(parameterPortalID);

            // Open the database connection and execute the command
            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
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

        public int AddRole(int portalId, String roleName)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_AddRole", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
            parameterPortalID.Value = portalId;
            myCommand.Parameters.Add(parameterPortalID);

            var parameterRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
            parameterRoleName.Value = roleName;
            myCommand.Parameters.Add(parameterRoleName);

            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRoleID);

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

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
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteRole", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = roleId;
            myCommand.Parameters.Add(parameterRoleID);

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
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

        public void UpdateRole(int roleId, String roleName)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateRole", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = roleId;
            myCommand.Parameters.Add(parameterRoleID);

            var parameterRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
            parameterRoleName.Value = roleName;
            myCommand.Parameters.Add(parameterRoleName);

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
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

        public IDataReader GetRoleMembers(int roleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetRoleMembership", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = roleId;
            myCommand.Parameters.Add(parameterRoleID);

            // Open the database connection and execute the command
            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
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
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_AddUserRole", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = roleId;
            myCommand.Parameters.Add(parameterRoleID);

            var parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = userId;
            myCommand.Parameters.Add(parameterUserID);

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
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
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteUserRole", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameterRoleID.Value = roleId;
            myCommand.Parameters.Add(parameterRoleID);

            var parameterUserID = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameterUserID.Value = userId;
            myCommand.Parameters.Add(parameterUserID);

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
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

        public IDataReader GetUsers()
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetUsers", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Open the database connection and execute the command
            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        #endregion
    }
}