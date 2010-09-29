// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace UIComposition.Contracts
{
    [ServiceContract]
    public interface IProjectService
    {
        [OperationContract]
        ObservableCollection<Project> RetrieveProjects(int employeeId);
    }
}