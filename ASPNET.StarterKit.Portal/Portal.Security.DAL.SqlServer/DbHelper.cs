using System.Data;
using System.Data.SqlClient;

namespace Portal.Security.DAL.SqlServer
{
    internal class DbHelper : SqlParameterHelper
    {
        internal string ConnectionString { get; set; }

        protected void DeleteItem(string commandText, int itemId)
        {
            // Create Instance of Connection and Command Object
            using (var myConnection = new SqlConnection(ConnectionString))
            {
                using (var myCommand = new SqlCommand(commandText, myConnection))
                {
                    // Mark the Command as a SPROC
                    myCommand.CommandType = CommandType.StoredProcedure;

                    // Add Parameters to SPROC
                    myCommand.Parameters.Add(InputItemId(itemId));

                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
        }

        protected int CreateItem(string commandText, params SqlParameter[] parameters)
        {
            SqlParameter parameterItemId = ReturnValueItemId();

            // Create Instance of Connection and Command Object
            using (var myConnection = new SqlConnection(ConnectionString))
            {
                using (var myCommand = new SqlCommand(commandText, myConnection))
                {
                    // Mark the Command as a SPROC
                    myCommand.CommandType = CommandType.StoredProcedure;

                    // Add Parameters to SPROC
                    myCommand.Parameters.Add(parameterItemId);
                    myCommand.Parameters.AddRange(parameters);

                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }
            }

            return (int) parameterItemId.Value;
        }

        protected IDataReader GetItems(string commandText, int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand(commandText, myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputModuleId(moduleId));

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        protected IDataReader GetSingleItem(string commandText, int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand(commandText, myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputItemId(itemId));

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        protected void UpdateItem(string commandText, int itemId, params SqlParameter[] parameters)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand(commandText, myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(InputItemId(itemId));
            myCommand.Parameters.AddRange(parameters);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}