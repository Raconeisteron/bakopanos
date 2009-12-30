using System.Collections.Generic;
using System.Linq;
using DeadDevsSociety.UnityDependencyInjection.BusinessObjects;
using DeadDevsSociety.UnityDependencyInjection.Data;

namespace DeadDevsSociety.UnityDependencyInjection.Business
{
    public class ProductsFacade
    {
        public IEnumerable<ProductBo> GetProducts(string filter)
        {
            IEnumerable<ProductBo> query = from item in new ProductsDao().List()
                                           select item;
            return query;
        }
    }
}