using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// discussions within the Portal database.
    /// </summary>
    internal class DiscussionsDb : IDiscussionsDb
    {
        private readonly string _connectionString;

        public DiscussionsDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region IDiscussionsDb Members

        /// <summary>
        /// Returns details for all of the messages in the discussion specified by ModuleID.
        /// </summary>
        public IDataReader GetTopLevelMessages(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetTopLevelMessages", myConnection);

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
        /// Returns details for all of the messages the thread, as identified by the Parent id string.
        /// </summary>
        public IDataReader GetThreadMessages(string parent)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetThreadMessages", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterParent = new SqlParameter("@Parent", SqlDbType.NVarChar, 750);
            parameterParent.Value = parent;
            myCommand.Parameters.Add(parameterParent);

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        /// <summary>
        /// The GetSingleMessage method returns the details for the message
        /// specified by the itemId parameter.
        /// </summary>        
        public IDataReader GetSingleMessage(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleMessage", myConnection);

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
        /// The AddMessage method adds a new message within the
        /// Discussions database table, and returns ItemID value as a result.
        /// </summary>        
        public int AddMessage(int moduleId, int parentId, string userName, string title, string body)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_AddMessage", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemId = myCommand.Parameters.Add(SqlParameterHelper.ReturnValueItemId());
            myCommand.Parameters.Add(SqlParameterHelper.InputTitle(title));

            var parameterBody = new SqlParameter("@Body", SqlDbType.NVarChar, 3000);
            parameterBody.Value = body;
            myCommand.Parameters.Add(parameterBody);

            var parameterParentId = new SqlParameter("@ParentID", SqlDbType.Int, 4);
            parameterParentId.Value = parentId;
            myCommand.Parameters.Add(parameterParentId);

            myCommand.Parameters.Add(SqlParameterHelper.InputUserName(userName));
            myCommand.Parameters.Add(SqlParameterHelper.InputModuleId(moduleId));

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int) parameterItemId.Value;
        }

        #endregion
    }
}