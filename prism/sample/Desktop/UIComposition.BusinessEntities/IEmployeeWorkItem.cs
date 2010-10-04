// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Presentation.Commands;

namespace UIComposition.Model
{
    public interface IEmployeeWorkItem : IEmployeeList, IEmployeeInfo, IProjectList
    {
        DelegateCommand<object> ReadCommand { get; }
    }
}