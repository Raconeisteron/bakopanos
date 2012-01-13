using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlAnnouncementsDb : IAnnouncementsDb
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        #region IAnnouncementsDb Members

        public IDataReader GetAnnouncements(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetAnnouncements", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;
            command.Parameters.Add(parameterModuleId);

            // Execute the command
            connection.Open();
            SqlDataReader result = command.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleAnnouncement(int itemId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetSingleAnnouncement", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;
            command.Parameters.Add(parameterItemId);

            // Execute the command
            connection.Open();
            SqlDataReader result = command.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        public void DeleteAnnouncement(int itemID)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_DeleteAnnouncement", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            command.Parameters.Add(parameterItemID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public int AddAnnouncement(int moduleId, string userName, string title, DateTime expireDate,
                                   string description, string moreLink, string mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_AddAnnouncement", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Direction = ParameterDirection.Output;
            command.Parameters.Add(parameterItemID);

            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleId;
            command.Parameters.Add(parameterModuleID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            command.Parameters.Add(parameterUserName);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 150);
            parameterTitle.Value = title;
            command.Parameters.Add(parameterTitle);

            var parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150);
            parameterMoreLink.Value = moreLink;
            command.Parameters.Add(parameterMoreLink);

            var parameterMobileMoreLink = new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150);
            parameterMobileMoreLink.Value = mobileMoreLink;
            command.Parameters.Add(parameterMobileMoreLink);

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;
            command.Parameters.Add(parameterExpireDate);

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;
            command.Parameters.Add(parameterDescription);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return (int) parameterItemID.Value;
        }

        public void UpdateAnnouncement(int itemId, string userName, string title, DateTime expireDate,
                                       string description, string moreLink, string mobileMoreLink)
        {
            if (userName.Length < 1) userName = "unknown";

            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_UpdateAnnouncement", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemId;
            command.Parameters.Add(parameterItemID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            command.Parameters.Add(parameterUserName);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 150);
            parameterTitle.Value = title;
            command.Parameters.Add(parameterTitle);

            var parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150);
            parameterMoreLink.Value = moreLink;
            command.Parameters.Add(parameterMoreLink);

            var parameterMobileMoreLink = new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150);
            parameterMobileMoreLink.Value = mobileMoreLink;
            command.Parameters.Add(parameterMobileMoreLink);

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;
            command.Parameters.Add(parameterExpireDate);

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;
            command.Parameters.Add(parameterDescription);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        #endregion
    }
}