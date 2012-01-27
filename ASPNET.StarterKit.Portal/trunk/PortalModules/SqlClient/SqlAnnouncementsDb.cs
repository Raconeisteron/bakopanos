using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlAnnouncementsDb : Db, IAnnouncementsDb
    {
        public SqlAnnouncementsDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
        }

        #region IAnnouncementsDb Members

        public Collection<PortalAnnouncement> GetAnnouncements(int moduleId)
        {
            // Add Parameters to SPROC
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            //Execute method and populate reader
            IDataReader reader = ExecuteReader("Portal_GetAnnouncements", CommandType.StoredProcedure,
                                               parameterModuleId);

            var announcementList = new Collection<PortalAnnouncement>();

            while (reader.Read())
                announcementList.Add(reader.ToPortalAnnouncement());

            // Return list
            return announcementList;
        }

        public PortalAnnouncement GetSingleAnnouncement(int itemId)
        {
            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            // Execute method and populate reader
            IDataReader reader = ExecuteReader("Portal_GetSingleAnnouncement", CommandType.StoredProcedure,
                                               parameterItemId);

            // Read once, since we have only one result
            PortalAnnouncement announcement;
            if (reader.Read())
                announcement = reader.ToPortalAnnouncement(itemId);
            else
                return null;

            // Return the item
            return announcement;
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
            parameterItemId.Size = 4;
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterMoreLink = CreateParameter("@MoreLink", moreLink);
            DbParameter parameterMobileMoreLink = CreateParameter("@MobileMoreLink", mobileMoreLink);
            DbParameter parameterExpireDate = CreateParameter("@ExpireDate", expireDate);
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


            return Convert.ToInt32(parameterItemId.Value);
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
            DbParameter parameterExpireDate = CreateParameter("@ExpireDate", expireDate);
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