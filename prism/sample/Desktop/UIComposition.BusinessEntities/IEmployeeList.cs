using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UIComposition.Model
{
    public interface IEmployeeList : INotifyPropertyChanged
    {
        ObservableCollection<EmployeeItem> Employees { get; set; }
    }
}