using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal.XmlFile
{
    [TestFixture]
    public class XmlRolesDbFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _tempFileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            File.Copy(@"xmlfile\Security.Xml", _tempFileName);
            _db = new XmlRolesDb(_tempFileName);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_tempFileName);
        }

        #endregion

        private IRolesDb _db;
        private string _tempFileName;


        public void CanGetPortalRoles()
        {
            throw new NotImplementedException();
        }

        public void CanGetUsers()
        {
            throw new NotImplementedException();
        }

        public void CanGetRoleMembers()
        {
            throw new NotImplementedException();
        }

        public void CanAddRole()
        {
            throw new NotImplementedException();
        }

        public void CanAddUserRole()
        {
            throw new NotImplementedException();
        }

        public void CanDeleteRole()
        {
            throw new NotImplementedException();
        }

        public void CanDeleteUserRole()
        {
            throw new NotImplementedException();
        }

        public void CanUpdateRole()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GetPortalMembersReturnsCorrectFirstPortalUser()
        {
            PortalUser item = _db.GetRoleMembers(0)[0];
            Assert.IsTrue(item.UserId == 0);
            Assert.IsTrue(item.Name == "Guest");
            Assert.IsTrue(item.Email == "guest");
        }

        [Test]
        public void GetPortalMembersReturnsCorrectNumberOfPortalUsers()
        {
            List<PortalUser> items = _db.GetRoleMembers(0);
            Assert.IsTrue(items.Count == 1);
        }

        [Test]
        public void GetPortalRolesReturnsCorrectFirstPortalRole()
        {
            PortalRole item = _db.GetPortalRoles(0)[0];
            Assert.IsTrue(item.PortalId == 0);
            Assert.IsTrue(item.RoleId == 0);
            Assert.IsTrue(item.RoleName == "Admins");
        }

        [Test]
        public void GetPortalRolesReturnsCorrectNumberOfPortalRoles()
        {
            List<PortalRole> items = _db.GetPortalRoles(0);
            Assert.IsTrue(items.Count == 1);
        }
    }
}