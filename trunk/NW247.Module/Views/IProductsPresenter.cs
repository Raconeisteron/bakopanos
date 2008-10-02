using System.Windows.Data;
using NW247.Infrastructure;
using NW247.Model;

namespace NW247.Module.Views
{
    public interface IProductsPresenter 
    {                
        void MoveFirst();
        void MovePrev();
        void MoveNext();
        void MoveLast();
        void Add();
        void Delete();
        void Cancel();
        void Save();
    }

    
}