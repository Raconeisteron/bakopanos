using System.ComponentModel;
using UIComposition.BusinessEntities;

namespace UIComposition.Services
{
    public interface IProjectInfo : INotifyPropertyChanged
    {
        ProjectItem ProjectItem { get; set; }
    }
}