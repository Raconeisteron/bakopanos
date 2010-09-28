using System.ComponentModel;

namespace UIComposition.Infrastructure
{
    public static class ViewModelExtensions
    {        
        public static void OnPropertyChanged(this PropertyChangedEventHandler propertyChanged, object instance, string propertyName)
        {            
            PropertyChangedEventHandler handler = propertyChanged;
            if (handler != null) handler(instance, new PropertyChangedEventArgs(propertyName));
        }
    }
}
