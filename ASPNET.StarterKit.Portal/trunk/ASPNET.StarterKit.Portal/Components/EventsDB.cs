using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    //*********************************************************************
    //
    // EventDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // events within the Portal database.
    //
    //*********************************************************************

    public class EventsDb : DbHelper
    {
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

        public static DataSet GetEvents(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlDataAdapter("Portal_GetEvents", myConnection);

            // Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            myCommand.SelectCommand.Parameters.Add(parameterModuleId);

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
        // The GetSingleEvent method returns a SqlDataReader containing details
        // about a specific event from the events database.
        //
        // Other relevant sources:
        //     + <a href="GetSingleEvent.htm" style="color:green">GetSingleEvent Stored Procedure</a>
        //
        //*********************************************************************

        public static SqlDataReader GetSingleEvent(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetSingleEvent", myConnection);

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
        // DeleteEvent Method
        //
        // The DeleteEvent method deletes a specified event from
        // the events database.
        //
        // Other relevant sources:
        //     + <a href="DeleteEvent.htm" style="color:green">DeleteEvent Stored Procedure</a>
        //
        //*********************************************************************

        public static void DeleteEvent(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_DeleteEvent", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            myCommand.Parameters.Add(parameterItemId);

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

        public static int AddEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
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

        public static void UpdateEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
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
    }
}