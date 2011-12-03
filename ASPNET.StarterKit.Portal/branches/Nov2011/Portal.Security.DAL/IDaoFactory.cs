namespace Portal.Security.Data
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