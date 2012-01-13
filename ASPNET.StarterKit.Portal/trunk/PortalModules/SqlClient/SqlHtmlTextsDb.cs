using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlHtmlTextsDb : IHtmlTextsDb
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        #region IHtmlTextsDb Members

        public IDataReader GetHtmlText(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_GetHtmlText", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleId;
            command.Parameters.Add(parameterModuleID);

            // Execute the command
            connection.Open();
            SqlDataReader result = command.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        public void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails)
        {
            // Create Instance of Connection and Command Object
            var connection =
                new SqlConnection(_connectionString);
            var command = new SqlCommand("Portal_UpdateHtmlText", connection);

            // Mark the Command as a SPROC
            command.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleId;
            command.Parameters.Add(parameterModuleID);

            var parameterDesktopHtml = new SqlParameter("@DesktopHtml", SqlDbType.NText);
            parameterDesktopHtml.Value = desktopHtml;
            command.Parameters.Add(parameterDesktopHtml);

            var parameterMobileSummary = new SqlParameter("@MobileSummary", SqlDbType.NText);
            parameterMobileSummary.Value = mobileSummary;
            command.Parameters.Add(parameterMobileSummary);

            var parameterMobileDetails = new SqlParameter("@MobileDetails", SqlDbType.NText);
            parameterMobileDetails.Value = mobileDetails;
            command.Parameters.Add(parameterMobileDetails);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        #endregion
    }
}