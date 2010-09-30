// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Unity;
using UIComposition.Modules.Employee.Controllers;

namespace UIComposition.Modules.Employee.Views
{
    public class ToolBarViewModel
    {
        [Dependency]
        public IEmployeesController Controller { get; set; }
    }
}