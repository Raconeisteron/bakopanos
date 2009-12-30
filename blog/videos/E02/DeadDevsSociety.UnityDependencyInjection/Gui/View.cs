using System.Windows.Forms;

namespace DeadDevsSociety.UnityDependencyInjection.Gui
{
    public class View : Form
    {
        public object DataSource { set; protected get; }
    }
}