using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    //*********************************************************************
    //
    // AnnounceDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // announcements within the Portal database.
    //
    //*********************************************************************

    internal class AnnouncementsDB : IAnnouncementsDB
    {
        private readonly string _connectionString;

        public AnnouncementsDB(string connectionString)
        {
            _connectionString = connectionString;
        }

        //*********************************************************************
        //
        // GetAnnouncements Method
        //
        // The GetAnnouncements method returns a DataSet containing all of the
        // announcements for a specific portal module from the Announcements
        // database table.
        //
        // NOTE: A DataSet is returned from this method to allow this method to support
        // both desktop and mobile Web UI.
        //
        // Other relevant sources:
        //     + <a href="GetAnnouncements.htm" style="color:green">GetAnnouncements Stored Procedure</a>
        //
        //*********************************************************************

        #region IAnnouncementsDB Members

        public DataSet GetAnnouncements(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlDataAdapter("Portal_GetAnnouncements", myConnection);

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
        // GetSingleAnnouncement Method
        //
        // The GetSingleAnnouncement method returns a IDataReader containing details
        // about a specific announcement from the Announcements database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleAnnouncement.htm" style="color:green">GetSingleAnnouncement Stored Procedure</a>
        //
        //*********************************************************************

        public IDataReader GetSingleAnnouncement(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleAnnouncement", myConnection);

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
        // DeleteAnnouncement Method
        //
        // The DeleteAnnouncement method deletes the specified announcement from
        // the Announcements database table.
        //
        // Other relevant sources:
        //     + <a href="DeleteAnnouncement.htm" style="color:green">DeleteAnnouncement Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteAnnouncement(int itemID)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteAnnouncement", myConnection);

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
        // AddAnnouncement Method
        //
        // The AddAnnouncement method adds a new announcement to the
        // Announcements database table, and returns the ItemId value as a result.
        //
        // Other relevant sources:
        //     + <a href="AddAnnouncement.htm" style="color:green">AddAnnouncement Stored Procedure</a>
        //
        //*********************************************************************

        public int AddAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                   String description, String moreLink, String mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_AddAnnouncement", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemID = myCommand.AddParameterItemId();
            myCommand.AddParameterModuleId(moduleId);
            myCommand.AddParameterUserName(userName);
            myCommand.AddParameterTitle(title);
            myCommand.AddParameterMoreLink(moreLink);
            myCommand.AddParameterMobileMoreLink(mobileMoreLink);
            myCommand.AddParameterExpireDate(expireDate);
            myCommand.AddParameterDescription(description);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int) parameterItemID.Value;
        }

        //*********************************************************************
        //
        // UpdateAnnouncement Method
        //
        // The UpdateAnnouncement method updates the specified announcement within
        // the Announcements database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateAnnouncement.htm" style="color:green">UpdateAnnouncement Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                       String description, String moreLink, String mobileMoreLink)
        {
            if (userName.Length < 1) userName = "unknown";

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateAnnouncement", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemId);
            myCommand.AddParameterUserName(userName);
            myCommand.AddParameterTitle(title);
            myCommand.AddParameterMoreLink(moreLink);
            myCommand.AddParameterMobileMoreLink(mobileMoreLink);
            myCommand.AddParameterExpireDate(expireDate);
            myCommand.AddParameterDescription(description);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}