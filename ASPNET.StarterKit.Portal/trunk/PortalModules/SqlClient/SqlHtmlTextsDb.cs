using System.Data;
using System.Data.Common;
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
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetHtmlText", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails)
        {

            // Add Parameters to SPROC
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);
            DbParameter parameterDesktopHtml = CreateParameter("@DesktopHtml", desktopHtml);
            DbParameter parameterMobileSummary = CreateParameter("@MobileSummary", mobileSummary);
            DbParameter parameterMobileDetails = CreateParameter("@MobileDetails", mobileDetails);

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