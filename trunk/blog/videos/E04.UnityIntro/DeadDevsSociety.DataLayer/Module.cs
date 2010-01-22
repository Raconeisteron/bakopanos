using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DeadDevsSociety.Framework;
using Microsoft.Practices.Unity;

namespace DeadDevsSociety.DataLayer
{

    internal interface IDataLayerConfiguration
    {
        string ConnectionString { get; }
    }

    public class Module : ConfigurationSection, IDataLayerConfiguration//,IModule
    {
        public IUnityContainer Configure(IUnityContainer container)
        {
            container.RegisterInstance<IDataLayerConfiguration>(this);
            container.RegisterType<IProductsData,ProductsData>();            
            return container;
        }

        [ConfigurationProperty("connectionString",IsRequired = true)]
        public string ConnectionString
        {
            get { return this["connectionString"] as string; }
        }
    }
}
