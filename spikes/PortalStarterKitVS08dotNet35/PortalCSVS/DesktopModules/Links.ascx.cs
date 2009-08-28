using System;

namespace ASPNET.StarterKit.Portal
{
    public partial class Links : PortalModuleControl<Links>
    {
        protected String linkImage = "";

        public Links()
        {
            Init += Page_Init;
        }

        //*******************************************************
        //
        // The Page_Load event handler on this User Control is used to
        // obtain a DataReader of link information from the Links
        // table, and then databind the results to a templated DataList
        // server control.  It uses the ASPNET.StarterKit.Portal.LinkDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************

        protected void Page_Load(object sender, EventArgs e)
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
            ILinkDB links = Global.Container.Resolve<ILinkDB>();

            myDataList.DataSource = links.GetLinks(ModuleId);
            myDataList.DataBind();
        }

        protected string ChooseURL(string itemID, string modID, string URL)
        {
            if (IsEditable)
                return "~/DesktopModules/EditLinks.aspx?ItemID=" + itemID + "&mid=" + modID;
            else
                return URL;
        }

        protected string ChooseTarget()
        {
            if (IsEditable)
                return "_self";
            else
                return "_new";
        }

        protected string ChooseTip(string desc)
        {
            if (IsEditable)
                return "Edit";
            else
                return desc;
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