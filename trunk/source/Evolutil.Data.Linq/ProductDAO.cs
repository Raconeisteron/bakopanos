using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolutil.Data;
using Evolutil.Data.Linq;
using Evolutil.Domain;
using Microsoft.Practices.Unity;

namespace Evolutil.Data.Linq
{
    public class ProductDAO : IProductDAO
    {
        [Dependency]
        public EvolutilDatabase Database { get; set; }
        
        public List<ProductBO> Read()
        {            
            var q = from product in Database.Products 
                    select product.ToProductBO();

            return q.ToList();            
        }
    }

    internal static class PruductDAOExt
    {
        public static ProductBO ToProductBO(this Product product)
        {
            return new ProductBO
                       {
                           ProductID = product.ProductID,
                           ProductName = product.ProductName,
                           //SupplierID = product.SupplierID,
                           //CategoryID = product.CategoryID,
                           QuantityPerUnit = product.QuantityPerUnit,
                           UnitPrice = product.UnitPrice,
                           UnitsInStock = product.UnitsInStock,
                           UnitsOnOrder = product.UnitsOnOrder,
                           ReorderLevel = product.ReorderLevel,
                           Discontinued = product.Discontinued                           
                       };
        }
    }
         
}
