using System.Collections.Generic;
using Bakopanos.BusinessObjects;

namespace Bakopanos.DataObjects
{
    public interface IProductsDAO
    {
        List<ProductBO> GetAllProducts();
        ProductBO GetSingleProduct(int ProductID);
    }
}