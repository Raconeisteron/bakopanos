using System;
using System.Web.UI;
using Microsoft.Practices.Unity;
using PortalStarterKit.Model;

namespace PortalStarterKit.Core
{
    /// <summary>
    ///   The PortalModuleControl class defines a custom base class inherited by all
    ///   desktop portal modules within the Portal.
    /// 
    ///   The PortalModuleControl class defines portal specific properties
    ///   that are used by the portal framework to correctly display portal modules
    /// </summary>
    public class PortalModule<T> : UserControl, IPortalControl
        where T : IPortalControl
    {
        // Public property accessors
        public string PortalId { get; set; }
        public string TabId { get; set; }

        public ModuleSettings ModuleSettings { get; set; }
        public IPortalSecurity PortalSecurity { get; set; }

        public bool IsEditable
        {
            get
            {
                //TODO
                return true;
            }
        }

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