using System.Web.UI;

namespace PortalStarterKit.Domain
{
    /// <summary>
    /// // The PortalModuleControl class defines a custom base class inherited by all
    /// desktop portal modules within the Portal.    
    /// The PortalModuleControl class defines portal specific properties
    /// that are used by the portal framework to correctly display portal modules
    /// </summary>
    public class PortalModuleControl : UserControl, IPortalTemplateControl
    {
        // Private field variables        
        private int _isEditable;

        public int ModuleId { get; set; }

        public bool IsEditable
        {
            get
            {
                // Perform tri-state switch check to avoid having to perform a security
                // role lookup on every property access (instead caching the result)
                if (_isEditable == 0)
                {
                    // Obtain SiteConfiguration from Current Context                    
                    if (SiteConfiguration.Portal.AlwaysShowEditButton
                        /*|| PortalSecurity.IsInRoles(_moduleConfiguration.AuthorizedEditRoles)*/)
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

        #region IPortalTemplateControl Members

        public Tab ActiveTab
        {
            get { return ((IPortalTemplateControl) Parent).ActiveTab; }
        }

        public SiteConfiguration SiteConfiguration
        {
            get { return ((IPortalTemplateControl) Parent).SiteConfiguration; }
        }

        #endregion
    }
}