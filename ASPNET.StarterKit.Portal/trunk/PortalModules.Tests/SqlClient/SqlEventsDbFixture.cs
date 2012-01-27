using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Transactions;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    [TestFixture, Explicit]
    public class SqlEventsDbFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _db = new SqlEventsDb(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }

        #endregion

        private readonly TransactionScope _scope = new TransactionScope();
        private IEventsDb _db;

        [Test]
        public void CanAddEvent()
        {
            string time = DateTime.Now.ToLongDateString();
            int id = _db.AddEvent(0, "un", "t", DateTime.Parse(time), "d", "ww");
            Assert.AreNotEqual(id, -1);
        }

        [Test]
        public void CanDeleteEvent()
        {
            string time = DateTime.Now.ToLongDateString();
            int id = _db.AddEvent(0, "un", "t", DateTime.Parse(time), "d", "ww");
            _db.DeleteEvent(id);
        }

        [Test]
        public void CanUpdateEvent()
        {
            _db.UpdateEvent(0, "un", "t", DateTime.Now, "d", "ww");
        }

        [Test]
        public void CanGetEvents()
        {

        }

        [Test]
        public void CanGetEventsCanReturnEmptyList()
        {
            Collection<PortalEvent> eventObjs = _db.GetEvents(9999);
            Assert.IsTrue(eventObjs.Count == 0);
        }

        [Test]
        public void CanGetSingleEvent()
        {
            string time = DateTime.Now.ToLongDateString();
            int id = _db.AddEvent(0, "un", "t", DateTime.Parse(time), "d", "ww");
            PortalEvent eventObj = _db.GetSingleEvent(id);
            Assert.AreEqual(eventObj.ItemId, id);
            Assert.AreEqual(eventObj.ModuleId, 0);
            Assert.AreEqual(eventObj.CreatedByUser, "un");
            Assert.AreEqual(eventObj.Title, "t");
            Assert.AreEqual(eventObj.ExpireDate, DateTime.Parse(time));
            Assert.AreEqual(eventObj.Description, "d");
            Assert.AreEqual(eventObj.WhereWhen, "ww");
        }

        [Test]
        public void CanGetSingleEventCanReturnNull()
        {
            PortalEvent eventObj = _db.GetSingleEvent(9999);
            Assert.IsNull(eventObj);
        }



    }
}