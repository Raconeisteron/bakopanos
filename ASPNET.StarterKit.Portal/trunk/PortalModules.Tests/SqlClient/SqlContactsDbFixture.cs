using System.Collections.ObjectModel;
using System.Configuration;
using System.Transactions;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    [TestFixture, Explicit]
    public class SqlContactsDbFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _db = new SqlContactsDb(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }

        #endregion

        private readonly TransactionScope _scope = new TransactionScope();
        private IContactsDb _db;

        [Test]
        public void CanAddContact()
        {
            int id = _db.AddContact(0, "un", "n", "r", "e", "c1", "c2");
            Assert.AreNotEqual(id, -1);
        }

        [Test]
        public void CanDeleteContact()
        {
            int id = _db.AddContact(0, "un", "n", "r", "e", "c1", "c2");
            _db.DeleteContact(id);
        }

        [Test]
        public void CanUpdateContact()
        {
            _db.UpdateContact(0, "un", "n", "r", "e", "c1", "c2");
        }

        [Test]
        public void CanGetContacts()
        {

        }

        [Test]
        public void CanGetContactsCanReturnEmptyList()
        {
            Collection<PortalContact> contacts = _db.GetContacts(9999);
            Assert.IsTrue(contacts.Count == 0);
        }

        [Test]
        public void CanGetSingleContact()
        {
            int id = _db.AddContact(0, "un", "n", "r", "e", "c1", "c2");
            PortalContact contact = _db.GetSingleContact(id);
            Assert.AreEqual(contact.ItemId, id);
            Assert.AreEqual(contact.ModuleId, 0);
            Assert.AreEqual(contact.CreatedByUser, "un");
            Assert.IsNotNull(contact.CreatedDate);
            Assert.AreEqual(contact.Name, "n");
            Assert.AreEqual(contact.Role, "r");
            Assert.AreEqual(contact.Email, "e");
            Assert.AreEqual(contact.Contact1, "c1");
            Assert.AreEqual(contact.Contact2, "c2");

        }

        [Test]
        public void CanGetSingleContactCanReturnNull()
        {
            PortalContact contact = _db.GetSingleContact(9999);
            Assert.IsNull(contact);
        }
    }
}