// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Unity;
using UIComposition.Employee.Controllers;

namespace UIComposition.Employee.Views
{
    public class ToolBarViewModel
    {
        [Dependency]
        public IEmployeesController Controller { get; set; }
    }
}