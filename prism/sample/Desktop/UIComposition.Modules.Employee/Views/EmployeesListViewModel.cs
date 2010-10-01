// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using UIComposition.Services;

namespace UIComposition.Modules.Employee.Views
{
    public class EmployeesListViewModel
    {
        public EmployeesListViewModel(IEmployeeList list, IEmployeeInfo selected)
        {
            List = list;
            Selected = selected;
        }
        
        public IEmployeeList List { get; private set; }

        public IEmployeeInfo Selected { get; private set; }
    }
}