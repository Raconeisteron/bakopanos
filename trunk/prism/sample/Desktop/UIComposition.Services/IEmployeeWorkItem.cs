// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using UIComposition.BusinessEntities;

namespace UIComposition.Services
{
    public interface IEmployeeWorkItem : INotifyPropertyChanged
    {
        ObservableCollection<EmployeeItem> Employees { get; set; }

        EmployeeItem Employee { get; set; }

        ObservableCollection<ProjectItem> Projects { get; set; }
        DelegateCommand<object> ReadCommand { get; }
    }
}