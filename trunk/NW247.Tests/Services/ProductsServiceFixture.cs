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
            IProductsService service = TestHelper.GetProductsService();
            NorthwindDataSet.ProductsDataTable table = service.GetProducts();
            Assert.IsTrue(table.Rows.Count > 0);
        }

        [Test]
        [Ignore]
        public void UpdateAll()
        {
        }
    }
}