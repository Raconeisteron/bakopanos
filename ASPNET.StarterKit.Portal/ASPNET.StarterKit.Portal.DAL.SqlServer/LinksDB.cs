using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// // Class that encapsulates all data logic necessary to add/query/delete
    /// links within the Portal database.
    /// </summary>
    internal class LinkDb : ILinksDb
    {
        private readonly string _connectionString;

        public LinkDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region ILinksDb Members

        /// <summary>
        /// The GetLinks method returns a IDataReader containing all of the
        /// links for a specific portal module from the announcements
        /// database.
        /// </summary>        
        public IDataReader GetLinks(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetLinks", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(SqlParameterHelper.InputModuleId(moduleId));

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        /// <summary>
        /// The GetSingleLink method returns a IDataReader containing details
        /// about a specific link from the Links database table.
        /// </summary>        
        public IDataReader GetSingleLink(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(SqlParameterHelper.InputItemId(itemId));

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        /// <summary>
        /// The DeleteLink method deletes a specified link from
        /// the Links database table.
        /// </summary>
        public void DeleteLink(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(SqlParameterHelper.InputItemId(itemId));

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        /// <summary>
        /// The AddLink method adds a new link within the
        /// links database table, and returns ItemID value as a result.
        /// </summary>
        public int AddLink(int moduleId, int itemId, string userName, string title, string url, string mobileUrl,
                           int viewOrder, string description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_AddLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemId = myCommand.Parameters.Add(SqlParameterHelper.ReturnValueItemId());
            myCommand.Parameters.Add(SqlParameterHelper.InputModuleId(moduleId));
            myCommand.Parameters.Add(SqlParameterHelper.InputUserName(userName));

            myCommand.Parameters.Add(SqlParameterHelper.InputTitle(title));

            myCommand.Parameters.Add(SqlParameterHelper.InputDescription(description));

            var parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 100);
            parameterUrl.Value = url;
            myCommand.Parameters.Add(parameterUrl);

            var parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100);
            parameterMobileUrl.Value = mobileUrl;
            myCommand.Parameters.Add(parameterMobileUrl);

            var parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4);
            parameterViewOrder.Value = viewOrder;
            myCommand.Parameters.Add(parameterViewOrder);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int) parameterItemId.Value;
        }

        /// <summary>
        /// // The UpdateLink method updates a specified link within
        /// the Links database table.
        /// </summary>        
        public void UpdateLink(int moduleId, int itemId, string userName, string title, string url, string mobileUrl,
                               int viewOrder, string description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(SqlParameterHelper.InputItemId(itemId));
            myCommand.Parameters.Add(SqlParameterHelper.InputUserName(userName));
            myCommand.Parameters.Add(SqlParameterHelper.InputTitle(title));
            myCommand.Parameters.Add(SqlParameterHelper.InputDescription(description));

            var parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 100);
            parameterUrl.Value = url;
            myCommand.Parameters.Add(parameterUrl);

            var parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100);
            parameterMobileUrl.Value = mobileUrl;
            myCommand.Parameters.Add(parameterMobileUrl);

            var parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4);
            parameterViewOrder.Value = viewOrder;
            myCommand.Parameters.Add(parameterViewOrder);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}