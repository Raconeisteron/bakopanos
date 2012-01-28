using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Transactions;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    [TestFixture, Explicit]
    public class SqlUsersDbFixture 
    {
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

        private readonly TransactionScope _scope = new TransactionScope();
        private IUsersDb _db;

        [Test]
        public void CanAddUser()
        {
            //act
            int userId = _db.AddUser("testuser", "a@a.com", "secret");
            //assert
            Assert.AreNotEqual(userId, -1);
            PortalUser user = _db.GetSingleUser("a@a.com");
            Assert.IsTrue(user.Email == "a@a.com");
            Assert.IsTrue(user.Name == "testuser");
            Assert.IsTrue(user.UserId == userId);
        }

        [Test]
        public void CanDeleteUser()
        {
            //arrange
            int userId = _db.AddUser("testuser", "a@a.com", "secret");
            //act
            _db.DeleteUser(userId);
            //assert
            PortalUser user = _db.GetSingleUser("aa@a.com");
            Assert.IsNotNull(user);
        }

        [Test]
        public void CanGetRoles()
        {
            //act
            string[] roles = _db.GetRoles("guest");
        }

        [Test]
        public void CanGetRolesByUser()
        {
            //act
            Collection<PortalRole> roles = _db.GetRolesByUser("guest");
        }

        [Test]
        public void CanGetRolesByUserCanReturnEmptyList()
        {
            //act
            Collection<PortalRole> roles = _db.GetRolesByUser("ooops");
            //assert            
            Assert.IsTrue(roles.Count == 0);
        }

        [Test]
        public void CanGetRolesCanReturnEmptyList()
        {
            //act
            string[] roles = _db.GetRoles("ooops");
            //assert            
            Assert.IsTrue(roles.Length == 0);
        }

        [Test]
        public void CanGetSingleUser()
        {
            //arrange
            int userId = _db.AddUser("testuser", "a@a.com", "secret");
            //act
            PortalUserDetails user = _db.GetSingleUser("a@a.com");
            //assert            
            Assert.IsTrue(user.Email == "a@a.com");
            Assert.IsTrue(user.Name == "testuser");
            Assert.IsTrue(user.Password == "secret");
            Assert.IsTrue(user.UserId == userId);
        }

        [Test]
        public void CanGetSingleUserCanReturnNull()
        {
            //act
            PortalUser user = _db.GetSingleUser("oops");
            //assert            
            Assert.IsNull(user);
        }

        [Test]
        public void CanLogin()
        {
            //act
            _db.Login("guest", "guest");
        }

        [Test]
        public void CanUpdateUser()
        {
            //arrange
            int userId = _db.AddUser("tu", "b@b.com", "secretsecret");

            //act
            _db.UpdateUser(userId, "a@a.com", "secret");
            //assert            
            PortalUserDetails user = _db.GetSingleUser("a@a.com");
            Assert.IsTrue(user.Email == "a@a.com");
            Assert.IsTrue(user.Name == "tu");
            Assert.IsTrue(user.Password == "secret");
            Assert.IsTrue(user.UserId == userId);
        }
    }
}