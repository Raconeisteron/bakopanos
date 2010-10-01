// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using UIComposition.Services;

namespace UIComposition.Modules.Project.Views
{
    public class ProjectsListViewModel
    {
        public ProjectsListViewModel(IProjectList list)
        {
            List = list;
        }
        
        public IProjectList List { get; private set; }

        public string HeaderInfo
        {
            get { return "Current Projects"; }
        }
    }
}