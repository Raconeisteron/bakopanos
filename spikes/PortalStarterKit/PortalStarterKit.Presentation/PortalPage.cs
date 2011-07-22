using System;
using System.Linq;
using System.Web.UI;
using PortalStarterKit.Model;

namespace PortalStarterKit.Presentation
{
    public class PortalPage : Page, IPortalTemplateControl
    {
        #region IPortalTemplateControl Members

        public Tab ActiveTab
        {
            get
            {
                if (Page.Request.Params["tabId"] == null)
                {
                    return SiteConfiguration.Portals[0].Tabs[0];
                }
                int tabId = Convert.ToInt32(Page.Request.Params["tabId"]);
                if (SiteConfiguration.Portals[0].Tabs.Exists(item => item.TabId == tabId))
                {
                    return SiteConfiguration.Portals[0].Tabs.Single(item => item.TabId == tabId);
                }
                return null;
            }
        }

        public SiteConfiguration SiteConfiguration
        {
            get { return (SiteConfiguration) Context.Cache["SiteConfiguration"]; }
        }

        #endregion

        private static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
            {
                return root;
            }

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Dynamically Populate the Left, Center and Right pane sections of the portal page
            if (ActiveTab != null)
            {
                // Loop through each entry in the configuration system for this tab
                foreach (Module moduleSettings in ActiveTab.Modules)
                {
                    Control parent = FindControlRecursive(this, moduleSettings.PaneName.ToString());

                    // create the user control instance and dynamically
                    // inject it into the page.  Otherwise, create a cached module instance that
                    // may or may not optionally inject the module into the tree
                    string desktopSrc =
                        SiteConfiguration.ModuleDefinitions.Single(
                            item => item.ModuleDefId == moduleSettings.ModuleDefId).SourceFile;
                    var portalModule = (PortalModuleControl) Page.LoadControl(desktopSrc);
                    portalModule.ModuleId = moduleSettings.ModuleId;
                    parent.Controls.Add(portalModule);

                    // Dynamically inject separator break between portal modules
                    parent.Controls.Add(new LiteralControl("<" + "br" + ">"));
                    parent.Visible = true;
                }
            }
        }
    }
}