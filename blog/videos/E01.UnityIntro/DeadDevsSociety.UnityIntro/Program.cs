using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DeadDevsSociety.PresentationLayer;

namespace DeadDevsSociety.UnityIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            new ProductsView().Show();

            Console.ReadLine();
        }
    }
}
