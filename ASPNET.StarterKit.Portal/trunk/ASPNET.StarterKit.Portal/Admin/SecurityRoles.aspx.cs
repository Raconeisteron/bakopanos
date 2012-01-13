using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNET.StarterKit.Portal
{
    public partial class SecurityRoles : Page
    {
        private int _roleId = -1;
        private string _roleName = "";
        private int _tabId;
        private int _tabIndex;

        //*******************************************************
        //
        // The Page_Load server event handler on this page is used
        // to populate the role information for the page
        //
        //*******************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            var portalSecurity = ComponentManager.Resolve<IPortalSecurity>();

            // Verify that the current user has access to access this page
            if (portalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Calculate security roleId
            if (Request.Params["roleid"] != null)
            {
                _roleId = Int32.Parse(Request.Params["roleid"]);
            }
            if (Request.Params["rolename"] != null)
            {
                _roleName = Request.Params["rolename"];
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
        // The Save_Click server event handler on this page is used
        // to save the current security settings to the configuration system
        //
        //*******************************************************

        protected void Save_Click(Object sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // Navigate back to admin page
            Response.Redirect("~/DesktopDefault.aspx?tabindex=" + _tabIndex + "&tabid=" + _tabId);
        }

        //*******************************************************
        //
        // The AddUser_Click server event handler is used to add
        // a new user to this security role 
        //
        //*******************************************************

        protected void AddUser_Click(Object sender, EventArgs e)
        {
            int userId;

            if (((LinkButton) sender).ID == "addNew")
            {
                // add new user to users table
                var users = ComponentManager.Resolve<IUsersDb>();
                if ((userId = users.AddUser(windowsUserName.Text, windowsUserName.Text, "acme")) == -1)
                {
                    Message.Text = "Add New Failed!  There is already an entry for <" + "u" + ">" + windowsUserName.Text +
                                   "<" + "/u" + "> in the Users database." + "<" + "br" + ">" +
                                   "Please use Add Existing for this user.";
                }
            }
            else
            {
                //get user id from dropdownlist of existing users
                userId = Int32.Parse(allUsers.SelectedItem.Value);
            }

            if (userId != -1)
            {
                // Add a new userRole to the database
                var roles = ComponentManager.Resolve<IRolesDb>();
                roles.AddUserRole(_roleId, userId);
            }

            // Rebind list
            BindData();
        }

        //*******************************************************
        //
        // The usersInRole_ItemCommand server event handler on this page 
        // is used to handle the user editing and deleting roles
        // from the usersInRole asp:datalist control
        //
        //*******************************************************

        private void UsersInRoleItemCommand(object sender, DataListCommandEventArgs e)
        {
            var roles = ComponentManager.Resolve<IRolesDb>();
            var userId = (int) usersInRole.DataKeys[e.Item.ItemIndex];

            if (e.CommandName == "delete")
            {
                // update database
                roles.DeleteUserRole(_roleId, userId);

                // Ensure that item is not editable
                usersInRole.EditItemIndex = -1;

                // Repopulate list
                BindData();
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
            // unhide the Windows Authentication UI, if application
            if (User.Identity.AuthenticationType != "Forms")
            {
                windowsUserName.Visible = true;
                addNew.Visible = true;
            }

            // add the role name to the title
            if (_roleName != "")
            {
                title.InnerText = "Role Membership: " + _roleName;
            }

            // Get the portal's roles from the database
            var roles = ComponentManager.Resolve<IRolesDb>();

            // bind users in role to DataList
            usersInRole.DataSource = roles.GetRoleMembers(_roleId);
            usersInRole.DataBind();

            // bind all portal users to dropdownlist
            allUsers.DataSource = roles.GetUsers();
            allUsers.DataBind();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            usersInRole.ItemCommand += UsersInRoleItemCommand;
        }
    }
}