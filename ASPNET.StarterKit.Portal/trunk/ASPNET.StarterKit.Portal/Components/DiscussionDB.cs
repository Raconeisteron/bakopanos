using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    //*********************************************************************
    //
    // DiscussionDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // discussions within the Portal database.
    //
    //*********************************************************************

    public class DiscussionDb
    {
        //*******************************************************
        //
        // GetTopLevelMessages Method
        //
        // Returns details for all of the messages in the discussion specified by ModuleID.
        //
        // Other relevant sources:
        //     + <a href="GetTopLevelMessages.htm" style="color:green">GetTopLevelMessages Stored Procedure</a>
        //
        //*******************************************************

        public static SqlDataReader GetTopLevelMessages(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetTopLevelMessages", myConnection);

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

        //*******************************************************
        //
        // GetThreadMessages Method
        //
        // Returns details for all of the messages the thread, as identified by the Parent id string.
        //
        // Other relevant sources:
        //     + <a href="GetThreadMessages.htm" style="color:green">GetThreadMessages Stored Procedure</a>
        //
        //*******************************************************

        public static SqlDataReader GetThreadMessages(String parent)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetThreadMessages", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterParent = new SqlParameter("@Parent", SqlDbType.NVarChar, 750) {Value = parent};
            myCommand.Parameters.Add(parameterParent);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        //*******************************************************
        //
        // GetSingleMessage Method
        //
        // The GetSingleMessage method returns the details for the message
        // specified by the itemId parameter.
        //
        // Other relevant sources:
        //     + <a href="GetSingleMessage.htm" style="color:green">GetSingleMessage Stored Procedure</a>
        //
        //*******************************************************

        public static SqlDataReader GetSingleMessage(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetSingleMessage", myConnection);

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
        // AddMessage Method
        //
        // The AddMessage method adds a new message within the
        // Discussions database table, and returns ItemID value as a result.
        //
        // Other relevant sources:
        //     + <a href="AddMessage.htm" style="color:green">AddMessage Stored Procedure</a>
        //
        //*********************************************************************

        public static int AddMessage(int moduleId, int parentId, String userName, String title, String body)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_AddMessage", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Direction = ParameterDirection.Output};
            myCommand.Parameters.Add(parameterItemId);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100) {Value = title};
            myCommand.Parameters.Add(parameterTitle);

            var parameterBody = new SqlParameter("@Body", SqlDbType.NVarChar, 3000) {Value = body};
            myCommand.Parameters.Add(parameterBody);

            var parameterParentId = new SqlParameter("@ParentID", SqlDbType.Int, 4) {Value = parentId};
            myCommand.Parameters.Add(parameterParentId);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            myCommand.Parameters.Add(parameterUserName);

            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            myCommand.Parameters.Add(parameterModuleId);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int) parameterItemId.Value;
        }
    }
}