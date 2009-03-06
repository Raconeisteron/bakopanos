using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPNET.StarterKit.Portal.Components;

namespace ASPNET.StarterKit.Portal
{
    public partial class ModuleDefinitions : Page
    {
        private int defId = -1;
        private int tabId;
        private int tabIndex;

        public ModuleDefinitions()
        {
            Page.Init += Page_Init;
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
            if (PortalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Calculate security defId
            if (Request.Params["defid"] != null)
            {
                defId = Int32.Parse(Request.Params["defid"]);
            }
            if (Request.Params["tabid"] != null)
            {
                tabId = Int32.Parse(Request.Params["tabid"]);
            }
            if (Request.Params["tabindex"] != null)
            {
                tabIndex = Int32.Parse(Request.Params["tabindex"]);
            }


            // If this is the first visit to the page, bind the definition data 
            if (Page.IsPostBack == false)
            {
                if (defId == -1)
                {
                    // new module definition
                    FriendlyName.Text = "New Definition";
                    DesktopSrc.Text = "DesktopModules/SomeModule.ascx";
                    MobileSrc.Text = "MobileModules/SomeModule.ascx";
                }
                else
                {
                    // Obtain the module definition to edit from the database
                    var config = new Configuration();
                    SiteConfiguration.ModuleDefinitionRow modDefRow = config.GetSingleModuleDefinition(defId);

                    // Read in information
                    FriendlyName.Text = modDefRow.FriendlyName;
                    DesktopSrc.Text = modDefRow.DesktopSourceFile;
                    MobileSrc.Text = modDefRow.MobileSourceFile;
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
                var config = new Configuration();

                if (defId == -1)
                {
                    // Obtain PortalSettings from Current Context
                    var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

                    // Add a new module definition to the database
                    config.AddModuleDefinition(portalSettings.PortalId, FriendlyName.Text, DesktopSrc.Text,
                                               MobileSrc.Text);
                }
                else
                {
                    // update the module definition
                    config.UpdateModuleDefinition(defId, FriendlyName.Text, DesktopSrc.Text, MobileSrc.Text);
                }

                // Redirect back to the portal admin page
                Response.Redirect("~/DesktopDefault.aspx?tabindex=" + tabIndex + "&tabid=" + tabId);
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
            var config = new Configuration();
            config.DeleteModuleDefinition(defId);

            // Redirect back to the portal admin page
            Response.Redirect("~/DesktopDefault.aspx?tabindex=" + tabIndex + "&tabid=" + tabId);
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
            Response.Redirect("~/DesktopDefault.aspx?tabindex=" + tabIndex + "&tabid=" + tabId);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}