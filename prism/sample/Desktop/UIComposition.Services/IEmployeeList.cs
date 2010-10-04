using System.Collections.ObjectModel;
using System.ComponentModel;
using UIComposition.BusinessEntities;

namespace UIComposition.Services
{
    public interface IEmployeeList : INotifyPropertyChanged
    {
        ObservableCollection<EmployeeItem> Employees { get; set; }
    }
}