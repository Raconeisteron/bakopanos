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

        //*********************************************************************
        //
        // GetEvents Method
        //
        // The GetEvents method returns a DataSet containing all of the
        // events for a specific portal module from the events
        // database.
        //
        // NOTE: A DataSet is returned from this method to allow this method to support
        // both desktop and mobile Web UI.
        //
        // Other relevant sources:
        //     + <a href="GetEvents.htm" style="color:green">GetEvents Stored Procedure</a>
        //
        //*********************************************************************

        #region IEventsDb Members

        public DataSet GetEvents(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlDataAdapter("Portal_GetEvents", myConnection);

            // Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.SelectCommand.AddParameterModuleId(moduleId);

            // Create and Fill the DataSet
            var myDataSet = new DataSet();
            myCommand.Fill(myDataSet);

            // Return the DataSet
            return myDataSet;
        }

        //*********************************************************************
        //
        // GetSingleEvent Method
        //
        // The GetSingleEvent method returns a IDataReader containing details
        // about a specific event from the events database.
        //
        // Other relevant sources:
        //     + <a href="GetSingleEvent.htm" style="color:green">GetSingleEvent Stored Procedure</a>
        //
        //*********************************************************************

        public IDataReader GetSingleEvent(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleEvent", myConnection);

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
        // DeleteEvent Method
        //
        // The DeleteEvent method deletes a specified event from
        // the events database.
        //
        // Other relevant sources:
        //     + <a href="DeleteEvent.htm" style="color:green">DeleteEvent Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteEvent(int itemID)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteEvent", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemID);

            // Open the database connection and execute SQL Command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        //*********************************************************************
        //
        // AddEvent Method
        //
        // The AddEvent method adds a new event within the Events database table, 
        // and returns the ItemID value as a result.
        //
        // Other relevant sources:
        //     + <a href="AddEvent.htm" style="color:green">AddEvent Stored Procedure</a>
        //
        //*********************************************************************

        public int AddEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                            String description, String wherewhen)
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
            SqlParameter parameterItemID = myCommand.AddParameterItemId();

            myCommand.AddParameterModuleId(moduleId);
            myCommand.AddParameterUserName(userName);
            myCommand.AddParameterTitle(title);

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100);
            parameterWhereWhen.Value = wherewhen;
            myCommand.Parameters.Add(parameterWhereWhen);


            myCommand.AddParameterExpireDate(expireDate);
            myCommand.AddParameterDescription(description);

            // Open the database connection and execute SQL Command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            // Return the new Event ItemID
            return (int) parameterItemID.Value;
        }

        //*********************************************************************
        //
        // UpdateEvent Method
        //
        // The UpdateEvent method updates the specified event within
        // the Events database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateEvent.htm" style="color:green">UpdateEvent Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                String description, String wherewhen)
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
            myCommand.AddParameterItemId(itemId);
            myCommand.AddParameterUserName(userName);
            myCommand.AddParameterTitle(title);

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100);
            parameterWhereWhen.Value = wherewhen;
            myCommand.Parameters.Add(parameterWhereWhen);

            myCommand.AddParameterExpireDate(expireDate);
            myCommand.AddParameterDescription(description);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}