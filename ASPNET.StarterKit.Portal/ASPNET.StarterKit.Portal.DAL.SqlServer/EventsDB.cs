using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// events within the Portal database.
    /// </summary>
    internal class EventsDb : DbHelper, IEventsDb
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
            myCommand.SelectCommand.Parameters.Add(InputModuleId(moduleId));

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
            // Return the datareader 
            return GetSingleItem(_connectionString, "Portal_GetSingleEvent", itemId);
        }

        /// <summary>
        /// The DeleteEvent method deletes a specified event from
        /// the events database.
        /// </summary>        
        public void DeleteEvent(int itemId)
        {
            DeleteItem(_connectionString, "Portal_DeleteEvent", itemId);
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
            SqlParameter parameterItemId = myCommand.Parameters.Add(ReturnValueItemId());

            myCommand.Parameters.Add(InputModuleId(moduleId));
            myCommand.Parameters.Add(InputUserName(userName));
            myCommand.Parameters.Add(InputTitle(title));

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100);
            parameterWhereWhen.Value = wherewhen;
            myCommand.Parameters.Add(parameterWhereWhen);


            myCommand.Parameters.Add(InputExpireDate(expireDate));
            myCommand.Parameters.Add(InputDescription(description));

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
            myCommand.Parameters.Add(InputItemId(itemId));
            myCommand.Parameters.Add(InputUserName(userName));
            myCommand.Parameters.Add(InputTitle(title));

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100);
            parameterWhereWhen.Value = wherewhen;
            myCommand.Parameters.Add(parameterWhereWhen);

            myCommand.Parameters.Add(InputExpireDate(expireDate));
            myCommand.Parameters.Add(InputDescription(description));

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}