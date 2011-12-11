using System;
using System.Web.UI.WebControls;

namespace ASPNET.StarterKit.Portal
{
    public partial class ModuleDefs : PortalModuleControl
    {
        private int _tabId;
        private int _tabIndex;


        //*******************************************************
        //
        // The Page_Load server event handler on this user control is used
        // to populate the current defs settings from the configuration system
        //
        //*******************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verify that the current user has access to access this page
            if (PortalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            if (Request.Params["tabid"] != null)
            {
                _tabId = Int32.Parse(Request.Params["tabid"]);
            }
            if (Request.Params["tabindex"] != null)
            {
                _tabIndex = Int32.Parse(Request.Params["tabindex"]);
            }

            // If this is the first visit to the page, bind the definition data to the datalist
            if (Page.IsPostBack == false)
            {
                BindData();
            }
        }

        //*******************************************************
        //
        // The AddDef_Click server event handler is used to add
        // a new module definition for this portal
        //
        //*******************************************************

        protected void AddDef_Click(Object Sender, EventArgs e)
        {
            // redirect to edit page
            Response.Redirect("~/Admin/ModuleDefinitions.aspx?defId=-1&tabindex=" + _tabIndex + "&tabid=" + _tabId);
        }

        //*******************************************************
        //
        // The DefsList_ItemCommand server event handler on this page 
        // is used to handle the user editing module definitions
        // from the DefsList asp:datalist control
        //
        //*******************************************************

        private void DefsList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            var moduleDefId = (int) defsList.DataKeys[e.Item.ItemIndex];

            // redirect to edit page
            Response.Redirect("~/Admin/ModuleDefinitions.aspx?defId=" + moduleDefId + "&tabindex=" + _tabIndex +
                              "&tabid=" + _tabId);
        }

        //*******************************************************
        //
        // The BindData helper method is used to bind the list of 
        // module definitions for this portal to an asp:datalist server control
        //
        //*******************************************************

        private void BindData()
        {
            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // Get the portal's defs from the database
            defsList.DataSource = Configuration.GetModuleDefinitions(portalSettings.PortalId);
            defsList.DataBind();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            defsList.ItemCommand += DefsList_ItemCommand;
        }
    }
}