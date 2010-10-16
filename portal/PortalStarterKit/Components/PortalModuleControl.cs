using System;
using System.Collections;
using Microsoft.Practices.Unity;

namespace PortalStarterKit.Components
{
    /// <summary>
    ///   The PortalModuleControl class defines a custom base class inherited by all
    ///   desktop portal modules within the Portal.
    /// 
    ///   The PortalModuleControl class defines portal specific properties
    ///   that are used by the portal framework to correctly display portal modules
    /// </summary>
    public class PortalModuleControl<T> : PortalModuleUserControl
        where T : PortalModuleUserControl
    {
        // Private field variables
        private int _isEditable;
        private Hashtable _settings;

        // Public property accessors
        public string PortalId { get; set; }

        public string TabId { get; set; }

        public ModuleSettings ModuleConfiguration { get; set; }


        protected override void OnInit(EventArgs e)
        {
            Global.BuildItemWithCurrentContext<T>(this);

            var siteConfiguration = Global.Container.Resolve<ISiteConfigurationService>();

            PortalId = Page.RouteData.Values["portalId"] as string ??
                       siteConfiguration.GetPortals()[0].PortalId;

            TabId = Page.RouteData.Values["tabId"] as string ??
                    siteConfiguration.GetTabs(PortalId)[0].TabId;

            base.OnInit(e);
        }
    }
}