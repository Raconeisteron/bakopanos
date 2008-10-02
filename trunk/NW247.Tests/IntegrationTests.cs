using Microsoft.Practices.Unity;
using NUnit.Framework;
using NW247.Data.NorthwindDataSetTableAdapters;
using NW247.Services;
using Rhino.Mocks;

namespace NW247
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void CanResolveProductsServiceWithMockDataTest()
        {
            IUnityContainer container = TestHelper.IntegrationTestContainer();

            var repo = new MockRepository();
            object adapter = repo.Stub(typeof (ProductsTableAdapter));
            container.RegisterInstance(typeof (ProductsTableAdapter), adapter);

            var service = container.Resolve<IProductsService>();
            Assert.IsNotNull(service); //should be able to resolve service
            Assert.IsTrue(service.GetProducts().Count == 0); //make sure that no data layer is attached
        }
    }
}