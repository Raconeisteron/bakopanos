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
    public partial class ToolBarView : UserControl
    {
        public ToolBarView(ToolBarViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}