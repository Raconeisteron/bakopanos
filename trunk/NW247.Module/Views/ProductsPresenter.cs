using System.Data;
using System.Windows.Data;
using Microsoft.Practices.Unity;
using NW247.Model;
using NW247.Services;

namespace NW247.Module.Views
{
    public class ProductsPresenter : IProductsPresenter
    {
        private readonly IProductsService _Service;
        private readonly NorthwindDataSet.ProductsDataTable _Model;
        private readonly CollectionView _ColView;

        //private IProductsController controller;        

        //[Dependency]
        //private IProductsController Controller
        //{
        //    set { controller = value; }
        //}

        public ProductsPresenter(IProductsService Service, IUnityContainer container)
        {
            _Service = Service;
            
            _Model =_Service.GetProducts();
            container.RegisterInstance(_Model/*, new ContainerControlledLifetimeManager()*/);
                        
            _ColView = CollectionViewSource.GetDefaultView(_Model) as CollectionView;
        }

        #region IProductsPresenter Members

        public void MoveFirst()
        {
            _ColView.MoveCurrentToFirst();
        }

        public void MovePrev()
        {
            if (_ColView.CurrentPosition > 0)
            {
                _ColView.MoveCurrentToPrevious();
            }
        }

        public void MoveNext()
        {
            if (_ColView.CurrentPosition < _ColView.Count - 1)
            {
                _ColView.MoveCurrentToNext();
            }
        }

        public void MoveLast()
        {
            _ColView.MoveCurrentToLast();
        }

        public void Add()
        {
            if (_Model != null)
            {
                NorthwindDataSet.ProductsRow row = _Model.NewProductsRow();
                row.ProductName = "<Name>";
                row.Discontinued = false;
                _Model.AddProductsRow(row);
                _ColView.MoveCurrentToLast();
            }
        }

        public void Delete()
        {
            if (_ColView.CurrentPosition > -1)
            {
                DataRow row = ((DataRowView) _ColView.CurrentItem).Row;
                row.Delete();
            }
        }

        public void Cancel()
        {
            _Model.RejectChanges();
        }

        public void Save()
        {
            if (_Model.DataSet.HasChanges())
            {
                var changes = (NorthwindDataSet.ProductsDataTable) _Model.GetChanges();
                _Service.UpdateAll(changes);
                _Model.Merge(changes);
                _ColView.MoveCurrentToLast();
            }
        }        

        #endregion
    }
}