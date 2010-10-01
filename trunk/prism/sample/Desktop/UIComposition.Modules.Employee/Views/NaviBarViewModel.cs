// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using UIComposition.Services;

namespace UIComposition.Modules.Employee.Views
{
    public class NaviBarViewModel
    {
        public NaviBarViewModel(IEmployeeWorkItem workItem)
        {
            WorkItem = workItem;
        }
        
        public IEmployeeWorkItem WorkItem { get; private set; }
    }
}