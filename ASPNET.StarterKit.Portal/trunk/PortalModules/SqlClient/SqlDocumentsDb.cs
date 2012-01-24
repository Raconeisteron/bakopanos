using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlDocumentsDb : Db, IDocumentsDb
    {

        public SqlDocumentsDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
        }

        #region IDocumentsDb Members

        public List<PortalDocument> GetDocuments(int moduleId)
        {

            // Add Parameters to SPROC
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            // Execute method
            IDataReader reader = ExecuteReader("Portal_GetDocuments", CommandType.StoredProcedure, parameterModuleId);

            var documentList = new List<PortalDocument>();

            while (reader.Read())
                documentList.Add(reader.ToPortalDocument());

            // Return list
            return documentList;
        }

        public PortalDocument GetSingleDocument(int itemId)
        {

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            // Execute method
            IDataReader reader = ExecuteReader("Portal_GetSingleDocument", CommandType.StoredProcedure, parameterItemId);

            //Read once, since we have only one result (itemId is Unique)
            reader.Read();
            PortalDocument document = reader.ToPortalDocument();

            // Return the item
            return document;
        }


        public PortalDocument GetDocumentContent(int itemId)
        {

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            // Execute method
            IDataReader reader = ExecuteReader("Portal_GetDocumentContent", CommandType.StoredProcedure, parameterItemId);

            //Read once, since we have only one result (itemId is Unique)
            reader.Read();
            PortalDocument document = reader.ToPortalDocument();

            // Return the item
            return document;
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