using System;
using System.Collections.Generic;
using System.Linq;
using FunqUnity.Domain;
using FunqUnity.Infrastructure.Repository;
using Microsoft.Practices.Unity;

namespace FunqUnity.Application
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(string name);
    }

    internal class ProductService : IProductService
    {
        [Dependency]
        public IProductRepository _repo { get; set; }         

        public IEnumerable<Product> GetProducts(string name)
        {            
            return from p in _repo.GetProducts() where p.Name.Contains(name) select p;
        }

        [InjectionMethod]
        public void Init()
        {
            //some init code here...
        }

    }
}
