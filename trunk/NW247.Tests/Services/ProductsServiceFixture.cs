using Microsoft.Practices.Unity;
using NUnit.Framework;
using NW247.Model;

namespace NW247.Services
{
    [TestFixture]
    public class ProductsServiceFixture
    {
        [Test]
        public void GetProducts()
        {   
            NorthwindDataSet.ProductsDataTable table = 
                TestHelper.GetProductsService().GetProducts();

            Assert.IsTrue(table.Rows.Count > 0);
        }

        [Test]
        [Ignore]
        public void UpdateAll()
        {
        }
    }
}