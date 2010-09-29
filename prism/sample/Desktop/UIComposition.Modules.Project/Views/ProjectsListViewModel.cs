// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Unity;
using UIComposition.Services;

namespace UIComposition.Modules.Project.Views
{
    public class ProjectsListViewModel
    {
        [Dependency]
        public IProjectList List { get; set; }

        public string HeaderInfo
        {
            get { return "Current Projects"; }
        }
    }
}