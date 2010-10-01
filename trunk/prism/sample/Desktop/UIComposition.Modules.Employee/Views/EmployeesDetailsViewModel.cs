// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Services;

namespace UIComposition.Modules.Employee.Views
{
    public class EmployeesDetailsViewModel
    {
        public EmployeesDetailsViewModel(IEmployeeInfo info)
        {
            Info = info;
        }
        
        public IEmployeeInfo Info
        {
            get; private set;
        }        
    }
}