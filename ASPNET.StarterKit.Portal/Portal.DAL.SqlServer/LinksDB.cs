using System.Data;

namespace Portal.Modules.DAL.SqlServer
{
    /// <summary>
    /// // Class that encapsulates all data logic necessary to add/query/delete
    /// links within the Portal database.
    /// </summary>
    internal class LinkDb : SqlDbHelper, ILinksDb
    {
        #region ILinksDb Members

        /// <summary>
        /// The GetLinks method returns a IDataReader containing all of the
        /// links for a specific portal module from the announcements
        /// database.
        /// </summary>        
        public IDataReader GetLinks(int moduleId)
        {
            return GetItems("Portal_GetLinks", InputModuleId(moduleId));
        }

        /// <summary>
        /// The GetSingleLink method returns a IDataReader containing details
        /// about a specific link from the Links database table.
        /// </summary>        
        public IDataReader GetSingleLink(int itemId)
        {
            return GetSingleItem("Portal_GetSingleLink", itemId);
        }

        /// <summary>
        /// The DeleteLink method deletes a specified link from
        /// the Links database table.
        /// </summary>
        public void DeleteLink(int itemId)
        {
            ExecuteNonQuery("Portal_DeleteLink", InputItemId(itemId));
        }

        /// <summary>
        /// The AddLink method adds a new link within the
        /// links database table, and returns ItemID value as a result.
        /// </summary>
        public int AddLink(int moduleId, string userName, string title, string url, string mobileUrl,
                           int viewOrder, string description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            return CreateItem("Portal_AddLink", ReturnValueItemId(), InputModuleId(moduleId),
                              InputUserName(userName),
                              InputTitle(title),
                              InputDescription(description),
                              InputUrl(url),
                              InputMobileUrl(mobileUrl),
                              InputViewOrder(viewOrder));
        }

        /// <summary>
        /// // The UpdateLink method updates a specified link within
        /// the Links database table.
        /// </summary>        
        public void UpdateLink(int itemId, string userName, string title, string url, string mobileUrl,
                               int viewOrder, string description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            ExecuteNonQuery("Portal_UpdateLink", InputItemId(itemId),
                       InputUserName(userName),
                       InputTitle(title),
                       InputDescription(description),
                       InputUrl(url),
                       InputMobileUrl(mobileUrl),
                       InputViewOrder(viewOrder));
        }

        #endregion
    }
}