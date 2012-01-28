using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using ASPNET.StarterKit.Portal.Data;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlLinksDb : Db, ILinksDb
    {
        public SqlLinksDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
        }

        #region ILinksDb Members

        public Collection<PortalLink> GetLinks(int moduleId)
        {
            // Add Parameters to SPROC
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            // Execute method
            IDataReader reader = ExecuteReader("Portal_GetLinks", CommandType.StoredProcedure, parameterModuleId);

            var linkList = new Collection<PortalLink>();

            while (reader.Read())
                linkList.Add(reader.ToPortalLink());

            // Return list
            return linkList;
        }

        public PortalLink GetSingleLink(int itemId)
        {
            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            // Execute method
            IDataReader reader = ExecuteReader("Portal_GetSingleLink", CommandType.StoredProcedure, parameterItemId);

            //Read once, since we have only one result (itemId is Unique)
            PortalLink link;
            if (reader.Read())
                link = reader.ToPortalLink(itemId);
            else
                return null;

            // Return the item
            return link;
        }

        public void DeleteLink(int itemId)
        {
            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            //Execute Method
            ExecuteNonQuery("Portal_DeleteLink", CommandType.StoredProcedure, parameterItemId);
        }

        public int AddLink(int moduleId, string userName, string title, string url, string mobileUrl,
                           int viewOrder, string description)
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
            DbParameter parameterDescription = CreateParameter("@Description", description);
            DbParameter parameterUrl = CreateParameter("@Url", url);
            DbParameter parameterMobileUrl = CreateParameter("@MobileUrl", mobileUrl);
            DbParameter parameterViewOrder = CreateParameter("@ViewOrder", viewOrder);

            //Execute Method
            ExecuteNonQuery("Portal_AddLink", CommandType.StoredProcedure,
                            parameterItemId,
                            parameterModuleId,
                            parameterUserName,
                            parameterTitle,
                            parameterDescription,
                            parameterUrl,
                            parameterMobileUrl,
                            parameterViewOrder);


            return Convert.ToInt32(parameterItemId.Value);
        }

        public void UpdateLink(int itemId, string userName, string title, string url, string mobileUrl,
                               int viewOrder, string description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterDescription = CreateParameter("@Description", description);
            DbParameter parameterUrl = CreateParameter("@Url", url);
            DbParameter parameterMobileUrl = CreateParameter("@MobileUrl", mobileUrl);
            DbParameter parameterViewOrder = CreateParameter("@ViewOrder", viewOrder);

            //Execute method
            ExecuteNonQuery("Portal_UpdateLink", CommandType.StoredProcedure,
                            parameterItemId,
                            parameterUserName,
                            parameterTitle,
                            parameterDescription,
                            parameterUrl,
                            parameterMobileUrl,
                            parameterViewOrder);
        }

        #endregion
    }
}