// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using System.ComponentModel;
using UIComposition.BusinessEntities;

namespace UIComposition.Services
{
    public interface ISelectedEmployeeWorkItem : INotifyPropertyChanged
    {
        EmployeeItem Employee { get; set; }

        ObservableCollection<ProjectItem> Projects { get; set; }
    }
}