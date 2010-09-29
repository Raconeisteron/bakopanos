// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using UIComposition.BusinessEntities;

namespace UIComposition.Services
{
    public interface IEmployeeWorkItem 
    {
        DelegateCommand<object> ReadCommand { get; }
    }
    
    public interface IEmployeeInfo : INotifyPropertyChanged
    {
        EmployeeItem Employee { get; set; }
    }

    public interface IEmployeeList : INotifyPropertyChanged
    {
        ObservableCollection<EmployeeItem> Employees { get; set; }
    }

    public interface IProjectList : INotifyPropertyChanged
    {
        ObservableCollection<ProjectItem> Projects { get; set; }
    }
}