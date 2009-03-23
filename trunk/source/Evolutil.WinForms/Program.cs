using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Evolutil.Library;
using Evolutil.Library.Log;
using Evolutil.ServiceContracts;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Evolutil.WinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IUnityContainer container = new Bootstraper().Start();
                       
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<Form1>());
        }
    }


}
