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
            DbParameter parameterItemId = CreateParameter("@ItemID");
            parameterItemId.Direction = ParameterDirection.Output;
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterMoreLink = CreateParameter("@MoreLink", moreLink);
            DbParameter parameterMobileMoreLink = CreateParameter("@MobileMoreLink", mobileMoreLink);
            DbParameter parameterExpireDate = CreateParameter("@ExpireDate",expireDate);
            DbParameter parameterDescription = CreateParameter("@Description", description);

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
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterMoreLink = CreateParameter("@MoreLink", moreLink);
            DbParameter parameterMobileMoreLink = CreateParameter("@MobileMoreLink", mobileMoreLink);
            DbParameter parameterExpireDate = CreateParameter("@ExpireDate",expireDate);
            DbParameter parameterDescription = CreateParameter("@Description", description);

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