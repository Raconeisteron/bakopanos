using System;

namespace ASPNET.StarterKit.Portal
{
    public partial class Links : PortalModuleControl
    {
        protected String LinkImage = "";

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
            var links = new LinkDb();

            myDataList.DataSource = LinkDb.GetLinks(ModuleId);
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
    }
}