using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    internal static class DbHelper
    {

        public static void ExecuteNonQuery(string connectionString, string commandText, params SqlParameter[] parameters)
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
    }
}