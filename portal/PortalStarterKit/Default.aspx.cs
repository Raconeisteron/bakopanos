using System;
using System.Web.UI;
using PortalStarterKit.Components;

namespace PortalStarterKit
{
    public partial class _Default : PortalPage<_Default>
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            TabSettings activeTab = SiteConfiguration.GetTab(PortalId, TabId);

            // Dynamically Populate the Left, Center and Right pane sections of the portal page
            if (activeTab.Modules.Count > 0)
            {
                // Loop through each entry in the configuration system for this tab
                foreach (ModuleSettings moduleSettings in activeTab.Modules)
                {
                    Control parent = null;

                    switch (moduleSettings.PaneName)
                    {
                        case PortalPane.Left:
                            parent = LeftPane;
                            break;
                        case PortalPane.Content:
                            parent = ContentPane;
                            break;
                        case PortalPane.Right:
                            parent = RightPane;
                            break;
                    }

                    // create the user control instance and dynamically
                    // inject it into the page.  
                    var portalModule = (IPortalControl)Page.LoadControl(moduleSettings.DesktopSrc);
                    portalModule.PortalId = PortalId;
                    portalModule.TabId = TabId;
                    portalModule.ModuleConfiguration = moduleSettings;
                    portalModule.PortalSecurity = PortalSecurity;

                    parent.Controls.Add((UserControl)portalModule);

                    // Dynamically inject separator break between portal modules
                    parent.Controls.Add(new LiteralControl("<" + "br" + ">"));
                    parent.Visible = true;
                }
            }
        }
    }
}