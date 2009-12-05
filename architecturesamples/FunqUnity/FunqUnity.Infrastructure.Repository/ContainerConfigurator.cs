using System;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using Microsoft.Practices.Unity;

namespace FunqUnity.Infrastructure.Repository
{


    public class ContainerConfigurator : ContainerConfiguratorBase
    {
        public override IUnityContainer Configure(IUnityContainer container)
        {
            container.RegisterType<IProductRepository, ProductRepository>(new ContainerControlledLifetimeManager());
            //deal with db specific technology here only...
            container.RegisterInstance<Func<IDbConnection>>(() => new SqlCeConnection(ConnectionString));
            container.RegisterInstance<Func<string, IDbCommand>>(
                delegate(string cmd)
                {
                    IDbConnection connection = container.Resolve<Func<IDbConnection>>()();
                    connection.Open();
                    return new SqlCeCommand(cmd, (SqlCeConnection)connection);
                }
                );

            return container;
        }

        [ConfigurationProperty("connectionString")]
        public string ConnectionString
        {
            get { return this["connectionString"] as string; }
            set { this["connectionString"] = value; }
        }

    }
}
