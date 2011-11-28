using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// events within the Portal database.
    /// </summary>
    internal class EventsDb : IEventsDb
    {
        private readonly string _connectionString;

        public EventsDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region IEventsDb Members

        /// <summary>
        /// The GetEvents method returns a DataSet containing all of the
        /// events for a specific portal module from the events
        /// database.
        /// </summary>        
        public DataSet GetEvents(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlDataAdapter("Portal_GetEvents", myConnection);

            // Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.SelectCommand.Parameters.Add(SqlParameterHelper.InputModuleId(moduleId));

            // Create and Fill the DataSet
            var myDataSet = new DataSet();
            myCommand.Fill(myDataSet);

            // Return the DataSet
            return myDataSet;
        }

        /// <summary>
        /// The GetSingleEvent method returns a IDataReader containing details
        /// about a specific event from the events database.
        /// </summary>
        public IDataReader GetSingleEvent(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleEvent", myConnection);

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
        /// The DeleteEvent method deletes a specified event from
        /// the events database.
        /// </summary>        
        public void DeleteEvent(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteEvent", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(SqlParameterHelper.InputItemId(itemId));

            // Open the database connection and execute SQL Command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        /// <summary>
        /// // The AddEvent method adds a new event within the Events database table, 
        /// and returns the ItemID value as a result.
        /// </summary>        
        public int AddEvent(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                            string description, string wherewhen)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_AddEvent", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemId = myCommand.Parameters.Add(SqlParameterHelper.ReturnValueItemId());

            myCommand.Parameters.Add(SqlParameterHelper.InputModuleId(moduleId));
            myCommand.Parameters.Add(SqlParameterHelper.InputUserName(userName));
            myCommand.Parameters.Add(SqlParameterHelper.InputTitle(title));

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100);
            parameterWhereWhen.Value = wherewhen;
            myCommand.Parameters.Add(parameterWhereWhen);


            myCommand.Parameters.Add(SqlParameterHelper.InputExpireDate(expireDate));
            myCommand.Parameters.Add(SqlParameterHelper.InputDescription(description));

            // Open the database connection and execute SQL Command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            // Return the new Event ItemID
            return (int) parameterItemId.Value;
        }

        /// <summary>
        /// The UpdateEvent method updates the specified event within
        /// the Events database table.
        /// </summary>        
        public void UpdateEvent(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                                string description, string wherewhen)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateEvent", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(SqlParameterHelper.InputItemId(itemId));
            myCommand.Parameters.Add(SqlParameterHelper.InputUserName(userName));
            myCommand.Parameters.Add(SqlParameterHelper.InputTitle(title));

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100);
            parameterWhereWhen.Value = wherewhen;
            myCommand.Parameters.Add(parameterWhereWhen);

            myCommand.Parameters.Add(SqlParameterHelper.InputExpireDate(expireDate));
            myCommand.Parameters.Add(SqlParameterHelper.InputDescription(description));

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}