using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlEventsDb : IEventsDb
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        #region IEventsDb Members

        public IDataReader GetEvents(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetEvents", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;
            command.Parameters.Add(parameterModuleId);

            // Execute the command
            connection.Open();
            SqlDataReader result = command.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleEvent(int itemId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetSingleEvent", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;
            command.Parameters.Add(parameterItemId);

            // Execute the command
            connection.Open();
            SqlDataReader result = command.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        public void DeleteEvent(int itemID)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_DeleteEvent", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            command.Parameters.Add(parameterItemID);

            // Open the database connection and execute SQL Command
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public int AddEvent(int moduleId, string userName, string title, DateTime expireDate,
                            string description, string wherewhen)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_AddEvent", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Direction = ParameterDirection.Output;
            command.Parameters.Add(parameterItemID);

            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleId;
            command.Parameters.Add(parameterModuleID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            command.Parameters.Add(parameterUserName);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            parameterTitle.Value = title;
            command.Parameters.Add(parameterTitle);

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100);
            parameterWhereWhen.Value = wherewhen;
            command.Parameters.Add(parameterWhereWhen);

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;
            command.Parameters.Add(parameterExpireDate);

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;
            command.Parameters.Add(parameterDescription);

            // Open the database connection and execute SQL Command
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            // Return the new Event ItemID
            return (int) parameterItemID.Value;
        }

        public void UpdateEvent(int itemId, string userName, string title, DateTime expireDate,
                                string description, string wherewhen)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_UpdateEvent", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemId;
            command.Parameters.Add(parameterItemID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            command.Parameters.Add(parameterUserName);

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            parameterTitle.Value = title;
            command.Parameters.Add(parameterTitle);

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100);
            parameterWhereWhen.Value = wherewhen;
            command.Parameters.Add(parameterWhereWhen);

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;
            command.Parameters.Add(parameterExpireDate);

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;
            command.Parameters.Add(parameterDescription);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        #endregion
    }
}