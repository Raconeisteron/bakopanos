using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ASPNET.StarterKit.Portal
{
    public class DiscussDetails : Page
    {
        protected Label Body;
        protected TextBox BodyField;
        protected Panel ButtonPanel;
        protected LinkButton cancelButton;
        protected Label CreatedByUser;
        protected Label CreatedDate;
        protected Panel EditPanel;
        private int itemId;
        private int moduleId;
        protected HtmlAnchor nextItem;
        protected HtmlAnchor prevItem;
        protected LinkButton ReplyBtn;
        protected Label Title;
        protected TextBox TitleField;
        protected LinkButton updateButton;

        public DiscussDetails()
        {
            Page.Init += Page_Init;
        }

        //*******************************************************
        //
        // The Page_Load server event handler on this page is used
        // to obtain the ModuleId and ItemId of the discussion list,
        // and to then display the message contents.
        //
        //*******************************************************

        private void Page_Load(object sender, EventArgs e)
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

        private void ReplyBtn_Click(Object Sender, EventArgs e)
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

        private void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Create new discussion database component
            var discuss = new DiscussionDB();

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

        private void CancelBtn_Click(Object sender, EventArgs e)
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
            var discuss = new DiscussionDB();
            SqlDataReader dr = discuss.GetSingleMessage(itemId);

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
            Title.Text = (String) dr["Title"];
            Body.Text = (String) dr["Body"];
            CreatedByUser.Text = (String) dr["CreatedByUser"];
            CreatedDate.Text = String.Format("{0:d}", dr["CreatedDate"]);
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

        private String ReTitle(String title)
        {
            if (title.Length > 0 & title.IndexOf("Re: ", 0) == -1)
            {
                title = "Re: " + title;
            }

            return title;
        }

        private void Page_Init(object sender, EventArgs e)
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
            this.ReplyBtn.Click += new System.EventHandler(this.ReplyBtn_Click);
            this.updateButton.Click += new System.EventHandler(this.UpdateBtn_Click);
            this.cancelButton.Click += new System.EventHandler(this.CancelBtn_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}