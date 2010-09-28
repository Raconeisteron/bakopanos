using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    //*********************************************************************
    //
    // AnnounceDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // announcements within the Portal database.
    //
    //*********************************************************************

    public class AnnouncementsDB : IAnnouncementsDB
    {
        private readonly string _connectionString;
        private Db _db;

        public AnnouncementsDB(ConnectionStringSettings settings, Db db)
        {
            _connectionString = settings.ConnectionString;
            _db = db;
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

        public DataSet GetAnnouncements()
        {
            DataSet dataset = _db.FillDataSet("select * from Portal_Announcements", CommandType.Text);            
            return dataset;
        }

        public DataSet GetAnnouncements(int moduleId)
        {
            DbParameter[] parameters =
                new[]
                    {
                        _db.CreateParameter("@ModuleID", moduleId)
                    };
            DataSet dataset = _db.FillDataSet("Portal_GetAnnouncements", CommandType.StoredProcedure, parameters);
            return dataset;
        }

        //*********************************************************************
        //
        // GetSingleAnnouncement Method
        //
        // The GetSingleAnnouncement method returns a SqlDataReader containing details
        // about a specific announcement from the Announcements database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleAnnouncement.htm" style="color:green">GetSingleAnnouncement Stored Procedure</a>
        //
        //*********************************************************************

        public IDataReader GetSingleAnnouncement(int itemId)
        {
            DbParameter[] parameters =
                new[]
                    {
                        _db.CreateParameter("@ItemID", itemId)
                    };
            IDataReader reader = _db.ExecuteReader("Portal_GetSingleAnnouncement", CommandType.StoredProcedure, parameters);
            return reader;
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
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            myCommand.Parameters.Add(parameterItemID);

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

        public int AddAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate, String description, String moreLink, String mobileMoreLink)
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
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterItemID);

            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 150);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            var parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150);
            parameterMoreLink.Value = moreLink;
            myCommand.Parameters.Add(parameterMoreLink);

            var parameterMobileMoreLink = new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150);
            parameterMobileMoreLink.Value = mobileMoreLink;
            myCommand.Parameters.Add(parameterMobileMoreLink);

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;
            myCommand.Parameters.Add(parameterExpireDate);

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;
            myCommand.Parameters.Add(parameterDescription);

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

        public void UpdateAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate, String description, String moreLink, String mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateAnnouncement", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemId;
            myCommand.Parameters.Add(parameterItemID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 150);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            var parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150);
            parameterMoreLink.Value = moreLink;
            myCommand.Parameters.Add(parameterMoreLink);

            var parameterMobileMoreLink = new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150);
            parameterMobileMoreLink.Value = mobileMoreLink;
            myCommand.Parameters.Add(parameterMobileMoreLink);

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;
            myCommand.Parameters.Add(parameterExpireDate);

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;
            myCommand.Parameters.Add(parameterDescription);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}