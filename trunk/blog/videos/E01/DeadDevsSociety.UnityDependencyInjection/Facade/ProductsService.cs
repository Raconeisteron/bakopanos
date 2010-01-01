using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DeadDevsSociety.UnityDependencyInjection.DataAccess;
using DeadDevsSociety.UnityDependencyInjection.Domain;
using DeadDevsSociety.UnityDependencyInjection.Library;

namespace DeadDevsSociety.UnityDependencyInjection.Facade
{
    public class ProductsService
    {
        private readonly ProductsRepository _repository = new ProductsRepository();

        public IEnumerable<Product> Products(string filter)
        {
            LoggerSingleton.Instance.Information("Filter=" + filter, "ProductsService");

            return from item in _repository.List()
                   where (Regex.IsMatch(item.FirstName, filter) |
                        Regex.IsMatch(item.LastName, filter))
                   select item;
        }
    }
}
