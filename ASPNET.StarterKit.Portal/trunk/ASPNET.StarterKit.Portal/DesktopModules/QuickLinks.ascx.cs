using System;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class QuickLinks : PortalModuleControl
    {
        protected string LinkImage = "";

        private ILinksDb _linksDb;
        private IPortalSecurity _portalSecurity;

        [InjectionMethod]
        public void Initialize(IPortalSecurity portalSecurity, ILinksDb linksDb)
        {
            _portalSecurity = portalSecurity;
            _linksDb = linksDb;
        }

        /// <summary>
        /// The Page_Load event handler on this User Control is used to
        /// obtain a DataReader of link information from the Links
        /// table, and then databind the results to a templated DataList
        /// server control.  It uses the ASPNET.StarterKit.Portal.LinkDB()
        /// data component to encapsulate all data functionality.
        /// </summary>
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
            // and bind to the list control

            myDataList.DataSource = _linksDb.GetLinks(ModuleId);
            myDataList.DataBind();

            // Ensure that only users in role may add links
            if (_portalSecurity.IsInRoles(ModuleConfiguration.AuthorizedEditRoles))
            {
                EditButton.Text = "Add Link";
                EditButton.NavigateUrl = "~/DesktopModules/EditLinks.aspx?mid=" + ModuleId;
            }
        }

        protected string ChooseURL(string itemID, string modID, string URL)
        {
            if (IsEditable)
                return "~/DesktopModules/EditLinks.aspx?ItemID=" + itemID + "&mid=" + modID;
            else
                return URL;
        }
    }
}