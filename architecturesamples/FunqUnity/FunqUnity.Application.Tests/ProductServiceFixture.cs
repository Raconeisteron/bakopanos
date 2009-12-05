using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunqUnity.Domain;
using FunqUnity.Infrastructure.Repository;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Rhino.Mocks;

namespace FunqUnity.Application.Tests
{
    [TestFixture]
    public class ProductServiceFixture
    {
        readonly IUnityContainer _container = new UnityContainer();
        readonly MockRepository _repository = new MockRepository();

        /// <summary>
        /// note that we ommit the database,....
        /// we simply verify that the filtering inside the ProductService
        /// works given a hard coded list of products
        /// </summary>
        [Test]
        public void GetProductsCheckFilteringWorksTest()
        {
            //arange            
            var mocks = _repository.StrictMock<IProductRepository>();            
            Expect.Call(mocks.GetProducts()).Return(new List<Product>
                                                       {
                                                           new Product {Name = "test1"},
                                                           new Product {Name = "prod1"}
                                                       });            
            _container.RegisterInstance(mocks);
            _container.RegisterType<IProductService, ProductService>();
            var service = _container.Resolve<IProductService>();         
   
            //act            
            mocks.Replay();
            IEnumerable<Product> products = service.GetProducts("test");

            //assert
            Assert.IsTrue(products.Count() == 1);
        }
    }
}
