using System;
using System.Data;
using System.Data.SqlClient;

namespace Portal.Modules.Data
{
    public class AnnouncementSqlClientDb : Db, IAnnouncementDb
    {
        #region IAnnouncementDb Members

        public override IDataReader GetList(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_GetAnnouncements", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleId);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }


        public override IDataReader GetSingle(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_GetSingleAnnouncement", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;
            myCommand.Parameters.Add(parameterItemId);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }


        public override void Delete(int itemID)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
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


        public int AddAnnouncement(int moduleId, string userName, string title, DateTime expireDate,
                                   string description, string moreLink, string mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
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


        public void UpdateAnnouncement(int itemId, string userName, string title, DateTime expireDate,
                                       string description, string moreLink, string mobileMoreLink)
        {
            if (userName.Length < 1) userName = "unknown";

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
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