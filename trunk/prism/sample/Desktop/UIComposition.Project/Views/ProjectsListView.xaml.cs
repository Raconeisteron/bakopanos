// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Windows.Controls;
using UIComposition.Project.Views;

namespace UIComposition.Project.Views
{
    public partial class ProjectsListView : UserControl
    {
        public ProjectsListView(ProjectsListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}