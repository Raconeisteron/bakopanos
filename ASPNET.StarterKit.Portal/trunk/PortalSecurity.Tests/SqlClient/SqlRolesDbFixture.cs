using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Transactions;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    [TestFixture, Explicit]
    public class SqlRolesDbFixture 
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _db = new SqlRolesDb(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }

        #endregion

        private readonly TransactionScope _scope = new TransactionScope();
        private IRolesDb _db;

        [Test]
        public void CanAddRole()
        {
            //act
            _db.AddRole(0, "tester");
        }

        [Test]
        public void CanAddUserRole()
        {
            //act
            _db.AddUserRole(0, 1);
        }

        [Test]
        public void CanDeleteRole()
        {
            //act
            _db.DeleteRole(1);
        }

        [Test]
        public void CanDeleteUserRole()
        {
            //act
            _db.DeleteUserRole(0, 1);
        }

        [Test]
        public void CanGetPortalRoles()
        {
            //act
            Collection<PortalRole> roles = _db.GetPortalRoles(0);
        }

        [Test]
        public void CanGetRoleMembers()
        {
            //act
            Collection<PortalUser> users = _db.GetRoleMembers(0);
        }

        [Test]
        public void CanGetUsers()
        {
            //act
            Collection<PortalUser> users = _db.GetUsers();
        }

        [Test]
        public void CanUpdateRole()
        {
            //act
            _db.UpdateRole(1, "anothertester");
        }
    }
}