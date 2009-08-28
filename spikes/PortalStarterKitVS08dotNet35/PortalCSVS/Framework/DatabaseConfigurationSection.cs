using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal.Framework
{
    public abstract class DatabaseConfigurationSection : ConfigurationSection, IContainerConfigurator,IDatabaseConfiguration
    {
        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString { get { return (string)this["connectionString"]; } }

        public abstract void Configure(IUnityContainer container);
    }

    

}