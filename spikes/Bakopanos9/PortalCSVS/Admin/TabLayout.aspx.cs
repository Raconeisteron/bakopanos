using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class TabLayout : PortalPage<TabLayout>
    {
        protected List<ModuleItem> contentList;
        protected List<ModuleItem> leftList;
        protected List<ModuleItem> rightList;
        private int tabId;

        [Dependency]
        public IRolesDB RolesDB { private get; set; }

        [Dependency]
        public IModulesConfig ModulesConfig { private get; set; }

        [Dependency]
        public ITabsConfig TabsConfig { private get; set; }

        [Dependency]
        public IModuleDefConfig ModuleDefConfig { private get; set; }

        [Dependency]
        public IGlobalConfig GlobalConfig { private get; set; }


        //*******************************************************
        //
        // The Page_Load server event handler on this page is used
        // to populate a tab's layout settings on the page
        //
        //*******************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verify that the current user has access to access this page
            if (PortalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Determine Tab to Edit
            if (Request.Params["tabid"] != null)
            {
                tabId = Int32.Parse(Request.Params["tabid"]);
            }

            // If first visit to the page, update all entries
            if (Page.IsPostBack == false)
            {
                BindData();
            }
        }

        //*******************************************************
        //
        // The AddModuleToPane_Click server event handler on this page is used
        // to add a new portal module into the tab
        //
        //*******************************************************

        protected void AddModuleToPane_Click(Object sender, EventArgs e)
        {
            // All new modules go to the end of the contentpane
            var m = new ModuleItem();
            m.ModuleTitle = moduleTitle.Text;
            m.ModuleDefId = Int32.Parse(moduleType.SelectedItem.Value);
            m.ModuleOrder = 999;

            // save to database
            m.ModuleId = ModulesConfig.AddModule(tabId, m.ModuleOrder, "ContentPane", m.ModuleTitle, m.ModuleDefId, 0, "Admins", false);

            // Obtain portalId from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // reload the portalSettings from the database
            HttpContext.Current.Items["PortalSettings"] = new PortalSettings(portalSettings.PortalId, tabId, GlobalConfig);

            // reorder the modules in the content pane
            List<ModuleItem> modules = GetModules("ContentPane");
            OrderModules(modules);

            // resave the order
            foreach (ModuleItem item in modules)
            {
                ModulesConfig.UpdateModuleOrder(item.ModuleId, item.ModuleOrder, "ContentPane");
            }

            // Redirect to the same page to pick up changes
            Response.Redirect(Request.RawUrl);
        }

        //*******************************************************
        //
        // The UpDown_Click server event handler on this page is
        // used to move a portal module up or down on a tab's layout pane
        //
        //*******************************************************

        protected void UpDown_Click(Object sender, ImageClickEventArgs e)
        {
            String cmd = ((ImageButton) sender).CommandName;
            String pane = ((ImageButton) sender).CommandArgument;
            var _listbox = (ListBox) Page.FindControl(pane);

            List<ModuleItem> modules = GetModules(pane);

            if (_listbox.SelectedIndex != -1)
            {
                int delta;
                int selection = -1;

                // Determine the delta to apply in the order number for the module
                // within the list.  +3 moves down one item; -3 moves up one item

                if (cmd == "down")
                {
                    delta = 3;
                    if (_listbox.SelectedIndex < _listbox.Items.Count - 1)
                    {
                        selection = _listbox.SelectedIndex + 1;
                    }
                }
                else
                {
                    delta = -3;
                    if (_listbox.SelectedIndex > 0)
                    {
                        selection = _listbox.SelectedIndex - 1;
                    }
                }

                ModuleItem m;
                m = modules[_listbox.SelectedIndex];
                m.ModuleOrder += delta;

                // reorder the modules in the content pane
                OrderModules(modules);

                // resave the order
                foreach (ModuleItem item in modules)
                {
                    ModulesConfig.UpdateModuleOrder(item.ModuleId, item.ModuleOrder, pane);
                }
            }

            // Redirect to the same page to pick up changes
            Response.Redirect(Request.RawUrl);
        }

        //*******************************************************
        //
        // The RightLeft_Click server event handler on this page is
        // used to move a portal module between layout panes on
        // the tab page
        //
        //*******************************************************

        protected void RightLeft_Click(Object sender, ImageClickEventArgs e)
        {
            String sourcePane = ((ImageButton) sender).Attributes["sourcepane"];
            String targetPane = ((ImageButton) sender).Attributes["targetpane"];
            var sourceBox = (ListBox) Page.FindControl(sourcePane);
            var targetBox = (ListBox) Page.FindControl(targetPane);

            if (sourceBox.SelectedIndex != -1)
            {
                // get source arraylist
                List<ModuleItem> sourceList = GetModules(sourcePane);

                // get a reference to the module to move
                // and assign a high order number to send it to the end of the target list
                ModuleItem m = sourceList[sourceBox.SelectedIndex];

                // add it to the database
                ModulesConfig.UpdateModuleOrder(m.ModuleId, 998, targetPane);

                // delete it from the source list
                sourceList.RemoveAt(sourceBox.SelectedIndex);

                // Obtain portalId from Current Context
                var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

                // reload the portalSettings from the database
                HttpContext.Current.Items["PortalSettings"] = new PortalSettings(portalSettings.PortalId, tabId, GlobalConfig);

                // reorder the modules in the source pane
                sourceList = GetModules(sourcePane);
                OrderModules(sourceList);

                // resave the order
                foreach (ModuleItem item in sourceList)
                {
                    ModulesConfig.UpdateModuleOrder(item.ModuleId, item.ModuleOrder, sourcePane);
                }

                // reorder the modules in the target pane
                List<ModuleItem> targetList = GetModules(targetPane);
                OrderModules(targetList);

                // resave the order
                foreach (ModuleItem item in targetList)
                {
                    ModulesConfig.UpdateModuleOrder(item.ModuleId, item.ModuleOrder, targetPane);
                }

                // Redirect to the same page to pick up changes
                Response.Redirect(Request.RawUrl);
            }
        }

        //*******************************************************
        //
        // The Apply_Click server event handler on this page is
        // used to save the current tab settings to the database and 
        // then redirect back to the main admin page.
        //
        //*******************************************************

        protected void Apply_Click(Object Sender, EventArgs e)
        {
            // Save changes then navigate back to admin.  
            String id = ((LinkButton) Sender).ID;

            SaveTabData();

            // redirect back to the admin page

            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];
            int adminIndex = portalSettings.DesktopTabs.Count - 1;

            Response.Redirect("~/DesktopDefault.aspx?tabindex=" + adminIndex + "&tabid=" + (portalSettings.DesktopTabs[adminIndex]).TabId);
        }

        //*******************************************************
        //
        // The TabSettings_Change server event handler on this page is
        // invoked any time the tab name or access security settings
        // change.  The event handler in turn calls the "SaveTabData"
        // helper method to ensure that these changes are persisted
        // to the portal configuration file.
        //
        //*******************************************************

        protected void TabSettings_Change(Object sender, EventArgs e)
        {
            // Ensure that settings are saved
            SaveTabData();
        }

        //*******************************************************
        //
        // The SaveTabData helper method is used to persist the
        // current tab settings to the database.
        //
        //*******************************************************

        private void SaveTabData()
        {
            // Construct Authorized User Roles String
            String authorizedRoles = "";

            foreach (ListItem item in authRoles.Items)
            {
                if (item.Selected)
                {
                    authorizedRoles = authorizedRoles + item.Text + ";";
                }
            }

            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // update Tab info in the database
            TabsConfig.UpdateTab(portalSettings.PortalId, tabId, tabName.Text, portalSettings.ActiveTab.TabOrder, authorizedRoles, mobileTabName.Text, showMobile.Checked);
        }

        //*******************************************************
        //
        // The EditBtn_Click server event handler on this page is
        // used to edit an individual portal module's settings
        //
        //*******************************************************

        protected void EditBtn_Click(Object sender, ImageClickEventArgs e)
        {
            String pane = ((ImageButton) sender).CommandArgument;
            var _listbox = (ListBox) Page.FindControl(pane);

            if (_listbox.SelectedIndex != -1)
            {
                int mid = Int32.Parse(_listbox.SelectedItem.Value);

                // Redirect to module settings page
                Response.Redirect("ModuleSettings.aspx?mid=" + mid + "&tabid=" + tabId);
            }
        }

        //*******************************************************
        //
        // The DeleteBtn_Click server event handler on this page is
        // used to delete an portal module from the page
        //
        //*******************************************************

        protected void DeleteBtn_Click(Object sender, ImageClickEventArgs e)
        {
            String pane = ((ImageButton) sender).CommandArgument;
            var _listbox = (ListBox) Page.FindControl(pane);
            List<ModuleItem> modules = GetModules(pane);

            if (_listbox.SelectedIndex != -1)
            {
                ModuleItem m = modules[_listbox.SelectedIndex];
                if (m.ModuleId > -1)
                {
                    // must delete from database too
                    ModulesConfig.DeleteModule(m.ModuleId);
                }
            }

            // Redirect to the same page to pick up changes
            Response.Redirect(Request.RawUrl);
        }


        //*******************************************************
        //
        // The BindData helper method is used to update the tab's
        // layout panes with the current configuration information
        //
        //*******************************************************

        private void BindData()
        {
            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];
            TabSettings tab = portalSettings.ActiveTab;

            // Populate Tab Names, etc.
            tabName.Text = tab.TabName;
            mobileTabName.Text = tab.MobileTabName;
            showMobile.Checked = tab.ShowMobile;

            // Populate checkbox list with all security roles for this portal
            // and "check" the ones already configured for this tab
            IDataReader roles = RolesDB.GetPortalRoles(portalSettings.PortalId);

            // Clear existing items in checkboxlist
            authRoles.Items.Clear();

            var allItem = new ListItem();
            allItem.Text = "All Users";

            if (tab.AuthorizedRoles.LastIndexOf("All Users") > -1)
            {
                allItem.Selected = true;
            }

            authRoles.Items.Add(allItem);

            while (roles.Read())
            {
                var item = new ListItem();
                item.Text = (String) roles["RoleName"];
                item.Value = roles["RoleID"].ToString();

                if ((tab.AuthorizedRoles.LastIndexOf(item.Text)) > -1)
                {
                    item.Selected = true;
                }

                authRoles.Items.Add(item);
            }

            // Populate the "Add Module" Data
            moduleType.DataSource = ModuleDefConfig.GetModuleDefinitions(portalSettings.PortalId);
            moduleType.DataBind();

            // Populate Right Hand Module Data
            rightList = GetModules("RightPane");
            rightPane.DataBind();

            // Populate Content Pane Module Data
            contentList = GetModules("ContentPane");
            contentPane.DataBind();

            // Populate Left Hand Pane Module Data
            leftList = GetModules("LeftPane");
            leftPane.DataBind();
        }

        //*******************************************************
        //
        // The GetModules helper method is used to get the modules
        // for a single pane within the tab
        //
        //*******************************************************

        private List<ModuleItem> GetModules(String pane)
        {
            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];
            var paneModules = new List<ModuleItem>();

            foreach (ModuleSettings module in portalSettings.ActiveTab.Modules)
            {
                if ((module.PaneName).ToLower() == pane.ToLower())
                {
                    var m = new ModuleItem();
                    m.ModuleTitle = module.ModuleTitle;
                    m.ModuleId = module.ModuleId;
                    m.ModuleDefId = module.ModuleDefId;
                    m.ModuleOrder = module.ModuleOrder;
                    paneModules.Add(m);
                }
            }

            return paneModules;
        }

        //*******************************************************
        //
        // The OrderModules helper method is used to reset the display
        // order for modules within a pane
        //
        //*******************************************************

        private static void OrderModules(List<ModuleItem> list)
        {
            int i = 1;

            // sort the arraylist
            list.Sort();

            // renumber the order
            foreach (ModuleItem m in list)
            {
                // number the items 1, 3, 5, etc. to provide an empty order
                // number when moving items up and down in the list.
                m.ModuleOrder = i;
                i += 2;
            }
        }
    }
}