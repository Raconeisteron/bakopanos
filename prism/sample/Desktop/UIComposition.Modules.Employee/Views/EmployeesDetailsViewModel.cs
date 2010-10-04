// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Model;
using UIComposition.Services;

namespace UIComposition.Employee.Views
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