// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.Generic;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace UIComposition.Services
{
    public interface IEmployeeWorkItem : IEmployeeList, IEmployeeInfo, IProjectList
    {
        DelegateCommand<object> ReadCommand { get; }
    }
}