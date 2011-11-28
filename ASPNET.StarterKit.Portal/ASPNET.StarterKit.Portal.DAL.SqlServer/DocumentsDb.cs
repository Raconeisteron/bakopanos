using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// documents within the Portal database.
    /// </summary>
    internal class DocumentsDb : IDocumentsDb
    {
        private readonly string _connectionString;

        public DocumentsDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        //*********************************************************************
        //
        // GetDocuments Method
        //
        // The GetDocuments method returns a IDataReader containing all of the
        // documents for a specific portal module from the documents
        // database.
        //
        // Other relevant sources:
        //     + <a href="GetDocuments.htm" style="color:green">GetDocuments Stored Procedure</a>
        //
        //*********************************************************************

        #region IDocumentsDb Members

        public IDataReader GetDocuments(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetDocuments", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterModuleId(moduleId);

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        //*********************************************************************
        //
        // GetSingleDocument Method
        //
        // The GetSingleDocument method returns a IDataReader containing details
        // about a specific document from the Documents database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleDocument.htm" style="color:green">GetSingleDocument Stored Procedure</a>
        //
        //*********************************************************************

        public IDataReader GetSingleDocument(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleDocument", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemId);

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

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

        public IDataReader GetDocumentContent(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetDocumentContent", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemId);

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

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

        public void DeleteDocument(int itemID)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteDocument", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemID);

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

        public void UpdateDocument(int moduleId, int itemId, String userName, String name, String url, String category,
                                   byte[] content, int size, String contentType)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateDocument", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemId);
            myCommand.AddParameterModuleId(moduleId);
            myCommand.AddParameterUserName(userName);

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