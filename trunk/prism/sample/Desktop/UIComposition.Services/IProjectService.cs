// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using UIComposition.BusinessEntities;

namespace UIComposition.Services
{
    public interface IProjectService
    {
        ObservableCollection<ProjectItem> RetrieveProjects();
        ObservableCollection<ProjectItem> RetrieveProjects(int employeeId);
    }
}