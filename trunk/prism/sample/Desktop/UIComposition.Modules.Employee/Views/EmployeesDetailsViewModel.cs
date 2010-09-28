// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using UIComposition.Services;

namespace UIComposition.Modules.Employee.Views
{
    public class EmployeesDetailsViewModel
    {
        public EmployeesDetailsViewModel(ISelectedEmployeeWorkItem selectedEmployee)
        {
            SelectedEmployee = selectedEmployee;
        }

        public ISelectedEmployeeWorkItem SelectedEmployee { get; set; }
    }
}