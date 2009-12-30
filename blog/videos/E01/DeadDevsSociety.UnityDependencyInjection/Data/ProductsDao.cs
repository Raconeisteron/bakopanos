using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using DeadDevsSociety.UnityDependencyInjection.Business;

namespace DeadDevsSociety.UnityDependencyInjection.Data
{
    public class ProductsDao : DataFactory<ProductBo>
    {
        public ProductsDao()
            : base(new SqlCeCommand())
        {
            Command.CommandText = "select * from products";
            string connectionString = ConfigurationManager.AppSettings["connectionstring"];
            Command.Connection = new SqlCeConnection(connectionString);
        }

        public override ProductBo Map(IDataRecord record)
        {
            var item = new ProductBo
                           {
                               FirstName = record["FirstName"] as string,
                               LastName = record["LastName"] as string
                           };

            return item;
        }
    }
}
