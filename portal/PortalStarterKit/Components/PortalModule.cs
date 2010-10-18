using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;
using System;
using PortalStarterKit.Components;

namespace PortalStarterKit.Components
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
        // Private field variables
        private int _isEditable;
        private Hashtable _settings;

        // Public property accessors
        public string PortalId { get; set; }

        public string TabId { get; set; }

        public ModuleSettings ModuleConfiguration { get; set; }

        public IPortalSecurity PortalSecurity { get; set; }

        public bool IsEditable
        {
            get
            {
                // Perform tri-state switch check to avoid having to perform a security
                // role lookup on every property access (instead caching the result)

                if (_isEditable == 0)
                {
                    // Obtain PortalSettings from Current Context

                    var portalSettings = (PortalSettings)HttpContext.Current.Items["PortalSettings"];

                    if (portalSettings.AlwaysShowEditButton || PortalSecurity.IsInRoles(ModuleConfiguration.AuthorizedEditRoles))
                    {
                        _isEditable = 1;
                    }
                    else
                    {
                        _isEditable = 2;
                    }
                }

                return (_isEditable == 1);
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