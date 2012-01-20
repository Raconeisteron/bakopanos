using System.Data;
using System.Data.Common;
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
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetDocuments", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleDocument(int itemId)
        {

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetSingleDocument", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader 
            return result;
        }


        public IDataReader GetDocumentContent(int itemId)
        {

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetDocumentContent", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader 
            return result;
        }

        public void DeleteDocument(int itemId)
        {

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

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
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);
            DbParameter parameterModuleId = CreateParameter("@ModuleID",moduleId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterName = CreateParameter("@FileFriendlyName", name);
            DbParameter parameterFileUrl = CreateParameter("@FileNameUrl", url);
            DbParameter parameterCategory = CreateParameter("@Category", category);
            DbParameter parameterContent = CreateParameter("@Content", content);
            DbParameter parameterContentType = CreateParameter("@ContentType", contentType);
            DbParameter parameterContentSize = CreateParameter("@ContentSize", size);

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