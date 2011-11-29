using System.Configuration;

namespace Portal.Security.DAL.SqlServer
{
    /// <summary>
    /// Sql Server specific factory that creates Sql Server specific data access objects.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public class DaoFactory : IDaoFactory
    {
        private readonly string _connectionString;

        public DaoFactory()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

        #region IDaoFactory Members

        public IRolesDb RolesDb
        {
            get { return new RolesDb {ConnectionString = _connectionString}; }
        }

        public IUsersDb UsersDb
        {
            get { return new UsersDb {ConnectionString = _connectionString}; }
        }

        #endregion
    }
}