using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DeadDevsSociety.Framework;
using DeadDevsSociety.PresentationLayer;
using Microsoft.Practices.Unity;

namespace DeadDevsSociety.UnityIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            IUnityContainer container = new UnityContainer();
            container.RegisterType<LogService, LogService>(new ContainerControlledLifetimeManager());

            //(ConfigurationManager.GetSection("presentation") as IModule).Configure(container);
            //(ConfigurationManager.GetSection("business") as IModule).Configure(container);
            //(ConfigurationManager.GetSection("data") as IModule).Configure(container);

            object presentation = ConfigurationManager.GetSection("presentation");
            object business = ConfigurationManager.GetSection("business");
            object data = ConfigurationManager.GetSection("data");

            object[] parameters = new object[] { container };

            presentation.GetType().GetMethod("Configure").Invoke(presentation, parameters);
            business.GetType().GetMethod("Configure").Invoke(business, parameters);
            data.GetType().GetMethod("Configure").Invoke(data, parameters);

            container.Resolve<IProductsView>();

            Console.ReadLine();
        }
    }
}
