using System.ComponentModel;

namespace UIComposition.Model
{
    public interface IProjectInfo : INotifyPropertyChanged
    {
        ProjectItem ProjectItem { get; set; }
    }
}