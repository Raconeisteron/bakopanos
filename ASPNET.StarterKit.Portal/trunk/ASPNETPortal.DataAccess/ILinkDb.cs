using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface ILinkDb
    {
        /// <returns>
        /// The GetLinks method returns a SqlDataReader containing all of the
        /// links for a specific portal module from the announcements
        /// database.
        /// </returns>
        DataTable GetLinks(int moduleId);

        /// <returns>
        /// The GetSingleLink method returns a SqlDataReader containing details
        /// about a specific link from the Links database table.
        /// </returns>
        DataRow GetSingleLink(int itemId);

        /// <summary>
        /// The DeleteLink method deletes a specified link from
        /// the Links database table.
        /// </summary>
        void DeleteLink(int itemId);

        /// <summary>
        /// The AddLink method adds a new link within the
        /// links database table, and returns ItemID value as a result.
        /// </summary>
        int AddLink(int moduleId, String userName, String title, String url, String mobileUrl,
                    int viewOrder, String description);

        /// <summary>
        /// The UpdateLink method updates a specified link within
        /// the Links database table.
        /// </summary>
        void UpdateLink(int itemId, String userName, String title, String url,
                        String mobileUrl, int viewOrder, String description);
    }
}