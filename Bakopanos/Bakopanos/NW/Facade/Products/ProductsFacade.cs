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
        private IProductsDAO _DAO;

        public ProductsFacade(IProductsDAO DAO)
        {
            _DAO = DAO;
        }

        public List<ProductBO> Products()
        {
            return new List<ProductBO>( _DAO.GetAllProducts() );
        }

    }
}
