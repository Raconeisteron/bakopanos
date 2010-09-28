// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace UIComposition.Contracts
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        ObservableCollection<Employee> RetrieveEmployees();
    }
}