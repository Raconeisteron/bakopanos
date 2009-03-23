using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Evolutil.Data;
using Evolutil.Domain;
using Evolutil.ServiceContracts;
using Microsoft.Practices.Unity;

namespace Evolutil.Services
{
    public class ProductService : IProductService
    {
        [Dependency]
        public IProductDAO ProductDAO { get; set; }

        public List<ProductBO> Read()
        {             
            return ProductDAO.Read();        
        }
    }
}
