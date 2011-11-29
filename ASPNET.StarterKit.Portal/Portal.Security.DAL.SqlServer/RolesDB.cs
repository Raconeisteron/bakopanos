using System.Data;
using System.Data.SqlClient;

namespace Portal.Security.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// Users, Roles and security settings values within the Portal database.
    /// </summary>
    internal class RolesDb : DbHelper, IRolesDb
    {
        #region IRolesDb Members

        /// <summary>
        /// The GetPortalRoles method returns a list of all role names for the 
        /// specified portal.
        /// </summary>        
        public IDataReader GetPortalRoles(int portalId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_GetPortalRoles", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputPortalId(portalId));

            // Open the database connection and execute the command
            myConnection.Open();
            IDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        /// <summary>
        /// The AddRole method creates a new security role for the specified portal,
        /// and returns the new RoleID value.
        /// </summary>
        public int AddRole(int portalId, string roleName)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_AddRole", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputPortalId(portalId));

            var parameterRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
            parameterRoleName.Value = roleName;
            myCommand.Parameters.Add(parameterRoleName);

            SqlParameter parameterRoleId = myCommand.Parameters.Add(ReturnValueRoleId());

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            // return the role id 
            return (int) parameterRoleId.Value;
        }

        /// <summary>
        /// // The DeleteRole deletes the specified role from the portal database.
        /// </summary>
        public void DeleteRole(int roleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_DeleteRole", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC            
            myCommand.Parameters.Add(InputRoleId(roleId));

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        /// <summary>
        /// The UpdateRole method updates the friendly name of the specified role.
        /// </summary>        
        public void UpdateRole(int roleId, string roleName)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_UpdateRole", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputRoleId(roleId));

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

        /// <summary>
        /// The GetRoleMembers method returns a list of all members in the specified
        // security role.
        /// </summary>        
        public IDataReader GetRoleMembers(int roleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_GetRoleMembership", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add(InputRoleId(roleId));

            // Open the database connection and execute the command
            myConnection.Open();
            IDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        /// <summary>
        /// The AddUserRole method adds the user to the specified security role.
        /// </summary>
        public void AddUserRole(int roleId, int userId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_AddUserRole", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputRoleId(roleId));
            myCommand.Parameters.Add(InputUserId(userId));

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        /// <summary>
        /// The DeleteUserRole method deletes the user from the specified role.
        /// </summary>
        public void DeleteUserRole(int roleId, int userId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_DeleteUserRole", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputRoleId(roleId));
            myCommand.Parameters.Add(InputUserId(userId));

            // Open the database connection and execute the command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}