using System;
using ASPNETPortal;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class Links : PortalModuleControl
    {
        protected String LinkImage = "";

        [Dependency]
        public ILinkDb Model { private get; set; }

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
                LinkImage = "~/images/edit.gif";
            }
            else
            {
                LinkImage = "~/images/navlink.gif";
            }

            // Obtain links information from the Links table
            // and bind to the datalist control
            myDataList.DataSource = Model.GetLinks(ModuleId);
            myDataList.DataBind();
        }

        protected string ChooseUrl(string itemId, string modId, string url)
        {
            if (IsEditable)
                return "~/DesktopModules/EditLinks.aspx?ItemID=" + itemId + "&mid=" + modId;
            return url;
        }

        protected string ChooseTarget()
        {
            if (IsEditable)
                return "_self";
            return "_new";
        }

        protected string ChooseTip(string desc)
        {
            if (IsEditable)
                return "Edit";
            return desc;
        }
    }
}