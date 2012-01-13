using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlContactsDb : IContactsDb
    {
        private readonly string _connectionString;

        public SqlContactsDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region IContactsDb Members

        public IDataReader GetContacts(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetContacts", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;
            command.Parameters.Add(parameterModuleId);

            // Execute the command
            connection.Open();
            SqlDataReader result = command.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleContact(int itemId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetSingleContact", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;
            command.Parameters.Add(parameterItemId);

            // Execute the command
            connection.Open();
            SqlDataReader result = command.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        public void DeleteContact(int itemID)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_DeleteContact", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            command.Parameters.Add(parameterItemID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public int AddContact(int moduleId, string userName, string name, string role, string email,
                              string contact1, string contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_AddContact", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Direction = ParameterDirection.Output;
            command.Parameters.Add(parameterItemID);

            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleId;
            command.Parameters.Add(parameterModuleID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            command.Parameters.Add(parameterUserName);

            var parameterName = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
            parameterName.Value = name;
            command.Parameters.Add(parameterName);

            var parameterRole = new SqlParameter("@Role", SqlDbType.NVarChar, 100);
            parameterRole.Value = role;
            command.Parameters.Add(parameterRole);

            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            command.Parameters.Add(parameterEmail);

            var parameterContact1 = new SqlParameter("@Contact1", SqlDbType.NVarChar, 100);
            parameterContact1.Value = contact1;
            command.Parameters.Add(parameterContact1);

            var parameterContact2 = new SqlParameter("@Contact2", SqlDbType.NVarChar, 100);
            parameterContact2.Value = contact2;
            command.Parameters.Add(parameterContact2);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return (int) parameterItemID.Value;
        }

        public void UpdateContact(int itemId, string userName, string name, string role, string email,
                                  string contact1, string contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_UpdateContact", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemId;
            command.Parameters.Add(parameterItemID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            command.Parameters.Add(parameterUserName);

            var parameterName = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
            parameterName.Value = name;
            command.Parameters.Add(parameterName);

            var parameterRole = new SqlParameter("@Role", SqlDbType.NVarChar, 100);
            parameterRole.Value = role;
            command.Parameters.Add(parameterRole);

            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            command.Parameters.Add(parameterEmail);

            var parameterContact1 = new SqlParameter("@Contact1", SqlDbType.NVarChar, 100);
            parameterContact1.Value = contact1;
            command.Parameters.Add(parameterContact1);

            var parameterContact2 = new SqlParameter("@Contact2", SqlDbType.NVarChar, 100);
            parameterContact2.Value = contact2;
            command.Parameters.Add(parameterContact2);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        #endregion
    }
}