using System;
using System.Collections;
using System.Web;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public partial class DesktopPortalBanner : UserControl
    {
        protected String LogoffLink = "";
        private int _tabIndex;

        public bool ShowTabs { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

            // Dynamically Populate the Portal Site Name
            siteName.Text = portalSettings.PortalName;

            // If user logged in, customize welcome message
            if (Request.IsAuthenticated)
            {
                WelcomeMessage.Text = "Welcome " + Context.User.Identity.Name + "! <" + "span class=Accent" + ">|<" +
                                      "/span" + ">";

                // if authentication mode is Cookie, provide a logoff link
                if (Context.User.Identity.AuthenticationType == "Forms")
                {
                    LogoffLink = "<" + "span class=\"Accent\">|</span>\n" + "<" + "a href=" +
                                 Global.GetApplicationPath(Request) + "/Admin/Logoff.aspx class=SiteLink> Logoff" + "<" +
                                 "/a>";
                }
            }

            // Dynamically render portal tab strip
            if (!ShowTabs) return;

            _tabIndex = portalSettings.ActiveTab.TabIndex;

            // Build list of tabs to be shown to user                                   
            var authorizedTabs = new ArrayList();
            int addedTabs = 0;

            foreach (object t in portalSettings.DesktopTabs)
            {
                var tab = (TabStripDetails) t;

                if (PortalSecurity.IsInRoles(tab.AuthorizedRoles))
                {
                    authorizedTabs.Add(tab);
                }

                if (addedTabs == _tabIndex)
                {
                    tabs.SelectedIndex = addedTabs;
                }

                addedTabs++;
            }

            // Populate Tab List at Top of the Page with authorized tabs
            tabs.DataSource = authorizedTabs;
            tabs.DataBind();
        }
    }
}