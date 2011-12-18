using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// events within the Portal database.
    /// </summary>
    internal class EventsDb : DbHelper, IEventsDb
    {
        #region IEventsDb Members

        /// <returns>
        /// The GetEvents method returns a DataSet containing all of the
        /// events for a specific portal module from the events
        /// database.
        /// </returns>
        public DataTable GetEvents(int moduleId)
        {
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};

            return GetDataTable("Portal_GetEvents", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleEvent method returns a SqlDataReader containing details
        /// about a specific event from the events database.
        /// </returns>
        public DataRow GetSingleEvent(int itemId)
        {
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};

            return GetDataRow("Portal_GetSingleEvent", parameterItemId);
        }

        /// <summary>
        /// The DeleteEvent method deletes a specified event from
        /// the events database.
        /// </summary>
        public void DeleteEvent(int itemId)
        {
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};

            ExecuteNonQuery("Portal_DeleteEvent", parameterItemId);
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
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_AddEvent", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Direction = ParameterDirection.Output};
            myCommand.Parameters.Add(parameterItemId);

            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            myCommand.Parameters.Add(parameterModuleId);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            myCommand.Parameters.Add(parameterUserName);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100) {Value = title};
            myCommand.Parameters.Add(parameterTitle);

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100) {Value = wherewhen};
            myCommand.Parameters.Add(parameterWhereWhen);

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8) {Value = expireDate};
            myCommand.Parameters.Add(parameterExpireDate);

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000) {Value = description};
            myCommand.Parameters.Add(parameterDescription);

            // Open the database connection and execute SQL Command
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            // Return the new Event ItemID
            return (int) parameterItemId.Value;
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
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_UpdateEvent", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            myCommand.Parameters.Add(parameterItemId);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            myCommand.Parameters.Add(parameterUserName);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100) {Value = title};
            myCommand.Parameters.Add(parameterTitle);

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100) {Value = wherewhen};
            myCommand.Parameters.Add(parameterWhereWhen);

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8) {Value = expireDate};
            myCommand.Parameters.Add(parameterExpireDate);

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000) {Value = description};
            myCommand.Parameters.Add(parameterDescription);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}