// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Windows.Controls;

namespace UIComposition.Employee.Views
{
    /// <summary>
    ///   Interaction logic for ToolBarView.xaml
    /// </summary>
    public partial class NaviBarView : UserControl
    {
        public NaviBarView(NaviBarViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}