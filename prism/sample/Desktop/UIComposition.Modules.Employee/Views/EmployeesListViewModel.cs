// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Unity;
using UIComposition.Services;

namespace UIComposition.Modules.Employee.Views
{
    public class EmployeesListViewModel
    {
        [Dependency]
        public IEmployeeList List { get; set; }

        [Dependency]
        public IEmployeeInfo Selected { get; set; }
    }
}