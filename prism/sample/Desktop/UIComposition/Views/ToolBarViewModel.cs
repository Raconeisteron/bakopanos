// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Unity;
using UIComposition.Services;

namespace UIComposition.Views
{
    public class ToolBarViewModel
    {
        [Dependency]
        public IEmployeeWorkItem WorkItem { get; set; }
    }
}