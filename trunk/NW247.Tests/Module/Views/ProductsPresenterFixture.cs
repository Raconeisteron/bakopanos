using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using NW247.Services;
using Rhino.Mocks;

namespace NW247.Module.Views
{
    [TestFixture]
    public class ProductsPresenterFixture : TestHelper
    {
        [Test]
        public void CanResolveProductsPresenterTest()
        {
            Assert.IsNotNull(GetProductsPresenter());
        }
    }
}
