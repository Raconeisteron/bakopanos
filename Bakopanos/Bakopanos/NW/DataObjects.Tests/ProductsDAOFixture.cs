using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Bakopanos.NW.BusinessObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bakopanos.NW.DataObjects
{
    [TestFixture]
    public class ProductsDAOFixture
    {
        IUnityContainer container;
        private string connstring = @"Data Source=.\sqlexpress;Initial Catalog=Northwind;Integrated Security=True";

        [SetUp]
        public void SetUp()
        {
            container = new UnityContainer();
            //set up for sqlserver...
            var db = new SqlDatabase(connstring);            
            container.RegisterInstance<Database>(db);
            container.RegisterType<IProductsDAO, ProductsDAO>();
        }

        [TearDown]
        public void TearDown()
        {
            container.Dispose();
        }

        [Test]
        public void CanConnectToDatabaseTest()
        {
            var db = container.Resolve<Database>();
            Assert.IsNotNull(db);

            using (db.CreateConnection()){}

        }

        [Test]
        public void GetAllProductsTest()
        {
            var dao = container.Resolve<IProductsDAO>();
            Assert.IsNotNull( dao.GetAllProducts() );
        }

        [Test]
        public void GetSingleProductTest()
        {
            var list = GetProductIDList();
            Assert.IsTrue(list.Count > 0);

            var dao = container.Resolve<IProductsDAO>();

            foreach (var id in list)
            {
                Assert.IsNotNull(dao.GetSingleProduct(id));
            }
        }

        //only a helper... 
        private List<int> GetProductIDList()
        {
            var list = new List<int>();

            var db = container.Resolve<Database>();
            using (db.CreateConnection())
            {
                using (IDataReader reader = db.ExecuteReader(CommandType.Text, "select distinct productid from products"))
                {
                    while (reader.Read())
                    {
                        list.Add( (int)reader["ProductID"] );
                    }
                }
            }

            return list;
        }

    }
}
