using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraHtmlTextsDb:IHtmlTextsDb
    {
        public IDataReader GetHtmlText(int moduleId)
        {
            throw new NotImplementedException();
        }

        public void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails)
        {
            throw new NotImplementedException();
        }
    }
}