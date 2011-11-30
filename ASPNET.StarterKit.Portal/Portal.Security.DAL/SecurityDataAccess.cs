using System;
using System.Configuration;

namespace Portal.Security.DAL
{
    /// <summary>
    /// This class shields the client from the details of database specific 
    /// data-access objects. It returns the appropriate data-access objects 
    /// according to the configuration in web.config.
    /// </summary>
    /// <remarks>
    /// GoF Design Patterns: Factory, Singleton, Proxy.
    /// 
    /// This class makes extensive use of the Factory pattern in determining which 
    /// database specific DAOs (Data Access Objects) to return.
    /// 
    /// This class is like a Singleton -- it is a static class and 
    /// therefore only one 'instance' will ever exist.
    /// 
    /// This class is a Proxy as it 'stands in' for the actual Data Access Object Factory.
    /// </remarks>
    public static class SecurityDataAccess
    {
        private static readonly IDaoFactory Factory;

        static SecurityDataAccess()
        {
            Type type = Type.GetType(ConfigurationManager.AppSettings["Security.DAL"]);
            Factory = (IDaoFactory) Activator.CreateInstance(type);
        }

        public static IRolesDb RolesDb
        {
            get { return Factory.RolesDb; }
        }

        public static IUsersDb UsersDb
        {
            get { return Factory.UsersDb; }
        }
    }
}