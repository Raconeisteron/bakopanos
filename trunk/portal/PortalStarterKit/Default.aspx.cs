﻿using System;
using System.Web.UI;
using ASPNET.StarterKit.Portal;
using Microsoft.Practices.Unity;

namespace PortalStarterKit
{
    public partial class _Default : PortalPage<_Default>
    {
        [Dependency]
        public ISiteConfigurationService ConfigurationService
        {
            private
            get; set;
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            PortalSettings activePortal = null;
            string portalId = Page.RouteData.Values["portalId"] as string;
            if (portalId == null)
            {
                //todo
                activePortal = ConfigurationService.DefaultPortal;
            }
            else
            {
                activePortal = ConfigurationService.ActivePortal(portalId);

            }

            TabSettings activeTab = null;
            string tabId = Page.RouteData.Values["tabId"] as string;
            if (tabId==null)
            {
                //todo
                activeTab = ConfigurationService.DefaultTab;
            }
            else
            {
                activeTab = ConfigurationService.ActiveTab(portalId, tabId);                
            }

            // Dynamically Populate the Left, Center and Right pane sections of the portal page
            if (activeTab.Modules.Count > 0)
            {
                // Loop through each entry in the configuration system for this tab
                foreach (ModuleSettings moduleSettings in activeTab.Modules)
                {
                    Control parent=null;

                    switch (moduleSettings.PaneName)
                    {
                        case "LeftPane":
                            parent = LeftPane;
                            break;
                        case "ContentPane":
                            parent = ContentPane;
                            break;
                        case "RightPane":
                            parent = RightPane;
                            break;
                    }

                   
    
                    // create the user control instance and dynamically
                    // inject it into the page.  
                    dynamic portalModule =  Page.LoadControl(moduleSettings.DesktopSrc);

                    BuildUpControl(portalModule);

                    portalModule.PortalId = activePortal.PortalId;
                    portalModule.TabId = activePortal.ActiveTab.TabId;
                    portalModule.ModuleConfiguration = moduleSettings;

                    parent.Controls.Add(portalModule);
               

                    // Dynamically inject separator break between portal modules
                    parent.Controls.Add(new LiteralControl("<" + "br" + ">"));
                    parent.Visible = true;
                }
            }
        }
    }
}