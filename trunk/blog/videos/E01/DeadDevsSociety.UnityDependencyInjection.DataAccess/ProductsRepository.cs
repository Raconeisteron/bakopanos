using System;
using System.Data;
using System.Data.SqlServerCe;
using DeadDevsSociety.UnityDependencyInjection.Domain;
using DeadDevsSociety.UnityDependencyInjection.Library;

namespace DeadDevsSociety.UnityDependencyInjection.DataAccess
{
    public class ProductsRepository : DataFactory<Product>
    {        
        public ProductsRepository() :
            base(new SqlCeCommand("select * from products", 
                new SqlCeConnection(@"Data Source=Database\Database1.sdf")))
        {
        }

        //required allow null == false
        private const string Id = "Id";
        private const string FirstName = "FirstName";
        private const string LastName = "LastName";
        //optional allow null == true
        private const string DateOfBirth = "DateOfBirth";

        protected override Product Mapp(IDataRecord record)
        {            
            var item = new Product
                           {
                               //required allow null == false
                               Id = (int) record[Id],
                               FirstName = record[FirstName] as string,
                               LastName = record[LastName] as string                 
                           };
            LoggerSingleton.Instance.Information(Id + " " + item.Id, "ProductRepository");

            //optional allow null == true
            if (record[DateOfBirth] != DBNull.Value)
            {
                item.DateOfBirth = Convert.ToDateTime(record[DateOfBirth]);
            }
            return item;
        }
    }    
    
}