using System.Configuration;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
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