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
        public void CanLogin()
        {
            //act
            _db.Login("guest", "guest");

        }

        [Test]
        public void CanGetRolesByUser()
        {
            //act
            List<PortalRole> roles = _db.GetRolesByUser("guest");

        }

        [Test]
        public void CanGetRoles()
        {
            //act
            string[] roles = _db.GetRoles("guest");

        }

        [Test]
        public void CanGetSingleUser()
        {
            //act
            PortalUser user = _db.GetSingleUser("guest");

        }

        [Test]
        public void CanUpdateUser()
        {
            //act
            _db.UpdateUser(0, "a@a.com", "secret");

        }

        [Test]
        public void CanDeleteUser()
        {
            //act
            _db.DeleteUser(0);
            
        }

        /// <summary>
        /// Tests that the AddUser method works and that all parameters
        /// </summary>
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