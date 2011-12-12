using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// documents within the Portal database.
    /// </summary>
    public class DocumentDb : DbHelper
    {
        /// <returns>
        /// The GetDocuments method returns a SqlDataReader containing all of the
        /// documents for a specific portal module from the documents
        /// database.
        /// </returns>
        public static DataTable GetDocuments(int moduleId)
        {
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};

            return GetDataTable("Portal_GetDocuments", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleDocument method returns a SqlDataReader containing details
        /// about a specific document from the Documents database table.
        /// </returns>
        public static DataRow GetSingleDocument(int itemId)
        {
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};

            return GetDataRow("Portal_GetSingleDocument", parameterItemId);
        }

        /// <returns>
        /// The GetDocumentContent method returns the contents of the specified
        /// document from the Documents database table.
        /// </returns>
        public static DataRow GetDocumentContent(int itemId)
        {
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};

            return GetDataRow("Portal_GetDocumentContent", parameterItemId);
        }


        /// <summary>
        /// The DeleteDocument method deletes the specified document from
        /// the Documents database table.
        /// </summary>
        public static void DeleteDocument(int itemId)
        {
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};

            ExecuteNonQuery("Portal_DeleteDocument", parameterItemId);
        }


        //*********************************************************************
        //
        // UpdateDocument Method
        //
        // The UpdateDocument method updates the specified document within
        // the Documents database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateDocument.htm" style="color:green">UpdateDocument Stored Procedure</a>
        //
        //*********************************************************************

        public static void UpdateDocument(int moduleId, int itemId, String userName, String name, String url,
                                          String category,
                                          byte[] content, int size, String contentType)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_UpdateDocument", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            myCommand.Parameters.Add(parameterItemId);

            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            myCommand.Parameters.Add(parameterModuleId);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            myCommand.Parameters.Add(parameterUserName);

            var parameterName = new SqlParameter("@FileFriendlyName", SqlDbType.NVarChar, 150) {Value = name};
            myCommand.Parameters.Add(parameterName);

            var parameterFileUrl = new SqlParameter("@FileNameUrl", SqlDbType.NVarChar, 250) {Value = url};
            myCommand.Parameters.Add(parameterFileUrl);

            var parameterCategory = new SqlParameter("@Category", SqlDbType.NVarChar, 50) {Value = category};
            myCommand.Parameters.Add(parameterCategory);

            var parameterContent = new SqlParameter("@Content", SqlDbType.Image) {Value = content};
            myCommand.Parameters.Add(parameterContent);

            var parameterContentType = new SqlParameter("@ContentType", SqlDbType.NVarChar, 50) {Value = contentType};
            myCommand.Parameters.Add(parameterContentType);

            var parameterContentSize = new SqlParameter("@ContentSize", SqlDbType.Int, 4) {Value = size};
            myCommand.Parameters.Add(parameterContentSize);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}