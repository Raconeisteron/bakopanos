using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DeadDevsSociety.UnityDependencyInjection.DataAccess;
using DeadDevsSociety.UnityDependencyInjection.Domain;

namespace DeadDevsSociety.UnityDependencyInjection.Facade
{
    public class ProductServices
    {
        private readonly ProductRepository _repository = new ProductRepository();

        public IEnumerable<Product> Products(string filter)
        {
            return from item in _repository.List()
                   where (Regex.IsMatch(item.FirstName, filter) |
                        Regex.IsMatch(item.LastName, filter))
                   select item;
        }
    }
}
