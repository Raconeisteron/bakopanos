// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Unity;
using UIComposition.Services;

namespace UIComposition.Modules.Employee.Views
{
    public class EmployeesDetailsViewModel
    {
        [Dependency]
        public IEmployeeInfo Info { get; set; }
    }
}