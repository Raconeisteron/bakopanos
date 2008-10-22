using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Bakopanos.Framework.Data;
using Bakopanos.NW.BusinessObjects;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Bakopanos.NW.DataObjects
{
    public class ProductsDAO : DataAccessObject, IProductsDAO
    {        
        public List<ProductBO> GetAllProducts()
        {
            var list = new List<ProductBO>();

            using (Database.CreateConnection())
            {
                using (IDataReader reader =
                    Database.ExecuteReader(CommandType.Text, "SELECT * FROM PRODUCTS"))
                {
                    while (reader.Read())
                    {
                        var p = new ProductBO
                                    {
                                        ProductID = (int) reader["ProductID"],
                                        ProductName = (string) reader["ProductName"],
                                        Discontinued = (bool) reader["Discontinued"]
                                    };
                        list.Add(p);
                    }
                }
            }

            return list;
        }

        public ProductBO GetSingleProduct(int ProductID)
        {
            var list = new List<ProductBO>();            

            using (Database.CreateConnection())
            {
                using (IDataReader reader =
                    Database.ExecuteReader(CommandType.Text, "SELECT * FROM PRODUCTS WHERE ProductID = " + ProductID))
                {
                    while (reader.Read())
                    {
                        var p = new ProductBO
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = (string)reader["ProductName"],
                            Discontinued = (bool)reader["Discontinued"]
                        };    
                        list.Add(p);
                    }
                }
            }
            
            if (list.Count!=1)
            {
                throw new ApplicationException("Should return a single row");                
            }

            return list[0];
        }
    }
}
