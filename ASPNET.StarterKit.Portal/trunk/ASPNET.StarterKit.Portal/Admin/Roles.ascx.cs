using System;
using System.Web.UI.WebControls;
using ASPNETPortal;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class Roles : PortalModuleControl
    {
        private int _tabId;
        private int _tabIndex;

        [Dependency]
        public IRolesDb Model { private get; set; }


        //*******************************************************
        //
        // The Page_Load server event handler on this user control is used
        // to populate the current roles settings from the configuration system
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

            // If this is the first visit to the page, bind the role data to the datalist
            if (Page.IsPostBack == false)
            {
                BindData();
            }
        }

        //*******************************************************
        //
        // The AddRole_Click server event handler is used to add
        // a new security role for this portal
        //
        //*******************************************************

        protected void AddRole_Click(Object sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // Add a new role to the database
            Model.AddRole(portalSettings.PortalId, "New Role");

            // set the edit item index to the last item
            rolesList.EditItemIndex = rolesList.Items.Count;

            // Rebind list
            BindData();
        }

        //*******************************************************
        //
        // The RolesList_ItemCommand server event handler on this page 
        // is used to handle the user editing and deleting roles
        // from the RolesList asp:datalist control
        //
        //*******************************************************

        private void RolesList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            var roleId = (int) rolesList.DataKeys[e.Item.ItemIndex];

            if (e.CommandName == "edit")
            {
                // Set editable list item index if "edit" button clicked next to the item
                rolesList.EditItemIndex = e.Item.ItemIndex;

                // Repopulate the datalist control
                BindData();
            }
            else if (e.CommandName == "apply")
            {
                // Apply changes
                String roleName = ((TextBox) e.Item.FindControl("roleName")).Text;

                // update database
                Model.UpdateRole(roleId, roleName);

                // Disable editable list item access
                rolesList.EditItemIndex = -1;

                // Repopulate the datalist control
                BindData();
            }
            else if (e.CommandName == "delete")
            {
                // update database
                Model.DeleteRole(roleId);

                // Ensure that item is not editable
                rolesList.EditItemIndex = -1;

                // Repopulate list
                BindData();
            }
            else if (e.CommandName == "members")
            {
                // Save role name changes first
                String roleName = ((TextBox) e.Item.FindControl("roleName")).Text;
                Model.UpdateRole(roleId, roleName);

                // redirect to edit page
                Response.Redirect("~/Admin/SecurityRoles.aspx?roleId=" + roleId + "&rolename=" + roleName +
                                  "&tabindex=" + _tabIndex + "&tabid=" + _tabId);
            }
        }

        //*******************************************************
        //
        // The BindData helper method is used to bind the list of 
        // security roles for this portal to an asp:datalist server control
        //
        //*******************************************************

        private void BindData()
        {
            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // Get the portal's roles from the database
            rolesList.DataSource = Model.GetPortalRoles(portalSettings.PortalId);
            rolesList.DataBind();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            rolesList.ItemCommand += RolesList_ItemCommand;
        }
    }
}