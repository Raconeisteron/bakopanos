using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// announcements within the Portal database.
    /// </summary>
    internal class AnnouncementsDb : DbHelper, IAnnouncementsDb
    {
        private readonly string _connectionString;

        public AnnouncementsDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region IAnnouncementsDb Members

        /// <summary>
        /// The GetAnnouncements method returns a DataSet containing all of the
        /// announcements for a specific portal module from the Announcements
        /// database table.
        /// </summary>        
        public IDataReader GetAnnouncements(int moduleId)
        {
            return GetItems(_connectionString, "Portal_GetAnnouncements", moduleId);
        }

        /// <summary>
        /// The GetSingleAnnouncement method returns a IDataReader containing details
        /// about a specific announcement from the Announcements database table.
        /// </summary>        
        public IDataReader GetSingleAnnouncement(int itemId)
        {
            return GetSingleItem(_connectionString, "Portal_GetSingleAnnouncement", itemId);
        }

        /// <summary>
        /// The DeleteAnnouncement method deletes the specified announcement from
        /// the Announcements database table.
        /// </summary>        
        public void DeleteAnnouncement(int itemId)
        {
            DeleteItem(_connectionString, "Portal_DeleteAnnouncement", itemId);
        }

        /// <summary>
        /// The AddAnnouncement method adds a new announcement to the
        /// Announcements database table, and returns the ItemId value as a result.
        /// </summary>        
        public int AddAnnouncement(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                                   string description, string moreLink, string mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Add Parameters to SPROC
            return CreateItem(_connectionString, "Portal_AddAnnouncement", InputModuleId(moduleId),
                              InputUserName(userName),
                              InputTitle(title),
                              InputMoreLink(moreLink),
                              InputMobileMoreLink(mobileMoreLink),
                              InputExpireDate(expireDate),
                              InputDescription(description));

        }

        /// <summary>
        /// The UpdateAnnouncement method updates the specified announcement within
        /// the Announcements database table.
        /// </summary>        
        public void UpdateAnnouncement(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                                       string description, string moreLink, string mobileMoreLink)
        {
            if (userName.Length < 1) userName = "unknown";

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateAnnouncement", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputItemId(itemId));
            myCommand.Parameters.Add(InputUserName(userName));
            myCommand.Parameters.Add(InputTitle(title));
            myCommand.Parameters.Add(InputMoreLink(moreLink));
            myCommand.Parameters.Add(InputMobileMoreLink(mobileMoreLink));
            myCommand.Parameters.Add(InputExpireDate(expireDate));
            myCommand.Parameters.Add(InputDescription(description));

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}