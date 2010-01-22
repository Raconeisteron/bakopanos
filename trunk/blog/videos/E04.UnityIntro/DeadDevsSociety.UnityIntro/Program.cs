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
            IUnityContainer container = new UnityContainer();
            container.RegisterType<LogService, LogService>(new ContainerControlledLifetimeManager());

            //(ConfigurationManager.GetSection("presentation") as IModule).Configure(container);
            //(ConfigurationManager.GetSection("business") as IModule).Configure(container);
            //(ConfigurationManager.GetSection("data") as IModule).Configure(container);

            string[] modules = ConfigurationManager.AppSettings["modules"].Split(',');

            foreach (string section in modules)
            {                
                object instance = ConfigurationManager.GetSection(section);
                var parameters = new object[] { container };
                instance.GetType().GetMethod("Configure").Invoke(instance, parameters);                
            }

            container.Resolve<IProductsView>();

            Console.ReadLine();
        }
    }
}
