// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Windows.Controls;

namespace UIComposition.Modules.Project.Views
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