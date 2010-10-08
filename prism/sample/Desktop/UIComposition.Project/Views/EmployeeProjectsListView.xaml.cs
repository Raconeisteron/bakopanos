// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Windows.Controls;
using UIComposition.Project.Views;

namespace UIComposition.Project.Views
{
    public partial class EmployeeProjectsListView : UserControl
    {
        public EmployeeProjectsListView(EmployeeProjectsListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}