using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlHtmlTextsDb :Db, IHtmlTextsDb
    {
        

        public SqlHtmlTextsDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
        }

        #region IHtmlTextsDb Members

        public List<PortalHtmlText> GetHtmlText(int moduleId)
        {

            // Add Parameters to SPROC
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            // Execute method
            IDataReader reader = ExecuteReader("Portal_GetHtmlText", CommandType.StoredProcedure, parameterModuleId);

            var htmlTextList = new List<PortalHtmlText>();

            while (reader.Read())
                htmlTextList.Add(reader.ToPortalHtmlText());

            // Return list
            return htmlTextList;
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