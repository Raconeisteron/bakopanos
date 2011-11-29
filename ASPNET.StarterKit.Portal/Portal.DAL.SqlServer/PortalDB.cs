using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.Modules.DAL.SqlServer
{
    internal class PortalDb : DbHelper, IPortalDb
    {
        #region IPortalDb Members

        public void DeleteModule(params int[] moduleIds)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand("Portal_DeleteModule", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myConnection.Open();

            foreach (int moduleId in moduleIds)
            {
                myCommand.Parameters.Clear();
                myCommand.Parameters.Add(InputModuleId(moduleId));

                // Open the database connection and execute the command
                myCommand.ExecuteNonQuery();
            }

            // Close the connection
            myConnection.Close();
        }

        #endregion
    }
}