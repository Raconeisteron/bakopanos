using System.Collections.ObjectModel;
using System.Configuration;
using System.Transactions;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    [TestFixture, Explicit]
    public class SqlDocumentsDbFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _db = new SqlDocumentsDb(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }

        #endregion

        private readonly TransactionScope _scope = new TransactionScope();
        private IDocumentsDb _db;

        [Test]
        public void CanDeleteDocument()
        {
            _db.DeleteDocument(0);
        }

        [Test]
        public void CanGetDocuments()
        {
        }

        [Test]
        public void CanGetDocumentsCanReturnEmptyList()
        {
            Collection<PortalDocument> documents = _db.GetDocuments(9999);
            Assert.IsTrue(documents.Count == 0);
        }

        [Test]
        public void CanGetSingleDocument()
        {

        }

        [Test]
        public void CanGetSingleDocumentCanReturnNull()
        {
            PortalDocument document = _db.GetSingleDocument(9999);
            Assert.IsNull(document);
        }

        [Test]
        public void CanUpdateDocument()
        {
            //_db.UpdateDocument(0, "un", "t", DateTime.Now, "d", "ml", "mml");
        }
    }
}