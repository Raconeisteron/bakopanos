using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bakopanos.NW.WinClient
{
    static class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();

            Application.Run(new MainForm());
            
        }
    }
}