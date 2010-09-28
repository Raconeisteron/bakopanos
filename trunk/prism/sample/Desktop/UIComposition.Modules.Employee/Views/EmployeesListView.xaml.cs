// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Windows.Controls;

namespace UIComposition.Modules.Employee.Views
{
    /// <summary>
    ///   Interaction logic for EmployeesListView.xaml
    /// </summary>
    public partial class EmployeesListView : UserControl
    {
        public EmployeesListView(EmployeesListViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}