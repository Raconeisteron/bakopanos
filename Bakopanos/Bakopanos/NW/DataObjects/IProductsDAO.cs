using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bakopanos.NW.BusinessObjects;

namespace Bakopanos.NW.DataObjects
{
    public interface IProductsDAO
    {
        List<ProductBO> GetAllProducts();
        ProductBO GetSingleProduct(int ProductID);
    }
}
