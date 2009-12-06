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
            //isolate data layer in child container
            IUnityContainer childContainer = container.CreateChildContainer();

            //use lambda to pull services from the child container
            childContainer.RegisterType<IProductRepository, ProductRepository>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<Func<IProductRepository>>(() => childContainer.Resolve<IProductRepository>());

            //deal with db specific technology only here...
            childContainer.RegisterInstance<Func<IDbConnection>>(() => new SqlCeConnection(ConnectionString));
            childContainer.RegisterInstance<Func<string, IDbCommand>>(
                delegate(string cmd)
                {
                    IDbConnection connection = childContainer.Resolve<Func<IDbConnection>>()();
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
