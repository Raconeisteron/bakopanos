using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    //*********************************************************************
    //
    // LinkDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // links within the Portal database.
    //
    //*********************************************************************

    public class LinkDb
    {
        //*********************************************************************
        //
        // GetLinks Method
        //
        // The GetLinks method returns a SqlDataReader containing all of the
        // links for a specific portal module from the announcements
        // database.
        //
        // Other relevant sources:
        //     + <a href="GetLinks.htm" style="color:green">GetLinks Stored Procedure</a>
        //
        //*********************************************************************

        public static SqlDataReader GetLinks(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetLinks", myConnection);

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
        // GetSingleLink Method
        //
        // The GetSingleLink method returns a SqlDataReader containing details
        // about a specific link from the Links database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleLink.htm" style="color:green">GetSingleLink Stored Procedure</a>
        //
        //*********************************************************************

        public static SqlDataReader GetSingleLink(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetSingleLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            myCommand.Parameters.Add(parameterItemId);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        //*********************************************************************
        //
        // DeleteLink Method
        //
        // The DeleteLink method deletes a specified link from
        // the Links database table.
        //
        // Other relevant sources:
        //     + <a href="DeleteLink.htm" style="color:green">DeleteLink Stored Procedure</a>
        //
        //*********************************************************************

        public static void DeleteLink(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_DeleteLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            myCommand.Parameters.Add(parameterItemId);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        //*********************************************************************
        //
        // AddLink Method
        //
        // The AddLink method adds a new link within the
        // links database table, and returns ItemID value as a result.
        //
        // Other relevant sources:
        //     + <a href="AddLink.htm" style="color:green">AddLink Stored Procedure</a>
        //
        //*********************************************************************

        public static int AddLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
                                  int viewOrder, String description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_AddLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Direction = ParameterDirection.Output};
            myCommand.Parameters.Add(parameterItemId);

            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            myCommand.Parameters.Add(parameterModuleId);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            myCommand.Parameters.Add(parameterUserName);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100) {Value = title};
            myCommand.Parameters.Add(parameterTitle);

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100) {Value = description};
            myCommand.Parameters.Add(parameterDescription);

            var parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 100) {Value = url};
            myCommand.Parameters.Add(parameterUrl);

            var parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100) {Value = mobileUrl};
            myCommand.Parameters.Add(parameterMobileUrl);

            var parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4) {Value = viewOrder};
            myCommand.Parameters.Add(parameterViewOrder);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int) parameterItemId.Value;
        }

        //*********************************************************************
        //
        // UpdateLink Method
        //
        // The UpdateLink method updates a specified link within
        // the Links database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateLink.htm" style="color:green">UpdateLink Stored Procedure</a>
        //
        //*********************************************************************

        public static void UpdateLink(int moduleId, int itemId, String userName, String title, String url,
                                      String mobileUrl,
                                      int viewOrder, String description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_UpdateLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            myCommand.Parameters.Add(parameterItemId);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            myCommand.Parameters.Add(parameterUserName);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100) {Value = title};
            myCommand.Parameters.Add(parameterTitle);

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100) {Value = description};
            myCommand.Parameters.Add(parameterDescription);

            var parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 100) {Value = url};
            myCommand.Parameters.Add(parameterUrl);

            var parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100) {Value = mobileUrl};
            myCommand.Parameters.Add(parameterMobileUrl);

            var parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4) {Value = viewOrder};
            myCommand.Parameters.Add(parameterViewOrder);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}