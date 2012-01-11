using System;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public abstract class PortalEditPage<T> : Page
        where T:PortalItem
    {
        protected int ItemId { get; set; }
        protected int ModuleId { get; set; }

        [Dependency]
        public IRepository<T> Repo { get; set; }

        protected abstract void Set(T item);
        protected abstract T Get();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Determine ModuleId of Announcements Portal Module
            ModuleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(ModuleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Determine ItemId of Announcement to Update
            if (Request.Params["ItemId"] != null)
            {
                ItemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // announcement itemId value is specified, and if so populate page
            // contents with the announcement details

            if (Page.IsPostBack) return;

            if (ItemId != 0)
            {
                // Obtain a single row of announcement information
                T item = Repo.GetSingle(ItemId);

                // Security check.  verify that itemid is within the module.
                if (item.ModuleId != ModuleId)
                {
                    Response.Redirect("~/Admin/EditAccessDenied.aspx");
                }

                Set(item);
            }

            // Store URL Referrer to return to portal
            ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
        }

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Only Update if the Entered Data is Valid
            if (Page.IsValid)
            {

                Repo.Save(Get());

                // Redirect back to the portal home page
                Response.Redirect((String)ViewState["UrlReferrer"]);
            }
        }

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            Repo.Delete(ItemId);

            // Redirect back to the portal home page
            Response.Redirect((String)ViewState["UrlReferrer"]);
        }

        protected void CancelBtn_Click(Object sender, EventArgs e)
        {
            // Redirect back to the portal home page
            Response.Redirect((String)ViewState["UrlReferrer"]);
        }
    }
}