using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlDocumentsDb : IDocumentsDb
    {
        private readonly string _connectionString;

        public SqlDocumentsDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region IDocumentsDb Members

        public IDataReader GetDocuments(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetDocuments", connection);

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

        public IDataReader GetSingleDocument(int itemId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetSingleDocument", connection);

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


        public IDataReader GetDocumentContent(int itemId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetDocumentContent", connection);

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

        public void DeleteDocument(int itemID)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_DeleteDocument", connection);

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

        public void UpdateDocument(int moduleId, int itemId, string userName, string name, string url, string category,
                                   byte[] content, int size, string contentType)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_UpdateDocument", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemId;
            command.Parameters.Add(parameterItemID);

            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleId;
            command.Parameters.Add(parameterModuleID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            command.Parameters.Add(parameterUserName);

            var parameterName = new SqlParameter("@FileFriendlyName", SqlDbType.NVarChar, 150);
            parameterName.Value = name;
            command.Parameters.Add(parameterName);

            var parameterFileUrl = new SqlParameter("@FileNameUrl", SqlDbType.NVarChar, 250);
            parameterFileUrl.Value = url;
            command.Parameters.Add(parameterFileUrl);

            var parameterCategory = new SqlParameter("@Category", SqlDbType.NVarChar, 50);
            parameterCategory.Value = category;
            command.Parameters.Add(parameterCategory);

            var parameterContent = new SqlParameter("@Content", SqlDbType.Image);
            parameterContent.Value = content;
            command.Parameters.Add(parameterContent);

            var parameterContentType = new SqlParameter("@ContentType", SqlDbType.NVarChar, 50);
            parameterContentType.Value = contentType;
            command.Parameters.Add(parameterContentType);

            var parameterContentSize = new SqlParameter("@ContentSize", SqlDbType.Int, 4);
            parameterContentSize.Value = size;
            command.Parameters.Add(parameterContentSize);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        #endregion
    }
}