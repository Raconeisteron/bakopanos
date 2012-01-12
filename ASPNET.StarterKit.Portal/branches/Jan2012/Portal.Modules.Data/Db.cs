using System.Configuration;
using Microsoft.Practices.Unity;

namespace Portal.Modules.Data
{
    public class Db
    {
        protected string ConnectionString { get; private set; }

        [InjectionMethod]
        public void Initialize(ConnectionStringSettings connectionStringSettings)
        {
            ConnectionString = connectionStringSettings.ConnectionString;
        }
    }
}