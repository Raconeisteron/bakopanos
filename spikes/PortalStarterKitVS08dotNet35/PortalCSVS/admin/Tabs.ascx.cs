using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNET.StarterKit.Portal
{
    public partial class Tabs : PortalModuleControl
    {
        protected ArrayList portalTabs;
        private int tabId;
        private int tabIndex;

        public Tabs()
        {
            Init += Page_Init;
        }

        //*******************************************************
        //
        // The Page_Load server event handler on this user control is used
        // to populate the current tab settings from the database
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
                tabId = Int32.Parse(Request.Params["tabid"]);
            }
            if (Request.Params["tabindex"] != null)
            {
                tabIndex = Int32.Parse(Request.Params["tabindex"]);
            }

            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            portalTabs = new ArrayList();
            foreach (TabStripDetails tab in portalSettings.DesktopTabs)
            {
                var t = new TabItem();
                t.TabName = tab.TabName;
                t.TabId = tab.TabId;
                t.TabOrder = tab.TabOrder;
                portalTabs.Add(t);
            }

            // Give the admin tab a big sort order number, to ensure it's
            // always at the end
            var adminTab = (TabItem) portalTabs[portalTabs.Count - 1];
            adminTab.TabOrder = 99999;

            // If this is the first visit to the page, bind the tab data to the page listbox
            if (Page.IsPostBack == false)
            {
                tabList.DataBind();
            }
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

            if (tabList.SelectedIndex != -1)
            {
                int delta;

                // Determine the delta to apply in the order number for the module
                // within the list.  +3 moves down one item; -3 moves up one item

                if (cmd == "down")
                {
                    delta = 3;
                }
                else
                {
                    delta = -3;
                }

                TabItem t;
                t = (TabItem) portalTabs[tabList.SelectedIndex];
                t.TabOrder += delta;

                // Reset the order numbers for the tabs within the portal  
                OrderTabs();

                // Redirect to this site to refresh
                Response.Redirect("~/DesktopDefault.aspx?tabindex=" + (portalTabs.Count - 1) + "&tabid=" +
                                  tabId);
            }
        }


        //*******************************************************
        //
        // The DeleteBtn_Click server event handler is used to delete
        // the selected tab from the portal
        //
        //*******************************************************

        protected void DeleteBtn_Click(Object sender, ImageClickEventArgs e)
        {
            if (tabList.SelectedIndex != -1)
            {
                // must delete from database too
                var t = (TabItem) portalTabs[tabList.SelectedIndex];
                var config = new Configuration();
                config.DeleteTab(t.TabId);

                // remove item from list
                portalTabs.RemoveAt(tabList.SelectedIndex);

                // reorder list
                OrderTabs();

                // Redirect to this site to refresh
                Response.Redirect("~/DesktopDefault.aspx?tabindex=" + tabIndex + "&tabid=" + tabId);
            }
        }


        //*******************************************************
        //
        // The AddTab_Click server event handler is used to add
        // a new security tab for this portal
        //
        //*******************************************************

        protected void AddTab_Click(Object Sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // New tabs go to the end of the list
            var t = new TabItem();
            t.TabName = "New Tab";
            t.TabId = -1;
            t.TabOrder = 999;
            portalTabs.Add(t);

            // write tab to database
            var config = new Configuration();
            t.TabId = config.AddTab(portalSettings.PortalId, t.TabName, t.TabOrder);

            // reload the _portalSettings from the database
            HttpContext.Current.Items["PortalSettings"] = new PortalSettings(portalSettings.PortalId, t.TabId);

            // Reset the order numbers for the tabs within the list  
            OrderTabs();

            // Redirect to edit page
            Response.Redirect("~/Admin/TabLayout.aspx?tabid=" + t.TabId);
        }

        //*******************************************************
        //
        // The EditBtn_Click server event handler is used to edit
        // the selected tab within the portal
        //
        //*******************************************************

        protected void EditBtn_Click(Object sender, ImageClickEventArgs e)
        {
            // Redirect to edit page of currently selected tab
            if (tabList.SelectedIndex != -1)
            {
                // Redirect to module settings page
                var t = (TabItem) portalTabs[tabList.SelectedIndex];

                Response.Redirect("~/Admin/TabLayout.aspx?tabid=" + t.TabId);
            }
        }

        //*******************************************************
        //
        // The OrderTabs helper method is used to reset the display
        // order for tabs within the portal
        //
        //*******************************************************

        private void OrderTabs()
        {
            int i = 1;

            // sort the arraylist
            portalTabs.Sort();

            // renumber the order and save to database
            foreach (TabItem t in portalTabs)
            {
                // number the items 1, 3, 5, etc. to provide an empty order
                // number when moving items up and down in the list.
                t.TabOrder = i;
                i += 2;

                // rewrite tab to database
                var config = new Configuration();
                config.UpdateTabOrder(t.TabId, t.TabOrder);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

        #region Web Form Designer generated code

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}