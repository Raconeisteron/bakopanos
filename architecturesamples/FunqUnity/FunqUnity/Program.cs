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

            IUnityContainer container = new UnityContainer().
                Configure(ConfigurationManager.AppSettings["ContainerConfiguration"].Split(';'));      

            var products = container.Resolve<IProductService>();

            foreach (var s in products.GetProducts("a"))
            {
                Console.WriteLine(s.Name);
            }

            Console.ReadLine();
        }

        

    }

    


}
