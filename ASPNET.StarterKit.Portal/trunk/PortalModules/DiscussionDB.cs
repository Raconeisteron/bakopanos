using System;
using System.Data;
using System.Data.Common;
using Framework.Data;

namespace ASPNETPortal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// discussions within the Portal database.
    /// </summary>
    internal class DiscussionDb : IDiscussionDb
    {
        private readonly IDbHelper _db;

        public DiscussionDb(IDbHelper db)
        {
            _db = db;
        }

        #region IDiscussionDb Members

        /// <returns>
        /// Returns details for all of the messages in the discussion specified by ModuleID.
        /// </returns>
        public DataTable GetTopLevelMessages(int moduleId)
        {
            DbParameter parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);

            return _db.GetDataTable("Portal_GetTopLevelMessages", parameterModuleId);
        }

        /// <returns>
        /// Returns details for all of the messages the thread, as identified by the Parent id string.
        /// </returns>
        public DataTable GetThreadMessages(String parent)
        {
            DbParameter parameterParent = _db.CreateParameter("@Parent", parent);

            return _db.GetDataTable("Portal_GetThreadMessages", parameterParent);
        }

        /// <returns>
        /// The GetSingleMessage method returns the details for the message
        /// specified by the itemId parameter.
        /// </returns>
        public DataRow GetSingleMessage(int itemId)
        {
            DbParameter parameterItemId = _db.CreateParameter("@ItemID", itemId);

            return _db.GetDataRow("Portal_GetSingleMessage", parameterItemId);
        }

        /// <summary>
        /// The AddMessage method adds a new message within the
        /// Discussions database table, and returns ItemID value as a result.
        /// </summary>
        public int AddMessage(int moduleId, int parentId, String userName, String title, String body)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            DbParameter parameterItemId = _db.CreateIdentityParameter("@ItemID");
            DbParameter parameterTitle = _db.CreateParameter("@Title", title);
            DbParameter parameterBody = _db.CreateParameter("@Body", body);
            DbParameter parameterParentId = _db.CreateParameter("@ParentID", parentId);
            DbParameter parameterUserName = _db.CreateParameter("@UserName", userName);
            DbParameter parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);

            return _db.ExecuteNonQuery<int>("Portal_AddMessage", parameterItemId, parameterTitle, parameterBody,
                                            parameterParentId,
                                            parameterUserName, parameterModuleId);
        }

        #endregion
    }
}