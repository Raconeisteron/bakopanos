using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlAnnouncementsDb : Db, IAnnouncementsDb
    {
        private readonly string _connectionString;

        public SqlAnnouncementsDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
            _connectionString = connectionString;
        }

        #region IAnnouncementsDb Members

        public IDataReader GetAnnouncements(int moduleId)
        {
            // Add Parameters to SPROC
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            //Execute method and populate result
            IDataReader reader = ExecuteReader("Portal_GetAnnouncements", CommandType.StoredProcedure,
                                               parameterModuleId);
            // Return the datareader
            return reader;
        }

        public IDataReader GetSingleAnnouncement(int itemId)
        {
            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            //Execute method and populate result
            IDataReader reader = ExecuteReader("Portal_GetSingleAnnouncement", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader
            return reader;
        }

        public void DeleteAnnouncement(int itemId)
        {
            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            //Execute method
            ExecuteNonQuery("Portal_DeleteAnnouncement", CommandType.StoredProcedure, parameterItemId);
        }

        public int AddAnnouncement(int moduleId, string userName, string title, DateTime expireDate,
                                   string description, string moreLink, string mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Direction = ParameterDirection.Output;

            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 150);
            parameterTitle.Value = title;

            var parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150);
            parameterMoreLink.Value = moreLink;

            var parameterMobileMoreLink = new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150);
            parameterMobileMoreLink.Value = mobileMoreLink;

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;

            //Execute method
            ExecuteNonQuery("Portal_AddAnnouncement", CommandType.StoredProcedure,
                parameterItemId,
                parameterModuleId,
                parameterUserName,
                parameterTitle,
                parameterMoreLink,
                parameterMobileMoreLink,
                parameterExpireDate,
                parameterDescription
                );



            return (int)parameterItemId.Value;
        }

        public void UpdateAnnouncement(int itemId, string userName, string title, DateTime expireDate,
                                       string description, string moreLink, string mobileMoreLink)
        {
            if (userName.Length < 1) userName = "unknown";


            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 150);
            parameterTitle.Value = title;

            var parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150);
            parameterMoreLink.Value = moreLink;

            var parameterMobileMoreLink = new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150);
            parameterMobileMoreLink.Value = mobileMoreLink;

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;

            //Execute method
            ExecuteNonQuery("Portal_UpdateAnnouncement", CommandType.StoredProcedure,
                parameterItemId,
                parameterUserName,
                parameterTitle,
                parameterMoreLink,
                parameterMobileMoreLink,
                parameterExpireDate,
                parameterDescription);

        }

        #endregion
    }
}