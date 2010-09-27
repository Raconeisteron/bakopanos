using System.ComponentModel;
using UIComposition.BusinessEntities;

namespace UIComposition.Services
{
    public interface IEmployeeContext : INotifyPropertyChanged
    {
        EmployeeItem SelectedEmployee
        {
            get;
            set;
        }
    }
}