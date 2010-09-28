// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using UIComposition.BusinessEntities;

namespace UIComposition.Services
{
    public interface IEmployeeService
    {
        ObservableCollection<EmployeeItem> RetrieveEmployees();
    }
}