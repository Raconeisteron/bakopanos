using System;
using System.Configuration;
using FunqUnity.Application;
using FunqUnity.Infrastructure;
using Microsoft.Practices.Unity;

namespace FunqUnity
{
    static class Program
    {
        static void Main(string[] args)
        {
            //Bootstrapper
            IUnityContainer container = new UnityContainer().
                Configure(ConfigurationManager.AppSettings["ContainerConfiguration"].Split(';'));

            //show something
            var products = container.Resolve<Func<IProductService>>()();
            foreach (var s in products.GetProducts("a"))
            {
                Console.WriteLine(s.Name);
            }

            //exit
            Console.ReadLine();
        }

        

    }

    


}
