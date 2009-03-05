using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal.Components
{
    public class PortalModuleControl : UserControl
    {
        // Private field variables

        private int _isEditable;
        private ModuleSettings _moduleConfiguration;
        private Hashtable _settings;

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

                    if (portalSettings.AlwaysShowEditButton ||
                        PortalSecurity.IsInRoles(_moduleConfiguration.AuthorizedEditRoles))
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
                    _settings = Configuration.GetModuleSettings(ModuleId);
                }

                return _settings;
            }
        }
    }

    public class CachedPortalModuleControl : Control
    {
        // Private field variables

        private String _cachedOutput = "";
        private ModuleSettings _moduleConfiguration;


        // Public property accessors

        public ModuleSettings ModuleConfiguration
        {
            get { return _moduleConfiguration; }
            set { _moduleConfiguration = value; }
        }

        public int ModuleId
        {
            get { return _moduleConfiguration.ModuleId; }
        }

        public int PortalId { get; set; }

        //*********************************************************************
        //
        // CacheKey Property
        //
        // The CacheKey property is used to calculate a "unique" cache key
        // entry to be used to store/retrieve the portal module's content
        // from the ASP.NET Cache.
        //
        //*********************************************************************

        public String CacheKey
        {
            get { return "Key:" + GetType() + ModuleId + PortalSecurity.IsInRoles(_moduleConfiguration.AuthorizedEditRoles); }
        }

        //*********************************************************************
        //
        // CreateChildControls Method
        //
        // The CreateChildControls method is called when the ASP.NET Page Framework
        // determines that it is time to instantiate a server control.
        // 
        // The CachedPortalModuleControl control overrides this method and attempts
        // to resolve any previously cached output of the portal module from the 
        // ASP.NET cache.  
        //
        // If it doesn't find cached output from a previous request, then the
        // CachedPortalModuleControl will instantiate and add the portal module's
        // User Control instance into the page tree.
        //
        //*********************************************************************

        protected override void CreateChildControls()
        {
            // Attempt to resolve previously cached content from the ASP.NET Cache

            if (_moduleConfiguration.CacheTime > 0)
            {
                _cachedOutput = (String) Context.Cache[CacheKey];
            }

            // If no cached content is found, then instantiate and add the portal
            // module user control into the portal's page server control tree

            if (_cachedOutput == null)
            {
                base.CreateChildControls();

                var module = (PortalModuleControl) Page.LoadControl(_moduleConfiguration.DesktopSrc);

                module.ModuleConfiguration = ModuleConfiguration;
                module.PortalId = PortalId;

                Controls.Add(module);
            }
        }

        //*********************************************************************
        //
        // Render Method
        //
        // The Render method is called when the ASP.NET Page Framework
        // determines that it is time to render content into the page output stream.
        // 
        // The CachedPortalModuleControl control overrides this method and captures
        // the output generated by the portal module user control.  It then 
        // adds this content into the ASP.NET Cache for future requests.
        //
        //*********************************************************************

        protected override void Render(HtmlTextWriter output)
        {
            // If no caching is specified, render the child tree and return 

            if (_moduleConfiguration.CacheTime == 0)
            {
                base.Render(output);
                return;
            }

            // If no cached output was found from a previous request, render
            // child controls into a TextWriter, and then cache the results
            // in the ASP.NET Cache for future requests.

            if (_cachedOutput == null)
            {
                TextWriter tempWriter = new StringWriter();
                base.Render(new HtmlTextWriter(tempWriter));
                _cachedOutput = tempWriter.ToString();

                Context.Cache.Insert(CacheKey, _cachedOutput, null,
                                     DateTime.Now.AddSeconds(_moduleConfiguration.CacheTime), TimeSpan.Zero);
            }

            // Output the user control's content

            output.Write(_cachedOutput);
        }
    }
}