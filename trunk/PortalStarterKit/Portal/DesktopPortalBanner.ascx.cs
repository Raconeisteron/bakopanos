using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using ASPNET.StarterKit.Portal.Components;

namespace ASPNET.StarterKit.Portal
{
    public partial class DesktopPortalBanner : UserControl
    {
        protected String LogoffLink = "";
        public bool ShowTabs = true;


        public int tabIndex;

        public DesktopPortalBanner()
        {
            Init += Page_Init;
        }

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
            if (ShowTabs)
            {
                tabIndex = portalSettings.ActiveTab.TabIndex;

                // Build list of tabs to be shown to user                                   
                var authorizedTabs = new ArrayList();
                int addedTabs = 0;

                for (int i = 0; i < portalSettings.DesktopTabs.Count; i++)
                {
                    var tab = (TabStripDetails) portalSettings.DesktopTabs[i];

                    if (PortalSecurity.IsInRoles(tab.AuthorizedRoles))
                    {
                        authorizedTabs.Add(tab);
                    }

                    if (addedTabs == tabIndex)
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

        protected void Page_Init(object sender, EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

        #region Web Form Designer generated code

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}