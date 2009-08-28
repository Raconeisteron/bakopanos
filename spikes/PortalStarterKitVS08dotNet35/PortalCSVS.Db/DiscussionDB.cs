using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public class DiscussionItem
    {
        public int ModuleId { get; set; }
        public int ParentId { get; set; }
        public String UserName { get; set; }
        public String Title { get; set; }
        public String Body { get; set; }
    }


    //*********************************************************************
    //
    // DiscussionDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // discussions within the Portal database.
    //
    //*********************************************************************

    public class DiscussionDB : IDiscussionDB
    {
        [Dependency]
        public IDatabaseConfiguration DatabaseConfiguration
        {
            private get;
            set;
        }

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

        #region IDiscussionDB Members

        public DbDataReader GetTopLevelMessages(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(DatabaseConfiguration.ConnectionString);
            var myCommand = new SqlCommand("Portal_GetTopLevelMessages", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;
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

        public DbDataReader GetThreadMessages(String parent)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(DatabaseConfiguration.ConnectionString);
            var myCommand = new SqlCommand("Portal_GetThreadMessages", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterParent = new SqlParameter("@Parent", SqlDbType.NVarChar, 750);
            parameterParent.Value = parent;
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

        public DbDataReader GetSingleMessage(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(DatabaseConfiguration.ConnectionString);
            var myCommand = new SqlCommand("Portal_GetSingleMessage", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;
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

        public int AddMessage(int moduleId, int parentId, String userName, String title, String body)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(DatabaseConfiguration.ConnectionString);
            var myCommand = new SqlCommand("Portal_AddMessage", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterItemID);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            var parameterBody = new SqlParameter("@Body", SqlDbType.NVarChar, 3000);
            parameterBody.Value = body;
            myCommand.Parameters.Add(parameterBody);

            var parameterParentID = new SqlParameter("@ParentID", SqlDbType.Int, 4);
            parameterParentID.Value = parentId;
            myCommand.Parameters.Add(parameterParentID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int) parameterItemID.Value;
        }

        #endregion
    }
}