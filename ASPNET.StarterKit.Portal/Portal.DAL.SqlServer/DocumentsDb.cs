using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.Modules.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// documents within the Portal database.
    /// </summary>
    internal class DocumentsDb : DbHelper, IDocumentsDb
    {
        #region IDocumentsDb Members

        /// <summary>
        /// The GetDocuments method returns a IDataReader containing all of the
        /// documents for a specific portal module from the documents
        /// database.
        /// </summary>        
        public IDataReader GetDocuments(int moduleId)
        {
            return GetItems("Portal_GetDocuments", moduleId);
        }

        /// <summary>
        /// The GetSingleDocument method returns a IDataReader containing details
        /// about a specific document from the Documents database table.
        /// </summary>        
        public IDataReader GetSingleDocument(int itemId)
        {
            return GetSingleItem("Portal_GetSingleDocument", itemId);
        }

        /// <summary>
        /// The GetDocumentContent method returns the contents of the specified
        /// document from the Documents database table.
        /// </summary>        
        public IDataReader GetDocumentContent(int itemId)
        {
            return GetSingleItem("Portal_GetDocumentContent", itemId);
        }

        /// <summary>
        /// // The DeleteDocument method deletes the specified document from
        /// the Documents database table.
        /// </summary>        
        public void DeleteDocument(int itemId)
        {
            DeleteItem("Portal_DeleteDocument", itemId);
        }

        /// <summary>
        /// The UpdateDocument method updates the specified document within
        /// the Documents database table.
        /// </summary>        
        public void UpdateDocument(int moduleId, int itemId, string userName, string name, string url, string category,
                                   byte[] content, int size, string contentType)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_UpdateDocument", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputItemId(itemId));
            myCommand.Parameters.Add(InputModuleId(moduleId));
            myCommand.Parameters.Add(InputUserName(userName));

            var parameterName = new SqlParameter("@FileFriendlyName", SqlDbType.NVarChar, 150);
            parameterName.Value = name;
            myCommand.Parameters.Add(parameterName);

            var parameterFileUrl = new SqlParameter("@FileNameUrl", SqlDbType.NVarChar, 250);
            parameterFileUrl.Value = url;
            myCommand.Parameters.Add(parameterFileUrl);

            var parameterCategory = new SqlParameter("@Category", SqlDbType.NVarChar, 50);
            parameterCategory.Value = category;
            myCommand.Parameters.Add(parameterCategory);

            var parameterContent = new SqlParameter("@Content", SqlDbType.Image);
            parameterContent.Value = content;
            myCommand.Parameters.Add(parameterContent);

            var parameterContentType = new SqlParameter("@ContentType", SqlDbType.NVarChar, 50);
            parameterContentType.Value = contentType;
            myCommand.Parameters.Add(parameterContentType);

            var parameterContentSize = new SqlParameter("@ContentSize", SqlDbType.Int, 4);
            parameterContentSize.Value = size;
            myCommand.Parameters.Add(parameterContentSize);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}