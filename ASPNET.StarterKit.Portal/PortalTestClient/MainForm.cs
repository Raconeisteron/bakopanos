using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Portal.Modules.Service;

namespace Portal
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;  
        }

        private void buttonCreateAnnouncement_Click(object sender, EventArgs e)
        {
            var client = new AnnouncementServiceClient();

            client.CreateOrUpdate(new PortalAnnouncement
                                      {
                                          ModuleId = 8,
                                          CreatedByUser = "costas",
                                          Title = "title",
                                          Description = "Description",
                                          ExpireDate = Convert.ToDateTime("1.1.2020"),
                                          MoreLink = "link",
                                          MobileMoreLink = "link"
                                      });
        }

        private void buttonCreateLink_Click(object sender, EventArgs e)
        {
            var client = new LinkServiceClient();

            client.CreateOrUpdate(new PortalLink
                                      {
                                          ModuleId = 1,
                                          CreatedByUser = "costas",
                                          CreatedDate = DateTime.Now,
                                          Description = "desc",
                                          Title = "some title",
                                          ViewOrder = 1,
                                          Url = "www.yahoo.com",
                                          MobileUrl = "www.yahoo.com"
                                      });
        }

        private void buttonCreateContact_Click(object sender, EventArgs e)
        {
            var client = new ContactServiceClient();

            client.CreateOrUpdate(new PortalContact
                                      {
                                          ModuleId = 9,
                                          CreatedByUser = "costas",
                                          CreatedDate = DateTime.Now,
                                          Name = "User1",
                                          Role = "Admin",
                                          Contact1 = "c1",
                                          Contact2 = "c2",
                                          Email = "a@a.com"
                                      });
        }

        private void buttonAnnouncements_Click(object sender, EventArgs e)
        {
            var client = new AnnouncementServiceClient();
            dataGridView1.DataSource = client.GetAnnouncements(8);
        }

        private void buttonLinks_Click(object sender, EventArgs e)
        {
            var client = new LinkServiceClient();
            dataGridView1.DataSource = client.GetLinks(1);
        }

    }

}
