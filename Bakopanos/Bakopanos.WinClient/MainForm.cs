using System.Windows.Forms;
using Microsoft.Practices.Unity;

namespace Bakopanos.WinClient
{
    public partial class MainForm : Form
    {
        public MainForm(IUnityContainer container)
        {
            InitializeComponent();

            container.RegisterInstance("MainWorkspace", workspace);
        }
    }
}