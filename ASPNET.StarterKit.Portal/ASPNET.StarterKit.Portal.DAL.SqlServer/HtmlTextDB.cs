using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// HTML/text within the Portal database.
    /// </summary>
    internal class HtmlTextDb : IHtmlTextDb
    {
        private readonly string _connectionString;

        public HtmlTextDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        //*********************************************************************
        //
        // GetHtmlText Method
        //
        // The GetHtmlText method returns a IDataReader containing details
        // about a specific item from the HtmlText database table.
        //
        // Other relevant sources:
        //     + <a href="GetHtmlText.htm" style="color:green">GetHtmlText Stored Procedure</a>
        //
        //*********************************************************************

        #region IHtmlTextDb Members

        public IDataReader GetHtmlText(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetHtmlText", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterModuleId(moduleId);

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }


        //*********************************************************************
        //
        // UpdateHtmlText Method
        //
        // The UpdateHtmlText method updates a specified item within
        // the HtmlText database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateHtmlText.htm" style="color:green">UpdateHtmlText Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateHtmlText", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterModuleId(moduleId);

            var parameterDesktopHtml = new SqlParameter("@DesktopHtml", SqlDbType.NText);
            parameterDesktopHtml.Value = desktopHtml;
            myCommand.Parameters.Add(parameterDesktopHtml);

            var parameterMobileSummary = new SqlParameter("@MobileSummary", SqlDbType.NText);
            parameterMobileSummary.Value = mobileSummary;
            myCommand.Parameters.Add(parameterMobileSummary);

            var parameterMobileDetails = new SqlParameter("@MobileDetails", SqlDbType.NText);
            parameterMobileDetails.Value = mobileDetails;
            myCommand.Parameters.Add(parameterMobileDetails);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}