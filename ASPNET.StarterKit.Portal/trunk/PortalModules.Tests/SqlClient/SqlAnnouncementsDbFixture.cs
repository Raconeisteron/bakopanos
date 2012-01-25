using System;
using System.Collections.Generic;
using System.Configuration;
using System.Transactions;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    [TestFixture, Explicit]
    public class SqlAnnouncementsDbFixture
    {
        private readonly TransactionScope _scope = new TransactionScope();
        private IAnnouncementsDb _db;

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

        [Test]
        public void CanGetAnnouncements()
        {
            
        }

        [Test]
        public void CanGetSingleAnnouncement()
        {
            
        }

        [Test]
        public void CanDeleteAnnouncement()
        {
            _db.DeleteAnnouncement(1);
        }

        [Test]
        public void CanAddAnnouncement()
        {
            _db.AddAnnouncement(0, "un", "t", DateTime.Now, "d", "ml", "mml");
        }

        [Test]
        public void CanUpdateAnnouncement()
        {
            _db.UpdateAnnouncement(0, "un", "t", DateTime.Now, "d", "ml", "mml");
            
        }
    }
}