using System;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class ModuleDefinitions : Page
    {
        private int _defId = -1;
        private IModuleDefConfigurationDb _moduleDefConfigurationDb;
        private IPortalSecurity _portalSecurity;
        private int _tabId;
        private int _tabIndex;

        [InjectionMethod]
        public void Initialize(IPortalSecurity portalSecurity, IModuleDefConfigurationDb moduleDefConfigurationDb)
        {
            _portalSecurity = portalSecurity;
            _moduleDefConfigurationDb = moduleDefConfigurationDb;
        }

        //*******************************************************
        //
        // The Page_Load server event handler on this page is used
        // to populate the role information for the page
        //
        //*******************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verify that the current user has access to access this page
            if (_portalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Calculate security defId
            if (Request.Params["defid"] != null)
            {
                _defId = Int32.Parse(Request.Params["defid"]);
            }
            if (Request.Params["tabid"] != null)
            {
                _tabId = Int32.Parse(Request.Params["tabid"]);
            }
            if (Request.Params["tabindex"] != null)
            {
                _tabIndex = Int32.Parse(Request.Params["tabindex"]);
            }


            // If this is the first visit to the page, bind the definition data 
            if (Page.IsPostBack == false)
            {
                if (_defId == -1)
                {
                    // new module definition
                    FriendlyName.Text = "New Definition";
                    DesktopSrc.Text = "DesktopModules/SomeModule.ascx";
                    MobileSrc.Text = "MobileModules/SomeModule.ascx";
                }
                else
                {
                    // Obtain the module definition to edit from the database
                    ModuleDefinitionItem modDef = _moduleDefConfigurationDb.GetSingleModuleDefinition(_defId);

                    // Read in information
                    FriendlyName.Text = modDef.FriendlyName;
                    DesktopSrc.Text = modDef.DesktopSourceFile;
                    MobileSrc.Text = modDef.MobileSourceFile;
                }
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update a link.  It  uses the ASPNET.StarterKit.Portal.LinkDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (_defId == -1)
                {
                    // Obtain PortalSettings from Current Context
                    var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

                    // Add a new module definition to the database
                    _moduleDefConfigurationDb.AddModuleDefinition(portalSettings.Portal.PortalId, FriendlyName.Text,
                                                                  DesktopSrc.Text,
                                                                  MobileSrc.Text);
                }
                else
                {
                    // update the module definition
                    _moduleDefConfigurationDb.UpdateModuleDefinition(_defId, FriendlyName.Text, DesktopSrc.Text,
                                                                     MobileSrc.Text);
                }

                // Redirect back to the portal admin page
                Response.Redirect("~/DesktopDefault.aspx?tabindex=" + _tabIndex + "&tabid=" + _tabId);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete an
        // a link.  It  uses the ASPNET.StarterKit.Portal.LinksDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            // delete definition
            _moduleDefConfigurationDb.DeleteModuleDefinition(_defId);

            // Redirect back to the portal admin page
            Response.Redirect("~/DesktopDefault.aspx?tabindex=" + _tabIndex + "&tabid=" + _tabId);
        }

        //****************************************************************
        //
        // The CancelBtn_Click event handler on this Page is used to cancel
        // out of the page -- and return the user back to the portal home
        // page.
        //
        //****************************************************************

        protected void CancelBtn_Click(Object sender, EventArgs e)
        {
            // Redirect back to the portal home page
            Response.Redirect("~/DesktopDefault.aspx?tabindex=" + _tabIndex + "&tabid=" + _tabId);
        }
    }
}