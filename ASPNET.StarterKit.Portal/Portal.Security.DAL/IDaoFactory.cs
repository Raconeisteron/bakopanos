namespace ASPNET.StarterKit.Portal.Security.DAL
{
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public interface IDaoFactory
    {
        IRolesDb RolesDb { get; }
        IUsersDb UsersDb { get; }
    }
}