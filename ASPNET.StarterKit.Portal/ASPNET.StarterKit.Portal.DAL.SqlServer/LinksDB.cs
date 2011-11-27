using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    //*********************************************************************
    //
    // LinkDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // links within the Portal database.
    //
    //*********************************************************************

    internal class LinkDB : ILinkDB
    {
        private readonly string _connectionString;

        public LinkDB(string connectionString)
        {
            _connectionString = connectionString;
        }

        //*********************************************************************
        //
        // GetLinks Method
        //
        // The GetLinks method returns a IDataReader containing all of the
        // links for a specific portal module from the announcements
        // database.
        //
        // Other relevant sources:
        //     + <a href="GetLinks.htm" style="color:green">GetLinks Stored Procedure</a>
        //
        //*********************************************************************

        #region ILinkDB Members

        public IDataReader GetLinks(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetLinks", myConnection);

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
        // GetSingleLink Method
        //
        // The GetSingleLink method returns a IDataReader containing details
        // about a specific link from the Links database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleLink.htm" style="color:green">GetSingleLink Stored Procedure</a>
        //
        //*********************************************************************

        public IDataReader GetSingleLink(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemId);

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

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

        public void DeleteLink(int itemID)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemID);

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

        public int AddLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
                           int viewOrder, String description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_AddLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = myCommand.AddParameterItemId();
            myCommand.AddParameterModuleId(moduleId);
            myCommand.AddParameterUserName(userName);

            myCommand.AddParameterTitle(title);

            myCommand.AddParameterDescription(description);

            var parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 100);
            parameterUrl.Value = url;
            myCommand.Parameters.Add(parameterUrl);

            var parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100);
            parameterMobileUrl.Value = mobileUrl;
            myCommand.Parameters.Add(parameterMobileUrl);

            var parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4);
            parameterViewOrder.Value = viewOrder;
            myCommand.Parameters.Add(parameterViewOrder);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int) parameterItemID.Value;
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

        public void UpdateLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
                               int viewOrder, String description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemId);
            myCommand.AddParameterUserName(userName);
            myCommand.AddParameterTitle(title);
            myCommand.AddParameterDescription(description);

            var parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 100);
            parameterUrl.Value = url;
            myCommand.Parameters.Add(parameterUrl);

            var parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100);
            parameterMobileUrl.Value = mobileUrl;
            myCommand.Parameters.Add(parameterMobileUrl);

            var parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4);
            parameterViewOrder.Value = viewOrder;
            myCommand.Parameters.Add(parameterViewOrder);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}