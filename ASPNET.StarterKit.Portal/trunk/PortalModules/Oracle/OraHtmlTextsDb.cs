using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraHtmlTextsDb : IHtmlTextsDb
    {
        #region IHtmlTextsDb Members

        public PortalHtmlText GetHtmlText(int moduleId)
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