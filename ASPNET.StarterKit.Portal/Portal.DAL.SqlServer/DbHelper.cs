using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    internal class DbHelper : SqlParameterHelper
    {
        protected static void ExecuteNonQuery(string connectionString, string commandText, params SqlParameter[] parameters)
        {
            // Create Instance of Connection and Command Object
            using (var myConnection = new SqlConnection(connectionString))
            {
                using (var myCommand = new SqlCommand(commandText, myConnection))
                {
                    // Mark the Command as a SPROC
                    myCommand.CommandType = CommandType.StoredProcedure;

                    // Add Parameters to SPROC
                    myCommand.Parameters.AddRange(parameters);

                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
        }

        protected static void DeleteItem(string connectionString, string commandText, int itemId)
        {
            // Create Instance of Connection and Command Object
            using (var myConnection = new SqlConnection(connectionString))
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

        protected static int CreateItem(string connectionString, string commandText, params SqlParameter[] parameters)
        {
            SqlParameter parameterItemId = ReturnValueItemId();

            // Create Instance of Connection and Command Object
            using (var myConnection = new SqlConnection(connectionString))
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

            return (int)parameterItemId.Value;
        }

        protected IDataReader GetItems(string connectionString, string commandText, int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(connectionString);
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

        protected static IDataReader GetSingleItem(string connectionString, string commandText, int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(connectionString);
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

    }
}