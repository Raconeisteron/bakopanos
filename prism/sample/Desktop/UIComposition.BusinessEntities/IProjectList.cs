using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UIComposition.Model
{
    public interface IProjectList : INotifyPropertyChanged
    {
        ObservableCollection<ProjectItem> Projects { get; set; }
    }
}