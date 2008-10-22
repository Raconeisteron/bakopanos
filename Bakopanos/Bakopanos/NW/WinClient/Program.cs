using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
            //set up for sqlserver...
            var db = new SqlDatabase(connstring);
            container.RegisterInstance<Database>(db);

            Application.Run( container.Resolve<MainForm>() );
            
        }
    }
}