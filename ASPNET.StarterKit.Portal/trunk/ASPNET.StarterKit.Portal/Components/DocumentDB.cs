using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    //*********************************************************************
    //
    // DocumentDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // documents within the Portal database.
    //
    //*********************************************************************

    public class DocumentDb
    {
        //*********************************************************************
        //
        // GetDocuments Method
        //
        // The GetDocuments method returns a SqlDataReader containing all of the
        // documents for a specific portal module from the documents
        // database.
        //
        // Other relevant sources:
        //     + <a href="GetDocuments.htm" style="color:green">GetDocuments Stored Procedure</a>
        //
        //*********************************************************************

        public static SqlDataReader GetDocuments(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetDocuments", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            myCommand.Parameters.Add(parameterModuleId);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        //*********************************************************************
        //
        // GetSingleDocument Method
        //
        // The GetSingleDocument method returns a SqlDataReader containing details
        // about a specific document from the Documents database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleDocument.htm" style="color:green">GetSingleDocument Stored Procedure</a>
        //
        //*********************************************************************

        public static SqlDataReader GetSingleDocument(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetSingleDocument", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            myCommand.Parameters.Add(parameterItemId);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        //*********************************************************************
        //
        // GetDocumentContent Method
        //
        // The GetDocumentContent method returns the contents of the specified
        // document from the Documents database table.
        //
        // Other relevant sources:
        //     + <a href="GetDocumentContent.htm" style="color:green">GetDocumentContent</a>
        //
        //*********************************************************************

        public static SqlDataReader GetDocumentContent(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetDocumentContent", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            myCommand.Parameters.Add(parameterItemId);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }


        //*********************************************************************
        //
        // DeleteDocument Method
        //
        // The DeleteDocument method deletes the specified document from
        // the Documents database table.
        //
        // Other relevant sources:
        //     + <a href="DeleteDocument.htm" style="color:green">DeleteDocument Stored Procedure</a>
        //
        //*********************************************************************

        public static void DeleteDocument(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_DeleteDocument", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            myCommand.Parameters.Add(parameterItemId);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
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

        public static void UpdateDocument(int moduleId, int itemId, String userName, String name, String url, String category,
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