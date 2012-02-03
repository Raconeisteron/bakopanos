using System.Configuration;
using System.Transactions;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    [TestFixture, Explicit]
    public class SqlHtmlTextsDbFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _db = new SqlHtmlTextsDb(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }

        #endregion

        private readonly TransactionScope _scope = new TransactionScope();
        private IHtmlTextsDb _db;

        [Test]
        public void CanGetHtmlText()
        {
        }

        [Test]
        public void CanGetHtmlTextCanReturnNull()
        {
            PortalHtmlText htmlText = _db.GetHtmlText(9999);
            Assert.IsNull(htmlText);
        }

        [Test]
        public void CanUpdateHtmlText()
        {
            //moduleId must exist in Database
            _db.UpdateHtmlText(2, "dh", "ms", "md");
        }
    }
}