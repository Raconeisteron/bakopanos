// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using UIComposition.Model;

namespace UIComposition.Services
{
    public interface IEmployeeService
    {
        ObservableCollection<EmployeeItem> RetrieveEmployees();
    }
}