using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public interface IPortalModuleControl
    {
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        int ModuleId { get; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        int PortalId { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        bool IsEditable { get; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        ModuleSettings ModuleConfiguration { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        Hashtable Settings { get; }
    }

    //*********************************************************************
    //
    // PortalModuleControl Class
    //
    // The PortalModuleControl class defines a custom base class inherited by all
    // desktop portal modules within the Portal.
    // 
    // The PortalModuleControl class defines portal specific properties
    // that are used by the portal framework to correctly display portal modules
    //
    //*********************************************************************

    public class PortalModuleControl<T> : UserControl, IPortalModuleControl where T : class 
    {
        [Dependency]
        public IModulesConfig ModulesConfig { private get; set; }

        // Private field variables

        private int _isEditable;
        private ModuleSettings _moduleConfiguration;
        private Hashtable _settings;

        // Methods
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Global.BuildItemWithCurrentContext<T>(this);
        }

        // Public property accessors

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ModuleId
        {
            get { return _moduleConfiguration.ModuleId; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PortalId { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEditable
        {
            get
            {
                // Perform tri-state switch check to avoid having to perform a security
                // role lookup on every property access (instead caching the result)

                if (_isEditable == 0)
                {
                    // Obtain PortalSettings from Current Context

                    var portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

                    if (portalSettings.AlwaysShowEditButton || PortalSecurity.IsInRoles(_moduleConfiguration.AuthorizedEditRoles))
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ModuleSettings ModuleConfiguration
        {
            get { return _moduleConfiguration; }
            set { _moduleConfiguration = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Hashtable Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = ModulesConfig.GetModuleSettings(ModuleId);
                }

                return _settings;
            }
        }
    }
}