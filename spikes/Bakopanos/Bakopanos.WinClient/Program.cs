using System;
using System.Configuration;
using System.Windows.Forms;
using Bakopanos.WinClient.ProductsModule;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Bakopanos.WinClient
{
    internal static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = new UnityContainer();
            container.RegisterInstance<IUnityContainer>(container);

            var section = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
            section.Containers.Default.Configure(container);

            //set up for sqlserver...
            string connstring = @"Data Source=.\sqlexpress;Initial Catalog=Northwind;Integrated Security=True";
            var db = new SqlDatabase(connstring);
            container.RegisterInstance<Database>(db);

            //main form
            Form form = container.Resolve<MainForm>();

            container.Resolve<IMainModule>().Run();

            Application.Run(form);
        }
    }
}