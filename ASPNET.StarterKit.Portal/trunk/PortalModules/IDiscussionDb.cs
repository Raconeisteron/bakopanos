using System;
using System.Data;

namespace ASPNETPortal
{
    public interface IDiscussionDb
    {
        /// <returns>
        /// Returns details for all of the messages in the discussion specified by ModuleID.
        /// </returns>
        DataTable GetTopLevelMessages(int moduleId);

        /// <returns>
        /// Returns details for all of the messages the thread, as identified by the Parent id string.
        /// </returns>
        DataTable GetThreadMessages(String parent);

        /// <returns>
        /// The GetSingleMessage method returns the details for the message
        /// specified by the itemId parameter.
        /// </returns>
        DataRow GetSingleMessage(int itemId);

        /// <summary>
        /// The AddMessage method adds a new message within the
        /// Discussions database table, and returns ItemID value as a result.
        /// </summary>
        int AddMessage(int moduleId, int parentId, String userName, String title, String body);
    }
}