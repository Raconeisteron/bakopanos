using System.Collections.Generic;
using System.Linq;
using DeadDevsSociety.UnityDependencyInjection.Data;
using DeadDevsSociety.UnityDependencyInjection.Gui;

namespace DeadDevsSociety.UnityDependencyInjection.Business
{
    public class ProductsFacade
    {
        public IEnumerable<ProductModel> GetProducts(string filter)
        {
            var query  = from item in new ProductsDao().List()
                     select item.ToModel();
            return query;
        }
    }
}
