using NUnit.Framework;

namespace ASPNET.StarterKit.Portal
{
    public interface IUsersDbFixture
    {
        [Test]
        void CanLogin();

        [Test]
        void CanGetRolesByUser();

        [Test]
        void CanGetRoles();

        [Test]
        void CanGetSingleUser();

        [Test]
        void CanUpdateUser();

        [Test]
        void CanDeleteUser();

        [Test]
        void CanAddUser();
    }
}