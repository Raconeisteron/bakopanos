using NUnit.Framework;

namespace ASPNET.StarterKit.Portal
{
    public interface IRolesDbFixture
    {
        [Test]
        void CanGetPortalRoles();

        [Test]
        void CanGetUsers();

        [Test]
        void CanGetRoleMembers();

        [Test]
        void CanAddRole();

        [Test]
        void CanAddUserRole();

        [Test]
        void CanDeleteRole();

        [Test]
        void CanDeleteUserRole();

        [Test]
        void CanUpdateRole();
    }
}