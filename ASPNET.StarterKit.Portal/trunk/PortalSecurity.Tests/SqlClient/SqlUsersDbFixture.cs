using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Transactions;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    [TestFixture, Explicit]
    public class SqlUsersDbFixture
    {
        private readonly TransactionScope _scope = new TransactionScope();
        private IUsersDb _db;

        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _db = new SqlUsersDb(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }

        #endregion



        [Test]
        public void CanAddUser()
        {
            //act
            int retValue = _db.AddUser("testuser", "a@a.com", "secret");
            //assert
            Assert.AreNotEqual(retValue, -1);
        }
    }
}