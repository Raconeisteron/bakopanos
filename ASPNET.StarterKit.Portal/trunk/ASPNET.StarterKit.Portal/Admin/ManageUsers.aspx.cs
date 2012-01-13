using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNET.StarterKit.Portal
{
    public partial class ManageUsers : Page
    {
        private int _tabId;
        private int _tabIndex;
        private int _userId = -1;
        private string _userName = "";


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

            // Calculate userid
            if (Request.Params["userid"] != null)
            {
                _userId = Int32.Parse(Request.Params["userid"]);
            }
            if (Request.Params["username"] != null)
            {
                _userName = Request.Params["username"];
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
                // new user?
                if (_userName == "")
                {
                    var users = new SqlUsersDb();

                    // make a unique new user record
                    int uid = -1;
                    int i = 0;

                    while (uid == -1)
                    {
                        string friendlyName = "New User created " + DateTime.Now;
                        _userName = "New User" + i;
                        uid = users.AddUser(friendlyName, _userName, "");
                        i++;
                    }

                    // redirect to this page with the corrected querystring args
                    Response.Redirect("~/Admin/ManageUsers.aspx?userId=" + uid + "&username=" + _userName + "&tabindex=" +
                                      _tabIndex + "&tabid=" + _tabId);
                }

                BindData();
            }
        }

        //*******************************************************
        //
        // The Save_Click server event handler on this page is used
        // to save the current security settings to the configuration system
        //
        //*******************************************************

        protected void Save_Click(Object Sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // Navigate back to admin page
            Response.Redirect("~/DesktopDefault.aspx?tabindex=" + _tabIndex + "&tabid=" + _tabId);
        }

        //*******************************************************
        //
        // The AddRole_Click server event handler is used to add
        // the user to this security role
        //
        //*******************************************************

        protected void AddRole_Click(Object sender, EventArgs e)
        {
            //get user id from dropdownlist of existing users
            int roleId = Int32.Parse(allRoles.SelectedItem.Value);

            // Add a new userRole to the database
            var roles = new SqlRolesDb();
            roles.AddUserRole(roleId, _userId);

            // Rebind list
            BindData();
        }

        //*******************************************************
        //
        // The UpdateUser_Click server event handler is used to add
        // the update the user settings
        //
        //*******************************************************

        protected void UpdateUser_Click(Object sender, EventArgs e)
        {
            var portalSecurity = ComponentManager.Resolve<IPortalSecurity>();

            // update the user record in the database
            var users = new SqlUsersDb();
            users.UpdateUser(_userId, Email.Text, portalSecurity.Encrypt(Password.Text));

            // redirect to this page with the corrected querystring args
            Response.Redirect("~/Admin/ManageUsers.aspx?userId=" + _userId + "&username=" + Email.Text + "&tabindex=" +
                              _tabIndex + "&tabid=" + _tabId);
        }

        //*******************************************************
        //
        // The UserRoles_ItemCommand server event handler on this page
        // is used to handle deleting the user from roles
        // from the userRoles asp:datalist control
        //
        //*******************************************************

        private void UserRoles_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            var roles = new SqlRolesDb();
            var roleId = (int) userRoles.DataKeys[e.Item.ItemIndex];

            // update database
            roles.DeleteUserRole(roleId, _userId);

            // Ensure that item is not editable
            userRoles.EditItemIndex = -1;

            // Repopulate list
            BindData();
        }

        //*******************************************************
        //
        // The BindData helper method is used to bind the list of
        // security roles for this portal to an asp:datalist server control
        //
        //*******************************************************

        private void BindData()
        {
            // Bind the Email and Password
            var users = new SqlUsersDb();
            IDataReader dr = users.GetSingleUser(_userName);

            // Read first row from database
            dr.Read();

            Email.Text = (String) dr["Email"];

            dr.Close();

            // add the user name to the title
            if (_userName != "")
            {
                title.InnerText = "Manage User: " + _userName;
            }

            // bind users in role to DataList
            userRoles.DataSource = users.GetRolesByUser(_userName);
            userRoles.DataBind();

            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // Get the portal's roles from the database
            var roles = new SqlRolesDb();

            // bind all portal roles to dropdownlist
            allRoles.DataSource = roles.GetPortalRoles(portalSettings.PortalId);
            allRoles.DataBind();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            userRoles.ItemCommand += UserRoles_ItemCommand;
        }
    }
}