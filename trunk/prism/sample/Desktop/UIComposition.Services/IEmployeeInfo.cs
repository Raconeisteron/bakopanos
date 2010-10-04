using System.ComponentModel;
using UIComposition.BusinessEntities;

namespace UIComposition.Services
{
    public interface IEmployeeInfo : INotifyPropertyChanged
    {
        EmployeeItem Employee { get; set; }
    }
}