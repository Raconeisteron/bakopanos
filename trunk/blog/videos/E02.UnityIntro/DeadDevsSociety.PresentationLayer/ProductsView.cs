using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeadDevsSociety.BusinessLayer;
using DeadDevsSociety.Framework;

namespace DeadDevsSociety.PresentationLayer
{
    public class ProductsView
    {
        private ProductsFacade _facade = new ProductsFacade();
        
        public void Show()
        {          
            LogServiceSingleton.LogService.WriteLine("presentation:show");
            foreach (Product item in _facade.GetProductByName("."))//all for now...
            {
                Console.WriteLine("{0}: {1}",item.Id,item.ProductName);
            }
        }
    }
}
