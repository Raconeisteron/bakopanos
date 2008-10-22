using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bakopanos.NW.BusinessObjects;
using Bakopanos.NW.DataObjects;
using Bakopanos.NW.Facade.Products;
using Bakopanos.NW.WinClient.ProductsModule;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.Unity;

namespace Bakopanos.NW.WinClient
{
    static class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();

            string connstring = @"Data Source=.\sqlexpress;Initial Catalog=Northwind;Integrated Security=True";

            IUnityContainer container = new UnityContainer();
            container.RegisterInstance<IUnityContainer>(container);

            //set up for sqlserver...
            var db = new SqlDatabase(connstring);
            container.RegisterInstance<Database>(db);

            //components
            container.RegisterType<IProductsDAO, ProductsDAO>();
            container.RegisterType<IProductsFacade, ProductsFacade>();

            //model
            container.RegisterInstance<ProductAggregate>(new ProductAggregate(), new ExternallyControlledLifetimeManager());

            //services                        
            
            //main form
            Form form = container.Resolve<MainForm>();

            //controllers
            container.Resolve<ProductsController>();

            Application.Run( form );
            
        }
    }
}