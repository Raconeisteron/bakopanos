// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Unity;
using UIComposition.Services;

namespace UIComposition.Modules.Employee.Views
{
    public class ToolBarViewModel
    {
        //oops, this goes in the employee module...
        [Dependency]
        public IEmployeeWorkItem WorkItem { get; set; }
    }
}