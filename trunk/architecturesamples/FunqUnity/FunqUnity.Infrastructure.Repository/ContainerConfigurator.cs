using System;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using Microsoft.Practices.Unity;

namespace FunqUnity.Infrastructure.Repository
{


    public class ContainerConfigurator : ContainerConfiguratorBase
    {
        IUnityContainer _childContainer;

        public override IUnityContainer Configure(IUnityContainer container)
        {
            //isolate data layer in child container
            _childContainer = container.CreateChildContainer();

            //use lambda to pull services from the child container
            _childContainer.RegisterType<IProductRepository, ProductRepository>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<Func<IProductRepository>>(() => _childContainer.Resolve<IProductRepository>());

            //deal with db specific technology only here...
            _childContainer.RegisterInstance<Func<IDbConnection>>(() => fConnection());
            _childContainer.RegisterInstance<Func<string, IDbCommand>>((cmd) => fCommand(cmd));

            return container;
        }

        private IDbConnection fConnection()
        {
            switch (ProviderName)
            {
                case "SqlCe":
                    return new SqlCeConnection(ConnectionString);
                default:
                    throw new NotSupportedException();
            }         
        }

        private IDbCommand fCommand(string cmd)
        {
            IDbConnection connection = _childContainer.Resolve<Func<IDbConnection>>()();
            connection.Open();
            switch (ProviderName)
            {
                case "SqlCe":
                    return new SqlCeCommand(cmd, (SqlCeConnection)connection);
                default:
                    throw new NotSupportedException();
            }
        }

        [ConfigurationProperty("connectionString")]
        public string ConnectionString
        {
            get { return this["connectionString"] as string; }            
        }


        [ConfigurationProperty("providerName")]
        public string ProviderName
        {
            get { return this["providerName"] as string; }            
        }        
               
    }
}
