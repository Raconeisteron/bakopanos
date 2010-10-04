using System.ComponentModel;

namespace UIComposition.Model
{
    public interface IEmployeeInfo : INotifyPropertyChanged
    {
        EmployeeItem Employee { get; set; }
    }
}