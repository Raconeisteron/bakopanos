using System.Collections.Generic;
using Bakopanos.BusinessObjects;
using Bakopanos.DataObjects;

namespace Bakopanos.Facade.Products
{
    public class ProductsFacade : IProductsFacade
    {
        private readonly IProductsDAO _DAO;

        public ProductsFacade(IProductsDAO DAO)
        {
            _DAO = DAO;
        }

        #region IProductsFacade Members

        public List<ProductBO> Products()
        {
            return new List<ProductBO>(_DAO.GetAllProducts());
        }

        #endregion
    }
}