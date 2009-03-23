using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolutil.Domain;

namespace Evolutil.Data.AdoNet
{
    public class ProductDAO : IProductDAO
    {
        public List<ProductBO> Read()
        {
            return new List<ProductBO>
                       {
                           new ProductBO{ProductID = 1, ProductName = "Costas"}
                       };
        }
    }
}
