// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using UIComposition.Model;

namespace UIComposition.Services
{
    public interface IProjectService
    {
        ObservableCollection<ProjectItem> RetrieveProjects();
        ObservableCollection<ProjectItem> RetrieveProjects(int employeeId);
    }
}