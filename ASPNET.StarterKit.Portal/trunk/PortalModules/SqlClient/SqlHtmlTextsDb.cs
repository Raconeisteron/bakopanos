using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlHtmlTextsDb :Db, IHtmlTextsDb
    {
        private readonly string _connectionString;

        public SqlHtmlTextsDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
            _connectionString = connectionString;
        }

        #region IHtmlTextsDb Members

        public IDataReader GetHtmlText(int moduleId)
        {

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetHtmlText", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails)
        {

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;

            var parameterDesktopHtml = new SqlParameter("@DesktopHtml", SqlDbType.NText);
            parameterDesktopHtml.Value = desktopHtml;

            var parameterMobileSummary = new SqlParameter("@MobileSummary", SqlDbType.NText);
            parameterMobileSummary.Value = mobileSummary;

            var parameterMobileDetails = new SqlParameter("@MobileDetails", SqlDbType.NText);
            parameterMobileDetails.Value = mobileDetails;

            //Execute Method
            ExecuteNonQuery("Portal_UpdateHtmlText", CommandType.StoredProcedure,
                parameterModuleId,
                parameterDesktopHtml,
                parameterMobileSummary,
                parameterMobileDetails);

        }

        #endregion
    }
}