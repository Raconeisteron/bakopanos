using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bakopanos.NW.BusinessObjects;
using Bakopanos.NW.DataObjects;
using Microsoft.Practices.Unity;

namespace Bakopanos.NW.Facade.Products
{
    public interface IProductsFacade
    {
        List<ProductBO> Products();
    }
}
