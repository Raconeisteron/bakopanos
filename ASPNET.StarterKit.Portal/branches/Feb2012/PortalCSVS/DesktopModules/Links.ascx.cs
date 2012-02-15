using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ASPNET.StarterKit.Portal {

	public abstract class Links : ASPNET.StarterKit.Portal.PortalModuleControl 
	{
		protected System.Web.UI.WebControls.DataList myDataList;


		protected String linkImage = "";

		//*******************************************************
		//
		// The Page_Load event handler on this User Control is used to
		// obtain a DataReader of link information from the Links
		// table, and then databind the results to a templated DataList
		// server control.  It uses the ASPNET.StarterKit.Portal.LinkDB()
		// data component to encapsulate all data functionality.
		//
		//*******************************************************

		private void Page_Load(object sender, System.EventArgs e) 
		{

			// Set the link image type
			if (IsEditable) 
			{
				linkImage = "~/images/edit.gif";
			}
			else 
			{
				linkImage = "~/images/navlink.gif";
			}

			// Obtain links information from the Links table
			// and bind to the datalist control
			ASPNET.StarterKit.Portal.LinkDB links = new ASPNET.StarterKit.Portal.LinkDB();

			myDataList.DataSource = links.GetLinks(ModuleId);
			myDataList.DataBind();
		}
        
		protected string ChooseURL(string itemID, string modID, string URL)
		{
			if(IsEditable)
				return "~/DesktopModules/EditLinks.aspx?ItemID=" + itemID.ToString() + "&mid=" + modID;
			else
				return URL;
		}
		
		protected string ChooseTarget()
		{
			if(IsEditable)
				return "_self";
			else
				return "_new";
		}
	
		protected string ChooseTip(string desc)
		{
			if(IsEditable)
				return "Edit";
			else
				return desc;
		}

        public Links() {
            this.Init += new System.EventHandler(Page_Init);
        }

        private void Page_Init(object sender, EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

		#region Web Form Designer generated code
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion
    }
}
