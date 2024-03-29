﻿using System;
using System.Web.UI;
using PortalStarterKit.Components;
using PortalStarterKit.Core;
using PortalStarterKit.Model;

namespace PortalStarterKit
{
    public partial class _Default : PortalPage<_Default>
    {
        protected void Page_Init(Object sender, EventArgs e)
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
                    parent.Controls.Add(GetPortalModule(this, moduleSettings));

                    // Dynamically inject separator break between portal modules
                    parent.Controls.Add(new LiteralControl("<" + "br" + ">"));
                    parent.Visible = true;
                }
            }
        }
    }
}