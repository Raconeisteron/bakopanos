using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeadDevsSociety.BusinessLayer;
using DeadDevsSociety.Framework;
using Microsoft.Practices.Unity;

namespace DeadDevsSociety.PresentationLayer
{
    public interface IProductsView
    {
    }

    internal class ProductsView : IProductsView
    {
        private readonly IProductsFacade _facade;
        private readonly LogService _logService;       

        public ProductsView(IProductsFacade facade, LogService logService)
        {
            _facade = facade;
            _logService = logService;
        }
        
        [InjectionMethod]
        public void Show()
        {          
            _logService.WriteLine("presentation:show");
            foreach (Product item in _facade.GetProductByName("."))//all for now...
            {
                Console.WriteLine("{0}: {1}",item.Id,item.ProductName);
            }
        }
    }
}
