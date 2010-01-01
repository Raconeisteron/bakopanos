using System;
using DeadDevsSociety.UnityDependencyInjection.Domain;
using DeadDevsSociety.UnityDependencyInjection.Facade;
using DeadDevsSociety.UnityDependencyInjection.Library;

namespace DeadDevsSociety.UnityDependencyInjection.Presentation
{   
    public class ProductsView
    {
        private readonly ProductsService _appServices = new ProductsService();

        public void Show()
        {
            LoggerSingleton.Instance.Information("Show ProductsView", "ProductView");

            foreach (Product item in _appServices.Products("."))
            {
                if (item.DateOfBirth.HasValue)
                {
                    Console.WriteLine("{0}, {1} ({2})",
                        item.FirstName, item.LastName, CalculateAge(item.DateOfBirth.Value.Date));                   
                }
                else
                {
                    Console.WriteLine("{0}, {1}", 
                        item.FirstName, item.LastName);                    
                }
            }
        }

        public static int CalculateAge(DateTime birthdate)
        {
            // get the difference in years
            int years = DateTime.Now.Year - birthdate.Year;
            // subtract another year if we're before the
            // birth day in the current year
            if (DateTime.Now.Month < birthdate.Month || 
                (DateTime.Now.Month == birthdate.Month && DateTime.Now.Day < birthdate.Day))
                years--;

            return years;
        }

    }    

}