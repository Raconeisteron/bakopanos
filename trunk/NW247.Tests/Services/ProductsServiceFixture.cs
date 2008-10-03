using Microsoft.Practices.Unity;
using NUnit.Framework;
using NW247.Model;

namespace NW247.Services
{
    [TestFixture]
    public class ProductsServiceFixture : TestHelper
    {
        [Test]
        public void CanResolveProductsServiceTest()
        {
            Assert.IsNotNull(GetProductsService());
        }        
    }
}