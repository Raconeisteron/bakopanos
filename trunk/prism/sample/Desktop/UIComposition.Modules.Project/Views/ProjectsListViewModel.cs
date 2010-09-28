// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using UIComposition.Services;

namespace UIComposition.Modules.Project.Views
{
    public class ProjectsListViewModel
    {
        public ProjectsListViewModel(ISelectedEmployeeWorkItem selectedEmployee)
        {
            SelectedEmployee = selectedEmployee;
        }

        public ISelectedEmployeeWorkItem SelectedEmployee { get; private set; }

        public string HeaderInfo
        {
            get { return "Current Projects"; }
        }
    }
}