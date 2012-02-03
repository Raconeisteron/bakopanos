using System.Collections.ObjectModel;
using System.Configuration;
using System.Transactions;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    [TestFixture, Explicit]
    public class SqlLinksDbFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _db = new SqlLinksDb(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }

        #endregion

        private readonly TransactionScope _scope = new TransactionScope();
        private ILinksDb _db;

        [Test]
        public void CanAddLink()
        {
            int id = _db.AddLink(0, "un", "t", "u", "mu", 0, "d");
            Assert.AreNotEqual(id, -1);
        }

        [Test]
        public void CanDeleteLink()
        {
            int id = _db.AddLink(0, "un", "t", "u", "mu", 0, "d");
            _db.DeleteLink(id);
        }

        [Test]
        public void CanGetLinks()
        {
        }

        [Test]
        public void CanGetLinksCanReturnEmptyList()
        {
            Collection<PortalLink> links = _db.GetLinks(9999);
            Assert.IsTrue(links.Count == 0);
        }

        [Test]
        public void CanGetSingleLink()
        {
            int id = _db.AddLink(0, "un", "t", "u", "mu", 0, "d");
            PortalLink link = _db.GetSingleLink(id);
            Assert.AreEqual(link.ItemId, id);
            Assert.AreEqual(link.ModuleId, 0);
            Assert.AreEqual(link.Title, "t");
            Assert.AreEqual(link.Url, "u");
            Assert.AreEqual(link.MobileUrl, "mu");
            Assert.AreEqual(link.ViewOrder, 0);
            Assert.AreEqual(link.Description, "d");
        }

        [Test]
        public void CanGetSingleLinkCanReturnNull()
        {
            PortalLink link = _db.GetSingleLink(9999);
            Assert.IsNull(link);
        }

        [Test]
        public void CanUpdateLink()
        {
            _db.UpdateLink(0, "un", "t", "u", "mu", 0, "d");
        }
    }
}