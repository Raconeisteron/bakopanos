using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DeadDevsSociety.DataLayer;
using DeadDevsSociety.Framework;
using Microsoft.Practices.Unity;

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

    public interface IProductsFacade
    {
        IEnumerable<Product> GetProductByName(string name);
    }

    internal class ProductsFacade : IProductsFacade
    {
        private readonly LogService _logService;
        public ProductsFacade(LogService logService)
        {
            _logService = logService;
        }

        [Dependency]
        public IProductsData Data
        {
            private get; set;
        }

        public IEnumerable<Product> GetProductByName(string name)
        {
            _logService.WriteLine("business:get");
            var regex = new Regex(name);
            return from item in Data.GetAllProducts()
                   where regex.IsMatch(item.ProductName)  
                   select item.Map();
        }
    }
}
