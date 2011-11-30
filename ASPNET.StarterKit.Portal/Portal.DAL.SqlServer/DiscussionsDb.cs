using System.Data;

namespace Portal.Modules.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// discussions within the Portal database.
    /// </summary>
    internal class DiscussionsDb : SqlDbHelper, IDiscussionsDb
    {
        #region IDiscussionsDb Members

        /// <summary>
        /// Returns details for all of the messages in the discussion specified by ModuleID.
        /// </summary>
        public IDataReader GetTopLevelMessages(int moduleId)
        {
            return GetItems("Portal_GetTopLevelMessages", InputModuleId(moduleId));
        }

        /// <summary>
        /// Returns details for all of the messages the thread, as identified by the Parent id string.
        /// </summary>
        public IDataReader GetThreadMessages(string parent)
        {
            return GetItems("Portal_GetThreadMessages", InputParent(parent));
        }

        /// <summary>
        /// The GetSingleMessage method returns the details for the message
        /// specified by the itemId parameter.
        /// </summary>        
        public IDataReader GetSingleMessage(int itemId)
        {
            return GetSingleItem("Portal_GetSingleMessage", itemId);
        }

        /// <summary>
        /// The AddMessage method adds a new message within the
        /// Discussions database table, and returns ItemID value as a result.
        /// </summary>        
        public int AddMessage(int moduleId, int parentId, string userName, string title, string body)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            return CreateItem("Portal_AddMessage", OutputItemId(), InputTitle(title), InputBody(body),
                              InputParentId(parentId),
                              InputUserName(userName), InputModuleId(moduleId));
        }

        #endregion
    }
}