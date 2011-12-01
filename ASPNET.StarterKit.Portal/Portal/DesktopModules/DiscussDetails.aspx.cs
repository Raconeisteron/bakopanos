using System;
using System.Data;
using System.Web.UI;
using Portal.Components;
using Portal.Modules.Data;

namespace Portal.DesktopModules
{
    public partial class DiscussDetails : Page
    {
        private int itemId;
        private int moduleId;


        //*******************************************************
        //
        // The Page_Load server event handler on this page is used
        // to obtain the ModuleId and ItemId of the discussion list,
        // and to then display the message contents.
        //
        //*******************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain moduleId and ItemId from QueryString
            moduleId = Int32.Parse(Request.Params["Mid"]);

            if (Request.Params["ItemId"] != null)
            {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }
            else
            {
                itemId = 0;
                EditPanel.Visible = true;
                ButtonPanel.Visible = false;
            }

            // Populate message contents if this is the first visit to the page
            if (Page.IsPostBack == false && itemId != 0)
            {
                BindData();
            }

            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                if (itemId == 0)
                {
                    Response.Redirect("~/Admin/EditAccessDenied.aspx");
                }
                else
                {
                    ReplyBtn.Visible = false;
                }
            }
        }

        //*******************************************************
        //
        // The ReplyBtn_Click server event handler on this page is used
        // to handle the scenario where a user clicks the message's
        // "Reply" button to perform a post.
        //
        //*******************************************************

        protected void ReplyBtn_Click(Object Sender, EventArgs e)
        {
            EditPanel.Visible = true;
            ButtonPanel.Visible = false;
        }

        //*******************************************************
        //
        // The UpdateBtn_Click server event handler on this page is used
        // to handle the scenario where a user clicks the "update"
        // button after entering a response to a message post.
        //
        //*******************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Create new discussion database component
            IDiscussionsDb discuss = ModulesDataAccess.DiscussionDb;

            // Add new message (updating the "itemId" on the page)
            itemId = discuss.AddMessage(moduleId, itemId, User.Identity.Name, Server.HtmlEncode(TitleField.Text),
                                        Server.HtmlEncode(BodyField.Text));

            // Update visibility of page elements
            EditPanel.Visible = false;
            ButtonPanel.Visible = true;

            // Repopulate page contents with new message
            BindData();
        }

        //*******************************************************
        //
        // The CancelBtn_Click server event handler on this page is used
        // to handle the scenario where a user clicks the "cancel"
        // button to discard a message post and toggle out of
        // edit mode.
        //
        //*******************************************************

        protected void CancelBtn_Click(Object sender, EventArgs e)
        {
            // Update visibility of page elements
            EditPanel.Visible = false;
            ButtonPanel.Visible = true;
        }

        //*******************************************************
        //
        // The BindData method is used to obtain details of a message
        // from the Discussion table, and update the page with
        // the message content.
        //
        //*******************************************************

        private void BindData()
        {
            // Obtain the selected item from the Discussion table
            IDiscussionsDb discuss = ModulesDataAccess.DiscussionDb;
            IDataReader dr = discuss.GetSingleMessage(itemId);

            // Load first row from database
            dr.Read();

            // Security check.  verify that itemid is within the module.
            int dbModuleID = Convert.ToInt32(dr["ModuleID"]);
            if (dbModuleID != moduleId)
            {
                dr.Close();
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Update labels with message contents
            Title.Text = (string) dr["Title"];
            Body.Text = (string) dr["Body"];
            CreatedByUser.Text = (string) dr["CreatedByUser"];
            CreatedDate.Text = string.Format("{0:d}", dr["CreatedDate"]);
            TitleField.Text = ReTitle(Title.Text);

            int prevId = 0;
            int nextId = 0;

            // Update next and preview links
            object id1 = dr["PrevMessageID"];

            if (id1 != DBNull.Value)
            {
                prevId = (int) id1;
                prevItem.HRef = Request.Path + "?ItemId=" + prevId + "&mid=" + moduleId;
            }

            object id2 = dr["NextMessageID"];

            if (id2 != DBNull.Value)
            {
                nextId = (int) id2;
                nextItem.HRef = Request.Path + "?ItemId=" + nextId + "&mid=" + moduleId;
            }

            // close the datareader
            dr.Close();

            // Show/Hide Next/Prev Button depending on whether there is a next/prev message
            if (prevId <= 0)
            {
                prevItem.Visible = false;
            }

            if (nextId <= 0)
            {
                nextItem.Visible = false;
            }
        }

        //*******************************************************
        //
        // The ReTitle helper method is used to create the subject
        // line of a response post to a message.
        //
        //*******************************************************

        private string ReTitle(string title)
        {
            if (title.Length > 0 & title.IndexOf("Re: ", 0) == -1)
            {
                title = "Re: " + title;
            }

            return title;
        }
    }
}