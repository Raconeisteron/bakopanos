// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using UIComposition.BusinessEntities;
using UIComposition.Services;

namespace UIComposition.Modules.Employee.Views
{
    public class EmployeesListViewModel
    {
        public EmployeesListViewModel(IEmployeeService employeeService, ISelectedEmployeeWorkItem selectedEmployee)
        {
            SelectedEmployee = selectedEmployee;
            Employees = employeeService.RetrieveEmployees();
        }

        public ObservableCollection<EmployeeItem> Employees { get; set; }

        public ISelectedEmployeeWorkItem SelectedEmployee { get; set; }
    }
}