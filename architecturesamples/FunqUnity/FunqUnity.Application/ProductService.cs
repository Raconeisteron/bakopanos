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
        private IProductRepository _repo;

        [Dependency]
        public Func<IProductRepository> Repo 
        {
            set
            {
                //use lambda but cache the instance
                _repo = value();
            }
        }         

        public IEnumerable<Product> GetProducts(string name)
        {
            List<Product> products = _repo.GetProducts();
            return from p in products 
                   where p.Name.Contains(name) 
                   select p;
        }

        [InjectionMethod]
        public void Init()
        {
            //some init code here...
        }

    }
}
