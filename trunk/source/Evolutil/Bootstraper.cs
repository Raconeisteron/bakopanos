using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Evolutil.Library;
using Evolutil.Library.Log;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Evolutil
{
    public class Bootstraper
    {
        public IUnityContainer Start()
        {
            IUnityContainer container = new UnityContainer();
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Containers.Default.Configure(container);

            var log = (ILogConfigurationSection)ConfigurationManager.GetSection("log");
            log.Configure(container);

            var log1 = (ILogConfigurationSection)ConfigurationManager.GetSection("log1");
            log1.Configure(container, "log1");

            var datalayer = ConfigurationManager.AppSettings["DataLayer"];
            var servicelayer = ConfigurationManager.AppSettings["ServiceLayer"];
            var connectionstring = ConfigurationManager.ConnectionStrings[datalayer].ConnectionString;
            container.RegisterInstance<string>("ConnectionString", connectionstring);

            //register layers...
            container.Resolve<IModuleBuilder>(datalayer);
            container.Resolve<IModuleBuilder>(servicelayer);

            return container;
        }
    }
}
