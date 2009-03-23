using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolutil.Domain;

namespace Evolutil.Data
{
    public interface IProductDAO
    {
        List<ProductBO> Read();
    }
}
