using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
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
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};

            return _db.GetDataTable("Portal_GetTopLevelMessages", parameterModuleId);
        }

        /// <returns>
        /// Returns details for all of the messages the thread, as identified by the Parent id string.
        /// </returns>
        public DataTable GetThreadMessages(String parent)
        {
            var parameterParent = new SqlParameter("@Parent", SqlDbType.NVarChar, 750) {Value = parent};

            return _db.GetDataTable("Portal_GetThreadMessages", parameterParent);
        }

        /// <returns>
        /// The GetSingleMessage method returns the details for the message
        /// specified by the itemId parameter.
        /// </returns>
        public DataRow GetSingleMessage(int itemId)
        {
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};

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

            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Direction = ParameterDirection.Output};
            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100) {Value = title};
            var parameterBody = new SqlParameter("@Body", SqlDbType.NVarChar, 3000) {Value = body};
            var parameterParentId = new SqlParameter("@ParentID", SqlDbType.Int, 4) {Value = parentId};
            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};

            return _db.ExecuteNonQuery<int>("Portal_AddMessage", parameterItemId, parameterTitle, parameterBody,
                                        parameterParentId,
                                        parameterUserName, parameterModuleId);
        }

        #endregion
    }
}