using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Bakopanos.NW.BusinessObjects;
using Bakopanos.NW.DataObjects;
using Microsoft.Practices.Unity;

namespace Bakopanos.NW.Facade.Products
{
    public class ProductsFacade : IProductsFacade
    {
        [Dependency]
        public ProductsDAO DAO
        {
            private get; set;
        }

        public Collection<ProductBO> Products()
        {
            return new Collection<ProductBO>( DAO.GetAllProducts() );
        }

    }
}
