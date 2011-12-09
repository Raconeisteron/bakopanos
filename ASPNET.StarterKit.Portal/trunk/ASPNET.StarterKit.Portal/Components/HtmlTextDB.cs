using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    //*********************************************************************
    //
    // HtmlTextDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // HTML/text within the Portal database.
    //
    //*********************************************************************

    public class HtmlTextDb
    {
        //*********************************************************************
        //
        // GetHtmlText Method
        //
        // The GetHtmlText method returns a SqlDataReader containing details
        // about a specific item from the HtmlText database table.
        //
        // Other relevant sources:
        //     + <a href="GetHtmlText.htm" style="color:green">GetHtmlText Stored Procedure</a>
        //
        //*********************************************************************

        public static SqlDataReader GetHtmlText(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetHtmlText", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            myCommand.Parameters.Add(parameterModuleId);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

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

        public static void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_UpdateHtmlText", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            myCommand.Parameters.Add(parameterModuleId);

            var parameterDesktopHtml = new SqlParameter("@DesktopHtml", SqlDbType.NText) {Value = desktopHtml};
            myCommand.Parameters.Add(parameterDesktopHtml);

            var parameterMobileSummary = new SqlParameter("@MobileSummary", SqlDbType.NText) {Value = mobileSummary};
            myCommand.Parameters.Add(parameterMobileSummary);

            var parameterMobileDetails = new SqlParameter("@MobileDetails", SqlDbType.NText) {Value = mobileDetails};
            myCommand.Parameters.Add(parameterMobileDetails);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}