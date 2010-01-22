using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DeadDevsSociety.DataLayer;
using DeadDevsSociety.Framework;

namespace DeadDevsSociety.BusinessLayer
{

    public static class Mapper
    {
        public static Product Map(this ProductEntity entity)
        {
            return new Product
                       {
                           Id = entity.Id,
                           ProductName = entity.ProductName
                       };
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
    }

    public class ProductsFacade
    {
        private readonly ProductsData _data = new ProductsData();

        public IEnumerable<Product> GetProductByName(string name)
        {
            LogServiceSingleton.LogService.WriteLine("business:get");
            var regex = new Regex(name);
            return from item in _data.GetAllProducts()
                   where regex.IsMatch(item.ProductName)  
                   select item.Map();
        }
    }
}
