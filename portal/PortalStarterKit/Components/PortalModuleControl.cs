using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using PortalStarterKit;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// The PortalModuleControl class defines a custom base class inherited by all
    /// desktop portal modules within the Portal.
    /// 
    /// The PortalModuleControl class defines portal specific properties
    /// that are used by the portal framework to correctly display portal modules
    /// </summary>
    public class PortalModuleControl<T> : UserControl
        where T : class 
    {
        // Private field variables

        private int _isEditable;
        private Hashtable _settings;

        // Public property accessors

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ModuleId
        {
            get { return ModuleConfiguration.ModuleId; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PortalId { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TabId { get; set; }


        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ModuleSettings ModuleConfiguration { get; set; }

        // Methods
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Global.BuildItemWithCurrentContext<T>(this);
        }
    }
}