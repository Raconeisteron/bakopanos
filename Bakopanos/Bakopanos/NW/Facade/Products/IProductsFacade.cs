using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bakopanos.NW.DataObjects;
using Microsoft.Practices.Unity;

namespace Bakopanos.NW.Facade.Products
{
    public interface IProductsFacade
    {
        ProductsDAO DAO { set; }
    }
}
