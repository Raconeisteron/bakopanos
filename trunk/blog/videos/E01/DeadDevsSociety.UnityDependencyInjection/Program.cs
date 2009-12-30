using System;
using System.Windows.Forms;
using DeadDevsSociety.UnityDependencyInjection.Gui;

namespace DeadDevsSociety.UnityDependencyInjection
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProductsForm());
        }
    }
}
