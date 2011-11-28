using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// announcements within the Portal database.
    /// </summary>
    internal class AnnouncementsDb : IAnnouncementsDb
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
        public DataSet GetAnnouncements(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlDataAdapter("Portal_GetAnnouncements", myConnection);

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
        /// The GetSingleAnnouncement method returns a IDataReader containing details
        /// about a specific announcement from the Announcements database table.
        /// </summary>        
        public IDataReader GetSingleAnnouncement(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleAnnouncement", myConnection);

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
        /// The DeleteAnnouncement method deletes the specified announcement from
        /// the Announcements database table.
        /// </summary>        
        public void DeleteAnnouncement(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteAnnouncement", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(SqlParameterHelper.InputItemId(itemId));

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
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

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_AddAnnouncement", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemId = myCommand.Parameters.Add(SqlParameterHelper.ReturnValueItemId());
            myCommand.Parameters.Add(SqlParameterHelper.InputModuleId(moduleId));
            myCommand.Parameters.Add(SqlParameterHelper.InputUserName(userName));
            myCommand.Parameters.Add(SqlParameterHelper.InputTitle(title));
            myCommand.Parameters.Add(SqlParameterHelper.InputMoreLink(moreLink));
            myCommand.Parameters.Add(SqlParameterHelper.InputMobileMoreLink(mobileMoreLink));
            myCommand.Parameters.Add(SqlParameterHelper.InputExpireDate(expireDate));
            myCommand.Parameters.Add(SqlParameterHelper.InputDescription(description));
            
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int) parameterItemId.Value;
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
            myCommand.Parameters.Add(SqlParameterHelper.InputItemId(itemId));
            myCommand.Parameters.Add(SqlParameterHelper.InputUserName(userName));
            myCommand.Parameters.Add(SqlParameterHelper.InputTitle(title));
            myCommand.Parameters.Add(SqlParameterHelper.InputMoreLink(moreLink));
            myCommand.Parameters.Add(SqlParameterHelper.InputMobileMoreLink(mobileMoreLink));
            myCommand.Parameters.Add(SqlParameterHelper.InputExpireDate(expireDate));
            myCommand.Parameters.Add(SqlParameterHelper.InputDescription(description));

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}