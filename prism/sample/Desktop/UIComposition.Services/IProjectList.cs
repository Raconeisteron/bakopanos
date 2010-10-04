using System.Collections.ObjectModel;
using System.ComponentModel;
using UIComposition.BusinessEntities;

namespace UIComposition.Services
{
    public interface IProjectList : INotifyPropertyChanged
    {
        ObservableCollection<ProjectItem> Projects { get; set; }
    }
}