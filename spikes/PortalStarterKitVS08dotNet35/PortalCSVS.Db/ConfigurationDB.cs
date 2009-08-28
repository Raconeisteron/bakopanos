using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    //*********************************************************************    
    // Class that encapsulates all data logic necessary to add/query/delete
    // configuration within the Portal database.
    //
    //*********************************************************************

    public class ConfigurationDb : IConfigurationDb
    {
        [Dependency]
        public IDatabaseConfiguration DatabaseConfiguration
        {
            private get;
            set;
        }

       public void DeletePortalModule(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(DatabaseConfiguration.ConnectionString);
            var myCommand = new SqlCommand("Portal_DeleteModule", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            myConnection.Open();

            parameterModuleID.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleID);

            // Open the database connection and execute the command
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

    }
}