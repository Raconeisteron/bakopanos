using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlDocumentsDb : Db, IDocumentsDb
    {
        private readonly string _connectionString;

        public SqlDocumentsDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
            _connectionString = connectionString;
        }

        #region IDocumentsDb Members

        public IDataReader GetDocuments(int moduleId)
        {

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetDocuments", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleDocument(int itemId)
        {

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetSingleDocument", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader 
            return result;
        }


        public IDataReader GetDocumentContent(int itemId)
        {

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetDocumentContent", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader 
            return result;
        }

        public void DeleteDocument(int itemID)
        {

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemID;

            //Execute method
            ExecuteNonQuery("Portal_DeleteDocument", CommandType.StoredProcedure, parameterItemId);
        }

        public void UpdateDocument(int moduleId, int itemId, string userName, string name, string url, string category,
                                   byte[] content, int size, string contentType)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;

            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;

            var parameterName = new SqlParameter("@FileFriendlyName", SqlDbType.NVarChar, 150);
            parameterName.Value = name;

            var parameterFileUrl = new SqlParameter("@FileNameUrl", SqlDbType.NVarChar, 250);
            parameterFileUrl.Value = url;

            var parameterCategory = new SqlParameter("@Category", SqlDbType.NVarChar, 50);
            parameterCategory.Value = category;

            var parameterContent = new SqlParameter("@Content", SqlDbType.Image);
            parameterContent.Value = content;

            var parameterContentType = new SqlParameter("@ContentType", SqlDbType.NVarChar, 50);
            parameterContentType.Value = contentType;

            var parameterContentSize = new SqlParameter("@ContentSize", SqlDbType.Int, 4);
            parameterContentSize.Value = size;

            //Execute method
            ExecuteNonQuery("Portal_UpdateDocument", CommandType.StoredProcedure,
                parameterItemId,
                parameterModuleId,
                parameterUserName,
                parameterName,
                parameterFileUrl,
                parameterCategory,
                parameterContent,
                parameterContentType,
                parameterContentSize
                );
        }

        #endregion
    }
}