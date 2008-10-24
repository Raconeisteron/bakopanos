using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bakopanos.NW.BusinessObjects;
using Bakopanos.NW.WinClient.ProductsModule;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Bakopanos.Framework.Composite;

namespace Bakopanos.NW.WinClient
{
    static class Program
    {        
        
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = new UnityContainer();
            container.RegisterInstance<IUnityContainer>(container);

            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Containers.Default.Configure(container);

            //set up for sqlserver...
            string connstring = @"Data Source=.\sqlexpress;Initial Catalog=Northwind;Integrated Security=True";
            var db = new SqlDatabase(connstring);
            container.RegisterInstance<Database>(db);
            
            //main form
            Form form = container.Resolve<MainForm>();
            
            container.Resolve<IMainModule>().Run();

            Application.Run( form );
            
        }
    }


    public interface IMainModule:IModule
    {
        IProductsController ProductsController { set; }        
    }

    public class MainModule:IMainModule
    {
        private IProductsController _ProductsController;

        [Dependency]
        public IProductsController ProductsController
        {
            set
            {
                _ProductsController = value;
            }
        }

        public MainModule(IUnityContainer container)
        {            
            container.RegisterType<IProductsController, ProductsController>();            
        }


        public void Run()
        {
            _ProductsController.Run();
        }
    }

}