using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Transactions;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    [TestFixture, Explicit]
    public class SqlAnnouncementsDbFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _db = new SqlAnnouncementsDb(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }

        #endregion

        private readonly TransactionScope _scope = new TransactionScope();
        private IAnnouncementsDb _db;

        [Test]
        public void CanAddAnnouncement()
        {
            int id = _db.AddAnnouncement(0, "un", "t", DateTime.Now, "d", "ml", "mml");
            Assert.AreNotEqual(id, -1);
        }

        [Test]
        public void CanDeleteAnnouncement()
        {
            int id = _db.AddAnnouncement(0, "un", "t", DateTime.Now, "d", "ml", "mml");
            _db.DeleteAnnouncement(id);
        }

        [Test]
        public void CanUpdateAnnouncement()
        {
            _db.UpdateAnnouncement(0, "un", "t", DateTime.Now, "d", "ml", "mml");
        }

        [Test]
        public void CanGetAnnouncements()
        {
            //string time = DateTime.Now.ToLongDateString();
            //int id1 = _db.AddAnnouncement(9001, "un", "t", DateTime.Parse(time), "d", "ml", "mml");
            //int id2 = _db.AddAnnouncement(9001, "un", "t", DateTime.Parse(time), "d", "ml", "mml");

            //Collection<PortalAnnouncement> announcements = _db.GetAnnouncements(9001);
            //Assert.AreEqual(announcements[0].ItemId, id1);
            //Assert.AreEqual(announcements[1].ItemId, id2);
        }

        [Test]
        public void CanGetAnnouncementsCanReturnEmptyList()
        {
            Collection<PortalAnnouncement> announcements = _db.GetAnnouncements(9999);
            Assert.IsTrue(announcements.Count == 0);
        }

        [Test]
        public void CanGetSingleAnnouncement()
        {
            string time = DateTime.Now.ToLongDateString();
            int id = _db.AddAnnouncement(0, "un", "t", DateTime.Parse(time), "d", "ml", "mml");
            PortalAnnouncement announcement = _db.GetSingleAnnouncement(id);
            Assert.AreEqual(announcement.ModuleId, 0);
            Assert.AreEqual(announcement.ItemId, id);
            Assert.AreEqual(announcement.Title, "t");
            Assert.AreEqual(announcement.CreatedByUser, "un");
            Assert.AreEqual(announcement.Description, "d");
            Assert.AreEqual(announcement.MoreLink, "ml");
            Assert.AreEqual(announcement.MobileMoreLink, "mml");
            Assert.AreEqual(announcement.ExpireDate, DateTime.Parse(time));
        }

        [Test]
        public void CanGetSingleAnnouncementCanReturnNull()
        {
            PortalAnnouncement announcement = _db.GetSingleAnnouncement(9999);
            Assert.IsNull(announcement);
        }
    }
}