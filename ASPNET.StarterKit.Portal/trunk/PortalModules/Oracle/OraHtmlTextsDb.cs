using System;
using System.Collections.Generic;
using ASPNET.StarterKit.Portal.PortalDao;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraHtmlTextsDb : IHtmlTextsDb
    {
        #region IHtmlTextsDb Members

        public List<PortalHtmlText> GetHtmlText(int moduleId)
        {
            throw new NotImplementedException();
        }

        public void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}