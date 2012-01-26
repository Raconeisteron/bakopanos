namespace ASPNET.StarterKit.Portal
{
    public interface IUsersDbFixture
    {
        void CanLogin();
        void CanGetRolesByUser();
        void CanGetRoles();
        void CanGetSingleUser();
        void CanUpdateUser();
        void CanDeleteUser();
        void CanAddUser();
        void CanGetRolesByUserCanReturnEmptyList();
        void CanGetRolesCanReturnEmptyList();
        void CanGetSingleUserCanReturnNull();
    }
}