using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using ASPNET.StarterKit.Portal.Data;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlDiscussionsDb : Db, IDiscussionsDb
    {
        public SqlDiscussionsDb(string connectionString) : base(connectionString, "System.Data.SqlClient")
        {
        }

        #region IDiscussionsDb Members

        public Collection<PortalDiscussion> GetTopLevelMessages(int moduleId)
        {
            // Add Parameters to SPROC
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            //Execute method and populate reader
            IDataReader reader = ExecuteReader("Portal_GetTopLevelMessages", CommandType.StoredProcedure,
                                               parameterModuleId);

            var topLevelMessagesList = new Collection<PortalDiscussion>();

            while (reader.Read())
                topLevelMessagesList.Add(reader.ToPortalDiscussion());

            // Return list
            return topLevelMessagesList;
        }

        public Collection<PortalDiscussion> GetThreadMessages(String parent)
        {
            // Add Parameters to SPROC
            DbParameter parameterParent = CreateParameter("@Parent", parent);

            //Execute method and populate reader
            IDataReader reader = ExecuteReader("Portal_GetThreadMessages", CommandType.StoredProcedure, parameterParent);

            var threadMessageList = new Collection<PortalDiscussion>();

            while (reader.Read())
                threadMessageList.Add(reader.ToPortalDiscussion());

            // Return list
            return threadMessageList;
        }

        public PortalDiscussion GetSingleMessage(int itemId)
        {
            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            //Execute method and populate result
            IDataReader reader = ExecuteReader("Portal_GetSingleMessage", CommandType.StoredProcedure, parameterItemId);

            //Read once, since we have only one result (itemId is Unique)
            PortalDiscussion message;
            if (reader.Read())
                message = reader.ToPortalDiscussion(itemId);
            else
                return null;

            // Return the item
            return message;
        }

        public int AddMessage(int moduleId, int parentId, string userName, string title, string body)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID");
            parameterItemId.Direction = ParameterDirection.Output;
            parameterItemId.Size = 4;
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterBody = CreateParameter("@Body", body);
            DbParameter parameterParentId = CreateParameter("@ParentID", parentId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            //Execute method
            ExecuteNonQuery("Portal_AddMessage", CommandType.StoredProcedure,
                            parameterItemId,
                            parameterTitle,
                            parameterBody,
                            parameterParentId,
                            parameterUserName,
                            parameterModuleId
                );

            return Convert.ToInt32(parameterItemId.Value);
        }

        #endregion
    }
}