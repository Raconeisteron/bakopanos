using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakopanos.NW.BusinessObjects
{    
    public class ProductBO
    {
        public int ProductID{ get; set;}
        public string ProductName{ get; set;}
        public bool Discontinued { get; set; }
    }
}
