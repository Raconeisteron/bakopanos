using System;
using DeadDevsSociety.UnityDependencyInjection.Presentation;

namespace DeadDevsSociety.UnityDependencyInjection.Launcher
{
    internal static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var presentation = new ProductsView();

            presentation.Show();

            Console.ReadLine();
        }
    }

   
}