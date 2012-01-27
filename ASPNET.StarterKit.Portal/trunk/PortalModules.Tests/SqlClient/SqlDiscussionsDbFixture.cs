using System.Collections.ObjectModel;
using System.Configuration;
using System.Transactions;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    [TestFixture, Explicit]
    public class SqlDiscussionsDbFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _db = new SqlDiscussionsDb(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }

        #endregion

        private readonly TransactionScope _scope = new TransactionScope();
        private IDiscussionsDb _db;

        [Test]
        public void CanAddMessage()
        {
            int id = _db.AddMessage(0, 0, "un", "t", "b");
            Assert.AreNotEqual(id, -1);
        }

        [Test]
        public void CanGetSingleMessage()
        {
            int id = _db.AddMessage(0, 0, "un", "t", "b");
            PortalDiscussion message = _db.GetSingleMessage(id);

            Assert.AreEqual(message.ItemId, id);
            Assert.AreEqual(message.ModuleId, 0);
            Assert.AreEqual(message.Title, "t");
            Assert.NotNull(message.CreatedDate);
            Assert.AreEqual(message.Body, "b");
            Assert.AreEqual(message.CreatedByUser, "un");


        }

        [Test]
        public void CanGetSingleMessageCanReturnNull()
        {
            PortalDiscussion message = _db.GetSingleMessage(9999);
            Assert.IsNull(message);
        }

        [Test]
        public void CanGetTopLevelMessagesCanReturnEmptyList()
        {
            Collection<PortalDiscussion> messages = _db.GetTopLevelMessages(9999);
            Assert.IsTrue(messages.Count == 0);
        }

        [Test]
        public void CanGetThreadMessagesCanReturnEmptyList()
        {
            Collection<PortalDiscussion> messages = _db.GetThreadMessages("RandomString12345");
            Assert.IsTrue(messages.Count == 0);

        }
    }
}