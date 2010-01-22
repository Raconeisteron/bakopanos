using System;
using System.Collections.Generic;
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

            container.RegisterType(
                Type.GetType("DeadDevsSociety.DataLayer.IProductsData,DeadDevsSociety.DataLayer"), 
                Type.GetType("DeadDevsSociety.DataLayer.ProductsData,DeadDevsSociety.DataLayer"));

            container.RegisterType(
                Type.GetType("DeadDevsSociety.BusinessLayer.IProductsFacade,DeadDevsSociety.BusinessLayer"),
                Type.GetType("DeadDevsSociety.BusinessLayer.ProductsFacade,DeadDevsSociety.BusinessLayer"));

            container.RegisterType(
                typeof(IProductsView),
                Type.GetType("DeadDevsSociety.PresentationLayer.ProductsView,DeadDevsSociety.PresentationLayer"));


            container.Resolve<IProductsView>();

            Console.ReadLine();
        }
    }
}
