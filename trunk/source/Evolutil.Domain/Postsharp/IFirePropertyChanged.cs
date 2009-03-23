#pragma warning disable 1591 // Ignore missing comments

namespace Evolutil.Domain.Postsharp
{
    public interface IFirePropertyChanged
    {
        void OnPropertyChanged(string propertyName);
    }
}