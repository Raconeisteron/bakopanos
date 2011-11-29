using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.Modules.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// HTML/text within the Portal database.
    /// </summary>
    internal class HtmlTextDb : DbHelper, IHtmlTextDb
    {
        #region IHtmlTextDb Members

        /// <summary>
        /// The GetHtmlText method returns a IDataReader containing details
        /// about a specific item from the HtmlText database table.
        /// </summary>        
        public IDataReader GetHtmlText(int moduleId)
        {
            return GetItems("Portal_GetHtmlText", moduleId);
        }

        /// <summary>
        /// The UpdateHtmlText method updates a specified item within
        /// the HtmlText database table.
        /// </summary>        
        public void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_UpdateHtmlText", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputModuleId(moduleId));
            myCommand.Parameters.Add(InputDesktopHtml(desktopHtml));
            myCommand.Parameters.Add(InputMobileSummary(mobileSummary));
            myCommand.Parameters.Add(InputMobileDetails(mobileDetails));

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}