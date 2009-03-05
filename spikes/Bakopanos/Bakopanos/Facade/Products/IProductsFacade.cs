using System.Collections.Generic;
using Bakopanos.BusinessObjects;

namespace Bakopanos.Facade.Products
{
    public interface IProductsFacade
    {
        List<ProductBO> Products();
    }
}