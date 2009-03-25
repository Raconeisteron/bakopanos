using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Evolutil.Domain.Postsharp;

namespace Evolutil.Domain
{
    [DataContract]
    [NotifyPropertyChangedAspect]
    [EditableObjectAspect]
    public class ProductBO
    {
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public string ProductName { get; set; }

        //public int? SupplierID { get; set; }

        //public int? CategoryID { get; set; }
        [DataMember]        
        public string QuantityPerUnit { get; set; }
        [DataMember]
        public decimal? UnitPrice { get; set; }
        [DataMember]
        public short? UnitsInStock { get; set; }
        [DataMember]
        public short? UnitsOnOrder { get; set; }
        [DataMember]
        public short? ReorderLevel { get; set; }
        [DataMember]
        public bool Discontinued { get; set; }
    }
}
